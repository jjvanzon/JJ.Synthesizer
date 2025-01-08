using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Derived Attribute
        
        public static int FrameSize(this SynthWishes obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this FlowNode obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this ConfigWishes obj) => obj.SizeOfBitDepth() * obj.Channels();
        internal static int FrameSize(this ConfigSection obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this Tape obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this TapeConfig obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this TapeAction obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this TapeActions obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this Buff obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this Sample obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this AudioFileOutput obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this WavHeaderStruct obj) => obj.SizeOfBitDepth() * obj.Channels();
        public static int FrameSize(this AudioInfoWish infoWish) => infoWish.SizeOfBitDepth() * infoWish.Channels();
        public static int FrameSize(this AudioFileInfo info) => info.SizeOfBitDepth() * info.Channels();
        
        [Obsolete(ObsoleteMessage)]
        public static int FrameSize(this (SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities)
            => entities.sampleDataType.SizeOfBitDepth() * entities.speakerSetup.Channels();
        
        [Obsolete(ObsoleteMessage)]
        public static int FrameSize(this (SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums)
            => enums.sampleDataTypeEnum.SizeOfBitDepth() * enums.speakerSetupEnum.Channels();
    }
}