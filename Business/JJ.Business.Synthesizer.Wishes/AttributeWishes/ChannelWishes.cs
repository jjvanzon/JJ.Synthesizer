using System;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Primary Audio Attribute
        
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
        
        public static int? Channel(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        public static ConfigWishes Channel(this ConfigWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
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
        
        public static int? Channel(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) return default;
            return Channel(obj.UnderlyingAudioFileOutput);
        }
        
        public static Buff Channel(this Buff obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            
            if (obj.UnderlyingAudioFileOutput == null && value == null)
            {
                // Both null is ok.
                return obj;
            }
            
            // Otherwise, let this method throw error upon null UnderlyingAudioFileOutput.
            Channel(obj.UnderlyingAudioFileOutput, value);
            
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
            
            if (channels == StereoChannels);
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
        public static AudioFileOutput Channel(this AudioFileOutput obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.AudioFileOutputChannels == null) throw new NullException(() => obj.AudioFileOutputChannels);
            if (obj.AudioFileOutputChannels.Contains(null)) throw new Exception("obj.AudioFileOutputChannels contains nulls.");
            
            if (value.HasValue)
            {
                if (obj.AudioFileOutputChannels.Count != 1)
                {
                    throw new Exception("Can only set Channel property for AudioFileOutputs with only 1 channel.");
                }
                
                obj.AudioFileOutputChannels[0].Index = value.Value;
            }
            else
            {
                for (int i = 0; i < obj.AudioFileOutputChannels.Count; i++)
                {
                    obj.AudioFileOutputChannels[i].Index = i;
                }
            }
            
            return obj;
        }
        
        public static int Channel(this AudioFileOutputChannel obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return obj.Index;
        }
        
        public static AudioFileOutputChannel Channel(this AudioFileOutputChannel obj, int value)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            obj.Index = value;
            return obj;
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static int? Channel(this ChannelEnum obj) => EnumToChannel(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum Channel(this ChannelEnum obj, int? channel) 
            => ChannelToEnum(channel, Channels(obj));
        
        [Obsolete(ObsoleteMessage)]
        public static int? Channel(this Channel obj) => obj?.Index;
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static Channel Channel(this Channel obj, int? channel, IContext context) 
            => ChannelToEntity(channel, Channels(obj), context);
        
        // Channel, Conversion-Style
        
        [Obsolete(ObsoleteMessage)] 
        public static int? EnumToChannel(this ChannelEnum obj)
        {
            switch (obj)
            {
                case ChannelEnum.Single: return CenterChannel;
                case ChannelEnum.Left: return LeftChannel;
                case ChannelEnum.Right: return RightChannel;
                case ChannelEnum.Undefined: return null;
                default: throw new ValueNotSupportedException(obj);
            }
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static int? EntityToChannel(this Channel entity)
        {
            if (entity == null) return null;
            return EnumToChannel(entity.ToEnum());
        }
        
        // Channel Shorthand
        
        public static bool IsCenter (this SynthWishes     obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        public static bool IsCenter (this FlowNode        obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        public static bool IsCenter (this ConfigWishes    obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        public static bool IsCenter (this Tape            obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        public static bool IsCenter (this TapeConfig      obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        public static bool IsCenter (this TapeActions     obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        public static bool IsCenter (this TapeAction      obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        public static bool IsCenter (this Buff            obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        public static bool IsCenter (this AudioFileOutput obj) => IsMono  (obj) && Channel(obj) == CenterChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsCenter(this ChannelEnum obj) => IsMono(obj) && Channel(obj) == CenterChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsCenter(this Channel     obj) => IsMono(obj) && Channel(obj) == CenterChannel;
        
        public static bool IsLeft   (this SynthWishes     obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        public static bool IsLeft   (this FlowNode        obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        public static bool IsLeft   (this ConfigWishes    obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        public static bool IsLeft   (this Tape            obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        public static bool IsLeft   (this TapeConfig      obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        public static bool IsLeft   (this TapeActions     obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        public static bool IsLeft   (this TapeAction      obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        public static bool IsLeft   (this Buff            obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        public static bool IsLeft   (this AudioFileOutput obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(this ChannelEnum obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(this Channel     obj) => IsStereo(obj) && Channel(obj) == LeftChannel;
        
        public static bool IsRight (this SynthWishes     obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        public static bool IsRight (this FlowNode        obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        public static bool IsRight (this ConfigWishes    obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        public static bool IsRight (this Tape            obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        public static bool IsRight (this TapeConfig      obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        public static bool IsRight (this TapeActions     obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        public static bool IsRight (this TapeAction      obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        public static bool IsRight (this Buff            obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        public static bool IsRight (this AudioFileOutput obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(this ChannelEnum obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(this Channel     obj) => IsStereo(obj) && Channel(obj) == RightChannel;
        
        public static SynthWishes     Center (this SynthWishes     obj                  ) => Mono(obj         ).Channel(CenterChannel);
        public static FlowNode        Center (this FlowNode        obj                  ) => Mono(obj         ).Channel(CenterChannel);
        public static ConfigWishes    Center (this ConfigWishes    obj                  ) => Mono(obj         ).Channel(CenterChannel);
        public static Tape            Center (this Tape            obj                  ) => Mono(obj         ).Channel(CenterChannel);
        public static TapeConfig      Center (this TapeConfig      obj                  ) => Mono(obj         ).Channel(CenterChannel);
        public static TapeActions     Center (this TapeActions     obj                  ) => Mono(obj         ).Channel(CenterChannel);
        public static TapeAction      Center (this TapeAction      obj                  ) => Mono(obj         ).Channel(CenterChannel);
        public static Buff            Center (this Buff            obj, IContext context) => Mono(obj, context).Channel(CenterChannel);
        public static AudioFileOutput Center (this AudioFileOutput obj, IContext context) => Mono(obj, context).Channel(CenterChannel);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Center(this ChannelEnum obj) => Mono(obj).Channel(CenterChannel);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Center(this Channel obj, IContext context) => Mono(obj, context).Channel(CenterChannel, context);
        
        public static SynthWishes     Left (this SynthWishes     obj                  ) => Stereo(obj         ).Channel(LeftChannel);
        public static FlowNode        Left (this FlowNode        obj                  ) => Stereo(obj         ).Channel(LeftChannel);
        public static ConfigWishes    Left (this ConfigWishes    obj                  ) => Stereo(obj         ).Channel(LeftChannel);
        public static Tape            Left (this Tape            obj                  ) => Stereo(obj         ).Channel(LeftChannel);
        public static TapeConfig      Left (this TapeConfig      obj                  ) => Stereo(obj         ).Channel(LeftChannel);
        public static TapeActions     Left (this TapeActions     obj                  ) => Stereo(obj         ).Channel(LeftChannel);
        public static TapeAction      Left (this TapeAction      obj                  ) => Stereo(obj         ).Channel(LeftChannel);
        public static Buff            Left (this Buff            obj, IContext context) => Stereo(obj, context).Channel(LeftChannel);
        public static AudioFileOutput Left (this AudioFileOutput obj, IContext context) => Stereo(obj, context).Channel(LeftChannel);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Left(this ChannelEnum obj) => Stereo(obj).Channel(LeftChannel);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Left(this Channel obj, IContext context) => Stereo(obj, context).Channel(LeftChannel, context);
        
        public static SynthWishes     Right (this SynthWishes     obj                  ) => Stereo(obj         ).Channel(RightChannel);
        public static FlowNode        Right (this FlowNode        obj                  ) => Stereo(obj         ).Channel(RightChannel);
        public static ConfigWishes    Right (this ConfigWishes    obj                  ) => Stereo(obj         ).Channel(RightChannel);
        public static Tape            Right (this Tape            obj                  ) => Stereo(obj         ).Channel(RightChannel);
        public static TapeConfig      Right (this TapeConfig      obj                  ) => Stereo(obj         ).Channel(RightChannel);
        public static TapeActions     Right (this TapeActions     obj                  ) => Stereo(obj         ).Channel(RightChannel);
        public static TapeAction      Right (this TapeAction      obj                  ) => Stereo(obj         ).Channel(RightChannel);
        public static Buff            Right (this Buff            obj, IContext context) => Stereo(obj, context).Channel(RightChannel);
        public static AudioFileOutput Right (this AudioFileOutput obj, IContext context) => Stereo(obj, context).Channel(RightChannel);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Right(this ChannelEnum obj) => Stereo(obj).Channel(RightChannel);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Right(this Channel obj, IContext context) => Stereo(obj, context).Channel(RightChannel, context);
    }
}