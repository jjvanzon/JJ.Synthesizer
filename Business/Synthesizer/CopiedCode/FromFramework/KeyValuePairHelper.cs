using System;
using System.Collections.Generic;

namespace JJ.Framework.Collections
{
	public static class KeyValuePairHelper
	{
		public static IDictionary<string, object> ConvertNamesAndValuesListToDictionary(IList<object> namesAndValues)
		{
			var dictionary = new Dictionary<string, object>();
			ConvertNamesAndValuesListWithDelegate(namesAndValues, (x, y) => dictionary.Add(x, y));
			return dictionary;
		}

		public static IList<KeyValuePair<string, object>> ConvertNamesAndValuesListToKeyValuePairs(IList<object> namesAndValues)
		{
			var list = new List<KeyValuePair<string, object>>();
			ConvertNamesAndValuesListWithDelegate(namesAndValues, (x, y) => list.Add(new KeyValuePair<string,object>(x, y)));
			return list;
		}

		private static void ConvertNamesAndValuesListWithDelegate(IList<object> namesAndValues, Action<string, object> addDelegate)
		{
			// Allow converting null to null.
			if (namesAndValues == null)
			{
				return;
			}

			if (namesAndValues.Count % 2 != 0) throw new Exception("namesAndValues.Count must be a multiple of 2.");

			for (int i = 0; i < namesAndValues.Count; i += 2)
			{
				object name = namesAndValues[i];
				object value = namesAndValues[i + 1];

				if (name == null) throw new Exception("Names in namesAndValues cannot contain nulls.");
				if (name.GetType() != typeof(string)) throw new Exception("Names in namesAndValues must be strings.");

				addDelegate((string)name, value);
			}
		}
	}
}
