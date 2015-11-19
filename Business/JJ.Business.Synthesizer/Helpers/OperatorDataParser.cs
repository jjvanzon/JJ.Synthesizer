using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    internal class OperatorDataParser
    {
        private class Result
        {
            public Result(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; private set; }
            public string Value { get; private set; }
        }

        public static double GetDouble(Operator op, string key)
        {
            double? value = TryGetDouble(op, key);
            if (!value.HasValue)
            {
                throw new Exception(String.Format("Value with key '{0}' in data '{1}' of operator with ID '{2}' is empty.", key, op.Data, op.ID));
            }
            return value.Value;
        }

        public static double? TryGetDouble(Operator op, string key)
        {
            string str = GetString(op, key);
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }

            double value;
            if (!Double.TryParse(str, out value))
            {
                throw new Exception(String.Format("Value with key '{0}' in data '{1}' of operator with ID '{2}' could not be parsed to Double.", key, op.Data, op.ID));
            }
            return value;
        }

        public static int GetInt32(Operator op, string key)
        {
            int? value = TryGetInt32(op, key);
            if (!value.HasValue)
            {
                throw new Exception(String.Format("Value with key '{0}' in data '{1}' of operator with ID '{2}' is empty.", key, op.Data, op.ID));
            }
            return value.Value;
        }

        public static int? TryGetInt32(Operator op, string key)
        {
            string str = GetString(op, key);
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }

            int value;
            if (!Int32.TryParse(str, out value))
            {
                throw new Exception(String.Format("Value with key '{0}' in data '{1}' of operator with ID '{2}' could not be parsed to Int32.", key, op.Data, op.ID));
            }
            return value;
        }


        public static TEnum GetEnum<TEnum>(Operator op, string key)
            where TEnum : struct
        {
            TEnum? value = TryGetEnum<TEnum>(op, key);
            if (!value.HasValue)
            {
                throw new Exception(String.Format("Value with key '{0}' in data '{1}' of operator with ID '{2}' is empty.", key, op.Data, op.ID));
            }
            return value.Value;
        }

        public static TEnum? TryGetEnum<TEnum>(Operator op, string key)
            where TEnum : struct
        {
            string str = GetString(op, key);
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }

            TEnum value;
            if (!Enum.TryParse(str, out value))
            {
                throw new Exception(String.Format("Value with key '{0}' in data '{1}' of operator with ID '{2}' could not be parsed to Enum of type '{3}'.", key, op.Data, op.ID, typeof(TEnum).FullName));
            }
            return value;
        }

        public static string GetString(Operator op, string key)
        {
            if (op == null) throw new NullException(() => op);

            IList<Result> results = Parse(op.Data);

            Result result = results.Where(x => String.Equals(x.Key, key)).FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            return result.Value;
        }

        public static void SetValue(Operator op, string key, object value)
        {
            IList<Result> results = Parse(op.Data);

            // Remove original value.
            results = results.Where(x => !String.Equals(x.Key, key)).ToList();

            // Add new value
            var result = new Result(key, Convert.ToString(value));
            results.Add(result);

            string data = Format(results);

            op.Data = data;
        }

        private static IList<Result> Parse(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                return new Result[0];
            }

            string[] propertiesSplit = data.Split(';');
            IList<Result> results = new List<Result>(propertiesSplit.Length);
            for (int i = 0; i < propertiesSplit.Length; i++)
            {
                string propertyString = propertiesSplit[i];

                Result result = ParseProperty(propertyString, data);

                results.Add(result);
            }

            return results;
        }

        /// <param name="data">For showing in an exception.</param>
        private static Result ParseProperty(string propertyString, string data)
        {
            string[] keyAndValueSplit = propertyString.Split('=');

            if (keyAndValueSplit.Length != 2)
            {
                throw new Exception(String.Format("propertyString in data must have an '=' in it. propertyString = '{0} ', data = '{1}'.", propertyString, data));
            }

            string key = keyAndValueSplit[0];
            string value = keyAndValueSplit[1];

            return new Result(key, value);
        }

        private static string Format(IList<Result> results)
        {
            string str = String.Join(";", results.Select(x => String.Format("{0}={1}", x.Key, x.Value)));
            return str;
        }
    }
}
