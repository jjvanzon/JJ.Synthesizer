using System;
using System.Threading;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes
{
    internal class ConfigSection
    {
        [XmlAttribute] public int? SamplingRate { get; set; }
        [XmlAttribute] public SpeakerSetupEnum? SpeakerSetup { get; set; }
        [XmlAttribute] public int? BitDepth { get; set; }
        [XmlAttribute] public AudioFileFormatEnum? AudioFormat { get; set; }
        [XmlAttribute] public InterpolationTypeEnum? Interpolation { get; set; }
        [XmlAttribute] public double? AudioLength { get; set; }
        [XmlAttribute] public string LongRunningTestCategory { get; set; }
        [XmlAttribute] public bool? PlayEnabled { get; set; }
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
        [XmlAttribute] public bool? PlayEnabled { get; set; }
        [XmlAttribute] public bool? Impersonate { get; set; }
    }

    /// <inheritdoc cref="docs._confighelper"/>
    internal static class ConfigHelper
    {
        private static readonly ConfigSection _section = FrameworkConfigWishes.TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        // Defaults for Optional Config
        public static PersistenceConfiguration PersistenceConfiguration { get; } =
            FrameworkConfigWishes.TryGetSection<PersistenceConfiguration>() ??
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
        
        // Even the defaults have defaults, to not require a config file.
        public static int                   SamplingRate     => _section.SamplingRate     ?? 48000;
        public static SpeakerSetupEnum      SpeakerSetup     => _section.SpeakerSetup     ?? SpeakerSetupEnum.Mono;
        public static SampleDataTypeEnum    BitDepth         => (_section.BitDepth ?? 32).ToBitDepth();
        public static AudioFileFormatEnum   AudioFormat      => _section.AudioFormat      ?? AudioFileFormatEnum.Wav;
        public static InterpolationTypeEnum Interpolation    => _section.Interpolation    ?? InterpolationTypeEnum.Line;
        public static double                AudioLength      => _section.AudioLength      ?? 1;
        public static bool                  PlayEnabled      => _section.PlayEnabled      ?? true;
        public static double                LeadingSilence   => _section.LeadingSilence   ?? 0.2;
        public static double                TrailingSilence  => _section.TrailingSilence  ?? 0.2;
        public static bool                  Parallels        => _section.Parallels        ?? true;
        public static bool                  MathOptimization => _section.MathOptimization ?? true;
        public static bool                  DiskCaching      => _section.DiskCaching      ?? false;

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
        public bool PlayEnabled             => _baseConfig.PlayEnabled             ?? false;
        public bool Impersonate             => _baseConfig.Impersonate             ?? false;
    }

    // Fluent Configuration
    
    // AudioLength SynthWishes

    public partial class SynthWishes
    {
        private FluentOutlet _audioLength;

        public FluentOutlet GetAudioLength
        {
            get
            {
                if (_audioLength != null && 
                    _audioLength.Calculate(time: 0) != 0)
                {
                    return _audioLength;
                }

                return _[ConfigHelper.AudioLength];
            }
        }

        public SynthWishes WithAudioLength(Outlet audioLength) => WithAudioLength(_[audioLength]);
        
        public SynthWishes WithAudioLength(double audioLength) => WithAudioLength(_[audioLength]);

        public SynthWishes WithAudioLength(FluentOutlet audioLength)
        {
            _audioLength = audioLength;
            return this;
        }

        public SynthWishes AddAudioLength(Outlet audioLength) => AddAudioLength(_[audioLength]);
        
        public SynthWishes AddAudioLength(double audioLength) => AddAudioLength(_[audioLength]);
        
        public SynthWishes AddAudioLength(FluentOutlet addedLength) => WithAudioLength(GetAudioLength + addedLength);
    }

    // AudioLength FluentOutlet 

    public partial class FluentOutlet
    {
        public FluentOutlet GetAudioLength => _synthWishes.GetAudioLength;
        public FluentOutlet WithAudioLength(FluentOutlet newLength) { _synthWishes.WithAudioLength(newLength); return this; }
        public FluentOutlet AddAudioLength(FluentOutlet additionalLength) { _synthWishes.AddAudioLength(additionalLength); return this; }
    }

    // Channel SynthWishes

    public partial class SynthWishes
    {
        private readonly ThreadLocal<ChannelEnum> _channel = new ThreadLocal<ChannelEnum>(() => ChannelEnum.Single);

        public ChannelEnum Channel
        {
            get => _channel.Value;
            set => _channel.Value = value;
        }

        public int ChannelIndex
        {
            get => Channel.ToIndex();
            set => Channel = value.ToChannel(GetSpeakerSetup);
        }

        public SynthWishes WithChannel(ChannelEnum channel)
        {
            Channel = channel;
            return this;
        }

        public SynthWishes WithLeft() => WithChannel(ChannelEnum.Left);
        public SynthWishes WithRight() => WithChannel(ChannelEnum.Right);
        public SynthWishes WithCenter() => WithChannel(ChannelEnum.Single);
    }

    // Channel FluentOutlet

    public partial class FluentOutlet
    {
        public ChannelEnum Channel { get => _synthWishes.Channel; set => _synthWishes.Channel = value; }
        public int ChannelIndex { get => _synthWishes.ChannelIndex; set => _synthWishes.ChannelIndex = value; }
        public FluentOutlet WithChannel(ChannelEnum channel) { _synthWishes.WithChannel(channel); return this; }
        public FluentOutlet WithLeft()  { _synthWishes.WithLeft(); return this; }
        public FluentOutlet WithRight() { _synthWishes.WithRight(); return this; }
        public FluentOutlet WithCenter() { _synthWishes.WithCenter(); return this; }
    }
    
    // SamplingRate SynthWishes

    public partial class SynthWishes
    {
        private int _samplingRate;

        /// <inheritdoc cref="docs._samplingrate" />
        public int GetSamplingRate => _samplingRate;

        /// <inheritdoc cref="docs._samplingrate" />
        public SynthWishes WithSamplingRate(int value)
        {
            _samplingRate = value;
            return this;
        }
    }

    // SamplingRate FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._samplingrate" />
        public int GetSamplingRate => _synthWishes.GetSamplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public FluentOutlet WithSamplingRate(int value) { _synthWishes.WithSamplingRate(value); return this; }
    }
    
    // BitDepth SynthWishes

    public partial class SynthWishes
    {
        private SampleDataTypeEnum _bitDepth;

        public SampleDataTypeEnum GetBitDepth
        {
            get
            {
                if (_bitDepth != SampleDataTypeEnum.Undefined)
                {
                    return _bitDepth;
                }

                return ConfigHelper.BitDepth;
            }
        }

        public SynthWishes WithBitDepth(SampleDataTypeEnum bitDepth)
        {
            _bitDepth = bitDepth;
            return this;
        }

        public SynthWishes With32Bit() => WithBitDepth(SampleDataTypeEnum.Float32);
        public SynthWishes With16Bit() => WithBitDepth(SampleDataTypeEnum.Int16);
        public SynthWishes With8Bit() => WithBitDepth(SampleDataTypeEnum.Byte);
    }

    // BitDepth FluentOutlet

    public partial class FluentOutlet
    {
        public SampleDataTypeEnum GetBitDepth => _synthWishes.GetBitDepth;
        public FluentOutlet WithBitDepth(SampleDataTypeEnum bitDepth) { _synthWishes.WithBitDepth(bitDepth); return this; }
        public FluentOutlet With32Bit() { _synthWishes.With32Bit(); return this; }
        public FluentOutlet With16Bit() { _synthWishes.With16Bit(); return this; }
        public FluentOutlet With8Bit() { _synthWishes.With8Bit(); return this; }
    }

    // SpeakerSetup SynthWishes

    public partial class SynthWishes
    {
        private SpeakerSetupEnum _speakerSetup;

        public SpeakerSetupEnum GetSpeakerSetup
        {
            get
            {
                if (_speakerSetup != SpeakerSetupEnum.Undefined)
                {
                    return _speakerSetup;
                }

                return ConfigHelper.SpeakerSetup;
            }
        }

        public SynthWishes WithSpeakerSetup(SpeakerSetupEnum speakerSetup)
        {
            _speakerSetup = speakerSetup;
            return this;
        }

        public SynthWishes WithMono() => WithSpeakerSetup(SpeakerSetupEnum.Mono);
        
        public SynthWishes WithStereo() => WithSpeakerSetup(SpeakerSetupEnum.Stereo);
    }

    // SpeakerSetup FluentOutlet

    public partial class FluentOutlet
    {
        public SpeakerSetupEnum GetSpeakerSetup => _synthWishes.GetSpeakerSetup;
        public FluentOutlet WithSpeakerSetup(SpeakerSetupEnum speakerSetup) { _synthWishes.WithSpeakerSetup(speakerSetup); return this; }
        public FluentOutlet WithMono() { _synthWishes.WithMono(); return this; }
        public FluentOutlet WithStereo() { _synthWishes.WithStereo(); return this; }
    }

    // AudioFormat SynthWishes

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

                return ConfigHelper.AudioFormat;
            }
        }

        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat)
        {
            _audioFormat = audioFormat;
            return this;
        }

        public SynthWishes AsWav() => WithAudioFormat(AudioFileFormatEnum.Wav);

        public SynthWishes AsRaw() => WithAudioFormat(AudioFileFormatEnum.Raw);
    }

    // AudioFormat FluentOutlet

    public partial class FluentOutlet
    {
        public AudioFileFormatEnum GetAudioFormat => _synthWishes.GetAudioFormat;
        public FluentOutlet WithAudioFormat(AudioFileFormatEnum audioFormat) { _synthWishes.WithAudioFormat(audioFormat); return this; }
        public FluentOutlet AsWav() { _synthWishes.AsWav(); return this; }
        public FluentOutlet AsRaw() { _synthWishes.AsRaw(); return this; }
    }

    // Interpolation SynthWishes

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

                return ConfigHelper.Interpolation;
            }
        }

        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolationEnum)
        {
            _interpolation = interpolationEnum;
            return this;
        }

        public SynthWishes WithLinear() => WithInterpolation(InterpolationTypeEnum.Line);

        public SynthWishes WithBlocky() => WithInterpolation(InterpolationTypeEnum.Block);
    }

    // Interpolation FluentOutlet

    public partial class FluentOutlet
    {
        public InterpolationTypeEnum GetInterpolation => _synthWishes.GetInterpolation;
        public FluentOutlet WithInterpolation(InterpolationTypeEnum interpolationEnum) { _synthWishes.WithInterpolation(interpolationEnum); return this; }
        public FluentOutlet WithLinear() { _synthWishes.WithLinear(); return this; }
        public FluentOutlet WithBlocky() { _synthWishes.WithBlocky(); return this; }
    }

    // SynthWishes DiskCaching

    public partial class SynthWishes
    {
        private bool? _diskCaching;

        public SynthWishes WithDiskCaching(bool? enabled = true)
        {
            _diskCaching = enabled;
            return this;
        }

        public bool GetDiskCaching => _diskCaching ?? ConfigHelper.DiskCaching;
    }

    // FluentOutlet DiskCaching

    public partial class FluentOutlet
    {
        public FluentOutlet WithDiskCaching(bool? enabled = true) { _synthWishes.WithDiskCaching(enabled); return this; }
        public bool GetDiskCaching => _synthWishes.GetDiskCaching;
    }

    // SynthWishes Parallels

    public partial class SynthWishes
    {
        private bool? _parallels;

        public SynthWishes WithParallels(bool? enabled = default)
        {
            _parallels = enabled;
            return this;
        }

        public bool GetParallels => _parallels ?? ConfigHelper.Parallels;
    }

    // FluentOutlet Parallel

    public partial class FluentOutlet
    {
        public FluentOutlet WithParallels(bool? enabled = default) { _synthWishes.WithParallels(enabled); return this; }
        private bool GetParallels => _synthWishes.GetParallels;
    }
    
    // SynthWishes PlayParallels

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._withpreviewparallels" />
        public bool MustPlayParallels { get; private set; }

        /// <inheritdoc cref="docs._withpreviewparallels" />
        public SynthWishes WithPlayParallels(bool mustPlayParallels = true)
        {
            MustPlayParallels = mustPlayParallels;
            return this;
        }
    }

    // FluentOutlet PlayParallels

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._withpreviewparallels" />
        public bool MustPlayParallels => _synthWishes.MustPlayParallels;
        /// <inheritdoc cref="docs._withpreviewparallels" />
        public FluentOutlet WithPlayParallels(bool mustPlayParallels = true) { _synthWishes.WithPlayParallels(mustPlayParallels); return this; }
    }
    
    // SynthWishes MathOptimizationEnabled
    
    public partial class SynthWishes
    {
        private bool? _mathOptimization;
        
        public SynthWishes WithMathOptimization(bool? mathOptimizationEnabled = true)
        {
            _mathOptimization = mathOptimizationEnabled;
            return this;
        }
        
        public bool GetMathOptimizationEnabled => _mathOptimization ?? ConfigHelper.MathOptimization;
    }
    
    // FluentOutlet MathOptimizationEnabled
    
    public partial class FluentOutlet
    {
        public FluentOutlet WithMathOptimization(bool? mathOptimizationEnabled = true) { _synthWishes.WithMathOptimization(mathOptimizationEnabled); return this; }
        private bool GetMathOptimizationEnabled => _synthWishes.GetMathOptimizationEnabled;
    }
}