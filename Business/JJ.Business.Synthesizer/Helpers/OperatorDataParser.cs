using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    internal class OperatorDataParser
    {
        private static CultureInfo _formattingCulture = new CultureInfo("en-US");

        private class ParsedKeyValuePair
        {
            public ParsedKeyValuePair(string key, string value)
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
            if (!Double.TryParse(str, NumberStyles.Any, _formattingCulture, out value))
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

        /// <summary> If the property is not present, default(TEnum) is returned. </summary>
        public static TEnum GetEnum<TEnum>(Operator op, string key)
            where TEnum : struct
        {
            string str = GetString(op, key);
            if (String.IsNullOrEmpty(str))
            {
                return default(TEnum);
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

            IList<ParsedKeyValuePair> results = Parse(op.Data);

            ParsedKeyValuePair result = results.Where(x => String.Equals(x.Key, key)).FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            return result.Value;
        }

        public static void SetValue(Operator op, string key, object value)
        {
            IList<ParsedKeyValuePair> results = Parse(op.Data);

            // Remove original value.
            results = results.Where(x => !String.Equals(x.Key, key)).ToList();

            // Add new value
            var result = new ParsedKeyValuePair(key, Convert.ToString(value, _formattingCulture));
            results.Add(result);

            string data = Format(results);

            op.Data = data;
        }

        private static IList<ParsedKeyValuePair> Parse(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                return new ParsedKeyValuePair[0];
            }

            string[] propertiesSplit = data.Split(';');
            IList<ParsedKeyValuePair> results = new List<ParsedKeyValuePair>(propertiesSplit.Length);
            for (int i = 0; i < propertiesSplit.Length; i++)
            {
                string propertyString = propertiesSplit[i];

                ParsedKeyValuePair result = ParseProperty(propertyString, data);

                results.Add(result);
            }

            return results;
        }

        /// <param name="data">For showing in an exception.</param>
        private static ParsedKeyValuePair ParseProperty(string propertyString, string data)
        {
            string[] keyAndValueSplit = propertyString.Split('=');

            if (keyAndValueSplit.Length != 2)
            {
                throw new Exception(String.Format("propertyString in data must have an '=' in it. propertyString = '{0} ', data = '{1}'.", propertyString, data));
            }

            string key = keyAndValueSplit[0];
            string value = keyAndValueSplit[1];

            return new ParsedKeyValuePair(key, value);
        }

        private static string Format(IList<ParsedKeyValuePair> results)
        {
            string str = String.Join(";", results.Select(x => String.Format(_formattingCulture, "{0}={1}", x.Key, x.Value)));
            return str;
        }
    }
}
