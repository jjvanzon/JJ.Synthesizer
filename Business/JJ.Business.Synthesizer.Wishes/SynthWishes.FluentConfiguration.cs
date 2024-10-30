using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Name
        
        private readonly ThreadLocal<string> _name = new ThreadLocal<string>();

        public string Name
        {
            get => _name.Value;
            private set => _name.Value = value;
        }

        public SynthWishes WithName(string uglyName = null, string fallbackName = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = uglyName;
            
            if (string.IsNullOrWhiteSpace(name))
            {
                name = fallbackName;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = callerMemberName;
            }
            
            Name = name;
            return this;
        }

        /// <summary>
        /// Gets the name chose by the user with the WithName method and then resets it to null
        /// after it retrieves it. If nothing was in it, it uses the fallback name supplied.
        /// Also, if an explicitName is passed, it will override all the other options.
        /// </summary>
        private string FetchName(string fallbackName = null, string explicitName = null, [CallerMemberName] string callerMemberName = null)
        {
            if (!string.IsNullOrWhiteSpace(explicitName))
            {
                // Not sure if it should be prettified too...
                return explicitName;
            }

            string name = Name;
            Name = null;

            if (string.IsNullOrWhiteSpace(name))
            {
                name = fallbackName;
            }

            name = NameHelper.PrettifyName(name);
            return name;
        }

        // AudioLength

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
        
        public SynthWishes AddAudioLength(FluentOutlet addedLength)
        {
            return WithAudioLength(AudioLength + addedLength);
        }

        // Channel

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

        // SpeakerSetup

        public SpeakerSetupEnum SpeakerSetup { get; set; } = SpeakerSetupEnum.Mono;

        public SynthWishes WithSpeakerSetup(SpeakerSetupEnum speakerSetup)
        {
            SpeakerSetup = speakerSetup;
            return this;
        }

        public SynthWishes Mono() => WithSpeakerSetup(SpeakerSetupEnum.Mono);
        
        public SynthWishes Stereo() => WithSpeakerSetup(SpeakerSetupEnum.Stereo);
        
        // BitDepth
        
        public SampleDataTypeEnum BitDepth { get; set; } = SampleDataTypeEnum.Int16;

        public SynthWishes WithBitDepth(SampleDataTypeEnum bitDepth)
        {
            BitDepth = bitDepth;
            return this;
        }

        public SynthWishes _16Bit() => WithBitDepth(SampleDataTypeEnum.Int16);
        
        public SynthWishes _8Bit() => WithBitDepth(SampleDataTypeEnum.Byte);
        
        // AudioFormat
        
        public AudioFileFormatEnum AudioFormat { get; set; } = AudioFileFormatEnum.Wav;

        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat)
        {
            AudioFormat = audioFormat;
            return this;
        }

        public SynthWishes AsWav() => WithAudioFormat(AudioFileFormatEnum.Wav);
        
        public SynthWishes AsRaw() => WithAudioFormat(AudioFileFormatEnum.Raw);
        
        // Interpolation
        
        public InterpolationTypeEnum Interpolation { get; set; } = InterpolationTypeEnum.Line;

        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolationEnum)
        {
            Interpolation = interpolationEnum;
            return this;
        }

        public SynthWishes Linear() => WithInterpolation(InterpolationTypeEnum.Line);
        
        public SynthWishes Blocky() => WithInterpolation(InterpolationTypeEnum.Block);

        // SamplingRateOverride

        /// <inheritdoc cref="docs._samplingrateoverride"/>
        internal int? SamplingRateOverride { get; private set; }

        /// <inheritdoc cref="docs._samplingrateoverride"/>
        [UsedImplicitly]
        internal SynthWishes WithSamplingRateOverride(int? value)
        {
            SamplingRateOverride = value;
            return this;
        }
    }
}
