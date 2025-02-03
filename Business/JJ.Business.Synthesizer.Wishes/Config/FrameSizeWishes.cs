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
        public static int GetFrameSize(this SynthWishes obj) => ConfigWishes.GetFrameSize(obj);
        
        public static int FrameSize(this FlowNode obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this FlowNode obj) => ConfigWishes.GetFrameSize(obj);
        
        internal static int FrameSize(this ConfigResolver obj) => ConfigWishes.FrameSize(obj);
        internal static int GetFrameSize(this ConfigResolver obj) => ConfigWishes.GetFrameSize(obj);

        // Global-Bound

        internal static int? FrameSize(this ConfigSection obj) => ConfigWishes.FrameSize(obj);
        internal static int? GetFrameSize(this ConfigSection obj) => ConfigWishes.GetFrameSize(obj);

        // Tape-Bound

        public static int FrameSize(this Tape obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this Tape obj) => ConfigWishes.GetFrameSize(obj);
        
        public static int FrameSize(this TapeConfig obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this TapeConfig obj) => ConfigWishes.GetFrameSize(obj);
        
        public static int FrameSize(this TapeAction obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this TapeAction obj) => ConfigWishes.GetFrameSize(obj);
        
        public static int FrameSize(this TapeActions obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this TapeActions obj) => ConfigWishes.GetFrameSize(obj);

        // Buff-Bound

        public static int FrameSize(this Buff obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this Buff obj) => ConfigWishes.GetFrameSize(obj);
        
        public static int FrameSize(this AudioFileOutput obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this AudioFileOutput obj) => ConfigWishes.GetFrameSize(obj);

        // Independent after Taping

        public static int FrameSize(this Sample obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this Sample obj) => ConfigWishes.GetFrameSize(obj);
        
        public static int FrameSize(this AudioInfoWish obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this AudioInfoWish obj) => ConfigWishes.GetFrameSize(obj);
        
        public static int FrameSize(this AudioFileInfo obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this AudioFileInfo obj) => ConfigWishes.GetFrameSize(obj);

        // Immutable

        public static int FrameSize(this WavHeaderStruct obj) => ConfigWishes.FrameSize(obj);
        public static int GetFrameSize(this WavHeaderStruct obj) => ConfigWishes.GetFrameSize(obj);

        [Obsolete(ObsoleteMessage)]
        public static int FrameSize(this (SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities) => ConfigWishes.FrameSize(entities);
        [Obsolete(ObsoleteMessage)]
        public static int ToFrameSize(this (SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities) => ConfigWishes.ToFrameSize(entities);
        [Obsolete(ObsoleteMessage)]
        public static int GetFrameSize(this (SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities) => ConfigWishes.GetFrameSize(entities);

        [Obsolete(ObsoleteMessage)]
        public static int FrameSize(this (SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums) => ConfigWishes.FrameSize(enums);
        [Obsolete(ObsoleteMessage)]
        public static int ToFrameSize(this (SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums) => ConfigWishes.ToFrameSize(enums);
        [Obsolete(ObsoleteMessage)]
        public static int GetFrameSize(this (SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums) => ConfigWishes.GetFrameSize(enums);

        // Conversion Formula

        public static int FrameSize(this (int bits, int channels) tuple) => ConfigWishes.FrameSize(tuple);
        public static int FrameSize(this (int? bits, int? channels) tuple) => ConfigWishes.FrameSize(tuple);
        public static int ToFrameSize(this (int bits, int channels) tuple) => ConfigWishes.ToFrameSize(tuple);
        public static int ToFrameSize(this (int? bits, int? channels) tuple) => ConfigWishes.ToFrameSize(tuple);
        public static int GetFrameSize(this (int bits, int channels) tuple) => ConfigWishes.GetFrameSize(tuple);
        public static int GetFrameSize(this (int? bits, int? channels) tuple) => ConfigWishes.GetFrameSize(tuple);
    }

    public partial class ConfigWishes
    {
        // Synth-Bound

        public static int FrameSize(SynthWishes obj) => GetFrameSize(obj);
        public static int GetFrameSize(SynthWishes obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        public static int FrameSize(FlowNode obj) => GetFrameSize(obj);
        public static int GetFrameSize(FlowNode obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        internal static int FrameSize(ConfigResolver obj) => GetFrameSize(obj);
        internal static int GetFrameSize(ConfigResolver obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        // Global-Bound

        internal static int? FrameSize(ConfigSection obj) => GetFrameSize(obj);
        internal static int? GetFrameSize(ConfigSection obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        // Tape-Bound

        public static int FrameSize(Tape obj) => GetFrameSize(obj);
        public static int GetFrameSize(Tape obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        public static int FrameSize(TapeConfig obj) => GetFrameSize(obj);
        public static int GetFrameSize(TapeConfig obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        public static int FrameSize(TapeAction obj) => GetFrameSize(obj);
        public static int GetFrameSize(TapeAction obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        public static int FrameSize(TapeActions obj) => GetFrameSize(obj);
        public static int GetFrameSize(TapeActions obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        // Buff-Bound

        public static int FrameSize(Buff obj) => GetFrameSize(obj);
        public static int GetFrameSize(Buff obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        public static int FrameSize(AudioFileOutput obj) => GetFrameSize(obj);
        public static int GetFrameSize(AudioFileOutput obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        // Independent after Taping

        public static int FrameSize(Sample obj) => GetFrameSize(obj);
        public static int GetFrameSize(Sample obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        public static int FrameSize(AudioInfoWish obj) => GetFrameSize(obj);
        public static int GetFrameSize(AudioInfoWish obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        public static int FrameSize(AudioFileInfo obj) => GetFrameSize(obj);
        public static int GetFrameSize(AudioFileInfo obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        // Immutable

        public static int FrameSize(WavHeaderStruct obj) => GetFrameSize(obj);
        public static int GetFrameSize(WavHeaderStruct obj)
        {
            return GetFrameSize(obj.Bits(), obj.Channels());
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int FrameSize((SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities) => GetFrameSize(entities);
        [Obsolete(ObsoleteMessage)]
        public static int ToFrameSize((SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities) => GetFrameSize(entities);
        [Obsolete(ObsoleteMessage)]
        public static int GetFrameSize((SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities)
        {
            return GetFrameSize(entities.sampleDataType.Bits(), entities.speakerSetup.Channels());
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int FrameSize((SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums) => GetFrameSize(enums);
        [Obsolete(ObsoleteMessage)]
        public static int ToFrameSize((SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums) => GetFrameSize(enums);
        [Obsolete(ObsoleteMessage)]
        public static int GetFrameSize((SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums)
        {
            return GetFrameSize(enums.sampleDataTypeEnum.Bits(), enums.speakerSetupEnum.Channels());
        }
        
        // Conversion Formula

        public static int FrameSize((int bits, int channels) tuple) => GetFrameSize(tuple);
        public static int ToFrameSize((int bits, int channels) tuple) => GetFrameSize(tuple);
        public static int GetFrameSize((int bits, int channels) tuple)
        {
            return GetFrameSize(tuple.bits, tuple.channels);
        }
        
        public static int FrameSize((int? bits, int? channels) tuple) => GetFrameSize(tuple);
        public static int ToFrameSize((int? bits, int? channels) tuple) => GetFrameSize(tuple);
        public static int GetFrameSize((int? bits, int? channels) tuple)
        {
            return GetFrameSize(tuple.bits, tuple.channels);
        }
        
        public static int FrameSize(int bits, int channels) => GetFrameSize(bits, channels);
        public static int ToFrameSize(int bits, int channels) => GetFrameSize(bits, channels);
        public static int GetFrameSize(int bits, int channels)
        {
            return AssertBits(bits) / 8 * AssertChannels(channels);
        }
        
        public static int FrameSize(int? bits, int? channels) => GetFrameSize(bits, channels);
        public static int ToFrameSize(int? bits, int? channels) => GetFrameSize(bits, channels);
        public static int GetFrameSize(int? bits, int? channels)
        {
            return CoalesceBits(bits) / 8 * CoalesceChannels(channels);
        }
    }
}