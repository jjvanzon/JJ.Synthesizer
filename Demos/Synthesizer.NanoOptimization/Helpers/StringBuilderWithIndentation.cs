using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers
{
    /// <summary>
    /// TODO: Consider moving this to the framework once it supports has enough methods.
    /// </summary>
    internal class StringBuilderWithIndentation
    {
        public StringBuilderWithIndentation(string tabString)
        {
            _tabString = tabString;
        }

        private readonly string _tabString = "    ";
        private readonly StringBuilder _sb = new StringBuilder();

        public override string ToString() => _sb.ToString();
        public void Indent() => IndentLevel++;
        public void Unindent() => IndentLevel--;
        public void AppendLine() => _sb.AppendLine();
        public void Append(object x) => _sb.Append(x);

        public void AppendLine(string value)
        {
            AppendTabs();

            _sb.Append(value);

            _sb.Append(Environment.NewLine);
        }

        public void AppendFormat(string format, params object[] args)
        {
            _sb.AppendFormat(format, args);
        }

        private int _indentLevel = 0;
        public int IndentLevel
        {
            get { return _indentLevel; }
            set
            {
                if (value < 0) throw new LessThanException(() => value, 0);
                _indentLevel = value;
            }
        }

        public void AppendTabs()
        {
            string tabs = GetTabs();
            _sb.Append(tabs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTabs()
        {
            string tabs = Repeat(_tabString, _indentLevel);
            return tabs;
        }

        /// <summary> TODO: Put this method in framework. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string Repeat(string stringToRepeat, int repeatCount)
        {
            if (stringToRepeat == null) throw new NullException(() => stringToRepeat);

            char[] sourceChars = stringToRepeat.ToCharArray();
            int sourceLength = sourceChars.Length;

            int destLength = sourceLength * repeatCount;
            var destChars = new char[destLength];

            for (int i = 0; i < destLength; i += sourceLength)
            {
                Array.Copy(sourceChars, 0, destChars, i, sourceLength);
            }

            string destString = new string(destChars);
            return destString;
        }
    }
}
