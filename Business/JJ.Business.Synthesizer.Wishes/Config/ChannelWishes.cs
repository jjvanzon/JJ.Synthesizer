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
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ChannelExtensionWishes
    {
        // A Primary Audio Attribute
        
        // Synth-Bound
        
        public static int? Channel(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        public static SynthWishes Channel(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
        public static int? Channel(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        public static FlowNode Channel(this FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
        internal static int? Channel(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        internal static ConfigResolver Channel(this ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
        // Tape-Bound
        
        public static int? Channel(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Channel;
        }
        
        public static Tape Channel(this Tape obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Channel = value;
            return obj;
        }
        
        public static int? Channel(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channel;
        }
        
        public static TapeConfig Channel(this TapeConfig obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channel = value;
            return obj;
        }
        
        public static int? Channel(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channel;
        }
        
        public static TapeActions Channel(this TapeActions obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channel = value;
            return obj;
        }
        
        public static int? Channel(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channel;
        }
        
        public static TapeAction Channel(this TapeAction obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channel = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static int? Channel(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.UnderlyingAudioFileOutput?.Channel();
        }
        
        public static Buff Channel(this Buff obj, int? value, IContext context)
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
        
        public static int? Channel(this AudioFileOutput obj)
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
        public static AudioFileOutput Channel(this AudioFileOutput obj, int? channel, IContext context)
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
        
        public static int Channel(this AudioFileOutputChannel obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Index;
        }
        
        public static AudioFileOutputChannel Channel(this AudioFileOutputChannel obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Index = value;
            return obj;
        }
        
        // Immutable
        
        [Obsolete(ObsoleteMessage)] 
        public static int? Channel(this ChannelEnum obj) => obj.EnumToChannel();
        
        [Obsolete(ObsoleteMessage)]
        public static int? Channel(this Channel obj) => obj?.Index;
        
        // Shorthand
        
        public   static bool IsCenter(this SynthWishes     obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        public   static bool IsCenter(this FlowNode        obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        [UsedImplicitly]
        internal static bool IsCenter(this ConfigResolver  obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        public   static bool IsCenter(this Tape            obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        public   static bool IsCenter(this TapeConfig      obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        public   static bool IsCenter(this TapeActions     obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        public   static bool IsCenter(this TapeAction      obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        public   static bool IsCenter(this Buff            obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        public   static bool IsCenter(this AudioFileOutput obj) => obj.IsMono  () && obj.Channel() == CenterChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsCenter(this ChannelEnum obj) => obj == ChannelEnum.Single;
        [Obsolete(ObsoleteMessage)] public static bool IsCenter(this Channel     obj) => obj.ToEnum() == ChannelEnum.Single;
        
        public   static bool IsLeft  (this SynthWishes     obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        public   static bool IsLeft  (this FlowNode        obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        [UsedImplicitly]
        internal static bool IsLeft  (this ConfigResolver  obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        public   static bool IsLeft  (this Tape            obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        public   static bool IsLeft  (this TapeConfig      obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        public   static bool IsLeft  (this TapeActions     obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        public   static bool IsLeft  (this TapeAction      obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        public   static bool IsLeft  (this Buff            obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        public   static bool IsLeft  (this AudioFileOutput obj) => obj.IsStereo() && obj.Channel() == LeftChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(this ChannelEnum obj) => obj == ChannelEnum.Left;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(this Channel     obj) => obj.ToEnum() == ChannelEnum.Left;
        
        public   static bool IsRight (this SynthWishes     obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        public   static bool IsRight (this FlowNode        obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        internal static bool IsRight (this ConfigResolver  obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        public   static bool IsRight (this Tape            obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        public   static bool IsRight (this TapeConfig      obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        public   static bool IsRight (this TapeActions     obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        public   static bool IsRight (this TapeAction      obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        public   static bool IsRight (this Buff            obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        public   static bool IsRight (this AudioFileOutput obj) => obj.IsStereo() && obj.Channel() == RightChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(this ChannelEnum obj) => obj == ChannelEnum.Right;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(this Channel     obj) => obj.ToEnum() == ChannelEnum.Right;
        
        public   static SynthWishes     Center (this SynthWishes     obj                  ) => obj.Mono(       ).Channel(CenterChannel);
        public   static FlowNode        Center (this FlowNode        obj                  ) => obj.Mono(       ).Channel(CenterChannel);
        internal static ConfigResolver  Center (this ConfigResolver  obj                  ) => obj.Mono(       ).Channel(CenterChannel);
        public   static Tape            Center (this Tape            obj                  ) => obj.Mono(       ).Channel(CenterChannel);
        public   static TapeConfig      Center (this TapeConfig      obj                  ) => obj.Mono(       ).Channel(CenterChannel);
        public   static TapeActions     Center (this TapeActions     obj                  ) => obj.Mono(       ).Channel(CenterChannel);
        public   static TapeAction      Center (this TapeAction      obj                  ) => obj.Mono(       ).Channel(CenterChannel);
        public   static Buff            Center (this Buff            obj, IContext context) => obj.Mono(context).Channel(CenterChannel, context);
        public   static AudioFileOutput Center (this AudioFileOutput obj, IContext context) => obj.Mono(context).Channel(CenterChannel, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum Center(this ChannelEnum oldChannelEnum) => ChannelEnum.Single;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static Channel Center(this Channel oldChannelEntity, IContext context) => ChannelEnum.Single.ToEntity(context);
        
        public   static SynthWishes     Left (this SynthWishes     obj                  ) => obj.Stereo(       ).Channel(LeftChannel);
        public   static FlowNode        Left (this FlowNode        obj                  ) => obj.Stereo(       ).Channel(LeftChannel);
        internal static ConfigResolver  Left (this ConfigResolver  obj                  ) => obj.Stereo(       ).Channel(LeftChannel);
        public   static Tape            Left (this Tape            obj                  ) => obj.Stereo(       ).Channel(LeftChannel);
        public   static TapeConfig      Left (this TapeConfig      obj                  ) => obj.Stereo(       ).Channel(LeftChannel);
        public   static TapeActions     Left (this TapeActions     obj                  ) => obj.Stereo(       ).Channel(LeftChannel);
        public   static TapeAction      Left (this TapeAction      obj                  ) => obj.Stereo(       ).Channel(LeftChannel);
        public   static Buff            Left (this Buff            obj, IContext context) => obj.Stereo(context).Channel(LeftChannel, context);
        public   static AudioFileOutput Left (this AudioFileOutput obj, IContext context) => obj.Stereo(context).Channel(LeftChannel, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Left(this ChannelEnum oldChannelEnum) => ChannelEnum.Left;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Left(this Channel oldChannelEntity, IContext context) => ChannelEnum.Left.ToEntity(context);
        
        public   static SynthWishes     Right (this SynthWishes     obj                  ) => obj.Stereo(       ).Channel(RightChannel);
        public   static FlowNode        Right (this FlowNode        obj                  ) => obj.Stereo(       ).Channel(RightChannel);
        internal static ConfigResolver  Right (this ConfigResolver  obj                  ) => obj.Stereo(       ).Channel(RightChannel);
        public   static Tape            Right (this Tape            obj                  ) => obj.Stereo(       ).Channel(RightChannel);
        public   static TapeConfig      Right (this TapeConfig      obj                  ) => obj.Stereo(       ).Channel(RightChannel);
        public   static TapeActions     Right (this TapeActions     obj                  ) => obj.Stereo(       ).Channel(RightChannel);
        public   static TapeAction      Right (this TapeAction      obj                  ) => obj.Stereo(       ).Channel(RightChannel);
        public   static Buff            Right (this Buff            obj, IContext context) => obj.Stereo(context).Channel(RightChannel, context);
        public   static AudioFileOutput Right (this AudioFileOutput obj, IContext context) => obj.Stereo(context).Channel(RightChannel, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Right(this ChannelEnum oldChannelEnum) => ChannelEnum.Right;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Right(this Channel oldChannelEntity, IContext context) => ChannelEnum.Right.ToEntity(context);
        
        public   static SynthWishes     NoChannel (this SynthWishes     obj                  ) => obj.Stereo(       ).Channel(EveryChannel);
        public   static FlowNode        NoChannel (this FlowNode        obj                  ) => obj.Stereo(       ).Channel(EveryChannel);
        internal static ConfigResolver  NoChannel (this ConfigResolver  obj                  ) => obj.Stereo(       ).Channel(EveryChannel);
        public   static Tape            NoChannel (this Tape            obj                  ) => obj.Stereo(       ).Channel(EveryChannel);
        public   static TapeConfig      NoChannel (this TapeConfig      obj                  ) => obj.Stereo(       ).Channel(EveryChannel);
        public   static TapeActions     NoChannel (this TapeActions     obj                  ) => obj.Stereo(       ).Channel(EveryChannel);
        public   static TapeAction      NoChannel (this TapeAction      obj                  ) => obj.Stereo(       ).Channel(EveryChannel);
        public   static Buff            NoChannel (this Buff            obj, IContext context) => obj.Stereo(context).Channel(EveryChannel, context);
        public   static AudioFileOutput NoChannel (this AudioFileOutput obj, IContext context) => obj.Stereo(context).Channel(EveryChannel, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum NoChannel(this ChannelEnum oldChannelEnum) => ChannelEnum.Undefined;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel NoChannel(this Channel obj) => null;
         
        // Conversion-Style
        
        [Obsolete(ObsoleteMessage)] 
        public static int? EnumToChannel(this ChannelEnum obj) => ConfigWishes.EnumToChannel(obj);

        [Obsolete(ObsoleteMessage)] 
        public static int? EntityToChannel(this Channel entity) => ConfigWishes.EntityToChannel(entity);
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