using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // From ChannelsWishes
        
        [Obsolete(ObsoleteMessage)]
        public static int Channels(this ChannelEnum obj) 
            => obj.ChannelEnumToChannels();
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] // ReSharper disable once UnusedParameter.Global
        public static ChannelEnum Channels(this ChannelEnum thisChannelEnum, int channelsValue)
        {
            int? thisChannel = thisChannelEnum.Channel();
            return channelsValue.ChannelsToChannelEnum(thisChannel);
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static int Channels(this Channel obj)
            => obj.ChannelEntityToChannels();

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static Channel Channels(this Channel thisChannelEntity, int channelsValue, IContext context)
        {
            int? channel = thisChannelEntity.Channel();
            ChannelEnum channelEnum = channelsValue.ChannelsToChannelEnum(channel);
            Channel channelEntity = channelEnum.ToEntity(context);
            return channelEntity;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum ChannelsToChannelEnum(this int thisChannels, int? channelValue)
        {
            AssertChannels(thisChannels);
            AssertChannel(channelValue);
            
            if (thisChannels == MonoChannels) return ChannelEnum.Single;
            if (thisChannels == StereoChannels && channelValue == LeftChannel) return ChannelEnum.Left;
            if (thisChannels == StereoChannels && channelValue == RightChannel) return ChannelEnum.Right;
            if (thisChannels == StereoChannels && channelValue == ConfigWishes.NoChannel) return ChannelEnum.Undefined;
            throw new Exception($"Unsupported combination of values {new { channels = thisChannels, channel = channelValue }}");
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static int ChannelEnumToChannels(this ChannelEnum channelEnum)
        {
            switch (channelEnum)
            {
                case ChannelEnum.Undefined: return default;
                case ChannelEnum.Single: return MonoChannels;
                case ChannelEnum.Left: return StereoChannels;
                case ChannelEnum.Right: return StereoChannels;
                default: throw new ValueNotSupportedException(channelEnum);
            }
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static Channel ChannelsToChannelEntity(this int thisChannels, int? channelValue, IContext context)
            => thisChannels.ChannelsToChannelEnum(channelValue).ToEntity(context);
        
        [Obsolete(ObsoleteMessage)]
        public static int ChannelEntityToChannels(this Channel entity) 
            => ChannelEnumToChannels(entity.ToEnum());
        
        [Obsolete(ObsoleteMessage)] public static bool IsMono  (this ChannelEnum obj) => obj.Channels() == MonoChannels;
        [Obsolete(ObsoleteMessage)] public static bool IsMono  (this Channel     obj) => obj.Channels() == MonoChannels;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this ChannelEnum obj) => obj.Channels() == StereoChannels;
        [Obsolete(ObsoleteMessage)] public static bool IsStereo(this Channel     obj) => obj.Channels() == StereoChannels;

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static ChannelEnum Mono(this ChannelEnum obj) => obj.Channels(MonoChannels);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static Channel Mono(this Channel obj, IContext context) => obj.Channels(MonoChannels, context);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static ChannelEnum Stereo(this ChannelEnum obj) => obj.Channels(StereoChannels);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static Channel Stereo(this Channel obj, IContext context) => obj.Channels(StereoChannels, context);

        // From ChannelWishes
                        
        [Obsolete(ObsoleteMessage)] 
        public static ChannelEnum ChannelToEnum(this int? channel, int channels) => channel.ChannelToEnum(channels.ChannelsToEnum());
        
        [Obsolete(ObsoleteMessage)] 
        public static ChannelEnum ChannelToEnum(this int? channel, SpeakerSetupEnum speakerSetupEnum)
        {
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono:
                    return ChannelEnum.Single;
                
                case SpeakerSetupEnum.Stereo:
                    if (channel == LeftChannel) return ChannelEnum.Left;
                    if (channel == RightChannel) return ChannelEnum.Right;
                    if (channel == ConfigWishes.NoChannel) return ChannelEnum.Undefined;
                    break;
                case SpeakerSetupEnum.Undefined:
                    if (channel == ConfigWishes.NoChannel) return ChannelEnum.Undefined;
                    // Tolerate inconsistent state for smooth switch between speaker setups.
                    if (channel == CenterChannel) return ChannelEnum.Single;
                    if (channel == LeftChannel) return ChannelEnum.Left;
                    if (channel == RightChannel) return ChannelEnum.Right;
                    break;
            }
            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { speakerSetupEnum, channel });
        }

        [Obsolete(ObsoleteMessage)] public static Channel ChannelToEntity(this int? channel, int channels, IContext context)
            => channel.ChannelToEnum(channels).ToEntity(context);
        
        [Obsolete(ObsoleteMessage)] public static Channel ChannelToEntity(this int? channel, SpeakerSetupEnum speakerSetupEnum, IContext context)
            => channel.ChannelToEnum(speakerSetupEnum).ToEntity(context);
    }
}
