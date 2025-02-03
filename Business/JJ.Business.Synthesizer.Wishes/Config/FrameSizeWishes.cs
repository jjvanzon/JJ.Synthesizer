using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // FrameSize: A Derived Attribute
        
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class FrameSizeExtensionWishes
    {
        // Synth-Bound

        public static int FrameSize(this SynthWishes obj) => ConfigWishes.FrameSize(obj);
        public static int FrameSize(this FlowNode obj) => ConfigWishes.FrameSize(obj);
        internal static int FrameSize(this ConfigResolver obj) => ConfigWishes.FrameSize(obj);

        // Global-Bound

        internal static int? FrameSize(this ConfigSection obj) => ConfigWishes.FrameSize(obj);

        // Tape-Bound

        public static int FrameSize(this Tape obj) => ConfigWishes.FrameSize(obj);
        public static int FrameSize(this TapeConfig obj) => ConfigWishes.FrameSize(obj);
        public static int FrameSize(this TapeAction obj) => ConfigWishes.FrameSize(obj);
        public static int FrameSize(this TapeActions obj) => ConfigWishes.FrameSize(obj);

        // Buff-Bound

        public static int FrameSize(this Buff obj) => ConfigWishes.FrameSize(obj);
        public static int FrameSize(this AudioFileOutput obj) => ConfigWishes.FrameSize(obj);

        // Independent after Taping

        public static int FrameSize(this Sample obj) => ConfigWishes.FrameSize(obj);
        public static int FrameSize(this AudioInfoWish obj) => ConfigWishes.FrameSize(obj);
        public static int FrameSize(this AudioFileInfo obj) => ConfigWishes.FrameSize(obj);

        // Immutable

        public static int FrameSize(this WavHeaderStruct obj) => ConfigWishes.FrameSize(obj);

        [Obsolete(ObsoleteMessage)]
        public static int FrameSize(this (SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities)
            => ConfigWishes.FrameSize(entities);

        [Obsolete(ObsoleteMessage)]
        public static int FrameSize(this (SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums)
            => ConfigWishes.FrameSize(enums);

        // Conversion Formula

        public static int FrameSize(this (int bits, int channels) tuple) => ConfigWishes.FrameSize(tuple);
        public static int FrameSize(this (int? bits, int? channels) tuple) => ConfigWishes.FrameSize(tuple);
    }

    public partial class ConfigWishes
    {
        // Synth-Bound

        public static int FrameSize(SynthWishes obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(FlowNode obj) => obj.SizeOfBitDepth() * obj.Channels();
        internal static int FrameSize(ConfigResolver obj) => obj.SizeOfBitDepth() * obj.Channels();

        // Global-Bound

        internal static int? FrameSize(ConfigSection obj) => obj.SizeOfBitDepth() * obj.Channels();

        // Tape-Bound

        public static int FrameSize(Tape obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(TapeConfig obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(TapeAction obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(TapeActions obj) => obj.SizeOfBitDepth() * obj.Channels();

        // Buff-Bound

        public static int FrameSize(Buff obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(AudioFileOutput obj) => obj.SizeOfBitDepth() * obj.Channels();

        // Independent after Taping

        public static int FrameSize(Sample obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(AudioInfoWish obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(AudioFileInfo obj) => obj.SizeOfBitDepth() * obj.Channels();

        // Immutable

        public static int FrameSize(WavHeaderStruct obj) => obj.SizeOfBitDepth() * obj.Channels();

        [Obsolete(ObsoleteMessage)]
        public static int FrameSize((SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities)
            => entities.sampleDataType.SizeOfBitDepth() * entities.speakerSetup.Channels();

        [Obsolete(ObsoleteMessage)]
        public static int FrameSize((SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums)
            => enums.sampleDataTypeEnum.SizeOfBitDepth() * enums.speakerSetupEnum.Channels();

        // Conversion Formula

        public static int FrameSize((int bits, int channels) tuple) => FrameSize(tuple.bits, tuple.channels);
        public static int FrameSize((int? bits, int? channels) tuple) => FrameSize(tuple.bits, tuple.channels);

        public static int FrameSize(int bits, int channels) => AssertBits(bits) / 8 * AssertChannels(channels);
        public static int FrameSize(int? bits, int? channels) => CoalesceBits(bits) / 8 * CoalesceChannels(channels);
    }
}