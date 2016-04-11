using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class DataPropertyParser
    {
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

        /// <summary>
        /// Whatever I try, I just cannot make the last semi-colon optional.
        /// Not sure what I am doing wrong, so I am going to work around it.
        /// </summary>
        private static Regex _regex_WithExcessiveSemiColonAtTheEnd = CreateRegex_WithExcessiveSemiColonAtTheEnd();

        private static Regex CreateRegex_WithExcessiveSemiColonAtTheEnd()
        {
            var regex = new Regex("^([^;=]+=[^;=]*;)*$", RegexOptions.Compiled);
            return regex;
        }

        public static bool DataIsWellFormed(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            bool dataIsWellFormed = DataIsWellFormed(op.Data);

            return dataIsWellFormed;
        }

        public static bool DataIsWellFormed(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                return true;
            }

            string dataWithExtraSemiColon = data + ";";

            bool isMatch = _regex_WithExcessiveSemiColonAtTheEnd.IsMatch(dataWithExtraSemiColon);
            return isMatch;
        }

        private static CultureInfo _formattingCulture = new CultureInfo("en-US");

        public static CultureInfo FormattingCulture
        {
            get { return _formattingCulture; }
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

        /// <summary>
        /// Returns null if the key is not present or value not filled in.
        /// Will throw an exception if said value cannot be parsed.
        /// </summary>
        public static double? TryGetDouble(Operator op, string key)
        {
            string str = TryGetString(op, key);
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }

            double value;
            if (!DoubleHelper.TryParse(str, _formattingCulture, out value))
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

        /// <summary>
        /// Returns null if the key is not present or value not filled in.
        /// Will throw an exception if said value cannot be parsed.
        /// </summary>
        public static int? TryGetInt32(Operator op, string key)
        {
            string str = TryGetString(op, key);
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
            string str = TryGetString(op, key);
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

        /// <summary>
        /// Returns null if key does not exist.
        /// </summary>
        public static string TryGetString(Operator op, string key)
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

        public static IList<string> GetKeys(Operator op)
        {
            IList<ParsedKeyValuePair> results = Parse(op.Data);
            IList<string> keys = results.Select(x => x.Key).ToArray();
            return keys;
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

        public static void RemoveKey(Operator op, string key)
        {
            IList<ParsedKeyValuePair> results = Parse(op.Data);

            results = results.Where(x => !String.Equals(x.Key, key)).ToList();

            string data = Format(results);

            op.Data = data;
        }

        private static IList<ParsedKeyValuePair> Parse(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                return new ParsedKeyValuePair[0];
            }

            string[] keyValueStrings = data.Split(';');
            IList<ParsedKeyValuePair> results = new List<ParsedKeyValuePair>(keyValueStrings.Length);
            for (int i = 0; i < keyValueStrings.Length; i++)
            {
                string keyValueString = keyValueStrings[i];

                ParsedKeyValuePair result = ParseProperty(keyValueString, data);

                results.Add(result);
            }

            return results;
        }

        /// <param name="data">For showing in an exception.</param>
        private static ParsedKeyValuePair ParseProperty(string keyValueString, string data)
        {
            string[] keyAndValueSplit = keyValueString.Split('=');

            if (keyAndValueSplit.Length != 2)
            {
                throw new Exception(String.Format("keyValueString in data must have an '=' character in it. keyValueString = '{0} ', data = '{1}'.", keyValueString, data));
            }

            string key = keyAndValueSplit[0];
            string value = keyAndValueSplit[1];

            return new ParsedKeyValuePair(key, value);
        }

        private static string Format(IList<ParsedKeyValuePair> parsedKeyValuePairs)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < parsedKeyValuePairs.Count; i++)
            {
                ParsedKeyValuePair parsedKeyValuePair = parsedKeyValuePairs[i];
                AssertParsedKeyValuePair(parsedKeyValuePair);
            }

            string str = String.Join(";", parsedKeyValuePairs.Select(x => String.Format(_formattingCulture, "{0}={1}", x.Key, x.Value)));
            return str;
        }

        private static void AssertParsedKeyValuePair(ParsedKeyValuePair parsedKeyValuePair)
        {
            if (parsedKeyValuePair == null) throw new NullException(() => parsedKeyValuePair);

            AssertKeyOrValue(parsedKeyValuePair.Key);
            AssertKeyOrValue(parsedKeyValuePair.Value);
        }

        private static void AssertKeyOrValue(string keyOrValue)
        {
            if (keyOrValue.Contains(';')) throw new Exception("keyOrValue cannot contain ';' character");
            if (keyOrValue.Contains('=')) throw new Exception("keyOrValue cannot contain '=' character");
        }
    }
}
