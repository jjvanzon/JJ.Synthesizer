using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.Environment;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        public void LogTapeTree(IList<Tape> tapes, bool includeCalculationGraphs = false)
        {
            Log("TapeTree", GetTapeTree(tapes, includeCalculationGraphs));
        }
        
        public string GetTapeTree(IList<Tape> tapes, bool includeCalculationGraphs = false)
        {
            if (!Enabled) return "";
            var sb = new StringBuilderWithIndentation_Adapted("   ", NewLine);
            PlotTapeTree(tapes, sb, includeCalculationGraphs);
            return sb.ToString();
        }
        
        private void PlotTapeTree(IList<Tape> tapes, StringBuilderWithIndentation_Adapted sb, bool includeCalculationGraphs)
        {
            sb.AppendLine();
            sb.AppendLine(PrettyTitle("Tape Tree"));
            sb.AppendLine();
            
            // Handle edge cases
            if (tapes == null) { sb.AppendLine("<Tapes=null>"); sb.AppendLine(); return; }
            if (tapes.Count == 0) { sb.AppendLine("<Tapes.Count=0>"); sb.AppendLine(); return; }
            if (tapes.Any(x => x == null))
            {
                for (var i = 0; i < tapes.Count; i++)
                {
                    if (tapes[i] == null) sb.AppendLine($"<Tape[{i}]=null>");
                }
                sb.AppendLine();
                return;
            }
            
            // Get Mail Lists
            
            var roots = tapes.Where(tape => tape.ParentTapes.Count == 0).ToArray();
            var multiUseTapes = tapes.Where(tape => tape.ParentTapes.Count > 1).ToArray();
            
            // Generate List of Main Tapes

            sb.AppendLine($"Roots ({roots.Length}):");
            if (roots.Length == 0)
            {
                sb.AppendLine($"<{tapes.Count} tapes but no roots>");
            }
            else
            {
                foreach (var tape in roots)
                {
                    sb.AppendLine(tape.Descriptor);
                }
            }
            sb.AppendLine();
            
            if (multiUseTapes.Length > 0)
            {
                sb.AppendLine($"Multi-Use ({multiUseTapes.Length}):");
                foreach (var tape in multiUseTapes)
                { 
                    sb.AppendLine(tape.Descriptor);
                }
                sb.AppendLine();
            }
            
            if (roots.Length > 0 || multiUseTapes.Length > 0)
            {
                sb.AppendLine($"All ({tapes.Count}):");
            }
            
            // Plot Hierarchy
            
            foreach(var tape in roots)
            {
                PlotTapeTreeRecursive(tape, sb, includeCalculationGraphs);
            }

            if (multiUseTapes.Length > 0)
            {
                sb.AppendLine();
            
                foreach(var tape in multiUseTapes)
                {
                    PlotTapeTreeRecursive(tape, sb, includeCalculationGraphs, skipMultiUse: false);
                }
            }
            
            sb.AppendLine();
        }
        
        private void PlotTapeTreeRecursive(
            Tape tape, StringBuilderWithIndentation_Adapted sb, bool includeCalculationGraphs, bool skipMultiUse = true)
        {
            // Handle edge-cases
            if (tape == null) { sb.AppendLine("<Tape=null>"); return; }
            if (tape.ChildTapes == null) { sb.AppendLine("<Tape.ChildTapes=null)>"); return; }
            if (tape.ParentTapes == null) { sb.AppendLine("<Tape.ParentTapes=null)>"); return; }
            
            bool isMultiUse = tape.ParentTapes.Count > 1;
            if (isMultiUse)
            {
                if (skipMultiUse)
                {
                    // Redirection
                    sb.AppendLine($" => {tape.GetName()} ({tape.IDDescriptor()}) ..."); 
                    return; 
                }
            }

            string formattedTape;
            {
                var sb2 = new StringBuilder();
                if (isMultiUse) 
                {
                    // Continuation
                    sb2.Append("=> ");
                }
                sb2.Append(tape.Descriptor);
                if (includeCalculationGraphs)
                {
                    sb2.Append("   | " + (tape.Outlet?.ToString() ?? "<Signal=null>"));
                }
                
                formattedTape = sb2.ToString();
            }
            sb.AppendLine(formattedTape);
            
            foreach (Tape childTape in tape.ChildTapes)
            {
                sb.Indent();
                PlotTapeTreeRecursive(childTape, sb, includeCalculationGraphs);
                sb.Unindent();
            }
        }
    }

    public static partial class LogExtensionWishes
    {
        // LogTapeTree
        
        public static void LogTapeTree(this SynthWishes entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        
        public static void LogTapeTree(this FlowNode entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        
        internal static void LogTapeTree(this ConfigResolver entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
                
        internal static void LogTapeTree(this ConfigSection entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        
        internal static void LogTapeTree(this Tape entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        
        internal static void LogTapeTree(this TapeConfig entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        
        internal static void LogTapeTree(this TapeActions entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        
        internal static void LogTapeTree(this TapeAction entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        
        internal static void LogTapeTree(this Buff entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);

        public static void LogTapeTree(this IList<Tape> tapes, bool includeCalculationGraphs = false)
            => tapes.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        
        // GetTapeTree

        public static string GetTapeTree(this SynthWishes entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);
        
        public static string GetTapeTree(this FlowNode entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);
        
        internal static string GetTapeTree(this ConfigResolver entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);
                
        internal static string GetTapeTree(this ConfigSection entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);
        
        internal static string GetTapeTree(this Tape entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);
        
        internal static string GetTapeTree(this TapeConfig entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);
        
        internal static string GetTapeTree(this TapeActions entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);
        
        internal static string GetTapeTree(this TapeAction entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);
        
        internal static string GetTapeTree(this Buff entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().GetTapeTree(tapes, includeCalculationGraphs);

        public static string GetTapeTree(this IList<Tape> tapes, bool includeCalculationGraphs = false)
            => tapes.Logging().GetTapeTree(tapes, includeCalculationGraphs);
    }
}

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // For inheritance situations, to avoid `this` qualifiers.
        
        protected string GetTapeTree(IList<Tape> tapes, bool includeCalculationGraphs = false)
            => Logging.GetTapeTree(tapes, includeCalculationGraphs);
        
        protected void LogTapeTree(IList<Tape> tapes, bool includeCalculationGraphs = false)
            => Logging.LogTapeTree(tapes, includeCalculationGraphs);
    }
}
