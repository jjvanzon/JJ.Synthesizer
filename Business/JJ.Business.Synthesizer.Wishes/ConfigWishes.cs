using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.EnvironmentHelperWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Configuration_Wishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Testing_Wishes;
// ReSharper disable RedundantNameQualifier

namespace JJ.Business.Synthesizer.Wishes
{
    // Config XML
    
    public enum TimeOutActionEnum
    {
        Undefined,
        Continue,
        Log,
        Stop
    }
    
    internal class ConfigSection
    {
        // Audio Quality
        
        [XmlAttribute] public int? Bits { get; set; }
        [XmlAttribute] public int? Channels { get; set; }
        [XmlAttribute] public int? SamplingRate { get; set; }
        [XmlAttribute] public AudioFileFormatEnum? AudioFormat { get; set; }
        [XmlAttribute] public InterpolationTypeEnum? Interpolation { get; set; }
        
        // Audio Lengths
        
        [XmlAttribute] public double? NoteLength { get; set; }
        [XmlAttribute] public double? BarLength { get; set; }
        [XmlAttribute] public double? BeatLength { get; set; }
        [XmlAttribute] public double? AudioLength { get; set; }
        [XmlAttribute] public double? LeadingSilence { get; set; }
        [XmlAttribute] public double? TrailingSilence { get; set; }

        // Feature Toggles
        
        [XmlAttribute] public bool? Play { get; set; }
        [XmlAttribute] public bool? MathBoost { get; set; }
        [XmlAttribute] public bool? ParallelTaping { get; set; }
        [XmlAttribute] public bool? CacheToDisk { get; set; }
        [XmlAttribute] public bool? PlayAllTapes { get; set; }
        
        // Tooling
        
        public ConfigToolingElement AzurePipelines { get; set; } = new ConfigToolingElement();
        public ConfigToolingElement NCrunch { get; set; } = new ConfigToolingElement();
        
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        [XmlAttribute] public double? LeafCheckTimeOut { get; set; }
        /// <inheritdoc cref="docs._leafchecktimeout" />
        [XmlAttribute] public TimeOutActionEnum? TimeOutAction { get; set; }
        [XmlAttribute] public int? ExtraBufferFrames { get; set; }
        [XmlAttribute] public string LongTestCategory { get; set; }
    }

    internal class ConfigToolingElement
    {
        [XmlAttribute] public int? SamplingRate { get; set; }
        [XmlAttribute] public int? SamplingRateLongRunning { get; set; }
        [XmlAttribute] public bool? Play { get; set; }
        [XmlAttribute] public bool? Impersonation { get; set; }
    }
    
    // ConfigWishes
    
    public class ConfigWishes
    {
        /// <summary> For static contexts use this. </summary>
        internal static ConfigWishes Default { get; } = new ConfigWishes();

        // Defaults

        // Audio Quality

        private const int                   DefaultBits          = 32;
        private const int                   DefaultChannels      = 1;
        private const int                   DefaultChannel       = 0;
        private const int                   DefaultSamplingRate  = 48000;
        private const AudioFileFormatEnum   DefaultAudioFormat   = Wav;
        private const InterpolationTypeEnum DefaultInterpolation = Line;

        // Audio Lengths
        
        private const double DefaultNoteLength      = 0.50;
        private const double DefaultBarLength       = 1.00;
        private const double DefaultBeatLength      = 0.25;
        private const double DefaultAudioLength     = 1.00;
        private const double DefaultLeadingSilence  = 0.25;
        private const double DefaultTrailingSilence = 0.25;
        
        // Feature Toggles
        
        private const bool   DefaultPlay           = true;
        private const bool   DefaultMathBoost      = true;
        private const bool   DefaultParallelTaping = true;
        private const bool   DefaultCacheToDisk    = false;
        private const bool   DefaultPlayAllTapes   = false;

        // Tooling
        
        private const bool   DefaultToolingPlay                           = false;
        private const bool   DefaultToolingImpersonation                  = false;
        private const int    DefaultNCrunchSamplingRate                   = 150;
        private const int    DefaultNCrunchSamplingRateLongRunning        = 8;
        private const int    DefaultAzurePipelinesSamplingRate            = 1500;
        private const int    DefaultAzurePipelinesSamplingRateLongRunning = 100;
        
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        private const double DefaultLeafCheckTimeOut         = 60;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        private const TimeOutActionEnum DefaultTimeOutAction = TimeOutActionEnum.Continue;
        private const int DefaultExtraBufferFrames           = 4;
        private const string DefaultLongTestCategory         = "Long";

        // Environment Variables
        
        private const string NCrunchEnvironmentVariableName         = "NCrunch";
        private const string AzurePipelinesEnvironmentVariableValue = "True";
        private const string AzurePipelinesEnvironmentVariableName  = "TF_BUILD";
        private const string NCrunchEnvironmentVariableValue        = "1";

        private static readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        // Audio Quality
        
        // Bits
        
        private SampleDataTypeEnum _sampleDataTypeEnum;
        public int GetBits => _sampleDataTypeEnum != default ? _sampleDataTypeEnum.GetBits() : _section.Bits ?? DefaultBits;
        public void WithBits(int bits) => _sampleDataTypeEnum = bits.ToSampleDataTypeEnum();
        public bool Is32Bit => GetBits == 32;
        public void With32Bit() => WithBits(32);
        public bool Is16Bit => GetBits == 16;
        public void With16Bit() => WithBits(16);
        public bool Is8Bit => GetBits == 8;
        public void With8Bit() => WithBits(8);
        
        // Channels
        
        private SpeakerSetupEnum _speakerSetupEnum;
        public int GetChannels => _speakerSetupEnum != default ? _speakerSetupEnum.GetChannels() : _section.Channels ?? DefaultChannels;
        public void WithChannels(int channels) => _speakerSetupEnum = channels.ToSpeakerSetupEnum();
        public bool IsMono => GetChannels == 1;
        public void WithMono() => WithChannels(1);
        public bool IsStereo => GetChannels == 2;
        public void WithStereo() => WithChannels(2);
        
        // Channel
        
        private     ChannelEnum                  _channelEnum = DefaultChannel.ToChannelEnum(DefaultChannels);
        public int? GetChannel                => _channelEnum == default ? default(int?) : _channelEnum.ToChannel();
        public void WithChannel(int? channel) => _channelEnum = channel?.ToChannelEnum(GetChannels) ?? default;
        public bool IsCenter  =>   GetChannels == 1 ? GetChannel == 0 : default;
        public void WithCenter() { WithChannels  (1); WithChannel  (0);        }
        public bool IsLeft    =>   GetChannels == 2 ? GetChannel == 0 : default;
        public void WithLeft  () { WithChannels  (2); WithChannel  (0);        }
        public bool IsRight   =>   GetChannels == 2 ? GetChannel == 1 : default;
        public void WithRight () { WithChannels  (2); WithChannel  (1);        }

        // SamplingRate
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        internal int _samplingRate;
        /// <inheritdoc cref="docs._getsamplingrate" />
        public void WithSamplingRate(int value) => _samplingRate = value;
        
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public int GetSamplingRate
        {
            get
            {
                if (_samplingRate != 0)
                {
                    return _samplingRate;
                }
                
                if (IsUnderNCrunch)
                {
                    bool testIsLong = CurrentTestIsInCategory(GetLongTestCategory);
                    
                    if (testIsLong)
                    {
                        return _section.NCrunch.SamplingRateLongRunning ?? DefaultNCrunchSamplingRateLongRunning;
                    }
                    else
                    {
                        return _section.NCrunch.SamplingRate ?? DefaultNCrunchSamplingRate;
                    }
                }
                
                if (IsUnderAzurePipelines)
                {
                    bool testIsLong = CurrentTestIsInCategory(GetLongTestCategory);
                    
                    if (testIsLong)
                    {
                        return _section.AzurePipelines.SamplingRateLongRunning ?? DefaultAzurePipelinesSamplingRateLongRunning;
                    }
                    else
                    {
                        return _section.AzurePipelines.SamplingRate ?? DefaultAzurePipelinesSamplingRate;
                    }
                }
                
                return _section.SamplingRate ?? DefaultSamplingRate;
            }
        }
        
        // AudioFormat
        
        private AudioFileFormatEnum _audioFormat;
        public AudioFileFormatEnum GetAudioFormat => _audioFormat != default ? _audioFormat : _section.AudioFormat ?? DefaultAudioFormat;
        public void WithAudioFormat(AudioFileFormatEnum audioFormat) => _audioFormat = audioFormat;
        public bool IsWav => GetAudioFormat == Wav;
        public void AsWav() => WithAudioFormat(Wav);
        public bool IsRaw => GetAudioFormat == Raw;
        public void AsRaw() => WithAudioFormat(Raw);
        
        // Interpolation
        
        private InterpolationTypeEnum _interpolation;
        public InterpolationTypeEnum GetInterpolation => _interpolation != default ? _interpolation : _section.Interpolation ?? DefaultInterpolation;
        public void WithInterpolation(InterpolationTypeEnum interpolation) => _interpolation = interpolation;
        public bool IsLinear => GetInterpolation == Line;
        public void WithLinear() => WithInterpolation(Line);
        public bool IsBlocky => GetInterpolation == Block;
        public void WithBlocky() => WithInterpolation(Block);

        // Durations
        
        // NoteLength
        
        private FlowNode _noteLength;
        
        public FlowNode GetNoteLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_noteLength != null)
            {
                return _noteLength;
            }
            
            if (_beatLength != null)
            {
                return _beatLength;
            }
            
            return synthWishes[_section.NoteLength ?? DefaultNoteLength];
        }
        
        public FlowNode SnapNoteLength(SynthWishes synthWishes, FlowNode noteLength)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            noteLength = noteLength ?? GetNoteLength(synthWishes);

            // Take snapshot value of noteLength,
            // for consistent volume curve lengths and buffer size cut-offs.
            double value = noteLength.Value;
            return synthWishes.Value(value);
        }
        
        public void WithNoteLength(FlowNode noteLength) => _noteLength = noteLength ?? throw new NullException(() => noteLength);
        
        public void WithNoteLength(double noteLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithNoteLength(synthWishes[noteLength]);
        }
        
        public void ResetNoteLength() => _noteLength = null;
        
        // BarLength
        
        private FlowNode _barLength;
        
        public FlowNode GetBarLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_barLength != null)
            {
                return _barLength;
            }
            
            if (_beatLength != null)
            {
                return _beatLength * 4;
            }

            return synthWishes[_section.BarLength ?? DefaultBarLength];
        }
        
        public void WithBarLength(FlowNode barLength) => _barLength = barLength ?? throw new NullException(() => barLength);
        
        public void WithBarLength(double barLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithBarLength(synthWishes[barLength]);
        }
        
        public void ResetBarLength() => _barLength = null;

        // BeatLength
        
        private FlowNode _beatLength;
        
        public FlowNode GetBeatLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_beatLength != null)
            {
                return _beatLength;
            }
            
            if (_barLength != null)
            {
                return _barLength * 0.25;
            }
            
            return synthWishes[_section.BeatLength ?? DefaultBeatLength];
        }
        
        public void WithBeatLength(FlowNode beatLength) => _beatLength = beatLength ?? throw new NullException(() => beatLength);
        
        public void WithBeatLength(double beatLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithBeatLength(synthWishes[beatLength]);
        }
        
        public void ResetBeatLength() => _beatLength = null;

        // Audio Length
        
        /// <inheritdoc cref="docs._audiolength" />
        private FlowNode _audioLength;

        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_audioLength != null &&
                _audioLength.Calculate(time: 0) != 0)
            {
                return _audioLength;
            }
            
            return synthWishes[_section.AudioLength ?? DefaultAudioLength];
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public void WithAudioLength(FlowNode newAudioLength) => _audioLength = newAudioLength ?? throw new NullException(() => newAudioLength);
        
        /// <inheritdoc cref="docs._audiolength" />
        public void WithAudioLength(double newAudioLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithAudioLength(synthWishes[newAudioLength]);
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public void AddAudioLength(FlowNode additionalLength)
        {
            if (additionalLength == null) throw new ArgumentNullException(nameof(additionalLength));
            SynthWishes synthWishes = additionalLength.SynthWishes;
            WithAudioLength(GetAudioLength(synthWishes) + additionalLength);
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public void AddAudioLength(double additionalLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            AddAudioLength(synthWishes[additionalLength]);
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public void ResetAudioLength() => _audioLength = null;
        
        // LeadingSilence
        
        private FlowNode _leadingSilence;
        
        public FlowNode GetLeadingSilence(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_leadingSilence != null)
            {
                return _leadingSilence;
            }
            
            return synthWishes[_section.LeadingSilence ?? DefaultLeadingSilence];
        }

        public void WithLeadingSilence(FlowNode seconds) => _leadingSilence = seconds ?? throw new NullException(() => seconds);
        
        public void WithLeadingSilence(double seconds, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithLeadingSilence(synthWishes[seconds]);
        }
        
        public void ResetLeadingSilence() => _leadingSilence = null;

        // TrailingSilence
        
        private FlowNode _trailingSilence;
        
        public FlowNode GetTrailingSilence(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_trailingSilence != null)
            {
                return _trailingSilence;
            }
            
            return synthWishes[_section.TrailingSilence ?? DefaultTrailingSilence];
        }
        
        public void WithTrailingSilence(FlowNode seconds) => _trailingSilence = seconds ?? throw new NullException(() => seconds);
        
        public void WithTrailingSilence(double seconds, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithTrailingSilence(synthWishes[seconds]);
        }
        
        public void ResetTrailingSilence() => _trailingSilence = null;

        // Feature Toggles
        
        // Play
        
        /// <inheritdoc cref="docs._playback" />
        private bool? _play;
        /// <inheritdoc cref="docs._playback" />
        public void WithPlay(bool? enabled = true) => _play = enabled;
        /// <inheritdoc cref="docs._playback" />
        public bool GetPlay(string fileExtension = null)
        {
            bool play = _play ?? _section.Play ?? DefaultPlay;
            if (!play)
            {
                return false;
            }
            
            if (IsUnderNCrunch)
            {
                return _section.NCrunch.Play ?? DefaultToolingPlay;
            }
            
            if (IsUnderAzurePipelines)
            {
                return _section.AzurePipelines.Play ?? DefaultToolingPlay;
            }
            
            if (!string.IsNullOrWhiteSpace(fileExtension))
            {
                if (!string.Equals(fileExtension, ".wav", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        // MathBoost
        
        private bool? _mathBoost;
        public bool GetMathBoost => _mathBoost ?? _section.MathBoost ?? DefaultMathBoost;
        public void WithMathBoost(bool? enabled = true) => _mathBoost = enabled;
        
        // ParallelTaping
        
        /// <inheritdoc cref="docs._paralleltaping" />
        private bool? _parallelTaping;
        /// <inheritdoc cref="docs._paralleltaping" />
        public bool GetParallelTaping => _parallelTaping ?? _section.ParallelTaping ?? DefaultParallelTaping;
        /// <inheritdoc cref="docs._paralleltaping" />
        public void WithParallelTaping(bool? enabled = true) => _parallelTaping = enabled;
        
        // CacheToDisk
        
        /// <inheritdoc cref="docs._cachetodisk" />
        private bool? _cacheToDisk;
        /// <inheritdoc cref="docs._cachetodisk" />
        public bool GetCacheToDisk => _cacheToDisk ?? _section.CacheToDisk ?? DefaultCacheToDisk;
        /// <inheritdoc cref="docs._cachetodisk" />
        public void WithCacheToDisk(bool? enabled = true) =>  _cacheToDisk = enabled;
        
        // PlayAllTapes
        
        /// <inheritdoc cref="docs._playalltapes" />
        private bool? _playAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _playAllTapes ?? _section.PlayAllTapes ?? DefaultPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public void WithPlayAllTapes(bool? enabled = true) => _playAllTapes = enabled;
        
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        private double? _leafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => _leafCheckTimeOut ?? _section.LeafCheckTimeOut ?? DefaultLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public void WithLeafCheckTimeOut(double? seconds) => _leafCheckTimeOut = seconds;

        
        /// <inheritdoc cref="docs._timeoutaction" />
        private TimeOutActionEnum _timeOutAction;
        /// <inheritdoc cref="docs._timeoutaction" />
        // ReSharper disable once PossibleInvalidOperationException
        public TimeOutActionEnum GetTimeOutAction => FilledIn(_timeOutAction) ? _timeOutAction : _section.TimeOutAction ?? DefaultTimeOutAction;
        /// <inheritdoc cref="docs._timeoutaction" />
        public void WithTimeOutAction(TimeOutActionEnum action) => _timeOutAction = action;
               
        /// <inheritdoc cref="docs._extrabufferframes" />
        private int? _extraBufferFrames;
        /// <inheritdoc cref="docs._extrabufferframes" />
        public int GetExtraBufferFrames => _extraBufferFrames ?? _section.ExtraBufferFrames ?? DefaultExtraBufferFrames;
        /// <inheritdoc cref="docs._extrabufferframes" />
        public void WithExtraBufferFrames(int? value) => _extraBufferFrames = value;

        private string _longTestCategory;
        public void WithLongTestCategory(string category) => _longTestCategory = category;
        public string GetLongTestCategory
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_longTestCategory)) return _longTestCategory;
                if (!string.IsNullOrWhiteSpace(_section.LongTestCategory)) return _section.LongTestCategory;
                return DefaultLongTestCategory;
            }
        }

        // Tooling

        internal bool GetNCrunchImpersonation => _section.NCrunch.Impersonation ?? DefaultToolingImpersonation;
        internal bool GetAzurePipelinesImpersonation => _section.AzurePipelines.Impersonation ?? DefaultToolingImpersonation;
        
        public bool IsUnderNCrunch
        {
            get
            {
                if (GetNCrunchImpersonation)
                {
                    return true;
                }
                
                bool isUnderNCrunch = EnvironmentVariableIsDefined(NCrunchEnvironmentVariableName, NCrunchEnvironmentVariableValue);
                return isUnderNCrunch;
            }
        }
        
        public bool IsUnderAzurePipelines
        {
            get
            {
                if (GetAzurePipelinesImpersonation)
                {
                    return true;
                }
                bool isUnderAzurePipelines = EnvironmentVariableIsDefined(AzurePipelinesEnvironmentVariableName, AzurePipelinesEnvironmentVariableValue);
                return isUnderAzurePipelines;
            }
        }
        
        // Persistence
        
        public static PersistenceConfiguration PersistenceConfigurationOrDefault { get; } 
            = TryGetSection<PersistenceConfiguration>() ?? GetDefaultInMemoryConfiguration();
        
        private static PersistenceConfiguration GetDefaultInMemoryConfiguration()
            => new PersistenceConfiguration
            {
                ContextType = "Memory",
                ModelAssembly = NameHelper.GetAssemblyName<Persistence.Synthesizer.Operator>(),
                MappingAssembly = NameHelper.GetAssemblyName<Persistence.Synthesizer.Memory.Mappings.OperatorMapping>(),
                RepositoryAssemblies = new[]
                {
                    NameHelper.GetAssemblyName<Persistence.Synthesizer.Memory.Repositories.NodeTypeRepository>(),
                    NameHelper.GetAssemblyName<Persistence.Synthesizer.DefaultRepositories.OperatorRepository>()
                }
            };
        
        // Warnings
        
        public IList<string> GetWarnings(string fileExtension = null)
        {
            var list = new List<string>();
            
            // Sampling Rate Override
            
            if (_samplingRate != 0)
            {
                list.Add($"Sampling rate override = {_samplingRate}");
            }
            
            // Running Under Tooling
            
            if (GetNCrunchImpersonation)
            {
                list.Add("Pretending to be NCrunch.");
            }
            
            if (IsUnderNCrunch)
            {
                list.Add($"Environment variable {NCrunchEnvironmentVariableName} = {NCrunchEnvironmentVariableValue}");
            }
            
            if (GetAzurePipelinesImpersonation)
            {
                list.Add("Pretending to be Azure Pipelines.");
            }
            
            if (IsUnderAzurePipelines)
            {
                list.Add($"Environment variable {AzurePipelinesEnvironmentVariableName} = {AzurePipelinesEnvironmentVariableValue} (Azure Pipelines)");
            }
            
            // Long Running
            
            bool isLong = CurrentTestIsInCategory(GetLongTestCategory);
            if (isLong)
            {
                list.Add($"Test has category '{GetLongTestCategory}'");
            }
            
            // Audio Disabled
            
            if (!GetPlay(fileExtension))
            {
                list.Add("Audio disabled");
            }
            
            return list;
        }
    }

    // SynthWishes ConfigWishes
    
    public partial class SynthWishes
    {
        // Audio Quality
        
        public int GetBits => Config.GetBits;
        public SynthWishes WithBits(int bits) { Config.WithBits(bits); return this; }
        public bool Is32Bit => Config.Is32Bit;
        public SynthWishes With32Bit() { Config.With32Bit(); return this; }
        public bool Is16Bit => Config.Is16Bit;
        public SynthWishes With16Bit() { Config.With16Bit(); return this; }
        public bool Is8Bit => Config.Is8Bit;
        public SynthWishes With8Bit() { Config.With8Bit(); return this; }
        
        public int GetChannels => Config.GetChannels;
        public SynthWishes WithChannels(int channels) { Config.WithChannels(channels); return this; }
        public bool IsMono => Config.IsMono;
        public SynthWishes WithMono() { Config.WithMono(); return this; }
        public bool IsStereo => Config.IsStereo;
        public SynthWishes WithStereo() { Config.WithStereo(); return this; }
        
        public int? GetChannel => Config.GetChannel;
        public SynthWishes WithChannel(int? channel) { Config.WithChannel(channel); return this; }
        public bool IsLeft => Config.IsLeft;
        public SynthWishes WithLeft() { Config.WithLeft(); return this; }
        public bool IsRight => Config.IsRight;
        public SynthWishes WithRight()  { Config.WithRight(); return this; }
        public bool IsCenter => Config.IsCenter;
        public SynthWishes WithCenter()  { Config.WithCenter(); return this; }
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate => Config.GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes WithSamplingRate(int value) { Config.WithSamplingRate(value); return this; }
        
        public AudioFileFormatEnum GetAudioFormat => Config.GetAudioFormat;
        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat) { Config.WithAudioFormat(audioFormat); return this; }
        public bool IsWav => Config.IsWav;
        public SynthWishes AsWav() { Config.AsWav(); return this; }
        public bool IsRaw => Config.IsRaw;
        public SynthWishes AsRaw() { Config.AsRaw(); return this; }
        
        public InterpolationTypeEnum GetInterpolation => Config.GetInterpolation;
        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolation) { Config.WithInterpolation(interpolation); return this; }
        public bool IsLinear => Config.IsLinear;
        public SynthWishes WithLinear() {Config.WithLinear(); return this; }
        public bool IsBlocky => Config.IsBlocky;
        public SynthWishes WithBlocky() { Config.WithBlocky(); return this; }

        // Durations
        
        public FlowNode GetNoteLength  => Config.GetNoteLength(this);
        public FlowNode SnapNoteLength(FlowNode noteLength)  => Config.SnapNoteLength(this, noteLength);
        public SynthWishes WithNoteLength(FlowNode seconds) { Config.WithNoteLength(seconds); return this; }
        public SynthWishes WithNoteLength(double seconds) { Config.WithNoteLength(seconds, this); return this; }
        public SynthWishes ResetNoteLength() { Config.ResetNoteLength(); return this; }
        
        public FlowNode GetBarLength => Config.GetBarLength(this);
        public SynthWishes WithBarLength(FlowNode seconds) { Config.WithBarLength(seconds); return this; }
        public SynthWishes WithBarLength(double seconds) { Config.WithBarLength(seconds, this); return this; }
        public SynthWishes ResetBarLength() { Config.ResetBarLength(); return this; }
        
        public FlowNode GetBeatLength => Config.GetBeatLength(this);
        public SynthWishes WithBeatLength(FlowNode seconds) { Config.WithBeatLength(seconds); return this; }
        public SynthWishes WithBeatLength(double seconds) { Config.WithBeatLength(seconds, this); return this; }
        public SynthWishes ResetBeatLength() { Config.ResetBeatLength(); return this; }

        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength => Config.GetAudioLength(this);
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength(double newLength) { Config.WithAudioLength(newLength, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength(FlowNode newLength) { Config.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddAudioLength(double additionalLength) { Config.AddAudioLength(additionalLength, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddAudioLength(FlowNode additionalLength) { Config.AddAudioLength(additionalLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes ResetAudioLength() { Config.ResetAudioLength(); return this; }
        
        public SynthWishes AddEchoDuration(int count = 4, FlowNode delay = default) => AddAudioLength(EchoDuration(count, delay));
        public SynthWishes AddEchoDuration(int count, double delay) => AddAudioLength(EchoDuration(count, delay));

        public FlowNode GetLeadingSilence => Config.GetLeadingSilence(this);
        public SynthWishes WithLeadingSilence(double seconds) { Config.WithLeadingSilence(seconds, this); return this; }
        public SynthWishes WithLeadingSilence(FlowNode seconds) { Config.WithLeadingSilence(seconds); return this; }
        public SynthWishes ResetLeadingSilence() { Config.ResetLeadingSilence(); return this; }
        
        public FlowNode GetTrailingSilence => Config.GetTrailingSilence(this);
        public SynthWishes WithTrailingSilence(double seconds) { Config.WithTrailingSilence(seconds, this); return this; }
        public SynthWishes WithTrailingSilence(FlowNode seconds) { Config.WithTrailingSilence(seconds); return this; }
        public SynthWishes ResetTrailingSilence() { Config.ResetTrailingSilence(); return this; }

        // Feature Toggles
        
        /// <inheritdoc cref="docs._playback" />
        public bool GetPlay(string fileExtension = null) => Config.GetPlay(fileExtension);
        /// <inheritdoc cref="docs._playback" />
        public SynthWishes WithPlay(bool? enabled = true) { Config.WithPlay(enabled); return this; }
        
        public bool GetMathBoost => Config.GetMathBoost;
        public SynthWishes WithMathBoost(bool? enabled = true) { Config.WithMathBoost(enabled); return this; }
        
        /// <inheritdoc cref="docs._paralleltaping" />
        public bool GetParallelTaping => Config.GetParallelTaping;
        /// <inheritdoc cref="docs._paralleltaping" />
        public SynthWishes WithParallelTaping(bool? enabled = true) { Config.WithParallelTaping(enabled); return this; }

        /// <inheritdoc cref="docs._cachetodisk" />
        public bool GetCacheToDisk => Config.GetCacheToDisk;
        /// <inheritdoc cref="docs._cachetodisk" />
        public SynthWishes WithCacheToDisk(bool? enabled = true) { Config.WithCacheToDisk(enabled); return this; }

        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => Config.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes WithPlayAllTapes(bool? enabled = true) { Config.WithPlayAllTapes(enabled); return this; }

        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => Config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes WithLeafCheckTimeOut(double? seconds) { Config.WithLeafCheckTimeOut(seconds); return this; }

        /// <inheritdoc cref="docs._leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction => Config.GetTimeOutAction;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes WithTimeOutAction(TimeOutActionEnum action) { Config.WithTimeOutAction(action); return this; }
        
        /// <inheritdoc cref="docs._extrabufferframes" />
        public int GetExtraBufferFrames => Config.GetExtraBufferFrames;
        /// <inheritdoc cref="docs._extrabufferframes" />
        public SynthWishes WithExtraBufferFrames(int? value) { Config.WithExtraBufferFrames(value); return this; }
    }
    
    // FlowNode ConfigWishes

    public partial class FlowNode
    {
        // Audio Quality
        
        public int GetBits => _synthWishes.GetBits;
        public FlowNode WithBits(int bits) { _synthWishes.WithBits(bits); return this; }
        public bool Is32Bit => _synthWishes.Is32Bit;
        public FlowNode With32Bit() { _synthWishes.With32Bit(); return this; }
        public bool Is16Bit => _synthWishes.Is16Bit;
        public FlowNode With16Bit() { _synthWishes.With16Bit(); return this; }
        public bool Is8Bit => _synthWishes.Is8Bit;
        public FlowNode With8Bit() { _synthWishes.With8Bit(); return this; }
        
        public int GetChannels => _synthWishes.GetChannels;
        public FlowNode WithChannels(int channels) { _synthWishes.WithChannels(channels); return this; }
        public bool IsMono => _synthWishes.IsMono;
        public FlowNode WithMono() { _synthWishes.WithMono(); return this; }
        public bool IsStereo => _synthWishes.IsStereo;
        public FlowNode WithStereo() { _synthWishes.WithStereo(); return this; }

        public int? GetChannel => _synthWishes.GetChannel;
        public FlowNode WithChannel(int? channel) { _synthWishes.WithChannel(channel); return this; }
        public bool IsLeft => _synthWishes.IsLeft;
        public FlowNode WithLeft()  { _synthWishes.WithLeft(); return this; }
        public bool IsRight => _synthWishes.IsRight;
        public FlowNode WithRight() { _synthWishes.WithRight(); return this; }
        public bool IsCenter => _synthWishes.IsCenter;
        public FlowNode WithCenter() { _synthWishes.WithCenter(); return this; }
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate => _synthWishes.GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public FlowNode WithSamplingRate(int value) { _synthWishes.WithSamplingRate(value); return this; }

        public AudioFileFormatEnum GetAudioFormat => _synthWishes.GetAudioFormat;
        public FlowNode WithAudioFormat(AudioFileFormatEnum audioFormat) { _synthWishes.WithAudioFormat(audioFormat); return this; }
        public bool IsWav => _synthWishes.IsWav;
        public FlowNode AsWav() { _synthWishes.AsWav(); return this; }
        public bool IsRaw => _synthWishes.IsRaw;
        public FlowNode AsRaw() { _synthWishes.AsRaw(); return this; }

        public InterpolationTypeEnum GetInterpolation => _synthWishes.GetInterpolation;
        public FlowNode WithInterpolation(InterpolationTypeEnum interpolation) { _synthWishes.WithInterpolation(interpolation); return this; }
        public bool IsLinear => _synthWishes.IsLinear;
        public FlowNode WithLinear() { _synthWishes.WithLinear(); return this; }
        public bool IsBlocky => _synthWishes.IsBlocky;
        public FlowNode WithBlocky() { _synthWishes.WithBlocky(); return this; }
        
        // Durations
        
        public FlowNode GetNoteLength => _synthWishes.GetNoteLength;
        public FlowNode SnapNoteLength(FlowNode noteLength) => _synthWishes.SnapNoteLength(noteLength);
        public FlowNode WithNoteLength(FlowNode newLength) { _synthWishes.WithNoteLength(newLength); return this; }
        public FlowNode WithNoteLength(double newLength) { _synthWishes.WithNoteLength(newLength); return this; }
        public FlowNode ResetNoteLength() { _synthWishes.ResetNoteLength(); return this; }
        
        public FlowNode GetBarLength => _synthWishes.GetBarLength;
        public FlowNode WithBarLength(FlowNode newLength) { _synthWishes.WithBarLength(newLength); return this; }
        public FlowNode WithBarLength(double newLength) { _synthWishes.WithBarLength(newLength); return this; }
        public FlowNode ResetBarLength() { _synthWishes.ResetBarLength(); return this; }

        public FlowNode GetBeatLength => _synthWishes.GetBeatLength;
        public FlowNode WithBeatLength(FlowNode newLength) { _synthWishes.WithBeatLength(newLength); return this; }
        public FlowNode WithBeatLength(double newLength) { _synthWishes.WithBeatLength(newLength); return this; }
        public FlowNode ResetBeatLength() { _synthWishes.ResetBeatLength(); return this; }

        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength => _synthWishes.GetAudioLength;
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode WithAudioLength(FlowNode newLength) { _synthWishes.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode WithAudioLength(double newLength) { _synthWishes.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode AddAudioLength(FlowNode additionalLength) { _synthWishes.AddAudioLength(additionalLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode AddAudioLength(double additionalLength) { _synthWishes.AddAudioLength(additionalLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode ResetAudioLength() { _synthWishes.ResetAudioLength(); return this; }
                
        public FlowNode AddEchoDuration(int count = 4, FlowNode delay = default) { _synthWishes.AddEchoDuration(count, delay); return this; }
        public FlowNode AddEchoDuration(int count, double delay) { _synthWishes.AddEchoDuration(count, delay); return this; }

        public FlowNode GetLeadingSilence => _synthWishes.GetLeadingSilence;
        public FlowNode WithLeadingSilence(double seconds) { _synthWishes.WithLeadingSilence(seconds); return this; }
        public FlowNode WithLeadingSilence(FlowNode seconds) { _synthWishes.WithLeadingSilence(seconds); return this; }
        public FlowNode ResetLeadingSilence() { _synthWishes.ResetLeadingSilence(); return this; }
        
        public FlowNode GetTrailingSilence => _synthWishes.GetTrailingSilence;
        public FlowNode WithTrailingSilence(double seconds) { _synthWishes.WithTrailingSilence(seconds); return this; }
        public FlowNode WithTrailingSilence(FlowNode seconds) { _synthWishes.WithTrailingSilence(seconds); return this; }
        public FlowNode ResetTrailingSilence() { _synthWishes.ResetTrailingSilence(); return this; }
        
        // Feature Toggles
        
        /// <inheritdoc cref="docs._playback" />
        public bool GetPlay(string fileExtension = null) => _synthWishes.GetPlay(fileExtension);
        /// <inheritdoc cref="docs._playback" />
        public FlowNode WithPlay(bool? enabled = true) { _synthWishes.WithPlay(enabled); return this; }
        
        public bool GetMathBoost => _synthWishes.GetMathBoost;
        public FlowNode WithMathBoost(bool? enabled = true) { _synthWishes.WithMathBoost(enabled); return this; }
        
        /// <inheritdoc cref="docs._paralleltaping" />
        public bool GetParallelTaping => _synthWishes.GetParallelTaping;
        /// <inheritdoc cref="docs._paralleltaping" />
        public FlowNode WithParallelTaping(bool? enabled = true) { _synthWishes.WithParallelTaping(enabled); return this; }

        /// <inheritdoc cref="docs._cachetodisk" />
        public bool GetCacheToDisk => _synthWishes.GetCacheToDisk;
        /// <inheritdoc cref="docs._cachetodisk" />
        public FlowNode WithCacheToDisk(bool? enabled = true) { _synthWishes.WithCacheToDisk(enabled); return this; }

        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _synthWishes.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public FlowNode WithPlayAllTapes(bool? enabled = true) { _synthWishes.WithPlayAllTapes(enabled); return this; }
 
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => _synthWishes.GetLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public FlowNode WithLeafCheckTimeOut(double? seconds) { _synthWishes.WithLeafCheckTimeOut(seconds); return this; }
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public TimeOutActionEnum TimeOutAction => _synthWishes.GetTimeOutAction;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public FlowNode WithTimeOutAction(TimeOutActionEnum action) { _synthWishes.WithTimeOutAction(action); return this; }
        
        /// <inheritdoc cref="docs._extrabufferframes" />
        public int GetExtraBufferFrames => _synthWishes.GetExtraBufferFrames;
        /// <inheritdoc cref="docs._extrabufferframes" />
        public FlowNode WithExtraBufferFrames(int? value) { _synthWishes.WithExtraBufferFrames(value); return this; }
    }
}