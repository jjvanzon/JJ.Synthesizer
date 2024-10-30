using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.Helpers;

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

            _sb.Append(FormatName(op));

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

        private static readonly string[] _curveSynonyms = { "Curve", "Envelope", "Bend" };
        private static readonly string[] _sampleSynonyms = { "Sample", "Audio", "Wav" };

        private static string FormatName(Operator op)
        {
            if (op == null) return "null";
            
            var formattedName = op.Name ?? op.OperatorTypeName;

            // Curves
            if (op.IsCurve() & !formattedName.Contains(_curveSynonyms))
            { 
                formattedName += " Curve";
            }

            // Samples
            if (op.IsSample() & !formattedName.Contains(_sampleSynonyms))
            { 
                formattedName += " Sample";
            }

            return formattedName;
        }

        private void BuildStringRecursive(Inlet inlet)
        {
            if (inlet?.Input?.Operator == null) return;

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
    }
}