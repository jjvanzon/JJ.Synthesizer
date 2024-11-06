using System;
using System.Threading;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes
{
    // Fluent Configuration
    
    // AudioLength SynthWishes

    public partial class SynthWishes
    {
        private FluentOutlet _audioLength;

        public FluentOutlet GetAudioLength
        {
            get
            {
                if ((_audioLength?.AsConst ?? 0) != 0)
                {
                    return _audioLength;
                }

                return _[ConfigHelper.DefaultAudioLength];
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
        private readonly ThreadLocal<ChannelEnum> _channel = new ThreadLocal<ChannelEnum>();

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

                return ConfigHelper.DefaultBitDepth;
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

                return ConfigHelper.DefaultSpeakerSetup;
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

                return ConfigHelper.DefaultAudioFormat;
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

                return ConfigHelper.DefaultInterpolation;
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

    // SynthWishes In-Memory Processing Enabled

    public partial class SynthWishes
    {
        private bool? _mustCacheToDisk;

        public SynthWishes WithCacheToDisk(bool? enabled = true)
        {
            _mustCacheToDisk = enabled;
            return this;
        }

        public bool MustCacheToDisk => _mustCacheToDisk ?? ConfigHelper.CacheToDisk;
    }

    // FluentOutlet In-Memory Processing Enabled

    public partial class FluentOutlet
    {
        public FluentOutlet WithCacheToDisk(bool? enabled = true) { _synthWishes.WithCacheToDisk(enabled); return this; }
        public bool MustCacheToDisk => _synthWishes.MustCacheToDisk;
    }

    // SynthWishes ParallelEnabled

    public partial class SynthWishes
    {
        private bool? _parallelEnabled;

        public SynthWishes WithParallelEnabled(bool? parallelEnabled = default)
        {
            _parallelEnabled = parallelEnabled;
            return this;
        }

        public bool GetParallelEnabled => _parallelEnabled ?? ConfigHelper.ParallelEnabled;
    }

    // FluentOutlet ParallelEnabled

    public partial class FluentOutlet
    {
        public FluentOutlet WithParallelEnabled(bool? parallelEnabled = default) { _synthWishes.WithParallelEnabled(parallelEnabled); return this; }
        private bool GetParallelEnabled => _synthWishes.GetParallelEnabled;
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
}