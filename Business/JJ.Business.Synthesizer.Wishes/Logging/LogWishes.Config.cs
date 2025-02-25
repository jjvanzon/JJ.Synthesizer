using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.Environment;
using static System.String;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;


namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
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
        
        public   void LogConfig(              SynthWishes    entity                      ) => LogSpaced(ConfigLog(entity));
        public   void LogConfig(              SynthWishes    entity, string sep          ) => LogSpaced(ConfigLog(entity, sep)); 
        public   void LogConfig(string title, SynthWishes    entity, string sep = default) => LogSpaced(ConfigLog(title, entity, sep));
        public   void LogConfig(              FlowNode       entity                      ) => Log(ConfigLog(entity));
        public   void LogConfig(              FlowNode       entity, string sep          ) => Log(ConfigLog(entity, sep)); 
        public   void LogConfig(string title, FlowNode       entity, string sep = " | "  ) => Log(ConfigLog(title, entity, sep));
        internal void LogConfig(              ConfigResolver entity, SynthWishes synthWishes                    ) => Log(ConfigLog(entity, synthWishes));
        internal void LogConfig(              ConfigResolver entity, SynthWishes synthWishes, string sep        ) => Log(ConfigLog(entity, synthWishes, sep)); 
        internal void LogConfig(string title, ConfigResolver entity, SynthWishes synthWishes, string sep = " | ") => Log(ConfigLog(title, entity, synthWishes, sep));
        internal void LogConfig(              ConfigSection  entity                      ) => Log(ConfigLog(entity));
        internal void LogConfig(              ConfigSection  entity, string sep          ) => Log(ConfigLog(entity, sep));
        internal void LogConfig(string title, ConfigSection  entity, string sep = " | "  ) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              Tape           entity                      ) => Log(ConfigLog(entity));
        public   void LogConfig(              Tape           entity, string sep          ) => Log(ConfigLog(entity, sep)); 
        public   void LogConfig(string title, Tape           entity, string sep = " | "  ) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              TapeConfig     entity                      ) => Log(ConfigLog(entity)); // By Design: No sep; no parts to separate.
        public   void LogConfig(              TapeActions    entity                      ) => LogConfig(       NoNull(() => entity).Tape     );
        public   void LogConfig(              TapeActions    entity, string sep          ) => LogConfig(       NoNull(() => entity).Tape, sep); 
        public   void LogConfig(string title, TapeActions    entity, string sep = " | "  ) => LogConfig(title, NoNull(() => entity).Tape, sep);
        public   void LogConfig(              TapeAction     entity                      ) => LogConfig(       NoNull(() => entity).Tape     );
        public   void LogConfig(              TapeAction     entity, string sep          ) => LogConfig(       NoNull(() => entity).Tape, sep); 
        public   void LogConfig(string title, TapeAction     entity, string sep = " | "  ) => LogConfig(title, NoNull(() => entity).Tape, sep);
        // TODO: Other entities
        

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
                        
        public string ConfigLog(TapeConfig tapeConfig)
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
        
        public string ConfigLog(Buff buff) => ConfigLog("", buff);
        public string ConfigLog(Buff buff, string sep) => ConfigLog("", buff, sep);
        public string ConfigLog(string title, Buff buff, string sep = " | ")
        {
            if (buff == null) throw new NullException(() => buff);
            if (buff.UnderlyingAudioFileOutput == null) return default;
            return ConfigLog(title, buff.UnderlyingAudioFileOutput, sep);
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
    }

    public static partial class LogExtensions
    {
        public   static void LogConfig(this FlowNode       entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this Tape           entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this TapeConfig     entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this TapeActions    entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this TapeAction     entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        //public   static void LogConfig(this Buff           entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        //internal static void LogConfig(this ConfigResolver entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);

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
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public void LogConfig()
            => LogWishes.LogConfig(this);
        
        public void LogConfig(string title, string sep = default)
            => LogWishes.LogConfig(title, this, sep);
    }
}
