using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers
{
    /// <summary>
    /// TODO: Consider moving this to the framework once it supports has enough methods.
    /// </summary>
    internal class StringBuilderWithIndentation
    {
        /// <summary>
        /// TODO: If this is to be generic enough this should be an option, the other option being use actual tab characters.
        /// </summary>
        private const string TAB_AS_SPACES = "    ";

        private readonly StringBuilder _sb = new StringBuilder();

        public override string ToString() => _sb.ToString();

        public void Indent() => IndentLevel++;
        public void Unindent() => IndentLevel--;
        public void AppendLine(string value) => AppendLineWithIndentation(value);
        public void AppendLine() => _sb.AppendLine();
        public void AppendFormat(string format, params object[] args) => _sb.AppendFormat(format, args);
        public void Append(object value) => _sb.Append(value);

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
            _sb.Append(GetTabs());

            // TODO: Remove outcommented code.
            //for (int i = 0; i < _indentLevel; i++)
            //{
            //    _sb.Append(TAB_AS_SPACES);
            //}
        }

        public string GetTabs()
        {
            // TODO: Can I do this slightly faster?
            string tabs = String.Concat(Enumerable.Repeat(TAB_AS_SPACES, _indentLevel));
            return tabs;
        }

        private void AppendLineWithIndentation(string value)
        {
            AppendTabs();

            _sb.Append(value);
            _sb.Append(Environment.NewLine);
        }
    }
}
