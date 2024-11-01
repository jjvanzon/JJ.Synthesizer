using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static System.Environment;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class StringWishes
    { 
        public static int CountLines(this string str)
        {
            // Less efficient:
            //int count = str.Trim().Split(NewLine).Length;
            //int count = 1 + str.Count(c => c == '\n');

            if (str == null) return 0;
                
            int count = 1; // Start with 1 to account for the first line
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '\n') // Platform safe for '\n' or "\r\n".
                {
                    count++;
                }
            }

            if (str.EndsWith(NewLine))
            {
                count--;
            }

            return count;
        }

        public static bool Contains(this string str, string substring, bool ignoreCase = false)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return str.IndexOf(substring, ToStringComparison(ignoreCase)) >= 0;
        }

        public static bool Contains(this string name, string[] words, bool ignoreCase = false)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return words.Any(x => name.IndexOf(x, ToStringComparison(ignoreCase)) >= 0);
        }

        private static StringComparison ToStringComparison(bool ignoreCase) 
            => ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        public static string PrettyDuration(double durationInSeconds) => PrettyTimeSpan(TimeSpan.FromSeconds(durationInSeconds));
        
        public static string PrettyTimeSpan(TimeSpan timeSpan)
        {
            double totalNanoseconds = timeSpan.TotalMilliseconds * 1000;
    
            if (timeSpan.TotalDays >= 1) return $"{timeSpan.TotalDays:0.00} d";
            if (timeSpan.TotalHours >= 1) return $"{timeSpan.TotalHours:0.00} h";
            if (timeSpan.TotalMinutes >= 1) return $"{timeSpan.TotalMinutes:0.00} min";
            if (timeSpan.TotalSeconds >= 1) return $"{timeSpan.TotalSeconds:0.00} s";
            if (timeSpan.TotalMilliseconds >= 1) return $"{timeSpan.TotalMilliseconds:0.00} ms";
    
            return $"{totalNanoseconds:0.00} ns";
        }
        
        public static string PrettyByteCount(long byteCount)
        {
            const double kB = 1024;
            const double MB = kB * 1024;
            const double GB = MB * 1024;

            if (byteCount <= 5 * kB) return $"{byteCount} bytes";
            if (byteCount <= 5 * MB) return $"{byteCount / kB:0} kB";
            if (byteCount <= 5 * GB) return $"{byteCount / MB:0} MB";
            
            return $"{byteCount / GB:0} GB";
        }
        
        public static string WithShortGuids(this string input, int length)
        {
            // Regular expression to find GUIDs in the format `8-4-4-4-12` (e.g., adb97479-aec6-46a5-9767-162bc727bfb8)
            var guidPattern = new Regex(@"\b[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}\b");

            // Replace each matched GUID with a truncated version
            return guidPattern.Replace(input, match =>
            {
                string guid = match.Value;
                return guid.Substring(0, Math.Min(length, guid.Length));
            });
        }
    }

    internal static class CollectionWishes
    { 
        public static TimeSpan Sum(this IEnumerable<TimeSpan> timeSpans)
        {
            if (timeSpans == null) throw new ArgumentNullException(nameof(timeSpans));
            return timeSpans.Aggregate((x, y) => x + y);
        }
        
        public static TimeSpan Sum<T>(this IEnumerable<T> source, Func<T, TimeSpan> selector)
        {
            return source.Select(selector).Sum();
        }
    }

    internal class StringBuilderWithIndentation
    {
        private readonly StringBuilder _sb = new StringBuilder();

        private readonly string _tabString;
        private readonly string _enter;
        private int tabCount;

        public StringBuilderWithIndentation()
            : this("  ", Environment.NewLine)
        { }

        public StringBuilderWithIndentation(string tabString, string enter)
        {
            _tabString = tabString;
            _enter = enter;
        }

        public void Append(object obj) => _sb.Append(obj);
        public void Append(string str) => _sb.Append(str);
        public void Append(char chr) => _sb.Append(chr);

        public void AppendLine(string line = "")
        {
            Append(_enter);
            AppendTabs();
            Append(line);
        }

        private void AppendTabs()
        {
            for (int i = 0; i < tabCount; i++)
            {
                _sb.Append(_tabString);
            }
        }

        public void Outdent() => tabCount--;

        public void Indent() => tabCount++;

        public override string ToString() => _sb.ToString();
    }

    internal static class CopiedFromFramework
    {
        public static bool IsProperty(this MethodBase method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));

            bool isProperty = method.Name.StartsWith("get_") ||
                              method.Name.StartsWith("set_");

            return isProperty;
        }
        
        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static double Product<TSource>(this IEnumerable<TSource> collection, Func<TSource, double> selector)
            => collection.Select(selector).Product();

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// </summary>
        public static double Product(this IEnumerable<double> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            double product = collection.FirstOrDefault();

            foreach (double value in collection.Skip(1))
            {
                product *= value;
            }

            return product;
        }
    }
}
