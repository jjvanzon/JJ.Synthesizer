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
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static System.IO.File;
using static System.IO.Path;
using static System.String;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    internal static class LogWishes
    {
        // Pretty Calculation Graphs
        
        public static IList<string> GetSynthLog(Buff buff, double calculationDuration)
        {
            // Get Info
            var stringifiedChannels = new List<string>();

            foreach (var audioFileOutputChannel in buff.UnderlyingAudioFileOutput.AudioFileOutputChannels)
            {
                string stringify = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                stringifiedChannels.Add(stringify);
            }

            // Gather Lines
            var lines = new List<string>();

            lines.Add("");
            lines.Add(GetPrettyTitle(ResolveName(buff)));
            lines.Add("");

            string realTimeComplexityMessage = FormatMetrics(buff.UnderlyingAudioFileOutput.Duration, calculationDuration, buff.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");

            lines.Add($"Calculation time: {PrettyDuration(calculationDuration)}");
            lines.Add($"Audio length: {PrettyDuration(buff.UnderlyingAudioFileOutput.Duration)}");
            lines.Add($"Sampling rate: {buff.UnderlyingAudioFileOutput.SamplingRate} Hz " +
                      $"| {buff.UnderlyingAudioFileOutput.GetSampleDataTypeEnum()} " +
                      $"| {buff.UnderlyingAudioFileOutput.GetSpeakerSetupEnum()}");

            lines.Add("");

            IList<string> warnings = buff.Messages.ToArray();
            if (warnings.Any())
            {
                lines.Add("Warnings:");
                lines.AddRange(warnings.Select(warning => $"- {warning}"));
                lines.Add("");
            }

            for (var i = 0; i < buff.UnderlyingAudioFileOutput.AudioFileOutputChannels.Count; i++)
            {
                var channelString = stringifiedChannels[i];

                lines.Add($"Calculation Channel {i + 1}:");
                lines.Add("");
                lines.Add(channelString);
                lines.Add("");
            }

            if (buff.Bytes != null)
            {
                lines.Add($"{PrettyByteCount(buff.Bytes.Length)} written to memory.");
            }
            if (Exists(buff.FilePath))
            {
                lines.Add($"Output file: {GetFullPath(buff.FilePath)}");
            }

            lines.Add("");

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
                
        public static string GetConfigLog(FlowNode flowNode)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return GetConfigLog(flowNode.SynthWishes);
        }

        public static string GetConfigLog(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return GetConfigLog(synthWishes.Config, synthWishes);
        }
        
        public static string GetConfigLog(Buff buff)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            return GetConfigLog(buff.UnderlyingAudioFileOutput);
        }

        public static string GetConfigLog(AudioInfoWish audioInfoWish)
        {
            string[] descriptors =
            {
                GetTimingDescriptor(audioInfoWish), 
                GetAudioFormatDescriptor(audioInfoWish)
            };
            
            string configLog = Join(NewLine, descriptors.Where(x => !IsNullOrWhiteSpace(x)));
            return configLog;
        }

        public static string GetConfigLog(AudioFileInfo audioFileInfo)
        {
            string[] descriptors =
            {
                GetTimingDescriptor(audioFileInfo), 
                GetAudioFormatDescriptor(audioFileInfo)
            };
            
            string configLog = Join(NewLine, descriptors.Where(x => !IsNullOrWhiteSpace(x)));
            return configLog;
        }

        public static string GetConfigLog(WavHeaderStruct wavHeader)
        {
            string[] descriptors =
            {
                GetTimingDescriptor(wavHeader), 
                GetAudioFormatDescriptor(wavHeader)
            };
            
            string configLog = Join(NewLine, descriptors.Where(x => !IsNullOrWhiteSpace(x)));
            return configLog;
        }
        
        public static string GetConfigLog(ConfigWishes configWishes, SynthWishes synthWishes)
        {
            string[] descriptors =
            {
                GetTimingDescriptor(configWishes, synthWishes), 
                GetAudioFormatDescriptor(configWishes), 
                GetFeaturesDescriptor(configWishes) 
            };
            
            string configLog = Join(NewLine, descriptors.Where(x => !IsNullOrWhiteSpace(x)));
            return configLog;
        }

        public static string GetConfigLog(ConfigSection configSection)
        {
            string[] descriptors =
            {
                GetTimingDescriptor(configSection), 
                GetAudioFormatDescriptor(configSection), 
                GetFeaturesDescriptor(configSection) 
            };
            
            string configLog = Join(NewLine, descriptors.Where(x => !IsNullOrWhiteSpace(x)));
            return configLog;
        }
        
        public static string GetConfigLog(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.Buff != null)
            {
                return GetConfigLog(tape.Buff);
            }

            // Otherwise: Limited data: Duration, Channel, tape.GetChannels(),
            //double? audioLength = tape.Buff tape.Duration?.Value;

            throw new NotImplementedException();
        }
        
        public static string GetConfigLog(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            
             string[] descriptors =
            {
                GetTimingDescriptor(audioFileOutput), 
                GetAudioFormatDescriptor(audioFileOutput)
            };
            
            string configLog = Join(NewLine, descriptors.Where(x => !IsNullOrWhiteSpace(x)));
            return configLog;
        }

        public static string GetConfigLog(Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            
             string[] descriptors =
            {
                GetTimingDescriptor(sample), 
                GetAudioFormatDescriptor(sample)
            };
            
            string configLog = Join(NewLine, descriptors.Where(x => !IsNullOrWhiteSpace(x)));
            return configLog;
        }

        private static string GetTimingDescriptor(ConfigWishes configWishes, SynthWishes synthWishes)
        {
            if (configWishes == null) throw new ArgumentNullException(nameof(configWishes));
            return GetTimingDescriptor(
                configWishes.GetAudioLength(synthWishes).Value,
                configWishes.GetLeadingSilence(synthWishes).Value,
                configWishes.GetTrailingSilence(synthWishes).Value,
                configWishes.GetBarLength(synthWishes).Value,
                configWishes.GetBeatLength(synthWishes).Value,
                configWishes.GetNoteLength(synthWishes).Value);
        }
        
        private static string GetTimingDescriptor(ConfigSection configSection)
        {
            if (configSection == null) throw new ArgumentNullException(nameof(configSection));
            return GetTimingDescriptor(
                configSection.AudioLength,
                configSection.LeadingSilence,
                configSection.TrailingSilence,
                configSection.BarLength,
                configSection.BeatLength,
                configSection.NoteLength);
        }

        private static string GetTimingDescriptor(WavHeaderStruct wavHeader)
            => GetTimingDescriptor(wavHeader.GetAudioLength());
        
        private static string GetTimingDescriptor(AudioFileInfo audioFileInfo)
        {
            if (audioFileInfo == null) throw new ArgumentNullException(nameof(audioFileInfo));
            return GetTimingDescriptor(audioFileInfo.GetAudioLength());
        }
                
        private static string GetTimingDescriptor(AudioInfoWish audioInfoWish)
        {
            if (audioInfoWish == null) throw new ArgumentNullException(nameof(audioInfoWish));
            return GetTimingDescriptor(audioInfoWish.GetAudioLength());
        }

        private static string GetTimingDescriptor(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            return GetTimingDescriptor(audioFileOutput.Duration);
        }

        private static string GetTimingDescriptor(Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return GetTimingDescriptor(sample.GetDuration());
        }

        public static string GetTimingDescriptor(
            double? audioLength = null, double? leadingSilence = null, double? trailingSilence = null, 
            double? barLength = null, double? beatLength = null, double? noteLength = null,
            bool includeCategoryLabel = true)
        {
            var elements = new List<string>();
            
            if (audioLength != null) elements.Add($"Audio length: {PrettyDuration(audioLength)}");
            
            if (leadingSilence != trailingSilence)
            {
                if (leadingSilence != null && leadingSilence != 0)
                {
                    elements.Add($"Leading Silence {PrettyDuration(leadingSilence)}");
                }
                
                if (trailingSilence != null && trailingSilence != 0)
                {
                    elements.Add($"Trailing Silence {PrettyDuration(trailingSilence)}");
                }
            }
            else
            {
                if (leadingSilence != null && leadingSilence != 0)
                {
                    elements.Add($"Padding {PrettyDuration(leadingSilence)}");
                }
            }

            if (barLength != null) elements.Add($"Bar {PrettyDuration(barLength)}");
            if (beatLength != null) elements.Add($"Beat {PrettyDuration(beatLength)}");
            if (noteLength != null) elements.Add($"Note {PrettyDuration(noteLength)}");
            
            string categoryLabel = default;
            if (includeCategoryLabel) categoryLabel = "[Timing] ";
            
            string descriptor = categoryLabel +  Join(" | ", elements);
            return descriptor;
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
                audioFileOutput.GetBits(), 
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
                sample.GetBits(), 
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
                audioFileInfo.GetBits(), 
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
                wavHeader.GetBits(), 
                wavHeader.ChannelCount);
        }

        /// <summary> Example: <code> [Format] Sampling rate: 8192 Hz | 32-Bit | Mono | Wav | Linear Interpolation </code> </summary>
        public static string GetAudioFormatDescriptor(
            int? samplingRate = null, int? bits = null,
            int? channelCount = null, int? channel = null, 
            AudioFileFormatEnum? audioFormat = null, 
            InterpolationTypeEnum? interpolation = null,
            bool includeCategoryLabel = true)
        {
            var elements = new List<string>();

            if (Has(samplingRate)) elements.Add($"Sampling rate: {samplingRate} Hz");
            if (Has(bits)) elements.Add($"{bits}-Bit");
            string channelDescriptor = GetChannelDescriptor(channelCount, channel);
            if (Has(channelDescriptor)) elements.Add(channelDescriptor);
            if (Has(audioFormat)) elements.Add($"{audioFormat}".ToUpper());
            if (Has(interpolation))
            {
                if (interpolation == InterpolationTypeEnum.Line) elements.Add("Linear Interpolation");
                else if (interpolation == InterpolationTypeEnum.Block) elements.Add("Blocky Interpolation");
                else elements.Add($"{interpolation} Interpolation");
            }

            string categoryLabel = default;
            if (includeCategoryLabel) categoryLabel = "[Format] ";
            
            string descriptor = categoryLabel + Join(" | ", elements);
            return descriptor;
        }
        
        private static string GetChannelDescriptor(int? channelCount, int? channel)
        {
            if (!Has(channelCount) && channel == null)
                return default;
            
            if (Has(channelCount) && channel == null) 
            {
                return channelCount == 1 ? "Mono" : channelCount == 2 ? "Stereo" : $"{channelCount} Channels";
            }
            
            if (!Has(channelCount) && channel != null)
            {
                return channel == 0 ? "Left" : channel == 1 ? "Right" : $"Channel {channel}";
            }
            
            if (Has(channelCount) && channel != null)
            {
                if (channelCount == 1)
                {
                    return channel == 0 ? "Mono" : $"Mono | ⚠️ Channel {channel}";
                }
                
                if (channelCount == 2)
                {
                    return channel == 0 ? "Left" : channel == 1 ? "Right" : $"Stereo | ⚠️ Channel {channel}";
                }
                
                return channel < channelCount
                    ? $"{channelCount} Channels | Channel {channel}"
                    : $"{channelCount} Channels | ⚠️ Channel {channel}";
            }
            
            return default;
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

        public static string GetFeaturesDescriptor(
            bool? audioPlayback = false, 
            bool? diskCache = false, 
            bool? mathBoost = false, 
            bool? parallelProcessing = false, 
            bool? playAllTapes = false,
            bool includeCategoryLabel = true)
        {
            var features = new List<string>();

            if (Has(audioPlayback)) features.Add("Audio Playback");
            if (Has(diskCache)) features.Add("Disk Cache");
            if (Has(mathBoost)) features.Add("Math Boost");
            if (Has(parallelProcessing)) features.Add("Parallel Processing");
            if (Has(playAllTapes)) features.Add("Play All Tapes");
            
            string categoryLabel = default;
            if (includeCategoryLabel) categoryLabel = "[Features] ";
            
            string descriptor = categoryLabel + Join(" | ", features);
            return descriptor;
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
                    sb.AppendLine(GetTapeDescriptor(tape));
                }
            }
            sb.AppendLine();
            
            if (multiUseTapes.Length > 0)
            {
                sb.AppendLine("Multi-Use:");
                foreach (var tape in multiUseTapes)
                { 
                    sb.AppendLine(GetTapeDescriptor(tape));
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
                    sb.AppendLine($" => {tape.GetName} ({GetIDDescriptor(tape)}) ..."); 
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
                sb2.Append(GetTapeDescriptor(tape));
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

        private static string GetIDDescriptor(Tape tape) => tape.Signal?.UnderlyingOperator?.ID.ToString() ?? "no ID";
        
        public static string GetTapeDescriptor(Tape tape)
        {
            if (tape == null)
            {
                return "<Tape=null>";
            }
            
            string prefix;
            if (tape.Channel == null) prefix = "(Stereo) ";
            else prefix = $"(Level {tape.NestingLevel}) ";
            
            string nameDescriptor = tape.GetName;
            if (IsNullOrWhiteSpace(nameDescriptor))
            {
                nameDescriptor = "<Untitled>";
            }
            
            // Add flag if true
            var flagStrings = new List<string>();
            if (tape.IsTape) flagStrings.Add("tape");
            if (tape.IsPlay) flagStrings.Add("play");
            if (tape.IsPlayChannel) flagStrings.Add("playc");
            if (tape.IsSave) flagStrings.Add("save");
            if (tape.IsSaveChannel) flagStrings.Add("savec");
            if (tape.IsIntercept) flagStrings.Add("intercept");
            if (tape.IsInterceptChannel) flagStrings.Add("interceptchan");
            if (tape.Callback != null) flagStrings.Add("callback");
            if (tape.ChannelCallback != null) flagStrings.Add("callbackchan");
            if (tape.IsPadding) flagStrings.Add("pad");
            if (tape.Channel.HasValue) flagStrings.Add($"c{tape.Channel}");
            if (tape.Duration != null) flagStrings.Add($"{tape.Duration.Value}s");

            string flagDescriptor = default;
            if (flagStrings.Count > 0)
            {
                flagDescriptor = " {" + Join(",", flagStrings) + "}";
            }
            
            string idDescriptor = $" ({GetIDDescriptor(tape)})";
            
            return prefix + nameDescriptor + flagDescriptor + idDescriptor;
        }

        public static string GetTapeDescriptors(IList<Tape> tapes)
        {
           if (!Has(tapes)) return default;
           string[] tapeDescriptors = tapes.Where(x => x != null).Select(GetTapeDescriptor).ToArray();
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
                return prefix + NewLine + GetTapeDescriptors(tapesLeft);
            }
            else
            {
                return prefix + "<none>";
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
                text += " " + @"""" + GetTapeDescriptor(tape) + @"""";
            }
            
            if (Has(message))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + message;
            }
            return text;
        }
        
        // Math Boost

        public static void LogMathOptimizationTitle()
        {
            Console.WriteLine("");
            Console.WriteLine("Math Boost");
            Console.WriteLine("----------");
            Console.WriteLine("");
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

        internal static string Stringify(string opName, FlowNode a, string mathSymbol, FlowNode b)
            => Stringify(opName, mathSymbol, a, b);
        
        internal static string Stringify(string opName, string mathSymbol, params FlowNode[] operands)
            => Stringify(opName, mathSymbol, (IList<FlowNode>)operands);
        
        internal static string Stringify(string opName, string mathSymbol, IList<FlowNode> operands)
            => $"{opName}({Stringify(mathSymbol, operands)})";
        
        internal static string Stringify(string mathSymbol, IList<FlowNode> operands)
            => Join(" " + mathSymbol + " ", operands.Select(Stringify));
        
        internal static string Stringify(FlowNode operand)
            => operand.Stringify(true);
        
        internal static string Stringify(
            string opName, FlowNode signal, string dimension, string mathSymbol, FlowNode transform)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {Stringify(transform)})";
        
        internal static string Stringify(
            string opName, FlowNode signal, string dimension, string mathSymbol, double value)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {value})";
 
        private static string Pad(string text) => (text ?? "").PadRight(19);
    }
}
