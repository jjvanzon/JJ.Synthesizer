using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // Channel: A Primary Audio Attribute

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ChannelExtensionWishes
    {
        // Synth-Bound
        
        public static bool IsCenter(this SynthWishes obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        public static bool IsLeft(this   SynthWishes obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        public static bool IsRight(this  SynthWishes obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel(this  SynthWishes obj) => GetChannel(obj);
        public static int? GetChannel(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        public static SynthWishes Center(this        SynthWishes obj) => SetCenter(obj);
        public static SynthWishes WithCenter(this    SynthWishes obj) => SetCenter(obj);
        public static SynthWishes AsCenter(this      SynthWishes obj) => SetCenter(obj);
        public static SynthWishes Left(this          SynthWishes obj) => SetLeft(obj);
        public static SynthWishes WithLeft(this      SynthWishes obj) => SetLeft(obj);
        public static SynthWishes AsLeft(this        SynthWishes obj) => SetLeft(obj);
        public static SynthWishes WithRight(this     SynthWishes obj) => SetRight(obj);
        public static SynthWishes Right(this         SynthWishes obj) => SetRight(obj);
        public static SynthWishes AsRight(this       SynthWishes obj) => SetRight(obj);
        public static SynthWishes NoChannel(this     SynthWishes obj) => SetNoChannel(obj);
        public static SynthWishes WithNoChannel(this SynthWishes obj) => SetNoChannel(obj);
        public static SynthWishes AsNoChannel(this   SynthWishes obj) => SetNoChannel(obj);
        public static SynthWishes Channel(this       SynthWishes obj, int? value) => SetChannel(obj, value);
        public static SynthWishes WithChannel(this   SynthWishes obj, int? value) => SetChannel(obj, value);
        public static SynthWishes AsChannel(this     SynthWishes obj, int? value) => SetChannel(obj, value);
        public static SynthWishes SetCenter(this     SynthWishes obj) => obj.Mono().SetChannel(CenterChannel);
        public static SynthWishes SetLeft(this       SynthWishes obj) => obj.Stereo().SetChannel(LeftChannel);
        public static SynthWishes SetRight(this      SynthWishes obj) => obj.Stereo().SetChannel(RightChannel);
        public static SynthWishes SetNoChannel(this  SynthWishes obj) => obj.Stereo().SetChannel(EveryChannel);
        public static SynthWishes SetChannel(this    SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }

        public static bool IsCenter(this FlowNode obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        public static bool IsLeft(this   FlowNode obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        public static bool IsRight(this  FlowNode obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel(this  FlowNode obj) => GetChannel(obj);
        public static int? GetChannel(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        public static FlowNode Center(this        FlowNode obj) => SetCenter(obj);
        public static FlowNode WithCenter(this    FlowNode obj) => SetCenter(obj);
        public static FlowNode AsCenter(this      FlowNode obj) => SetCenter(obj);
        public static FlowNode Left(this          FlowNode obj) => SetLeft(obj);
        public static FlowNode WithLeft(this      FlowNode obj) => SetLeft(obj);
        public static FlowNode AsLeft(this        FlowNode obj) => SetLeft(obj);
        public static FlowNode WithRight(this     FlowNode obj) => SetRight(obj);
        public static FlowNode Right(this         FlowNode obj) => SetRight(obj);
        public static FlowNode AsRight(this       FlowNode obj) => SetRight(obj);
        public static FlowNode NoChannel(this     FlowNode obj) => SetNoChannel(obj);
        public static FlowNode WithNoChannel(this FlowNode obj) => SetNoChannel(obj);
        public static FlowNode AsNoChannel(this   FlowNode obj) => SetNoChannel(obj);
        public static FlowNode Channel(this       FlowNode obj, int? value) => SetChannel(obj, value);
        public static FlowNode WithChannel(this   FlowNode obj, int? value) => SetChannel(obj, value);
        public static FlowNode AsChannel(this     FlowNode obj, int? value) => SetChannel(obj, value);
        public static FlowNode SetCenter(this     FlowNode obj) => obj.Mono().SetChannel(CenterChannel);
        public static FlowNode SetLeft(this       FlowNode obj) => obj.Stereo().SetChannel(LeftChannel);
        public static FlowNode SetRight(this      FlowNode obj) => obj.Stereo().SetChannel(RightChannel);
        public static FlowNode SetNoChannel(this  FlowNode obj) => obj.Stereo().SetChannel(EveryChannel);
        public static FlowNode SetChannel(this    FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
        [UsedImplicitly] internal static bool IsCenter(this ConfigResolver obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        [UsedImplicitly] internal static bool IsLeft(this   ConfigResolver obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        [UsedImplicitly] internal static bool IsRight(this  ConfigResolver obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        [UsedImplicitly] internal static int? Channel(this  ConfigResolver obj) => GetChannel(obj);
        [UsedImplicitly] internal static int? GetChannel(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        internal static ConfigResolver Center(this        ConfigResolver obj) => SetCenter(obj);
        internal static ConfigResolver WithCenter(this    ConfigResolver obj) => SetCenter(obj);
        internal static ConfigResolver AsCenter(this      ConfigResolver obj) => SetCenter(obj);
        internal static ConfigResolver Left(this          ConfigResolver obj) => SetLeft(obj);
        internal static ConfigResolver WithLeft(this      ConfigResolver obj) => SetLeft(obj);
        internal static ConfigResolver AsLeft(this        ConfigResolver obj) => SetLeft(obj);
        internal static ConfigResolver WithRight(this     ConfigResolver obj) => SetRight(obj);
        internal static ConfigResolver Right(this         ConfigResolver obj) => SetRight(obj);
        internal static ConfigResolver AsRight(this       ConfigResolver obj) => SetRight(obj);
        internal static ConfigResolver NoChannel(this     ConfigResolver obj) => SetNoChannel(obj);
        internal static ConfigResolver WithNoChannel(this ConfigResolver obj) => SetNoChannel(obj);
        internal static ConfigResolver AsNoChannel(this   ConfigResolver obj) => SetNoChannel(obj);
        internal static ConfigResolver Channel(this       ConfigResolver obj, int? value) => SetChannel(obj, value);
        internal static ConfigResolver WithChannel(this   ConfigResolver obj, int? value) => SetChannel(obj, value);
        internal static ConfigResolver AsChannel(this     ConfigResolver obj, int? value) => SetChannel(obj, value);
        internal static ConfigResolver SetCenter(this     ConfigResolver obj) => obj.Mono().SetChannel(CenterChannel);
        internal static ConfigResolver SetLeft(this       ConfigResolver obj) => obj.Stereo().SetChannel(LeftChannel);
        internal static ConfigResolver SetRight(this      ConfigResolver obj) => obj.Stereo().SetChannel(RightChannel);
        internal static ConfigResolver SetNoChannel(this  ConfigResolver obj) => obj.Stereo().SetChannel(EveryChannel);
        internal static ConfigResolver SetChannel(this    ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
        // Tape-Bound
        
        public static bool IsCenter(this Tape obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        public static bool IsLeft(this Tape obj) => GetChannel(obj) == LeftChannel && IsStereo(obj);
        public static bool IsRight(this  Tape obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel(this  Tape obj) => GetChannel(obj);
        public static int? GetChannel(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Channel;
        }
        
        public static Tape Center(this        Tape obj) => SetCenter(obj);
        public static Tape WithCenter(this    Tape obj) => SetCenter(obj);
        public static Tape AsCenter(this      Tape obj) => SetCenter(obj);
        public static Tape Left(this          Tape obj) => SetLeft(obj);
        public static Tape WithLeft(this      Tape obj) => SetLeft(obj);
        public static Tape AsLeft(this        Tape obj) => SetLeft(obj);
        public static Tape WithRight(this     Tape obj) => SetRight(obj);
        public static Tape Right(this         Tape obj) => SetRight(obj);
        public static Tape AsRight(this       Tape obj) => SetRight(obj);
        public static Tape NoChannel(this     Tape obj) => SetNoChannel(obj);
        public static Tape WithNoChannel(this Tape obj) => SetNoChannel(obj);
        public static Tape AsNoChannel(this   Tape obj) => SetNoChannel(obj);
        public static Tape Channel(this       Tape obj, int? value) => SetChannel(obj, value);
        public static Tape WithChannel(this   Tape obj, int? value) => SetChannel(obj, value);
        public static Tape AsChannel(this     Tape obj, int? value) => SetChannel(obj, value);
        public static Tape SetCenter(this     Tape obj) => obj.Mono().SetChannel(CenterChannel);
        public static Tape SetLeft(this       Tape obj) => obj.Stereo().SetChannel(LeftChannel);
        public static Tape SetRight(this      Tape obj) => obj.Stereo().SetChannel(RightChannel);
        public static Tape SetNoChannel(this  Tape obj) => obj.Stereo().SetChannel(EveryChannel);
        public static Tape SetChannel(this    Tape obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Channel = value;
            return obj;
        }
        
        public static bool IsCenter(this TapeConfig obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        public static bool IsLeft(this TapeConfig obj) => GetChannel(obj) == LeftChannel && IsStereo(obj);
        public static bool IsRight(this TapeConfig obj) => GetChannel(obj) == RightChannel && IsStereo(obj);
        public static int? Channel(this TapeConfig obj) => GetChannel(obj);
        public static int? GetChannel(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channel;
        }
        
        public static TapeConfig Center(this        TapeConfig obj) => SetCenter(obj);
        public static TapeConfig WithCenter(this    TapeConfig obj) => SetCenter(obj);
        public static TapeConfig AsCenter(this      TapeConfig obj) => SetCenter(obj);
        public static TapeConfig Left(this          TapeConfig obj) => SetLeft(obj);
        public static TapeConfig WithLeft(this      TapeConfig obj) => SetLeft(obj);
        public static TapeConfig AsLeft(this        TapeConfig obj) => SetLeft(obj);
        public static TapeConfig WithRight(this     TapeConfig obj) => SetRight(obj);
        public static TapeConfig Right(this         TapeConfig obj) => SetRight(obj);
        public static TapeConfig AsRight(this       TapeConfig obj) => SetRight(obj);
        public static TapeConfig NoChannel(this     TapeConfig obj) => SetNoChannel(obj);
        public static TapeConfig WithNoChannel(this TapeConfig obj) => SetNoChannel(obj);
        public static TapeConfig AsNoChannel(this   TapeConfig obj) => SetNoChannel(obj);
        public static TapeConfig Channel(this       TapeConfig obj, int? value) => SetChannel(obj, value);
        public static TapeConfig WithChannel(this   TapeConfig obj, int? value) => SetChannel(obj, value);
        public static TapeConfig AsChannel(this     TapeConfig obj, int? value) => SetChannel(obj, value);
        public static TapeConfig SetCenter(this     TapeConfig obj) => obj.Mono().SetChannel(CenterChannel);
        public static TapeConfig SetLeft(this       TapeConfig obj) => obj.Stereo().SetChannel(LeftChannel);
        public static TapeConfig SetRight(this      TapeConfig obj) => obj.Stereo().SetChannel(RightChannel);
        public static TapeConfig SetNoChannel(this  TapeConfig obj) => obj.Stereo().SetChannel(EveryChannel);
        public static TapeConfig SetChannel(this    TapeConfig obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channel = value;
            return obj;
        }
        
        public static bool IsCenter(this TapeActions obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        public static bool IsLeft(this TapeActions obj) => GetChannel(obj) == LeftChannel && IsStereo(obj);
        public static bool IsRight(this TapeActions obj) => GetChannel(obj) == RightChannel && IsStereo(obj);
        public static int? Channel(this TapeActions obj) => GetChannel(obj);
        public static int? GetChannel(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channel;
        }
        
        public static TapeActions Center(this        TapeActions obj) => SetCenter(obj);
        public static TapeActions WithCenter(this    TapeActions obj) => SetCenter(obj);
        public static TapeActions AsCenter(this      TapeActions obj) => SetCenter(obj);
        public static TapeActions Left(this          TapeActions obj) => SetLeft(obj);
        public static TapeActions WithLeft(this      TapeActions obj) => SetLeft(obj);
        public static TapeActions AsLeft(this        TapeActions obj) => SetLeft(obj);
        public static TapeActions WithRight(this     TapeActions obj) => SetRight(obj);
        public static TapeActions Right(this         TapeActions obj) => SetRight(obj);
        public static TapeActions AsRight(this       TapeActions obj) => SetRight(obj);
        public static TapeActions NoChannel(this     TapeActions obj) => SetNoChannel(obj);
        public static TapeActions WithNoChannel(this TapeActions obj) => SetNoChannel(obj);
        public static TapeActions AsNoChannel(this   TapeActions obj) => SetNoChannel(obj);
        public static TapeActions Channel(this       TapeActions obj, int? value) => SetChannel(obj, value);
        public static TapeActions WithChannel(this   TapeActions obj, int? value) => SetChannel(obj, value);
        public static TapeActions AsChannel(this     TapeActions obj, int? value) => SetChannel(obj, value);
        public static TapeActions SetCenter(this     TapeActions obj) => obj.Mono().SetChannel(CenterChannel);
        public static TapeActions SetLeft(this       TapeActions obj) => obj.Stereo().SetChannel(LeftChannel);
        public static TapeActions SetRight(this      TapeActions obj) => obj.Stereo().SetChannel(RightChannel);
        public static TapeActions SetNoChannel(this  TapeActions obj) => obj.Stereo().SetChannel(EveryChannel);
        public static TapeActions SetChannel(this    TapeActions obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channel = value;
            return obj;
        }
        
        public static bool IsCenter(this TapeAction obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        public static bool IsLeft(this TapeAction obj) => GetChannel(obj) == LeftChannel && IsStereo(obj);
        public static bool IsRight(this TapeAction obj) => GetChannel(obj) == RightChannel && IsStereo(obj);
        public static int? Channel(this TapeAction obj) => GetChannel(obj);
        public static int? GetChannel(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channel;
        }
        
        public static TapeAction Center(this        TapeAction obj) => SetCenter(obj);
        public static TapeAction WithCenter(this    TapeAction obj) => SetCenter(obj);
        public static TapeAction AsCenter(this      TapeAction obj) => SetCenter(obj);
        public static TapeAction Left(this          TapeAction obj) => SetLeft(obj);
        public static TapeAction WithLeft(this      TapeAction obj) => SetLeft(obj);
        public static TapeAction AsLeft(this        TapeAction obj) => SetLeft(obj);
        public static TapeAction WithRight(this     TapeAction obj) => SetRight(obj);
        public static TapeAction Right(this         TapeAction obj) => SetRight(obj);
        public static TapeAction AsRight(this       TapeAction obj) => SetRight(obj);
        public static TapeAction NoChannel(this     TapeAction obj) => SetNoChannel(obj);
        public static TapeAction WithNoChannel(this TapeAction obj) => SetNoChannel(obj);
        public static TapeAction AsNoChannel(this   TapeAction obj) => SetNoChannel(obj);
        public static TapeAction Channel(this       TapeAction obj, int? value) => SetChannel(obj, value);
        public static TapeAction WithChannel(this   TapeAction obj, int? value) => SetChannel(obj, value);
        public static TapeAction AsChannel(this     TapeAction obj, int? value) => SetChannel(obj, value);
        public static TapeAction SetCenter(this     TapeAction obj) => obj.Mono().SetChannel(CenterChannel);
        public static TapeAction SetLeft(this       TapeAction obj) => obj.Stereo().SetChannel(LeftChannel);
        public static TapeAction SetRight(this      TapeAction obj) => obj.Stereo().SetChannel(RightChannel);
        public static TapeAction SetNoChannel(this  TapeAction obj) => obj.Stereo().SetChannel(EveryChannel);
        public static TapeAction SetChannel(this    TapeAction obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channel = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static bool IsCenter(this Buff obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        public static bool IsLeft(this Buff obj) => GetChannel(obj) == LeftChannel && IsStereo(obj);
        public static bool IsRight(this Buff obj) => GetChannel(obj) == RightChannel && IsStereo(obj);
        public static int? Channel(this Buff obj) => GetChannel(obj);
        public static int? GetChannel(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.UnderlyingAudioFileOutput?.Channel();
        }
        
        public static Buff Center(this        Buff obj, IContext context) => SetCenter(obj, context);
        public static Buff WithCenter(this    Buff obj, IContext context) => SetCenter(obj, context);
        public static Buff AsCenter(this      Buff obj, IContext context) => SetCenter(obj, context);
        public static Buff Left(this          Buff obj, IContext context) => SetLeft(obj, context);
        public static Buff WithLeft(this      Buff obj, IContext context) => SetLeft(obj, context);
        public static Buff AsLeft(this        Buff obj, IContext context) => SetLeft(obj, context);
        public static Buff WithRight(this     Buff obj, IContext context) => SetRight(obj, context);
        public static Buff Right(this         Buff obj, IContext context) => SetRight(obj, context);
        public static Buff AsRight(this       Buff obj, IContext context) => SetRight(obj, context);
        public static Buff NoChannel(this     Buff obj, IContext context) => SetNoChannel(obj, context);
        public static Buff WithNoChannel(this Buff obj, IContext context) => SetNoChannel(obj, context);
        public static Buff AsNoChannel(this   Buff obj, IContext context) => SetNoChannel(obj, context);
        public static Buff Channel(this       Buff obj, int? value, IContext context) => SetChannel(obj, value, context);
        public static Buff WithChannel(this   Buff obj, int? value, IContext context) => SetChannel(obj, value, context);
        public static Buff AsChannel(this     Buff obj, int? value, IContext context) => SetChannel(obj, value, context);
        public static Buff SetCenter(this     Buff obj, IContext context) => obj.Mono(context).SetChannel(CenterChannel, context);
        public static Buff SetLeft(this       Buff obj, IContext context) => obj.Stereo(context).SetChannel(LeftChannel, context);
        public static Buff SetRight(this      Buff obj, IContext context) => obj.Stereo(context).SetChannel(RightChannel, context);
        public static Buff SetNoChannel(this  Buff obj, IContext context) => obj.Stereo(context).SetChannel(EveryChannel, context);
        public static Buff SetChannel(this    Buff obj, int? value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            
            if (obj.UnderlyingAudioFileOutput == null && value == null)
            {
                // Both null: it's ok to set to null.
                return obj;
            }
            
            // Otherwise, let this method throw error upon null UnderlyingAudioFileOutput.
            obj.UnderlyingAudioFileOutput.Channel(value, context);
            
            return obj;
        }
        
        public static bool IsCenter(this AudioFileOutput obj) => GetChannel(obj) == CenterChannel && IsMono(obj);
        public static bool IsLeft(this AudioFileOutput obj) => GetChannel(obj) == LeftChannel && IsStereo(obj);
        public static bool IsRight(this AudioFileOutput obj) => GetChannel(obj) == RightChannel && IsStereo(obj);
        public static int? Channel(this AudioFileOutput obj) => GetChannel(obj);
        public static int? GetChannel(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.AudioFileOutputChannels == null) throw new NullException(() => obj.AudioFileOutputChannels);
            
            int channels = obj.Channels();
            int signalCount = obj.AudioFileOutputChannels.Count;
            int? firstChannelNumber = obj.AudioFileOutputChannels.ElementAtOrDefault(0)?.Channel();
            
            // Mono has channel 0 only.
            if (channels == MonoChannels) return CenterChannel;
            
            if (channels == StereoChannels)
            {
                if (signalCount == 2)
                {
                    // Handles stereo with 2 channels defined, so not specific channel can be returned,
                    return null;
                }
                if (signalCount == 1)
                {
                    // By returning index, we handle both "Left-only" and "Right-only" (single channel 1) scenarios.
                    if (firstChannelNumber != null)
                    {
                        return firstChannelNumber;
                    }
                }
            }
            
            throw new Exception(
                "Unsupported combination of values: " + Environment.NewLine +
                $"obj.Channels = {channels}, " + Environment.NewLine +
                $"obj.AudioFileOutputChannels.Count = {signalCount} ({nameof(signalCount)})" + Environment.NewLine +
                $"obj.AudioFileOutputChannels[0].Index = {firstChannelNumber} ({nameof(firstChannelNumber)})");
        }

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Center(this AudioFileOutput obj, IContext context) => SetCenter(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithCenter(this AudioFileOutput obj, IContext context) => SetCenter(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsCenter(this AudioFileOutput obj, IContext context) => SetCenter(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Left(this AudioFileOutput obj, IContext context) => SetLeft(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithLeft(this AudioFileOutput obj, IContext context) => SetLeft(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsLeft(this AudioFileOutput obj, IContext context) => SetLeft(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithRight(this AudioFileOutput obj, IContext context) => SetRight(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Right(this AudioFileOutput obj, IContext context) => SetRight(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsRight(this AudioFileOutput obj, IContext context) => SetRight(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput NoChannel(this AudioFileOutput obj, IContext context) => SetNoChannel(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithNoChannel(this AudioFileOutput obj, IContext context) => SetNoChannel(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsNoChannel(this AudioFileOutput obj, IContext context) => SetNoChannel(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Channel(this AudioFileOutput obj, int? value, IContext context) => SetChannel(obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithChannel(this AudioFileOutput obj, int? value, IContext context) => SetChannel(obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsChannel(this AudioFileOutput obj, int? value, IContext context) => SetChannel(obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetCenter(this AudioFileOutput obj, IContext context) => obj.Mono(context).SetChannel(CenterChannel, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetLeft(this AudioFileOutput obj, IContext context) => obj.Stereo(context).SetChannel(LeftChannel, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetRight(this AudioFileOutput obj, IContext context) => obj.Stereo(context).SetChannel(RightChannel, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetNoChannel(this AudioFileOutput obj, IContext context) => obj.Stereo(context).SetChannel(EveryChannel, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetChannel(this AudioFileOutput obj, int? channel, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.AudioFileOutputChannels == null) throw new NullException(() => obj.AudioFileOutputChannels);
            if (obj.AudioFileOutputChannels.Contains(null)) throw new Exception("obj.AudioFileOutputChannels contains nulls.");
            
            if (channel == CenterChannel && obj.IsMono())
            {
                obj.Channels(MonoChannels, context);
                CreateOrRemoveChannels(obj, signalCount: 1, context);
                obj.AudioFileOutputChannels[0].Index = CenterChannel;
            }
            else if (channel == LeftChannel && obj.IsStereo())
            {
                obj.SpeakerSetup = GetSubstituteSpeakerSetup(StereoChannels, context);
                CreateOrRemoveChannels(obj, signalCount: 1, context);
                obj.AudioFileOutputChannels[0].Index = LeftChannel;
            }
            else if (channel == RightChannel)
            {
                obj.SpeakerSetup = GetSubstituteSpeakerSetup(StereoChannels, context);
                CreateOrRemoveChannels(obj, signalCount: 1, context);
                obj.AudioFileOutputChannels[0].Index = RightChannel;
            }
            else if (channel == EveryChannel) 
            {
                obj.SpeakerSetup = GetSubstituteSpeakerSetup(StereoChannels, context);
                CreateOrRemoveChannels(obj, signalCount: 2, context);
                obj.AudioFileOutputChannels[0].Index = 0;
                obj.AudioFileOutputChannels[0].Index = 1;
            }
            else
            {
                throw new Exception($"Invalid combination of values: {new { AudioFileOutput_Channels = obj.Channels(), channel }}");
            }
            
            return obj;
        }
        
        public static int Channel(this AudioFileOutputChannel obj) => GetChannel(obj);
        public static int GetChannel(this AudioFileOutputChannel obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Index;
        }
        
        public static AudioFileOutputChannel Channel(this     AudioFileOutputChannel obj, int value) => SetChannel(obj, value);
        public static AudioFileOutputChannel WithChannel(this AudioFileOutputChannel obj, int value) => SetChannel(obj, value);
        public static AudioFileOutputChannel AsChannel(this   AudioFileOutputChannel obj, int value) => SetChannel(obj, value);
        public static AudioFileOutputChannel SetChannel(this  AudioFileOutputChannel obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Index = value;
            return obj;
        }

        // Immutable

        [Obsolete(ObsoleteMessage)] public static bool IsCenter(this      ChannelEnum enumValue) => enumValue == ChannelEnum.Single;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(this        ChannelEnum enumValue) => enumValue == ChannelEnum.Left;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(this       ChannelEnum enumValue) => enumValue == ChannelEnum.Right;
        [Obsolete(ObsoleteMessage)] public static int? Channel(this       ChannelEnum enumValue) => ConfigWishes.EnumToChannel(enumValue);
        [Obsolete(ObsoleteMessage)] public static int? GetChannel(this    ChannelEnum enumValue) => ConfigWishes.EnumToChannel(enumValue);
        [Obsolete(ObsoleteMessage)] public static int? EnumToChannel(this ChannelEnum enumValue) => ConfigWishes.EnumToChannel(enumValue);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithCenter(this ChannelEnum oldChannelEnum) => SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsCenter(this ChannelEnum oldChannelEnum) => SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToCenter(this ChannelEnum oldChannelEnum) => SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Center(this ChannelEnum oldChannelEnum) => SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithLeft(this ChannelEnum oldChannelEnum) => SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsLeft(this ChannelEnum oldChannelEnum) => SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToLeft(this ChannelEnum oldChannelEnum) => SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Left(this ChannelEnum oldChannelEnum) => SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithRight(this ChannelEnum oldChannelEnum) => SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsRight(this ChannelEnum oldChannelEnum) => SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToRight(this ChannelEnum oldChannelEnum) => SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Right(this ChannelEnum oldChannelEnum) => SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithNoChannel(this ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsNoChannel(this ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToNoChannel(this ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum NoChannel(this ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetCenter(this ChannelEnum oldChannelEnum) => ChannelEnum.Single;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetLeft(this ChannelEnum oldChannelEnum) => ChannelEnum.Left;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetRight(this ChannelEnum oldChannelEnum) => ChannelEnum.Right;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetNoChannel(this ChannelEnum oldChannelEnum) => ChannelEnum.Undefined;

        [Obsolete(ObsoleteMessage)] public static bool IsCenter(this        Channel entity) => entity.ToEnum() == ChannelEnum.Single;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(this          Channel entity) => entity.ToEnum() == ChannelEnum.Left;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(this         Channel entity) => entity.ToEnum() == ChannelEnum.Right; 
        [Obsolete(ObsoleteMessage)] public static int? Channel(this         Channel entity) => ConfigWishes.EntityToChannel(entity);
        [Obsolete(ObsoleteMessage)] public static int? GetChannel(this      Channel entity) => ConfigWishes.EntityToChannel(entity);
        [Obsolete(ObsoleteMessage)] public static int? EntityToChannel(this Channel entity) => ConfigWishes.EntityToChannel(entity);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithCenter(this Channel oldChannelEntity, IContext context) => SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsCenter(this Channel oldChannelEntity, IContext context) => SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToCenter(this Channel oldChannelEntity, IContext context) => SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Center(this Channel oldChannelEntity, IContext context) => SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithLeft(this Channel oldChannelEntity, IContext context) => SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsLeft(this Channel oldChannelEntity, IContext context) => SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToLeft(this Channel oldChannelEntity, IContext context) => SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Left(this Channel oldChannelEntity, IContext context) => SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithRight(this Channel oldChannelEntity, IContext context) => SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsRight(this Channel oldChannelEntity, IContext context) => SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToRight(this Channel oldChannelEntity, IContext context) => SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Right(this Channel oldChannelEntity, IContext context) => SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithNoChannel(this Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsNoChannel(this Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToNoChannel(this Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel NoChannel(this Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetCenter(this Channel oldChannelEntity, IContext context) => ChannelEnum.Single.ToEntity(context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetLeft(this Channel oldChannelEntity, IContext context) => ChannelEnum.Left.ToEntity(context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetRight(this Channel oldChannelEntity, IContext context) => ChannelEnum.Right.ToEntity(context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetNoChannel(this Channel oldChannelEntity) => null;
    }
    
    public partial class ConfigWishes
    { 
        // Constants
        
        public const int CenterChannel = 0;
        public const int LeftChannel = 0;
        public const int RightChannel = 1;
        public static readonly int? ChannelEmpty = null;
        public static readonly int? AnyChannel = null;
        public static readonly int? EveryChannel = null;

        // Conversion-Style
        
        [Obsolete(ObsoleteMessage)] 
        public static int? EnumToChannel(ChannelEnum obj)
        {
            switch (obj)
            {
                case ChannelEnum.Single: return CenterChannel;
                case ChannelEnum.Left: return LeftChannel;
                case ChannelEnum.Right: return RightChannel;
                case ChannelEnum.Undefined: return ChannelEmpty;
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static int? EntityToChannel(Channel entity) => entity.ToEnum().EnumToChannel();
    }
}