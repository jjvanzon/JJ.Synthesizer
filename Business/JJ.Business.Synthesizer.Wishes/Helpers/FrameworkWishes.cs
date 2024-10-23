using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Environment;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class FrameworkWishes
    { 
        public static int CountLines(string str)
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
