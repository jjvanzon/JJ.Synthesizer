using System;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;
using static System.Environment;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class OperatorFormatter
    {
        private readonly string[] _simpleOperatorTypeNames = 
            {"Adder","Add", "Multiply", "Divide", "Substract"};

        // Entry Points
        
        public string FormatRecursive(Operator entity)
        {
            _sb = new StringBuilder();
            BuildStringRecursive(entity);
            return _sb.ToString();
        }
        
        public string FormatRecursive(Inlet entity)
        {
            _sb = new StringBuilder();
            BuildStringRecursive(entity, out bool multiLine);
            return _sb.ToString();
        }
        
        public string FormatRecursive(Outlet outlet)
        {
            _sb = new StringBuilder();
            BuildStringRecursive(outlet);
            return _sb.ToString();
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

            Append($"{op.Name ?? op.OperatorTypeName}");

            if (op.Inlets.Count != 0)
            {
                Append('(');
            }

            bool multiLine = false;
            
            for (var i = 0; i < op.Inlets.Count; i++)
            {
                Inlet inlet = op.Inlets[i];

                BuildStringRecursive(inlet, out multiLine);
                
                int isLast = op.Inlets.Count - 1;
                if (i != isLast)
                {
                    Append(',');
                }
            }

            if (op.Inlets.Count != 0)
            {
                Append(')');
            }
            
            if (multiLine) AppendLine();
        }
        
        private void BuildStringRecursive(Inlet inlet, out bool multiLine)
        {
            multiLine = false;
            
            if (inlet?.Input?.Operator == null) return;
            
            bool mustIncludeName = MustIncludeName(inlet);
            if (mustIncludeName)
            {
                Append($"{inlet.Name}=");
            }

            if (!inlet.IsConst())
            {
                Indent();
                AppendLine();
                multiLine = true;
            }

            BuildStringRecursive(inlet.Input.Operator);

            if (!inlet.IsConst())
            {
                Outdent();
            }
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
        
        // String Builder
        
        private StringBuilder _sb;
        private int tabCount;

        private void Append(string str) => _sb.Append(str);
        private void Append(char chr) => _sb.Append(chr);

        
        private void AppendLine(string line = "")
        {
            Append(NewLine);
            AppendTabs();
            Append(line);
        }

        private void AppendTabs()
        {
            for (int i = 0; i < tabCount; i++)
            {
                _sb.Append("  ");
            }
        }
        
        void Outdent() => tabCount--;

        void Indent() => tabCount++;
    }
}