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
    internal class ConfigSection
    {
        [XmlAttribute] public int? SamplingRate { get; set; }
        [XmlAttribute] public SpeakerSetupEnum? Speakers { get; set; }
        [XmlAttribute] public int? Bits { get; set; }
        [XmlAttribute] public AudioFileFormatEnum? AudioFormat { get; set; }
        [XmlAttribute] public InterpolationTypeEnum? Interpolation { get; set; }
        [XmlAttribute] public double? AudioLength { get; set; }
        [XmlAttribute] public string LongTestCategory { get; set; }
        [XmlAttribute] public bool? AudioPlayBack { get; set; }
        [XmlAttribute] public bool? PlayAllTapes { get; set; }
        [XmlAttribute] public double? LeadingSilence { get; set; }
        [XmlAttribute] public double? TrailingSilence { get; set; }
        [XmlAttribute] public bool? Parallels { get; set; }
        [XmlAttribute] public bool? MathOptimization { get; set; }
        [XmlAttribute] public bool? DiskCaching { get; set; }

        public ConfigToolingElement AzurePipelines { get; set; } = new ConfigToolingElement();
        public ConfigToolingElement NCrunch { get; set; } = new ConfigToolingElement();
    }

    internal class ConfigToolingElement
    {
        [XmlAttribute] public int? SamplingRate { get; set; }
        [XmlAttribute] public int? SamplingRateLongRunning { get; set; }
        [XmlAttribute] public bool? AudioPlayBack { get; set; }
        [XmlAttribute] public bool? Impersonate { get; set; }
    }
    
    // ConfigResolver
    
    internal class ConfigResolver
    {
        private const bool                  DefaultAudioPlayBack    = true;
        private const double                DefaultLeadingSilence   = 0.25;
        private const double                DefaultTrailingSilence  = 0.25;
        private const int                   DefaultSamplingRate     = 48000;
        private const SpeakerSetupEnum      DefaultSpeakers         = Mono;
        private const int                   DefaultBits             = 32;
        private const AudioFileFormatEnum   DefaultAudioFormat      = Wav;
        private const InterpolationTypeEnum DefaultInterpolation    = Line;
        private const double                DefaultAudioLength      = 1;
        private const bool                  DefaultPlayAllTapes     = false;
        private const bool                  DefaultParallels        = true;
        private const bool                  DefaultMathOptimization = true;
        private const bool                  DefaultDiskCaching      = false;
        private const string                DefaultLongTestCategory = "Long";
        
        private const int                   DefaultToolingSamplingRate            = 150;
        private const int                   DefaultToolingSamplingRateLongRunning = 30;
        private const bool                  DefaultToolingAudioPlayBack           = false;
        private const bool                  DefaultToolingImpersonate             = false;
        
        private static readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        // AudioPlayBack
        
        private bool? _audioPlayBack;
        [Obsolete(WarningSettingMayNotWork)] public void WithAudioPlayBack(bool? value) => _audioPlayBack = value;
        public bool GetAudioPlayBack(string fileExtension = null)
        {
            bool audioPlayBack = _audioPlayBack ?? _section.AudioPlayBack ?? DefaultAudioPlayBack;
            if (!audioPlayBack)
            {
                return false;
            }
            
            if (IsUnderNCrunch)
            {
                return _section.NCrunch.AudioPlayBack ?? DefaultToolingAudioPlayBack;
            }
            
            if (IsUnderAzurePipelines)
            {
                return _section.AzurePipelines.AudioPlayBack ?? DefaultToolingAudioPlayBack;
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

        // LeadingSilence
        
        private double? _leadingSilence;
        public double GetLeadingSilence => _leadingSilence ?? _section.LeadingSilence ?? DefaultLeadingSilence;
        public void WithLeadingSilence(double? value) => _leadingSilence = value;
        
        // TrailingSilence
        
        private double? _trailingSilence;
        public double GetTrailingSilence => _trailingSilence ?? _section.TrailingSilence ?? DefaultTrailingSilence;
        public void WithTrailingSilence(double? value) => _trailingSilence = value;
        
        // Speakers
        
        private SpeakerSetupEnum _speakers;
        public SpeakerSetupEnum GetSpeakers => _speakers != default ? _speakers : _section.Speakers ?? DefaultSpeakers;
        public void WithSpeakers(SpeakerSetupEnum speakers) => _speakers = speakers;
        public void WithMono() => WithSpeakers(Mono);
        public void WithStereo() => WithSpeakers(Stereo);

        // Bits
        
        private SampleDataTypeEnum _sampleDataTypeEnum;
        public int GetBits => _sampleDataTypeEnum != default ? _sampleDataTypeEnum.GetBits() : _section.Bits ?? DefaultBits;
        public void WithBits(int bits) => _sampleDataTypeEnum = bits.ToSampleDataTypeEnum();
        public void With32Bit() => WithBits(32);
        public void With16Bit() => WithBits(16);
        public void With8Bit() => WithBits(8);

        // LongTestCategory
        
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
        
        // DiskCaching
        
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool? _diskCaching;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetDiskCaching => _diskCaching ?? _section.DiskCaching ?? DefaultDiskCaching;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public void WithDiskCaching(bool? enabled = default) =>  _diskCaching = enabled;
        
        // Parallels
        
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool? _parallels;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetParallels => _parallels ?? _section.Parallels ?? DefaultParallels;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public void WithParallels(bool? enabled = default) => _parallels = enabled;
        
        // PlayAllTapes
        
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool? _playAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetPlayAllTapes => _playAllTapes ?? _section.PlayAllTapes ?? DefaultPlayAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public void WithPlayAllTapes(bool? enabled = true) => _playAllTapes = enabled;
        
        // MathOptimization
        
        private bool? _mathOptimization;
        public bool GetMathOptimization => _mathOptimization ?? _section.MathOptimization ?? DefaultMathOptimization;
        public void WithMathOptimization(bool? enabled = default) => _mathOptimization = enabled;
        
        // Channel
        
        // Channel has a special role. Custom handling. Not in config file. Transient in nature.
        public ChannelEnum Channel { get; set; } = ChannelEnum.Single;
        public int ChannelIndex { get => Channel.ToIndex(); set => Channel = value.ToChannel(GetSpeakers); }
        public void WithChannel(ChannelEnum channel) => Channel = channel; 
        public void WithLeft() => WithChannel(ChannelEnum.Left);
        public void WithRight() => WithChannel(ChannelEnum.Right);
        public void WithCenter() => WithChannel(ChannelEnum.Single);
        
        // SamplingRate
        
        /// <inheritdoc cref="docs._samplingrate" />
        private int _samplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public void WithSamplingRate(int value) => _samplingRate = value;
        
        /// <inheritdoc cref="docs._resolvesamplingrate"/>
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
        
        // AudioLength
        
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
        
        // Tooling
        
        internal bool GetImpersonateNCrunch => _section.NCrunch.Impersonate ?? DefaultToolingImpersonate;
        internal bool GetImpersonateAzurePipelines => _section.AzurePipelines.Impersonate ?? DefaultToolingImpersonate;
        
        public bool IsUnderNCrunch
        {
            get
            {
                if (GetImpersonateNCrunch)
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
                if (GetImpersonateAzurePipelines)
                {
                    return true;
                }
                bool isUnderAzurePipelines = EnvironmentVariableIsDefined(AzurePipelinesEnvironmentVariableName, AzurePipelinesEnvironmentVariableValue);
                return isUnderAzurePipelines;
            }
        }
        
        // Persistence Config
        
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
    
    public partial class SynthWishes
    {
        private ConfigResolver ConfigResolver { get; set; }
        
        private void InitializeConfigWishes() => ConfigResolver = new ConfigResolver();
    }
    
    // AudioLength

    public partial class SynthWishes
    {
        public FlowNode GetAudioLength => ConfigResolver.GetAudioLength(this);
        public SynthWishes WithAudioLength(double newLength) { ConfigResolver.WithAudioLength(newLength, this); return this; }
        public SynthWishes WithAudioLength(FlowNode newLength) { ConfigResolver.WithAudioLength(newLength); return this; }
        public SynthWishes AddAudioLength(double additionalLength) { ConfigResolver.AddAudioLength(additionalLength, this); return this; }
        public SynthWishes AddAudioLength(FlowNode additionalLength) { ConfigResolver.AddAudioLength(additionalLength); return this; }
    }

    public partial class FlowNode
    {
        public FlowNode GetAudioLength => _synthWishes.GetAudioLength;
        public FlowNode WithAudioLength(FlowNode newLength) { _synthWishes.WithAudioLength(newLength); return this; }
        public FlowNode AddAudioLength(FlowNode additionalLength) { _synthWishes.AddAudioLength(additionalLength); return this; }
    }

    // Channel

    public partial class SynthWishes
    {
        public ChannelEnum Channel { get => ConfigResolver.Channel; set => ConfigResolver.Channel = value; }
        public int ChannelIndex { get => ConfigResolver.ChannelIndex; set => ConfigResolver.ChannelIndex = value; }
        public SynthWishes WithChannel(ChannelEnum channel) { ConfigResolver.WithChannel(channel); return this; }
        public SynthWishes WithLeft() { ConfigResolver.WithLeft(); return this; }
        public SynthWishes WithRight()  { ConfigResolver.WithRight(); return this; }
        public SynthWishes WithCenter()  { ConfigResolver.WithCenter(); return this; }
    }

    public partial class FlowNode
    {
        public ChannelEnum Channel { get => _synthWishes.Channel; set => _synthWishes.Channel = value; }
        public int ChannelIndex { get => _synthWishes.ChannelIndex; set => _synthWishes.ChannelIndex = value; }
        public FlowNode WithChannel(ChannelEnum channel) { _synthWishes.WithChannel(channel); return this; }
        public FlowNode WithLeft()  { _synthWishes.WithLeft(); return this; }
        public FlowNode WithRight() { _synthWishes.WithRight(); return this; }
        public FlowNode WithCenter() { _synthWishes.WithCenter(); return this; }
    }
    
    // SamplingRate

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._resolvesamplingrate"/>
        public int GetSamplingRate => ConfigResolver.GetSamplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public SynthWishes WithSamplingRate(int value) { ConfigResolver.WithSamplingRate(value); return this; }
    }

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._resolvesamplingrate"/>
        public int GetSamplingRate => _synthWishes.GetSamplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public FlowNode WithSamplingRate(int value) { _synthWishes.WithSamplingRate(value); return this; }
    }
    
    // Bits

    public partial class SynthWishes
    {
        public int GetBits => ConfigResolver.GetBits;
        public SynthWishes WithBits(int bits) { ConfigResolver.WithBits(bits); return this; }
        public SynthWishes With32Bit() { ConfigResolver.With32Bit(); return this; }
        public SynthWishes With16Bit() { ConfigResolver.With16Bit(); return this; }
        public SynthWishes With8Bit() { ConfigResolver.With8Bit(); return this; }
    }

    public partial class FlowNode
    {
        public int GetBits => _synthWishes.GetBits;
        public FlowNode WithBits(int bits) { _synthWishes.WithBits(bits); return this; }
        public FlowNode With32Bit() { _synthWishes.With32Bit(); return this; }
        public FlowNode With16Bit() { _synthWishes.With16Bit(); return this; }
        public FlowNode With8Bit() { _synthWishes.With8Bit(); return this; }
    }

    // Speakers

    public partial class SynthWishes
    {
        public SpeakerSetupEnum GetSpeakers => ConfigResolver.GetSpeakers;
        public SynthWishes WithSpeakers(SpeakerSetupEnum speakers) { ConfigResolver.WithSpeakers(speakers); return this; }
        public SynthWishes WithMono() { ConfigResolver.WithMono(); return this; }
        public SynthWishes WithStereo() { ConfigResolver.WithStereo(); return this; }
    }

    public partial class FlowNode
    {
        public SpeakerSetupEnum GetSpeakers => _synthWishes.GetSpeakers;
        public FlowNode WithSpeakers(SpeakerSetupEnum speakers) { _synthWishes.WithSpeakers(speakers); return this; }
        public FlowNode WithMono() { _synthWishes.WithMono(); return this; }
        public FlowNode WithStereo() { _synthWishes.WithStereo(); return this; }
    }

    // AudioFormat

    public partial class SynthWishes
    {
        public AudioFileFormatEnum GetAudioFormat => ConfigResolver.GetAudioFormat;
        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat) { ConfigResolver.WithAudioFormat(audioFormat); return this; }
        public SynthWishes AsWav() { ConfigResolver.AsWav(); return this; }  
        public SynthWishes AsRaw() { ConfigResolver.AsRaw(); return this; }
    } 

    public partial class FlowNode
    {
        public AudioFileFormatEnum GetAudioFormat => _synthWishes.GetAudioFormat;
        public FlowNode WithAudioFormat(AudioFileFormatEnum audioFormat) { _synthWishes.WithAudioFormat(audioFormat); return this; }
        public FlowNode AsWav() { _synthWishes.AsWav(); return this; }
        public FlowNode AsRaw() { _synthWishes.AsRaw(); return this; }
    }

    // Interpolation

    public partial class SynthWishes
    {
        public InterpolationTypeEnum GetInterpolation => ConfigResolver.GetInterpolation;
        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolation) { ConfigResolver.WithInterpolation(interpolation); return this; }
        public SynthWishes WithLinear() {ConfigResolver.WithLinear(); return this; }
        public SynthWishes WithBlocky() { ConfigResolver.WithBlocky(); return this; }
    }

    public partial class FlowNode
    {
        public InterpolationTypeEnum GetInterpolation => _synthWishes.GetInterpolation;
        public FlowNode WithInterpolation(InterpolationTypeEnum interpolation) { _synthWishes.WithInterpolation(interpolation); return this; }
        public FlowNode WithLinear() { _synthWishes.WithLinear(); return this; }
        public FlowNode WithBlocky() { _synthWishes.WithBlocky(); return this; }
    }
    
    // AudioPlayBack
    
    public partial class SynthWishes
    {
        [Obsolete(WarningSettingMayNotWork)]
        public SynthWishes WithAudioPlayBack(bool? enabled = default) { ConfigResolver.WithAudioPlayBack(enabled); return this; }
        public bool GetAudioPlayBack(string fileExtension = null) => ConfigResolver.GetAudioPlayBack(fileExtension);
    }
    
    public partial class FlowNode
    {
        [Obsolete(WarningSettingMayNotWork)]
        public FlowNode WithAudioPlayBack(bool? enabled = default) { _synthWishes.WithAudioPlayBack(enabled); return this; }
        public bool GetAudioPlayBack(string fileExtension = null) => _synthWishes.GetAudioPlayBack(fileExtension);
    }
    
    // LeadingSilence
    
    public partial class SynthWishes
    {
        public SynthWishes WithLeadingSilence(double? seconds = default) { ConfigResolver.WithLeadingSilence(seconds); return this; }
        public double GetLeadingSilence => ConfigResolver.GetLeadingSilence;
    }
    
    public partial class FlowNode
    {
        public FlowNode WithLeadingSilence(double? seconds = default) { _synthWishes.WithLeadingSilence(seconds); return this; }
        public double GetLeadingSilence => _synthWishes.GetLeadingSilence;
    }
    
    // TrailingSilence
    
    public partial class SynthWishes
    {
        public SynthWishes WithTrailingSilence(double? seconds = default) { ConfigResolver.WithTrailingSilence(seconds); return this; }
        public double GetTrailingSilence => ConfigResolver.GetTrailingSilence;
    }
    
    public partial class FlowNode
    {
        public FlowNode WithTrailingSilence(double? seconds = default) { _synthWishes.WithTrailingSilence(seconds); return this; }
        public double GetTrailingSilence => _synthWishes.GetTrailingSilence;
    }

    // DiskCaching

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetDiskCaching => ConfigResolver.GetDiskCaching;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public SynthWishes WithDiskCaching(bool? enabled = default) { ConfigResolver.WithDiskCaching(enabled); return this; }
    }

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetDiskCaching => _synthWishes.GetDiskCaching;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public FlowNode WithDiskCaching(bool? enabled = default) { _synthWishes.WithDiskCaching(enabled); return this; }
    }

    // Parallels

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetParallels => ConfigResolver.GetParallels;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public SynthWishes WithParallels(bool? enabled = default) { ConfigResolver.WithParallels(enabled); return this; }
    }

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetParallels => _synthWishes.GetParallels;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public FlowNode WithParallels(bool? enabled = default) { _synthWishes.WithParallels(enabled); return this; }
    }
    
    // PlayAllTapes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetPlayAllTapes => ConfigResolver.GetPlayAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public SynthWishes WithPlayAllTapes(bool? enabled = default) { ConfigResolver.WithPlayAllTapes(enabled); return this; }
    }

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetPlayAllTapes => _synthWishes.GetPlayAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public FlowNode WithPlayAllTapes(bool? enabled = default) { _synthWishes.WithPlayAllTapes(enabled); return this; }
    }
    
    // MathOptimization
    
    public partial class SynthWishes
    {
        public bool GetMathOptimization => ConfigResolver.GetMathOptimization;
        public SynthWishes WithMathOptimization(bool? enabled = default) { ConfigResolver.WithMathOptimization(enabled); return this; }
    }
    
    public partial class FlowNode
    {
        public FlowNode WithMathOptimization(bool? enabled = default) { _synthWishes.WithMathOptimization(enabled); return this; }
        public bool GetMathOptimization => _synthWishes.GetMathOptimization;
    }
}