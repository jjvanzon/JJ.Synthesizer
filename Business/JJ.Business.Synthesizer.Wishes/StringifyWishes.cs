using System;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;

using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes
{
    // Stringify FluentOutlet
    
    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_stringify"/>
        public string Stringify(bool singleLine = false, bool mustUseShortOperators = false) 
            => _this.Stringify(singleLine, mustUseShortOperators);
    }

    // Stringify Extensions
    
    public static class StringifyExtensionWishes
    { 
        // Operators

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Outlet entity, bool singleLine = false, bool mustUseShortOperators = false)
            => new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(entity);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Operator entity, bool singleLine = false, bool mustUseShortOperators = false)
            => new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(entity);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Inlet entity, bool singleLine = false, bool mustUseShortOperators = false)
            => new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(entity);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this OperatorWrapperBase wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Operator);
        }

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this SampleOperatorWrapper wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Result);
        }

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this CurveInWrapper wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Result);
        }
    }

    // Stringifier

    /// <inheritdoc cref="_stringify"/>
    internal class OperatorStringifier
    {
        private readonly bool _singleLine;
        private readonly bool _mustUseShortOperators;
        private StringBuilderWithIndentation _sb;

        public OperatorStringifier(bool singleLine = false, bool mustUseShortOperators = false)
        {
            _singleLine = singleLine;
            _mustUseShortOperators = mustUseShortOperators;
        }

        // Entry Points

        /// <inheritdoc cref="_stringify"/>
        public string StringifyRecursive(Operator entity)
        {
            _sb = CreateStringBuilder();
            BuildStringRecursive(entity);
            return RemoveOuterBraces(_sb.ToString());
        }

        /// <inheritdoc cref="_stringify"/>
        public string StringifyRecursive(Inlet entity)
        {
            _sb = CreateStringBuilder();
            BuildStringRecursive(entity);
            return RemoveOuterBraces(_sb.ToString());
        }

        /// <inheritdoc cref="_stringify"/>
        public string StringifyRecursive(Outlet outlet)
        {
            _sb = CreateStringBuilder();
            BuildStringRecursive(outlet);
            return RemoveOuterBraces(_sb.ToString());
        }

        // Create StringBuilder

        private StringBuilderWithIndentation CreateStringBuilder()
        {
            if (_singleLine)
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
            bool mustIncludeName = MustIncludeName(op);
            int filledInletCount = GetFilledInletCount(op);
            char separator = GetSeparator(op);

            if (op.IsConst())
            {
                _sb.Append(op.AsConst());
                return;
            }

            if (mustIncludeName)
            {
                _sb.Append(FormatName(op));
            }

            if (filledInletCount != 0)
            {
                _sb.Append('(');
            }

            for (var i = 0; i < filledInletCount; i++)
            {
                Inlet inlet = op.Inlets[i];

                BuildStringRecursive(inlet);

                int isLast = filledInletCount - 1;
                if (i != isLast)
                {
                    _sb.Append(separator);
                }
            }

            if (filledInletCount != 0)
            {
                _sb.Append(')');
            }
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
 
        // Short Notation + - * /

        private bool MustIncludeName(Operator op) => !CanUseShortNotation(op);

        private char GetSeparator(Operator op)
        {
            if (!CanUseShortNotation(op)) return ',';

            if (op.IsAdd()) return '+';
            if (op.IsSubtract()) return '-';
            if (op.IsMultiply()) return '*';
            if (op.IsDivide()) return '/';

            return ',';
        }

        private bool CanUseShortNotation(Operator op)
        {
            if (!_mustUseShortOperators) return false;
            if (op.IsAdd() || op.IsSubtract()) return true;
            if (op.IsMultiply() || op.IsDivide()) return op.Origin() == null;
            return false;
        }

        /// <summary> Returns the amount of inlets filled in, leaving out the last ones not filled in. </summary>
        private int GetFilledInletCount(Operator op) => op.Inlets.TakeWhile(x => x.Input != null).Count();

        // Name Formatting
        
        private static readonly string[] _curveSynonyms = { "Curve", "Envelope", "Bend" };
        private static readonly string[] _sampleSynonyms = { "Sample", "Audio", "Wav" };

        private static string FormatName(Operator op)
        {
            if (op == null) return "null";
            
            var formattedName = op.Name;
            
            if (!NameMentionsOperatorType(formattedName, op.OperatorTypeName))
            {
                formattedName += " " + op.OperatorTypeName;
            }

            return formattedName;
        }

        private static bool NameMentionsOperatorType(string name, string operatorTypeName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            if (name.Contains(operatorTypeName, ignoreCase: true))
            {
                return true;
            }
            
            string operatorTypeDisplayName = PropertyDisplayNames.ResourceManager.GetString(operatorTypeName);
            if (name.Contains(operatorTypeDisplayName, ignoreCase: true))
            {
                return true;
            }
            
            if (operatorTypeName.Contains(_curveSynonyms) && name.Contains(_curveSynonyms))
            {
                return true;
            }
            
            if (operatorTypeName.Contains(_sampleSynonyms) && name.Contains(_sampleSynonyms))
            {
                return true;
            }

            return false;
        }

        // Other Helpers
        
        private static string RemoveOuterBraces(string str)
        {
            // Cut away outer braces
            if (str.StartsWith("(") && str.EndsWith(")"))
            {
                str = str.CutLeft(1).CutRight(1);
            }

            return str;
        }

    }
}