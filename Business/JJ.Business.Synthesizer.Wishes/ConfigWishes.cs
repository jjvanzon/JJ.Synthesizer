using System;
using System.Threading;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.ConfigResolver;

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
        [XmlAttribute] public string LongRunningTestCategory { get; set; }
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
        internal const string WarningSettingMayNotWork 
            = "Setting might not work in all contexts " +
              "where the system is unaware of the SynthWishes object. " +
              "This is because of a design decision in the software, that might be corrected later.";
            
        private static readonly ConfigSection _configSection 
            = FrameworkConfigurationWishes.TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        public const bool                  DefaultAudioPlayBack    = true;
        public const double                DefaultLeadingSilence   = 0.25;
        public const double                DefaultTrailingSilence  = 0.25;
        public const int                   DefaultSamplingRate     = 48000;
        public const SpeakerSetupEnum      DefaultSpeakers         = SpeakerSetupEnum.Mono;
        public const int                   DefaultBits             = 32;
        public const AudioFileFormatEnum   DefaultAudioFormat      = AudioFileFormatEnum.Wav;
        public const InterpolationTypeEnum DefaultInterpolation    = InterpolationTypeEnum.Line;
        public const double                DefaultAudioLength      = 1;
        public const bool                  DefaultPlayAllTapes     = false;
        public const bool                  DefaultParallels        = true;
        public const bool                  DefaultMathOptimization = true;
        public const bool                  DefaultDiskCaching      = false;
        
        private bool? _audioPlayBack;
        private double? _leadingSilence;
        private double? _trailingSilence;
        
        public bool GetAudioPlayBack => _audioPlayBack ?? _configSection.AudioPlayBack ?? DefaultAudioPlayBack;
        [Obsolete(WarningSettingMayNotWork)] public void WithAudioPlayBack(bool? value) => _audioPlayBack = value;
        
        public double GetLeadingSilence => _leadingSilence ?? _configSection.LeadingSilence ?? DefaultLeadingSilence;
        public void WithLeadingSilence(double? value) => _leadingSilence = value;
        
        public double GetTrailingSilence => _trailingSilence ?? _configSection.TrailingSilence ?? DefaultTrailingSilence;
        public void WithTrailingSilence(double? value) => _trailingSilence = value;
    }
    
    public partial class SynthWishes
    {
        internal static ConfigResolver DefaultConfigResolver { get; } = new ConfigResolver();
        internal static ToolingHelper DefaultToolingHelper { get; } = new ToolingHelper(DefaultConfigResolver);
        
        private readonly ConfigResolver _configResolver = new ConfigResolver();
        internal ToolingHelper ToolingHelper { get; private set; }
        
        private void InitializeConfigWishes()
        {
            ToolingHelper = new ToolingHelper(_configResolver);
        }
    }

    /// <inheritdoc cref="docs._confighelper"/>
    internal static class ConfigHelper
    {
        private static readonly ConfigSection _section = FrameworkConfigurationWishes.TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        // Defaults for Optional Config
        public static PersistenceConfiguration PersistenceConfiguration { get; } =
            FrameworkConfigurationWishes.TryGetSection<PersistenceConfiguration>() ??
            GetDefaultInMemoryConfiguration();

        private static PersistenceConfiguration GetDefaultInMemoryConfiguration() => new PersistenceConfiguration
        {
            ContextType = "Memory",
            ModelAssembly = NameHelper.GetAssemblyName<JJ.Persistence.Synthesizer.Operator>(),
            MappingAssembly = NameHelper.GetAssemblyName<JJ.Persistence.Synthesizer.Memory.Mappings.OperatorMapping>(),
            RepositoryAssemblies = new[]
            {
                NameHelper.GetAssemblyName<JJ.Persistence.Synthesizer.Memory.Repositories.NodeTypeRepository>(),
                NameHelper.GetAssemblyName<JJ.Persistence.Synthesizer.DefaultRepositories.OperatorRepository>()
            }
        };
        
        public static int SamplingRate => _section.SamplingRate ?? 48000;
        public static string LongRunningTestCategory
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_section.LongRunningTestCategory))
                {
                    return _section.LongRunningTestCategory;
                }
                
                return "Long";
            }
        }

        public static ConfigToolingElementWithDefaults AzurePipelines { get; } = new ConfigToolingElementWithDefaults(_section.AzurePipelines);
        public static ConfigToolingElementWithDefaults NCrunch { get; } = new ConfigToolingElementWithDefaults(_section.NCrunch);
    }

    public class ConfigToolingElementWithDefaults
    {
        private readonly ConfigToolingElement _baseConfig;
            
        internal ConfigToolingElementWithDefaults(ConfigToolingElement baseConfig) => _baseConfig = baseConfig;
            
        public int  SamplingRate            => _baseConfig.SamplingRate            ?? 150;
        public int  SamplingRateLongRunning => _baseConfig.SamplingRateLongRunning ?? 30;
        public bool AudioPlayBack           => _baseConfig.AudioPlayBack           ?? false;
        public bool Impersonate             => _baseConfig.Impersonate             ?? false;
    }

    // Defaults
    
    public partial class SynthWishes
    {
        private static readonly ConfigSection _configSection 
            = FrameworkConfigurationWishes.TryGetSection<ConfigSection>() ?? new ConfigSection();

    }
    
    // AudioLength

    public partial class SynthWishes
    {
        private FlowNode _audioLength;
        public FlowNode GetAudioLength
        {
            get
            {
                if (_audioLength != null && 
                    _audioLength.Calculate(time: 0) != 0)
                {
                    return _audioLength;
                }
                
                return _[_configSection.AudioLength ?? DefaultAudioLength];
            }
        }

        public SynthWishes WithAudioLength(Outlet audioLength) => WithAudioLength(_[audioLength]);
        public SynthWishes WithAudioLength(double audioLength) => WithAudioLength(_[audioLength]);
        public SynthWishes WithAudioLength(FlowNode audioLength) { _audioLength = audioLength; return this; }

        public SynthWishes AddAudioLength(Outlet audioLength) => AddAudioLength(_[audioLength]);
        public SynthWishes AddAudioLength(double audioLength) => AddAudioLength(_[audioLength]);
        public SynthWishes AddAudioLength(FlowNode addedLength) => WithAudioLength(GetAudioLength + addedLength);
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
        private readonly ThreadLocal<ChannelEnum> _channel = new ThreadLocal<ChannelEnum>(() => ChannelEnum.Single);
        public ChannelEnum Channel { get => _channel.Value; set => _channel.Value = value; }
        public int ChannelIndex { get => Channel.ToIndex(); set => Channel = value.ToChannel(GetSpeakers); }
        public SynthWishes WithChannel(ChannelEnum channel) { Channel = channel; return this; }
        public SynthWishes WithLeft() => WithChannel(ChannelEnum.Left);
        public SynthWishes WithRight() => WithChannel(ChannelEnum.Right);
        public SynthWishes WithCenter() => WithChannel(ChannelEnum.Single);
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
        private int _samplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public int GetSamplingRate => _samplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public SynthWishes WithSamplingRate(int value) { _samplingRate = value; return this; }
    }

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._samplingrate" />
        public int GetSamplingRate => _synthWishes.GetSamplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public FlowNode WithSamplingRate(int value) { _synthWishes.WithSamplingRate(value); return this; }
    }
    
    // Bits

    public partial class SynthWishes
    {
        private SampleDataTypeEnum _sampleDataTypeEnum;

        public int GetBits
        {
            get
            {
                if (_sampleDataTypeEnum != default)
                {
                    return _sampleDataTypeEnum.GetBits();
                }

                return _configSection.Bits ?? DefaultBits;
            }
        }

        public SynthWishes WithBits(int bits) { _sampleDataTypeEnum = bits.ToSampleDataTypeEnum(); return this; }
        public SynthWishes With32Bit() => WithBits(32);
        public SynthWishes With16Bit() => WithBits(16);
        public SynthWishes With8Bit() => WithBits(8);
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
        private SpeakerSetupEnum _speakers;
        public SpeakerSetupEnum GetSpeakers
        {
            get
            {
                if (_speakers != SpeakerSetupEnum.Undefined)
                {
                    return _speakers;
                }

                return _configSection.Speakers ?? DefaultSpeakers;
            }
        }

        public SynthWishes WithSpeakers(SpeakerSetupEnum speakers) { _speakers = speakers; return this; }
        public SynthWishes WithMono() => WithSpeakers(SpeakerSetupEnum.Mono);
        public SynthWishes WithStereo() => WithSpeakers(SpeakerSetupEnum.Stereo);
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
        private AudioFileFormatEnum _audioFormat;
        public AudioFileFormatEnum GetAudioFormat
        {
            get
            {
                if (_audioFormat != AudioFileFormatEnum.Undefined)
                {
                    return _audioFormat;
                }

                return _configSection.AudioFormat ?? DefaultAudioFormat;
            }
        }

        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat) { _audioFormat = audioFormat; return this; }
        public SynthWishes AsWav() => WithAudioFormat(AudioFileFormatEnum.Wav);
        public SynthWishes AsRaw() => WithAudioFormat(AudioFileFormatEnum.Raw);
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
        private InterpolationTypeEnum _interpolation;
        public InterpolationTypeEnum GetInterpolation
        {
            get
            {
                if (_interpolation != InterpolationTypeEnum.Undefined)
                {
                    return _interpolation;
                }

                return _configSection.Interpolation ?? DefaultInterpolation;
            }
        }

        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolationEnum) { _interpolation = interpolationEnum; return this; }
        public SynthWishes WithLinear() => WithInterpolation(InterpolationTypeEnum.Line);
        public SynthWishes WithBlocky() => WithInterpolation(InterpolationTypeEnum.Block);
    }

    public partial class FlowNode
    {
        public InterpolationTypeEnum GetInterpolation => _synthWishes.GetInterpolation;
        public FlowNode WithInterpolation(InterpolationTypeEnum interpolationEnum) { _synthWishes.WithInterpolation(interpolationEnum); return this; }
        public FlowNode WithLinear() { _synthWishes.WithLinear(); return this; }
        public FlowNode WithBlocky() { _synthWishes.WithBlocky(); return this; }
    }
    
    // AudioPlayBack
    
    public partial class SynthWishes
    {
        [Obsolete(WarningSettingMayNotWork)]
        public SynthWishes WithAudioPlayBack(bool? enabled = DefaultAudioPlayBack) { _configResolver.WithAudioPlayBack(enabled); return this; }
        public bool GetAudioPlayBack => _configResolver.GetAudioPlayBack;
    }
    
    public partial class FlowNode
    {
        [Obsolete(WarningSettingMayNotWork)]
        public FlowNode WithAudioPlayBack(bool? enabled = DefaultAudioPlayBack) { _synthWishes.WithAudioPlayBack(enabled); return this; }
        public bool GetAudioPlayBack => _synthWishes.GetAudioPlayBack;
    }
    
    // LeadingSilence
    
    public partial class SynthWishes
    {
        public SynthWishes WithLeadingSilence(double? seconds = DefaultLeadingSilence) { _configResolver.WithLeadingSilence(seconds); return this; }
        public double GetLeadingSilence => _configResolver.GetLeadingSilence;
    }
    
    public partial class FlowNode
    {
        public FlowNode WithLeadingSilence(double? seconds = DefaultLeadingSilence) { _synthWishes.WithLeadingSilence(seconds); return this; }
        public double GetLeadingSilence => _synthWishes.GetLeadingSilence;
    }
    
    // TrailingSilence
    
    public partial class SynthWishes
    {
        public SynthWishes WithTrailingSilence(double? seconds = DefaultTrailingSilence) { _configResolver.WithTrailingSilence(seconds); return this; }
        public double GetTrailingSilence => _configResolver.GetTrailingSilence;
    }
    
    public partial class FlowNode
    {
        public FlowNode WithTrailingSilence(double? seconds = DefaultTrailingSilence) { _synthWishes.WithTrailingSilence(seconds); return this; }
        public double GetTrailingSilence => _synthWishes.GetTrailingSilence;
    }

    // DiskCaching

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool? _diskCaching;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public SynthWishes WithDiskCaching(bool? enabled = true) { _diskCaching = enabled; return this; }
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetDiskCaching => _diskCaching ?? _configSection.DiskCaching ?? DefaultDiskCaching;
    }

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public FlowNode WithDiskCaching(bool? enabled = true) { _synthWishes.WithDiskCaching(enabled); return this; }
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetDiskCaching => _synthWishes.GetDiskCaching;
    }

    // Parallels

    public partial class SynthWishes
    {
        private bool? _parallels;
        public SynthWishes WithParallels(bool? enabled = default) { _parallels = enabled; return this; }
        public bool GetParallels => _parallels ?? _configSection.Parallels ?? DefaultParallels;
    }

    public partial class FlowNode
    {
        public FlowNode WithParallels(bool? enabled = default) { _synthWishes.WithParallels(enabled); return this; }
        private bool GetParallels => _synthWishes.GetParallels;
    }
    
    // PlayAllTapes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool? _playAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetPlayAllTapes => _playAllTapes ?? _configSection.PlayAllTapes ?? DefaultPlayAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public SynthWishes WithPlayAllTapes(bool? enabled = true) { _playAllTapes = enabled; return this; }
    }

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetPlayAllTapes => _synthWishes.GetPlayAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public FlowNode WithPlayAllTapes(bool? enabled = true) { _synthWishes.WithPlayAllTapes(enabled); return this; }
    }
    
    // MathOptimization
    
    public partial class SynthWishes
    {
        private bool? _mathOptimization;
        public SynthWishes WithMathOptimization(bool? enabled = true) { _mathOptimization = enabled; return this; }
        public bool GetMathOptimization => _mathOptimization ?? _configSection.MathOptimization ?? DefaultMathOptimization;
    }
    
    public partial class FlowNode
    {
        public FlowNode WithMathOptimization(bool? enabled = true) { _synthWishes.WithMathOptimization(enabled); return this; }
        private bool GetMathOptimization => _synthWishes.GetMathOptimization;
    }
}