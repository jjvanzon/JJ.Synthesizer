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
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    internal partial class LogWishes
    {
        private void LogConfig      (string message) => Log      ("Config", message);
        private void LogConfigSpaced(string message) => LogSpaced("Config", message);

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
        
        public string ConfigLog(              SynthWishes synthWishes, string sep = default) => ConfigLog("Options", synthWishes, sep);
        public string ConfigLog(string title, SynthWishes synthWishes, string sep = default)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return ConfigLog(title, synthWishes.Config, synthWishes, sep);
        }
        
        public string ConfigLog(              FlowNode flowNode, string sep = " | ") => ConfigLog("FlowNode Options", flowNode, sep);
        public string ConfigLog(string title, FlowNode flowNode, string sep = " | ")
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            return ConfigLog(title, flowNode.SynthWishes, sep);
        }
                
        internal string ConfigLog(              ConfigResolver configWishes,                          string sep = " | ") => ConfigLog("",    configWishes,              sep);
        internal string ConfigLog(              ConfigResolver configWishes, SynthWishes synthWishes, string sep = " | ") => ConfigLog("",    configWishes, synthWishes, sep);
        internal string ConfigLog(string title, ConfigResolver configWishes,                          string sep = " | ") => ConfigLog(title, configWishes, null,        sep);
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
        
        internal string ConfigLog(              ConfigSection configSection, string sep = " | ") => ConfigLog("", configSection, sep);
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

        public string ConfigLog(              Tape tape, string sep = " | ") => ConfigLog("", tape, sep);
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
        
        public string ConfigLog(              TapeActions tapeActions, string sep = " | ") => ConfigLog("", tapeActions, sep);
        public string ConfigLog(string title, TapeActions tapeActions, string sep = " | ")
        {
            if (tapeActions == null) throw new NullException(() => tapeActions);
            return ConfigLog(title, tapeActions.Tape, sep);
        }

        public string ConfigLog(              TapeAction tapeAction, string sep = " | ") => ConfigLog("", tapeAction, sep);
        public string ConfigLog(string title, TapeAction tapeAction, string sep = " | ")
        {
            if (tapeAction == null) throw new NullException(() => tapeAction);
            return ConfigLog(title, tapeAction.Tape, sep);
        }
        
        public string ConfigLog(              Buff buff, string sep = " | ") => ConfigLog("", buff, sep);
        public string ConfigLog(string title, Buff buff, string sep = " | ")
        {
            if (buff == null) throw new NullException(() => buff);
            if (buff.UnderlyingAudioFileOutput == null) return default;
            return ConfigLog(title, buff.UnderlyingAudioFileOutput, sep);
        }
                
        public string ConfigLog(              AudioFileOutput audioFileOutput, string sep = " | ") => ConfigLog("", audioFileOutput, sep);
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
                
        public string ConfigLog(              Sample sample, string sep = " | ") => ConfigLog("", sample, sep);
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

        public string ConfigLog(              AudioInfoWish audioInfoWish, string sep = " | ") => ConfigLog("", audioInfoWish, sep);
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
        
        public string ConfigLog(              AudioFileInfo audioFileInfo, string sep = " | ") => ConfigLog("Audio Info", audioFileInfo, sep);
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
        
        public string ConfigLog(              WavHeaderStruct wavHeader, string sep = " | ") => ConfigLog("WAV Header", wavHeader, sep);
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

        public   void LogConfig(              SynthWishes     entity            ) => LogConfigSpaced(ConfigLog(       entity     )); 
        public   void LogConfig(string title, SynthWishes     entity            ) => LogConfigSpaced(ConfigLog(title, entity     ));
        public   void LogConfig(              SynthWishes     entity, string sep) => LogConfigSpaced(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, SynthWishes     entity, string sep) => LogConfigSpaced(ConfigLog(title, entity, sep));
        public   void LogConfig(              FlowNode        entity            ) => LogConfig      (ConfigLog(       entity     )); 
        public   void LogConfig(string title, FlowNode        entity            ) => LogConfig      (ConfigLog(title, entity     ));
        public   void LogConfig(              FlowNode        entity, string sep) => LogConfig      (ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, FlowNode        entity, string sep) => LogConfig      (ConfigLog(title, entity, sep));
        internal void LogConfig(              ConfigResolver  entity                                     ) => LogConfig(ConfigLog(       entity                  )); 
        internal void LogConfig(string title, ConfigResolver  entity                                     ) => LogConfig(ConfigLog(title, entity                  ));
        internal void LogConfig(              ConfigResolver  entity,                          string sep) => LogConfig(ConfigLog(       entity,              sep)); 
        internal void LogConfig(string title, ConfigResolver  entity,                          string sep) => LogConfig(ConfigLog(title, entity,              sep));
        internal void LogConfig(              ConfigResolver  entity, SynthWishes synthWishes            ) => LogConfig(ConfigLog(       entity, synthWishes     )); 
        internal void LogConfig(string title, ConfigResolver  entity, SynthWishes synthWishes            ) => LogConfig(ConfigLog(title, entity, synthWishes     ));
        internal void LogConfig(              ConfigResolver  entity, SynthWishes synthWishes, string sep) => LogConfig(ConfigLog(       entity, synthWishes, sep)); 
        internal void LogConfig(string title, ConfigResolver  entity, SynthWishes synthWishes, string sep) => LogConfig(ConfigLog(title, entity, synthWishes, sep));
        internal void LogConfig(              ConfigSection   entity            ) => LogConfig(ConfigLog(       entity     ));
        internal void LogConfig(string title, ConfigSection   entity            ) => LogConfig(ConfigLog(title, entity     ));
        internal void LogConfig(              ConfigSection   entity, string sep) => LogConfig(ConfigLog(       entity, sep));
        internal void LogConfig(string title, ConfigSection   entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              Tape            entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, Tape            entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              Tape            entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, Tape            entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              TapeConfig      entity            ) => LogConfig(ConfigLog(       entity     ));
        public   void LogConfig(              TapeActions     entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, TapeActions     entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              TapeActions     entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, TapeActions     entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              TapeAction      entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, TapeAction      entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              TapeAction      entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, TapeAction      entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              Buff            entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, Buff            entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              Buff            entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, Buff            entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              AudioFileOutput entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, AudioFileOutput entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              AudioFileOutput entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, AudioFileOutput entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              Sample          entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, Sample          entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              Sample          entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, Sample          entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              AudioInfoWish   entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, AudioInfoWish   entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              AudioInfoWish   entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, AudioInfoWish   entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              AudioFileInfo   entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, AudioFileInfo   entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              AudioFileInfo   entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, AudioFileInfo   entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
        public   void LogConfig(              WavHeaderStruct entity            ) => LogConfig(ConfigLog(       entity     )); 
        public   void LogConfig(string title, WavHeaderStruct entity            ) => LogConfig(ConfigLog(title, entity     ));
        public   void LogConfig(              WavHeaderStruct entity, string sep) => LogConfig(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, WavHeaderStruct entity, string sep) => LogConfig(ConfigLog(title, entity, sep));
    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public   string ConfigLog(                                                         ) => Logging.ConfigLog(       this                );
        public   string ConfigLog(string title                                             ) => Logging.ConfigLog(title, this                );
        public   string ConfigLog(string title,                                  string sep) => Logging.ConfigLog(title, this,            sep);
        public   string ConfigLog(              FlowNode        flowNode                   ) => Logging.ConfigLog(       flowNode            );
        public   string ConfigLog(string title, FlowNode        flowNode                   ) => Logging.ConfigLog(title, flowNode            );
        public   string ConfigLog(              FlowNode        flowNode,        string sep) => Logging.ConfigLog(       flowNode,        sep);
        public   string ConfigLog(string title, FlowNode        flowNode,        string sep) => Logging.ConfigLog(title, flowNode,        sep);
        internal string ConfigLog(              ConfigResolver  configResolver             ) => Logging.ConfigLog(       configResolver, this     );
        internal string ConfigLog(string title, ConfigResolver  configResolver             ) => Logging.ConfigLog(title, configResolver, this     );
        internal string ConfigLog(              ConfigResolver  configResolver,  string sep) => Logging.ConfigLog(       configResolver, this, sep);
        internal string ConfigLog(string title, ConfigResolver  configResolver,  string sep) => Logging.ConfigLog(title, configResolver, this, sep);
        internal string ConfigLog(              ConfigSection   configSection              ) => Logging.ConfigLog(       configSection       );
        internal string ConfigLog(string title, ConfigSection   configSection              ) => Logging.ConfigLog(title, configSection       );
        internal string ConfigLog(              ConfigSection   configSection,   string sep) => Logging.ConfigLog(       configSection,   sep);
        internal string ConfigLog(string title, ConfigSection   configSection,   string sep) => Logging.ConfigLog(title, configSection,   sep);
        public   string ConfigLog(              Tape            tape                       ) => Logging.ConfigLog(       tape                );
        public   string ConfigLog(string title, Tape            tape                       ) => Logging.ConfigLog(title, tape                );
        public   string ConfigLog(              Tape            tape,            string sep) => Logging.ConfigLog(       tape,            sep);
        public   string ConfigLog(string title, Tape            tape,            string sep) => Logging.ConfigLog(title, tape,            sep);
        public   string ConfigLog(              TapeConfig      tapeConfig                 ) => Logging.ConfigLog(       tapeConfig          );
        public   string ConfigLog(              TapeActions     tapeActions                ) => Logging.ConfigLog(       tapeActions         );
        public   string ConfigLog(string title, TapeActions     tapeActions                ) => Logging.ConfigLog(title, tapeActions         );
        public   string ConfigLog(              TapeActions     tapeActions,     string sep) => Logging.ConfigLog(       tapeActions,     sep);
        public   string ConfigLog(string title, TapeActions     tapeActions,     string sep) => Logging.ConfigLog(title, tapeActions,     sep);
        public   string ConfigLog(              TapeAction      tapeAction                 ) => Logging.ConfigLog(       tapeAction          );
        public   string ConfigLog(string title, TapeAction      tapeAction                 ) => Logging.ConfigLog(title, tapeAction          );
        public   string ConfigLog(              TapeAction      tapeAction,      string sep) => Logging.ConfigLog(       tapeAction,      sep);
        public   string ConfigLog(string title, TapeAction      tapeAction,      string sep) => Logging.ConfigLog(title, tapeAction,      sep);
        public   string ConfigLog(              Buff            buff                       ) => Logging.ConfigLog(       buff                );
        public   string ConfigLog(string title, Buff            buff                       ) => Logging.ConfigLog(title, buff                );
        public   string ConfigLog(              Buff            buff,            string sep) => Logging.ConfigLog(       buff,            sep);
        public   string ConfigLog(string title, Buff            buff,            string sep) => Logging.ConfigLog(title, buff,            sep);
        public   string ConfigLog(              AudioFileOutput audioFileOutput            ) => Logging.ConfigLog(       audioFileOutput     );
        public   string ConfigLog(string title, AudioFileOutput audioFileOutput            ) => Logging.ConfigLog(title, audioFileOutput     );
        public   string ConfigLog(              AudioFileOutput audioFileOutput, string sep) => Logging.ConfigLog(       audioFileOutput, sep);
        public   string ConfigLog(string title, AudioFileOutput audioFileOutput, string sep) => Logging.ConfigLog(title, audioFileOutput, sep);
        public   string ConfigLog(              Sample          sample                     ) => Logging.ConfigLog(       sample              );
        public   string ConfigLog(string title, Sample          sample                     ) => Logging.ConfigLog(title, sample              );
        public   string ConfigLog(              Sample          sample,          string sep) => Logging.ConfigLog(       sample,          sep);
        public   string ConfigLog(string title, Sample          sample,          string sep) => Logging.ConfigLog(title, sample,          sep);
        public   string ConfigLog(              AudioInfoWish   audioInfoWish              ) => Logging.ConfigLog(       audioInfoWish       );
        public   string ConfigLog(string title, AudioInfoWish   audioInfoWish              ) => Logging.ConfigLog(title, audioInfoWish       );
        public   string ConfigLog(              AudioInfoWish   audioInfoWish,   string sep) => Logging.ConfigLog(       audioInfoWish,   sep);
        public   string ConfigLog(string title, AudioInfoWish   audioInfoWish,   string sep) => Logging.ConfigLog(title, audioInfoWish,   sep);
        public   string ConfigLog(              AudioFileInfo   audioFileInfo              ) => Logging.ConfigLog(       audioFileInfo       );
        public   string ConfigLog(string title, AudioFileInfo   audioFileInfo              ) => Logging.ConfigLog(title, audioFileInfo       );
        public   string ConfigLog(              AudioFileInfo   audioFileInfo,   string sep) => Logging.ConfigLog(       audioFileInfo,   sep);
        public   string ConfigLog(string title, AudioFileInfo   audioFileInfo,   string sep) => Logging.ConfigLog(title, audioFileInfo,   sep);
        public   string ConfigLog(              WavHeaderStruct wavHeaderStruct            ) => Logging.ConfigLog(       wavHeaderStruct     );
        public   string ConfigLog(string title, WavHeaderStruct wavHeaderStruct            ) => Logging.ConfigLog(title, wavHeaderStruct     );
        public   string ConfigLog(              WavHeaderStruct wavHeaderStruct, string sep) => Logging.ConfigLog(       wavHeaderStruct, sep);
        public   string ConfigLog(string title, WavHeaderStruct wavHeaderStruct, string sep) => Logging.ConfigLog(title, wavHeaderStruct, sep);

        public   void   LogConfig(                                                         ) => Logging.LogConfig(       this                );
        public   void   LogConfig(string title                                             ) => Logging.LogConfig(title, this                );
        public   void   LogConfig(string title,                                  string sep) => Logging.LogConfig(title, this,            sep);
        public   void   LogConfig(              FlowNode        flowNode                   ) => Logging.LogConfig(       flowNode            );
        public   void   LogConfig(string title, FlowNode        flowNode                   ) => Logging.LogConfig(title, flowNode            );
        public   void   LogConfig(              FlowNode        flowNode,        string sep) => Logging.LogConfig(       flowNode,        sep);
        public   void   LogConfig(string title, FlowNode        flowNode,        string sep) => Logging.LogConfig(title, flowNode,        sep);
        internal void   LogConfig(              ConfigResolver  configResolver             ) => Logging.LogConfig(       configResolver, this     );
        internal void   LogConfig(string title, ConfigResolver  configResolver             ) => Logging.LogConfig(title, configResolver, this     );
        internal void   LogConfig(              ConfigResolver  configResolver,  string sep) => Logging.LogConfig(       configResolver, this, sep);
        internal void   LogConfig(string title, ConfigResolver  configResolver,  string sep) => Logging.LogConfig(title, configResolver, this, sep);
        internal void   LogConfig(              ConfigSection   configSection              ) => Logging.LogConfig(       configSection       );
        internal void   LogConfig(string title, ConfigSection   configSection              ) => Logging.LogConfig(title, configSection       );
        internal void   LogConfig(              ConfigSection   configSection,   string sep) => Logging.LogConfig(       configSection,   sep);
        internal void   LogConfig(string title, ConfigSection   configSection,   string sep) => Logging.LogConfig(title, configSection,   sep);
        public   void   LogConfig(              Tape            tape                       ) => Logging.LogConfig(       tape                );
        public   void   LogConfig(string title, Tape            tape                       ) => Logging.LogConfig(title, tape                );
        public   void   LogConfig(              Tape            tape,            string sep) => Logging.LogConfig(       tape,            sep);
        public   void   LogConfig(string title, Tape            tape,            string sep) => Logging.LogConfig(title, tape,            sep);
        public   void   LogConfig(              TapeConfig      tapeConfig                 ) => Logging.LogConfig(       tapeConfig          );
        public   void   LogConfig(              TapeActions     tapeActions                ) => Logging.LogConfig(       tapeActions         );
        public   void   LogConfig(string title, TapeActions     tapeActions                ) => Logging.LogConfig(title, tapeActions         );
        public   void   LogConfig(              TapeActions     tapeActions,     string sep) => Logging.LogConfig(       tapeActions,     sep);
        public   void   LogConfig(string title, TapeActions     tapeActions,     string sep) => Logging.LogConfig(title, tapeActions,     sep);
        public   void   LogConfig(              TapeAction      tapeAction                 ) => Logging.LogConfig(       tapeAction          );
        public   void   LogConfig(string title, TapeAction      tapeAction                 ) => Logging.LogConfig(title, tapeAction          );
        public   void   LogConfig(              TapeAction      tapeAction,      string sep) => Logging.LogConfig(       tapeAction,      sep);
        public   void   LogConfig(string title, TapeAction      tapeAction,      string sep) => Logging.LogConfig(title, tapeAction,      sep);
        public   void   LogConfig(              Buff            buff                       ) => Logging.LogConfig(       buff                );
        public   void   LogConfig(string title, Buff            buff                       ) => Logging.LogConfig(title, buff                );
        public   void   LogConfig(              Buff            buff,            string sep) => Logging.LogConfig(       buff,            sep);
        public   void   LogConfig(string title, Buff            buff,            string sep) => Logging.LogConfig(title, buff,            sep);
        public   void   LogConfig(              AudioFileOutput audioFileOutput            ) => Logging.LogConfig(       audioFileOutput     );
        public   void   LogConfig(string title, AudioFileOutput audioFileOutput            ) => Logging.LogConfig(title, audioFileOutput     );
        public   void   LogConfig(              AudioFileOutput audioFileOutput, string sep) => Logging.LogConfig(       audioFileOutput, sep);
        public   void   LogConfig(string title, AudioFileOutput audioFileOutput, string sep) => Logging.LogConfig(title, audioFileOutput, sep);
        public   void   LogConfig(              Sample          sample                     ) => Logging.LogConfig(       sample              );
        public   void   LogConfig(string title, Sample          sample                     ) => Logging.LogConfig(title, sample              );
        public   void   LogConfig(              Sample          sample,          string sep) => Logging.LogConfig(       sample,          sep);
        public   void   LogConfig(string title, Sample          sample,          string sep) => Logging.LogConfig(title, sample,          sep);
        public   void   LogConfig(              AudioInfoWish   audioInfoWish              ) => Logging.LogConfig(       audioInfoWish       );
        public   void   LogConfig(string title, AudioInfoWish   audioInfoWish              ) => Logging.LogConfig(title, audioInfoWish       );
        public   void   LogConfig(              AudioInfoWish   audioInfoWish,   string sep) => Logging.LogConfig(       audioInfoWish,   sep);
        public   void   LogConfig(string title, AudioInfoWish   audioInfoWish,   string sep) => Logging.LogConfig(title, audioInfoWish,   sep);
        public   void   LogConfig(              AudioFileInfo   audioFileInfo              ) => Logging.LogConfig(       audioFileInfo       );
        public   void   LogConfig(string title, AudioFileInfo   audioFileInfo              ) => Logging.LogConfig(title, audioFileInfo       );
        public   void   LogConfig(              AudioFileInfo   audioFileInfo,   string sep) => Logging.LogConfig(       audioFileInfo,   sep);
        public   void   LogConfig(string title, AudioFileInfo   audioFileInfo,   string sep) => Logging.LogConfig(title, audioFileInfo,   sep);
        public   void   LogConfig(              WavHeaderStruct wavHeaderStruct            ) => Logging.LogConfig(       wavHeaderStruct     );
        public   void   LogConfig(string title, WavHeaderStruct wavHeaderStruct            ) => Logging.LogConfig(title, wavHeaderStruct     );
        public   void   LogConfig(              WavHeaderStruct wavHeaderStruct, string sep) => Logging.LogConfig(       wavHeaderStruct, sep);
        public   void   LogConfig(string title, WavHeaderStruct wavHeaderStruct, string sep) => Logging.LogConfig(title, wavHeaderStruct, sep);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        public   static string ConfigLog(this FlowNode        flowNode                                 ) => ResolveLogging(flowNode      ).ConfigLog(       flowNode            );
        public   static string ConfigLog(this FlowNode        flowNode,        string title            ) => ResolveLogging(flowNode      ).ConfigLog(title, flowNode            );
        public   static string ConfigLog(this FlowNode        flowNode,        string title, string sep) => ResolveLogging(flowNode      ).ConfigLog(title, flowNode,        sep);
        public   static string ConfigLog(this Tape            tape                                     ) => ResolveLogging(tape          ).ConfigLog(       tape                );
        public   static string ConfigLog(this Tape            tape,            string title            ) => ResolveLogging(tape          ).ConfigLog(title, tape                );
        public   static string ConfigLog(this Tape            tape,            string title, string sep) => ResolveLogging(tape          ).ConfigLog(title, tape,            sep);
        public   static string ConfigLog(this TapeConfig      tapeConfig                               ) => ResolveLogging(tapeConfig    ).ConfigLog(       tapeConfig          );
        public   static string ConfigLog(this TapeActions     tapeActions                              ) => ResolveLogging(tapeActions   ).ConfigLog(       tapeActions         );
        public   static string ConfigLog(this TapeActions     tapeActions,     string title            ) => ResolveLogging(tapeActions   ).ConfigLog(title, tapeActions         );
        public   static string ConfigLog(this TapeActions     tapeActions,     string title, string sep) => ResolveLogging(tapeActions   ).ConfigLog(title, tapeActions,     sep);
        public   static string ConfigLog(this TapeAction      tapeAction                               ) => ResolveLogging(tapeAction    ).ConfigLog(       tapeAction          );
        public   static string ConfigLog(this TapeAction      tapeAction ,     string title            ) => ResolveLogging(tapeAction    ).ConfigLog(title, tapeAction          );
        public   static string ConfigLog(this TapeAction      tapeAction ,     string title, string sep) => ResolveLogging(tapeAction    ).ConfigLog(title, tapeAction,      sep);
        public   static string ConfigLog(this Buff            buff                                     ) => ResolveLogging(buff          ).ConfigLog(       buff                );
        public   static string ConfigLog(this Buff            buff,            string title            ) => ResolveLogging(buff          ).ConfigLog(title, buff                );
        public   static string ConfigLog(this Buff            buff,            string title, string sep) => ResolveLogging(buff          ).ConfigLog(title, buff,            sep);
        internal static string ConfigLog(this ConfigResolver  configResolver                           ) => ResolveLogging(configResolver).ConfigLog(       configResolver      );
        internal static string ConfigLog(this ConfigResolver  configResolver,  string title            ) => ResolveLogging(configResolver).ConfigLog(title, configResolver      );
        internal static string ConfigLog(this ConfigResolver  configResolver,  string title, string sep) => ResolveLogging(configResolver).ConfigLog(title, configResolver,  sep);
        internal static string ConfigLog(this ConfigSection   configSection                                                     ) => ResolveLogging(configSection               ).ConfigLog(       configSection       );
        internal static string ConfigLog(this ConfigSection   configSection,                            string title            ) => ResolveLogging(configSection               ).ConfigLog(title, configSection       );
        internal static string ConfigLog(this ConfigSection   configSection,                            string title, string sep) => ResolveLogging(configSection               ).ConfigLog(title, configSection,   sep);
        internal static string ConfigLog(this ConfigSection   configSection,   SynthWishes synthWishes                          ) => ResolveLogging(configSection,   synthWishes).ConfigLog(       configSection       );
        internal static string ConfigLog(this ConfigSection   configSection,   SynthWishes synthWishes, string title            ) => ResolveLogging(configSection,   synthWishes).ConfigLog(title, configSection       );
        internal static string ConfigLog(this ConfigSection   configSection,   SynthWishes synthWishes, string title, string sep) => ResolveLogging(configSection,   synthWishes).ConfigLog(title, configSection,   sep);
        public   static string ConfigLog(this AudioFileOutput audioFileOutput                                                   ) => ResolveLogging(audioFileOutput             ).ConfigLog(       audioFileOutput     );
        public   static string ConfigLog(this AudioFileOutput audioFileOutput,                          string title            ) => ResolveLogging(audioFileOutput             ).ConfigLog(title, audioFileOutput     );
        public   static string ConfigLog(this AudioFileOutput audioFileOutput,                          string title, string sep) => ResolveLogging(audioFileOutput             ).ConfigLog(title, audioFileOutput, sep);
        public   static string ConfigLog(this AudioFileOutput audioFileOutput, SynthWishes synthWishes                          ) => ResolveLogging(audioFileOutput, synthWishes).ConfigLog(       audioFileOutput     );
        public   static string ConfigLog(this AudioFileOutput audioFileOutput, SynthWishes synthWishes, string title            ) => ResolveLogging(audioFileOutput, synthWishes).ConfigLog(title, audioFileOutput     );
        public   static string ConfigLog(this AudioFileOutput audioFileOutput, SynthWishes synthWishes, string title, string sep) => ResolveLogging(audioFileOutput, synthWishes).ConfigLog(title, audioFileOutput, sep);
        public   static string ConfigLog(this Sample          sample                                                            ) => ResolveLogging(sample                      ).ConfigLog(       sample              );
        public   static string ConfigLog(this Sample          sample,                                   string title            ) => ResolveLogging(sample                      ).ConfigLog(title, sample              );
        public   static string ConfigLog(this Sample          sample,                                   string title, string sep) => ResolveLogging(sample                      ).ConfigLog(title, sample,          sep);
        public   static string ConfigLog(this Sample          sample,          SynthWishes synthWishes                          ) => ResolveLogging(sample,          synthWishes).ConfigLog(       sample              );
        public   static string ConfigLog(this Sample          sample,          SynthWishes synthWishes, string title            ) => ResolveLogging(sample,          synthWishes).ConfigLog(title, sample              );
        public   static string ConfigLog(this Sample          sample,          SynthWishes synthWishes, string title, string sep) => ResolveLogging(sample,          synthWishes).ConfigLog(title, sample,          sep);
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish                                                     ) => ResolveLogging(audioInfoWish               ).ConfigLog(       audioInfoWish       );
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,                            string title            ) => ResolveLogging(audioInfoWish               ).ConfigLog(title, audioInfoWish       );
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,                            string title, string sep) => ResolveLogging(audioInfoWish               ).ConfigLog(title, audioInfoWish,   sep);
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes                          ) => ResolveLogging(audioInfoWish,   synthWishes).ConfigLog(       audioInfoWish       );
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes, string title            ) => ResolveLogging(audioInfoWish,   synthWishes).ConfigLog(title, audioInfoWish       );
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes, string title, string sep) => ResolveLogging(audioInfoWish,   synthWishes).ConfigLog(title, audioInfoWish,   sep);
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo                                                     ) => ResolveLogging(audioFileInfo               ).ConfigLog(       audioFileInfo       );
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,                            string title            ) => ResolveLogging(audioFileInfo               ).ConfigLog(title, audioFileInfo       );
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,                            string title, string sep) => ResolveLogging(audioFileInfo               ).ConfigLog(title, audioFileInfo,   sep);
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes                          ) => ResolveLogging(audioFileInfo,   synthWishes).ConfigLog(       audioFileInfo       );
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes, string title            ) => ResolveLogging(audioFileInfo,   synthWishes).ConfigLog(title, audioFileInfo       );
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes, string title, string sep) => ResolveLogging(audioFileInfo,   synthWishes).ConfigLog(title, audioFileInfo,   sep);
        public   static string ConfigLog(this WavHeaderStruct wavHeader                                                         ) => ResolveLogging(wavHeader                   ).ConfigLog(       wavHeader           );
        public   static string ConfigLog(this WavHeaderStruct wavHeader,                                string title            ) => ResolveLogging(wavHeader                   ).ConfigLog(title, wavHeader           );
        public   static string ConfigLog(this WavHeaderStruct wavHeader,                                string title, string sep) => ResolveLogging(wavHeader                   ).ConfigLog(title, wavHeader,       sep);
        public   static string ConfigLog(this WavHeaderStruct wavHeader,       SynthWishes synthWishes                          ) => ResolveLogging(wavHeader,       synthWishes).ConfigLog(       wavHeader           );
        public   static string ConfigLog(this WavHeaderStruct wavHeader,       SynthWishes synthWishes, string title            ) => ResolveLogging(wavHeader,       synthWishes).ConfigLog(title, wavHeader           );
        public   static string ConfigLog(this WavHeaderStruct wavHeader,       SynthWishes synthWishes, string title, string sep) => ResolveLogging(wavHeader,       synthWishes).ConfigLog(title, wavHeader,       sep);
        
        public   static void   LogConfig(this FlowNode        flowNode                                 ) => ResolveLogging(flowNode      ).LogConfig(       flowNode            );
        public   static void   LogConfig(this FlowNode        flowNode,        string title            ) => ResolveLogging(flowNode      ).LogConfig(title, flowNode            );
        public   static void   LogConfig(this FlowNode        flowNode,        string title, string sep) => ResolveLogging(flowNode      ).LogConfig(title, flowNode,        sep);
        public   static void   LogConfig(this Tape            tape                                     ) => ResolveLogging(tape          ).LogConfig(       tape                );
        public   static void   LogConfig(this Tape            tape,            string title            ) => ResolveLogging(tape          ).LogConfig(title, tape                );
        public   static void   LogConfig(this Tape            tape,            string title, string sep) => ResolveLogging(tape          ).LogConfig(title, tape,            sep);
        public   static void   LogConfig(this TapeConfig      tapeConfig                               ) => ResolveLogging(tapeConfig    ).LogConfig(       tapeConfig          );
        public   static void   LogConfig(this TapeActions     tapeActions                              ) => ResolveLogging(tapeActions   ).LogConfig(       tapeActions         );
        public   static void   LogConfig(this TapeActions     tapeActions,     string title            ) => ResolveLogging(tapeActions   ).LogConfig(title, tapeActions         );
        public   static void   LogConfig(this TapeActions     tapeActions,     string title, string sep) => ResolveLogging(tapeActions   ).LogConfig(title, tapeActions,     sep);
        public   static void   LogConfig(this TapeAction      tapeAction                               ) => ResolveLogging(tapeAction    ).LogConfig(       tapeAction          );
        public   static void   LogConfig(this TapeAction      tapeAction ,     string title            ) => ResolveLogging(tapeAction    ).LogConfig(title, tapeAction          );
        public   static void   LogConfig(this TapeAction      tapeAction ,     string title, string sep) => ResolveLogging(tapeAction    ).LogConfig(title, tapeAction,      sep);
        public   static void   LogConfig(this Buff            buff                                     ) => ResolveLogging(buff          ).LogConfig(       buff                );
        public   static void   LogConfig(this Buff            buff,            string title            ) => ResolveLogging(buff          ).LogConfig(title, buff                );
        public   static void   LogConfig(this Buff            buff,            string title, string sep) => ResolveLogging(buff          ).LogConfig(title, buff,            sep);
        internal static void   LogConfig(this ConfigResolver  configResolver                           ) => ResolveLogging(configResolver).LogConfig(       configResolver      );
        internal static void   LogConfig(this ConfigResolver  configResolver,  string title            ) => ResolveLogging(configResolver).LogConfig(title, configResolver      );
        internal static void   LogConfig(this ConfigResolver  configResolver,  string title, string sep) => ResolveLogging(configResolver).LogConfig(title, configResolver,  sep);
        internal static void   LogConfig(this ConfigSection   configSection                                                     ) => ResolveLogging(configSection               ).LogConfig(       configSection       );
        internal static void   LogConfig(this ConfigSection   configSection,                            string title            ) => ResolveLogging(configSection               ).LogConfig(title, configSection       );
        internal static void   LogConfig(this ConfigSection   configSection,                            string title, string sep) => ResolveLogging(configSection               ).LogConfig(title, configSection,   sep);
        internal static void   LogConfig(this ConfigSection   configSection,   SynthWishes synthWishes                          ) => ResolveLogging(configSection,   synthWishes).LogConfig(       configSection       );
        internal static void   LogConfig(this ConfigSection   configSection,   SynthWishes synthWishes, string title            ) => ResolveLogging(configSection,   synthWishes).LogConfig(title, configSection       );
        internal static void   LogConfig(this ConfigSection   configSection,   SynthWishes synthWishes, string title, string sep) => ResolveLogging(configSection,   synthWishes).LogConfig(title, configSection,   sep);
        public   static void   LogConfig(this AudioFileOutput audioFileOutput                                                   ) => ResolveLogging(audioFileOutput             ).LogConfig(       audioFileOutput     );
        public   static void   LogConfig(this AudioFileOutput audioFileOutput,                          string title            ) => ResolveLogging(audioFileOutput             ).LogConfig(title, audioFileOutput     );
        public   static void   LogConfig(this AudioFileOutput audioFileOutput,                          string title, string sep) => ResolveLogging(audioFileOutput             ).LogConfig(title, audioFileOutput, sep);
        public   static void   LogConfig(this AudioFileOutput audioFileOutput, SynthWishes synthWishes                          ) => ResolveLogging(audioFileOutput, synthWishes).LogConfig(       audioFileOutput     );
        public   static void   LogConfig(this AudioFileOutput audioFileOutput, SynthWishes synthWishes, string title            ) => ResolveLogging(audioFileOutput, synthWishes).LogConfig(title, audioFileOutput     );
        public   static void   LogConfig(this AudioFileOutput audioFileOutput, SynthWishes synthWishes, string title, string sep) => ResolveLogging(audioFileOutput, synthWishes).LogConfig(title, audioFileOutput, sep);
        public   static void   LogConfig(this Sample          sample                                                            ) => ResolveLogging(sample                      ).LogConfig(       sample              );
        public   static void   LogConfig(this Sample          sample,                                   string title            ) => ResolveLogging(sample                      ).LogConfig(title, sample              );
        public   static void   LogConfig(this Sample          sample,                                   string title, string sep) => ResolveLogging(sample                      ).LogConfig(title, sample,          sep);
        public   static void   LogConfig(this Sample          sample,          SynthWishes synthWishes                          ) => ResolveLogging(sample,          synthWishes).LogConfig(       sample              );
        public   static void   LogConfig(this Sample          sample,          SynthWishes synthWishes, string title            ) => ResolveLogging(sample,          synthWishes).LogConfig(title, sample              );
        public   static void   LogConfig(this Sample          sample,          SynthWishes synthWishes, string title, string sep) => ResolveLogging(sample,          synthWishes).LogConfig(title, sample,          sep);
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish                                                     ) => ResolveLogging(audioInfoWish               ).LogConfig(       audioInfoWish       );
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,                            string title            ) => ResolveLogging(audioInfoWish               ).LogConfig(title, audioInfoWish       );
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,                            string title, string sep) => ResolveLogging(audioInfoWish               ).LogConfig(title, audioInfoWish,   sep);
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes                          ) => ResolveLogging(audioInfoWish,   synthWishes).LogConfig(       audioInfoWish       );
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes, string title            ) => ResolveLogging(audioInfoWish,   synthWishes).LogConfig(title, audioInfoWish       );
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes, string title, string sep) => ResolveLogging(audioInfoWish,   synthWishes).LogConfig(title, audioInfoWish,   sep);
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo                                                     ) => ResolveLogging(audioFileInfo               ).LogConfig(       audioFileInfo       );
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,                            string title            ) => ResolveLogging(audioFileInfo               ).LogConfig(title, audioFileInfo       );
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,                            string title, string sep) => ResolveLogging(audioFileInfo               ).LogConfig(title, audioFileInfo,   sep);
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes                          ) => ResolveLogging(audioFileInfo,   synthWishes).LogConfig(       audioFileInfo       );
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes, string title            ) => ResolveLogging(audioFileInfo,   synthWishes).LogConfig(title, audioFileInfo       );
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes, string title, string sep) => ResolveLogging(audioFileInfo,   synthWishes).LogConfig(title, audioFileInfo,   sep);
        public   static void   LogConfig(this WavHeaderStruct wavHeader                                                         ) => ResolveLogging(wavHeader                   ).LogConfig(       wavHeader           );
        public   static void   LogConfig(this WavHeaderStruct wavHeader,                                string title            ) => ResolveLogging(wavHeader                   ).LogConfig(title, wavHeader           );
        public   static void   LogConfig(this WavHeaderStruct wavHeader,                                string title, string sep) => ResolveLogging(wavHeader                   ).LogConfig(title, wavHeader,       sep);
        public   static void   LogConfig(this WavHeaderStruct wavHeader,       SynthWishes synthWishes                          ) => ResolveLogging(wavHeader,       synthWishes).LogConfig(       wavHeader           );
        public   static void   LogConfig(this WavHeaderStruct wavHeader,       SynthWishes synthWishes, string title            ) => ResolveLogging(wavHeader,       synthWishes).LogConfig(title, wavHeader           );
        public   static void   LogConfig(this WavHeaderStruct wavHeader,       SynthWishes synthWishes, string title, string sep) => ResolveLogging(wavHeader,       synthWishes).LogConfig(title, wavHeader,       sep);
    }
}
