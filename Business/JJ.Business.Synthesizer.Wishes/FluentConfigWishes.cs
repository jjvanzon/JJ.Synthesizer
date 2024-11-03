using System.Threading;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes
{
    // Fluent Configuration
    
    // AudioLength SynthWishes

    public partial class SynthWishes
    {
        private FluentOutlet _audioLength;

        public FluentOutlet AudioLength
        {
            get => _audioLength ?? _[1];
            set => _audioLength = value ?? _[1];
        }

        public SynthWishes WithAudioLength(Outlet audioLength) => WithAudioLength(_[audioLength]);

        public SynthWishes WithAudioLength(double audioLength) => WithAudioLength(_[audioLength]);

        public SynthWishes WithAudioLength(FluentOutlet audioLength)
        {
            AudioLength = audioLength ?? _[1];
            return this;
        }

        public SynthWishes AddAudioLength(Outlet audioLength) => AddAudioLength(_[audioLength]);

        public SynthWishes AddAudioLength(double audioLength) => AddAudioLength(_[audioLength]);

        public SynthWishes AddAudioLength(FluentOutlet addedLength) => WithAudioLength(AudioLength + addedLength);
    }

    // AudioLength FluentOutlet 

    public partial class FluentOutlet
    {
        public FluentOutlet AudioLength { get => x.AudioLength; set => x.AudioLength = value; }
        public FluentOutlet WithAudioLength(FluentOutlet newLength) { x.WithAudioLength(newLength); return this; }
        public FluentOutlet AddAudioLength(FluentOutlet additionalLength) { x.AddAudioLength(additionalLength); return this; }
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
            set => Channel = value.ToChannelEnum(SpeakerSetup);
        }

        public SynthWishes WithChannel(ChannelEnum channel)
        {
            Channel = channel;
            return this;
        }

        public SynthWishes Left() => WithChannel(ChannelEnum.Left);

        public SynthWishes Right() => WithChannel(ChannelEnum.Right);

        public SynthWishes Center() => WithChannel(ChannelEnum.Single);
    }

    // Channel FluentOutlet

    public partial class FluentOutlet
    {
        public ChannelEnum Channel { get => x.Channel; set => x.Channel = value; }
        public int ChannelIndex { get => x.ChannelIndex; set => x.ChannelIndex = value; }
        public FluentOutlet WithChannel(ChannelEnum channel) { x.WithChannel(channel); return this; }
        public FluentOutlet Left()  { x.Left(); return this; }
        public FluentOutlet Right() { x.Right(); return this; }
        public FluentOutlet Center() { x.Center(); return this; }
    }
    
    // SamplingRate SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="_samplingrate" />
        public int SamplingRate { get; set; }

        /// <inheritdoc cref="_samplingrate" />
        public SynthWishes WithSamplingRate(int value)
        {
            SamplingRate = value;
            return this;
        }
    }

    // SamplingRate FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_samplingrate" />
        public int SamplingRate { get => x.SamplingRate; set => x.SamplingRate = value; }
        /// <inheritdoc cref="_samplingrate" />
        public FluentOutlet WithSamplingRate(int value) { x.SamplingRate = value; return this; }
    }
    
    // BitDepth SynthWishes

    public partial class SynthWishes
    {
        public SampleDataTypeEnum BitDepth { get; set; } = SampleDataTypeEnum.Int16;

        public SynthWishes WithBitDepth(SampleDataTypeEnum bitDepth)
        {
            BitDepth = bitDepth;
            return this;
        }

        public SynthWishes _16Bit() => WithBitDepth(SampleDataTypeEnum.Int16);

        public SynthWishes _8Bit() => WithBitDepth(SampleDataTypeEnum.Byte);
    }

    // BitDepth FluentOutlet

    public partial class FluentOutlet
    {
        public SampleDataTypeEnum BitDepth { get => x.BitDepth; set => x.BitDepth = value; }
        public FluentOutlet WithBitDepth(SampleDataTypeEnum bitDepth) { x.WithBitDepth(bitDepth); return this; }
        public FluentOutlet _16Bit() { x._16Bit(); return this; }
        public FluentOutlet _8Bit() { x._8Bit(); return this; }
    }

    // SpeakerSetup SynthWishes

    public partial class SynthWishes
    {
        public SpeakerSetupEnum SpeakerSetup { get; set; } = SpeakerSetupEnum.Mono;

        public SynthWishes WithSpeakerSetup(SpeakerSetupEnum speakerSetup)
        {
            SpeakerSetup = speakerSetup;
            return this;
        }

        public SynthWishes Mono() => WithSpeakerSetup(SpeakerSetupEnum.Mono);

        public SynthWishes Stereo() => WithSpeakerSetup(SpeakerSetupEnum.Stereo);
    }

    // SpeakerSetup FluentOutlet

    public partial class FluentOutlet
    {
        public SpeakerSetupEnum SpeakerSetup { get => x.SpeakerSetup; set => x.SpeakerSetup = value; }
        public FluentOutlet WithSpeakerSetup(SpeakerSetupEnum speakerSetup) { x.SpeakerSetup = speakerSetup; return this; }
        public FluentOutlet Mono() { x.Mono(); return this; }
        public FluentOutlet Stereo() { x.Stereo(); return this; }
    }

    // AudioFormat SynthWishes

    public partial class SynthWishes
    {
        public AudioFileFormatEnum AudioFormat { get; set; } = AudioFileFormatEnum.Wav;

        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat)
        {
            AudioFormat = audioFormat;
            return this;
        }

        public SynthWishes AsWav() => WithAudioFormat(AudioFileFormatEnum.Wav);

        public SynthWishes AsRaw() => WithAudioFormat(AudioFileFormatEnum.Raw);
    }

    // AudioFormat FluentOutlet

    public partial class FluentOutlet
    {
        public AudioFileFormatEnum AudioFormat { get => x.AudioFormat; set => x.AudioFormat = value; }
        public FluentOutlet WithAudioFormat(AudioFileFormatEnum audioFormat) { x.WithAudioFormat(audioFormat); return this; }
        public FluentOutlet AsWav() { x.AsWav(); return this; }
        public FluentOutlet AsRaw() { x.AsRaw(); return this; }
    }

    // Interpolation SynthWishes

    public partial class SynthWishes
    {
        public InterpolationTypeEnum Interpolation { get; set; } = InterpolationTypeEnum.Line;

        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolationEnum)
        {
            Interpolation = interpolationEnum;
            return this;
        }

        public SynthWishes Linear() => WithInterpolation(InterpolationTypeEnum.Line);

        public SynthWishes Blocky() => WithInterpolation(InterpolationTypeEnum.Block);
    }

    // Interpolation FluentOutlet

    public partial class FluentOutlet
    {
        public InterpolationTypeEnum Interpolation { get => x.Interpolation; set => x.Interpolation = value; }
        public FluentOutlet WithInterpolation(InterpolationTypeEnum interpolationEnum) { x.WithInterpolation(interpolationEnum); return this; }
        public FluentOutlet Linear() { x.Linear(); return this; }
        public FluentOutlet Blocky() { x.Blocky(); return this; }
    }

    // SynthWishes In-Memory Processing Enabled

    public partial class SynthWishes
    {
        private bool? _inMemoryProcessingEnabled;

        public SynthWishes WithInMemoryProcessing(bool? enabled = true)
        {
            _inMemoryProcessingEnabled = enabled;
            return this;
        }

        public bool InMemoryProcessingEnabled => _inMemoryProcessingEnabled ?? ConfigHelper.InMemoryProcessing;
    }

    // FluentOutlet In-Memory Processing Enabled

    public partial class FluentOutlet
    {
        public FluentOutlet WithInMemoryProcessing(bool? enabled = true) { x.WithInMemoryProcessing(enabled); return this; }
        public bool InMemoryProcessingEnabled => x.InMemoryProcessingEnabled;
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

        public bool ParallelEnabled => _parallelEnabled ?? ConfigHelper.ParallelEnabled;
    }

    // FluentOutlet ParallelEnabled

    public partial class FluentOutlet
    {
        public FluentOutlet WithParallelEnabled(bool? parallelEnabled = default) { x.WithParallelEnabled(parallelEnabled); return this; }
        private bool ParallelEnabled => x.ParallelEnabled;
    }

    // SynthWishes PreviewParallels

    public partial class SynthWishes
    {
        /// <inheritdoc cref="_withpreviewparallels" />
        public SynthWishes WithPreviewParallels(bool mustPreviewParallels = true)
        {
            WithPlayParallels(mustPreviewParallels);
            WithSaveParallels(mustPreviewParallels);
            return this;
        }
    }

    // FluentOutlet PreviewParallels

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_withpreviewparallels" />
        public FluentOutlet WithPreviewParallels(bool mustPreviewParallels = true) { x.WithPreviewParallels(mustPreviewParallels); return this; }
    }

    // SynthWishes PlayParallels

    public partial class SynthWishes
    {
        /// <inheritdoc cref="_withpreviewparallels" />
        public bool MustPlayParallels { get; private set; }

        /// <inheritdoc cref="_withpreviewparallels" />
        public SynthWishes WithPlayParallels(bool mustPlayParallels = true)
        {
            MustPlayParallels = mustPlayParallels;
            return this;
        }
    }

    // FluentOutlet PlayParallels

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_withpreviewparallels" />
        public bool MustPlayParallels => x.MustPlayParallels;

        /// <inheritdoc cref="_withpreviewparallels" />
        public FluentOutlet WithPlayParallels(bool mustPlayParallels = true) { x.WithPlayParallels(mustPlayParallels); return this; }
    }

    // SynthWishes SaveParallels

    public partial class SynthWishes
    {
        /// <inheritdoc cref="_withpreviewparallels" />
        public bool MustSaveParallels { get; private set; }

        /// <inheritdoc cref="_withpreviewparallels" />
        public SynthWishes WithSaveParallels(bool mustSaveParallels = true)
        {
            MustSaveParallels = mustSaveParallels;
            return this;
        }
    }

    // FluentOutlet SaveParallels

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_withpreviewparallels" />
        public bool MustSaveParallels { get; private set; }

        /// <inheritdoc cref="_withpreviewparallels" />
        public FluentOutlet WithSaveParallels(bool mustSaveParallels = true) { MustSaveParallels = mustSaveParallels; return this; }
    }
}