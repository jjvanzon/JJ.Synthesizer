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
        public FluentOutlet GetAudioLength => _x.GetAudioLength;
        public FluentOutlet WithAudioLength(FluentOutlet newLength) { _x.WithAudioLength(newLength); return this; }
        public FluentOutlet AddAudioLength(FluentOutlet additionalLength) { _x.AddAudioLength(additionalLength); return this; }
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
        public ChannelEnum Channel { get => _x.Channel; set => _x.Channel = value; }
        public int ChannelIndex { get => _x.ChannelIndex; set => _x.ChannelIndex = value; }
        public FluentOutlet WithChannel(ChannelEnum channel) { _x.WithChannel(channel); return this; }
        public FluentOutlet WithLeft()  { _x.WithLeft(); return this; }
        public FluentOutlet WithRight() { _x.WithRight(); return this; }
        public FluentOutlet WithCenter() { _x.WithCenter(); return this; }
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
        public int GetSamplingRate => _x.GetSamplingRate;
        /// <inheritdoc cref="docs._samplingrate" />
        public FluentOutlet WithSamplingRate(int value) { _x.WithSamplingRate(value); return this; }
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

        public SynthWishes With16Bit() => WithBitDepth(SampleDataTypeEnum.Int16);
        
        public SynthWishes With8Bit() => WithBitDepth(SampleDataTypeEnum.Byte);
    }

    // BitDepth FluentOutlet

    public partial class FluentOutlet
    {
        public SampleDataTypeEnum GetBitDepth => _x.GetBitDepth;
        public FluentOutlet WithBitDepth(SampleDataTypeEnum bitDepth) { _x.WithBitDepth(bitDepth); return this; }
        public FluentOutlet With16Bit() { _x.With16Bit(); return this; }
        public FluentOutlet With8Bit() { _x.With8Bit(); return this; }
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
        public SpeakerSetupEnum GetSpeakerSetup => _x.GetSpeakerSetup;
        public FluentOutlet WithSpeakerSetup(SpeakerSetupEnum speakerSetup) { _x.WithSpeakerSetup(speakerSetup); return this; }
        public FluentOutlet WithMono() { _x.WithMono(); return this; }
        public FluentOutlet WithStereo() { _x.WithStereo(); return this; }
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
        public AudioFileFormatEnum GetAudioFormat => _x.GetAudioFormat;
        public FluentOutlet WithAudioFormat(AudioFileFormatEnum audioFormat) { _x.WithAudioFormat(audioFormat); return this; }
        public FluentOutlet AsWav() { _x.AsWav(); return this; }
        public FluentOutlet AsRaw() { _x.AsRaw(); return this; }
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
        public InterpolationTypeEnum GetInterpolation => _x.GetInterpolation;
        public FluentOutlet WithInterpolation(InterpolationTypeEnum interpolationEnum) { _x.WithInterpolation(interpolationEnum); return this; }
        public FluentOutlet WithLinear() { _x.WithLinear(); return this; }
        public FluentOutlet WithBlocky() { _x.WithBlocky(); return this; }
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
        public FluentOutlet WithCacheToDisk(bool? enabled = true) { _x.WithCacheToDisk(enabled); return this; }
        public bool MustCacheToDisk => _x.MustCacheToDisk;
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
        public FluentOutlet WithParallelEnabled(bool? parallelEnabled = default) { _x.WithParallelEnabled(parallelEnabled); return this; }
        private bool GetParallelEnabled => _x.GetParallelEnabled;
    }

    //// SynthWishes PreviewParallels

    //public partial class SynthWishes
    //{
    //    /// <inheritdoc cref="docs._withpreviewparallels" />
    //    public SynthWishes WithPreviewParallels(bool mustPreviewParallels = true)
    //    {
    //        WithPlayParallels(mustPreviewParallels);
    //        return this;
    //    }
    //}

    //// FluentOutlet PreviewParallels

    //public partial class FluentOutlet
    //{
    //    /// <inheritdoc cref="docs._withpreviewparallels" />
    //    public FluentOutlet WithPreviewParallels(bool mustPreviewParallels = true) { _x.WithPreviewParallels(mustPreviewParallels); return this; }
    //}

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
        public bool MustPlayParallels => _x.MustPlayParallels;
        /// <inheritdoc cref="docs._withpreviewparallels" />
        public FluentOutlet WithPlayParallels(bool mustPlayParallels = true) { _x.WithPlayParallels(mustPlayParallels); return this; }
    }

    //// SynthWishes SaveParallels

    //public partial class SynthWishes
    //{
    //    /// <inheritdoc cref="docs._withpreviewparallels" />
    //    public bool MustSaveParallels { get; private set; }

    //    /// <inheritdoc cref="docs._withpreviewparallels" />
    //    public SynthWishes WithSaveParallels(bool mustSaveParallels = true)
    //    {
    //        MustSaveParallels = mustSaveParallels;
    //        return this;
    //    }
    //}

    //// FluentOutlet SaveParallels

    //public partial class FluentOutlet
    //{
    //    /// <inheritdoc cref="docs._withpreviewparallels" />
    //    public bool MustSaveParallels => _x.MustSaveParallels;
    //    /// <inheritdoc cref="docs._withpreviewparallels" />
    //    public FluentOutlet WithSaveParallels(bool mustSaveParallels = true) { _x.WithSaveParallels(mustSaveParallels); return this; }
    //}
}