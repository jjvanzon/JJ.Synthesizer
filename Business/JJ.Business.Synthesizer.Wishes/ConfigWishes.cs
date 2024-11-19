using System;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Persistence;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.ConfigResolver;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkCommonWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkConfigurationWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ToolingHelper;
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
        
        [XmlAttribute] public double? AudioLength { get; set; }
        [XmlAttribute] public double? LeadingSilence { get; set; }
        [XmlAttribute] public double? TrailingSilence { get; set; }

        // Feature Toggles
        
        [XmlAttribute] public bool? PlayBack { get; set; }
        [XmlAttribute] public bool? MathBoost { get; set; }
        [XmlAttribute] public bool? Parallels { get; set; }
        [XmlAttribute] public bool? DiskCacheOn { get; set; }
        [XmlAttribute] public bool? PlayAllTapes { get; set; }
        
        // Tooling
        
        [XmlAttribute] public string LongTestCategory { get; set; }
        public ConfigToolingElement AzurePipelines { get; set; } = new ConfigToolingElement();
        public ConfigToolingElement NCrunch { get; set; } = new ConfigToolingElement();
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
        // Defaults
        
        // Audio Quality
        
        private const int                   DefaultBits          = 32;
        private const SpeakerSetupEnum      DefaultSpeakers      = Mono;
        private const int                   DefaultSamplingRate  = 48000;
        private const AudioFileFormatEnum   DefaultAudioFormat   = Wav;
        private const InterpolationTypeEnum DefaultInterpolation = Line;
        
        // Audio Lengths
        
        private const double                DefaultAudioLength     = 1;
        private const double                DefaultLeadingSilence  = 0.25;
        private const double                DefaultTrailingSilence = 0.25;
        
        // Feature Toggles
        
        private const bool                  DefaultPlayBack     = true;
        private const bool                  DefaultMathBoost    = true;
        private const bool                  DefaultParallels    = true;
        private const bool                  DefaultDiskCacheOn  = false;
        private const bool                  DefaultPlayAllTapes = false;
        
        // Tooling
        
        private const string                DefaultLongTestCategory               = "Long";
        private const int                   DefaultToolingSamplingRate            = 150;
        private const int                   DefaultToolingSamplingRateLongRunning = 30;
        private const bool                  DefaultToolingPlayBack                = false;
        private const bool                  DefaultToolingImpersonation           = false;
        
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
        
        public ChannelEnum Channel { get; set; } = ChannelEnum.Single;
        public int ChannelIndex { get => Channel.ToIndex(); set => Channel = value.ToChannel(GetSpeakers); }
        public void WithChannel(ChannelEnum channel) => Channel = channel;
        public void WithLeft() => WithChannel(ChannelEnum.Left);
        public void WithRight() => WithChannel(ChannelEnum.Right);
        public void WithCenter() => WithChannel(ChannelEnum.Single);

        // SamplingRate
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        private int _samplingRate;
        /// <inheritdoc cref="docs._getsamplingrate" />
        public void WithSamplingRate(int value) => _samplingRate = value;
        
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public int GetSamplingRate
        {
            get
            {
                if (_samplingRate != 0)
                {
                    // TODO: Use this message somewhere?
                    //string message = $"Sampling rate override: {samplingRateOverride}";
                    return _samplingRate;
                }
                
                if (IsUnderNCrunch)
                {
                    bool testIsLong = CurrentTestIsInCategory(GetLongTestCategory);
                    
                    if (testIsLong)
                    {
                        return _section.NCrunch.SamplingRateLongRunning ?? DefaultToolingSamplingRateLongRunning;
                    }
                    else
                    {
                        return _section.NCrunch.SamplingRate ?? DefaultToolingSamplingRate;
                    }
                }
                
                if (IsUnderAzurePipelines)
                {
                    bool testIsLong = CurrentTestIsInCategory(GetLongTestCategory);
                    
                    if (testIsLong)
                    {
                        return _section.AzurePipelines.SamplingRateLongRunning ?? DefaultToolingSamplingRateLongRunning;
                    }
                    else
                    {
                        return _section.AzurePipelines.SamplingRate ?? DefaultToolingSamplingRate;
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

        // Audio Lengths
        
        // Audio Length
        
        private FlowNode _audioLength;
        
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
        
        public void WithAudioLength(FlowNode newAudioLength) => _audioLength = newAudioLength;
        
        public void WithAudioLength(double newAudioLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            WithAudioLength(synthWishes._[newAudioLength]);
        }
        
        public void AddAudioLength(FlowNode additionalLength)
        {
            if (additionalLength == null) throw new ArgumentNullException(nameof(additionalLength));
            SynthWishes synthWishes = additionalLength.SynthWishes;
            WithAudioLength(GetAudioLength(synthWishes) + additionalLength);
        }
        
        public void AddAudioLength(double additionalLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            AddAudioLength(synthWishes._[additionalLength]);
        }
        
        // LeadingSilence
        
        private double? _leadingSilence;
        public double GetLeadingSilence => _leadingSilence ?? _section.LeadingSilence ?? DefaultLeadingSilence;
        public void WithLeadingSilence(double? value) => _leadingSilence = value;
        
        // TrailingSilence
        
        private double? _trailingSilence;
        public double GetTrailingSilence => _trailingSilence ?? _section.TrailingSilence ?? DefaultTrailingSilence;
        public void WithTrailingSilence(double? value) => _trailingSilence = value;
            
        // Feature Toggles
        
        // PlayBack
        
        private bool? _playBack;
        [Obsolete(WarningSettingMayNotWork)] public void WithPlayBack(bool? enabled = true) => _playBack = enabled;
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
        
        // Parallels
        
        /// <inheritdoc cref="docs._parallels" />
        private bool? _parallels;
        /// <inheritdoc cref="docs._parallels" />
        public bool GetParallels => _parallels ?? _section.Parallels ?? DefaultParallels;
        /// <inheritdoc cref="docs._parallels" />
        public void WithParallels(bool? enabled = true) => _parallels = enabled;
        
        // DiskCacheOn
        
        /// <inheritdoc cref="docs._diskcacheon" />
        private bool? _diskCacheOn;
        /// <inheritdoc cref="docs._diskcacheon" />
        public bool GetDiskCacheOn => _diskCacheOn ?? _section.DiskCacheOn ?? DefaultDiskCacheOn;
        /// <inheritdoc cref="docs._diskcacheon" />
        public void WithDiskCacheOn(bool? enabled = true) =>  _diskCacheOn = enabled;
        
        // PlayAllTapes
        
        /// <inheritdoc cref="docs._playalltapes" />
        private bool? _playAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _playAllTapes ?? _section.PlayAllTapes ?? DefaultPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public void WithPlayAllTapes(bool? enabled = true) => _playAllTapes = enabled;
        
        // Tooling
        
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
        
        internal const string WarningSettingMayNotWork
            = "Setting might not work in all contexts " +
              "where the system is unaware of the SynthWishes object. " +
              "This is because of a design decision in the software, that might be corrected later.";
    }

    // SynthWishes ConfigWishes
    
    public partial class SynthWishes
    {
        private ConfigResolver _configResolver;
        
        private void InitializeConfigWishes() => _configResolver = new ConfigResolver();
        
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
        
        public ChannelEnum Channel { get => _configResolver.Channel; set => _configResolver.Channel = value; }
        public int ChannelIndex { get => _configResolver.ChannelIndex; set => _configResolver.ChannelIndex = value; }
        public SynthWishes WithChannel(ChannelEnum channel) { _configResolver.WithChannel(channel); return this; }
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

        // Audio Lengths
        
        public FlowNode GetAudioLength => _configResolver.GetAudioLength(this);
        public SynthWishes WithAudioLength(double newLength) { _configResolver.WithAudioLength(newLength, this); return this; }
        public SynthWishes WithAudioLength(FlowNode newLength) { _configResolver.WithAudioLength(newLength); return this; }
        public SynthWishes AddAudioLength(double additionalLength) { _configResolver.AddAudioLength(additionalLength, this); return this; }
        public SynthWishes AddAudioLength(FlowNode additionalLength) { _configResolver.AddAudioLength(additionalLength); return this; }
        
        public double GetLeadingSilence => _configResolver.GetLeadingSilence;
        public SynthWishes WithLeadingSilence(double? seconds = default) { _configResolver.WithLeadingSilence(seconds); return this; }
        
        public double GetTrailingSilence => _configResolver.GetTrailingSilence;
        public SynthWishes WithTrailingSilence(double? seconds = default) { _configResolver.WithTrailingSilence(seconds); return this; }
        
        // Feature Toggles
        
        public bool GetPlayBack(string fileExtension = null) => _configResolver.GetPlayBack(fileExtension);
        [Obsolete(WarningSettingMayNotWork)]
        public SynthWishes WithPlayBack(bool? enabled = true) { _configResolver.WithPlayBack(enabled); return this; }
        
        public bool GetMathBoost => _configResolver.GetMathBoost;
        public SynthWishes WithMathBoost(bool? enabled = true) { _configResolver.WithMathBoost(enabled); return this; }
        
        /// <inheritdoc cref="docs._parallels" />
        public bool GetParallels => _configResolver.GetParallels;
        /// <inheritdoc cref="docs._parallels" />
        public SynthWishes WithParallels(bool? enabled = true) { _configResolver.WithParallels(enabled); return this; }

        /// <inheritdoc cref="docs._diskcacheon" />
        public bool GetDiskCacheOn => _configResolver.GetDiskCacheOn;
        /// <inheritdoc cref="docs._diskcacheon" />
        public SynthWishes WithDiskCacheOn(bool? enabled = true) { _configResolver.WithDiskCacheOn(enabled); return this; }

        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _configResolver.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes WithPlayAllTapes(bool? enabled = true) { _configResolver.WithPlayAllTapes(enabled); return this; }
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

        public ChannelEnum Channel { get => _synthWishes.Channel; set => _synthWishes.Channel = value; }
        public int ChannelIndex { get => _synthWishes.ChannelIndex; set => _synthWishes.ChannelIndex = value; }
        public FlowNode WithChannel(ChannelEnum channel) { _synthWishes.WithChannel(channel); return this; }
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
        
        // Audio Lengths
        
        public FlowNode GetAudioLength => _synthWishes.GetAudioLength;
        public FlowNode WithAudioLength(FlowNode newLength) { _synthWishes.WithAudioLength(newLength); return this; }
        public FlowNode AddAudioLength(FlowNode additionalLength) { _synthWishes.AddAudioLength(additionalLength); return this; }

        public double GetLeadingSilence => _synthWishes.GetLeadingSilence;
        public FlowNode WithLeadingSilence(double? seconds = default) { _synthWishes.WithLeadingSilence(seconds); return this; }
        
        public double GetTrailingSilence => _synthWishes.GetTrailingSilence;
        public FlowNode WithTrailingSilence(double? seconds = default) { _synthWishes.WithTrailingSilence(seconds); return this; }
        
        // Feature Toggles
        
        public bool GetPlayBack(string fileExtension = null) => _synthWishes.GetPlayBack(fileExtension);
        [Obsolete(WarningSettingMayNotWork)]
        public FlowNode WithPlayBack(bool? enabled = true) { _synthWishes.WithPlayBack(enabled); return this; }
        
        public bool GetMathBoost => _synthWishes.GetMathBoost;
        public FlowNode WithMathBoost(bool? enabled = true) { _synthWishes.WithMathBoost(enabled); return this; }
        
        /// <inheritdoc cref="docs._parallels" />
        public bool GetParallels => _synthWishes.GetParallels;
        /// <inheritdoc cref="docs._parallels" />
        public FlowNode WithParallels(bool? enabled = true) { _synthWishes.WithParallels(enabled); return this; }

        /// <inheritdoc cref="docs._diskcacheon" />
        public bool GetDiskCacheOn => _synthWishes.GetDiskCacheOn;
        /// <inheritdoc cref="docs._diskcacheon" />
        public FlowNode WithDiskCacheOn(bool? enabled = true) { _synthWishes.WithDiskCacheOn(enabled); return this; }

        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _synthWishes.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public FlowNode WithPlayAllTapes(bool? enabled = true) { _synthWishes.WithPlayAllTapes(enabled); return this; }
    }
}