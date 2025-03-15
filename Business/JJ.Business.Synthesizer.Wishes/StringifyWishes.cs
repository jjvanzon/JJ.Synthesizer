using System;
using System.Linq;
using System.Text;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.OperandWishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Text.Core;
using static System.Environment;
using static System.String;
using static JJ.Framework.Nully.Core.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    // Stringify FlowNode
    
    public partial class FlowNode
    {
        /// <inheritdoc cref="_stringify"/>
        public string Stringify(bool singleLine = false, bool canOmitNameForBasicMath = false) 
            => _underlyingOutlet.Stringify(singleLine, canOmitNameForBasicMath);
    }

    // Stringify Extensions
    
    public static class StringifyExtensionWishes
    { 
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Outlet entity, bool singleLine = false, bool canOmitNameForBasicMath = false)
            => new Stringifier(singleLine, canOmitNameForBasicMath).StringifyRecursive(entity);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Operator entity, bool singleLine = false, bool canOmitNameForBasicMath = false)
            => new Stringifier(singleLine, canOmitNameForBasicMath).StringifyRecursive(entity);
        
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Tape tape, bool singleLine = false, bool canOmitNameForBasicMath = false)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            string monoChannelString = tape.Outlet?.Stringify(singleLine, canOmitNameForBasicMath);
            string leftChannelString = tape.Outlets?.ElementAtOrDefault(0)?.Stringify(singleLine, canOmitNameForBasicMath);
            string rightChannelString = tape.Outlets?.ElementAtOrDefault(1)?.Stringify(singleLine, canOmitNameForBasicMath);

            var sb = new StringBuilder();

            if (Has(monoChannelString))
            {
                sb.AppendLine("Mono Channel:");
                sb.AppendLine();
                sb.AppendLine(monoChannelString);
            }
            
            if (Has(leftChannelString))
            {
                sb.AppendLine("Left Channel:");
                sb.AppendLine();
                sb.AppendLine(leftChannelString);
            }

            if (Has(rightChannelString))
            {
                sb.AppendLine();
                sb.AppendLine("Right Channel:");
                sb.AppendLine();
                sb.AppendLine(rightChannelString);
            }

            return sb.ToString();
        }
        
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Buff buff, bool singleLine = false, bool canOmitNameForBasicMath = false)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            return Stringify(buff.UnderlyingAudioFileOutput, singleLine, canOmitNameForBasicMath);
        }
        
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this AudioFileOutput entity, bool singleLine = false, bool canOmitNameForBasicMath = false)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Join(NewLine, entity.AudioFileOutputChannels.Select(x => x.Stringify(singleLine, canOmitNameForBasicMath)));
        }
        
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this AudioFileOutputChannel entity, bool singleLine = false, bool canOmitNameForBasicMath = false)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Outlet?.Stringify(singleLine, canOmitNameForBasicMath) ?? "";
        }
    }

    // Stringifier

    /// <inheritdoc cref="_stringify"/>
    internal class Stringifier
    {
        private readonly bool _singleLine;
        private readonly bool _canOmitNameForBasicMath;
        internal StringBuilderWithIndentation_Adapted _sb; // Internal for obsolete extension methods

        public Stringifier(bool singleLine = false, bool canOmitNameForBasicMath = false)
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

        // Internal for obsolete extension methods
        internal StringBuilderWithIndentation_Adapted CreateStringBuilder()
        {
            if (_singleLine)
            {
                return new StringBuilderWithIndentation_Adapted("", "");
            }
            else
            {
                return new StringBuilderWithIndentation_Adapted();
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
                Inlet inlet = op.Inlets[i];
                
                BuildStringRecursive(inlet);

                // Conditional separator
                int isLast = filledInletCount - 1;
                if (i != isLast) _sb.Append(separator);
            }

            if (filledInletCount != 0)
            {
                _sb.Append(')');
            }
        }

        // Internal for obsolete extension methods
        internal void BuildStringRecursive(Inlet inlet)
        {
            if (inlet?.Input?.Operator == null) return;

            if (!inlet.IsConst())
            {
                _sb.Indent();
                _sb.AppendEnter();
                _sb.AppendTabs();
            }
            
            BuildStringRecursive(inlet.Input.Operator);

            if (!inlet.IsConst())
            {
                _sb.Unindent();
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
            if (HasCustomName(op))
            {
                return true;
            }
            
            if (!_canOmitNameForBasicMath)
            {
                return true;
            }
            
            if (IsSimpleMath(op))
            {
                if (op.Origin() != null)
                {
                    return true;
                }

                return false;
            }
            
            return true;
        }

        private static bool IsSimpleMath(Operator op) 
            => op.IsAdd() || op.IsSubtract() || op.IsMultiply() || op.IsDivide();

        private bool HasCustomName(Operator op) => !IsNullOrWhiteSpace(op.Name) && 
                                                   !NameIsOperatorTypeName(op);

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
            if (!Has(name))
            {
                return false;
            }

            if (name.Contains(operatorTypeName, ignoreCase: true))
            {
                return true;
            }
            
            if (name.Contains(operatorTypeName.ToDisplayName(), ignoreCase: true))
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

        // Internal for InletStringifyExtensions
        internal static string RemoveOuterBraces(string str)
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