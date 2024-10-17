using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public static class OperatorFormatter
    {
        public static string FormatRecursive(Outlet outlet)
        {
            var sb = new StringBuilder();
            BuildStringRecursive(outlet, sb);
            return sb.ToString();
        }

        public static string FormatRecursive(Operator op)
        {
            var sb = new StringBuilder();
            BuildStringRecursive(op, sb);
            return sb.ToString();
        }

        private static void BuildStringRecursive(Outlet outlet, StringBuilder sb)
        {
            BuildStringRecursive(outlet?.Operator, sb);
        }

        private static void BuildStringRecursive(Operator op, StringBuilder sb)
        {
            double? asConst = op.AsConst();
            if (asConst != null)
            {
                sb.Append(asConst);
                return;
            }

            sb.Append($"{op.Name ?? op.OperatorTypeName}");

            bool isMultiLine = op.Inlets.Any(x => x.Input != null && !x.Input.Operator.IsConst());

            if (op.Inlets.Count != 0)
            {
                if (isMultiLine)
                {
                    sb.AppendLine();
                    sb.AppendLine("(");
                }
                else sb.Append('(');
            }

            for (var i = 0; i < op.Inlets.Count; i++)
            {
                Inlet inlet = op.Inlets[i];
                
                BuildStringRecursive(inlet, sb);
                
                int isLast = op.Inlets.Count - 1;
                if (i != isLast)
                {
                    sb.Append(',');
                }
            }

            if (op.Inlets.Count != 0)
            {
                //if (isMultiLine)
                //{
                //    sb.AppendLine();
                //    sb.AppendLine(")");
                //}
                //else 
                sb.Append(')');
            }
        }

        private static void BuildStringRecursive(Inlet inlet, StringBuilder sb)
        {
            if (inlet?.Input?.Operator == null) return;

            //sb.Append($"{inlet.Name}");

            BuildStringRecursive(inlet.Input.Operator, sb);
        }
    }
}