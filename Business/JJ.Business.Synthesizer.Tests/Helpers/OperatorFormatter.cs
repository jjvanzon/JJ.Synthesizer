using System;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;
using static System.Environment;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class OperatorFormatter
    {
        private StringBuilder _sb;
        private int tabs;

        private void AppendLine(string line = "")
        {
            _sb.Append(NewLine);
            AppendTabs();
            _sb.Append(line);
        }

        private void AppendTabs()
        {
            for (int i = 0; i < tabs; i++)
            {
                _sb.Append(" ");
            }
        }

        public string FormatRecursive(Outlet outlet)
        {
            _sb = new StringBuilder();
            BuildStringRecursive(outlet);
            return _sb.ToString();
        }

        public string FormatRecursive(Operator op)
        {
            _sb = new StringBuilder();
            BuildStringRecursive(op);
            return _sb.ToString();
        }

        private void BuildStringRecursive(Outlet outlet)
        {
            BuildStringRecursive(outlet?.Operator);
        }

        private void BuildStringRecursive(Operator op)
        {
            double? asConst = op.AsConst();
            if (asConst != null)
            {
                _sb.Append(asConst);
                return;
            }

            _sb.Append($"{op.Name ?? op.OperatorTypeName}");

            bool isMultiLine = op.Inlets.Any(x => x.Input != null && !x.Input.Operator.IsConst());

            if (op.Inlets.Count != 0)
            {
                if (isMultiLine)
                {
                    AppendLine("(");
                    tabs++;
                }
                else _sb.Append('(');
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
                if (isMultiLine)
                {
                    _sb.Append(')');
                    //AppendLine(")");
                    tabs--;
                }
                else 
                    _sb.Append(')');
            }
        }

        private string[] _simpleOperatorTypeNames = {"Adder","Add", "Multiply", "Divide", "Substract"};
        
        private void BuildStringRecursive(Inlet inlet)
        {
            if (inlet?.Input?.Operator == null) return;

            bool mustIncludeName = MustIncludeName(inlet);
            if (mustIncludeName)
            {
                _sb.Append($"{inlet.Name}=");
            }

            BuildStringRecursive(inlet.Input.Operator);
        }

        private bool MustIncludeName(Inlet inlet)
        {
            bool isAlone = inlet?.Operator?.Inlets?.Count > 1;
            if (isAlone)
            {
                return false;
            }

            bool isSimple = _simpleOperatorTypeNames.Contains(inlet?.Operator?.OperatorTypeName) ||
                            _simpleOperatorTypeNames.Contains(inlet?.Operator?.Name);
            if (isSimple)
            {
                return false;
            }

            return true;
        }
    }
}