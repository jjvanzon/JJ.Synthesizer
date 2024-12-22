using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static System.IO.File;
using static System.IO.Path;
using static System.String;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    public static class LogExtensions
    {
        public static IList<string> SynthLog(this Tape tape, double calculationDuration) => LogWishes.GetSynthLog(tape, calculationDuration);
        public static string Descriptor(this Tape tape) => LogWishes.GetDescriptor(tape);
        
        public static string ConfigLog(this SynthWishes synthWishes) => LogWishes.ConfigLog(synthWishes);
        public static string ConfigLog(this SynthWishes synthWishes, string title) => LogWishes.ConfigLog(title, synthWishes);
        public static string ConfigLog(this SynthWishes synthWishes, string title, string sep) => LogWishes.ConfigLog(title, synthWishes, sep);
        public static string ConfigLog(this FlowNode flowNode) => LogWishes.ConfigLog(flowNode);
        public static string ConfigLog(this FlowNode flowNode, string title) => LogWishes.ConfigLog(title, flowNode);
        public static string ConfigLog(this FlowNode flowNode, string title, string sep) => LogWishes.ConfigLog(title, flowNode, sep);
        public static string ConfigLog(this Buff buff) => LogWishes.ConfigLog(buff);
        public static string ConfigLog(this Buff buff, string title) => LogWishes.ConfigLog(title, buff);
        public static string ConfigLog(this Buff buff, string title, string sep) => LogWishes.ConfigLog(title, buff, sep);
        public static string ConfigLog(this AudioInfoWish audioInfoWish) => LogWishes.ConfigLog(audioInfoWish);
        public static string ConfigLog(this AudioInfoWish audioInfoWish, string title) => LogWishes.ConfigLog(title, audioInfoWish);
        public static string ConfigLog(this AudioInfoWish audioInfoWish, string title, string sep) => LogWishes.ConfigLog(title, audioInfoWish, sep);
        public static string ConfigLog(this AudioFileInfo audioFileInfo) => LogWishes.ConfigLog(audioFileInfo);
        public static string ConfigLog(this AudioFileInfo audioFileInfo, string title) => LogWishes.ConfigLog(title, audioFileInfo);
        public static string ConfigLog(this AudioFileInfo audioFileInfo, string title, string sep) => LogWishes.ConfigLog(title, audioFileInfo, sep);
        public static string ConfigLog(this WavHeaderStruct wavHeader) => LogWishes.ConfigLog(wavHeader);
        public static string ConfigLog(this WavHeaderStruct wavHeader, string title) => LogWishes.ConfigLog(title, wavHeader);
        public static string ConfigLog(this WavHeaderStruct wavHeader, string title, string sep) => LogWishes.ConfigLog(title, wavHeader, sep);
        public static string ConfigLog(this ConfigWishes configWishes) => LogWishes.ConfigLog(configWishes);
        public static string ConfigLog(this ConfigWishes configWishes, SynthWishes synthWishes) => LogWishes.ConfigLog(configWishes, synthWishes);
        public static string ConfigLog(this ConfigWishes configWishes, SynthWishes synthWishes, string sep) => LogWishes.ConfigLog(configWishes, synthWishes, sep);
        public static string ConfigLog(this ConfigWishes configWishes, string title, string sep = " | ") => LogWishes.ConfigLog(title, configWishes, sep);
        public static string ConfigLog(this ConfigWishes configWishes, string title, SynthWishes synthWishes, string sep = " | ") => LogWishes.ConfigLog(title, configWishes, synthWishes, sep);
        internal static string ConfigLog(this ConfigSection configSection) => LogWishes.ConfigLog(configSection);
        internal static string ConfigLog(this ConfigSection configSection, string title) => LogWishes.ConfigLog(title, configSection);
        internal static string ConfigLog(this ConfigSection configSection, string title, string sep) => LogWishes.ConfigLog(title, configSection, sep);
        public static string ConfigLog(this Tape tape) => LogWishes.ConfigLog(tape);
        public static string ConfigLog(this Tape tape, string title) => LogWishes.ConfigLog(title, tape);
        public static string ConfigLog(this Tape tape, string title, string sep) => LogWishes.ConfigLog(title, tape, sep);
        public static string ConfigLog(this AudioFileOutput audioFileOutput) => LogWishes.ConfigLog(audioFileOutput);
        public static string ConfigLog(this AudioFileOutput audioFileOutput, string title) => LogWishes.ConfigLog(title, audioFileOutput);
        public static string ConfigLog(this AudioFileOutput audioFileOutput, string title, string sep) => LogWishes.ConfigLog(title, audioFileOutput, sep);
        public static string ConfigLog(this Sample sample) => LogWishes.ConfigLog(sample);
        public static string ConfigLog(this Sample sample, string title) => LogWishes.ConfigLog(title, sample);
        public static string ConfigLog(this Sample sample, string title, string sep) => LogWishes.ConfigLog(title, sample, sep);
    }
    
    internal static class LogWishes
    {
        // Pretty Calculation Graphs
        
        public static IList<string> GetSynthLog(Tape tape, double calculationDuration)
        {
            var lines = new List<string>();

            // No Tape
            
            if (tape == null)
            {
                lines.Add("");
                lines.Add(PrettyTitle("Synth Log"));
                lines.Add("⚠ Warning: No Tape!");
                return lines;
            }

            // Title
            
            lines.Add("");
            lines.Add(PrettyTitle("Tape: " + GetDescriptor(tape)));
            
            // Properties
            
            lines.Add("");
            lines.Add("Complexity: Ｏ (" + tape.Complexity() + ")");
            lines.Add("Calculation Time: " + PrettyDuration(calculationDuration));
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
            
            var signals = tape.ConcatSignals();
            if (signals.Count > 0)
            {
                for (var i = 0; i < signals.Count; i++)
                {
                    lines.Add("");
                    
                    lines.Add($"Calculation {GetChannelDescriptor(signals.Count, i)}:");
                    lines.Add("");
                    lines.Add($"{signals[i].Stringify()}");
                }
            }
            else
            {
                lines.Add("");
                lines.Add("⚠ Warning: No Signals!");
            }
            
            // Buffer
            
            byte[] bytes = tape.Bytes;
            bool fileExists = Exists(tape.FilePathResolved);
            
            if (Has(bytes))
            {
                lines.Add("");
                // ReSharper disable once PossibleNullReferenceException
                lines.Add($"  {PrettyByteCount(bytes.Length)} written to memory.");
            }

            if (fileExists)
            {
                lines.Add("");
                lines.Add(FormatOutputFile(GetFullPath(tape.FilePathResolved)));
            }

            if (!fileExists && !Has(bytes))
            {
                lines.Add("");
                lines.Add("⚠ Warning: Tape not recorded!");
            }

            return lines;
        }

        public static string FormatMetrics(double audioDuration, double calculationDuration, int complexity)
        {
            string realTimeMessage = FormatRealTimeMessage(audioDuration, calculationDuration);
            string sep = realTimeMessage != default ? " | " : "";
            string complexityMessage = $"Complexity Ｏ ( {complexity} )";
            string metricsMessage = $"{realTimeMessage}{sep}{complexityMessage}";
            return metricsMessage;
        }
        
        public static string FormatRealTimeMessage(double audioDuration, double calculationDuration)
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

        // Config Log
                
        private static string ConfigLog(string title, string group1, string group2 = null, string group3 = null, string sep = null)
        {
            string titleElement = Has(title) ? PrettyTitle(title) + NewLine + NewLine : "";
            string[] groups = { group1, group2, group3 };
            if (!Has(sep, false)) sep = NewLine;
            string log = titleElement + Join(sep, groups.Where(FilledIn));
            return log;
        }

        public static string GetDurationsDescriptor(
            double? audioLength = null, double? leadingSilence = null, double? trailingSilence = null, 
            double? barLength = null, double? beatLength = null, double? noteLength = null)
        {
            var elements = new List<string>();
            
            if (audioLength != null) elements.Add($"{PrettyDuration(audioLength)}");
            if (leadingSilence != trailingSilence)
            {
                if (Has(leadingSilence)) elements.Add($"Leading Silence {leadingSilence:F2}");
                if (Has(trailingSilence)) elements.Add($"Trailing Silence {trailingSilence:F2}");
            }
            else
            {
                if (Has(leadingSilence)) elements.Add($"Pad {leadingSilence:F2}");
            }
            if (barLength != null) elements.Add($"Bar {barLength:F2}");
            if (beatLength != null) elements.Add($"Beat {beatLength:F2}");
            if (noteLength != null) elements.Add($"Note {noteLength:F2}");
            
            string descriptor = Join(" | ", elements);
            return descriptor;
        }
        
        /// <summary> Example: <code> [Format] Sampling rate: 8192 Hz | 32-Bit | Mono | Wav | Linear Interpolation </code> </summary>
        public static string GetAudioFormatDescriptor(
            int? samplingRate = null, int? bits = null,
            int? channelCount = null, int? channel = null, 
            AudioFileFormatEnum? audioFormat = null, 
            InterpolationTypeEnum? interpolation = null)
        {
            var elements = new List<string>();

            if (Has(samplingRate)) elements.Add($"{samplingRate} Hz");
            if (Has(bits)) elements.Add($"{bits}-Bit");
            string channelDescriptor = GetChannelDescriptor(channelCount, channel);
            if (Has(channelDescriptor)) elements.Add(channelDescriptor);
            if (Has(audioFormat)) elements.Add($"{audioFormat}".ToUpper());
            if (Has(interpolation))
            {
                if (interpolation == Line) elements.Add("Linear");
                else if (interpolation == Block) elements.Add("Blocky");
                else elements.Add($"{interpolation} Interpolation");
            }
            
            string descriptor = Join(" | ", elements);
            return descriptor;
        }
        
        public static string GetChannelDescriptor(int? channelCount, int? channel)
        {
            if (!Has(channelCount) && channel == null)
                return default;
            
            if (Has(channelCount) && channel == null)
                return channelCount == 1 ? "Mono" : channelCount == 2 ? "Stereo" : $"{channelCount} Channels";
            
            if (!Has(channelCount) && channel != null)
                return channel == 0 ? "Left" : channel == 1 ? "Right" : $"Channel {channel}";
            
            if (Has(channelCount) && channel != null)
            {
                if (channelCount == 1)
                    return channel == 0 ? "Mono" : $"Mono | ⚠ Channel {channel}";
                
                if (channelCount == 2)
                    return channel == 0 ? "Left" : channel == 1 ? "Right" : $"Stereo | ⚠ Channel {channel}";
                
                return channel < channelCount
                    ? $"{channelCount} Channels | Channel {channel}"
                    : $"{channelCount} Channels | ⚠ Channel {channel}";
            }
            
            return default;
        }
        
        public static string GetFeaturesDescriptor(
            bool? audioPlayback = null, 
            bool? diskCache = null, 
            bool? mathBoost = null, 
            bool? parallelProcessing = null, 
            bool? playAllTapes = null)
        {
            var features = new List<string>();

            if (Has(audioPlayback)) features.Add("Audio Playback"); 
            if (Has(diskCache)) features.Add("Disk Cache");
            if (Has(mathBoost)) features.Add("Math Boost"); 
            if (Has(parallelProcessing)) features.Add("Parallel Processing");
            if (Has(playAllTapes)) features.Add("Play All Tapes");
            
            if (audioPlayback != null && audioPlayback == false) features.Add("⚠ No Audio"); 
            if (mathBoost != null && mathBoost == false) features.Add("⚠ No Math Boost");
            if (parallelProcessing != null && parallelProcessing == false) features.Add("⚠ No Parallel Processing");
            
            string descriptor = Join(" | ", features);
            return descriptor;
        }

        public static string ConfigLog(SynthWishes synthWishes) => ConfigLog("Options", synthWishes, NewLine);
        public static string ConfigLog(SynthWishes synthWishes, string sep) => ConfigLog("Options", synthWishes, sep);
        
        public static string ConfigLog(string title, SynthWishes synthWishes, string sep = default)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return ConfigLog(title, synthWishes.Config, synthWishes, sep);
        }

        public static string ConfigLog(FlowNode flowNode) => ConfigLog("FlowNode Options", flowNode);
        public static string ConfigLog(FlowNode flowNode, string sep) => ConfigLog("FlowNode Options", flowNode, sep);        
        
        public static string ConfigLog(string title, FlowNode flowNode, string sep = " | ")
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return ConfigLog(title, flowNode.SynthWishes, sep);
        }

        public static string ConfigLog(Buff buff) => ConfigLog("", buff);
        public static string ConfigLog(Buff buff, string sep) => ConfigLog("", buff, sep);
        
        public static string ConfigLog(string title, Buff buff, string sep = " | ")
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            if (buff.UnderlyingAudioFileOutput == null) return default;
            return ConfigLog(title, buff.UnderlyingAudioFileOutput, sep);
        }

        public static string ConfigLog(AudioInfoWish audioInfoWish) => ConfigLog("Audio Info", audioInfoWish);
        public static string ConfigLog(AudioInfoWish audioInfoWish, string sep) => ConfigLog("Audio Info", audioInfoWish, sep);
        
        public static string ConfigLog(string title, AudioInfoWish audioInfoWish, string sep = " | ")
        {
            if (!Has(sep, false)) sep = NewLine;
            return ConfigLog(title, GetDurationsDescriptor(audioInfoWish), GetAudioFormatDescriptor(audioInfoWish), sep: sep);
        }

        public static string ConfigLog(AudioFileInfo audioFileInfo) => ConfigLog("Audio Info", audioFileInfo);
        public static string ConfigLog(AudioFileInfo audioFileInfo, string sep) => ConfigLog("Audio Info", audioFileInfo, sep);

        public static string ConfigLog(string title, AudioFileInfo audioFileInfo, string sep = " | ")
        {
            if (!Has(sep, false)) sep = NewLine;
            return ConfigLog(title, GetDurationsDescriptor(audioFileInfo), GetAudioFormatDescriptor(audioFileInfo), sep: sep);
        }

        public static string ConfigLog(WavHeaderStruct wavHeader) => ConfigLog("WAV Header", wavHeader);
        public static string ConfigLog(WavHeaderStruct wavHeader, string sep) => ConfigLog("WAV Header", wavHeader, sep);

        public static string ConfigLog(string title, WavHeaderStruct wavHeader, string sep = " | ")
        {
            if (!Has(sep, false)) sep = NewLine;
            return ConfigLog(title, GetDurationsDescriptor(wavHeader), GetAudioFormatDescriptor(wavHeader), sep: sep);
        }

        public static string ConfigLog(ConfigWishes configWishes) 
            => ConfigLog("", configWishes);
        
        public static string ConfigLog(ConfigWishes configWishes, SynthWishes synthWishes) 
            => ConfigLog("", configWishes, synthWishes);
        
        public static string ConfigLog(ConfigWishes configWishes, SynthWishes synthWishes, string sep) 
            => ConfigLog("", configWishes, synthWishes, sep);
        
        public static string ConfigLog(string title, ConfigWishes configWishes, string sep = " | ")  
            => ConfigLog(title, configWishes, null, sep);

        public static string ConfigLog(string title, ConfigWishes configWishes, SynthWishes synthWishes, string sep = " | ") 
            => ConfigLog(
                title,
                GetAudioFormatDescriptor(configWishes),
                GetFeaturesDescriptor(configWishes),
                Has(synthWishes) ? GetDurationsDescriptor(configWishes, synthWishes) : "",
                sep: sep);

        public static string ConfigLog(ConfigSection configSection) => ConfigLog("", configSection);
        public static string ConfigLog(ConfigSection configSection, string sep) => ConfigLog("", configSection, sep);

        public static string ConfigLog(string title, ConfigSection configSection, string sep = " | ")
            => ConfigLog(
                title,
                GetFeaturesDescriptor(configSection),
                GetAudioFormatDescriptor(configSection),
                GetDurationsDescriptor(configSection),
                sep: sep);

        public static string ConfigLog(Tape tape) => ConfigLog("", tape);
        public static string ConfigLog(Tape tape, string sep) => ConfigLog("", tape, sep);

        public static string ConfigLog(string title, Tape tape, string sep = " | ")
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
                        
            string durationsDescriptor = GetDurationsDescriptor(
                tape.Duration,
                tape.LeadingSilence,
                tape.TrailingSilence);

            string audioFormatDescriptor = GetAudioFormatDescriptor(
                tape.SamplingRate,
                tape.Bits,
                tape.Channels, 
                tape.Channel,
                tape.AudioFormat,
                tape.Interpolation);
            
            string featuresDescriptor = GetFeaturesDescriptor(
                diskCache: tape.CacheToDisk,
                playAllTapes: tape.PlayAllTapes);

            string configLog = ConfigLog(
                title, 
                durationsDescriptor,
                audioFormatDescriptor,
                featuresDescriptor,
                sep: sep);

            return configLog;
        }

        public static string ConfigLog(AudioFileOutput audioFileOutput) => ConfigLog("Audio File Output", audioFileOutput);
        public static string ConfigLog(AudioFileOutput audioFileOutput, string sep) => ConfigLog("Audio File Output", audioFileOutput, sep);

        public static string ConfigLog(string title, AudioFileOutput audioFileOutput, string sep = " | ") 
            => ConfigLog(title, GetDurationsDescriptor(audioFileOutput), GetAudioFormatDescriptor(audioFileOutput), sep: sep);

        public static string ConfigLog(Sample sample) => ConfigLog("", sample);
        public static string ConfigLog(Sample sample, string sep) => ConfigLog("", sample, sep);

        public static string ConfigLog(string title, Sample sample, string sep = " | ") 
            => ConfigLog(title, GetDurationsDescriptor(sample), GetAudioFormatDescriptor(sample), sep: sep);

        private static string GetDurationsDescriptor(ConfigWishes configWishes, SynthWishes synthWishes)
        {
            if (configWishes == null) throw new ArgumentNullException(nameof(configWishes));
            return GetDurationsDescriptor(
                configWishes.GetAudioLength(synthWishes).Value,
                configWishes.GetLeadingSilence(synthWishes).Value,
                configWishes.GetTrailingSilence(synthWishes).Value,
                configWishes.GetBarLength(synthWishes).Value,
                configWishes.GetBeatLength(synthWishes).Value,
                configWishes.GetNoteLength(synthWishes).Value);
        }
        
        private static string GetDurationsDescriptor(ConfigSection configSection)
        {
            if (configSection == null) throw new ArgumentNullException(nameof(configSection));
            return GetDurationsDescriptor(
                configSection.AudioLength,
                configSection.LeadingSilence,
                configSection.TrailingSilence,
                configSection.BarLength,
                configSection.BeatLength,
                configSection.NoteLength);
        }

        private static string GetDurationsDescriptor(WavHeaderStruct wavHeader)
            => GetDurationsDescriptor(wavHeader.AudioLength());
        
        private static string GetDurationsDescriptor(AudioFileInfo audioFileInfo)
        {
            if (audioFileInfo == null) throw new ArgumentNullException(nameof(audioFileInfo));
            return GetDurationsDescriptor(audioFileInfo.AudioLength());
        }
                
        private static string GetDurationsDescriptor(AudioInfoWish audioInfoWish)
        {
            if (audioInfoWish == null) throw new ArgumentNullException(nameof(audioInfoWish));
            return GetDurationsDescriptor(audioInfoWish.AudioLength());
        }

        private static string GetDurationsDescriptor(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            return GetDurationsDescriptor(audioFileOutput.Duration);
        }

        private static string GetDurationsDescriptor(Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return GetDurationsDescriptor(sample.GetDuration());
        }
        
        private static string GetAudioFormatDescriptor(ConfigWishes configWishes)
        {
            if (configWishes == null) throw new ArgumentNullException(nameof(configWishes));
            return GetAudioFormatDescriptor(
                configWishes.GetSamplingRate, 
                configWishes.GetBits, 
                configWishes.GetChannels, 
                configWishes.GetChannel, 
                configWishes.GetAudioFormat,
                configWishes.GetInterpolation);
        }
        
        private static string GetAudioFormatDescriptor(ConfigSection configSection)
        {
            if (configSection == null) throw new ArgumentNullException(nameof(configSection));
            return GetAudioFormatDescriptor(
                configSection.SamplingRate,
                configSection.Bits,
                configSection.Channels,
                channel: null,
                configSection.AudioFormat,
                configSection.Interpolation);
        }
        
        private static string GetAudioFormatDescriptor(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            return GetAudioFormatDescriptor(
                audioFileOutput.SamplingRate, 
                audioFileOutput.Bits(), 
                audioFileOutput.GetChannelCount(), 
                channel: null,
                audioFileOutput.GetAudioFileFormatEnum(), 
                interpolation: null);
        }

        private static string GetAudioFormatDescriptor(Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return GetAudioFormatDescriptor(
                sample.SamplingRate, 
                sample.Bits(), 
                sample.GetChannelCount(), 
                channel: null,
                sample.GetAudioFileFormatEnum(), 
                sample.GetInterpolationTypeEnum());
        }
                
        private static string GetAudioFormatDescriptor(AudioFileInfo audioFileInfo)
        {
            if (audioFileInfo == null) throw new ArgumentNullException(nameof(audioFileInfo));
            return GetAudioFormatDescriptor(
                audioFileInfo.SamplingRate, 
                audioFileInfo.Bits(), 
                audioFileInfo.ChannelCount);
        }

        private static string GetAudioFormatDescriptor(AudioInfoWish audioInfoWish)
        {
            if (audioInfoWish == null) throw new ArgumentNullException(nameof(audioInfoWish));
            return GetAudioFormatDescriptor(
                audioInfoWish.SamplingRate, 
                audioInfoWish.Bits, 
                audioInfoWish.Channels);
        }

        private static string GetAudioFormatDescriptor(WavHeaderStruct wavHeader)
        {
            return GetAudioFormatDescriptor(
                wavHeader.SamplingRate, 
                wavHeader.Bits(), 
                wavHeader.ChannelCount);
        }

        private static string GetFeaturesDescriptor(ConfigWishes configWishes)
        {
            if (configWishes == null) throw new ArgumentNullException(nameof(configWishes));
            return GetFeaturesDescriptor(
                configWishes.GetPlay(), 
                configWishes.GetCacheToDisk,
                configWishes.GetMathBoost, 
                configWishes.GetParallelTaping,
                configWishes.GetPlayAllTapes);
        }
        
        private static string GetFeaturesDescriptor(ConfigSection configSection)
        {
            if (configSection == null) throw new ArgumentNullException(nameof(configSection));
            return GetFeaturesDescriptor(
                configSection.Play, 
                configSection.CacheToDisk, 
                configSection.MathBoost, 
                configSection.ParallelTaping, 
                configSection.PlayAllTapes);
        }
        
        // Tapes
        
        public static string PlotTapeTree(IList<Tape> tapes, bool includeCalculationGraphs = false)
        {
            var sb = new StringBuilderWithIndentationWish("   ", NewLine);
            PlotTapeTree(tapes, sb, includeCalculationGraphs);
            sb.AppendLine();
            return sb.ToString();
        }
        
        private static void PlotTapeTree(IList<Tape> tapes, StringBuilderWithIndentationWish sb, bool includeCalculationGraphs)
        {
            sb.AppendLine("Tape Tree");
            sb.AppendLine("---------");
            sb.AppendLine();
            
            // Handle edge cases
            if (tapes == null) { sb.AppendLine("<Tapes=null>"); return; }
            if (tapes.Count == 0) { sb.AppendLine("<Tapes.Count=0>"); return; }
            if (tapes.Any(x => x == null))
            {
                for (var i = 0; i < tapes.Count; i++)
                {
                    if (tapes[i] == null) sb.AppendLine($"<Tape[{i}]=null>");
                }
                return;
            }
            
            // Get Mail Lists
            
            var roots = tapes.Where(tape => tape.ParentTapes.Count == 0).ToArray();
            var multiUseTapes = tapes.Where(tape => tape.ParentTapes.Count > 1).ToArray();
            
            // Generate List of Main Tapes

            sb.AppendLine("Roots:");
            if (roots.Length == 0)
            {
                sb.AppendLine($"<{tapes.Count} tapes but no roots>");
            }
            else
            {
                foreach (var tape in roots)
                {
                    sb.AppendLine(GetDescriptor(tape));
                }
            }
            sb.AppendLine();
            
            if (multiUseTapes.Length > 0)
            {
                sb.AppendLine("Multi-Use:");
                foreach (var tape in multiUseTapes)
                { 
                    sb.AppendLine(GetDescriptor(tape));
                }
                sb.AppendLine();
            }
            
            if (roots.Length > 0 || multiUseTapes.Length > 0)
            {
                sb.AppendLine("All:");
            }
            
            // Plot Hierarchy
            
            foreach(var tape in roots)
            {
                PlotTapeHierarchyRecursive(tape, sb, includeCalculationGraphs);
            }

            if (multiUseTapes.Length > 0)
            {
                sb.AppendLine();
            
                foreach(var tape in multiUseTapes)
                {
                    PlotTapeHierarchyRecursive(tape, sb, includeCalculationGraphs, skipMultiUse: false);
                }
            }
        }
        
        private static void PlotTapeHierarchyRecursive(
            Tape tape, StringBuilderWithIndentationWish sb, bool includeCalculationGraphs, bool skipMultiUse = true)
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
                    sb.AppendLine($" => {tape.GetName()} ({GetIDDescriptor(tape)}) ..."); 
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
                sb2.Append(GetDescriptor(tape));
                if (includeCalculationGraphs)
                {
                    sb2.Append("   | " + (tape.Signal?.ToString() ?? "<Signal=null>"));
                }
                
                formattedTape = sb2.ToString();
            }
            sb.AppendLine(formattedTape);
            
            foreach (Tape childTape in tape.ChildTapes)
            {
                sb.Indent();
                PlotTapeHierarchyRecursive(childTape, sb, includeCalculationGraphs);
                sb.Outdent();
            }
        }

        private static string GetIDDescriptor(Tape tape)
        {
            var ids = new List<int?>();
           
            if (Has(tape.Signal))
            {
                ids.Add(tape.Signal.UnderlyingOperator?.ID);
            }
            if (Has(tape.Signals))
            {
                ids.AddRange(tape.Signals.Select(x => x?.UnderlyingOperator?.ID));
            }
            
            string formattedIDs = Join("|", ids.Where(FilledIn));
            if (!Has(formattedIDs))
            {
                formattedIDs = "no ID";
            }

            return formattedIDs;
        }
        
        public static string GetDescriptor(Tape tape)
        {
            if (tape == null) return "<Tape=null>";

            string prefix = "";
            if (tape.IsStereo && tape.Channel == null) prefix = "(Stereo) ";
            else if (tape.NestingLevel > 0) prefix = $"(Level {tape.NestingLevel}) ";
            
            string nameDescriptor = tape.GetName();
            if (!Has(nameDescriptor)) nameDescriptor = "<Untitled>";
            
            // Add flag if true
            var flags = new List<string>();
            
            if (tape.IsTape) flags.Add("tape");
            
            if (tape.IsPlayed) flags.Add("played");
            else if (tape.IsPlay) flags.Add("play");
            
            if (tape.ChannelIsPlayed) flags.Add("c-played");
            else if (tape.IsPlayChannel) flags.Add("c-play");
            
            if (tape.IsSaved) flags.Add("saved");
            else if (tape.IsSave) flags.Add("save");
            
            if (tape.ChannelIsSaved) flags.Add("c-saved");
            else if (tape.IsSaveChannel) flags.Add("c-save");
            
            if (tape.IsIntercepted) flags.Add("intercepted");
            else if (tape.IsIntercept) flags.Add("intercept");
            
            if (tape.ChannelIsIntercepted) flags.Add("c-intercepted");
            else if (tape.IsInterceptChannel) flags.Add("c-intercept");
            
            if (tape.Callback != null) flags.Add("callback");
            if (tape.ChannelCallback != null) flags.Add("c-callback");
            
            //if (tape.Channel.HasValue) flags.Add($"c{tape.Channel}");
            flags.Add(GetChannelDescriptor(tape.Channels, tape.Channel)?.ToLower());
            
            if (Has(tape.Duration))
            {
                flags.Add($"{tape.Duration}s");
            }

            if (tape.IsPadded)
            {
                if (tape.LeadingSilence == tape.TrailingSilence)
                {
                    flags.Add($"pad{tape.LeadingSilence:#.##}");
                }
                else
                {
                    flags.Add($"pad{tape.LeadingSilence:#.##},{tape.TrailingSilence:#.##}");
                }
            }

            SynthWishes synthWishes = tape.ConcatSignals().FirstOrDefault()?.SynthWishes;
            if (synthWishes != null)
            {
                if (Has(tape.SamplingRate) && tape.SamplingRate != synthWishes.GetSamplingRate) flags.Add($"{tape.SamplingRate}hz");
                if (Has(tape.Bits) && tape.Bits != synthWishes.GetBits) flags.Add($"{tape.Bits}bit");
                if (Has(tape.AudioFormat) && tape.AudioFormat != synthWishes.GetAudioFormat) flags.Add($"{tape.AudioFormat}".ToLower());
                if (Has(tape.Interpolation) && tape.Interpolation != synthWishes.GetInterpolation) flags.Add($"{tape.Interpolation}".ToLower());
            }

            flags = flags.Where(FilledIn).ToList();
            
            string flagDescriptor = default;
            if (flags.Count > 0)
            {
                flagDescriptor = " {" + Join(",", flags) + "}";
            }
            
            string idDescriptor = $" ({GetIDDescriptor(tape)})";
            
            return prefix + nameDescriptor + flagDescriptor + idDescriptor;
        }

        public static string GetDescriptors(IList<Tape> tapes)
        {
           if (!Has(tapes)) return default;
           string[] tapeDescriptors = tapes.Where(x => x != null).Select(GetDescriptor).ToArray();
           return Join(NewLine, tapeDescriptors);
        }
        
        public static string GetTapesLeftMessage(int todoCount, Tape[] tapesLeft)
        {
            string prefix = default;
            if (todoCount != 0)
            {
                prefix = $"{todoCount} {nameof(Tape)}(s) Left: ";
            }
            
            if (Has(tapesLeft))
            {
                return prefix + NewLine + GetDescriptors(tapesLeft);
            }
            else
            {
                return prefix + "<none>";
            }
        }

        public static void LogPlayAction(Tape tape, string action)
        {
            if (tape.PlayAllTapes)
            {
                LogAction(tape, action + " {all}");
            }
            else
            {
                LogAction(tape, action);
            }
        }

        public static void LogAction(string typeName, string message) 
            => LogActionBase(null, typeName, null, message);
        
        public static string GetActionMessage(string typeName, string message) 
            => GetActionMessage(null, typeName, null, message);
        
        public static void LogAction(string typeName, string action, string message) 
            => LogActionBase(null, typeName, action, message);

        public static string GetActionMessage(string typeName, string action, string message) 
            => GetActionMessage(null, typeName, action, message);

        public static void LogAction(Tape tape, string action) 
            => LogActionBase(tape, null, action, null);

        public static string GetActionMessage(Tape tape, string action) 
            => GetActionMessage(tape, null, action, null);
        
        public static void LogAction(Tape tape, string action, string message) 
            => LogActionBase(tape, null, action, message);
        
        public static string GetActionMessage(Tape tape, string action, string message) 
            => GetActionMessage(tape, null, action, message);
        
        private static void LogActionBase(Tape tape, string typeName, string action, string message)
        {
            string text = GetActionMessage(tape, typeName, action, message);
            Console.WriteLine(text);
        }
        
        public static string GetActionMessage(Tape tape, string typeName, string action, string message)
        {
            if (!Has(typeName)) typeName = nameof(Tape);
            
            string text = PrettyTime() + " [" + typeName.ToUpper() + "]";
            
            if (Has(action))
            {
                text += " " + action;
            }
            
            if (tape != null)
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + '"' + GetDescriptor(tape) + '"';
            }
            
            if (Has(message))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + message;
            }
            return text;
        }

        // Misc
                
        public static void LogSampleCreated(Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            LogAction(nameof(Sample), "Create", $"\"{sample.Name}\" {ConfigLog(sample)}");
        }
                
        public static void LogOutputFile(string filePath, string sourceFilePath = null)
        {
            string message = FormatOutputFile(filePath, sourceFilePath);
            Console.WriteLine(message);
        }
        
        internal static string FormatOutputFile(string filePath, string sourceFilePath = null)
        {
            string prefix = "  ";
            string sourceFileString = default;
            if (Has(sourceFilePath)) sourceFileString += $"(copied {sourceFilePath})";
            string message = prefix + filePath + sourceFileString;
            return message;
        }
        
        // Math Boost

        public static void LogMathBoostTitle(bool mathBoost)
        {
            if (!mathBoost) return;
            Console.WriteLine("");
            Console.WriteLine("Math Boost");
            Console.WriteLine("----------");
            Console.WriteLine("");
        }
        
        public static void LogMathBoostDone(bool mathBoost)
        {
            if (!mathBoost) return;
            //Console.WriteLine("Done");
        }

        public static void LogComputeConstant(
            FlowNode a, string mathSymbol, FlowNode b, FlowNode result,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad("Compute const") + $" : {Stringify(opName, a, mathSymbol, b)} => {Stringify(result)}");
        
        public static void LogIdentityOperation(
            FlowNode a, string mathSymbol, FlowNode identityValue,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad("Identity op") + $" : {Stringify(opName, a, mathSymbol, identityValue)} => {Stringify(a)}");
        
        public static void LogIdentityOperation(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad($"Identity op ({dimension})") + $" : {Stringify(opName, signal, dimension, mathSymbol, transform)} => {Stringify(signal)}");
        
        public static void LogAlwaysOneOptimization(
            FlowNode a, string mathSymbol, FlowNode b,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad("Always 1") + $" : {Stringify(opName, a, mathSymbol, b)} => 1");
        
        public static void LogAlwaysOneOptimization(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad($"Always 1 ({dimension})") + " : " +
                                 $"{Stringify(opName, signal, dimension, mathSymbol, transform)} => " +
                                 $"{Stringify(opName, signal, dimension, "=", 1)}");
        
        public static void LogInvariance(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad($"Invariance ({dimension})") + $" : {Stringify(opName, signal, dimension, mathSymbol, transform)} => {Stringify(signal)}");
        
        public static void LogDivisionByMultiplication(FlowNode a, FlowNode b, FlowNode result)
            => Console.WriteLine(Pad("Div => mul") + $" : {Stringify(a)} / {Stringify(b)} => {Stringify(result)}");
        
        public static void LogDistributeMultiplyOverAddition(FlowNode formulaBefore, FlowNode formulaAfter)
            => Console.WriteLine(Pad("Distribute * over +") + $" : {Stringify(formulaBefore)} => {Stringify(formulaAfter)}");
        
        public static void LogAdditionOptimizations(
            IList<FlowNode> terms, IList<FlowNode> flattenedTerms, IList<FlowNode> optimizedTerms,
            IList<FlowNode> consts, double constant, [CallerMemberName] string opName = null)
        {
            string symbol = "+";
            
            bool wasFlattened = terms.Count != flattenedTerms.Count;
            if (wasFlattened)
            {
                Console.WriteLine(Pad($"Flatten {symbol}") + $" : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, flattenedTerms)}");
            }
            
            bool hasConst0 = consts.Count >= 1 && constant == 0;
            if (hasConst0)
            {
                Console.WriteLine(Pad("Eliminate 0") + $" : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Console.WriteLine(Pad("Compute const") + $" : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool noTermsLeft = terms.Count != 0 && optimizedTerms.Count == 0;
            if (noTermsLeft)
            {
                Console.WriteLine(Pad("0 terms remain") + $" : {Stringify(opName, symbol, terms)} => 0");
            }
            
            bool oneTermLeft = optimizedTerms.Count == 1;
            if (oneTermLeft)
            {
                Console.WriteLine(Pad($"Eliminate {symbol}") + $" : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(symbol, optimizedTerms)}");
            }
        }
        
        public static void LogMultiplicationOptimizations(
            IList<FlowNode> factors, IList<FlowNode> optimizedFactors,
            IList<FlowNode> consts, double constant, [CallerMemberName] string opName = null)
        {
            string symbol = "*";
            
            bool hasConst1 = consts.Count >= 1 && constant == 1;
            if (hasConst1)
            {
                Console.WriteLine(Pad("Eliminate 1") + $" : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }
            
            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Console.WriteLine(Pad("Compute const") + $" : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }
            
            bool noFactorsLeft = factors.Count != 0 && optimizedFactors.Count == 0;
            if (noFactorsLeft)
            {
                Console.WriteLine(Pad("0 factors remain") + $" : {Stringify(opName, symbol, factors)} => 1");
            }
            
            bool oneFactorLeft = optimizedFactors.Count == 1;
            if (oneFactorLeft)
            {
                Console.WriteLine(Pad($"Eliminate {symbol}") + $" : {Stringify(opName, symbol, optimizedFactors)} => {Stringify(symbol, optimizedFactors)}");
            }
        }
        
        // Specialized Stringifications

        private static string Stringify(string opName, FlowNode a, string mathSymbol, FlowNode b)
            => Stringify(opName, mathSymbol, a, b);
        
        private static string Stringify(string opName, string mathSymbol, params FlowNode[] operands)
            => Stringify(opName, mathSymbol, (IList<FlowNode>)operands);
        
        private static string Stringify(string opName, string mathSymbol, IList<FlowNode> operands)
            => $"{opName}({Stringify(mathSymbol, operands)})";
        
        private static string Stringify(string mathSymbol, IList<FlowNode> operands)
            => Join(" " + mathSymbol + " ", operands.Select(Stringify));
        
        private static string Stringify(FlowNode operand)
            => operand.Stringify(true);
        
        private static string Stringify(
            string opName, FlowNode signal, string dimension, string mathSymbol, FlowNode transform)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {Stringify(transform)})";
        
        private static string Stringify(
            string opName, FlowNode signal, string dimension, string mathSymbol, double value)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {value})";
 
        private static string Pad(string text) => (text ?? "").PadRight(19);
    }
}
