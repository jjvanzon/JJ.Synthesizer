using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedTypeParameter

#pragma warning disable CS0618

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class BitExtensionWishes
    {
        // A Primary Audio Attribute

        // Synth-Bound

        public static bool Is8Bit(this SynthWishes obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this SynthWishes obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this SynthWishes obj) => GetBits(obj) == 32;
        public static int Bits(this SynthWishes obj) => GetBits(obj);
        public static int GetBits(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        
        public static SynthWishes With8Bit(this SynthWishes obj) => SetBits(obj, 8);
        public static SynthWishes With16Bit(this SynthWishes obj) => SetBits(obj, 16);
        public static SynthWishes With32Bit(this SynthWishes obj) => SetBits(obj, 32);
        public static SynthWishes As8Bit(this SynthWishes obj) => SetBits(obj, 8);
        public static SynthWishes As16Bit(this SynthWishes obj) => SetBits(obj, 16);
        public static SynthWishes As32Bit(this SynthWishes obj) => SetBits(obj, 32);
        public static SynthWishes Set8Bit(this SynthWishes obj) => SetBits(obj, 8);
        public static SynthWishes Set16Bit(this SynthWishes obj) => SetBits(obj, 16);
        public static SynthWishes Set32Bit(this SynthWishes obj) => SetBits(obj, 32);
        public static SynthWishes Bits(this SynthWishes obj, int? value) => SetBits(obj, value);
        public static SynthWishes WithBits(this SynthWishes obj, int? value) => SetBits(obj, value);
        public static SynthWishes AsBits(this SynthWishes obj, int? value) => SetBits(obj, value);
        public static SynthWishes SetBits(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }
        
        public static bool Is8Bit(this FlowNode obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this FlowNode obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this FlowNode obj) => GetBits(obj) == 32;
        public static int Bits(this FlowNode obj) => GetBits(obj);
        public static int GetBits(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        
        public static FlowNode With8Bit(this FlowNode obj) => SetBits(obj, 8);
        public static FlowNode With16Bit(this FlowNode obj) => SetBits(obj, 16);
        public static FlowNode With32Bit(this FlowNode obj) => SetBits(obj, 32);
        public static FlowNode As8Bit(this FlowNode obj) => SetBits(obj, 8);
        public static FlowNode As16Bit(this FlowNode obj) => SetBits(obj, 16);
        public static FlowNode As32Bit(this FlowNode obj) => SetBits(obj, 32);
        public static FlowNode Set8Bit(this FlowNode obj) => SetBits(obj, 8);
        public static FlowNode Set16Bit(this FlowNode obj) => SetBits(obj, 16);
        public static FlowNode Set32Bit(this FlowNode obj) => SetBits(obj, 32);
        public static FlowNode Bits(this FlowNode obj, int? value) => SetBits(obj, value);
        public static FlowNode WithBits(this FlowNode obj, int? value) => SetBits(obj, value);
        public static FlowNode AsBits(this FlowNode obj, int? value) => SetBits(obj, value);
        public static FlowNode SetBits(this FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }

        [UsedImplicitly] internal static bool Is8Bit(this ConfigResolver obj) => GetBits(obj) == 8;
        [UsedImplicitly] internal static bool Is16Bit(this ConfigResolver obj) => GetBits(obj) == 16;
        [UsedImplicitly] internal static bool Is32Bit(this ConfigResolver obj) => GetBits(obj) == 32;
        internal static int Bits(this ConfigResolver obj) => GetBits(obj);
        internal static int GetBits(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        
        [UsedImplicitly] internal static ConfigResolver With8Bit(this ConfigResolver obj) => SetBits(obj, 8);
        [UsedImplicitly] internal static ConfigResolver With16Bit(this ConfigResolver obj) => SetBits(obj, 16);
        [UsedImplicitly] internal static ConfigResolver With32Bit(this ConfigResolver obj) => SetBits(obj, 32);
        [UsedImplicitly] internal static ConfigResolver As8Bit(this ConfigResolver obj) => SetBits(obj, 8);
        [UsedImplicitly] internal static ConfigResolver As16Bit(this ConfigResolver obj) => SetBits(obj, 16);
        [UsedImplicitly] internal static ConfigResolver As32Bit(this ConfigResolver obj) => SetBits(obj, 32);
        [UsedImplicitly] internal static ConfigResolver Set8Bit(this ConfigResolver obj) => SetBits(obj, 8);
        [UsedImplicitly] internal static ConfigResolver Set16Bit(this ConfigResolver obj) => SetBits(obj, 16);
        [UsedImplicitly] internal static ConfigResolver Set32Bit(this ConfigResolver obj) => SetBits(obj, 32);
        internal static ConfigResolver Bits(this ConfigResolver obj, int? value) => SetBits(obj, value);
        internal static ConfigResolver WithBits(this ConfigResolver obj, int? value) => SetBits(obj, value);
        internal static ConfigResolver AsBits(this ConfigResolver obj, int? value) => SetBits(obj, value);
        internal static ConfigResolver SetBits(this ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }

        // Global-Bound

        [UsedImplicitly] internal static bool Is8Bit(this ConfigSection obj) => GetBits(obj) == 8;
        [UsedImplicitly] internal static bool Is16Bit(this ConfigSection obj) => GetBits(obj) == 16;
        [UsedImplicitly] internal static bool Is32Bit(this ConfigSection obj) => GetBits(obj) == 32;
        internal static int? Bits(this ConfigSection obj) => GetBits(obj);
        internal static int? GetBits(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        // Tape-Bound
        
        public static bool Is8Bit(this Tape obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this Tape obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this Tape obj) => GetBits(obj) == 32;
        public static int Bits(this Tape obj) => GetBits(obj);
        public static int GetBits(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Bits;
        }
        
        public static Tape With8Bit(this Tape obj) => SetBits(obj, 8);
        public static Tape With16Bit(this Tape obj) => SetBits(obj, 16);
        public static Tape With32Bit(this Tape obj) => SetBits(obj, 32);
        public static Tape As8Bit(this Tape obj) => SetBits(obj, 8);
        public static Tape As16Bit(this Tape obj) => SetBits(obj, 16);
        public static Tape As32Bit(this Tape obj) => SetBits(obj, 32);
        public static Tape Set8Bit(this Tape obj) => SetBits(obj, 8);
        public static Tape Set16Bit(this Tape obj) => SetBits(obj, 16);
        public static Tape Set32Bit(this Tape obj) => SetBits(obj, 32);
        public static Tape Bits(this Tape obj, int value) => SetBits(obj, value);
        public static Tape WithBits(this Tape obj, int value) => SetBits(obj, value);
        public static Tape AsBits(this Tape obj, int value) => SetBits(obj, value);
        public static Tape SetBits(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Bits = value;
            return obj;
        }
        
        public static bool Is8Bit(this TapeConfig obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this TapeConfig obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this TapeConfig obj) => GetBits(obj) == 32;
        public static int Bits(this TapeConfig obj) => GetBits(obj);
        public static int GetBits(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        public static TapeConfig With8Bit(this TapeConfig obj) => SetBits(obj, 8);
        public static TapeConfig With16Bit(this TapeConfig obj) => SetBits(obj, 16);
        public static TapeConfig With32Bit(this TapeConfig obj) => SetBits(obj, 32);
        public static TapeConfig As8Bit(this TapeConfig obj) => SetBits(obj, 8);
        public static TapeConfig As16Bit(this TapeConfig obj) => SetBits(obj, 16);
        public static TapeConfig As32Bit(this TapeConfig obj) => SetBits(obj, 32);
        public static TapeConfig Set8Bit(this TapeConfig obj) => SetBits(obj, 8);
        public static TapeConfig Set16Bit(this TapeConfig obj) => SetBits(obj, 16);
        public static TapeConfig Set32Bit(this TapeConfig obj) => SetBits(obj, 32);
        public static TapeConfig Bits(this TapeConfig obj, int value) => SetBits(obj, value);
        public static TapeConfig WithBits(this TapeConfig obj, int value) => SetBits(obj, value);
        public static TapeConfig AsBits(this TapeConfig obj, int value) => SetBits(obj, value);
        public static TapeConfig SetBits(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Bits = value;
            return obj;
        }
        
        public static bool Is8Bit(this TapeActions obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this TapeActions obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this TapeActions obj) => GetBits(obj) == 32;
        public static int Bits(this TapeActions obj) => GetBits(obj);
        public static int GetBits(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Bits;
        }
        
        public static TapeActions With8Bit(this TapeActions obj) => SetBits(obj, 8);
        public static TapeActions With16Bit(this TapeActions obj) => SetBits(obj, 16);
        public static TapeActions With32Bit(this TapeActions obj) => SetBits(obj, 32);
        public static TapeActions As8Bit(this TapeActions obj) => SetBits(obj, 8);
        public static TapeActions As16Bit(this TapeActions obj) => SetBits(obj, 16);
        public static TapeActions As32Bit(this TapeActions obj) => SetBits(obj, 32);
        public static TapeActions Set8Bit(this TapeActions obj) => SetBits(obj, 8);
        public static TapeActions Set16Bit(this TapeActions obj) => SetBits(obj, 16);
        public static TapeActions Set32Bit(this TapeActions obj) => SetBits(obj, 32);
        public static TapeActions Bits(this TapeActions obj, int value) => SetBits(obj, value);
        public static TapeActions WithBits(this TapeActions obj, int value) => SetBits(obj, value);
        public static TapeActions AsBits(this TapeActions obj, int value) => SetBits(obj, value);
        public static TapeActions SetBits(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Bits = value;
            return obj;
        }
        
        public static bool Is8Bit(this TapeAction obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this TapeAction obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this TapeAction obj) => GetBits(obj) == 32;
        public static int Bits(this TapeAction obj) => GetBits(obj);
        public static int GetBits(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Bits;
        }
        
        public static TapeAction With8Bit(this TapeAction obj) => SetBits(obj, 8);
        public static TapeAction With16Bit(this TapeAction obj) => SetBits(obj, 16);
        public static TapeAction With32Bit(this TapeAction obj) => SetBits(obj, 32);
        public static TapeAction As8Bit(this TapeAction obj) => SetBits(obj, 8);
        public static TapeAction As16Bit(this TapeAction obj) => SetBits(obj, 16);
        public static TapeAction As32Bit(this TapeAction obj) => SetBits(obj, 32);
        public static TapeAction Set8Bit(this TapeAction obj) => SetBits(obj, 8);
        public static TapeAction Set16Bit(this TapeAction obj) => SetBits(obj, 16);
        public static TapeAction Set32Bit(this TapeAction obj) => SetBits(obj, 32);
        public static TapeAction Bits(this TapeAction obj, int value) => SetBits(obj, value);
        public static TapeAction WithBits(this TapeAction obj, int value) => SetBits(obj, value);
        public static TapeAction AsBits(this TapeAction obj, int value) => SetBits(obj, value);
        public static TapeAction SetBits(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Bits = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static bool Is8Bit(this Buff obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this Buff obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this Buff obj) => GetBits(obj) == 32;
        public static int Bits(this Buff obj) => GetBits(obj);
        public static int GetBits(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.UnderlyingAudioFileOutput.Bits();
        }
        
        public static Buff With8Bit(this Buff obj, IContext context) => SetBits(obj, 8, context);
        public static Buff With16Bit(this Buff obj, IContext context) => SetBits(obj, 16, context);
        public static Buff With32Bit(this Buff obj, IContext context) => SetBits(obj, 32, context);
        public static Buff As8Bit(this Buff obj, IContext context) => SetBits(obj, 8, context);
        public static Buff As16Bit(this Buff obj, IContext context) => SetBits(obj, 16, context);
        public static Buff As32Bit(this Buff obj, IContext context) => SetBits(obj, 32, context);
        public static Buff Set8Bit(this Buff obj, IContext context) => SetBits(obj, 8, context);
        public static Buff Set16Bit(this Buff obj, IContext context) => SetBits(obj, 16, context);
        public static Buff Set32Bit(this Buff obj, IContext context) => SetBits(obj, 32, context);
        public static Buff Bits(this Buff obj, int value, IContext context) => SetBits(obj, value, context);
        public static Buff WithBits(this Buff obj, int value, IContext context) => SetBits(obj, value, context);
        public static Buff AsBits(this Buff obj, int value, IContext context) => SetBits(obj, value, context);
        public static Buff SetBits(this Buff obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.Bits(value, context);
            return obj;
        }
        
        public static bool Is8Bit(this AudioFileOutput obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this AudioFileOutput obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this AudioFileOutput obj) => GetBits(obj) == 32;
        public static int Bits(this AudioFileOutput obj) => GetBits(obj);
        public static int GetBits(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSampleDataTypeEnum().EnumToBits();
        }
        
        public static AudioFileOutput With8Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 8, context);
        public static AudioFileOutput With16Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 16, context);
        public static AudioFileOutput With32Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 32, context);
        public static AudioFileOutput As8Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 8, context);
        public static AudioFileOutput As16Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 16, context);
        public static AudioFileOutput As32Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 32, context);
        public static AudioFileOutput Set8Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 8, context);
        public static AudioFileOutput Set16Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 16, context);
        public static AudioFileOutput Set32Bit(this AudioFileOutput obj, IContext context) => SetBits(obj, 32, context);
        public static AudioFileOutput Bits(this AudioFileOutput obj, int value, IContext context) => SetBits(obj, value, context);
        public static AudioFileOutput AsBits(this AudioFileOutput obj, int value, IContext context) => SetBits(obj, value, context);
        public static AudioFileOutput WithBits(this AudioFileOutput obj, int value, IContext context) => SetBits(obj, value, context);
        public static AudioFileOutput SetBits(this AudioFileOutput obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return obj;
        }
        
        // Independent after Taping
        
        public static bool Is8Bit(this Sample obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this Sample obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this Sample obj) => GetBits(obj) == 32;
        public static int Bits(this Sample obj) => GetBits(obj);
        public static int GetBits(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSampleDataTypeEnum().EnumToBits();
        }
        
        public static Sample With8Bit(this Sample obj, IContext context) => SetBits(obj, 8, context);
        public static Sample With16Bit(this Sample obj, IContext context) => SetBits(obj, 16, context);
        public static Sample With32Bit(this Sample obj, IContext context) => SetBits(obj, 32, context);
        public static Sample As8Bit(this Sample obj, IContext context) => SetBits(obj, 8, context);
        public static Sample As16Bit(this Sample obj, IContext context) => SetBits(obj, 16, context);
        public static Sample As32Bit(this Sample obj, IContext context) => SetBits(obj, 32, context);
        public static Sample Set8Bit(this Sample obj, IContext context) => SetBits(obj, 8, context);
        public static Sample Set16Bit(this Sample obj, IContext context) => SetBits(obj, 16, context);
        public static Sample Set32Bit(this Sample obj, IContext context) => SetBits(obj, 32, context);
        public static Sample Bits(this Sample obj, int value, IContext context) => SetBits(obj, value, context);
        public static Sample WithBits(this Sample obj, int value, IContext context) => SetBits(obj, value, context);
        public static Sample AsBits(this Sample obj, int value, IContext context) => SetBits(obj, value, context);
        public static Sample SetBits(this Sample obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return obj;
        }
        
        public static bool Is8Bit(this AudioInfoWish obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this AudioInfoWish obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this AudioInfoWish obj) => GetBits(obj) == 32;
        public static int Bits(this AudioInfoWish obj) => GetBits(obj);
        public static int GetBits(this AudioInfoWish obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        public static AudioInfoWish With8Bit(this AudioInfoWish obj) => SetBits(obj, 8);
        public static AudioInfoWish With16Bit(this AudioInfoWish obj) => SetBits(obj, 16);
        public static AudioInfoWish With32Bit(this AudioInfoWish obj) => SetBits(obj, 32);
        public static AudioInfoWish As8Bit(this AudioInfoWish obj) => SetBits(obj, 8);
        public static AudioInfoWish As16Bit(this AudioInfoWish obj) => SetBits(obj, 16);
        public static AudioInfoWish As32Bit(this AudioInfoWish obj) => SetBits(obj, 32);
        public static AudioInfoWish Set8Bit(this AudioInfoWish obj) => SetBits(obj, 8);
        public static AudioInfoWish Set16Bit(this AudioInfoWish obj) => SetBits(obj, 16);
        public static AudioInfoWish Set32Bit(this AudioInfoWish obj) => SetBits(obj, 32);
        public static AudioInfoWish Bits(this AudioInfoWish obj, int value) => SetBits(obj, value);
        public static AudioInfoWish WithBits(this AudioInfoWish obj, int value) => SetBits(obj, value);
        public static AudioInfoWish AsBits(this AudioInfoWish obj, int value) => SetBits(obj, value);
        public static AudioInfoWish SetBits(this AudioInfoWish obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Bits = AssertBits(value, strict: false);
            return obj;
        }
        
        public static bool Is8Bit(this AudioFileInfo obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this AudioFileInfo obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this AudioFileInfo obj) => GetBits(obj) == 32;
        public static int Bits(this AudioFileInfo obj) => GetBits(obj);
        public static int GetBits(this AudioFileInfo obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.BytesPerValue.Bits();
        }
        
        public static AudioFileInfo With8Bit(this AudioFileInfo obj) => SetBits(obj, 8);
        public static AudioFileInfo With16Bit(this AudioFileInfo obj) => SetBits(obj, 16);
        public static AudioFileInfo With32Bit(this AudioFileInfo obj) => SetBits(obj, 32);
        public static AudioFileInfo As8Bit(this AudioFileInfo obj) => SetBits(obj, 8);
        public static AudioFileInfo As16Bit(this AudioFileInfo obj) => SetBits(obj, 16);
        public static AudioFileInfo As32Bit(this AudioFileInfo obj) => SetBits(obj, 32);
        public static AudioFileInfo Set8Bit(this AudioFileInfo obj) => SetBits(obj, 8);
        public static AudioFileInfo Set16Bit(this AudioFileInfo obj) => SetBits(obj, 16);
        public static AudioFileInfo Set32Bit(this AudioFileInfo obj) => SetBits(obj, 32);
        public static AudioFileInfo Bits(this AudioFileInfo obj, int bits) => SetBits(obj, bits);
        public static AudioFileInfo WithBits(this AudioFileInfo obj, int bits) => SetBits(obj, bits);
        public static AudioFileInfo AsBits(this AudioFileInfo obj, int bits) => SetBits(obj, bits);
        public static AudioFileInfo SetBits(this AudioFileInfo obj, int bits)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.BytesPerValue = bits.SizeOfBitDepth();
            return obj;
        }

        // Immutable        
        
        public static bool Is8Bit(this WavHeaderStruct obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this WavHeaderStruct obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this WavHeaderStruct obj) => GetBits(obj) == 32;
        public static int Bits(this WavHeaderStruct obj) => GetBits(obj);
        public static int GetBits(this WavHeaderStruct obj)
        {
            return obj.BitsPerValue;
        }
        
        public static WavHeaderStruct With8Bit(this WavHeaderStruct obj) => SetBits(obj, 8);
        public static WavHeaderStruct With16Bit(this WavHeaderStruct obj) => SetBits(obj, 16);
        public static WavHeaderStruct With32Bit(this WavHeaderStruct obj) => SetBits(obj, 32);
        public static WavHeaderStruct As8Bit(this WavHeaderStruct obj) => SetBits(obj, 8);
        public static WavHeaderStruct As16Bit(this WavHeaderStruct obj) => SetBits(obj, 16);
        public static WavHeaderStruct As32Bit(this WavHeaderStruct obj) => SetBits(obj, 32);
        public static WavHeaderStruct Set8Bit(this WavHeaderStruct obj) => SetBits(obj, 8);
        public static WavHeaderStruct Set16Bit(this WavHeaderStruct obj) => SetBits(obj, 16);
        public static WavHeaderStruct Set32Bit(this WavHeaderStruct obj) => SetBits(obj, 32);
        public static WavHeaderStruct Bits(this WavHeaderStruct obj, int value) => SetBits(obj, value);
        public static WavHeaderStruct WithBits(this WavHeaderStruct obj, int value) => SetBits(obj, value);
        public static WavHeaderStruct AsBits(this WavHeaderStruct obj, int value) => SetBits(obj, value);
        public static WavHeaderStruct SetBits(this WavHeaderStruct obj, int value)
        {
            return obj.ToWish().Bits(value).ToWavHeader();
        }

        [Obsolete(ObsoleteMessage)] public static bool Is8Bit(this SampleDataTypeEnum obj) => GetBits(obj) == 8;
        [Obsolete(ObsoleteMessage)] public static bool Is16Bit(this SampleDataTypeEnum obj) => GetBits(obj) == 16;
        [Obsolete(ObsoleteMessage)] public static bool Is32Bit(this SampleDataTypeEnum obj) => GetBits(obj) == 32;
        [Obsolete(ObsoleteMessage)] public static int Bits(this SampleDataTypeEnum obj) => GetBits(obj);
        [Obsolete(ObsoleteMessage)] public static int GetBits(this SampleDataTypeEnum obj)
        {
            return EnumToBits(obj);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With8Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With16Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With32Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As8Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As16Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As32Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set8Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set16Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set32Bit(this SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Bits(this SampleDataTypeEnum oldEnumValue, int newBits) => SetBits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum WithBits(this SampleDataTypeEnum oldEnumValue, int newBits) => SetBits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum AsBits(this SampleDataTypeEnum oldEnumValue, int newBits) => SetBits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SetBits(this SampleDataTypeEnum oldEnumValue, int newBits)
        {
            return newBits.BitsToEnum();
        }

        [Obsolete(ObsoleteMessage)] public static bool Is8Bit(this SampleDataType obj) => GetBits(obj) == 8;
        [Obsolete(ObsoleteMessage)] public static bool Is16Bit(this SampleDataType obj) => GetBits(obj) == 16;
        [Obsolete(ObsoleteMessage)] public static bool Is32Bit(this SampleDataType obj) => GetBits(obj) == 32;
        [Obsolete(ObsoleteMessage)] public static int Bits(this SampleDataType obj) => GetBits(obj);
        [Obsolete(ObsoleteMessage)] public static int GetBits(this SampleDataType obj)
        {
            return obj.EntityToBits();
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With8Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 8, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With16Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 16, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With32Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 32, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As8Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 8, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As16Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 16, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As32Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 32, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set8Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 8, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set16Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 16, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set32Bit(this SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 32, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Bits(this SampleDataType oldSampleDataType, int newBits, IContext context) => SetBits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType WithBits(this SampleDataType oldSampleDataType, int newBits, IContext context) => SetBits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType AsBits(this SampleDataType oldSampleDataType, int newBits, IContext context) => SetBits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType SetBits(this SampleDataType oldSampleDataType, int newBits, IContext context)
        {
            return newBits.BitsToEntity(context);
        }
        
        public static bool Is8Bit(this Type obj) => GetBits(obj) == 8;
        public static bool Is16Bit(this Type obj) => GetBits(obj) == 16;
        public static bool Is32Bit(this Type obj) => GetBits(obj) == 32;
        public static int Bits(this Type valueType) => GetBits(valueType);
        public static int GetBits(this Type valueType)
        {
            return TypeToBits(valueType);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With8Bit(this Type oldValueType) => SetBits(oldValueType, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With16Bit(this Type oldValueType) => SetBits(oldValueType, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With32Bit(this Type oldValueType) => SetBits(oldValueType, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As8Bit(this Type oldValueType) => SetBits(oldValueType, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As16Bit(this Type oldValueType) => SetBits(oldValueType, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As32Bit(this Type oldValueType) => SetBits(oldValueType, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set8Bit(this Type oldValueType) => SetBits(oldValueType, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set16Bit(this Type oldValueType) => SetBits(oldValueType, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set32Bit(this Type oldValueType) => SetBits(oldValueType, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Bits(this Type oldValueType, int newBits) => SetBits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type WithBits(this Type oldValueType, int newBits) => SetBits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type AsBits(this Type oldValueType, int newBits) => SetBits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SetBits(this Type oldValueType, int newBits)
        {
            return newBits.BitsToType();
        }

        // Conversion-Style
        
        public static int TypeToBits(this Type obj) => ConfigWishes.TypeToBits(obj);
        public static Type BitsToType(this int value) => ConfigWishes.BitsToType(value);
        
        [Obsolete(ObsoleteMessage)] 
        public static int EnumToBits(this SampleDataTypeEnum obj) => ConfigWishes.EnumToBits(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static SampleDataTypeEnum BitsToEnum(this int bits) => ConfigWishes.BitsToEnum(bits);
        
        [Obsolete(ObsoleteMessage)]
        public static int EntityToBits(this SampleDataType obj) => ConfigWishes.EntityToBits(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static SampleDataType BitsToEntity(this int bits, IContext context) => ConfigWishes.BitsToEntity(bits, context);
        
        // Synonyms

        public static int ToBits(this Type obj) => ConfigWishes.ToBits(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static int ToBits(this SampleDataTypeEnum obj) => ConfigWishes.ToBits(obj);
        
        [Obsolete(ObsoleteMessage)]
        public static int ToBits(this SampleDataType obj) => ConfigWishes.ToBits(obj);
    }

    public partial class ConfigWishes
    {
        // With Type Arguments
        
        public static int TypeToBits<T>() => typeof(T).TypeToBits();
        public static int Bits<TValueType>() => TypeToBits<TValueType>();
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Bits<TValueType>(int value) => value.BitsToType();

        public static bool Is8Bit <TValue> () => Bits<TValue>() == 8;
        public static bool Is16Bit<TValue> () => Bits<TValue>() == 16;
        public static bool Is32Bit<TValue> () => Bits<TValue>() == 32;

        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With8Bit<TValue>() => Bits<TValue>(8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With16Bit<TValue>() => Bits<TValue>(16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With32Bit<TValue>() => Bits<TValue>(32);

        // Conversion-Style

        public static int TypeToBits(Type obj)
        {
            if (obj == typeof(byte)) return 8;
            if (obj == typeof(Int16)) return 16;
            if (obj == typeof(float)) return 32;
            throw new ValueNotSupportedException(obj);
        }
        
        public static Type BitsToType(int value)
        {
            switch (AssertBits(value, strict: false))
            {
                case 8 : return typeof(byte);
                case 16: return typeof(Int16);
                case 32: return typeof(float);
                default: return default; // ncrunch: no coverage
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int EnumToBits(SampleDataTypeEnum obj)
        {
            switch (obj)
            {
                case SampleDataTypeEnum.Byte: return 8;
                case SampleDataTypeEnum.Int16: return 16;
                case SampleDataTypeEnum.Float32: return 32;
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum BitsToEnum(int bits)
        {
            switch (bits)
            {
                case 32: return SampleDataTypeEnum.Float32;
                case 16: return SampleDataTypeEnum.Int16;
                case 8: return SampleDataTypeEnum.Byte;
            }
            
            AssertBits(bits, strict: false); return default;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static int EntityToBits(SampleDataType obj) => obj.ToEnum().EnumToBits();
        
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType BitsToEntity(int bits, IContext context) => bits.BitsToEnum().ToEntity(context);
        
        // Synonyms
        
        public static int ToBits(Type obj) => TypeToBits(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static int ToBits(SampleDataTypeEnum obj) => EnumToBits(obj);
        
        [Obsolete(ObsoleteMessage)]
        public static int ToBits(SampleDataType obj) => EntityToBits(obj);
   }
}