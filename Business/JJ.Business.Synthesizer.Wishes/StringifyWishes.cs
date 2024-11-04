using System;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;

using static JJ.Business.Synthesizer.Wishes.docs;
#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Wishes
{
    // Stringify FluentOutlet
    
    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_stringify"/>
        public string Stringify(bool singleLine = false, bool mustUseShortOperators = false) 
            => _wrappedOutlet.Stringify(singleLine, mustUseShortOperators);
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
    }

    // Stringifier

    /// <inheritdoc cref="_stringify"/>
    internal partial class OperatorStringifier
    {
        private readonly bool _singleLine;
        private readonly bool _canOmitNameForBasicMath;
        private StringBuilderWithIndentation _sb;

        public OperatorStringifier(bool singleLine = false, bool canOmitNameForBasicMath = false)
        {
            _singleLine = singleLine;
            _canOmitNameForBasicMath = canOmitNameForBasicMath;
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
            if (op.IsConst())
            {
                _sb.Append(op.AsConst());
                return;
            }

            if (NameIsNeeded(op))
            {
                _sb.Append(FormatName(op));
            }

            int filledInletCount = GetFilledInletCount(op);
            if (filledInletCount != 0)
            {
                _sb.Append('(');
            }

            string separator = GetSeparator(op);
            
            for (var i = 0; i < filledInletCount; i++)
            {
                BuildStringRecursive(op.Inlets[i]);

                // Conditional separator
                int isLast = filledInletCount - 1;
                if (i != isLast) _sb.Append(separator);
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
 
        // Omitting Names (for Basic Math)

        /// <summary>
        /// Checks the option for omitting names for basic maths. <br/>
        /// Checks if a custom name is assigned: then it must show the name. <br/>
        /// Then checks for basic operators (<c> + - * / </c>)
        /// Name shouldn't be needed then.<br/>
        /// Except when that pesky deprecated origin parameter is filled in.
        /// Then name is needed again.<br/>
        /// Can result in notation like this:<br/>
        /// <c>Tremolo Multiply( ... * ...)</c><br/>
        /// <c>(Sine(440)*TimeMultiply(Curve,2))</c>
        /// </summary>
        private bool NameIsNeeded(Operator op)
        {
            bool nameIsNeeded = true;

            if (HasCustomName(op))
            {
                return nameIsNeeded;
            }
            
            if (!_canOmitNameForBasicMath)
            {
                return nameIsNeeded;
            }
            
            if (IsSimpleMath(op))
            {
                if (op.Origin() != null)
                {
                    return nameIsNeeded;
                }

                return nameIsNeeded = false;
            }
            
            return nameIsNeeded;
        }

        private static bool IsSimpleMath(Operator op) 
            => op.IsAdd() || op.IsSubtract() || op.IsMultiply() || op.IsDivide();

        private bool HasCustomName(Operator op) => !NameIsOperatorTypeName(op.Name, op.OperatorTypeName);
        
        private bool NameIsOperatorTypeName(string name, string operatorTypeName)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            if (string.Equals(name, operatorTypeName))
            {
                return true;
            }
            
            string operatorTypeDisplayName = PropertyDisplayNames.ResourceManager.GetString(operatorTypeName);
            if (string.Equals(name, operatorTypeDisplayName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        // Separator
        
        /// <summary>
        /// Returns usually ',' but for simple math operations returns + - * or / <br/>
        /// Only if the deprecated Origin operand is used with Multiply or Divide again,
        /// it falls back to the comma notation.
        /// </summary>
        private string GetSeparator(Operator op)
        {
            if (op.IsAdd()) return " + ";
            if (op.IsSubtract()) return " - ";
            if (op.IsMultiply() && op.Origin() == null) return " * ";
            if (op.IsDivide() && op.Origin() == null) return " / ";

            return ",";
        }

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
        
        /// <summary>
        /// Returns the amount of inlets filled in, leaving out the last ones not filled in.
        /// </summary>
        private int GetFilledInletCount(Operator op) => op.Inlets.TakeWhile(x => x.Input != null).Count();

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