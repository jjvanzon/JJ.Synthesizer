using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // From ChannelsWishes
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public static int Channels(this ChannelEnum obj) 
            => ChannelEnumToChannels(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] // ReSharper disable once UnusedParameter.Global
        public static ChannelEnum Channels(this ChannelEnum obj, int channels) 
            => ChannelsToChannelEnum(channels, Channel(obj));
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static int Channels(this Channel obj) => ChannelEntityToChannels(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static Channel Channels(this Channel obj, int channels, IContext context)
            => ChannelsToChannelEntity(channels, Channel(obj), context);
            
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public static ChannelEnum ChannelsToChannelEnum(this int channels, int? channel)
        {
            if (channel == null) return ChannelEnum.Undefined;
            if (channels == 1 && channel == 0) return ChannelEnum.Single;
            if (channels == 2 && channel == 0) return ChannelEnum.Left;
            if (channels == 2 && channel == 1) return ChannelEnum.Right;
            throw new Exception($"Unsupported combination of values {new { channels, channel }}");
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static int ChannelEnumToChannels(this ChannelEnum channelEnum)
        {
            switch (channelEnum)
            {
                case ChannelEnum.Undefined: return 0;
                case ChannelEnum.Single: return 1;
                case ChannelEnum.Left: return 2;
                case ChannelEnum.Right: return 2;
                default: throw new ValueNotSupportedException(channelEnum);
            }
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static Channel ChannelsToChannelEntity(this int channels, int? channel, IContext context)
            => ChannelsToChannelEnum(channels, channel).ToEntity(context);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public static int ChannelEntityToChannels(this Channel entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return ChannelEnumToChannels(entity.ToEnum());
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsMono  (this ChannelEnum obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsMono  (this Channel     obj) => Channels(obj) == 1;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsStereo(this ChannelEnum obj) => Channels(obj) == 2;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static bool IsStereo(this Channel     obj) => Channels(obj) == 2;

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static ChannelEnum Mono(this ChannelEnum obj) => Channels(obj, 1);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static Channel Mono(this Channel obj, IContext context) => Channels(obj, 1, context);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static ChannelEnum Stereo(this ChannelEnum obj) => Channels(obj, 2);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static Channel Stereo(this Channel obj, IContext context) => Channels(obj, 2, context);

        // From ChannelWishes
                        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static ChannelEnum ChannelToEnum(this int? channel, int channels)
            => ChannelToEnum(channel, channels.ChannelsToEnum());
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] 
        public static ChannelEnum ChannelToEnum(this int? channel, SpeakerSetupEnum speakerSetupEnum)
        {
            if (channel == default) return default;
            
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono:
                    if (channel == 0) return ChannelEnum.Single;
                    break;
                
                case SpeakerSetupEnum.Stereo:
                    if (channel == 0) return ChannelEnum.Left;
                    if (channel == 1) return ChannelEnum.Right;
                    break;
            }
            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { speakerSetupEnum, channel });
        }

        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static Channel ChannelToEntity(this int? channel, int channels, IContext context)
            => ChannelToEnum(channel, channels).ToEntity(context);
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static Channel ChannelToEntity(this int? channel, SpeakerSetupEnum speakerSetupEnum, IContext context)
            => ChannelToEnum(channel, speakerSetupEnum).ToEntity(context);
    }
}
