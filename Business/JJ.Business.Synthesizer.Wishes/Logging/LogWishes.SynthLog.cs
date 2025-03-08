using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static System.IO.File;
using static System.String;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    internal partial class LogWishes
    {
        // Pretty Calculation Graphs

        public void LogSynth(Tape tape, double? calculationDuration = null) 
            => tape.Log(LogCategories.SynthLog, SynthLog(tape, calculationDuration));
        
        public string SynthLog(Tape tape, double? calculationDuration = null)
        {
            if (!_logger.WillLog(LogCategories.SynthLog)) return "";

            var lines = new List<string>();

            // No Tape
            
            if (tape == null)
            {
                lines.Add("");
                lines.Add(PrettyTitle("Record"));
                lines.Add("⚠ No Tape!");
                return Join(NewLine, lines);
            }

            // Title
            
            lines.Add("");
            lines.Add(PrettyTitle("Record: " + tape.Descriptor()));
            
            // Properties
            
            lines.Add("");
            lines.Add("Complexity: Ｏ (" + tape.Complexity() + ")");
            if (calculationDuration != null) lines.Add("Calculation Time: " + PrettyDuration(calculationDuration));
            lines.Add(ConfigLog(tape));

            // Warnings
            
            var audioFileOutput = tape.UnderlyingAudioFileOutput;
            if (audioFileOutput != null)
            {
                var warnings = audioFileOutput.GetWarnings();
                var warnings2 = audioFileOutput.AudioFileOutputChannels?
                                               .SelectMany(x => x?.Outlet?.GetWarnings())
                                               .ToArray();
                if (warnings2 != null) warnings.AddRange(warnings2);

                if (warnings.Any())
                {
                    lines.Add("");
                    lines.Add("Warnings:");
                    lines.AddRange(warnings.Select(warning => $"⚠ {warning}"));
                }
            }
            
            // Calculation Graphs
            
            var signals = tape.Outlets;
            if (signals.Count <= 0)
            {
                lines.Add("");
                lines.Add("⚠ No Signals!");
            }
            if (signals.Count == 1)
            {
                lines.Add("");
                lines.Add(signals[0].Stringify());
            }
            else
            {
                for (var i = 0; i < signals.Count; i++)
                {
                    lines.Add("");
                    lines.Add(ChannelDescriptor(signals.Count, i) + ":");
                    lines.Add("");
                    lines.Add(signals[i].Stringify());
                }
            }
            
            // Buffer
            
            lines.Add("");
            
            string bytesMessage = tape.MemoryActionMessage();
            if (Has(bytesMessage)) lines.Add(bytesMessage);
            
            string fileMessage = tape.FileActionMessage();
            if (Has(fileMessage)) lines.Add(fileMessage);
            
            if (!Has(tape.Bytes) && !Exists(tape.FilePathResolved))
            {
                lines.Add("⚠ Tape not recorded!");
                lines.Add("");
            }

            return Join(NewLine, lines);
        }
        
        internal string FormatMetrics(double audioDuration, double calculationDuration, int complexity)
        {
            string realTimeMessage = FormatRealTimeMessage(audioDuration, calculationDuration);
            string sep = realTimeMessage != default ? " | " : "";
            string complexityMessage = $"Complexity Ｏ ( {complexity} )";
            string metricsMessage = $"{realTimeMessage}{sep}{complexityMessage}";
            return metricsMessage;
        }
        
        private string FormatRealTimeMessage(double audioDuration, double calculationDuration)
        {
            //var isRunningInTooling = ToolingHelper.IsRunningInTooling;
            //if (isRunningInTooling)
            //{
            //    // If running in tooling, omitting the performance message from the result,
            //    // because it has little meaning with sampling rates  below 150
            //    // that are employed for tooling by default, to keep them running fast.
            //    return default;
            //}

            double realTimePercent = audioDuration / calculationDuration* 100;

            string realTimeStatusGlyph;
            if (realTimePercent < 100)
            {
                realTimeStatusGlyph = "❌";
            }
            else
            {
                realTimeStatusGlyph = "✔";
            }

            var realTimeMessage = $"{realTimeStatusGlyph} {realTimePercent:F0} % Real Time";

            return realTimeMessage;
        }
    }
}

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public string SynthLog(Tape tape, double? calculationDuration = null)
            => ResolveLogging(tape).SynthLog(tape, calculationDuration);
        
        public void LogSynth(Tape tape, double? calculationDuration = null)
            => ResolveLogging(tape).LogSynth(tape, calculationDuration);

        internal string FormatMetrics(double audioDuration, double calculationDuration, int complexity)
            => Logging.FormatMetrics(audioDuration, calculationDuration, complexity);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        // LogSynth
        
        public static void LogSynth(this SynthWishes entity, Tape tape, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(tape, calculationDuration);
        
        public static void LogSynth(this FlowNode entity, Tape tape, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(tape, calculationDuration);
        

        public static void LogSynth(this Tape entity, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(entity, calculationDuration);
        
        public static void LogSynth(this TapeConfig entity, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(entity.Tape, calculationDuration);
        
        public static void LogSynth(this TapeActions entity, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(entity.Tape, calculationDuration);
        
        public static void LogSynth(this TapeAction entity, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(entity.Tape, calculationDuration);
        
        public static void LogSynth(this Buff entity, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(entity.Tape, calculationDuration);
        
        public static void LogSynth(this AudioFileOutput entity, Tape tape, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(tape, calculationDuration);
        
        public static void LogSynth(this Sample entity, Tape tape, double? calculationDuration = null) 
            => ResolveLogging(entity).LogSynth(tape, calculationDuration);

        // SynthLog
        
        public static string SynthLog(this FlowNode entity, Tape tape, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(tape, calculationDuration);
        
        public static string SynthLog(this Tape entity, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(entity, calculationDuration);
        
        public static string SynthLog(this TapeConfig entity, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(entity.Tape, calculationDuration);
        
        public static string SynthLog(this TapeActions entity, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(entity.Tape, calculationDuration);
        
        public static string SynthLog(this TapeAction entity, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(entity.Tape, calculationDuration);
        
        public static string SynthLog(this Buff entity, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(entity.Tape, calculationDuration);
        
        public static string SynthLog(this Buff entity, Tape tape, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(tape, calculationDuration);
        
        public static string SynthLog(this AudioFileOutput entity, Tape tape, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(tape, calculationDuration);
        
        public static string SynthLog(this Sample entity, Tape tape, double? calculationDuration = null) 
            => ResolveLogging(entity).SynthLog(tape, calculationDuration);

        // FormatMetrics
        
        internal static string FormatMetrics(this FlowNode entity, double audioDuration, double calculationDuration, int complexity)
            => ResolveLogging(entity).FormatMetrics(audioDuration, calculationDuration, complexity);
        internal static string FormatMetrics(this Tape entity, double audioDuration, double calculationDuration, int complexity)
            => ResolveLogging(entity).FormatMetrics(audioDuration, calculationDuration, complexity);
        internal static string FormatMetrics(this TapeConfig entity, double audioDuration, double calculationDuration, int complexity)
            => ResolveLogging(entity).FormatMetrics(audioDuration, calculationDuration, complexity);
        internal static string FormatMetrics(this TapeActions entity, double audioDuration, double calculationDuration, int complexity)
            => ResolveLogging(entity).FormatMetrics(audioDuration, calculationDuration, complexity);
        internal static string FormatMetrics(this TapeAction entity, double audioDuration, double calculationDuration, int complexity)
            => ResolveLogging(entity).FormatMetrics(audioDuration, calculationDuration, complexity);
        internal static string FormatMetrics(this Buff entity, double audioDuration, double calculationDuration, int complexity)
            => ResolveLogging(entity).FormatMetrics(audioDuration, calculationDuration, complexity);
        internal static string FormatMetrics(this AudioFileOutput entity, double audioDuration, double calculationDuration, int complexity)
            => ResolveLogging(entity).FormatMetrics(audioDuration, calculationDuration, complexity);
        internal static string FormatMetrics(this Sample entity, double audioDuration, double calculationDuration, int complexity)
            => ResolveLogging(entity).FormatMetrics(audioDuration, calculationDuration, complexity);
    }
}
