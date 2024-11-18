using System;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.ConfigResolver;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkConfigurationWishes;

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
        private static readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        public const bool                  DefaultAudioPlayBack    = true;
        public const double                DefaultLeadingSilence   = 0.25;
        public const double                DefaultTrailingSilence  = 0.25;
        public const int                   DefaultSamplingRate     = 48000;
        public const SpeakerSetupEnum      DefaultSpeakers         = Mono;
        public const int                   DefaultBits             = 32;
        public const AudioFileFormatEnum   DefaultAudioFormat      = Wav;
        public const InterpolationTypeEnum DefaultInterpolation    = Line;
        public const double                DefaultAudioLength      = 1;
        public const bool                  DefaultPlayAllTapes     = false;
        public const bool                  DefaultParallels        = true;
        public const bool                  DefaultMathOptimization = true;
        public const bool                  DefaultDiskCaching      = false;
        public const string                DefaultLongTestCategory = "Long";
        
        private bool? _audioPlayBack;
        public bool GetAudioPlayBack => _audioPlayBack ?? _section.AudioPlayBack ?? DefaultAudioPlayBack;
        [Obsolete(WarningSettingMayNotWork)] public void WithAudioPlayBack(bool? value) => _audioPlayBack = value;
        
        private double? _leadingSilence;
        public double GetLeadingSilence => _leadingSilence ?? _section.LeadingSilence ?? DefaultLeadingSilence;
        public void WithLeadingSilence(double? value) => _leadingSilence = value;
        
        private double? _trailingSilence;
        public double GetTrailingSilence => _trailingSilence ?? _section.TrailingSilence ?? DefaultTrailingSilence;
        public void WithTrailingSilence(double? value) => _trailingSilence = value;
        
        private SpeakerSetupEnum _speakers;
        public SpeakerSetupEnum GetSpeakers => _speakers != default ? _speakers : _section.Speakers ?? DefaultSpeakers;
        public void WithSpeakers(SpeakerSetupEnum speakers) => _speakers = speakers;
        public void WithMono() => WithSpeakers(Mono);
        public void WithStereo() => WithSpeakers(Stereo);

        private SampleDataTypeEnum _sampleDataTypeEnum;
        public int GetBits => _sampleDataTypeEnum != default ? _sampleDataTypeEnum.GetBits() : _section.Bits ?? DefaultBits;
        public void WithBits(int bits) => _sampleDataTypeEnum = bits.ToSampleDataTypeEnum();
        public void With32Bit() => WithBits(32);
        public void With16Bit() => WithBits(16);
        public void With8Bit() => WithBits(8);

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
        
        private AudioFileFormatEnum _audioFormat;
        public AudioFileFormatEnum GetAudioFormat => _audioFormat != default ? _audioFormat : _section.AudioFormat ?? DefaultAudioFormat;
        public void WithAudioFormat(AudioFileFormatEnum audioFormat) => _audioFormat = audioFormat;
        public void AsWav() => WithAudioFormat(Wav);
        public void AsRaw() => WithAudioFormat(Raw);
        
        private InterpolationTypeEnum _interpolation;
        public InterpolationTypeEnum GetInterpolation => _interpolation != default ? _interpolation : _section.Interpolation ?? DefaultInterpolation;
        public void WithInterpolation(InterpolationTypeEnum interpolation) => _interpolation = interpolation;
        public void WithLinear() => WithInterpolation(Line);
        public void WithBlocky() => WithInterpolation(Block);
        
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool? _diskCaching;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetDiskCaching => _diskCaching ?? _section.DiskCaching ?? DefaultDiskCaching;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public void WithDiskCaching(bool? enabled = default) =>  _diskCaching = enabled;
        
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool? _parallels;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetParallels => _parallels ?? _section.Parallels ?? DefaultParallels;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public void WithParallels(bool? enabled = default) => _parallels = enabled;
        
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool? _playAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetPlayAllTapes => _playAllTapes ?? _section.PlayAllTapes ?? DefaultPlayAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public void WithPlayAllTapes(bool? enabled = true) => _playAllTapes = enabled;
        
        private bool? _mathOptimization;
        public bool GetMathOptimization => _mathOptimization ?? _section.MathOptimization ?? DefaultMathOptimization;
        public void WithMathOptimization(bool? enabled = default) => _mathOptimization = enabled;
        
        // Channel has a special role. Custom handling. Not in config file. Transient in nature.
        public ChannelEnum Channel { get; set; } = ChannelEnum.Single;
        public int ChannelIndex { get => Channel.ToIndex(); set => Channel = value.ToChannel(GetSpeakers); }
        public void WithChannel(ChannelEnum channel) => Channel = channel; 
        public void WithLeft() => WithChannel(ChannelEnum.Left);
        public void WithRight() => WithChannel(ChannelEnum.Right);
        public void WithCenter() => WithChannel(ChannelEnum.Single);
        
        /// <inheritdoc cref="docs._samplingrate" />
        private int _samplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public int GetSamplingRate => _samplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public void WithSamplingRate(int value) => _samplingRate = value;
        
        /// <inheritdoc cref="docs._resolvesamplingrate"/>
        public int ResolveSamplingRate()
        {
            int samplingRateOverride = GetSamplingRate;
            if (samplingRateOverride != 0)
            {
                // TODO: Use this message somewhere?
                //string message = $"Sampling rate override: {samplingRateOverride}";
                return samplingRateOverride;
            }
            
            if (ToolingHelper.IsUnderNCrunch)
            {
                bool testIsLong = ToolingHelper.CurrentTestIsInCategory(GetLongTestCategory);
                
                if (testIsLong)
                {
                    return ConfigHelper.NCrunch.SamplingRateLongRunning;
                }
                else
                {
                    return ConfigHelper.NCrunch.SamplingRate;
                }
            }
            
            if (ToolingHelper.IsUnderAzurePipelines)
            {
                bool testIsLong = ToolingHelper.CurrentTestIsInCategory(GetLongTestCategory);
                
                if (testIsLong)
                {
                    return ConfigHelper.AzurePipelines.SamplingRateLongRunning;
                }
                else
                {
                    return ConfigHelper.AzurePipelines.SamplingRate;
                }
            }
            
            return ConfigHelper.SamplingRate;
        }
        
        internal const string WarningSettingMayNotWork
            = "Setting might not work in all contexts " +
              "where the system is unaware of the SynthWishes object. " +
              "This is because of a design decision in the software, that might be corrected later.";
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
        private static readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        // Defaults for Optional Config
        public static PersistenceConfiguration PersistenceConfiguration { get; } =
            TryGetSection<PersistenceConfiguration>() ??
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
        
        public static int SamplingRate => _section.SamplingRate ?? DefaultSamplingRate;

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

    public partial class SynthWishes
    {
        private static readonly ConfigSection _configSection 
            = TryGetSection<ConfigSection>() ?? new ConfigSection();
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
        public ChannelEnum Channel { get => _configResolver.Channel; set => _configResolver.Channel = value; }
        public int ChannelIndex { get => _configResolver.ChannelIndex; set => _configResolver.ChannelIndex = value; }
        public SynthWishes WithChannel(ChannelEnum channel) { _configResolver.WithChannel(channel); return this; }
        public SynthWishes WithLeft() { _configResolver.WithLeft(); return this; }
        public SynthWishes WithRight()  { _configResolver.WithRight(); return this; }
        public SynthWishes WithCenter()  { _configResolver.WithCenter(); return this; }
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
        /// <inheritdoc cref="docs._samplingrate" />
        public int GetSamplingRate => _configResolver.GetSamplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public SynthWishes WithSamplingRate(int value) { _configResolver.WithSamplingRate(value); return this; }
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
        public int GetBits => _configResolver.GetBits;
        public SynthWishes WithBits(int bits) { _configResolver.WithBits(bits); return this; }
        public SynthWishes With32Bit() { _configResolver.With32Bit(); return this; }
        public SynthWishes With16Bit() { _configResolver.With16Bit(); return this; }
        public SynthWishes With8Bit() { _configResolver.With8Bit(); return this; }
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
        public SpeakerSetupEnum GetSpeakers => _configResolver.GetSpeakers;
        public SynthWishes WithSpeakers(SpeakerSetupEnum speakers) { _configResolver.WithSpeakers(speakers); return this; }
        public SynthWishes WithMono() { _configResolver.WithMono(); return this; }
        public SynthWishes WithStereo() { _configResolver.WithStereo(); return this; }
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
        public AudioFileFormatEnum GetAudioFormat => _configResolver.GetAudioFormat;
        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat) { _configResolver.WithAudioFormat(audioFormat); return this; }
        public SynthWishes AsWav() { _configResolver.AsWav(); return this; }  
        public SynthWishes AsRaw() { _configResolver.AsRaw(); return this; }
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
        public InterpolationTypeEnum GetInterpolation => _configResolver.GetInterpolation;
        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolation) { _configResolver.WithInterpolation(interpolation); return this; }
        public SynthWishes WithLinear() {_configResolver.WithLinear(); return this; }
        public SynthWishes WithBlocky() { _configResolver.WithBlocky(); return this; }
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
        public SynthWishes WithAudioPlayBack(bool? enabled = default) { _configResolver.WithAudioPlayBack(enabled); return this; }
        public bool GetAudioPlayBack => _configResolver.GetAudioPlayBack;
    }
    
    public partial class FlowNode
    {
        [Obsolete(WarningSettingMayNotWork)]
        public FlowNode WithAudioPlayBack(bool? enabled = default) { _synthWishes.WithAudioPlayBack(enabled); return this; }
        public bool GetAudioPlayBack => _synthWishes.GetAudioPlayBack;
    }
    
    // LeadingSilence
    
    public partial class SynthWishes
    {
        public SynthWishes WithLeadingSilence(double? seconds = default) { _configResolver.WithLeadingSilence(seconds); return this; }
        public double GetLeadingSilence => _configResolver.GetLeadingSilence;
    }
    
    public partial class FlowNode
    {
        public FlowNode WithLeadingSilence(double? seconds = default) { _synthWishes.WithLeadingSilence(seconds); return this; }
        public double GetLeadingSilence => _synthWishes.GetLeadingSilence;
    }
    
    // TrailingSilence
    
    public partial class SynthWishes
    {
        public SynthWishes WithTrailingSilence(double? seconds = default) { _configResolver.WithTrailingSilence(seconds); return this; }
        public double GetTrailingSilence => _configResolver.GetTrailingSilence;
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
        public bool GetDiskCaching => _configResolver.GetDiskCaching;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public SynthWishes WithDiskCaching(bool? enabled = default) { _configResolver.WithDiskCaching(enabled); return this; }
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
        public bool GetParallels => _configResolver.GetParallels;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public SynthWishes WithParallels(bool? enabled = default) { _configResolver.WithParallels(enabled); return this; }
    }

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        private bool GetParallels => _synthWishes.GetParallels;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public FlowNode WithParallels(bool? enabled = default) { _synthWishes.WithParallels(enabled); return this; }
    }
    
    // PlayAllTapes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public bool GetPlayAllTapes => _configResolver.GetPlayAllTapes;
        /// <inheritdoc cref="docs._parallelsanddiskcaching" />
        public SynthWishes WithPlayAllTapes(bool? enabled = default) { _configResolver.WithPlayAllTapes(enabled); return this; }
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
        public bool GetMathOptimization => _configResolver.GetMathOptimization;
        public SynthWishes WithMathOptimization(bool? enabled = default) { _configResolver.WithMathOptimization(enabled); return this; }
    }
    
    public partial class FlowNode
    {
        public FlowNode WithMathOptimization(bool? enabled = default) { _synthWishes.WithMathOptimization(enabled); return this; }
        private bool GetMathOptimization => _synthWishes.GetMathOptimization;
    }
}