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
using static JJ.Business.Synthesizer.Wishes.SynthWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global
#pragma warning disable CS0618


namespace JJ.Business.Synthesizer.Wishes.Config
{
    // Channels: A Primary Audio Attribute

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ChannelsExtensionWishes
    {
        // Synth-Bound

        public static bool IsMono(this SynthWishes obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this SynthWishes obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this SynthWishes obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this SynthWishes obj) => ConfigWishes.GetChannels(obj);

        public static SynthWishes Mono(this SynthWishes obj) => ConfigWishes.Mono(obj);
        public static SynthWishes Stereo(this SynthWishes obj) => ConfigWishes.Stereo(obj);
        public static SynthWishes Channels(this SynthWishes obj, int? value) => ConfigWishes.Channels(obj, value);
        public static SynthWishes WithMono(this SynthWishes obj) => ConfigWishes.WithMono(obj);
        public static SynthWishes WithStereo(this SynthWishes obj) => ConfigWishes.WithStereo(obj);
        public static SynthWishes WithChannels(this SynthWishes obj, int? value) => ConfigWishes.WithChannels(obj, value);
        public static SynthWishes SetMono(this SynthWishes obj) => ConfigWishes.SetMono(obj);
        public static SynthWishes SetStereo(this SynthWishes obj) => ConfigWishes.SetStereo(obj);
        public static SynthWishes SetChannels(this SynthWishes obj, int? value) => ConfigWishes.SetChannels(obj, value);

        public static bool IsMono(this FlowNode obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this FlowNode obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this FlowNode obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this FlowNode obj) => ConfigWishes.GetChannels(obj);

        public static FlowNode Mono(this FlowNode obj) => ConfigWishes.Mono(obj);
        public static FlowNode Stereo(this FlowNode obj) => ConfigWishes.Stereo(obj);
        public static FlowNode Channels(this FlowNode obj, int? value) => ConfigWishes.Channels(obj, value);
        public static FlowNode WithMono(this FlowNode obj) => ConfigWishes.WithMono(obj);
        public static FlowNode WithStereo(this FlowNode obj) => ConfigWishes.WithStereo(obj);
        public static FlowNode WithChannels(this FlowNode obj, int? value) => ConfigWishes.WithChannels(obj, value);
        public static FlowNode SetMono(this FlowNode obj) => ConfigWishes.SetMono(obj);
        public static FlowNode SetStereo(this FlowNode obj) => ConfigWishes.SetStereo(obj);
        public static FlowNode SetChannels(this FlowNode obj, int? value) => ConfigWishes.SetChannels(obj, value);

        internal static bool IsMono  (this ConfigResolver  obj) => ConfigWishes.IsMono(obj);
        internal static bool IsStereo(this ConfigResolver  obj) => ConfigWishes.IsStereo(obj);
        internal static int Channels(this ConfigResolver obj) => ConfigWishes.Channels(obj);
        internal static int GetChannels(this ConfigResolver obj) => ConfigWishes.GetChannels(obj);

        [UsedImplicitly] internal static ConfigResolver Mono(this ConfigResolver obj) => ConfigWishes.Mono(obj);
        [UsedImplicitly] internal static ConfigResolver Stereo(this ConfigResolver obj) => ConfigWishes.Stereo(obj);
        [UsedImplicitly] internal static ConfigResolver Channels(this ConfigResolver obj, int? value) => ConfigWishes.Channels(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithMono(this ConfigResolver obj) => ConfigWishes.WithMono(obj);
        [UsedImplicitly] internal static ConfigResolver WithStereo(this ConfigResolver obj) => ConfigWishes.WithStereo(obj);
        [UsedImplicitly] internal static ConfigResolver WithChannels(this ConfigResolver obj, int? value) => ConfigWishes.WithChannels(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetMono(this ConfigResolver obj) => ConfigWishes.SetMono(obj);
        [UsedImplicitly] internal static ConfigResolver SetStereo(this ConfigResolver obj) => ConfigWishes.SetStereo(obj);
        [UsedImplicitly] internal static ConfigResolver SetChannels(this ConfigResolver obj, int? value) => ConfigWishes.SetChannels(obj, value);

        // Global-Bound

        [UsedImplicitly] internal static bool IsMono(this ConfigSection obj) => ConfigWishes.IsMono(obj);
        [UsedImplicitly] internal static bool IsStereo(this ConfigSection obj) => ConfigWishes.IsStereo(obj);
        [UsedImplicitly] internal static int? Channels(this ConfigSection obj) => ConfigWishes.Channels(obj);
        [UsedImplicitly] internal static int? GetChannels(this ConfigSection obj) => ConfigWishes.GetChannels(obj);

        // Tape Bound

        public static bool IsMono(this Tape obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this Tape obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this Tape obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this Tape obj) => ConfigWishes.GetChannels(obj);

        public static Tape Mono(this Tape obj) => ConfigWishes.Mono(obj);
        public static Tape Stereo(this Tape obj) => ConfigWishes.Stereo(obj);
        public static Tape Channels(this Tape obj, int value) => ConfigWishes.Channels(obj, value);
        public static Tape WithMono(this Tape obj) => ConfigWishes.WithMono(obj);
        public static Tape WithStereo(this Tape obj) => ConfigWishes.WithStereo(obj);
        public static Tape WithChannels(this Tape obj, int value) => ConfigWishes.WithChannels(obj, value);
        public static Tape SetMono(this Tape obj) => ConfigWishes.SetMono(obj);
        public static Tape SetStereo(this Tape obj) => ConfigWishes.SetStereo(obj);
        public static Tape SetChannels(this Tape obj, int value) => ConfigWishes.SetChannels(obj, value);

        public static bool IsMono(this TapeConfig obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this TapeConfig obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this TapeConfig obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this TapeConfig obj) => ConfigWishes.GetChannels(obj);

        public static TapeConfig Mono(this TapeConfig obj) => ConfigWishes.Mono(obj);
        public static TapeConfig Stereo(this TapeConfig obj) => ConfigWishes.Stereo(obj);
        public static TapeConfig Channels(this TapeConfig obj, int value) => ConfigWishes.Channels(obj, value);
        public static TapeConfig WithMono(this TapeConfig obj) => ConfigWishes.WithMono(obj);
        public static TapeConfig WithStereo(this TapeConfig obj) => ConfigWishes.WithStereo(obj);
        public static TapeConfig WithChannels(this TapeConfig obj, int value) => ConfigWishes.WithChannels(obj, value);
        public static TapeConfig SetMono(this TapeConfig obj) => ConfigWishes.SetMono(obj);
        public static TapeConfig SetStereo(this TapeConfig obj) => ConfigWishes.SetStereo(obj);
        public static TapeConfig SetChannels(this TapeConfig obj, int value) => ConfigWishes.SetChannels(obj, value);

        public static bool IsMono(this TapeActions obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this TapeActions obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this TapeActions obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this TapeActions obj) => ConfigWishes.GetChannels(obj);

        public static TapeActions Mono(this TapeActions obj) => ConfigWishes.Mono(obj);
        public static TapeActions Stereo(this TapeActions obj) => ConfigWishes.Stereo(obj);
        public static TapeActions Channels(this TapeActions obj, int value) => ConfigWishes.Channels(obj, value);
        public static TapeActions WithMono(this TapeActions obj) => ConfigWishes.WithMono(obj);
        public static TapeActions WithStereo(this TapeActions obj) => ConfigWishes.WithStereo(obj);
        public static TapeActions WithChannels(this TapeActions obj, int value) => ConfigWishes.WithChannels(obj, value);
        public static TapeActions SetMono(this TapeActions obj) => ConfigWishes.SetMono(obj);
        public static TapeActions SetStereo(this TapeActions obj) => ConfigWishes.SetStereo(obj);
        public static TapeActions SetChannels(this TapeActions obj, int value) => ConfigWishes.SetChannels(obj, value);

        public static bool IsMono(this TapeAction obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this TapeAction obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this TapeAction obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this TapeAction obj) => ConfigWishes.GetChannels(obj);

        public static TapeAction Mono(this TapeAction obj) => ConfigWishes.Mono(obj);
        public static TapeAction Stereo(this TapeAction obj) => ConfigWishes.Stereo(obj);
        public static TapeAction Channels(this TapeAction obj, int value) => ConfigWishes.Channels(obj, value);
        public static TapeAction SetMono(this TapeAction obj) => ConfigWishes.SetMono(obj);
        public static TapeAction SetStereo(this TapeAction obj) => ConfigWishes.SetStereo(obj);
        public static TapeAction SetChannels(this TapeAction obj, int value) => ConfigWishes.SetChannels(obj, value);
        public static TapeAction WithMono(this TapeAction obj) => ConfigWishes.WithMono(obj);
        public static TapeAction WithStereo(this TapeAction obj) => ConfigWishes.WithStereo(obj);
        public static TapeAction WithChannels(this TapeAction obj, int value) => ConfigWishes.WithChannels(obj, value);

        // Buff-Bound

        public static bool IsMono(this Buff obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this Buff obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this Buff obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this Buff obj) => ConfigWishes.GetChannels(obj);

        public static Buff Mono(this Buff obj, IContext context) => ConfigWishes.Mono(obj, context);
        public static Buff Stereo(this Buff obj, IContext context) => ConfigWishes.Stereo(obj, context);
        public static Buff Channels(this Buff obj, int value, IContext context) => ConfigWishes.Channels(obj, value, context);
        public static Buff WithMono(this Buff obj, IContext context) => ConfigWishes.WithMono(obj, context);
        public static Buff WithStereo(this Buff obj, IContext context) => ConfigWishes.WithStereo(obj, context);
        public static Buff WithChannels(this Buff obj, int value, IContext context) => ConfigWishes.WithChannels(obj, value, context);
        public static Buff SetMono(this Buff obj, IContext context) => ConfigWishes.SetMono(obj, context);
        public static Buff SetStereo(this Buff obj, IContext context) => ConfigWishes.SetStereo(obj, context);
        public static Buff SetChannels(this Buff obj, int value, IContext context) => ConfigWishes.SetChannels(obj, value, context);

        public static bool IsMono(this AudioFileOutput obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this AudioFileOutput obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this AudioFileOutput obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this AudioFileOutput obj) => ConfigWishes.GetChannels(obj);

        public static AudioFileOutput Mono(this AudioFileOutput obj, IContext context) => ConfigWishes.Mono(obj, context);
        public static AudioFileOutput Stereo(this AudioFileOutput obj, IContext context) => ConfigWishes.Stereo(obj, context);
        public static AudioFileOutput Channels(this AudioFileOutput obj, int value, IContext context) => ConfigWishes.Channels(obj, value, context);
        public static AudioFileOutput WithMono(this AudioFileOutput obj, IContext context) => ConfigWishes.WithMono(obj, context);
        public static AudioFileOutput WithStereo(this AudioFileOutput obj, IContext context) => ConfigWishes.WithStereo(obj, context);
        public static AudioFileOutput WithChannels(this AudioFileOutput obj, int value, IContext context) => ConfigWishes.WithChannels(obj, value, context);
        public static AudioFileOutput SetMono(this AudioFileOutput obj, IContext context) => ConfigWishes.SetMono(obj, context);
        public static AudioFileOutput SetStereo(this AudioFileOutput obj, IContext context) => ConfigWishes.SetStereo(obj, context);
        public static AudioFileOutput SetChannels(this AudioFileOutput obj, int value, IContext context) => ConfigWishes.SetChannels(obj, value, context);

        // Independent after Taping

        public static bool IsMono(this Sample obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this Sample obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this Sample obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this Sample obj) => ConfigWishes.GetChannels(obj);

        public static Sample Mono(this Sample obj, IContext context) => ConfigWishes.Mono(obj, context);
        public static Sample Stereo(this Sample obj, IContext context) => ConfigWishes.Stereo(obj, context);
        public static Sample Channels(this Sample obj, int value, IContext context) => ConfigWishes.Channels(obj, value, context);
        public static Sample WithMono(this Sample obj, IContext context) => ConfigWishes.WithMono(obj, context);
        public static Sample WithStereo(this Sample obj, IContext context) => ConfigWishes.WithStereo(obj, context);
        public static Sample WithChannels(this Sample obj, int value, IContext context) => ConfigWishes.WithChannels(obj, value, context);
        public static Sample SetMono(this Sample obj, IContext context) => ConfigWishes.SetMono(obj, context);
        public static Sample SetStereo(this Sample obj, IContext context) => ConfigWishes.SetStereo(obj, context);
        public static Sample SetChannels(this Sample obj, int value, IContext context) => ConfigWishes.SetChannels(obj, value, context);

        public static bool IsMono(this AudioInfoWish obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this AudioInfoWish obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this AudioInfoWish obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this AudioInfoWish obj) => ConfigWishes.GetChannels(obj);

        public static AudioInfoWish Mono(this AudioInfoWish obj) => ConfigWishes.Mono(obj);
        public static AudioInfoWish Stereo(this AudioInfoWish obj) => ConfigWishes.Stereo(obj);
        public static AudioInfoWish Channels(this AudioInfoWish obj, int value) => ConfigWishes.Channels(obj, value);
        public static AudioInfoWish WithMono(this AudioInfoWish obj) => ConfigWishes.WithMono(obj);
        public static AudioInfoWish WithStereo(this AudioInfoWish obj) => ConfigWishes.WithStereo(obj);
        public static AudioInfoWish WithChannels(this AudioInfoWish obj, int value) => ConfigWishes.WithChannels(obj, value);
        public static AudioInfoWish SetMono(this AudioInfoWish obj) => ConfigWishes.SetMono(obj);
        public static AudioInfoWish SetStereo(this AudioInfoWish obj) => ConfigWishes.SetStereo(obj);
        public static AudioInfoWish SetChannels(this AudioInfoWish obj, int value) => ConfigWishes.SetChannels(obj, value);

        public static bool IsMono(this AudioFileInfo obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this AudioFileInfo obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this AudioFileInfo obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this AudioFileInfo obj) => ConfigWishes.GetChannels(obj);

        public static AudioFileInfo Mono(this AudioFileInfo obj) => ConfigWishes.Mono(obj);
        public static AudioFileInfo Stereo(this AudioFileInfo obj) => ConfigWishes.Stereo(obj);
        public static AudioFileInfo Channels(this AudioFileInfo obj, int value) => ConfigWishes.Channels(obj, value);
        public static AudioFileInfo WithMono(this AudioFileInfo obj) => ConfigWishes.WithMono(obj);
        public static AudioFileInfo WithStereo(this AudioFileInfo obj) => ConfigWishes.WithStereo(obj);
        public static AudioFileInfo WithChannels(this AudioFileInfo obj, int value) => ConfigWishes.WithChannels(obj, value);
        public static AudioFileInfo SetMono(this AudioFileInfo obj) => ConfigWishes.SetMono(obj);
        public static AudioFileInfo SetStereo(this AudioFileInfo obj) => ConfigWishes.SetStereo(obj);
        public static AudioFileInfo SetChannels(this AudioFileInfo obj, int value) => ConfigWishes.SetChannels(obj, value);

        // Immutable

        public static bool IsMono(this WavHeaderStruct obj) => ConfigWishes.IsMono(obj);
        public static bool IsStereo(this WavHeaderStruct obj) => ConfigWishes.IsStereo(obj);
        public static int Channels(this WavHeaderStruct obj) => ConfigWishes.Channels(obj);
        public static int GetChannels(this WavHeaderStruct obj) => ConfigWishes.GetChannels(obj);

        public static WavHeaderStruct Mono(this WavHeaderStruct obj) => ConfigWishes.Mono(obj);
        public static WavHeaderStruct Stereo(this WavHeaderStruct obj) => ConfigWishes.Stereo(obj);
        public static WavHeaderStruct Channels(this WavHeaderStruct obj, int value) => ConfigWishes.Channels(obj, value);
        public static WavHeaderStruct WithMono(this WavHeaderStruct obj) => ConfigWishes.WithMono(obj);
        public static WavHeaderStruct WithStereo(this WavHeaderStruct obj) => ConfigWishes.WithStereo(obj);
        public static WavHeaderStruct WithChannels(this WavHeaderStruct obj, int value) => ConfigWishes.WithChannels(obj, value);
        public static WavHeaderStruct SetMono(this WavHeaderStruct obj) => ConfigWishes.SetMono(obj);
        public static WavHeaderStruct SetStereo(this WavHeaderStruct obj) => ConfigWishes.SetStereo(obj);
        public static WavHeaderStruct SetChannels(this WavHeaderStruct obj, int value) => ConfigWishes.SetChannels(obj, value);

        [Obsolete(ObsoleteMessage)] public static bool IsMono(this SpeakerSetupEnum obj) => ConfigWishes.IsMono(obj);
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this SpeakerSetupEnum obj) => ConfigWishes.IsStereo(obj);
        [Obsolete(ObsoleteMessage)] public static int Channels(this SpeakerSetupEnum obj) => ConfigWishes.Channels(obj);
        [Obsolete(ObsoleteMessage)] public static int GetChannels(this SpeakerSetupEnum obj) => ConfigWishes.GetChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int ToChannels(this SpeakerSetupEnum enumValue) => ConfigWishes.ToChannels(enumValue);
        [Obsolete(ObsoleteMessage)] public static int AsChannels(this SpeakerSetupEnum enumValue) => ConfigWishes.AsChannels(enumValue);
        [Obsolete(ObsoleteMessage)] public static int EnumToChannels(this SpeakerSetupEnum enumValue) => ConfigWishes.EnumToChannels(enumValue);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Mono(this SpeakerSetupEnum oldEnumValue) => ConfigWishes.Mono(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Stereo(this SpeakerSetupEnum oldEnumValue) => ConfigWishes.Stereo(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Channels(this SpeakerSetupEnum oldEnumValue, int newChannels) => ConfigWishes.Channels(oldEnumValue, newChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum WithMono(this SpeakerSetupEnum oldEnumValue) => ConfigWishes.WithMono(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum WithStereo(this SpeakerSetupEnum oldEnumValue) => ConfigWishes.WithStereo(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum WithChannels(this SpeakerSetupEnum oldEnumValue, int newChannels) => ConfigWishes.WithChannels(oldEnumValue, newChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum SetMono(this SpeakerSetupEnum oldEnumValue) => ConfigWishes.SetMono(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum SetStereo(this SpeakerSetupEnum oldEnumValue) => ConfigWishes.SetStereo(oldEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum SetChannels(this SpeakerSetupEnum oldEnumValue, int newChannels) => ConfigWishes.SetChannels(oldEnumValue, newChannels);
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum ToEnum(this int channels) => ConfigWishes.ToEnum(channels);
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum ChannelsToEnum(this int channels) => ConfigWishes.ChannelsToEnum(channels);

        [Obsolete(ObsoleteMessage)] public static bool IsMono(this SpeakerSetup obj) => ConfigWishes.IsMono(obj);
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this SpeakerSetup obj) => ConfigWishes.IsStereo(obj);
        [Obsolete(ObsoleteMessage)] public static int Channels(this SpeakerSetup obj) => ConfigWishes.Channels(obj);
        [Obsolete(ObsoleteMessage)] public static int GetChannels(this SpeakerSetup obj) => ConfigWishes.GetChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int ToChannels(this SpeakerSetup obj) => ConfigWishes.ToChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int AsChannels(this SpeakerSetup obj) => ConfigWishes.AsChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int EntityToChannels(this SpeakerSetup entity) => ConfigWishes.EntityToChannels(entity);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Mono(this SpeakerSetup oldSpeakerSetup, IContext context) => ConfigWishes.Mono(oldSpeakerSetup, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Stereo(this SpeakerSetup oldSpeakerSetup, IContext context) => ConfigWishes.Stereo(oldSpeakerSetup, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Channels(this SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ConfigWishes.Channels(oldSpeakerSetup, newChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup WithMono(this SpeakerSetup oldSpeakerSetup, IContext context) => ConfigWishes.WithMono(oldSpeakerSetup, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup WithStereo(this SpeakerSetup oldSpeakerSetup, IContext context) => ConfigWishes.WithStereo(oldSpeakerSetup, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup WithChannels(this SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ConfigWishes.WithChannels(oldSpeakerSetup, newChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup SetMono(this SpeakerSetup oldSpeakerSetup, IContext context) => ConfigWishes.SetMono(oldSpeakerSetup, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup SetStereo(this SpeakerSetup oldSpeakerSetup, IContext context) => ConfigWishes.SetStereo(oldSpeakerSetup, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup SetChannels(this SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ConfigWishes.SetChannels(oldSpeakerSetup, newChannels, context);
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup ChannelsToEntity(this int channels, IContext context) => ConfigWishes.ChannelsToEntity(channels, context);

        // TODO: More Channel(Enum) extensions

        [Obsolete(ObsoleteMessage)] public static bool IsMono(this ChannelEnum obj) => ConfigWishes.IsMono(obj);
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this ChannelEnum obj) => ConfigWishes.IsStereo(obj);

        [Obsolete(ObsoleteMessage)] public static bool IsMono(this Channel obj) => ConfigWishes.IsMono(obj);
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this Channel obj) => ConfigWishes.IsStereo(obj);
    }

    public partial class ConfigWishes
    {
        // Constants
    
        public const int NoChannels = 0;
        public const int MonoChannels = 1;
        public const int StereoChannels = 2;

        // Synth-Bound

        public static bool IsMono(SynthWishes obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(SynthWishes obj) => obj.Channels() == StereoChannels;
        public static int Channels(SynthWishes obj) => obj.GetChannels();
        public static int GetChannels(SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }

        public static SynthWishes Mono(SynthWishes obj) => obj.Channels(MonoChannels);
        public static SynthWishes Stereo(SynthWishes obj) => obj.Channels(StereoChannels);
        public static SynthWishes Channels(SynthWishes obj, int? value) => obj.SetChannels(value);
        public static SynthWishes WithMono(SynthWishes obj) => obj.Channels(MonoChannels);
        public static SynthWishes WithStereo(SynthWishes obj) => obj.Channels(StereoChannels);
        public static SynthWishes WithChannels(SynthWishes obj, int? value) => obj.SetChannels(value);
        public static SynthWishes SetMono(SynthWishes obj) => obj.Channels(MonoChannels);
        public static SynthWishes SetStereo(SynthWishes obj) => obj.Channels(StereoChannels);
        public static SynthWishes SetChannels(SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }

        public static bool IsMono(FlowNode obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(FlowNode obj) => obj.Channels() == StereoChannels;
        public static int Channels(FlowNode obj) => obj.GetChannels();
        public static int GetChannels(FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static FlowNode Mono(FlowNode obj) => obj.Channels(MonoChannels);
        public static FlowNode Stereo(FlowNode obj) => obj.Channels(StereoChannels);
        public static FlowNode Channels(FlowNode obj, int? value) => obj.SetChannels(value);
        public static FlowNode WithMono(FlowNode obj) => obj.Channels(MonoChannels);
        public static FlowNode WithStereo(FlowNode obj) => obj.Channels(StereoChannels);
        public static FlowNode WithChannels(FlowNode obj, int? value) => obj.SetChannels(value);
        public static FlowNode SetMono(FlowNode obj) => obj.Channels(MonoChannels);
        public static FlowNode SetStereo(FlowNode obj) => obj.Channels(StereoChannels);
        public static FlowNode SetChannels(FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        internal static bool IsMono(ConfigResolver obj) => obj.Channels() == MonoChannels;
        internal static bool IsStereo(ConfigResolver obj) => obj.Channels() == StereoChannels;
        internal static int Channels(ConfigResolver obj) => obj.GetChannels();
        internal static int GetChannels(ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        internal static ConfigResolver Mono(ConfigResolver obj) => obj.Channels(MonoChannels);
        internal static ConfigResolver Stereo(ConfigResolver obj) => obj.Channels(StereoChannels);
        internal static ConfigResolver Channels(ConfigResolver obj, int? value) => obj.SetChannels(value);
        [UsedImplicitly]
        internal static ConfigResolver WithMono(ConfigResolver obj) => obj.Channels(MonoChannels);
        [UsedImplicitly]
        internal static ConfigResolver WithStereo(ConfigResolver obj) => obj.Channels(StereoChannels);
        internal static ConfigResolver WithChannels(ConfigResolver obj, int? value) => obj.SetChannels(value);
        [UsedImplicitly]
        internal static ConfigResolver SetMono(ConfigResolver obj) => obj.Channels(MonoChannels);
        [UsedImplicitly]
        internal static ConfigResolver SetStereo(ConfigResolver obj) => obj.Channels(StereoChannels);
        internal static ConfigResolver SetChannels(ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        // Global-Bound
        
        [UsedImplicitly]
        internal static bool IsMono(ConfigSection obj) => obj.Channels() == MonoChannels;
        [UsedImplicitly]
        internal static bool IsStereo(ConfigSection obj) => obj.Channels() == StereoChannels;
        internal static int? Channels(ConfigSection obj) => obj.GetChannels();
        internal static int? GetChannels(ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
        
        // Tape Bound
        
        public static bool IsMono(Tape obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(Tape obj) => obj.Channels() == StereoChannels;
        public static int Channels(Tape obj) => obj.GetChannels();
        public static int GetChannels(Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Channels;
        }
        
        public static Tape Mono(Tape obj) => obj.Channels(MonoChannels);
        public static Tape Stereo(Tape obj) => obj.Channels(StereoChannels);
        public static Tape Channels(Tape obj, int value) => obj.SetChannels(value);
        public static Tape WithMono(Tape obj) => obj.Channels(MonoChannels);
        public static Tape WithStereo(Tape obj) => obj.Channels(StereoChannels);
        public static Tape WithChannels(Tape obj, int value) => obj.SetChannels(value);
        public static Tape SetMono(Tape obj) => obj.Channels(MonoChannels);
        public static Tape SetStereo(Tape obj) => obj.Channels(StereoChannels);
        public static Tape SetChannels(Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Channels = value;
            return obj;
        }
        
        public static bool IsMono(TapeConfig obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(TapeConfig obj) => obj.Channels() == StereoChannels;
        public static int Channels(TapeConfig obj) => obj.GetChannels();
        public static int GetChannels(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
        
        public static TapeConfig Mono(TapeConfig obj) => obj.Channels(MonoChannels);
        public static TapeConfig Stereo(TapeConfig obj) => obj.Channels(StereoChannels);
        public static TapeConfig Channels(TapeConfig obj, int value) => obj.SetChannels(value);
        public static TapeConfig WithMono(TapeConfig obj) => obj.Channels(MonoChannels);
        public static TapeConfig WithStereo(TapeConfig obj) => obj.Channels(StereoChannels);
        public static TapeConfig WithChannels(TapeConfig obj, int value) => obj.SetChannels(value);
        public static TapeConfig SetMono(TapeConfig obj) => obj.Channels(MonoChannels);
        public static TapeConfig SetStereo(TapeConfig obj) => obj.Channels(StereoChannels);
        public static TapeConfig SetChannels(TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channels = value;
            return obj;
        }
        
        public static bool IsMono(TapeActions obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(TapeActions obj) => obj.Channels() == StereoChannels;
        public static int Channels(TapeActions obj) => obj.GetChannels();
        public static int GetChannels(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channels;
        }
        
        
        public static TapeActions Mono(TapeActions obj) => obj.Channels(MonoChannels);
        public static TapeActions Stereo(TapeActions obj) => obj.Channels(StereoChannels);
        public static TapeActions Channels(TapeActions obj, int value) => obj.SetChannels(value);
        public static TapeActions WithMono(TapeActions obj) => obj.Channels(MonoChannels);
        public static TapeActions WithStereo(TapeActions obj) => obj.Channels(StereoChannels);
        public static TapeActions WithChannels(TapeActions obj, int value) => obj.SetChannels(value);
        public static TapeActions SetMono(TapeActions obj) => obj.Channels(MonoChannels);
        public static TapeActions SetStereo(TapeActions obj) => obj.Channels(StereoChannels);
        public static TapeActions SetChannels(TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channels = value;
            return obj;
        }
        
        public static bool IsMono(TapeAction obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(TapeAction obj) => obj.Channels() == StereoChannels;
        public static int Channels(TapeAction obj) => obj.GetChannels();
        public static int GetChannels(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channels;
        }
        
        public static TapeAction Mono(TapeAction obj) => obj.Channels(MonoChannels);
        public static TapeAction Stereo(TapeAction obj) => obj.Channels(StereoChannels);
        public static TapeAction Channels(TapeAction obj, int value) => obj.SetChannels(value);
        public static TapeAction WithMono(TapeAction obj) => obj.Channels(MonoChannels);
        public static TapeAction WithStereo(TapeAction obj) => obj.Channels(StereoChannels);
        public static TapeAction WithChannels(TapeAction obj, int value) => obj.SetChannels(value);
        public static TapeAction SetMono(TapeAction obj) => obj.Channels(MonoChannels);
        public static TapeAction SetStereo(TapeAction obj) => obj.Channels(StereoChannels);
        public static TapeAction SetChannels(TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channels = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static bool IsMono(Buff obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(Buff obj) => obj.Channels() == StereoChannels;
        public static int Channels(Buff obj) => obj.GetChannels();
        public static int GetChannels(Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) return NoChannels;
            return obj.UnderlyingAudioFileOutput.Channels();
        }
        
        public static Buff Mono(Buff obj, IContext context) => obj.Channels(MonoChannels, context);
        public static Buff Stereo(Buff obj, IContext context) => obj.Channels(StereoChannels, context);
        public static Buff Channels(Buff obj, int value, IContext context) => obj.SetChannels(value, context);
        public static Buff WithMono(Buff obj, IContext context) => obj.Channels(MonoChannels, context);
        public static Buff WithStereo(Buff obj, IContext context) => obj.Channels(StereoChannels, context);
        public static Buff WithChannels(Buff obj, int value, IContext context) => obj.SetChannels(value, context);
        public static Buff SetMono(Buff obj, IContext context) => obj.Channels(MonoChannels, context);
        public static Buff SetStereo(Buff obj, IContext context) => obj.Channels(StereoChannels, context);
        public static Buff SetChannels(Buff obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.Channels(value, context);
            return obj;
        }
        
        public static bool IsMono(AudioFileOutput obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(AudioFileOutput obj) => obj.Channels() == StereoChannels;
        public static int Channels(AudioFileOutput obj) => obj.GetChannels();
        public static int GetChannels(AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannelCount();
        }
        
        public static AudioFileOutput Mono(AudioFileOutput obj, IContext context) => obj.Channels(MonoChannels, context);
        public static AudioFileOutput Stereo(AudioFileOutput obj, IContext context) => obj.Channels(StereoChannels, context);
        public static AudioFileOutput Channels(AudioFileOutput obj, int value, IContext context) => obj.SetChannels(value, context);
        public static AudioFileOutput WithMono(AudioFileOutput obj, IContext context) => obj.Channels(MonoChannels, context);
        public static AudioFileOutput WithStereo(AudioFileOutput obj, IContext context) => obj.Channels(StereoChannels, context);
        public static AudioFileOutput WithChannels(AudioFileOutput obj, int value, IContext context) => obj.SetChannels(value, context);
        public static AudioFileOutput SetMono(AudioFileOutput obj, IContext context) => obj.Channels(MonoChannels, context);
        public static AudioFileOutput SetStereo(AudioFileOutput obj, IContext context) => obj.Channels(StereoChannels, context);
        public static AudioFileOutput SetChannels(AudioFileOutput obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(value, context);
            // Do not adjust channels, to accommodate Left-Only and Right-Only scenarios with 1 channel, but Stereo speaker setup.
            //CreateOrRemoveChannels(obj, value, context); 
            return obj;
        }
        
        // Independent after Taping
        
        public static bool IsMono(Sample obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(Sample obj) => obj.Channels() == StereoChannels;
        public static int Channels(Sample obj) => obj.GetChannels();
        public static int GetChannels(Sample obj) 
            => obj.GetChannelCount();
        
        public static Sample Mono(Sample obj, IContext context) => obj.Channels(MonoChannels, context);
        public static Sample Stereo(Sample obj, IContext context) => obj.Channels(StereoChannels, context);
        public static Sample Channels(Sample obj, int value, IContext context) => obj.SetChannels(value, context);
        public static Sample WithMono(Sample obj, IContext context) => obj.Channels(MonoChannels, context);
        public static Sample WithChannels(Sample obj, int value, IContext context) => obj.SetChannels(value, context);
        public static Sample SetMono(Sample obj, IContext context) => obj.Channels(MonoChannels, context);
        public static Sample SetStereo(Sample obj, IContext context) => obj.Channels(StereoChannels, context);
        public static Sample SetChannels(Sample obj, int value, IContext context)
        {
            obj.SetSpeakerSetupEnum(value.ChannelsToEnum(), context);
            return obj;
        }
                         
        public static bool IsMono(AudioInfoWish obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(AudioInfoWish obj) => obj.Channels() == StereoChannels;
        public static int Channels(AudioInfoWish obj) => obj.GetChannels();
        public static int GetChannels(AudioInfoWish obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
       
        public static AudioInfoWish Mono(AudioInfoWish obj) => obj.Channels(MonoChannels);
        public static AudioInfoWish Stereo(AudioInfoWish obj) => obj.Channels(StereoChannels);
        public static AudioInfoWish Channels(AudioInfoWish obj, int value) => obj.SetChannels(value);
        public static AudioInfoWish WithMono(AudioInfoWish obj) => obj.Channels(MonoChannels);
        public static AudioInfoWish WithStereo(AudioInfoWish obj) => obj.Channels(StereoChannels);
        public static AudioInfoWish WithChannels(AudioInfoWish obj, int value) => obj.SetChannels(value);
        public static AudioInfoWish SetMono(AudioInfoWish obj) => obj.Channels(MonoChannels);
        public static AudioInfoWish SetStereo(AudioInfoWish obj) => obj.Channels(StereoChannels);
        public static AudioInfoWish SetChannels(AudioInfoWish obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channels = AssertChannels(value);
            return obj;
        }
        
        public static bool IsMono(AudioFileInfo obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(AudioFileInfo obj) => obj.Channels() == StereoChannels;
        public static int Channels(AudioFileInfo obj) => obj.GetChannels();
        public static int GetChannels(AudioFileInfo obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.ChannelCount;
        }
        
        public static AudioFileInfo Mono(AudioFileInfo obj) => obj.Channels(MonoChannels);
        public static AudioFileInfo Stereo(AudioFileInfo obj) => obj.Channels(StereoChannels);
        public static AudioFileInfo Channels(AudioFileInfo obj, int value) => obj.SetChannels(value);
        public static AudioFileInfo WithMono(AudioFileInfo obj) => obj.Channels(MonoChannels);
        public static AudioFileInfo WithStereo(AudioFileInfo obj) => obj.Channels(StereoChannels);
        public static AudioFileInfo WithChannels(AudioFileInfo obj, int value) => obj.SetChannels(value);
        public static AudioFileInfo SetMono(AudioFileInfo obj) => obj.Channels(MonoChannels);
        public static AudioFileInfo SetStereo(AudioFileInfo obj) => obj.Channels(StereoChannels);
        public static AudioFileInfo SetChannels(AudioFileInfo obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.ChannelCount = AssertChannels(value);
            return obj;
        }

        // Immutable
        
        public static bool IsMono(WavHeaderStruct obj) => obj.Channels() == MonoChannels;
        public static bool IsStereo(WavHeaderStruct obj) => obj.Channels() == StereoChannels;
        public static int Channels(WavHeaderStruct obj) => obj.GetChannels();
        public static int GetChannels(WavHeaderStruct obj) 
            => obj.ChannelCount;
        
        public static WavHeaderStruct Mono(WavHeaderStruct obj) => obj.Channels(MonoChannels);
        public static WavHeaderStruct Stereo(WavHeaderStruct obj) => obj.Channels(StereoChannels);
        public static WavHeaderStruct Channels(WavHeaderStruct obj, int value) => obj.SetChannels(value);
        public static WavHeaderStruct WithMono(WavHeaderStruct obj) => obj.Channels(MonoChannels);
        public static WavHeaderStruct WithStereo(WavHeaderStruct obj) => obj.Channels(StereoChannels);
        public static WavHeaderStruct WithChannels(WavHeaderStruct obj, int value) => obj.SetChannels(value);
        public static WavHeaderStruct SetMono(WavHeaderStruct obj) => obj.Channels(MonoChannels);
        public static WavHeaderStruct SetStereo(WavHeaderStruct obj) => obj.Channels(StereoChannels);
        public static WavHeaderStruct SetChannels(WavHeaderStruct obj, int value) 
            => obj.ToWish().Channels(value).ToWavHeader();
        
        [Obsolete(ObsoleteMessage)] public static bool IsMono(SpeakerSetupEnum obj) => obj == SpeakerSetupEnum.Mono;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(SpeakerSetupEnum obj) => obj == SpeakerSetupEnum.Stereo;
        [Obsolete(ObsoleteMessage)] public static int Channels(SpeakerSetupEnum    obj)       => EnumToChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int GetChannels(SpeakerSetupEnum obj)       => EnumToChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int ToChannels(SpeakerSetupEnum  enumValue) => EnumToChannels(enumValue);
        [Obsolete(ObsoleteMessage)] public static int AsChannels(SpeakerSetupEnum  enumValue) => EnumToChannels(enumValue);
        [Obsolete(ObsoleteMessage)] public static int EnumToChannels(SpeakerSetupEnum enumValue)
        {
            switch (enumValue)
            {
                case SpeakerSetupEnum.Mono: return MonoChannels;
                case SpeakerSetupEnum.Stereo: return StereoChannels;
                case SpeakerSetupEnum.Undefined: return NoChannels;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Mono(SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Stereo(SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum Channels(SpeakerSetupEnum oldEnumValue, int newChannels) => ChannelsToEnum(newChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum WithMono(SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum WithStereo(SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum WithChannels(SpeakerSetupEnum oldEnumValue, int newChannels) => ChannelsToEnum(newChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum SetMono(SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum SetStereo(SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum SetChannels(SpeakerSetupEnum oldEnumValue, int newChannels) => ChannelsToEnum(newChannels);
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum ToEnum(int channels) => ChannelsToEnum(channels);
        [Obsolete(ObsoleteMessage)] public static SpeakerSetupEnum ChannelsToEnum(int channels)
        {
            switch (AssertChannels((int?)channels))
            {
                case NoChannels: return SpeakerSetupEnum.Undefined;
                case MonoChannels: return SpeakerSetupEnum.Mono;
                case StereoChannels: return SpeakerSetupEnum.Stereo;
                default: return default; // ncrunch: no coverage
            }
        }

        [Obsolete(ObsoleteMessage)] public static bool IsMono(SpeakerSetup obj) => obj.ToEnum().IsMono();
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(SpeakerSetup obj) => obj.ToEnum().IsStereo();
        [Obsolete(ObsoleteMessage)] public static int Channels(SpeakerSetup obj) => EntityToChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int GetChannels(SpeakerSetup obj) => EntityToChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int ToChannels(SpeakerSetup obj) => EntityToChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int AsChannels(SpeakerSetup obj) => EntityToChannels(obj);
        [Obsolete(ObsoleteMessage)] public static int EntityToChannels(SpeakerSetup entity) => entity.ToEnum().EnumToChannels();
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Mono(SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(MonoChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Stereo(SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(StereoChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup Channels(SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ChannelsToEntity(newChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup WithMono(SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(MonoChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup WithStereo(SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(StereoChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup WithChannels(SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ChannelsToEntity(newChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup SetMono(SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(MonoChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup SetStereo(SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(StereoChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup SetChannels(SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ChannelsToEntity(newChannels, context);
        [Obsolete(ObsoleteMessage)] public static SpeakerSetup ChannelsToEntity(int channels, IContext context)
        {
            return channels.ChannelsToEnum().ToEntity(context);
        }
        
        // TODO: More Channel(Enum) helpers

        [Obsolete(ObsoleteMessage)] public static bool IsMono(ChannelEnum obj) => obj == ChannelEnum.Single;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(Channel obj) => obj.ToEnum().IsMono();

        [Obsolete(ObsoleteMessage)] public static bool IsStereo(ChannelEnum obj) => obj == ChannelEnum.Left || obj == ChannelEnum.Right || obj == ChannelEnum.Undefined; // Undefined = stereo signal with 2 channels = not a specific channel
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(Channel obj) => obj.ToEnum().IsStereo();





    }
}