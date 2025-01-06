using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
        
        #region FrameSize
        
        public static int FrameSize(this SynthWishes obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this FlowNode obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this ConfigWishes obj) => SizeOfBitDepth(obj) * Channels(obj);
        internal static int FrameSize(this ConfigSection obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Tape obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeConfig obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeAction obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeActions obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Buff obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Sample obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this AudioFileOutput obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this WavHeaderStruct obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this AudioInfoWish infoWish) => SizeOfBitDepth(infoWish) * Channels(infoWish);
        public static int FrameSize(this AudioFileInfo info) => SizeOfBitDepth(info) * Channels(info);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public static int FrameSize(this (SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities)
            => SizeOfBitDepth(entities.sampleDataType) * Channels(entities.speakerSetup);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public static int FrameSize(this (SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums)
            => SizeOfBitDepth(enums.sampleDataTypeEnum) * Channels(enums.speakerSetupEnum);
        
        #endregion
    }
}