using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Configuration_Wishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Testing_Wishes;
// ReSharper disable RedundantNameQualifier

namespace JJ.Business.Synthesizer.Wishes
{
    // Config XML
    
    internal class ConfigSection
    {
        // Audio Quality
        
        [XmlAttribute] public int? Bits { get; set; }
        [XmlAttribute] public SpeakerSetupEnum? Speakers { get; set; }
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
        
        [XmlAttribute] public bool? PlayBack { get; set; }
        [XmlAttribute] public bool? MathBoost { get; set; }
        [XmlAttribute] public bool? ParallelTaping { get; set; }
        [XmlAttribute] public bool? CacheToDisk { get; set; }
        [XmlAttribute] public bool? PlayAllTapes { get; set; }
        
        // Tooling
        
        public ConfigToolingElement AzurePipelines { get; set; } = new ConfigToolingElement();
        public ConfigToolingElement NCrunch { get; set; } = new ConfigToolingElement();
        
        // Misc Settings
        
        [XmlAttribute] public int? ExtraBufferFrames { get; set; }
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        [XmlAttribute] public double? ParallelTaskCheckDelay { get; set; }
        [XmlAttribute] public string LongTestCategory { get; set; }
    }

    internal class ConfigToolingElement
    {
        [XmlAttribute] public int? SamplingRate { get; set; }
        [XmlAttribute] public int? SamplingRateLongRunning { get; set; }
        [XmlAttribute] public bool? PlayBack { get; set; }
        [XmlAttribute] public bool? Impersonation { get; set; }
    }
    
    // ConfigResolver
    
    internal class ConfigResolver
    {
        /// <summary> For static contexts use this. </summary>
        public static ConfigResolver Default { get; } = new ConfigResolver();

        // Defaults

        // Audio Quality

        private const int                   DefaultBits          = 32;
        private const SpeakerSetupEnum      DefaultSpeakers      = Mono;
        private const ChannelEnum           DefaultChannel       = ChannelEnum.Single;
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
        
        private const bool   DefaultPlayBack          = true;
        private const bool   DefaultMathBoost         = true;
        private const bool   DefaultParallelTaping    = true;
        private const bool   DefaultCacheToDisk       = false;
        private const bool   DefaultPlayAllTapes      = false;

        // Tooling
        
        private const bool   DefaultToolingPlayBack                       = false;
        private const bool   DefaultToolingImpersonation                  = false;
        private const int    DefaultNCrunchSamplingRate                   = 150;
        private const int    DefaultNCrunchSamplingRateLongRunning        = 8;
        private const int    DefaultAzurePipelinesSamplingRate            = 1500;
        private const int    DefaultAzurePipelinesSamplingRateLongRunning = 100;
        
        // Misc Settings
        
        private const int    DefaultExtraBufferFrames      = 4;
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        private const double DefaultParallelTaskCheckDelay = 0.001;
        private const string DefaultLongTestCategory       = "Long";

        // Environment Variables
        
        private const string NCrunchEnvironmentVariableName = "NCrunch";
        private const string AzurePipelinesEnvironmentVariableValue = "True";
        private const string AzurePipelinesEnvironmentVariableName = "TF_BUILD";
        private const string NCrunchEnvironmentVariableValue = "1";

        private static readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        // Audio Quality
        
        // Bits
        
        private SampleDataTypeEnum _sampleDataTypeEnum;
        public int GetBits => _sampleDataTypeEnum != default ? _sampleDataTypeEnum.GetBits() : _section.Bits ?? DefaultBits;
        public void WithBits(int bits) => _sampleDataTypeEnum = bits.ToSampleDataTypeEnum();
        public void With32Bit() => WithBits(32);
        public void With16Bit() => WithBits(16);
        public void With8Bit() => WithBits(8);
        
        // Speakers
        
        private SpeakerSetupEnum _speakers;
        public SpeakerSetupEnum GetSpeakers => _speakers != default ? _speakers : _section.Speakers ?? DefaultSpeakers;
        public void WithSpeakers(SpeakerSetupEnum speakers) => _speakers = speakers;
        public void WithMono() => WithSpeakers(Mono);
        public void WithStereo() => WithSpeakers(Stereo);
        
        // Channel
        
        private ChannelEnum _channel = DefaultChannel;
        public ChannelEnum GetChannel => _channel;
        public void WithChannel(ChannelEnum channel) => _channel = channel;
        public int GetChannelIndex => _channel.ToIndex();
        public void WithChannelIndex(int value) => _channel = value.ToChannel(GetSpeakers);
        public void WithLeft() => WithChannel(ChannelEnum.Left);
        public void WithRight() => WithChannel(ChannelEnum.Right);
        public void WithCenter() => WithChannel(ChannelEnum.Single);

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
        public void AsWav() => WithAudioFormat(Wav);
        public void AsRaw() => WithAudioFormat(Raw);
        
        // Interpolation
        
        private InterpolationTypeEnum _interpolation;
        public InterpolationTypeEnum GetInterpolation => _interpolation != default ? _interpolation : _section.Interpolation ?? DefaultInterpolation;
        public void WithInterpolation(InterpolationTypeEnum interpolation) => _interpolation = interpolation;
        public void WithLinear() => WithInterpolation(Line);
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
            
            return synthWishes._[_section.NoteLength ?? DefaultNoteLength];
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
            WithNoteLength(synthWishes._[noteLength]);
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

            return synthWishes._[_section.BarLength ?? DefaultBarLength];
        }
        
        public void WithBarLength(FlowNode barLength) => _barLength = barLength ?? throw new NullException(() => barLength);
        
        public void WithBarLength(double barLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithBarLength(synthWishes._[barLength]);
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
            
            return synthWishes._[_section.BeatLength ?? DefaultBeatLength];
        }
        
        public void WithBeatLength(FlowNode beatLength) => _beatLength = beatLength ?? throw new NullException(() => beatLength);
        
        public void WithBeatLength(double beatLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithBeatLength(synthWishes._[beatLength]);
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
            
            return synthWishes._[_section.AudioLength ?? DefaultAudioLength];
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public void WithAudioLength(FlowNode newAudioLength) => _audioLength = newAudioLength ?? throw new NullException(() => newAudioLength);
        
        /// <inheritdoc cref="docs._audiolength" />
        public void WithAudioLength(double newAudioLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithAudioLength(synthWishes._[newAudioLength]);
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
            AddAudioLength(synthWishes._[additionalLength]);
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public void ResetAudioLength() => _audioLength = null;
        
        // LeadingSilence
        
        private FlowNode _leadingSilence;
        
        public FlowNode GetLeadingSilence(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_leadingSilence != null &&
                _leadingSilence.Calculate(time: 0) != 0)
            {
                return _leadingSilence;
            }
            
            return synthWishes._[_section.LeadingSilence ?? DefaultLeadingSilence];
        }

        public void WithLeadingSilence(FlowNode seconds) => _leadingSilence = seconds ?? throw new NullException(() => seconds);
        
        public void WithLeadingSilence(double seconds, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithLeadingSilence(synthWishes._[seconds]);
        }
        
        public void ResetLeadingSilence() => _leadingSilence = null;

        // TrailingSilence
        
        private FlowNode _trailingSilence;
        
        public FlowNode GetTrailingSilence(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_trailingSilence != null &&
                _trailingSilence.Calculate(time: 0) != 0)
            {
                return _trailingSilence;
            }
            
            return synthWishes._[_section.TrailingSilence ?? DefaultTrailingSilence];
        }
        
        public void WithTrailingSilence(FlowNode seconds) => _trailingSilence = seconds ?? throw new NullException(() => seconds);
        
        public void WithTrailingSilence(double seconds, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithTrailingSilence(synthWishes._[seconds]);
        }
        
        public void ResetTrailingSilence() => _trailingSilence = null;

        // Feature Toggles
        
        // PlayBack
        
        /// <inheritdoc cref="docs._playback" />
        private bool? _playBack;
        /// <inheritdoc cref="docs._playback" />
        public void WithPlayBack(bool? enabled = true) => _playBack = enabled;
        /// <inheritdoc cref="docs._playback" />
        public bool GetPlayBack(string fileExtension = null)
        {
            bool playBack = _playBack ?? _section.PlayBack ?? DefaultPlayBack;
            if (!playBack)
            {
                return false;
            }
            
            if (IsUnderNCrunch)
            {
                return _section.NCrunch.PlayBack ?? DefaultToolingPlayBack;
            }
            
            if (IsUnderAzurePipelines)
            {
                return _section.AzurePipelines.PlayBack ?? DefaultToolingPlayBack;
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
        
        /// <inheritdoc cref="docs._extrabufferframes" />
        private int? _extraBufferFrames;
        /// <inheritdoc cref="docs._extrabufferframes" />
        public int GetExtraBufferFrames => _extraBufferFrames ?? _section.ExtraBufferFrames ?? DefaultExtraBufferFrames;
        /// <inheritdoc cref="docs._extrabufferframes" />
        public void WithExtraBufferFrames(int? value) => _extraBufferFrames = value;
        
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        private double? _parallelTaskCheckDelay;
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        public double GetParallelTaskCheckDelay => _parallelTaskCheckDelay ?? _section.ParallelTaskCheckDelay ?? DefaultParallelTaskCheckDelay;
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        public void WithParallelTaskCheckDelay(double? milliseconds) => _parallelTaskCheckDelay = milliseconds;
        
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
            
            if (!GetPlayBack(fileExtension))
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
        
        public int GetBits => _configResolver.GetBits;
        public SynthWishes WithBits(int bits) { _configResolver.WithBits(bits); return this; }
        public SynthWishes With32Bit() { _configResolver.With32Bit(); return this; }
        public SynthWishes With16Bit() { _configResolver.With16Bit(); return this; }
        public SynthWishes With8Bit() { _configResolver.With8Bit(); return this; }
        
        public SpeakerSetupEnum GetSpeakers => _configResolver.GetSpeakers;
        public SynthWishes WithSpeakers(SpeakerSetupEnum speakers) { _configResolver.WithSpeakers(speakers); return this; }
        public SynthWishes WithMono() { _configResolver.WithMono(); return this; }
        public SynthWishes WithStereo() { _configResolver.WithStereo(); return this; }
        
        public ChannelEnum GetChannel => _configResolver.GetChannel;
        public int GetChannelIndex => _configResolver.GetChannelIndex;
        public SynthWishes WithChannel(ChannelEnum channel) { _configResolver.WithChannel(channel); return this; }
        public SynthWishes WithChannelIndex(int value) { _configResolver.WithChannelIndex(value); return this; }
        public SynthWishes WithLeft() { _configResolver.WithLeft(); return this; }
        public SynthWishes WithRight()  { _configResolver.WithRight(); return this; }
        public SynthWishes WithCenter()  { _configResolver.WithCenter(); return this; }
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate => _configResolver.GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes WithSamplingRate(int value) { _configResolver.WithSamplingRate(value); return this; }
        
        public AudioFileFormatEnum GetAudioFormat => _configResolver.GetAudioFormat;
        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat) { _configResolver.WithAudioFormat(audioFormat); return this; }
        public SynthWishes AsWav() { _configResolver.AsWav(); return this; }
        public SynthWishes AsRaw() { _configResolver.AsRaw(); return this; }
        
        public InterpolationTypeEnum GetInterpolation => _configResolver.GetInterpolation;
        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolation) { _configResolver.WithInterpolation(interpolation); return this; }
        public SynthWishes WithLinear() {_configResolver.WithLinear(); return this; }
        public SynthWishes WithBlocky() { _configResolver.WithBlocky(); return this; }

        // Durations
        
        public FlowNode GetNoteLength  => _configResolver.GetNoteLength(this);
        public FlowNode SnapNoteLength(FlowNode noteLength)  => _configResolver.SnapNoteLength(this, noteLength);
        public SynthWishes WithNoteLength(FlowNode seconds) { _configResolver.WithNoteLength(seconds); return this; }
        public SynthWishes WithNoteLength(double seconds) { _configResolver.WithNoteLength(seconds, this); return this; }
        public SynthWishes ResetNoteLength() { _configResolver.ResetNoteLength(); return this; }
        
        public FlowNode GetBarLength => _configResolver.GetBarLength(this);
        public SynthWishes WithBarLength(FlowNode seconds) { _configResolver.WithBarLength(seconds); return this; }
        public SynthWishes WithBarLength(double seconds) { _configResolver.WithBarLength(seconds, this); return this; }
        public SynthWishes ResetBarLength() { _configResolver.ResetBarLength(); return this; }
        
        public FlowNode GetBeatLength => _configResolver.GetBeatLength(this);
        public SynthWishes WithBeatLength(FlowNode seconds) { _configResolver.WithBeatLength(seconds); return this; }
        public SynthWishes WithBeatLength(double seconds) { _configResolver.WithBeatLength(seconds, this); return this; }
        public SynthWishes ResetBeatLength() { _configResolver.ResetBeatLength(); return this; }

        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength => _configResolver.GetAudioLength(this);
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength(double newLength) { _configResolver.WithAudioLength(newLength, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength(FlowNode newLength) { _configResolver.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddAudioLength(double additionalLength) { _configResolver.AddAudioLength(additionalLength, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddAudioLength(FlowNode additionalLength) { _configResolver.AddAudioLength(additionalLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes ResetAudioLength() { _configResolver.ResetAudioLength(); return this; }
        
        public FlowNode GetLeadingSilence => _configResolver.GetLeadingSilence(this);
        public SynthWishes WithLeadingSilence(double seconds) { _configResolver.WithLeadingSilence(seconds, this); return this; }
        public SynthWishes WithLeadingSilence(FlowNode seconds) { _configResolver.WithLeadingSilence(seconds); return this; }
        public SynthWishes ResetLeadingSilence() { _configResolver.ResetLeadingSilence(); return this; }
        
        public FlowNode GetTrailingSilence => _configResolver.GetTrailingSilence(this);
        public SynthWishes WithTrailingSilence(double seconds) { _configResolver.WithTrailingSilence(seconds, this); return this; }
        public SynthWishes WithTrailingSilence(FlowNode seconds) { _configResolver.WithTrailingSilence(seconds); return this; }
        public SynthWishes ResetTrailingSilence() { _configResolver.ResetTrailingSilence(); return this; }

        // Feature Toggles
        
        /// <inheritdoc cref="docs._playback" />
        public bool GetPlayBack(string fileExtension = null) => _configResolver.GetPlayBack(fileExtension);
        /// <inheritdoc cref="docs._playback" />
        public SynthWishes WithPlayBack(bool? enabled = true) { _configResolver.WithPlayBack(enabled); return this; }
        
        public bool GetMathBoost => _configResolver.GetMathBoost;
        public SynthWishes WithMathBoost(bool? enabled = true) { _configResolver.WithMathBoost(enabled); return this; }
        
        /// <inheritdoc cref="docs._paralleltaping" />
        public bool GetParallelTaping => _configResolver.GetParallelTaping;
        /// <inheritdoc cref="docs._paralleltaping" />
        public SynthWishes WithParallelTaping(bool? enabled = true) { _configResolver.WithParallelTaping(enabled); return this; }

        /// <inheritdoc cref="docs._cachetodisk" />
        public bool GetCacheToDisk => _configResolver.GetCacheToDisk;
        /// <inheritdoc cref="docs._cachetodisk" />
        public SynthWishes WithCacheToDisk(bool? enabled = true) { _configResolver.WithCacheToDisk(enabled); return this; }

        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _configResolver.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes WithPlayAllTapes(bool? enabled = true) { _configResolver.WithPlayAllTapes(enabled); return this; }

        // Misc Settings
        
        /// <inheritdoc cref="docs._extrabufferframes" />
        public int GetExtraBufferFrames => _configResolver.GetExtraBufferFrames;
        /// <inheritdoc cref="docs._extrabufferframes" />
        public SynthWishes WithExtraBufferFrames(int? value) {_configResolver.WithExtraBufferFrames(value); return this; }
        
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        public double GetParallelTaskCheckDelay => _configResolver.GetParallelTaskCheckDelay;
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        public SynthWishes WithParallelTaskCheckDelay(double? seconds) {_configResolver.WithParallelTaskCheckDelay(seconds); return this; }
    }
    
    // FlowNode ConfigWishes

    public partial class FlowNode
    {
        // Audio Quality
        
        public int GetBits => _synthWishes.GetBits;
        public FlowNode WithBits(int bits) { _synthWishes.WithBits(bits); return this; }
        public FlowNode With32Bit() { _synthWishes.With32Bit(); return this; }
        public FlowNode With16Bit() { _synthWishes.With16Bit(); return this; }
        public FlowNode With8Bit() { _synthWishes.With8Bit(); return this; }
        
        public SpeakerSetupEnum GetSpeakers => _synthWishes.GetSpeakers;
        public FlowNode WithSpeakers(SpeakerSetupEnum speakers) { _synthWishes.WithSpeakers(speakers); return this; }
        public FlowNode WithMono() { _synthWishes.WithMono(); return this; }
        public FlowNode WithStereo() { _synthWishes.WithStereo(); return this; }

        public ChannelEnum GetChannel => _synthWishes.GetChannel;
        public int GetChannelIndex => _synthWishes.GetChannelIndex;
        public FlowNode WithChannel(ChannelEnum channel) { _synthWishes.WithChannel(channel); return this; }
        public FlowNode WithChannelIndex(int value) { _synthWishes.WithChannelIndex(value); return this;}
        public FlowNode WithLeft()  { _synthWishes.WithLeft(); return this; }
        public FlowNode WithRight() { _synthWishes.WithRight(); return this; }
        public FlowNode WithCenter() { _synthWishes.WithCenter(); return this; }
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate => _synthWishes.GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public FlowNode WithSamplingRate(int value) { _synthWishes.WithSamplingRate(value); return this; }

        public AudioFileFormatEnum GetAudioFormat => _synthWishes.GetAudioFormat;
        public FlowNode WithAudioFormat(AudioFileFormatEnum audioFormat) { _synthWishes.WithAudioFormat(audioFormat); return this; }
        public FlowNode AsWav() { _synthWishes.AsWav(); return this; }
        public FlowNode AsRaw() { _synthWishes.AsRaw(); return this; }

        public InterpolationTypeEnum GetInterpolation => _synthWishes.GetInterpolation;
        public FlowNode WithInterpolation(InterpolationTypeEnum interpolation) { _synthWishes.WithInterpolation(interpolation); return this; }
        public FlowNode WithLinear() { _synthWishes.WithLinear(); return this; }
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
        public bool GetPlayBack(string fileExtension = null) => _synthWishes.GetPlayBack(fileExtension);
        /// <inheritdoc cref="docs._playback" />
        public FlowNode WithPlayBack(bool? enabled = true) { _synthWishes.WithPlayBack(enabled); return this; }
        
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
        
        /// <inheritdoc cref="docs._extrabufferframes" />
        public int GetExtraBufferFrames => _synthWishes.GetExtraBufferFrames;
        /// <inheritdoc cref="docs._extrabufferframes" />
        public FlowNode WithExtraBufferFrames(int? value) { _synthWishes.WithExtraBufferFrames(value); return this; }
        
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        public double GetParallelTaskCheckDelay => _synthWishes.GetParallelTaskCheckDelay;
        /// <inheritdoc cref="docs._paralleltaskcheckdelay" />
        public FlowNode WithParallelTaskCheckDelay(double? seconds) { _synthWishes.WithParallelTaskCheckDelay(seconds); return this; }
}
}