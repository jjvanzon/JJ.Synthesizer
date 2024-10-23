using System;
using System.Text;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.Helpers;

// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._stringify"/>
    internal class OperatorStringifier
    {
        private StringBuilderWithIndentation _sb;

        // Entry Points

        /// <inheritdoc cref="docs._stringify"/>
        public string StringifyRecursive(Operator entity, bool singleLine = false)
        {
            _sb = CreateStringBuilder(singleLine);
            BuildStringRecursive(entity);
            return _sb.ToString();
        }

        /// <inheritdoc cref="docs._stringify"/>
        public string StringifyRecursive(Inlet entity, bool singleLine = false)
        {
            _sb = CreateStringBuilder(singleLine);
            BuildStringRecursive(entity);
            return _sb.ToString();
        }

        /// <inheritdoc cref="docs._stringify"/>
        public string StringifyRecursive(Outlet outlet, bool singleLine = false)
        {
            _sb = CreateStringBuilder(singleLine);
            BuildStringRecursive(outlet);
            return _sb.ToString();
        }

        // Create StringBuilder

        private StringBuilderWithIndentation CreateStringBuilder(bool singleLine)
        {
            if (singleLine)
            {
                return new StringBuilderWithIndentation("", "");
            }
            else
            {
                return new StringBuilderWithIndentation();
            }
        }

        // Recursive String Building

        private void BuildStringRecursive(Outlet outlet)
            => BuildStringRecursive(outlet?.Operator);

        private void BuildStringRecursive(Operator op)
        {
            if (op.IsConst())
            {
                _sb.Append(op.AsConst());
                return;
            }

            _sb.Append($"{op.Name ?? op.OperatorTypeName}");

            if (op.Inlets.Count != 0)
            {
                _sb.Append('(');
            }

            for (var i = 0; i < op.Inlets.Count; i++)
            {
                Inlet inlet = op.Inlets[i];

                BuildStringRecursive(inlet);

                int isLast = op.Inlets.Count - 1;
                if (i != isLast)
                {
                    _sb.Append(',');
                }
            }

            if (op.Inlets.Count != 0)
            {
                _sb.Append(')');
            }
        }

        private void BuildStringRecursive(Inlet inlet)
        {
            if (inlet?.Input?.Operator == null) return;

            //bool mustIncludeName = MustIncludeInletName(inlet);
            //if (mustIncludeName)
            //{
            //    Append($"{inlet.Name}=");
            //}

            if (!inlet.IsConst())
            {
                _sb.Indent();
                _sb.AppendLine();
            }

            BuildStringRecursive(inlet.Input.Operator);

            if (!inlet.IsConst())
            {
                _sb.Outdent();
            }
        }
        //private readonly string[] _simpleOperatorTypeNames =
        //    { "Adder", "Add", "Multiply", "Divide", "Substract" };

        //private bool MustIncludeInletName(Inlet inlet)
        //{
        //    bool isAlone = inlet?.Operator?.Inlets?.Count > 1;
        //    if (isAlone)
        //    {
        //        return false;
        //    }
        //
        //    bool isSimple = _simpleOperatorTypeNames.Contains(inlet?.Input?.Operator.OperatorTypeName) ||
        //                    _simpleOperatorTypeNames.Contains(inlet?.Input?.Operator.Name);
        //    if (isSimple)
        //    {
        //        return false;
        //    }
        //
        //    return true;
        //}

        // String Builder

        private class StringBuilderWithIndentation
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
    }
}