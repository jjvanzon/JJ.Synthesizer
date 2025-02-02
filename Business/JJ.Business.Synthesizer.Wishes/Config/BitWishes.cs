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
        
        public static bool Is8Bit(this  SynthWishes obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this SynthWishes obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this SynthWishes obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    SynthWishes obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this SynthWishes obj) => ConfigWishes.GetBits(obj);
        
        public static SynthWishes With8Bit(this  SynthWishes obj)             => ConfigWishes.With8Bit(obj);
        public static SynthWishes With16Bit(this SynthWishes obj)             => ConfigWishes.With16Bit(obj);
        public static SynthWishes With32Bit(this SynthWishes obj)             => ConfigWishes.With32Bit(obj);
        public static SynthWishes As8Bit(this    SynthWishes obj)             => ConfigWishes.As8Bit(obj);
        public static SynthWishes As16Bit(this   SynthWishes obj)             => ConfigWishes.As16Bit(obj);
        public static SynthWishes As32Bit(this   SynthWishes obj)             => ConfigWishes.As32Bit(obj);
        public static SynthWishes Set8Bit(this   SynthWishes obj)             => ConfigWishes.Set8Bit(obj);
        public static SynthWishes Set16Bit(this  SynthWishes obj)             => ConfigWishes.Set16Bit(obj);
        public static SynthWishes Set32Bit(this  SynthWishes obj)             => ConfigWishes.Set32Bit(obj);
        public static SynthWishes Bits(this      SynthWishes obj, int? value) => ConfigWishes.Bits(obj, value);
        public static SynthWishes WithBits(this  SynthWishes obj, int? value) => ConfigWishes.WithBits(obj, value);
        public static SynthWishes AsBits(this    SynthWishes obj, int? value) => ConfigWishes.AsBits(obj, value);
        public static SynthWishes SetBits(this   SynthWishes obj, int? value) => ConfigWishes.SetBits(obj, value);
        
        public static bool Is8Bit(this  FlowNode obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this FlowNode obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this FlowNode obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    FlowNode obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this FlowNode obj) => ConfigWishes.GetBits(obj);
        
        public static FlowNode With8Bit(this  FlowNode obj)             => ConfigWishes.With8Bit(obj);
        public static FlowNode With16Bit(this FlowNode obj)             => ConfigWishes.With16Bit(obj);
        public static FlowNode With32Bit(this FlowNode obj)             => ConfigWishes.With32Bit(obj);
        public static FlowNode As8Bit(this    FlowNode obj)             => ConfigWishes.As8Bit(obj);
        public static FlowNode As16Bit(this   FlowNode obj)             => ConfigWishes.As16Bit(obj);
        public static FlowNode As32Bit(this   FlowNode obj)             => ConfigWishes.As32Bit(obj);
        public static FlowNode Set8Bit(this   FlowNode obj)             => ConfigWishes.Set8Bit(obj);
        public static FlowNode Set16Bit(this  FlowNode obj)             => ConfigWishes.Set16Bit(obj);
        public static FlowNode Set32Bit(this  FlowNode obj)             => ConfigWishes.Set32Bit(obj);
        public static FlowNode Bits(this      FlowNode obj, int? value) => ConfigWishes.Bits(obj, value);
        public static FlowNode WithBits(this  FlowNode obj, int? value) => ConfigWishes.WithBits(obj, value);
        public static FlowNode AsBits(this    FlowNode obj, int? value) => ConfigWishes.AsBits(obj, value);
        public static FlowNode SetBits(this   FlowNode obj, int? value) => ConfigWishes.SetBits(obj, value);

        [UsedImplicitly] internal static bool Is8Bit(this ConfigResolver obj)  => ConfigWishes.Is8Bit(obj);
        [UsedImplicitly] internal static bool Is16Bit(this ConfigResolver obj) => ConfigWishes.Is16Bit(obj);
        [UsedImplicitly] internal static bool Is32Bit(this ConfigResolver obj) => ConfigWishes.Is32Bit(obj);
        internal static int Bits(this ConfigResolver obj) => ConfigWishes.Bits(obj);
        internal static int GetBits(this ConfigResolver obj) => ConfigWishes.GetBits(obj);

        [UsedImplicitly] internal static ConfigResolver With8Bit(this ConfigResolver obj)  => ConfigWishes.With8Bit(obj);
        [UsedImplicitly] internal static ConfigResolver With16Bit(this ConfigResolver obj) => ConfigWishes.With16Bit(obj);
        [UsedImplicitly] internal static ConfigResolver With32Bit(this ConfigResolver obj) => ConfigWishes.With32Bit(obj);
        [UsedImplicitly] internal static ConfigResolver As8Bit(this ConfigResolver obj)    => ConfigWishes.As8Bit(obj);
        [UsedImplicitly] internal static ConfigResolver As16Bit(this ConfigResolver obj)   => ConfigWishes.As16Bit(obj);
        [UsedImplicitly] internal static ConfigResolver As32Bit(this ConfigResolver obj)   => ConfigWishes.As32Bit(obj);
        [UsedImplicitly] internal static ConfigResolver Set8Bit(this ConfigResolver obj)   => ConfigWishes.Set8Bit(obj);
        [UsedImplicitly] internal static ConfigResolver Set16Bit(this ConfigResolver obj)  => ConfigWishes.Set16Bit(obj);
        [UsedImplicitly] internal static ConfigResolver Set32Bit(this ConfigResolver obj)  => ConfigWishes.Set32Bit(obj);
        internal static ConfigResolver Bits(this ConfigResolver obj, int? value)     => ConfigWishes.Bits(obj, value);
        internal static ConfigResolver WithBits(this ConfigResolver obj, int? value) => ConfigWishes.WithBits(obj, value);
        internal static ConfigResolver AsBits(this ConfigResolver obj, int? value)   => ConfigWishes.AsBits(obj, value);
        internal static ConfigResolver SetBits(this ConfigResolver obj, int? value)  => ConfigWishes.SetBits(obj, value);

        // Global-Bound

        [UsedImplicitly] internal static bool Is8Bit(this ConfigSection obj)  => ConfigWishes.Is8Bit(obj);
        [UsedImplicitly] internal static bool Is16Bit(this ConfigSection obj) => ConfigWishes.Is16Bit(obj);
        [UsedImplicitly] internal static bool Is32Bit(this ConfigSection obj) => ConfigWishes.Is32Bit(obj);
        internal static int? Bits(this ConfigSection obj) => ConfigWishes.Bits(obj);
        internal static int? GetBits(this ConfigSection obj) => ConfigWishes.GetBits(obj);

        // Tape-Bound
        
        public static bool Is8Bit(this  Tape obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this Tape obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this Tape obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    Tape obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this Tape obj) => ConfigWishes.GetBits(obj);
        
        public static Tape With8Bit(this  Tape obj)            => ConfigWishes.With8Bit(obj);
        public static Tape With16Bit(this Tape obj)            => ConfigWishes.With16Bit(obj);
        public static Tape With32Bit(this Tape obj)            => ConfigWishes.With32Bit(obj);
        public static Tape As8Bit(this    Tape obj)            => ConfigWishes.As8Bit(obj);
        public static Tape As16Bit(this   Tape obj)            => ConfigWishes.As16Bit(obj);
        public static Tape As32Bit(this   Tape obj)            => ConfigWishes.As32Bit(obj);
        public static Tape Set8Bit(this   Tape obj)            => ConfigWishes.Set8Bit(obj);
        public static Tape Set16Bit(this  Tape obj)            => ConfigWishes.Set16Bit(obj);
        public static Tape Set32Bit(this  Tape obj)            => ConfigWishes.Set32Bit(obj);
        public static Tape Bits(this      Tape obj, int value) => ConfigWishes.Bits(obj, value);
        public static Tape WithBits(this  Tape obj, int value) => ConfigWishes.WithBits(obj, value);
        public static Tape AsBits(this    Tape obj, int value) => ConfigWishes.AsBits(obj, value);
        public static Tape SetBits(this   Tape obj, int value) => ConfigWishes.SetBits(obj, value);
        
        public static bool Is8Bit(this  TapeConfig obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this TapeConfig obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this TapeConfig obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    TapeConfig obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this TapeConfig obj) => ConfigWishes.GetBits(obj);

        public static TapeConfig With8Bit(this  TapeConfig obj)            => ConfigWishes.With8Bit(obj);
        public static TapeConfig With16Bit(this TapeConfig obj)            => ConfigWishes.With16Bit(obj);
        public static TapeConfig With32Bit(this TapeConfig obj)            => ConfigWishes.With32Bit(obj);
        public static TapeConfig As8Bit(this    TapeConfig obj)            => ConfigWishes.As8Bit(obj);
        public static TapeConfig As16Bit(this   TapeConfig obj)            => ConfigWishes.As16Bit(obj);
        public static TapeConfig As32Bit(this   TapeConfig obj)            => ConfigWishes.As32Bit(obj);
        public static TapeConfig Set8Bit(this   TapeConfig obj)            => ConfigWishes.Set8Bit(obj);
        public static TapeConfig Set16Bit(this  TapeConfig obj)            => ConfigWishes.Set16Bit(obj);
        public static TapeConfig Set32Bit(this  TapeConfig obj)            => ConfigWishes.Set32Bit(obj);
        public static TapeConfig Bits(this      TapeConfig obj, int value) => ConfigWishes.Bits(obj, value);
        public static TapeConfig WithBits(this  TapeConfig obj, int value) => ConfigWishes.WithBits(obj, value);
        public static TapeConfig AsBits(this    TapeConfig obj, int value) => ConfigWishes.AsBits(obj, value);
        public static TapeConfig SetBits(this TapeConfig obj, int value)   => ConfigWishes.SetBits(obj, value);

        public static bool Is8Bit(this  TapeActions obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this TapeActions obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this TapeActions obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    TapeActions obj) => ConfigWishes.Bits(obj);
        public static int GetBits(this TapeActions obj)  => ConfigWishes.GetBits(obj);

        public static TapeActions With8Bit(this  TapeActions obj)            => ConfigWishes.With8Bit(obj);
        public static TapeActions With16Bit(this TapeActions obj)            => ConfigWishes.With16Bit(obj);
        public static TapeActions With32Bit(this TapeActions obj)            => ConfigWishes.With32Bit(obj);
        public static TapeActions As8Bit(this    TapeActions obj)            => ConfigWishes.As8Bit(obj);
        public static TapeActions As16Bit(this   TapeActions obj)            => ConfigWishes.As16Bit(obj);
        public static TapeActions As32Bit(this   TapeActions obj)            => ConfigWishes.As32Bit(obj);
        public static TapeActions Set8Bit(this   TapeActions obj)            => ConfigWishes.Set8Bit(obj);
        public static TapeActions Set16Bit(this  TapeActions obj)            => ConfigWishes.Set16Bit(obj);
        public static TapeActions Set32Bit(this  TapeActions obj)            => ConfigWishes.Set32Bit(obj);
        public static TapeActions Bits(this      TapeActions obj, int value) => ConfigWishes.Bits(obj, value);
        public static TapeActions WithBits(this  TapeActions obj, int value) => ConfigWishes.WithBits(obj, value);
        public static TapeActions AsBits(this    TapeActions obj, int value) => ConfigWishes.AsBits(obj, value);
        public static TapeActions SetBits(this   TapeActions obj, int value) => ConfigWishes.SetBits(obj, value);

        public static bool Is8Bit(this  TapeAction obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this TapeAction obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this TapeAction obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    TapeAction obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this TapeAction obj) => ConfigWishes.GetBits(obj);

        public static TapeAction With8Bit(this  TapeAction obj)            => ConfigWishes.With8Bit(obj);
        public static TapeAction With16Bit(this TapeAction obj)            => ConfigWishes.With16Bit(obj);
        public static TapeAction With32Bit(this TapeAction obj)            => ConfigWishes.With32Bit(obj);
        public static TapeAction As8Bit(this    TapeAction obj)            => ConfigWishes.As8Bit(obj);
        public static TapeAction As16Bit(this   TapeAction obj)            => ConfigWishes.As16Bit(obj);
        public static TapeAction As32Bit(this   TapeAction obj)            => ConfigWishes.As32Bit(obj);
        public static TapeAction Set8Bit(this   TapeAction obj)            => ConfigWishes.Set8Bit(obj);
        public static TapeAction Set16Bit(this  TapeAction obj)            => ConfigWishes.Set16Bit(obj);
        public static TapeAction Set32Bit(this  TapeAction obj)            => ConfigWishes.Set32Bit(obj);
        public static TapeAction Bits(this      TapeAction obj, int value) => ConfigWishes.Bits(obj, value);
        public static TapeAction WithBits(this  TapeAction obj, int value) => ConfigWishes.WithBits(obj, value);
        public static TapeAction AsBits(this    TapeAction obj, int value) => ConfigWishes.AsBits(obj, value);
        public static TapeAction SetBits(this   TapeAction obj, int value) => ConfigWishes.SetBits(obj, value);

        // Buff-Bound
        
        public static bool Is8Bit(this  Buff obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this Buff obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this Buff obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    Buff obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this Buff obj) => ConfigWishes.GetBits(obj);

        public static Buff With8Bit(this Buff obj, IContext context) => ConfigWishes.With8Bit(obj, context);
        public static Buff With16Bit(this Buff obj, IContext context) => ConfigWishes.Set16Bit(obj, context);
        public static Buff With32Bit(this Buff obj, IContext context) => ConfigWishes.Set32Bit(obj, context);
        public static Buff As8Bit(this Buff obj, IContext context) => ConfigWishes.As8Bit(obj, context);
        public static Buff As16Bit(this Buff obj, IContext context) => ConfigWishes.As16Bit(obj, context);
        public static Buff As32Bit(this Buff obj, IContext context) => ConfigWishes.As32Bit(obj, context);
        public static Buff Set8Bit(this Buff obj, IContext context) => ConfigWishes.Set8Bit(obj, context);
        public static Buff Set16Bit(this Buff obj, IContext context) => ConfigWishes.Set16Bit(obj, context);
        public static Buff Set32Bit(this Buff obj, IContext context) => ConfigWishes.Set32Bit(obj, context);
        public static Buff Bits(this Buff obj, int value, IContext context) => ConfigWishes.Bits(obj, value, context);
        public static Buff WithBits(this Buff obj, int value, IContext context) => ConfigWishes.WithBits(obj, value, context);
        public static Buff AsBits(this Buff obj, int value, IContext context) => ConfigWishes.AsBits(obj, value, context);
        public static Buff SetBits(this Buff obj, int value, IContext context) => ConfigWishes.SetBits(obj, value, context);
        
        public static bool Is8Bit(this  AudioFileOutput obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this AudioFileOutput obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this AudioFileOutput obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    AudioFileOutput obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this AudioFileOutput obj) => ConfigWishes.GetBits(obj);

        public static AudioFileOutput With8Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.With8Bit(obj, context);
        public static AudioFileOutput With16Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.With16Bit(obj, context);
        public static AudioFileOutput With32Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.With32Bit(obj, context);
        public static AudioFileOutput As8Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.As8Bit(obj, context);
        public static AudioFileOutput As16Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.As16Bit(obj, context);
        public static AudioFileOutput As32Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.As32Bit(obj, context);
        public static AudioFileOutput Set8Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.Set8Bit(obj, context);
        public static AudioFileOutput Set16Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.Set16Bit(obj, context);
        public static AudioFileOutput Set32Bit(this AudioFileOutput obj, IContext context) => ConfigWishes.Set32Bit(obj, context);
        public static AudioFileOutput Bits(this AudioFileOutput obj, int value, IContext context) => ConfigWishes.Bits(obj, value, context);
        public static AudioFileOutput AsBits(this AudioFileOutput obj, int value, IContext context) => ConfigWishes.AsBits(obj, value, context);
        public static AudioFileOutput WithBits(this AudioFileOutput obj, int value, IContext context) => ConfigWishes.WithBits(obj, value, context);
        public static AudioFileOutput SetBits(this AudioFileOutput obj, int value, IContext context) => ConfigWishes.SetBits(obj, value, context);

        // Independent after Taping
        
        public static bool Is8Bit(this  Sample obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this Sample obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this Sample obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    Sample obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this Sample obj) => ConfigWishes.GetBits(obj);
        
        public static Sample With8Bit(this  Sample obj, IContext context) => ConfigWishes.With8Bit(obj, context);
        public static Sample With16Bit(this Sample obj, IContext context) => ConfigWishes.With16Bit(obj, context);
        public static Sample With32Bit(this Sample obj, IContext context) => ConfigWishes.With32Bit(obj, context);
        public static Sample As8Bit(this    Sample obj, IContext context) => ConfigWishes.As8Bit(obj, context);
        public static Sample As16Bit(this   Sample obj, IContext context) => ConfigWishes.As16Bit(obj, context);
        public static Sample As32Bit(this   Sample obj, IContext context) => ConfigWishes.As32Bit(obj, context);
        public static Sample Set8Bit(this   Sample obj, IContext context) => ConfigWishes.Set8Bit(obj, context);
        public static Sample Set16Bit(this  Sample obj, IContext context) => ConfigWishes.Set16Bit(obj, context);
        public static Sample Set32Bit(this  Sample obj, IContext context) => ConfigWishes.Set32Bit(obj, context);
        public static Sample Bits(this      Sample obj, int value, IContext context) => ConfigWishes.Bits(obj, value, context);
        public static Sample WithBits(this  Sample obj, int value, IContext context) => ConfigWishes.WithBits(obj, value, context);
        public static Sample AsBits(this    Sample obj, int value, IContext context) => ConfigWishes.AsBits(obj, value, context);
        public static Sample SetBits(this   Sample obj, int value, IContext context) => ConfigWishes.SetBits(obj, value, context);
        
        public static bool Is8Bit(this  AudioInfoWish obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this AudioInfoWish obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this AudioInfoWish obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    AudioInfoWish obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this AudioInfoWish obj) => ConfigWishes.GetBits(obj);
        
        public static AudioInfoWish With8Bit(this  AudioInfoWish obj) => ConfigWishes.With8Bit(obj);
        public static AudioInfoWish With16Bit(this AudioInfoWish obj) => ConfigWishes.With16Bit(obj);
        public static AudioInfoWish With32Bit(this AudioInfoWish obj) => ConfigWishes.With32Bit(obj);
        public static AudioInfoWish As8Bit(this    AudioInfoWish obj) => ConfigWishes.As8Bit(obj);
        public static AudioInfoWish As16Bit(this   AudioInfoWish obj) => ConfigWishes.As16Bit(obj);
        public static AudioInfoWish As32Bit(this   AudioInfoWish obj) => ConfigWishes.As32Bit(obj);
        public static AudioInfoWish Set8Bit(this   AudioInfoWish obj) => ConfigWishes.Set8Bit(obj);
        public static AudioInfoWish Set16Bit(this  AudioInfoWish obj) => ConfigWishes.Set16Bit(obj);
        public static AudioInfoWish Set32Bit(this  AudioInfoWish obj) => ConfigWishes.Set32Bit(obj);
        public static AudioInfoWish Bits(this      AudioInfoWish obj, int value) => ConfigWishes.Bits(obj, value);
        public static AudioInfoWish WithBits(this  AudioInfoWish obj, int value) => ConfigWishes.WithBits(obj, value);
        public static AudioInfoWish AsBits(this    AudioInfoWish obj, int value) => ConfigWishes.AsBits(obj, value);
        public static AudioInfoWish SetBits(this   AudioInfoWish obj, int value) => ConfigWishes.SetBits(obj, value);
        
        public static bool Is8Bit(this  AudioFileInfo obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this AudioFileInfo obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this AudioFileInfo obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    AudioFileInfo obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this AudioFileInfo obj) => ConfigWishes.GetBits(obj);
        
        public static AudioFileInfo With8Bit(this  AudioFileInfo obj) => ConfigWishes.With8Bit(obj);
        public static AudioFileInfo With16Bit(this AudioFileInfo obj) => ConfigWishes.With16Bit(obj);
        public static AudioFileInfo With32Bit(this AudioFileInfo obj) => ConfigWishes.With32Bit(obj);
        public static AudioFileInfo As8Bit(this    AudioFileInfo obj) => ConfigWishes.As8Bit(obj);
        public static AudioFileInfo As16Bit(this   AudioFileInfo obj) => ConfigWishes.As16Bit(obj);
        public static AudioFileInfo As32Bit(this   AudioFileInfo obj) => ConfigWishes.As32Bit(obj);
        public static AudioFileInfo Set8Bit(this   AudioFileInfo obj) => ConfigWishes.Set8Bit(obj);
        public static AudioFileInfo Set16Bit(this  AudioFileInfo obj) => ConfigWishes.Set16Bit(obj);
        public static AudioFileInfo Set32Bit(this  AudioFileInfo obj) => ConfigWishes.Set32Bit(obj);
        public static AudioFileInfo Bits(this      AudioFileInfo obj, int bits) => ConfigWishes.Bits(obj, bits);
        public static AudioFileInfo WithBits(this  AudioFileInfo obj, int bits) => ConfigWishes.WithBits(obj, bits);
        public static AudioFileInfo AsBits(this    AudioFileInfo obj, int bits) => ConfigWishes.AsBits(obj, bits);
        public static AudioFileInfo SetBits(this   AudioFileInfo obj, int bits) => ConfigWishes.SetBits(obj, bits);

        // Immutable        
        
        public static bool Is8Bit(this  WavHeaderStruct obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this WavHeaderStruct obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this WavHeaderStruct obj) => ConfigWishes.Is32Bit(obj);
        public static int  Bits(this    WavHeaderStruct obj) => ConfigWishes.Bits(obj);
        public static int  GetBits(this WavHeaderStruct obj) => ConfigWishes.GetBits(obj);
        
        public static WavHeaderStruct With8Bit(this  WavHeaderStruct obj) => ConfigWishes.With8Bit(obj);
        public static WavHeaderStruct With16Bit(this WavHeaderStruct obj) => ConfigWishes.With16Bit(obj);
        public static WavHeaderStruct With32Bit(this WavHeaderStruct obj) => ConfigWishes.With32Bit(obj);
        public static WavHeaderStruct As8Bit(this    WavHeaderStruct obj) => ConfigWishes.As8Bit(obj);
        public static WavHeaderStruct As16Bit(this   WavHeaderStruct obj) => ConfigWishes.As16Bit(obj);
        public static WavHeaderStruct As32Bit(this   WavHeaderStruct obj) => ConfigWishes.As32Bit(obj);
        public static WavHeaderStruct Set8Bit(this   WavHeaderStruct obj) => ConfigWishes.Set8Bit(obj);
        public static WavHeaderStruct Set16Bit(this  WavHeaderStruct obj) => ConfigWishes.Set16Bit(obj);
        public static WavHeaderStruct Set32Bit(this  WavHeaderStruct obj) => ConfigWishes.Set32Bit(obj);
        public static WavHeaderStruct Bits(this      WavHeaderStruct obj, int value) => ConfigWishes.Bits(obj, value);
        public static WavHeaderStruct WithBits(this  WavHeaderStruct obj, int value) => ConfigWishes.WithBits(obj, value);
        public static WavHeaderStruct AsBits(this    WavHeaderStruct obj, int value) => ConfigWishes.AsBits(obj, value);
        public static WavHeaderStruct SetBits(this   WavHeaderStruct obj, int value) => ConfigWishes.SetBits(obj, value);

        [Obsolete(ObsoleteMessage)] public static bool Is8Bit(this SampleDataTypeEnum obj) => ConfigWishes.Is8Bit(obj);
        [Obsolete(ObsoleteMessage)] public static bool Is16Bit(this SampleDataTypeEnum obj) => ConfigWishes.Is16Bit(obj);
        [Obsolete(ObsoleteMessage)] public static bool Is32Bit(this SampleDataTypeEnum obj) => ConfigWishes.Is32Bit(obj);
        [Obsolete(ObsoleteMessage)] public static int Bits(this SampleDataTypeEnum obj) => ConfigWishes.Bits(obj);
        [Obsolete(ObsoleteMessage)] public static int GetBits(this SampleDataTypeEnum obj) => ConfigWishes.GetBits(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With8Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.With8Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With16Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.With16Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With32Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.With32Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As8Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.As8Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As16Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.As16Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As32Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.As32Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set8Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.Set8Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set16Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.Set16Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set32Bit(this SampleDataTypeEnum oldEnumValue) => ConfigWishes.Set32Bit(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Bits(this SampleDataTypeEnum oldEnumValue, int newBits) => ConfigWishes.Bits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum WithBits(this SampleDataTypeEnum oldEnumValue, int newBits) => ConfigWishes.WithBits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum AsBits(this SampleDataTypeEnum oldEnumValue, int newBits) => ConfigWishes.AsBits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SetBits(this SampleDataTypeEnum oldEnumValue, int newBits)=> ConfigWishes.SetBits(oldEnumValue, newBits);

        [Obsolete(ObsoleteMessage)] public static bool Is8Bit(this SampleDataType obj) => ConfigWishes.Is8Bit(obj);
        [Obsolete(ObsoleteMessage)] public static bool Is16Bit(this SampleDataType obj) => ConfigWishes.Is16Bit(obj);
        [Obsolete(ObsoleteMessage)] public static bool Is32Bit(this SampleDataType obj) => ConfigWishes.Is32Bit(obj);
        [Obsolete(ObsoleteMessage)] public static int Bits(this SampleDataType obj) => ConfigWishes.Bits(obj);
        [Obsolete(ObsoleteMessage)] public static int GetBits(this SampleDataType obj) => ConfigWishes.GetBits(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With8Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.With8Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With16Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.With16Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With32Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.With32Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As8Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.As8Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As16Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.As16Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As32Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.As32Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set8Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.Set8Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set16Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.Set16Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set32Bit(this SampleDataType oldSampleDataType, IContext context) => ConfigWishes.Set32Bit(oldSampleDataType, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Bits(this SampleDataType oldSampleDataType, int newBits, IContext context) => ConfigWishes.Bits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType WithBits(this SampleDataType oldSampleDataType, int newBits, IContext context) => ConfigWishes.WithBits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType AsBits(this SampleDataType oldSampleDataType, int newBits, IContext context) => ConfigWishes.AsBits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType SetBits(this SampleDataType oldSampleDataType, int newBits, IContext context) => ConfigWishes.SetBits(oldSampleDataType, newBits, context);

        public static bool Is8Bit(this Type obj) => ConfigWishes.Is8Bit(obj);
        public static bool Is16Bit(this Type obj) => ConfigWishes.Is16Bit(obj);
        public static bool Is32Bit(this Type obj) => ConfigWishes.Is32Bit(obj);
        public static int Bits(this Type valueType) => ConfigWishes.Bits(valueType);
        public static int GetBits(this Type valueType) => ConfigWishes.GetBits(valueType);

        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With8Bit(this Type oldValueType) => ConfigWishes.With8Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With16Bit(this Type oldValueType) => ConfigWishes.With16Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With32Bit(this Type oldValueType) => ConfigWishes.With32Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As8Bit(this Type oldValueType) => ConfigWishes.As8Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As16Bit(this Type oldValueType) => ConfigWishes.As16Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As32Bit(this Type oldValueType) => ConfigWishes.As32Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set8Bit(this Type oldValueType) => ConfigWishes.Set8Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set16Bit(this Type oldValueType) => ConfigWishes.Set16Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set32Bit(this Type oldValueType) => ConfigWishes.Set32Bit(oldValueType);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Bits(this Type oldValueType, int newBits) => ConfigWishes.Bits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type WithBits(this Type oldValueType, int newBits) => ConfigWishes.WithBits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type AsBits(this Type oldValueType, int newBits) => ConfigWishes.AsBits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SetBits(this Type oldValueType, int newBits) => ConfigWishes.SetBits(oldValueType, newBits);

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
        // Bits: A Primary Audio Attribute

        // Synth-Bound

        public static bool Is8Bit(SynthWishes obj) => GetBits(obj) == 8;
        public static bool Is16Bit(SynthWishes obj) => GetBits(obj) == 16;
        public static bool Is32Bit(SynthWishes obj) => GetBits(obj) == 32;
        public static int Bits(SynthWishes obj) => GetBits(obj);
        public static int GetBits(SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        
        public static SynthWishes With8Bit(SynthWishes obj) => SetBits(obj, 8);
        public static SynthWishes With16Bit(SynthWishes obj) => SetBits(obj, 16);
        public static SynthWishes With32Bit(SynthWishes obj) => SetBits(obj, 32);
        public static SynthWishes As8Bit(SynthWishes obj) => SetBits(obj, 8);
        public static SynthWishes As16Bit(SynthWishes obj) => SetBits(obj, 16);
        public static SynthWishes As32Bit(SynthWishes obj) => SetBits(obj, 32);
        public static SynthWishes Set8Bit(SynthWishes obj) => SetBits(obj, 8);
        public static SynthWishes Set16Bit(SynthWishes obj) => SetBits(obj, 16);
        public static SynthWishes Set32Bit(SynthWishes obj) => SetBits(obj, 32);
        public static SynthWishes Bits(SynthWishes obj, int? value) => SetBits(obj, value);
        public static SynthWishes WithBits(SynthWishes obj, int? value) => SetBits(obj, value);
        public static SynthWishes AsBits(SynthWishes obj, int? value) => SetBits(obj, value);
        public static SynthWishes SetBits(SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }
        
        public static bool Is8Bit(FlowNode obj) => GetBits(obj) == 8;
        public static bool Is16Bit(FlowNode obj) => GetBits(obj) == 16;
        public static bool Is32Bit(FlowNode obj) => GetBits(obj) == 32;
        public static int Bits(FlowNode obj) => GetBits(obj);
        public static int GetBits(FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        
        public static FlowNode With8Bit(FlowNode obj) => SetBits(obj, 8);
        public static FlowNode With16Bit(FlowNode obj) => SetBits(obj, 16);
        public static FlowNode With32Bit(FlowNode obj) => SetBits(obj, 32);
        public static FlowNode As8Bit(FlowNode obj) => SetBits(obj, 8);
        public static FlowNode As16Bit(FlowNode obj) => SetBits(obj, 16);
        public static FlowNode As32Bit(FlowNode obj) => SetBits(obj, 32);
        public static FlowNode Set8Bit(FlowNode obj) => SetBits(obj, 8);
        public static FlowNode Set16Bit(FlowNode obj) => SetBits(obj, 16);
        public static FlowNode Set32Bit(FlowNode obj) => SetBits(obj, 32);
        public static FlowNode Bits(FlowNode obj, int? value) => SetBits(obj, value);
        public static FlowNode WithBits(FlowNode obj, int? value) => SetBits(obj, value);
        public static FlowNode AsBits(FlowNode obj, int? value) => SetBits(obj, value);
        public static FlowNode SetBits(FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }

        [UsedImplicitly] internal static bool Is8Bit(ConfigResolver obj) => GetBits(obj) == 8;
        [UsedImplicitly] internal static bool Is16Bit(ConfigResolver obj) => GetBits(obj) == 16;
        [UsedImplicitly] internal static bool Is32Bit(ConfigResolver obj) => GetBits(obj) == 32;
        internal static int Bits(ConfigResolver obj) => GetBits(obj);
        internal static int GetBits(ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetBits;
        }
        
        [UsedImplicitly] internal static ConfigResolver With8Bit(ConfigResolver obj) => SetBits(obj, 8);
        [UsedImplicitly] internal static ConfigResolver With16Bit(ConfigResolver obj) => SetBits(obj, 16);
        [UsedImplicitly] internal static ConfigResolver With32Bit(ConfigResolver obj) => SetBits(obj, 32);
        [UsedImplicitly] internal static ConfigResolver As8Bit(ConfigResolver obj) => SetBits(obj, 8);
        [UsedImplicitly] internal static ConfigResolver As16Bit(ConfigResolver obj) => SetBits(obj, 16);
        [UsedImplicitly] internal static ConfigResolver As32Bit(ConfigResolver obj) => SetBits(obj, 32);
        [UsedImplicitly] internal static ConfigResolver Set8Bit(ConfigResolver obj) => SetBits(obj, 8);
        [UsedImplicitly] internal static ConfigResolver Set16Bit(ConfigResolver obj) => SetBits(obj, 16);
        [UsedImplicitly] internal static ConfigResolver Set32Bit(ConfigResolver obj) => SetBits(obj, 32);
        internal static ConfigResolver Bits(ConfigResolver obj, int? value) => SetBits(obj, value);
        internal static ConfigResolver WithBits(ConfigResolver obj, int? value) => SetBits(obj, value);
        internal static ConfigResolver AsBits(ConfigResolver obj, int? value) => SetBits(obj, value);
        internal static ConfigResolver SetBits(ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithBits(value);
        }

        // Global-Bound

        [UsedImplicitly] internal static bool Is8Bit(ConfigSection obj) => GetBits(obj) == 8;
        [UsedImplicitly] internal static bool Is16Bit(ConfigSection obj) => GetBits(obj) == 16;
        [UsedImplicitly] internal static bool Is32Bit(ConfigSection obj) => GetBits(obj) == 32;
        internal static int? Bits(ConfigSection obj) => GetBits(obj);
        internal static int? GetBits(ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        // Tape-Bound
        
        public static bool Is8Bit(Tape obj) => GetBits(obj) == 8;
        public static bool Is16Bit(Tape obj) => GetBits(obj) == 16;
        public static bool Is32Bit(Tape obj) => GetBits(obj) == 32;
        public static int Bits(Tape obj) => GetBits(obj);
        public static int GetBits(Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Bits;
        }
        
        public static Tape With8Bit(Tape obj) => SetBits(obj, 8);
        public static Tape With16Bit(Tape obj) => SetBits(obj, 16);
        public static Tape With32Bit(Tape obj) => SetBits(obj, 32);
        public static Tape As8Bit(Tape obj) => SetBits(obj, 8);
        public static Tape As16Bit(Tape obj) => SetBits(obj, 16);
        public static Tape As32Bit(Tape obj) => SetBits(obj, 32);
        public static Tape Set8Bit(Tape obj) => SetBits(obj, 8);
        public static Tape Set16Bit(Tape obj) => SetBits(obj, 16);
        public static Tape Set32Bit(Tape obj) => SetBits(obj, 32);
        public static Tape Bits(Tape obj, int value) => SetBits(obj, value);
        public static Tape WithBits(Tape obj, int value) => SetBits(obj, value);
        public static Tape AsBits(Tape obj, int value) => SetBits(obj, value);
        public static Tape SetBits(Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Bits = value;
            return obj;
        }
        
        public static bool Is8Bit(TapeConfig obj) => GetBits(obj) == 8;
        public static bool Is16Bit(TapeConfig obj) => GetBits(obj) == 16;
        public static bool Is32Bit(TapeConfig obj) => GetBits(obj) == 32;
        public static int Bits(TapeConfig obj) => GetBits(obj);
        public static int GetBits(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        public static TapeConfig With8Bit(TapeConfig obj) => SetBits(obj, 8);
        public static TapeConfig With16Bit(TapeConfig obj) => SetBits(obj, 16);
        public static TapeConfig With32Bit(TapeConfig obj) => SetBits(obj, 32);
        public static TapeConfig As8Bit(TapeConfig obj) => SetBits(obj, 8);
        public static TapeConfig As16Bit(TapeConfig obj) => SetBits(obj, 16);
        public static TapeConfig As32Bit(TapeConfig obj) => SetBits(obj, 32);
        public static TapeConfig Set8Bit(TapeConfig obj) => SetBits(obj, 8);
        public static TapeConfig Set16Bit(TapeConfig obj) => SetBits(obj, 16);
        public static TapeConfig Set32Bit(TapeConfig obj) => SetBits(obj, 32);
        public static TapeConfig Bits(TapeConfig obj, int value) => SetBits(obj, value);
        public static TapeConfig WithBits(TapeConfig obj, int value) => SetBits(obj, value);
        public static TapeConfig AsBits(TapeConfig obj, int value) => SetBits(obj, value);
        public static TapeConfig SetBits(TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Bits = value;
            return obj;
        }
        
        public static bool Is8Bit(TapeActions obj) => GetBits(obj) == 8;
        public static bool Is16Bit(TapeActions obj) => GetBits(obj) == 16;
        public static bool Is32Bit(TapeActions obj) => GetBits(obj) == 32;
        public static int Bits(TapeActions obj) => GetBits(obj);
        public static int GetBits(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Bits;
        }
        
        public static TapeActions With8Bit(TapeActions obj) => SetBits(obj, 8);
        public static TapeActions With16Bit(TapeActions obj) => SetBits(obj, 16);
        public static TapeActions With32Bit(TapeActions obj) => SetBits(obj, 32);
        public static TapeActions As8Bit(TapeActions obj) => SetBits(obj, 8);
        public static TapeActions As16Bit(TapeActions obj) => SetBits(obj, 16);
        public static TapeActions As32Bit(TapeActions obj) => SetBits(obj, 32);
        public static TapeActions Set8Bit(TapeActions obj) => SetBits(obj, 8);
        public static TapeActions Set16Bit(TapeActions obj) => SetBits(obj, 16);
        public static TapeActions Set32Bit(TapeActions obj) => SetBits(obj, 32);
        public static TapeActions Bits(TapeActions obj, int value) => SetBits(obj, value);
        public static TapeActions WithBits(TapeActions obj, int value) => SetBits(obj, value);
        public static TapeActions AsBits(TapeActions obj, int value) => SetBits(obj, value);
        public static TapeActions SetBits(TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Bits = value;
            return obj;
        }
        
        public static bool Is8Bit(TapeAction obj) => GetBits(obj) == 8;
        public static bool Is16Bit(TapeAction obj) => GetBits(obj) == 16;
        public static bool Is32Bit(TapeAction obj) => GetBits(obj) == 32;
        public static int Bits(TapeAction obj) => GetBits(obj);
        public static int GetBits(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Bits;
        }
        
        public static TapeAction With8Bit(TapeAction obj) => SetBits(obj, 8);
        public static TapeAction With16Bit(TapeAction obj) => SetBits(obj, 16);
        public static TapeAction With32Bit(TapeAction obj) => SetBits(obj, 32);
        public static TapeAction As8Bit(TapeAction obj) => SetBits(obj, 8);
        public static TapeAction As16Bit(TapeAction obj) => SetBits(obj, 16);
        public static TapeAction As32Bit(TapeAction obj) => SetBits(obj, 32);
        public static TapeAction Set8Bit(TapeAction obj) => SetBits(obj, 8);
        public static TapeAction Set16Bit(TapeAction obj) => SetBits(obj, 16);
        public static TapeAction Set32Bit(TapeAction obj) => SetBits(obj, 32);
        public static TapeAction Bits(TapeAction obj, int value) => SetBits(obj, value);
        public static TapeAction WithBits(TapeAction obj, int value) => SetBits(obj, value);
        public static TapeAction AsBits(TapeAction obj, int value) => SetBits(obj, value);
        public static TapeAction SetBits(TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Bits = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static bool Is8Bit(Buff obj) => GetBits(obj) == 8;
        public static bool Is16Bit(Buff obj) => GetBits(obj) == 16;
        public static bool Is32Bit(Buff obj) => GetBits(obj) == 32;
        public static int Bits(Buff obj) => GetBits(obj);
        public static int GetBits(Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.UnderlyingAudioFileOutput.Bits();
        }
        
        public static Buff With8Bit(Buff obj, IContext context) => SetBits(obj, 8, context);
        public static Buff With16Bit(Buff obj, IContext context) => SetBits(obj, 16, context);
        public static Buff With32Bit(Buff obj, IContext context) => SetBits(obj, 32, context);
        public static Buff As8Bit(Buff obj, IContext context) => SetBits(obj, 8, context);
        public static Buff As16Bit(Buff obj, IContext context) => SetBits(obj, 16, context);
        public static Buff As32Bit(Buff obj, IContext context) => SetBits(obj, 32, context);
        public static Buff Set8Bit(Buff obj, IContext context) => SetBits(obj, 8, context);
        public static Buff Set16Bit(Buff obj, IContext context) => SetBits(obj, 16, context);
        public static Buff Set32Bit(Buff obj, IContext context) => SetBits(obj, 32, context);
        public static Buff Bits(Buff obj, int value, IContext context) => SetBits(obj, value, context);
        public static Buff WithBits(Buff obj, int value, IContext context) => SetBits(obj, value, context);
        public static Buff AsBits(Buff obj, int value, IContext context) => SetBits(obj, value, context);
        public static Buff SetBits(Buff obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.Bits(value, context);
            return obj;
        }
        
        public static bool Is8Bit(AudioFileOutput obj) => GetBits(obj) == 8;
        public static bool Is16Bit(AudioFileOutput obj) => GetBits(obj) == 16;
        public static bool Is32Bit(AudioFileOutput obj) => GetBits(obj) == 32;
        public static int Bits(AudioFileOutput obj) => GetBits(obj);
        public static int GetBits(AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSampleDataTypeEnum().EnumToBits();
        }
        
        public static AudioFileOutput With8Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 8, context);
        public static AudioFileOutput With16Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 16, context);
        public static AudioFileOutput With32Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 32, context);
        public static AudioFileOutput As8Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 8, context);
        public static AudioFileOutput As16Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 16, context);
        public static AudioFileOutput As32Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 32, context);
        public static AudioFileOutput Set8Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 8, context);
        public static AudioFileOutput Set16Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 16, context);
        public static AudioFileOutput Set32Bit(AudioFileOutput obj, IContext context) => SetBits(obj, 32, context);
        public static AudioFileOutput Bits(AudioFileOutput obj, int value, IContext context) => SetBits(obj, value, context);
        public static AudioFileOutput AsBits(AudioFileOutput obj, int value, IContext context) => SetBits(obj, value, context);
        public static AudioFileOutput WithBits(AudioFileOutput obj, int value, IContext context) => SetBits(obj, value, context);
        public static AudioFileOutput SetBits(AudioFileOutput obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return obj;
        }
        
        // Independent after Taping
        
        public static bool Is8Bit(Sample obj) => GetBits(obj) == 8;
        public static bool Is16Bit(Sample obj) => GetBits(obj) == 16;
        public static bool Is32Bit(Sample obj) => GetBits(obj) == 32;
        public static int Bits(Sample obj) => GetBits(obj);
        public static int GetBits(Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSampleDataTypeEnum().EnumToBits();
        }
        
        public static Sample With8Bit(Sample obj, IContext context) => SetBits(obj, 8, context);
        public static Sample With16Bit(Sample obj, IContext context) => SetBits(obj, 16, context);
        public static Sample With32Bit(Sample obj, IContext context) => SetBits(obj, 32, context);
        public static Sample As8Bit(Sample obj, IContext context) => SetBits(obj, 8, context);
        public static Sample As16Bit(Sample obj, IContext context) => SetBits(obj, 16, context);
        public static Sample As32Bit(Sample obj, IContext context) => SetBits(obj, 32, context);
        public static Sample Set8Bit(Sample obj, IContext context) => SetBits(obj, 8, context);
        public static Sample Set16Bit(Sample obj, IContext context) => SetBits(obj, 16, context);
        public static Sample Set32Bit(Sample obj, IContext context) => SetBits(obj, 32, context);
        public static Sample Bits(Sample obj, int value, IContext context) => SetBits(obj, value, context);
        public static Sample WithBits(Sample obj, int value, IContext context) => SetBits(obj, value, context);
        public static Sample AsBits(Sample obj, int value, IContext context) => SetBits(obj, value, context);
        public static Sample SetBits(Sample obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetSampleDataTypeEnum(value.BitsToEnum(), context);
            return obj;
        }
        
        public static bool Is8Bit(AudioInfoWish obj) => GetBits(obj) == 8;
        public static bool Is16Bit(AudioInfoWish obj) => GetBits(obj) == 16;
        public static bool Is32Bit(AudioInfoWish obj) => GetBits(obj) == 32;
        public static int Bits(AudioInfoWish obj) => GetBits(obj);
        public static int GetBits(AudioInfoWish obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Bits;
        }
        
        public static AudioInfoWish With8Bit(AudioInfoWish obj) => SetBits(obj, 8);
        public static AudioInfoWish With16Bit(AudioInfoWish obj) => SetBits(obj, 16);
        public static AudioInfoWish With32Bit(AudioInfoWish obj) => SetBits(obj, 32);
        public static AudioInfoWish As8Bit(AudioInfoWish obj) => SetBits(obj, 8);
        public static AudioInfoWish As16Bit(AudioInfoWish obj) => SetBits(obj, 16);
        public static AudioInfoWish As32Bit(AudioInfoWish obj) => SetBits(obj, 32);
        public static AudioInfoWish Set8Bit(AudioInfoWish obj) => SetBits(obj, 8);
        public static AudioInfoWish Set16Bit(AudioInfoWish obj) => SetBits(obj, 16);
        public static AudioInfoWish Set32Bit(AudioInfoWish obj) => SetBits(obj, 32);
        public static AudioInfoWish Bits(AudioInfoWish obj, int value) => SetBits(obj, value);
        public static AudioInfoWish WithBits(AudioInfoWish obj, int value) => SetBits(obj, value);
        public static AudioInfoWish AsBits(AudioInfoWish obj, int value) => SetBits(obj, value);
        public static AudioInfoWish SetBits(AudioInfoWish obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Bits = AssertBits(value, strict: false);
            return obj;
        }
        
        public static bool Is8Bit(AudioFileInfo obj) => GetBits(obj) == 8;
        public static bool Is16Bit(AudioFileInfo obj) => GetBits(obj) == 16;
        public static bool Is32Bit(AudioFileInfo obj) => GetBits(obj) == 32;
        public static int Bits(AudioFileInfo obj) => GetBits(obj);
        public static int GetBits(AudioFileInfo obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.BytesPerValue.Bits();
        }
        
        public static AudioFileInfo With8Bit(AudioFileInfo obj) => SetBits(obj, 8);
        public static AudioFileInfo With16Bit(AudioFileInfo obj) => SetBits(obj, 16);
        public static AudioFileInfo With32Bit(AudioFileInfo obj) => SetBits(obj, 32);
        public static AudioFileInfo As8Bit(AudioFileInfo obj) => SetBits(obj, 8);
        public static AudioFileInfo As16Bit(AudioFileInfo obj) => SetBits(obj, 16);
        public static AudioFileInfo As32Bit(AudioFileInfo obj) => SetBits(obj, 32);
        public static AudioFileInfo Set8Bit(AudioFileInfo obj) => SetBits(obj, 8);
        public static AudioFileInfo Set16Bit(AudioFileInfo obj) => SetBits(obj, 16);
        public static AudioFileInfo Set32Bit(AudioFileInfo obj) => SetBits(obj, 32);
        public static AudioFileInfo Bits(AudioFileInfo obj, int bits) => SetBits(obj, bits);
        public static AudioFileInfo WithBits(AudioFileInfo obj, int bits) => SetBits(obj, bits);
        public static AudioFileInfo AsBits(AudioFileInfo obj, int bits) => SetBits(obj, bits);
        public static AudioFileInfo SetBits(AudioFileInfo obj, int bits)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.BytesPerValue = bits.SizeOfBitDepth();
            return obj;
        }

        // Immutable        
        
        public static bool Is8Bit(WavHeaderStruct obj) => GetBits(obj) == 8;
        public static bool Is16Bit(WavHeaderStruct obj) => GetBits(obj) == 16;
        public static bool Is32Bit(WavHeaderStruct obj) => GetBits(obj) == 32;
        public static int Bits(WavHeaderStruct obj) => GetBits(obj);
        public static int GetBits(WavHeaderStruct obj)
        {
            return obj.BitsPerValue;
        }
        
        public static WavHeaderStruct With8Bit(WavHeaderStruct obj) => SetBits(obj, 8);
        public static WavHeaderStruct With16Bit(WavHeaderStruct obj) => SetBits(obj, 16);
        public static WavHeaderStruct With32Bit(WavHeaderStruct obj) => SetBits(obj, 32);
        public static WavHeaderStruct As8Bit(WavHeaderStruct obj) => SetBits(obj, 8);
        public static WavHeaderStruct As16Bit(WavHeaderStruct obj) => SetBits(obj, 16);
        public static WavHeaderStruct As32Bit(WavHeaderStruct obj) => SetBits(obj, 32);
        public static WavHeaderStruct Set8Bit(WavHeaderStruct obj) => SetBits(obj, 8);
        public static WavHeaderStruct Set16Bit(WavHeaderStruct obj) => SetBits(obj, 16);
        public static WavHeaderStruct Set32Bit(WavHeaderStruct obj) => SetBits(obj, 32);
        public static WavHeaderStruct Bits(WavHeaderStruct obj, int value) => SetBits(obj, value);
        public static WavHeaderStruct WithBits(WavHeaderStruct obj, int value) => SetBits(obj, value);
        public static WavHeaderStruct AsBits(WavHeaderStruct obj, int value) => SetBits(obj, value);
        public static WavHeaderStruct SetBits(WavHeaderStruct obj, int value)
        {
            return obj.ToWish().Bits(value).ToWavHeader();
        }

        [Obsolete(ObsoleteMessage)] public static bool Is8Bit(SampleDataTypeEnum obj) => GetBits(obj) == 8;
        [Obsolete(ObsoleteMessage)] public static bool Is16Bit(SampleDataTypeEnum obj) => GetBits(obj) == 16;
        [Obsolete(ObsoleteMessage)] public static bool Is32Bit(SampleDataTypeEnum obj) => GetBits(obj) == 32;
        [Obsolete(ObsoleteMessage)] public static int Bits(SampleDataTypeEnum obj) => GetBits(obj);
        [Obsolete(ObsoleteMessage)] public static int GetBits(SampleDataTypeEnum obj)
        {
            return EnumToBits(obj);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With8Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With16Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum With32Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As8Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As16Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum As32Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set8Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set16Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Set32Bit(SampleDataTypeEnum oldEnumValue) => SetBits(oldEnumValue, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum Bits(SampleDataTypeEnum oldEnumValue, int newBits) => SetBits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum WithBits(SampleDataTypeEnum oldEnumValue, int newBits) => SetBits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum AsBits(SampleDataTypeEnum oldEnumValue, int newBits) => SetBits(oldEnumValue, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SetBits(SampleDataTypeEnum oldEnumValue, int newBits)
        {
            return newBits.BitsToEnum();
        }

        [Obsolete(ObsoleteMessage)] public static bool Is8Bit(SampleDataType obj) => GetBits(obj) == 8;
        [Obsolete(ObsoleteMessage)] public static bool Is16Bit(SampleDataType obj) => GetBits(obj) == 16;
        [Obsolete(ObsoleteMessage)] public static bool Is32Bit(SampleDataType obj) => GetBits(obj) == 32;
        [Obsolete(ObsoleteMessage)] public static int Bits(SampleDataType obj) => GetBits(obj);
        [Obsolete(ObsoleteMessage)] public static int GetBits(SampleDataType obj)
        {
            return obj.EntityToBits();
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With8Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 8, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With16Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 16, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType With32Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 32, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As8Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 8, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As16Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 16, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType As32Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 32, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set8Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 8, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set16Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 16, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Set32Bit(SampleDataType oldSampleDataType, IContext context) => SetBits(oldSampleDataType, 32, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType Bits(SampleDataType oldSampleDataType, int newBits, IContext context) => SetBits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType WithBits(SampleDataType oldSampleDataType, int newBits, IContext context) => SetBits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType AsBits(SampleDataType oldSampleDataType, int newBits, IContext context) => SetBits(oldSampleDataType, newBits, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType SetBits(SampleDataType oldSampleDataType, int newBits, IContext context)
        {
            return newBits.BitsToEntity(context);
        }
        
        public static bool Is8Bit(Type obj) => GetBits(obj) == 8;
        public static bool Is16Bit(Type obj) => GetBits(obj) == 16;
        public static bool Is32Bit(Type obj) => GetBits(obj) == 32;
        public static int Bits(Type valueType) => GetBits(valueType);
        public static int GetBits(Type valueType)
        {
            return TypeToBits(valueType);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With8Bit(Type oldValueType) => SetBits(oldValueType, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With16Bit(Type oldValueType) => SetBits(oldValueType, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type With32Bit(Type oldValueType) => SetBits(oldValueType, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As8Bit(Type oldValueType) => SetBits(oldValueType, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As16Bit(Type oldValueType) => SetBits(oldValueType, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type As32Bit(Type oldValueType) => SetBits(oldValueType, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set8Bit(Type oldValueType) => SetBits(oldValueType, 8);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set16Bit(Type oldValueType) => SetBits(oldValueType, 16);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Set32Bit(Type oldValueType) => SetBits(oldValueType, 32);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type Bits(Type oldValueType, int newBits) => SetBits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type WithBits(Type oldValueType, int newBits) => SetBits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type AsBits(Type oldValueType, int newBits) => SetBits(oldValueType, newBits);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SetBits(Type oldValueType, int newBits)
        {
            return newBits.BitsToType();
        }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
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