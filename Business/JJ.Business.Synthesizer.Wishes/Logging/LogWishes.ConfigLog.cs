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
using static JJ.Business.Synthesizer.Wishes.docs;

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

        public   void LogConfig(              SynthWishes     entity            ) => LogSpaced(ConfigLog(       entity     )); 
        public   void LogConfig(string title, SynthWishes     entity            ) => LogSpaced(ConfigLog(title, entity     ));
        public   void LogConfig(              SynthWishes     entity, string sep) => LogSpaced(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, SynthWishes     entity, string sep) => LogSpaced(ConfigLog(title, entity, sep));
        public   void LogConfig(              FlowNode        entity            ) => Log      (ConfigLog(       entity     )); 
        public   void LogConfig(string title, FlowNode        entity            ) => Log      (ConfigLog(title, entity     ));
        public   void LogConfig(              FlowNode        entity, string sep) => Log      (ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, FlowNode        entity, string sep) => Log      (ConfigLog(title, entity, sep));
        internal void LogConfig(              ConfigResolver  entity                                     ) => Log(ConfigLog(       entity                  )); 
        internal void LogConfig(string title, ConfigResolver  entity                                     ) => Log(ConfigLog(title, entity                  ));
        internal void LogConfig(              ConfigResolver  entity,                          string sep) => Log(ConfigLog(       entity,              sep)); 
        internal void LogConfig(string title, ConfigResolver  entity,                          string sep) => Log(ConfigLog(title, entity,              sep));
        internal void LogConfig(              ConfigResolver  entity, SynthWishes synthWishes            ) => Log(ConfigLog(       entity, synthWishes     )); 
        internal void LogConfig(string title, ConfigResolver  entity, SynthWishes synthWishes            ) => Log(ConfigLog(title, entity, synthWishes     ));
        internal void LogConfig(              ConfigResolver  entity, SynthWishes synthWishes, string sep) => Log(ConfigLog(       entity, synthWishes, sep)); 
        internal void LogConfig(string title, ConfigResolver  entity, SynthWishes synthWishes, string sep) => Log(ConfigLog(title, entity, synthWishes, sep));
        internal void LogConfig(              ConfigSection   entity            ) => Log(ConfigLog(       entity     ));
        internal void LogConfig(string title, ConfigSection   entity            ) => Log(ConfigLog(title, entity     ));
        internal void LogConfig(              ConfigSection   entity, string sep) => Log(ConfigLog(       entity, sep));
        internal void LogConfig(string title, ConfigSection   entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              Tape            entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, Tape            entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              Tape            entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, Tape            entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              TapeConfig      entity            ) => Log(ConfigLog(       entity     ));
        public   void LogConfig(              TapeActions     entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, TapeActions     entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              TapeActions     entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, TapeActions     entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              TapeAction      entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, TapeAction      entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              TapeAction      entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, TapeAction      entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              Buff            entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, Buff            entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              Buff            entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, Buff            entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              AudioFileOutput entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, AudioFileOutput entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              AudioFileOutput entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, AudioFileOutput entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              Sample          entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, Sample          entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              Sample          entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, Sample          entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              AudioInfoWish   entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, AudioInfoWish   entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              AudioInfoWish   entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, AudioInfoWish   entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              AudioFileInfo   entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, AudioFileInfo   entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              AudioFileInfo   entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, AudioFileInfo   entity, string sep) => Log(ConfigLog(title, entity, sep));
        public   void LogConfig(              WavHeaderStruct entity            ) => Log(ConfigLog(       entity     )); 
        public   void LogConfig(string title, WavHeaderStruct entity            ) => Log(ConfigLog(title, entity     ));
        public   void LogConfig(              WavHeaderStruct entity, string sep) => Log(ConfigLog(       entity, sep)); 
        public   void LogConfig(string title, WavHeaderStruct entity, string sep) => Log(ConfigLog(title, entity, sep));
    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public   string ConfigLog(                                                         ) => LogWishes.ConfigLog(       this                );
        public   string ConfigLog(string title                                             ) => LogWishes.ConfigLog(title, this                );
        public   string ConfigLog(string title,                                  string sep) => LogWishes.ConfigLog(title, this,            sep);
        public   string ConfigLog(              FlowNode        flowNode                   ) => LogWishes.ConfigLog(       flowNode            );
        public   string ConfigLog(string title, FlowNode        flowNode                   ) => LogWishes.ConfigLog(title, flowNode            );
        public   string ConfigLog(              FlowNode        flowNode,        string sep) => LogWishes.ConfigLog(       flowNode,        sep);
        public   string ConfigLog(string title, FlowNode        flowNode,        string sep) => LogWishes.ConfigLog(title, flowNode,        sep);
        internal string ConfigLog(              ConfigResolver  configResolver             ) => LogWishes.ConfigLog(       configResolver, this     );
        internal string ConfigLog(string title, ConfigResolver  configResolver             ) => LogWishes.ConfigLog(title, configResolver, this     );
        internal string ConfigLog(              ConfigResolver  configResolver,  string sep) => LogWishes.ConfigLog(       configResolver, this, sep);
        internal string ConfigLog(string title, ConfigResolver  configResolver,  string sep) => LogWishes.ConfigLog(title, configResolver, this, sep);
        internal string ConfigLog(              ConfigSection   configSection              ) => LogWishes.ConfigLog(       configSection       );
        internal string ConfigLog(string title, ConfigSection   configSection              ) => LogWishes.ConfigLog(title, configSection       );
        internal string ConfigLog(              ConfigSection   configSection,   string sep) => LogWishes.ConfigLog(       configSection,   sep);
        internal string ConfigLog(string title, ConfigSection   configSection,   string sep) => LogWishes.ConfigLog(title, configSection,   sep);
        public   string ConfigLog(              Tape            tape                       ) => LogWishes.ConfigLog(       tape                );
        public   string ConfigLog(string title, Tape            tape                       ) => LogWishes.ConfigLog(title, tape                );
        public   string ConfigLog(              Tape            tape,            string sep) => LogWishes.ConfigLog(       tape,            sep);
        public   string ConfigLog(string title, Tape            tape,            string sep) => LogWishes.ConfigLog(title, tape,            sep);
        public   string ConfigLog(              TapeConfig      tapeConfig                 ) => LogWishes.ConfigLog(       tapeConfig          );
        public   string ConfigLog(              TapeActions     tapeActions                ) => LogWishes.ConfigLog(       tapeActions         );
        public   string ConfigLog(string title, TapeActions     tapeActions                ) => LogWishes.ConfigLog(title, tapeActions         );
        public   string ConfigLog(              TapeActions     tapeActions,     string sep) => LogWishes.ConfigLog(       tapeActions,     sep);
        public   string ConfigLog(string title, TapeActions     tapeActions,     string sep) => LogWishes.ConfigLog(title, tapeActions,     sep);
        public   string ConfigLog(              TapeAction      tapeAction                 ) => LogWishes.ConfigLog(       tapeAction          );
        public   string ConfigLog(string title, TapeAction      tapeAction                 ) => LogWishes.ConfigLog(title, tapeAction          );
        public   string ConfigLog(              TapeAction      tapeAction,      string sep) => LogWishes.ConfigLog(       tapeAction,      sep);
        public   string ConfigLog(string title, TapeAction      tapeAction,      string sep) => LogWishes.ConfigLog(title, tapeAction,      sep);
        public   string ConfigLog(              Buff            buff                       ) => LogWishes.ConfigLog(       buff                );
        public   string ConfigLog(string title, Buff            buff                       ) => LogWishes.ConfigLog(title, buff                );
        public   string ConfigLog(              Buff            buff,            string sep) => LogWishes.ConfigLog(       buff,            sep);
        public   string ConfigLog(string title, Buff            buff,            string sep) => LogWishes.ConfigLog(title, buff,            sep);
        public   string ConfigLog(              AudioFileOutput audioFileOutput            ) => LogWishes.ConfigLog(       audioFileOutput     );
        public   string ConfigLog(string title, AudioFileOutput audioFileOutput            ) => LogWishes.ConfigLog(title, audioFileOutput     );
        public   string ConfigLog(              AudioFileOutput audioFileOutput, string sep) => LogWishes.ConfigLog(       audioFileOutput, sep);
        public   string ConfigLog(string title, AudioFileOutput audioFileOutput, string sep) => LogWishes.ConfigLog(title, audioFileOutput, sep);
        public   string ConfigLog(              Sample          sample                     ) => LogWishes.ConfigLog(       sample              );
        public   string ConfigLog(string title, Sample          sample                     ) => LogWishes.ConfigLog(title, sample              );
        public   string ConfigLog(              Sample          sample,          string sep) => LogWishes.ConfigLog(       sample,          sep);
        public   string ConfigLog(string title, Sample          sample,          string sep) => LogWishes.ConfigLog(title, sample,          sep);
        public   string ConfigLog(              AudioInfoWish   audioInfoWish              ) => LogWishes.ConfigLog(       audioInfoWish       );
        public   string ConfigLog(string title, AudioInfoWish   audioInfoWish              ) => LogWishes.ConfigLog(title, audioInfoWish       );
        public   string ConfigLog(              AudioInfoWish   audioInfoWish,   string sep) => LogWishes.ConfigLog(       audioInfoWish,   sep);
        public   string ConfigLog(string title, AudioInfoWish   audioInfoWish,   string sep) => LogWishes.ConfigLog(title, audioInfoWish,   sep);
        public   string ConfigLog(              AudioFileInfo   audioFileInfo              ) => LogWishes.ConfigLog(       audioFileInfo       );
        public   string ConfigLog(string title, AudioFileInfo   audioFileInfo              ) => LogWishes.ConfigLog(title, audioFileInfo       );
        public   string ConfigLog(              AudioFileInfo   audioFileInfo,   string sep) => LogWishes.ConfigLog(       audioFileInfo,   sep);
        public   string ConfigLog(string title, AudioFileInfo   audioFileInfo,   string sep) => LogWishes.ConfigLog(title, audioFileInfo,   sep);
        public   string ConfigLog(              WavHeaderStruct wavHeaderStruct            ) => LogWishes.ConfigLog(       wavHeaderStruct     );
        public   string ConfigLog(string title, WavHeaderStruct wavHeaderStruct            ) => LogWishes.ConfigLog(title, wavHeaderStruct     );
        public   string ConfigLog(              WavHeaderStruct wavHeaderStruct, string sep) => LogWishes.ConfigLog(       wavHeaderStruct, sep);
        public   string ConfigLog(string title, WavHeaderStruct wavHeaderStruct, string sep) => LogWishes.ConfigLog(title, wavHeaderStruct, sep);

        public   void   LogConfig(                                                         ) => LogWishes.LogConfig(       this                );
        public   void   LogConfig(string title                                             ) => LogWishes.LogConfig(title, this                );
        public   void   LogConfig(string title,                                  string sep) => LogWishes.LogConfig(title, this,            sep);
        public   void   LogConfig(              FlowNode        flowNode                   ) => LogWishes.LogConfig(       flowNode            );
        public   void   LogConfig(string title, FlowNode        flowNode                   ) => LogWishes.LogConfig(title, flowNode            );
        public   void   LogConfig(              FlowNode        flowNode,        string sep) => LogWishes.LogConfig(       flowNode,        sep);
        public   void   LogConfig(string title, FlowNode        flowNode,        string sep) => LogWishes.LogConfig(title, flowNode,        sep);
        internal void   LogConfig(              ConfigResolver  configResolver             ) => LogWishes.LogConfig(       configResolver, this     );
        internal void   LogConfig(string title, ConfigResolver  configResolver             ) => LogWishes.LogConfig(title, configResolver, this     );
        internal void   LogConfig(              ConfigResolver  configResolver,  string sep) => LogWishes.LogConfig(       configResolver, this, sep);
        internal void   LogConfig(string title, ConfigResolver  configResolver,  string sep) => LogWishes.LogConfig(title, configResolver, this, sep);
        internal void   LogConfig(              ConfigSection   configSection              ) => LogWishes.LogConfig(       configSection       );
        internal void   LogConfig(string title, ConfigSection   configSection              ) => LogWishes.LogConfig(title, configSection       );
        internal void   LogConfig(              ConfigSection   configSection,   string sep) => LogWishes.LogConfig(       configSection,   sep);
        internal void   LogConfig(string title, ConfigSection   configSection,   string sep) => LogWishes.LogConfig(title, configSection,   sep);
        public   void   LogConfig(              Tape            tape                       ) => LogWishes.LogConfig(       tape                );
        public   void   LogConfig(string title, Tape            tape                       ) => LogWishes.LogConfig(title, tape                );
        public   void   LogConfig(              Tape            tape,            string sep) => LogWishes.LogConfig(       tape,            sep);
        public   void   LogConfig(string title, Tape            tape,            string sep) => LogWishes.LogConfig(title, tape,            sep);
        public   void   LogConfig(              TapeConfig      tapeConfig                 ) => LogWishes.LogConfig(       tapeConfig          );
        public   void   LogConfig(              TapeActions     tapeActions                ) => LogWishes.LogConfig(       tapeActions         );
        public   void   LogConfig(string title, TapeActions     tapeActions                ) => LogWishes.LogConfig(title, tapeActions         );
        public   void   LogConfig(              TapeActions     tapeActions,     string sep) => LogWishes.LogConfig(       tapeActions,     sep);
        public   void   LogConfig(string title, TapeActions     tapeActions,     string sep) => LogWishes.LogConfig(title, tapeActions,     sep);
        public   void   LogConfig(              TapeAction      tapeAction                 ) => LogWishes.LogConfig(       tapeAction          );
        public   void   LogConfig(string title, TapeAction      tapeAction                 ) => LogWishes.LogConfig(title, tapeAction          );
        public   void   LogConfig(              TapeAction      tapeAction,      string sep) => LogWishes.LogConfig(       tapeAction,      sep);
        public   void   LogConfig(string title, TapeAction      tapeAction,      string sep) => LogWishes.LogConfig(title, tapeAction,      sep);
        public   void   LogConfig(              Buff            buff                       ) => LogWishes.LogConfig(       buff                );
        public   void   LogConfig(string title, Buff            buff                       ) => LogWishes.LogConfig(title, buff                );
        public   void   LogConfig(              Buff            buff,            string sep) => LogWishes.LogConfig(       buff,            sep);
        public   void   LogConfig(string title, Buff            buff,            string sep) => LogWishes.LogConfig(title, buff,            sep);
        public   void   LogConfig(              AudioFileOutput audioFileOutput            ) => LogWishes.LogConfig(       audioFileOutput     );
        public   void   LogConfig(string title, AudioFileOutput audioFileOutput            ) => LogWishes.LogConfig(title, audioFileOutput     );
        public   void   LogConfig(              AudioFileOutput audioFileOutput, string sep) => LogWishes.LogConfig(       audioFileOutput, sep);
        public   void   LogConfig(string title, AudioFileOutput audioFileOutput, string sep) => LogWishes.LogConfig(title, audioFileOutput, sep);
        public   void   LogConfig(              Sample          sample                     ) => LogWishes.LogConfig(       sample              );
        public   void   LogConfig(string title, Sample          sample                     ) => LogWishes.LogConfig(title, sample              );
        public   void   LogConfig(              Sample          sample,          string sep) => LogWishes.LogConfig(       sample,          sep);
        public   void   LogConfig(string title, Sample          sample,          string sep) => LogWishes.LogConfig(title, sample,          sep);
        public   void   LogConfig(              AudioInfoWish   audioInfoWish              ) => LogWishes.LogConfig(       audioInfoWish       );
        public   void   LogConfig(string title, AudioInfoWish   audioInfoWish              ) => LogWishes.LogConfig(title, audioInfoWish       );
        public   void   LogConfig(              AudioInfoWish   audioInfoWish,   string sep) => LogWishes.LogConfig(       audioInfoWish,   sep);
        public   void   LogConfig(string title, AudioInfoWish   audioInfoWish,   string sep) => LogWishes.LogConfig(title, audioInfoWish,   sep);
        public   void   LogConfig(              AudioFileInfo   audioFileInfo              ) => LogWishes.LogConfig(       audioFileInfo       );
        public   void   LogConfig(string title, AudioFileInfo   audioFileInfo              ) => LogWishes.LogConfig(title, audioFileInfo       );
        public   void   LogConfig(              AudioFileInfo   audioFileInfo,   string sep) => LogWishes.LogConfig(       audioFileInfo,   sep);
        public   void   LogConfig(string title, AudioFileInfo   audioFileInfo,   string sep) => LogWishes.LogConfig(title, audioFileInfo,   sep);
        public   void   LogConfig(              WavHeaderStruct wavHeaderStruct            ) => LogWishes.LogConfig(       wavHeaderStruct     );
        public   void   LogConfig(string title, WavHeaderStruct wavHeaderStruct            ) => LogWishes.LogConfig(title, wavHeaderStruct     );
        public   void   LogConfig(              WavHeaderStruct wavHeaderStruct, string sep) => LogWishes.LogConfig(       wavHeaderStruct, sep);
        public   void   LogConfig(string title, WavHeaderStruct wavHeaderStruct, string sep) => LogWishes.LogConfig(title, wavHeaderStruct, sep);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        public   static string ConfigLog(this FlowNode        flowNode                                 ) => flowNode      .GetLogWishes().ConfigLog(       flowNode            );
        public   static string ConfigLog(this FlowNode        flowNode,        string title            ) => flowNode      .GetLogWishes().ConfigLog(title, flowNode            );
        public   static string ConfigLog(this FlowNode        flowNode,        string title, string sep) => flowNode      .GetLogWishes().ConfigLog(title, flowNode,        sep);
        public   static string ConfigLog(this Tape            tape                                     ) => tape          .GetLogWishes().ConfigLog(       tape                );
        public   static string ConfigLog(this Tape            tape,            string title            ) => tape          .GetLogWishes().ConfigLog(title, tape                );
        public   static string ConfigLog(this Tape            tape,            string title, string sep) => tape          .GetLogWishes().ConfigLog(title, tape,            sep);
        public   static string ConfigLog(this TapeConfig      tapeConfig                               ) => tapeConfig    .GetLogWishes().ConfigLog(       tapeConfig          );
        public   static string ConfigLog(this TapeActions     tapeActions                              ) => tapeActions   .GetLogWishes().ConfigLog(       tapeActions         );
        public   static string ConfigLog(this TapeActions     tapeActions,     string title            ) => tapeActions   .GetLogWishes().ConfigLog(title, tapeActions         );
        public   static string ConfigLog(this TapeActions     tapeActions,     string title, string sep) => tapeActions   .GetLogWishes().ConfigLog(title, tapeActions,     sep);
        public   static string ConfigLog(this TapeAction      tapeAction                               ) => tapeAction    .GetLogWishes().ConfigLog(       tapeAction          );
        public   static string ConfigLog(this TapeAction      tapeAction ,     string title            ) => tapeAction    .GetLogWishes().ConfigLog(title, tapeAction          );
        public   static string ConfigLog(this TapeAction      tapeAction ,     string title, string sep) => tapeAction    .GetLogWishes().ConfigLog(title, tapeAction,      sep);
        public   static string ConfigLog(this Buff            buff                                     ) => buff          .GetLogWishes().ConfigLog(       buff                );
        public   static string ConfigLog(this Buff            buff,            string title            ) => buff          .GetLogWishes().ConfigLog(title, buff                );
        public   static string ConfigLog(this Buff            buff,            string title, string sep) => buff          .GetLogWishes().ConfigLog(title, buff,            sep);
        internal static string ConfigLog(this ConfigResolver  configResolver                           ) => configResolver.GetLogWishes().ConfigLog(       configResolver      );
        internal static string ConfigLog(this ConfigResolver  configResolver,  string title            ) => configResolver.GetLogWishes().ConfigLog(title, configResolver      );
        internal static string ConfigLog(this ConfigResolver  configResolver,  string title, string sep) => configResolver.GetLogWishes().ConfigLog(title, configResolver,  sep);
        internal static string ConfigLog(this ConfigSection   configSection                                                     ) => configSection  .GetLogWishes(           ).ConfigLog(       configSection       );
        internal static string ConfigLog(this ConfigSection   configSection,                            string title            ) => configSection  .GetLogWishes(           ).ConfigLog(title, configSection       );
        internal static string ConfigLog(this ConfigSection   configSection,                            string title, string sep) => configSection  .GetLogWishes(           ).ConfigLog(title, configSection,   sep);
        internal static string ConfigLog(this ConfigSection   configSection,   SynthWishes synthWishes                          ) => configSection  .GetLogWishes(synthWishes).ConfigLog(       configSection       );
        internal static string ConfigLog(this ConfigSection   configSection,   SynthWishes synthWishes, string title            ) => configSection  .GetLogWishes(synthWishes).ConfigLog(title, configSection       );
        internal static string ConfigLog(this ConfigSection   configSection,   SynthWishes synthWishes, string title, string sep) => configSection  .GetLogWishes(synthWishes).ConfigLog(title, configSection,   sep);
        public   static string ConfigLog(this AudioFileOutput audioFileOutput                                                   ) => audioFileOutput.GetLogWishes(           ).ConfigLog(       audioFileOutput     );
        public   static string ConfigLog(this AudioFileOutput audioFileOutput,                          string title            ) => audioFileOutput.GetLogWishes(           ).ConfigLog(title, audioFileOutput     );
        public   static string ConfigLog(this AudioFileOutput audioFileOutput,                          string title, string sep) => audioFileOutput.GetLogWishes(           ).ConfigLog(title, audioFileOutput, sep);
        public   static string ConfigLog(this AudioFileOutput audioFileOutput, SynthWishes synthWishes                          ) => audioFileOutput.GetLogWishes(synthWishes).ConfigLog(       audioFileOutput     );
        public   static string ConfigLog(this AudioFileOutput audioFileOutput, SynthWishes synthWishes, string title            ) => audioFileOutput.GetLogWishes(synthWishes).ConfigLog(title, audioFileOutput     );
        public   static string ConfigLog(this AudioFileOutput audioFileOutput, SynthWishes synthWishes, string title, string sep) => audioFileOutput.GetLogWishes(synthWishes).ConfigLog(title, audioFileOutput, sep);
        public   static string ConfigLog(this Sample          sample                                                            ) => sample         .GetLogWishes(           ).ConfigLog(       sample              );
        public   static string ConfigLog(this Sample          sample,                                   string title            ) => sample         .GetLogWishes(           ).ConfigLog(title, sample              );
        public   static string ConfigLog(this Sample          sample,                                   string title, string sep) => sample         .GetLogWishes(           ).ConfigLog(title, sample,          sep);
        public   static string ConfigLog(this Sample          sample,          SynthWishes synthWishes                          ) => sample         .GetLogWishes(synthWishes).ConfigLog(       sample              );
        public   static string ConfigLog(this Sample          sample,          SynthWishes synthWishes, string title            ) => sample         .GetLogWishes(synthWishes).ConfigLog(title, sample              );
        public   static string ConfigLog(this Sample          sample,          SynthWishes synthWishes, string title, string sep) => sample         .GetLogWishes(synthWishes).ConfigLog(title, sample,          sep);
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish                                                     ) => audioInfoWish  .GetLogWishes(           ).ConfigLog(       audioInfoWish       );
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,                            string title            ) => audioInfoWish  .GetLogWishes(           ).ConfigLog(title, audioInfoWish       );
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,                            string title, string sep) => audioInfoWish  .GetLogWishes(           ).ConfigLog(title, audioInfoWish,   sep);
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes                          ) => audioInfoWish  .GetLogWishes(synthWishes).ConfigLog(       audioInfoWish       );
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes, string title            ) => audioInfoWish  .GetLogWishes(synthWishes).ConfigLog(title, audioInfoWish       );
        public   static string ConfigLog(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes, string title, string sep) => audioInfoWish  .GetLogWishes(synthWishes).ConfigLog(title, audioInfoWish,   sep);
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo                                                     ) => audioFileInfo  .GetLogWishes(           ).ConfigLog(       audioFileInfo       );
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,                            string title            ) => audioFileInfo  .GetLogWishes(           ).ConfigLog(title, audioFileInfo       );
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,                            string title, string sep) => audioFileInfo  .GetLogWishes(           ).ConfigLog(title, audioFileInfo,   sep);
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes                          ) => audioFileInfo  .GetLogWishes(synthWishes).ConfigLog(       audioFileInfo       );
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes, string title            ) => audioFileInfo  .GetLogWishes(synthWishes).ConfigLog(title, audioFileInfo       );
        public   static string ConfigLog(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes, string title, string sep) => audioFileInfo  .GetLogWishes(synthWishes).ConfigLog(title, audioFileInfo,   sep);
        public   static string ConfigLog(this WavHeaderStruct wavHeader                                                         ) => wavHeader      .GetLogWishes(           ).ConfigLog(       wavHeader           );
        public   static string ConfigLog(this WavHeaderStruct wavHeader,                                string title            ) => wavHeader      .GetLogWishes(           ).ConfigLog(title, wavHeader           );
        public   static string ConfigLog(this WavHeaderStruct wavHeader,                                string title, string sep) => wavHeader      .GetLogWishes(           ).ConfigLog(title, wavHeader,       sep);
        public   static string ConfigLog(this WavHeaderStruct wavHeader,       SynthWishes synthWishes                          ) => wavHeader      .GetLogWishes(synthWishes).ConfigLog(       wavHeader           );
        public   static string ConfigLog(this WavHeaderStruct wavHeader,       SynthWishes synthWishes, string title            ) => wavHeader      .GetLogWishes(synthWishes).ConfigLog(title, wavHeader           );
        public   static string ConfigLog(this WavHeaderStruct wavHeader,       SynthWishes synthWishes, string title, string sep) => wavHeader      .GetLogWishes(synthWishes).ConfigLog(title, wavHeader,       sep);
        
        public   static void   LogConfig(this FlowNode        flowNode                                 ) => flowNode      .GetLogWishes().LogConfig(       flowNode            );
        public   static void   LogConfig(this FlowNode        flowNode,        string title            ) => flowNode      .GetLogWishes().LogConfig(title, flowNode            );
        public   static void   LogConfig(this FlowNode        flowNode,        string title, string sep) => flowNode      .GetLogWishes().LogConfig(title, flowNode,        sep);
        public   static void   LogConfig(this Tape            tape                                     ) => tape          .GetLogWishes().LogConfig(       tape                );
        public   static void   LogConfig(this Tape            tape,            string title            ) => tape          .GetLogWishes().LogConfig(title, tape                );
        public   static void   LogConfig(this Tape            tape,            string title, string sep) => tape          .GetLogWishes().LogConfig(title, tape,            sep);
        public   static void   LogConfig(this TapeConfig      tapeConfig                               ) => tapeConfig    .GetLogWishes().LogConfig(       tapeConfig          );
        public   static void   LogConfig(this TapeActions     tapeActions                              ) => tapeActions   .GetLogWishes().LogConfig(       tapeActions         );
        public   static void   LogConfig(this TapeActions     tapeActions,     string title            ) => tapeActions   .GetLogWishes().LogConfig(title, tapeActions         );
        public   static void   LogConfig(this TapeActions     tapeActions,     string title, string sep) => tapeActions   .GetLogWishes().LogConfig(title, tapeActions,     sep);
        public   static void   LogConfig(this TapeAction      tapeAction                               ) => tapeAction    .GetLogWishes().LogConfig(       tapeAction          );
        public   static void   LogConfig(this TapeAction      tapeAction ,     string title            ) => tapeAction    .GetLogWishes().LogConfig(title, tapeAction          );
        public   static void   LogConfig(this TapeAction      tapeAction ,     string title, string sep) => tapeAction    .GetLogWishes().LogConfig(title, tapeAction,      sep);
        public   static void   LogConfig(this Buff            buff                                     ) => buff          .GetLogWishes().LogConfig(       buff                );
        public   static void   LogConfig(this Buff            buff,            string title            ) => buff          .GetLogWishes().LogConfig(title, buff                );
        public   static void   LogConfig(this Buff            buff,            string title, string sep) => buff          .GetLogWishes().LogConfig(title, buff,            sep);
        internal static void   LogConfig(this ConfigResolver  configResolver                           ) => configResolver.GetLogWishes().LogConfig(       configResolver      );
        internal static void   LogConfig(this ConfigResolver  configResolver,  string title            ) => configResolver.GetLogWishes().LogConfig(title, configResolver      );
        internal static void   LogConfig(this ConfigResolver  configResolver,  string title, string sep) => configResolver.GetLogWishes().LogConfig(title, configResolver,  sep);
        internal static void   LogConfig(this ConfigSection   configSection                                                     ) => configSection  .GetLogWishes(           ).LogConfig(       configSection       );
        internal static void   LogConfig(this ConfigSection   configSection,                            string title            ) => configSection  .GetLogWishes(           ).LogConfig(title, configSection       );
        internal static void   LogConfig(this ConfigSection   configSection,                            string title, string sep) => configSection  .GetLogWishes(           ).LogConfig(title, configSection,   sep);
        internal static void   LogConfig(this ConfigSection   configSection,   SynthWishes synthWishes                          ) => configSection  .GetLogWishes(synthWishes).LogConfig(       configSection       );
        internal static void   LogConfig(this ConfigSection   configSection,   SynthWishes synthWishes, string title            ) => configSection  .GetLogWishes(synthWishes).LogConfig(title, configSection       );
        internal static void   LogConfig(this ConfigSection   configSection,   SynthWishes synthWishes, string title, string sep) => configSection  .GetLogWishes(synthWishes).LogConfig(title, configSection,   sep);
        public   static void   LogConfig(this AudioFileOutput audioFileOutput                                                   ) => audioFileOutput.GetLogWishes(           ).LogConfig(       audioFileOutput     );
        public   static void   LogConfig(this AudioFileOutput audioFileOutput,                          string title            ) => audioFileOutput.GetLogWishes(           ).LogConfig(title, audioFileOutput     );
        public   static void   LogConfig(this AudioFileOutput audioFileOutput,                          string title, string sep) => audioFileOutput.GetLogWishes(           ).LogConfig(title, audioFileOutput, sep);
        public   static void   LogConfig(this AudioFileOutput audioFileOutput, SynthWishes synthWishes                          ) => audioFileOutput.GetLogWishes(synthWishes).LogConfig(       audioFileOutput     );
        public   static void   LogConfig(this AudioFileOutput audioFileOutput, SynthWishes synthWishes, string title            ) => audioFileOutput.GetLogWishes(synthWishes).LogConfig(title, audioFileOutput     );
        public   static void   LogConfig(this AudioFileOutput audioFileOutput, SynthWishes synthWishes, string title, string sep) => audioFileOutput.GetLogWishes(synthWishes).LogConfig(title, audioFileOutput, sep);
        public   static void   LogConfig(this Sample          sample                                                            ) => sample         .GetLogWishes(           ).LogConfig(       sample              );
        public   static void   LogConfig(this Sample          sample,                                   string title            ) => sample         .GetLogWishes(           ).LogConfig(title, sample              );
        public   static void   LogConfig(this Sample          sample,                                   string title, string sep) => sample         .GetLogWishes(           ).LogConfig(title, sample,          sep);
        public   static void   LogConfig(this Sample          sample,          SynthWishes synthWishes                          ) => sample         .GetLogWishes(synthWishes).LogConfig(       sample              );
        public   static void   LogConfig(this Sample          sample,          SynthWishes synthWishes, string title            ) => sample         .GetLogWishes(synthWishes).LogConfig(title, sample              );
        public   static void   LogConfig(this Sample          sample,          SynthWishes synthWishes, string title, string sep) => sample         .GetLogWishes(synthWishes).LogConfig(title, sample,          sep);
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish                                                     ) => audioInfoWish  .GetLogWishes(           ).LogConfig(       audioInfoWish       );
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,                            string title            ) => audioInfoWish  .GetLogWishes(           ).LogConfig(title, audioInfoWish       );
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,                            string title, string sep) => audioInfoWish  .GetLogWishes(           ).LogConfig(title, audioInfoWish,   sep);
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes                          ) => audioInfoWish  .GetLogWishes(synthWishes).LogConfig(       audioInfoWish       );
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes, string title            ) => audioInfoWish  .GetLogWishes(synthWishes).LogConfig(title, audioInfoWish       );
        public   static void   LogConfig(this AudioInfoWish   audioInfoWish,   SynthWishes synthWishes, string title, string sep) => audioInfoWish  .GetLogWishes(synthWishes).LogConfig(title, audioInfoWish,   sep);
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo                                                     ) => audioFileInfo  .GetLogWishes(           ).LogConfig(       audioFileInfo       );
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,                            string title            ) => audioFileInfo  .GetLogWishes(           ).LogConfig(title, audioFileInfo       );
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,                            string title, string sep) => audioFileInfo  .GetLogWishes(           ).LogConfig(title, audioFileInfo,   sep);
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes                          ) => audioFileInfo  .GetLogWishes(synthWishes).LogConfig(       audioFileInfo       );
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes, string title            ) => audioFileInfo  .GetLogWishes(synthWishes).LogConfig(title, audioFileInfo       );
        public   static void   LogConfig(this AudioFileInfo   audioFileInfo,   SynthWishes synthWishes, string title, string sep) => audioFileInfo  .GetLogWishes(synthWishes).LogConfig(title, audioFileInfo,   sep);
        public   static void   LogConfig(this WavHeaderStruct wavHeader                                                         ) => wavHeader      .GetLogWishes(           ).LogConfig(       wavHeader           );
        public   static void   LogConfig(this WavHeaderStruct wavHeader,                                string title            ) => wavHeader      .GetLogWishes(           ).LogConfig(title, wavHeader           );
        public   static void   LogConfig(this WavHeaderStruct wavHeader,                                string title, string sep) => wavHeader      .GetLogWishes(           ).LogConfig(title, wavHeader,       sep);
        public   static void   LogConfig(this WavHeaderStruct wavHeader,       SynthWishes synthWishes                          ) => wavHeader      .GetLogWishes(synthWishes).LogConfig(       wavHeader           );
        public   static void   LogConfig(this WavHeaderStruct wavHeader,       SynthWishes synthWishes, string title            ) => wavHeader      .GetLogWishes(synthWishes).LogConfig(title, wavHeader           );
        public   static void   LogConfig(this WavHeaderStruct wavHeader,       SynthWishes synthWishes, string title, string sep) => wavHeader      .GetLogWishes(synthWishes).LogConfig(title, wavHeader,       sep);
    }
}
