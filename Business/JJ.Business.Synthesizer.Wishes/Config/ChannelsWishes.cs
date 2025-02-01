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
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

#pragma warning disable CS0618

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ChannelsExtensionWishes
    {
        // A Primary Audio Attribute
        
        // Synth-Bound
        
        public static int Channels(this SynthWishes obj) => obj.GetChannels();
        public static int GetChannels(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static SynthWishes Channels(this SynthWishes obj, int? value) => obj.SetChannels(value);
        public static SynthWishes SetChannels(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        public static int Channels(this FlowNode obj) => obj.GetChannels();
        public static int GetChannels(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        public static FlowNode Channels(this FlowNode obj, int? value) => obj.SetChannels(value);
        public static FlowNode SetChannels(this FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        internal static int Channels(this ConfigResolver obj) => obj.GetChannels();
        internal static int GetChannels(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannels;
        }
        
        internal static ConfigResolver Channels(this ConfigResolver obj, int? value) => obj.SetChannels(value);
        internal static ConfigResolver SetChannels(this ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannels(value);
        }
        
        // Global-Bound
        
        internal static int? Channels(this ConfigSection obj) => obj.GetChannels();
        internal static int? GetChannels(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
        
        // Tape Bound
        
        public static int Channels(this Tape obj) => obj.GetChannels();
        public static int GetChannels(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Channels;
        }
        
        public static Tape Channels(this Tape obj, int value) => obj.SetChannels(value);
        public static Tape WithChannels(this Tape obj, int value) => obj.SetChannels(value);
        public static Tape SetChannels(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Channels = value;
            return obj;
        }
        
        public static int Channels(this TapeConfig obj) => obj.GetChannels();
        public static int GetChannels(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
        
        public static TapeConfig Channels(this TapeConfig obj, int value) => obj.SetChannels(value);
        public static TapeConfig WithChannels(this TapeConfig obj, int value) => obj.SetChannels(value);
        public static TapeConfig SetChannels(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channels = value;
            return obj;
        }
        
        public static int Channels(this TapeActions obj) => obj.GetChannels();
        public static int GetChannels(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channels;
        }
        
        
        public static TapeActions Channels(this TapeActions obj, int value) => obj.SetChannels(value);
        public static TapeActions WithChannels(this TapeActions obj, int value) => obj.SetChannels(value);
        public static TapeActions SetChannels(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channels = value;
            return obj;
        }
        
        public static int Channels(this TapeAction obj) => obj.GetChannels();
        public static int GetChannels(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channels;
        }
        
        public static TapeAction Channels(this TapeAction obj, int value) => obj.SetChannels(value);
        public static TapeAction WithChannels(this TapeAction obj, int value) => obj.SetChannels(value);
        public static TapeAction SetChannels(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channels = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static int Channels(this Buff obj) => obj.GetChannels();
        public static int GetChannels(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) return NoChannels;
            return obj.UnderlyingAudioFileOutput.Channels();
        }
        
        public static Buff Channels(this Buff obj, int value, IContext context) => obj.SetChannels(value, context);
        public static Buff WithChannels(this Buff obj, int value, IContext context) => obj.SetChannels(value, context);
        public static Buff SetChannels(this Buff obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) throw new NullException(() => obj);
            obj.UnderlyingAudioFileOutput.Channels(value, context);
            return obj;
        }
        
        public static int Channels(this AudioFileOutput obj) => obj.GetChannels();
        public static int GetChannels(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannelCount();
        }
        
        public static AudioFileOutput Channels(this AudioFileOutput obj, int value, IContext context) => obj.SetChannels(value, context);
        public static AudioFileOutput WithChannels(this AudioFileOutput obj, int value, IContext context) => obj.SetChannels(value, context);
        public static AudioFileOutput SetChannels(this AudioFileOutput obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(value, context);
            // Do not adjust channels, to accommodate Left-Only and Right-Only scenarios with 1 channel, but Stereo speaker setup.
            //CreateOrRemoveChannels(obj, value, context); 
            return obj;
        }
        
        // Independent after Taping
        
        public static int Channels(this Sample obj) => obj.GetChannels();
        public static int GetChannels(this Sample obj) 
            => obj.GetChannelCount();
        
        public static Sample Channels(this Sample obj, int value, IContext context) => obj.SetChannels(value, context);
        public static Sample WithChannels(this Sample obj, int value, IContext context) => obj.SetChannels(value, context);
        public static Sample SetChannels(this Sample obj, int value, IContext context)
        {
            obj.SetSpeakerSetupEnum(value.ChannelsToEnum(), context);
            return obj;
        }
                         
        public static int Channels(this AudioInfoWish obj) => obj.GetChannels();
        public static int GetChannels(this AudioInfoWish obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channels;
        }
       
        public static AudioInfoWish Channels(this AudioInfoWish obj, int value) => obj.SetChannels(value);
        public static AudioInfoWish WithChannels(this AudioInfoWish obj, int value) => obj.SetChannels(value);
        public static AudioInfoWish SetChannels(this AudioInfoWish obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channels = AssertChannels(value);
            return obj;
        }
        
        public static int Channels(this AudioFileInfo obj) => obj.GetChannels();
        public static int GetChannels(this AudioFileInfo obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.ChannelCount;
        }
        
        public static AudioFileInfo Channels(this AudioFileInfo obj, int value) => obj.SetChannels(value);
        public static AudioFileInfo WithChannels(this AudioFileInfo obj, int value) => obj.SetChannels(value);
        public static AudioFileInfo SetChannels(this AudioFileInfo obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.ChannelCount = AssertChannels(value);
            return obj;
        }

        // Immutable
        
        public static int Channels(this WavHeaderStruct obj) => obj.GetChannels();
        public static int GetChannels(this WavHeaderStruct obj) 
            => obj.ChannelCount;
        
        public static WavHeaderStruct Channels(this WavHeaderStruct obj, int value) => obj.SetChannels(value);
        public static WavHeaderStruct WithChannels(this WavHeaderStruct obj, int value) => obj.SetChannels(value);
        public static WavHeaderStruct SetChannels(this WavHeaderStruct obj, int value) 
            => obj.ToWish().Channels(value).ToWavHeader();
        
        [Obsolete(ObsoleteMessage)]
        public static int EnumToChannels(this SpeakerSetupEnum enumValue) => ConfigWishes.EnumToChannels(enumValue);
        [Obsolete(ObsoleteMessage)]
        public static int Channels(this SpeakerSetupEnum obj) => ConfigWishes.Channels(obj);
        [Obsolete(ObsoleteMessage)]
        public static int GetChannels(this SpeakerSetupEnum obj) => ConfigWishes.GetChannels(obj);
        [Obsolete(ObsoleteMessage)]
        public static int ToChannels(this SpeakerSetupEnum enumValue) => ConfigWishes.ToChannels(enumValue);

        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum ChannelsToEnum(this int channels) => ConfigWishes.ChannelsToEnum(channels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum Channels(this SpeakerSetupEnum oldEnumValue, int newChannels) => ConfigWishes.Channels(oldEnumValue, newChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum SetChannels(this SpeakerSetupEnum oldEnumValue, int newChannels) => ConfigWishes.SetChannels(oldEnumValue, newChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum WithChannels(this SpeakerSetupEnum oldEnumValue, int newChannels) => ConfigWishes.WithChannels(oldEnumValue, newChannels);
        
        [Obsolete(ObsoleteMessage)] 
        public static int EntityToChannels(this SpeakerSetup entity) => ConfigWishes.EntityToChannels(entity);
        [Obsolete(ObsoleteMessage)] 
        public static int Channels(this SpeakerSetup obj) => ConfigWishes.Channels(obj);
        [Obsolete(ObsoleteMessage)] 
        public static int GetChannels(this SpeakerSetup obj) => ConfigWishes.GetChannels(obj);
        [Obsolete(ObsoleteMessage)] 
        public static int ToChannels(this SpeakerSetup obj) => ConfigWishes.ToChannels(obj);
        
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetup ChannelsToEntity(this int channels, IContext context) => ConfigWishes.ChannelsToEntity(channels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup Channels(this SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ConfigWishes.Channels(oldSpeakerSetup, newChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup SetChannels(this SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ConfigWishes.SetChannels(oldSpeakerSetup, newChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup WithChannels(this SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ConfigWishes.WithChannels(oldSpeakerSetup, newChannels, context);
        
        // Channels Shorthand
        
        public   static bool IsMono  (this SynthWishes     obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this FlowNode        obj) => obj.Channels() == MonoChannels;
        internal static bool IsMono  (this ConfigResolver  obj) => obj.Channels() == MonoChannels;
        [UsedImplicitly]
        internal static bool IsMono  (this ConfigSection   obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this Tape            obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this TapeConfig      obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this TapeActions     obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this TapeAction      obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this Buff            obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this Sample          obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this AudioFileOutput obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this WavHeaderStruct obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this AudioInfoWish   obj) => obj.Channels() == MonoChannels;
        public   static bool IsMono  (this AudioFileInfo   obj) => obj.Channels() == MonoChannels;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this SpeakerSetupEnum obj) => obj == SpeakerSetupEnum.Mono;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this SpeakerSetup     obj) => obj.ToEnum().IsMono();
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this ChannelEnum      obj) => obj == ChannelEnum.Single;
        [Obsolete(ObsoleteMessage)] public static bool IsMono(this Channel          obj) => obj.ToEnum().IsMono();
        
        public   static bool IsStereo(this SynthWishes     obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this FlowNode        obj) => obj.Channels() == StereoChannels;
        internal static bool IsStereo(this ConfigResolver  obj) => obj.Channels() == StereoChannels;
        [UsedImplicitly]
        internal static bool IsStereo(this ConfigSection   obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this Tape            obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this TapeConfig      obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this TapeActions     obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this TapeAction      obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this Buff            obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this Sample          obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this AudioFileOutput obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this WavHeaderStruct obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this AudioInfoWish   obj) => obj.Channels() == StereoChannels;
        public   static bool IsStereo(this AudioFileInfo   obj) => obj.Channels() == StereoChannels;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this SpeakerSetupEnum obj) => obj == SpeakerSetupEnum.Stereo;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this SpeakerSetup     obj) => obj.ToEnum().IsStereo();
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this ChannelEnum      obj) => obj == ChannelEnum.Left || obj == ChannelEnum.Right || obj == ChannelEnum.Undefined; // Undefined = stereo signal with 2 channels = not a specific channel
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this Channel          obj) => obj.ToEnum().IsStereo();
        
        public   static SynthWishes     Mono      (this SynthWishes     obj) => obj.Channels(MonoChannels);
        public   static FlowNode        Mono      (this FlowNode        obj) => obj.Channels(MonoChannels);
        internal static ConfigResolver  Mono      (this ConfigResolver  obj) => obj.Channels(MonoChannels);
        public   static Tape            Mono      (this Tape            obj) => obj.Channels(MonoChannels);
        public   static TapeConfig      Mono      (this TapeConfig      obj) => obj.Channels(MonoChannels);
        public   static TapeActions     Mono      (this TapeActions     obj) => obj.Channels(MonoChannels);
        public   static TapeAction      Mono      (this TapeAction      obj) => obj.Channels(MonoChannels);
        public   static Buff            Mono      (this Buff            obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static Sample          Mono      (this Sample          obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static AudioFileOutput Mono      (this AudioFileOutput obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static WavHeaderStruct Mono      (this WavHeaderStruct obj) => obj.Channels(MonoChannels);
        public   static AudioInfoWish   Mono      (this AudioInfoWish   obj) => obj.Channels(MonoChannels);
        public   static AudioFileInfo   Mono      (this AudioFileInfo   obj) => obj.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum Mono(this SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup Mono(this SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(MonoChannels, context);

        public   static SynthWishes     WithMono  (this SynthWishes     obj) => obj.Channels(MonoChannels);
        public   static FlowNode        WithMono  (this FlowNode        obj) => obj.Channels(MonoChannels);
        [UsedImplicitly]
        internal static ConfigResolver  WithMono  (this ConfigResolver  obj) => obj.Channels(MonoChannels);
        public   static Tape            WithMono  (this Tape            obj) => obj.Channels(MonoChannels);
        public   static TapeConfig      WithMono  (this TapeConfig      obj) => obj.Channels(MonoChannels);
        public   static TapeActions     WithMono  (this TapeActions     obj) => obj.Channels(MonoChannels);
        public   static TapeAction      WithMono  (this TapeAction      obj) => obj.Channels(MonoChannels);
        public   static Buff            WithMono  (this Buff            obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static Sample          WithMono  (this Sample          obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static AudioFileOutput WithMono  (this AudioFileOutput obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static WavHeaderStruct WithMono  (this WavHeaderStruct obj) => obj.Channels(MonoChannels);
        public   static AudioInfoWish   WithMono  (this AudioInfoWish   obj) => obj.Channels(MonoChannels);
        public   static AudioFileInfo   WithMono  (this AudioFileInfo   obj) => obj.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum WithMono(this SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup WithMono(this SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(MonoChannels, context);

        public   static SynthWishes     SetMono  (this SynthWishes     obj) => obj.Channels(MonoChannels);
        public   static FlowNode        SetMono  (this FlowNode        obj) => obj.Channels(MonoChannels);
        [UsedImplicitly]
        internal static ConfigResolver  SetMono  (this ConfigResolver  obj) => obj.Channels(MonoChannels);
        public   static Tape            SetMono  (this Tape            obj) => obj.Channels(MonoChannels);
        public   static TapeConfig      SetMono  (this TapeConfig      obj) => obj.Channels(MonoChannels);
        public   static TapeActions     SetMono  (this TapeActions     obj) => obj.Channels(MonoChannels);
        public   static TapeAction      SetMono  (this TapeAction      obj) => obj.Channels(MonoChannels);
        public   static Buff            SetMono  (this Buff            obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static Sample          SetMono  (this Sample          obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static AudioFileOutput SetMono  (this AudioFileOutput obj, IContext context) => obj.Channels(MonoChannels, context);
        public   static WavHeaderStruct SetMono  (this WavHeaderStruct obj) => obj.Channels(MonoChannels);
        public   static AudioInfoWish   SetMono  (this AudioInfoWish   obj) => obj.Channels(MonoChannels);
        public   static AudioFileInfo   SetMono  (this AudioFileInfo   obj) => obj.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum SetMono(this SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(MonoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup SetMono(this SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(MonoChannels, context);
                                                  
        public   static SynthWishes     Stereo    (this SynthWishes     obj) => obj.Channels(StereoChannels);
        public   static FlowNode        Stereo    (this FlowNode        obj) => obj.Channels(StereoChannels);
        internal static ConfigResolver  Stereo    (this ConfigResolver  obj) => obj.Channels(StereoChannels);
        public   static Tape            Stereo    (this Tape            obj) => obj.Channels(StereoChannels);
        public   static TapeConfig      Stereo    (this TapeConfig      obj) => obj.Channels(StereoChannels);
        public   static TapeActions     Stereo    (this TapeActions     obj) => obj.Channels(StereoChannels);
        public   static TapeAction      Stereo    (this TapeAction      obj) => obj.Channels(StereoChannels);
        public   static Buff            Stereo    (this Buff            obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static Sample          Stereo    (this Sample          obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static AudioFileOutput Stereo    (this AudioFileOutput obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static WavHeaderStruct Stereo    (this WavHeaderStruct obj) => obj.Channels(StereoChannels);
        public   static AudioInfoWish   Stereo    (this AudioInfoWish   obj) => obj.Channels(StereoChannels);
        public   static AudioFileInfo   Stereo    (this AudioFileInfo   obj) => obj.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum Stereo(this SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetup Stereo(this SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(StereoChannels, context);

        public   static SynthWishes     WithStereo(this SynthWishes     obj) => obj.Channels(StereoChannels);
        public   static FlowNode        WithStereo(this FlowNode        obj) => obj.Channels(StereoChannels);
        [UsedImplicitly]
        internal static ConfigResolver  WithStereo(this ConfigResolver  obj) => obj.Channels(StereoChannels);
        public   static Tape            WithStereo(this Tape            obj) => obj.Channels(StereoChannels);
        public   static TapeConfig      WithStereo(this TapeConfig      obj) => obj.Channels(StereoChannels);
        public   static TapeActions     WithStereo(this TapeActions     obj) => obj.Channels(StereoChannels);
        public   static TapeAction      WithStereo(this TapeAction      obj) => obj.Channels(StereoChannels);
        public   static Buff            WithStereo(this Buff            obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static Sample          WithStereo(this Sample          obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static AudioFileOutput WithStereo(this AudioFileOutput obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static WavHeaderStruct WithStereo(this WavHeaderStruct obj) => obj.Channels(StereoChannels);
        public   static AudioInfoWish   WithStereo(this AudioInfoWish   obj) => obj.Channels(StereoChannels);
        public   static AudioFileInfo   WithStereo(this AudioFileInfo   obj) => obj.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum WithStereo(this SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetup WithStereo(this SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(StereoChannels, context);

        public   static SynthWishes     SetStereo(this SynthWishes     obj) => obj.Channels(StereoChannels);
        public   static FlowNode        SetStereo(this FlowNode        obj) => obj.Channels(StereoChannels);
        [UsedImplicitly]
        internal static ConfigResolver  SetStereo(this ConfigResolver  obj) => obj.Channels(StereoChannels);
        public   static Tape            SetStereo(this Tape            obj) => obj.Channels(StereoChannels);
        public   static TapeConfig      SetStereo(this TapeConfig      obj) => obj.Channels(StereoChannels);
        public   static TapeActions     SetStereo(this TapeActions     obj) => obj.Channels(StereoChannels);
        public   static TapeAction      SetStereo(this TapeAction      obj) => obj.Channels(StereoChannels);
        public   static Buff            SetStereo(this Buff            obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static Sample          SetStereo(this Sample          obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static AudioFileOutput SetStereo(this AudioFileOutput obj, IContext context) => obj.Channels(StereoChannels, context);
        public   static WavHeaderStruct SetStereo(this WavHeaderStruct obj) => obj.Channels(StereoChannels);
        public   static AudioInfoWish   SetStereo(this AudioInfoWish   obj) => obj.Channels(StereoChannels);
        public   static AudioFileInfo   SetStereo(this AudioFileInfo   obj) => obj.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum SetStereo(this SpeakerSetupEnum oldEnumValue) => oldEnumValue.Channels(StereoChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetup SetStereo(this SpeakerSetup oldSpeakerSetup, IContext context) => oldSpeakerSetup.Channels(StereoChannels, context);
    }
    
    public partial class ConfigWishes
    {
        // Constants
    
        public const int NoChannels = 0;
        public const int MonoChannels = 1;
        public const int StereoChannels = 2;

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetupEnum ChannelsToEnum(int channels)
        {
            switch (AssertChannels((int?)channels))
            {
                case NoChannels: return SpeakerSetupEnum.Undefined;
                case MonoChannels: return SpeakerSetupEnum.Mono;
                case StereoChannels: return SpeakerSetupEnum.Stereo;
                default: return default; // ncrunch: no coverage
            }
        }
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum Channels(SpeakerSetupEnum oldEnumValue, int newChannels) => ChannelsToEnum(newChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum SetChannels(SpeakerSetupEnum oldEnumValue, int newChannels) => ChannelsToEnum(newChannels);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetupEnum WithChannels(SpeakerSetupEnum oldEnumValue, int newChannels) => ChannelsToEnum(newChannels);

        [Obsolete(ObsoleteMessage)]
        public static int EnumToChannels(SpeakerSetupEnum enumValue)
        {
            switch (enumValue)
            {
                case SpeakerSetupEnum.Mono: return MonoChannels;
                case SpeakerSetupEnum.Stereo: return StereoChannels;
                case SpeakerSetupEnum.Undefined: return NoChannels;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }
        [Obsolete(ObsoleteMessage)]
        public static int Channels(SpeakerSetupEnum obj) => EnumToChannels(obj);
        [Obsolete(ObsoleteMessage)]
        public static int GetChannels(SpeakerSetupEnum obj) => EnumToChannels(obj);
        [Obsolete(ObsoleteMessage)]
        public static int ToChannels(SpeakerSetupEnum enumValue) => EnumToChannels(enumValue);
        
        [Obsolete(ObsoleteMessage)] 
        public static SpeakerSetup ChannelsToEntity(int channels, IContext context) => channels.ChannelsToEnum().ToEntity(context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup Channels(SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ChannelsToEntity(newChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup SetChannels(SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ChannelsToEntity(newChannels, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup WithChannels(SpeakerSetup oldSpeakerSetup, int newChannels, IContext context) => ChannelsToEntity(newChannels, context);

        [Obsolete(ObsoleteMessage)] 
        public static int EntityToChannels(SpeakerSetup entity) => entity.ToEnum().EnumToChannels();
        [Obsolete(ObsoleteMessage)] 
        public static int Channels(SpeakerSetup obj) => EntityToChannels(obj);
        [Obsolete(ObsoleteMessage)] 
        public static int GetChannels(SpeakerSetup obj) => EntityToChannels(obj);
        [Obsolete(ObsoleteMessage)] 
        public static int ToChannels(SpeakerSetup obj) => EntityToChannels(obj);
    }
}