using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Logging;
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static System.IO.File;
using static System.String;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.FilledInHelper;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.Wishes.Logging.LoggingFactory;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        private LogWishes Logging { get; } = new LogWishes();

        public SynthWishes WithLogging(bool enabled = true) { Logging.LoggingEnabled = enabled; return this; }
        
        // Basics
        
        public void Log(string message = null)
            => Logging.Log(message);
        
        public void LogTitleStrong(string title)
            => Logging.LogTitleStrong(title);
        
        public void LogTitle(string title)
            => Logging.LogTitle(title);
        
        public void LogOutputFile(string filePath, string sourceFilePath = null)
            => Logging.LogOutputFile(filePath, sourceFilePath);
        
        // Config
        
        public void LogConfig()
            => Logging.LogConfig(this);
        
        public void LogConfig(string title, string sep = default)
            => Logging.LogConfig(title, this, sep);
        
        // Actions
        
        internal void LogAction(TapeAction action, string message = null)
            => Logging.LogAction(action, message);
        
        internal void LogAction(Tape entity, string action, string message = null)
            => Logging.LogAction(entity, action, message);
        
        internal void LogAction(Buff entity, string action, string message = null)
            => Logging.LogAction(entity, action, message);
        
        internal void LogAction(Sample entity, string action, string message = null)
            => Logging.LogAction(entity, action, message);
        
        internal void LogAction(AudioFileOutput entity, string action, string message = null)
            => Logging.LogAction(entity, action, message);
        
        internal void LogAction(FlowNode entity, string action, string message = null)
            => Logging.LogAction(entity, action, message);
        
        public void LogAction(object entity, string action, string name, string message = null)
            => Logging.LogAction(entity, action, name, message);
        
        internal void LogAction(string typeName, string message)
            => Logging.LogAction(typeName, message);
        
        internal void LogAction(string typeName, string action, string message)
            => Logging.LogAction(typeName, action, message);
        
        internal void LogAction(string typeName, string action, string objectName, string message)
            => Logging.LogAction(typeName, action, objectName, message);
        
        // Math
        
        internal void LogMathBoostTitle(bool mathBoost)
            => Logging.LogMathBoostTitle(mathBoost);
        
        internal void LogMathBoostDone(bool mathBoost) 
            => Logging.LogMathBoostDone(mathBoost);
        
        internal void LogComputeConstant(
            FlowNode a, string mathSymbol, FlowNode b, FlowNode result,
            [CallerMemberName] string opName = null)
            => Logging.LogComputeConstant(a, mathSymbol, b, result, opName);
        
        internal void LogIdentityOperation(
            FlowNode a, string mathSymbol, FlowNode identityValue,
            [CallerMemberName] string opName = null) 
            => Logging.LogIdentityOperation(a, mathSymbol, identityValue, opName);
        
        internal void LogIdentityOperation(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null) 
            => Logging.LogIdentityOperation(signal, dimension, mathSymbol, transform, opName);
        
        internal void LogAlwaysOneOptimization(
            FlowNode a, string mathSymbol, FlowNode b,
            [CallerMemberName] string opName = null) 
            => Logging.LogAlwaysOneOptimization(a, mathSymbol, b, opName);
        
        internal void LogAlwaysOneOptimization(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null) 
            => Logging.LogAlwaysOneOptimization(signal, dimension, mathSymbol, transform, opName);
        
        internal void LogInvariance(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null) 
            => Logging.LogInvariance(signal, dimension, mathSymbol, transform, opName);
        
        internal void LogDivisionByMultiplication(FlowNode a, FlowNode b, FlowNode result) 
            => Logging.LogDivisionByMultiplication(a, b, result);
        
        internal void LogDistributeMultiplyOverAddition(FlowNode formulaBefore, FlowNode formulaAfter) 
            => Logging.LogDistributeMultiplyOverAddition(formulaBefore, formulaAfter);
        
        internal void LogAdditionOptimizations(
            IList<FlowNode> terms, IList<FlowNode> flattenedTerms, IList<FlowNode> optimizedTerms,
            IList<FlowNode> consts, double constant, [CallerMemberName] string opName = null)
            => Logging.LogAdditionOptimizations(terms, flattenedTerms, optimizedTerms, consts, constant, opName);
        
        internal void LogMultiplicationOptimizations(
            IList<FlowNode> factors, IList<FlowNode> optimizedFactors,
            IList<FlowNode> consts, double constant, [CallerMemberName] string opName = null) 
            => Logging.LogMultiplicationOptimizations(factors, optimizedFactors, consts, constant, opName);
    }

    public static class LogExtensions
    {
        public static string SynthLog(this Tape tape, double? calculationDuration = null) => Static.SynthLog(tape, calculationDuration);
        
        public static string Descriptor(this Tape tape)                       => Static.Descriptor(tape);
        public static string Descriptor(this TapeActions actions)             => Static.Descriptor(actions);
        public static string Descriptor(this AudioFileOutput audioFileOutput) => Static.Descriptor(audioFileOutput);
        public static string Descriptor(this IList<FlowNode> signals)         => Static.Descriptor(signals);

        public   static string ConfigLog(this SynthWishes     synthWishes                              ) => Static.ConfigLog(synthWishes);
        public   static string ConfigLog(this SynthWishes     synthWishes,     string title            ) => Static.ConfigLog(title, synthWishes);
        public   static string ConfigLog(this SynthWishes     synthWishes,     string title, string sep) => Static.ConfigLog(title, synthWishes, sep);
        public   static string ConfigLog(this FlowNode        flowNode                                 ) => Static.ConfigLog(flowNode);
        public   static string ConfigLog(this FlowNode        flowNode,        string title            ) => Static.ConfigLog(title, flowNode);
        public   static string ConfigLog(this FlowNode        flowNode,        string title, string sep) => Static.ConfigLog(title, flowNode, sep);
        internal static string ConfigLog(this ConfigResolver  configWishes                                                              ) => Static.ConfigLog(configWishes);
        internal static string ConfigLog(this ConfigResolver  configWishes,    SynthWishes synthWishes                                  ) => Static.ConfigLog(configWishes, synthWishes);
        internal static string ConfigLog(this ConfigResolver  configWishes,    SynthWishes synthWishes,               string sep        ) => Static.ConfigLog(configWishes, synthWishes, sep);
        internal static string ConfigLog(this ConfigResolver  configWishes,    string title,                          string sep = " | ") => Static.ConfigLog(title, configWishes, sep);
        internal static string ConfigLog(this ConfigResolver  configWishes,    String title, SynthWishes synthWishes, string sep = " | ") => Static.ConfigLog(title, configWishes, synthWishes, sep);
        internal static string ConfigLog(this ConfigSection   configSection                            ) => Static.ConfigLog(configSection);
        internal static string ConfigLog(this ConfigSection   configSection,   string title            ) => Static.ConfigLog(title, configSection);
        internal static string ConfigLog(this ConfigSection   configSection,   string title, string sep) => Static.ConfigLog(title, configSection, sep);
        public   static string ConfigLog(this Tape            tape                                     ) => Static.ConfigLog(tape);
        public   static string ConfigLog(this Tape            tape,            string title            ) => Static.ConfigLog(title, tape);
        public   static string ConfigLog(this Tape            tape,            string title, string sep) => Static.ConfigLog(title, tape, sep);
        public   static string ConfigLog(this TapeConfig      tapeConfig                               ) => Static.ConfigLog(tapeConfig);
        public   static string ConfigLog(this Buff            buff                                     ) => Static.ConfigLog(buff);
        public   static string ConfigLog(this Buff            buff,            string title            ) => Static.ConfigLog(title, buff);
        public   static string ConfigLog(this Buff            buff,            string title, string sep) => Static.ConfigLog(title, buff, sep);
        public   static string ConfigLog(this AudioFileOutput audioFileOutput                          ) => Static.ConfigLog(audioFileOutput);
        public   static string ConfigLog(this AudioFileOutput audioFileOutput, string title            ) => Static.ConfigLog(title, audioFileOutput);
        public   static string ConfigLog(this AudioFileOutput audioFileOutput, string title, string sep) => Static.ConfigLog(title, audioFileOutput, sep);
        public   static string ConfigLog(this Sample          sample                                   ) => Static.ConfigLog(sample);
        public   static string ConfigLog(this Sample          sample,          string title            ) => Static.ConfigLog(title, sample);
        public   static string ConfigLog(this Sample          sample,          string title, string sep) => Static.ConfigLog(title, sample, sep);
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish                            ) => Static.ConfigLog(audioInfoWish);
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,   string sep              ) => Static.ConfigLog(audioInfoWish, sep);
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,   string title, string sep) => Static.ConfigLog(title, audioInfoWish, sep);
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo                            ) => Static.ConfigLog(audioFileInfo);
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,   string sep              ) => Static.ConfigLog(audioFileInfo, sep);
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,   string title, string sep) => Static.ConfigLog(title, audioFileInfo, sep);
        public   static string ConfigLog(this WavHeaderStruct wavHeader                                ) => Static.ConfigLog(wavHeader);
        public   static string ConfigLog(this WavHeaderStruct wavHeader,       string sep              ) => Static.ConfigLog(wavHeader, sep);
        public   static string ConfigLog(this WavHeaderStruct wavHeader,       string title, string sep) => Static.ConfigLog(title, wavHeader, sep);
    }
    
    public class LogWishes
    {
        public static LogWishes Static { get; } = new LogWishes();

        // Basics
        
        private readonly ILogger _logger = CreateLoggerFromConfig(ConfigResolver.Static.LoggerConfig);

            
        public bool LoggingEnabled { get; set; } = true; // = Config.LoggerConfig.Active ?? DefaultLoggingEnabled; // TODO: Use config somehow

        // NOTE: All the threading, locking and flushing helped
        // Test Explorer in Visual Studio 2022
        // avoid mangling blank lines, for the most part.
        
        private readonly object _logLock = new object();
        private readonly ThreadLocal<bool> _blankLinePending = new ThreadLocal<bool>();

        //public void Log(string message = default) => Log(_staticLogger, message);
        public void Log(string message = default)
        {
            if (!LoggingEnabled) return;
            
            lock (_logLock)
            {
                message = message ?? "";
               
                if (!message.FilledIn())
                {
                    _blankLinePending.Value = true;
                    return;
                }
                
                if (_blankLinePending.Value)
                {
                    if (!message.StartsWithBlankLine())
                    {
                        message = NewLine + message;
                    }
                }

                _blankLinePending.Value = EndsWithBlankLine(message);

                _logger.Log(message.TrimEnd());
            }
        }
                
        //public void LogTitleStrong(string title) => LogTitleStrong(_staticLogger, title);
        public void LogTitleStrong(string title)
        {
            string upperCase = (title ?? "").ToUpper();
            
            Log();
            Log(PrettyTitle(upperCase, underlineChar: '='));
            Log();
        }

        //public void LogTitle(string title) => LogTitle(_staticLogger, title);
        public void LogTitle(string title)
        {
            Log();
            Log(PrettyTitle(title));
            Log();
        }
        
        //internal void LogOutputFile(string filePath, string sourceFilePath = null) => LogOutputFile(_staticLogger, filePath, sourceFilePath);
        internal void LogOutputFile(string filePath, string sourceFilePath = null)
        {
            Log(FormatOutputFile(filePath, sourceFilePath));
        }

        internal string FormatOutputFile(string filePath, string sourceFilePath = null)
        {
            if (!Has(filePath)) return default;
            if (!Exists(filePath)) return default;
            
            string prefix = "  ";
            string sourceFileString = default;
            if (Has(sourceFilePath)) sourceFileString += $" (copied {sourceFilePath})";
            string message = prefix + filePath + sourceFileString;
            return message;
        }
                
        private string FormatOutputBytes(byte[] bytes)
        {
            if (!Has(bytes)) return default;
            return $"  {PrettyByteCount(bytes.Length)} written to memory.";
        }
        
        // Pretty Calculation Graphs
        
        public string SynthLog(Tape tape, double? calculationDuration = null)
        {
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
            lines.Add("Output:");
            lines.Add("");
        
            byte[] bytes = tape.Bytes;
            
            if (Has(bytes))
            {
                lines.Add(FormatOutputBytes(bytes));
            }

            string formattedFilePath = FormatOutputFile(tape.FilePathResolved);
            if (Has(formattedFilePath))
            {
                lines.Add(formattedFilePath);
            }

            if (!Has(formattedFilePath) && !Has(bytes))
            {
                lines.Add("⚠ Tape not recorded!");
            }
            
            lines.Add("");

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

        // Config Log
                
        public string ConfigLog(string title, string group1, string group2 = null, string group3 = null, string sep = null)
        {
            string titleElement = Has(title) ? PrettyTitle(title) + NewLine + NewLine : "";
            string[] groups = { group1, group2, group3 };
            if (!Has(sep, false)) sep = NewLine;
            string log = titleElement + Join(sep, groups.Where(FilledIn));
            return log;
        }
        
        public string DurationsDescriptor(
            double? audioLength = null, double? leadingSilence = null, double? trailingSilence = null, 
            double? barLength = null, double? beatLength = null, double? noteLength = null)
        {
            var elements = new List<string>();
            
            if (audioLength != null) elements.Add($"{audioLength:F2}s");
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
        public string AudioFormatDescriptor(
            int? samplingRate = null, int? bits = null,
            int? channels = null, int? channel = null, 
            AudioFileFormatEnum? audioFormat = null, 
            InterpolationTypeEnum? interpolation = null)
        {
            var elements = new List<string>();

            if (Has(samplingRate)) elements.Add($"{samplingRate} Hz");
            if (Has(bits)) elements.Add($"{bits}-Bit");
            string channelDescriptor = ChannelDescriptor(channels, channel);
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

        public string ChannelDescriptor(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            return ChannelDescriptor(tape.Config);
        }

        public string ChannelDescriptor(TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new ArgumentNullException(nameof(tapeConfig));
            return ChannelDescriptor(tapeConfig.Channels, tapeConfig.Channel);
        }

        public string ChannelDescriptor(int? channelCount, int? channel = null)
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
        
        public string FeaturesDescriptor(
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
        
        public void LogConfig(SynthWishes synthWishes) 
        {
            Log();
            Log(ConfigLog(synthWishes));
            Log();
        }
        
        public void LogConfig(SynthWishes synthWishes, string sep) 
        {
            Log();
            Log(ConfigLog(synthWishes, sep));
            Log();
        }
        
        public void LogConfig(string title, SynthWishes synthWishes, string sep = default) 
        {
            Log();
            Log(ConfigLog(title, synthWishes, sep));
            Log();
        }
        
        public string ConfigLog(SynthWishes synthWishes) => ConfigLog("Options", synthWishes, NewLine);
        public string ConfigLog(SynthWishes synthWishes, string sep) => ConfigLog("Options", synthWishes, sep);
        public string ConfigLog(string title, SynthWishes synthWishes, string sep = default)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return ConfigLog(title, synthWishes.Config, synthWishes, sep);
        }
        
        public string ConfigLog(FlowNode flowNode) => ConfigLog("FlowNode Options", flowNode);
        public string ConfigLog(FlowNode flowNode, string sep) => ConfigLog("FlowNode Options", flowNode, sep);
        public string ConfigLog(string title, FlowNode flowNode, string sep = " | ")
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            return ConfigLog(title, flowNode.SynthWishes, sep);
        }
        
        public string ConfigLog(Buff buff) => ConfigLog("", buff);
        public string ConfigLog(Buff buff, string sep) => ConfigLog("", buff, sep);
        public string ConfigLog(string title, Buff buff, string sep = " | ")
        {
            if (buff == null) throw new NullException(() => buff);
            if (buff.UnderlyingAudioFileOutput == null) return default;
            return ConfigLog(title, buff.UnderlyingAudioFileOutput, sep);
        }
        
        public string ConfigLog(AudioInfoWish audioInfoWish) => ConfigLog("", audioInfoWish);
        public string ConfigLog(AudioInfoWish audioInfoWish, string sep) => ConfigLog("", audioInfoWish, sep);
        public string ConfigLog(string title, AudioInfoWish audioInfoWish, string sep = " | ")
        {
            if (audioInfoWish == null) throw new NullException(()  => audioInfoWish);
            
            if (!Has(sep, false)) sep = NewLine;
            
            string durationsDescriptor = DurationsDescriptor(
                audioInfoWish.AudioLength());
            
            string audioFormatDescriptor = AudioFormatDescriptor(
                audioInfoWish.SamplingRate,
                audioInfoWish.Bits,
                audioInfoWish.Channels);
            
            return ConfigLog(
                title, 
                durationsDescriptor, 
                audioFormatDescriptor, 
                sep: sep);
        }
        
        public string ConfigLog(AudioFileInfo audioFileInfo) => ConfigLog("Audio Info", audioFileInfo);
        public string ConfigLog(AudioFileInfo audioFileInfo, string sep) => ConfigLog("Audio Info", audioFileInfo, sep);
        public string ConfigLog(string title, AudioFileInfo audioFileInfo, string sep = " | ")
        {
            if (audioFileInfo == null) throw new NullException(() => audioFileInfo);
            
            if (!Has(sep, false)) sep = NewLine;
            
            string durationsDescriptor = DurationsDescriptor(
                audioFileInfo.AudioLength());
            
            string audioFormatDescriptor = AudioFormatDescriptor(
                audioFileInfo.SamplingRate,
                audioFileInfo.Bits(),
                audioFileInfo.ChannelCount);
            
            return ConfigLog(
                title, 
                durationsDescriptor, 
                audioFormatDescriptor, 
                sep: sep);
        }
        
        public string ConfigLog(WavHeaderStruct wavHeader) => ConfigLog("WAV Header", wavHeader);
        public string ConfigLog(WavHeaderStruct wavHeader, string sep) => ConfigLog("WAV Header", wavHeader, sep);
        public string ConfigLog(string title, WavHeaderStruct wavHeader, string sep = " | ")
        {
            
            if (!Has(sep, false)) sep = NewLine;
            
            string durationsDescriptor = DurationsDescriptor(
                wavHeader.AudioLength());
            
            string audioFormatDescriptor = AudioFormatDescriptor(
                wavHeader.SamplingRate,
                wavHeader.Bits(),
                wavHeader.ChannelCount);
            
            return ConfigLog(
                title, 
                durationsDescriptor, 
                audioFormatDescriptor, 
                sep: sep);
        }
        
        internal string ConfigLog(ConfigResolver configWishes) => ConfigLog("", configWishes);
        internal string ConfigLog(ConfigResolver configWishes, SynthWishes synthWishes) => ConfigLog("", configWishes, synthWishes);
        internal string ConfigLog(ConfigResolver configWishes, SynthWishes synthWishes, string sep) => ConfigLog("", configWishes, synthWishes, sep);
        internal string ConfigLog(string title, ConfigResolver configWishes, string sep = " | ") => ConfigLog(title, configWishes, null, sep);
        internal string ConfigLog(string title, ConfigResolver configWishes, SynthWishes synthWishes, string sep = " | ")
        {
            if (configWishes == null) throw new NullException(() => configWishes);
            
            string audioFormatDescriptor = AudioFormatDescriptor(
                configWishes.GetSamplingRate,
                configWishes.GetBits,
                configWishes.GetChannels,
                configWishes.GetChannel,
                configWishes.GetAudioFormat,
                configWishes.GetInterpolation);
            
            string featuresDescriptor = FeaturesDescriptor(
                configWishes.GetAudioPlayback(),
                configWishes.GetDiskCache,
                configWishes.GetMathBoost,
                configWishes.GetParallelProcessing,
                configWishes.GetPlayAllTapes);

            string durationsDescriptor = Has(synthWishes) ? DurationsDescriptor(
                configWishes.GetAudioLength(synthWishes).Value,
                configWishes.GetLeadingSilence(synthWishes).Value,
                configWishes.GetTrailingSilence(synthWishes).Value,
                configWishes.GetBarLength(synthWishes).Value,
                configWishes.GetBeatLength(synthWishes).Value,
                configWishes.GetNoteLength(synthWishes).Value) : "";
            
            return ConfigLog(
                title,
                audioFormatDescriptor,
                featuresDescriptor,
                durationsDescriptor,
                sep: sep);
        }
        
        internal string ConfigLog(ConfigSection configSection) => ConfigLog("", configSection);
        internal string ConfigLog(ConfigSection configSection, string sep) => ConfigLog("", configSection, sep);
        internal string ConfigLog(string title, ConfigSection configSection, string sep = " | ")
        {
            if (configSection == null) throw new NullException(() => configSection);
            
            string featuresDescriptor = FeaturesDescriptor(
                configSection.AudioPlayback,
                configSection.DiskCache,
                configSection.MathBoost,
                configSection.ParallelProcessing,
                configSection.PlayAllTapes);
            
            string audioFormatDescriptor = AudioFormatDescriptor(
                configSection.SamplingRate,
                configSection.Bits,
                configSection.Channels,
                channel: null,
                configSection.AudioFormat,
                configSection.Interpolation);
            
            string durationsDescriptor = DurationsDescriptor(
                configSection.AudioLength,
                configSection.LeadingSilence,
                configSection.TrailingSilence,
                configSection.BarLength,
                configSection.BeatLength,
                configSection.NoteLength);
            
            return ConfigLog(
                title,
                featuresDescriptor,
                audioFormatDescriptor,
                durationsDescriptor,
                sep: sep);
        }
        
        public string ConfigLog(Tape tape) => ConfigLog("", tape);
        public string ConfigLog(Tape tape, string sep) => ConfigLog("", tape, sep);
        public string ConfigLog(string title, Tape tape, string sep = " | ")
        {
            if (tape == null) throw new NullException(() => tape);
                        
            string durationsDescriptor = DurationsDescriptor(
                tape.Duration,
                tape.LeadingSilence,
                tape.TrailingSilence);

            string audioFormatDescriptor = AudioFormatDescriptor(
                tape.Config.SamplingRate,
                tape.Config.Bits,
                tape.Config.Channels, 
                tape.Config.Channel,
                tape.Config.AudioFormat,
                tape.Config.Interpolation);
            
            string featuresDescriptor = FeaturesDescriptor(
                diskCache: tape.Actions.DiskCache.On,
                playAllTapes: tape.Actions.PlayAllTapes.On);

            return ConfigLog(
                title, 
                durationsDescriptor,
                audioFormatDescriptor,
                featuresDescriptor,
                sep: sep);
        }
        
        public string ConfigLog(AudioFileOutput audioFileOutput) => ConfigLog("", audioFileOutput);
        public string ConfigLog(AudioFileOutput audioFileOutput, string sep) => ConfigLog("", audioFileOutput, sep);
        public string ConfigLog(string title, AudioFileOutput audioFileOutput, string sep = " | ")
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            
            string durationsDescriptor = DurationsDescriptor(
                audioFileOutput.Duration);
            
            string audioFormatDescriptor = AudioFormatDescriptor(
                audioFileOutput.SamplingRate,
                audioFileOutput.Bits(),
                audioFileOutput.GetChannelCount(),
                channel: null,
                audioFileOutput.GetAudioFileFormatEnum());
            
            return ConfigLog(
                title, 
                durationsDescriptor, 
                audioFormatDescriptor, 
                sep: sep);
        }
        
        public string ConfigLog(Sample sample) => ConfigLog("", sample);
        public string ConfigLog(Sample sample, string sep) => ConfigLog("", sample, sep);
        public string ConfigLog(string title, Sample sample, string sep = " | ")
        {
            if (sample == null) throw new NullException(() => sample);
            
            string durationsDescriptor = DurationsDescriptor(
                sample.GetDuration());
            
            string audioFormatDescriptor = AudioFormatDescriptor(
                sample.SamplingRate,
                sample.Bits(),
                sample.GetChannelCount(),
                channel: null,
                sample.GetAudioFileFormatEnum(),
                sample.GetInterpolationTypeEnum());
            
            return ConfigLog(
                title, 
                durationsDescriptor, 
                audioFormatDescriptor, 
                sep: sep);
        }
        
        internal string ConfigLog(TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new ArgumentNullException(nameof(tapeConfig));
            
            var configLog = AudioFormatDescriptor(
                tapeConfig.SamplingRate,
                tapeConfig.Bits,
                tapeConfig.Channels,
                tapeConfig.Channel,
                tapeConfig.AudioFormat,
                tapeConfig.Interpolation);
            
            return configLog;
        }
        
        // Tapes
        
        internal string PlotTapeTree(IList<Tape> tapes, bool includeCalculationGraphs = false)
        {
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
                    sb.AppendLine(Descriptor(tape));
                }
            }
            sb.AppendLine();
            
            if (multiUseTapes.Length > 0)
            {
                sb.AppendLine($"Multi-Use ({multiUseTapes.Length}):");
                foreach (var tape in multiUseTapes)
                { 
                    sb.AppendLine(Descriptor(tape));
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
                    sb.AppendLine($" => {tape.GetName()} ({IDDescriptor(tape)}) ..."); 
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
                sb2.Append(Descriptor(tape));
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
        
        internal string IDDescriptor(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            return IDDescriptor(tape.IDs);
        }
        
        internal string IDDescriptor(IList<int> ids)
        {
            if (!Has(ids))
            {
                return "no ID";
            }
            
            string idDescriptor = Join("|", ids);
            return idDescriptor;
        }
        
        public string Descriptor(TapeActions actions)
        {
            if (actions == null) throw new ArgumentNullException(nameof(actions));
            
            var elements = new List<string>();
            
            if (actions.Play.Done) elements.Add("played");
            else if (actions.Play.On) elements.Add("play");
            
            if (actions.PlayChannels.Done) elements.Add("played-ch");
            else if (actions.PlayChannels.On) elements.Add("play-ch");
            
            if (actions.Save.Done) elements.Add("saved");
            else if (actions.Save.On) elements.Add("save");
            
            if (actions.SaveChannels.Done) elements.Add("saved-ch");
            else if (actions.SaveChannels.On) elements.Add("save-ch");
            
            if (actions.BeforeRecord.Done) elements.Add("before-done");
            else if (actions.BeforeRecord.On) elements.Add("before");
            if (actions.BeforeRecord.Callback != null) elements.Add("cbk");
            
            if (actions.AfterRecord.Done) elements.Add("after-done");
            else if (actions.AfterRecord.On) elements.Add("after");
            if (actions.AfterRecord.Callback != null) elements.Add("cbk");
            
            if (actions.BeforeRecordChannel.Done) elements.Add("before-ch-done");
            else if (actions.BeforeRecordChannel.On) elements.Add("before-ch");
            if (actions.BeforeRecordChannel.Callback != null) elements.Add("cbk");
            
            if (actions.AfterRecordChannel.Done) elements.Add("after-ch-done");
            else if (actions.AfterRecordChannel.On) elements.Add("after-ch");
            if (actions.AfterRecordChannel.Callback != null) elements.Add("cbk");

            return Join(",", elements);
        }
        
        public string Descriptor(Tape tape)
        {
            if (tape == null) return "<Tape=null>";

            string prefix = "";
            if (tape.Config.IsStereo && tape.Config.Channel == null) prefix = "(Stereo) ";
            else if (tape.NestingLevel > 0) prefix = $"(Level {tape.NestingLevel}) ";
            
            string nameDescriptor = tape.GetName();
            if (!Has(nameDescriptor)) nameDescriptor = "<Untitled>";
            
            var flags = new List<string>();
            
            string actions = Descriptor(tape.Actions);
            flags.Add(actions);
            
            if (tape.IsTape) flags.Add("tape");
            
            string channel = ChannelDescriptor(tape)?.ToLower();
            flags.Add(channel);
            
            if (Has(tape.Duration)) flags.Add($"{tape.Duration:0.###}s");
            
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
            
            if (Has(tape.UnderlyingAudioFileOutput)) flags.Add("out");
            if (Has(tape.Bytes)) flags.Add("mem");
            // TODO: Combine with saved flag, report inconsistencies if present.
            if (Exists(tape.FilePathResolved)) flags.Add("file");
            if (Has(tape.Sample)) flags.Add("smp");

            if (tape.SynthWishes != null)
            {
                if (Has(tape.Config.SamplingRate) && 
                    tape.Config.SamplingRate != tape.SynthWishes.GetSamplingRate)
                {
                    flags.Add($"{tape.Config.SamplingRate}hz");
                }

                if (Has(tape.Config.Bits) && 
                    tape.Config.Bits != tape.SynthWishes.GetBits)
                {
                    flags.Add($"{tape.Config.Bits}bit");
                }

                if (Has(tape.Config.AudioFormat) && 
                    tape.Config.AudioFormat != tape.SynthWishes.GetAudioFormat)
                {
                    flags.Add($"{tape.Config.AudioFormat}".ToLower());
                }

                if (Has(tape.Config.Interpolation) &&
                    tape.Config.Interpolation != tape.SynthWishes.GetInterpolation)
                {
                    flags.Add($"{tape.Config.Interpolation}".ToLower());
                }
            }

            flags = flags.Where(FilledIn).ToList();
            
            string flagDescriptor = default;
            if (flags.Count > 0)
            {
                flagDescriptor = " {" + Join(",", flags) + "}";
            }
            
            string idDescriptor = $" ({IDDescriptor(tape)})";
            
            return prefix + nameDescriptor + flagDescriptor + idDescriptor;
        }
        
        private string Descriptors(IList<Tape> tapes)
        {
           if (!Has(tapes)) return default;
           string[] tapeDescriptors = tapes.Where(x => x != null).Select(Descriptor).ToArray();
           return Join(NewLine, tapeDescriptors);
        }
        
        internal string TapesLeftMessage(int todoCount, Tape[] tapesLeft)
        {
            string prefix = default;
            if (todoCount != 0)
            {
                prefix = $"{todoCount} {nameof(Tape)}(s) Left: ";
            }
            
            if (Has(tapesLeft))
            {
                return prefix + NewLine + Descriptors(tapesLeft);
            }
            else
            {
                return prefix + "<none>";
            }
        }
        
        // Actions

        internal void LogAction(TapeAction action, string message = null)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            Log(ActionMessage("Action", action.Type, action.Tape.Descriptor(), message));
        }
        
        internal void LogAction(Tape entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Tape), action, entity.Descriptor(), message));
        }

        internal void LogAction(Buff entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Buff), action, entity.Name, message));
        }

        internal void LogAction(Sample entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Sample), action, entity.Name, message ?? ConfigLog(entity)));
        }

        internal void LogAction(AudioFileOutput entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage("Audio File Out", action, entity.Name, message ?? ConfigLog(entity)));
        }
        
        internal void LogAction(FlowNode entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Operator), action, entity.Name, message));
        }
        
        public void LogAction(object entity, string action, string name, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(entity.GetType().Name, action, name, message));
        }

        internal void LogAction(string typeName, string message) 
            => Log(ActionMessage(typeName, null, null, message));
        
        internal void LogAction(string typeName, string action, string message) 
            => Log(ActionMessage(typeName, action, null, message));
        
        internal void LogAction(string typeName, string action, string objectName, string message) 
            => Log(ActionMessage(typeName, action, objectName, message));

        public string ActionMessage(string typeName, ActionEnum action, string objectName, string message)
            => ActionMessage(typeName, action.ToString(), objectName, message);
        
        public string ActionMessage(string typeName, string action, string objectName, string message)
        {
            string text = PrettyTime();
                
            if (Has(typeName))
            {
                text += " [" + typeName.ToUpper() + "]";
            }
            
            if (Has(action))
            {
                text += " " + action;
            }
            
            if (Has(objectName))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + '"' + objectName + '"';
            }
            
            if (Has(message))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + message;
            }
            
            return text;
        }

        public string Descriptor(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            
            string name = Has(audioFileOutput.Name) ? audioFileOutput.Name : "";
            string configLog = ConfigLog(audioFileOutput);
            string filePath = Exists(audioFileOutput.FilePath) ? audioFileOutput.FilePath : "";
            
            string joined = Join(" | ", new[] { name, configLog, filePath }.Where(FilledIn));
            return joined;
        }

        public string Descriptor(IList<FlowNode> signals)
        {
            if (signals == null) throw new ArgumentNullException(nameof(signals));
            return signals.Count == 0 ? "<Signal=null>" : Join(" | ", signals.Select(x => $"{x}"));
        }

        // Math Boost

        internal void LogMathBoostTitle(bool mathBoost)
        {
            if (!mathBoost) return;
            LogTitle("Math Boost");
        }
        
        internal void LogMathBoostDone(bool mathBoost)
        {
            if (!mathBoost) return;
            //LogLine("Done");
        }

        internal void LogComputeConstant(
            FlowNode a, string mathSymbol, FlowNode b, FlowNode result,
            [CallerMemberName] string opName = null)
            => Log(Pad("Compute const") + $" : {Stringify(opName, a, mathSymbol, b)} => {Stringify(result)}");
        
        internal void LogIdentityOperation(
            FlowNode a, string mathSymbol, FlowNode identityValue,
            [CallerMemberName] string opName = null)
            => Log(Pad("Identity op") + $" : {Stringify(opName, a, mathSymbol, identityValue)} => {Stringify(a)}");
        
        internal void LogIdentityOperation(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Log(Pad($"Identity op ({dimension})") + $" : {Stringify(opName, signal, dimension, mathSymbol, transform)} => {Stringify(signal)}");
        
        internal void LogAlwaysOneOptimization(
            FlowNode a, string mathSymbol, FlowNode b,
            [CallerMemberName] string opName = null)
            => Log(Pad("Always 1") + $" : {Stringify(opName, a, mathSymbol, b)} => 1");
        
        internal void LogAlwaysOneOptimization(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Log(Pad($"Always 1 ({dimension})") + " : " +
                       $"{Stringify(opName, signal, dimension, mathSymbol, transform)} => " +
                       $"{Stringify(opName, signal, dimension, "=", 1)}");
        
        internal void LogInvariance(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Log(Pad($"Invariance ({dimension})") + $" : {Stringify(opName, signal, dimension, mathSymbol, transform)} => {Stringify(signal)}");
        
        internal void LogDivisionByMultiplication(FlowNode a, FlowNode b, FlowNode result)
            => Log(Pad("Div => mul") + $" : {Stringify(a)} / {Stringify(b)} => {Stringify(result)}");
        
        internal void LogDistributeMultiplyOverAddition(FlowNode formulaBefore, FlowNode formulaAfter)
            => Log(Pad("Distribute * over +") + $" : {Stringify(formulaBefore)} => {Stringify(formulaAfter)}");
        
        internal void LogAdditionOptimizations(
            IList<FlowNode> terms, IList<FlowNode> flattenedTerms, IList<FlowNode> optimizedTerms,
            IList<FlowNode> consts, double constant, [CallerMemberName] string opName = null)
        {
            string symbol = "+";
            
            bool wasFlattened = terms.Count != flattenedTerms.Count;
            if (wasFlattened)
            {
                Log(Pad($"Flatten {symbol}") + $" : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, flattenedTerms)}");
            }
            
            bool hasConst0 = consts.Count >= 1 && constant == 0;
            if (hasConst0)
            {
                Log(Pad("Eliminate 0") + $" : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Log(Pad("Compute const") + $" : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool noTermsLeft = terms.Count != 0 && optimizedTerms.Count == 0;
            if (noTermsLeft)
            {
                Log(Pad("0 terms remain") + $" : {Stringify(opName, symbol, terms)} => 0");
            }
            
            bool oneTermLeft = optimizedTerms.Count == 1;
            if (oneTermLeft)
            {
                Log(Pad($"Eliminate {symbol}") + $" : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(symbol, optimizedTerms)}");
            }
        }
        
        internal void LogMultiplicationOptimizations(
            IList<FlowNode> factors, IList<FlowNode> optimizedFactors,
            IList<FlowNode> consts, double constant, [CallerMemberName] string opName = null)
        {
            string symbol = "*";
            
            bool hasConst1 = consts.Count >= 1 && constant == 1;
            if (hasConst1)
            {
                Log(Pad("Eliminate 1") + $" : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }
            
            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Log(Pad("Compute const") + $" : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }
            
            bool noFactorsLeft = factors.Count != 0 && optimizedFactors.Count == 0;
            if (noFactorsLeft)
            {
                Log(Pad("0 factors remain") + $" : {Stringify(opName, symbol, factors)} => 1");
            }
            
            bool oneFactorLeft = optimizedFactors.Count == 1;
            if (oneFactorLeft)
            {
                Log(Pad($"Eliminate {symbol}") + $" : {Stringify(opName, symbol, optimizedFactors)} => {Stringify(symbol, optimizedFactors)}");
            }
        }
        
        // Specialized Stringifications

        private string Stringify(string opName, FlowNode a, string mathSymbol, FlowNode b)
            => Stringify(opName, mathSymbol, a, b);
        
        private string Stringify(string opName, string mathSymbol, params FlowNode[] operands)
            => Stringify(opName, mathSymbol, (IList<FlowNode>)operands);
        
        private string Stringify(string opName, string mathSymbol, IList<FlowNode> operands)
            => $"{opName}({Stringify(mathSymbol, operands)})";
        
        private string Stringify(string mathSymbol, IList<FlowNode> operands)
            => Join(" " + mathSymbol + " ", operands.Select(Stringify));
        
        private string Stringify(FlowNode operand)
            => operand.Stringify(true);
        
        private string Stringify(
            string opName, FlowNode signal, string dimension, string mathSymbol, FlowNode transform)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {Stringify(transform)})";
        
        private string Stringify(
            string opName, FlowNode signal, string dimension, string mathSymbol, double value)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {value})";
 
        private string Pad(string text) => (text ?? "").PadRight(19);
    }
}
