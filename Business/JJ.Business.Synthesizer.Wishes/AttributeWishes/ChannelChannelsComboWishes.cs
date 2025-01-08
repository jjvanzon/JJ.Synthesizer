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
        // Channels (Mono/Stereo)
        
        [Obsolete(ObsoleteMessage)]
        public static int Channels(this ChannelEnum obj) => obj.ChannelEnumToChannels();
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] // ReSharper disable once UnusedParameter.Global
        public static ChannelEnum Channels(this ChannelEnum oldChannelEnum, int newChannelsValue)
        {
            if (newChannelsValue == NoChannels) return ChannelEnum.Undefined;
            if (newChannelsValue == MonoChannels) return ChannelEnum.Single;
            if (newChannelsValue == StereoChannels) 
            {
                if (oldChannelEnum == ChannelEnum.Left) return ChannelEnum.Left;
                if (oldChannelEnum == ChannelEnum.Right) return ChannelEnum.Right;
                if (oldChannelEnum  == ChannelEnum.Single) return ChannelEnum.Left;
                if (oldChannelEnum  == ChannelEnum.Undefined) return ChannelEnum.Undefined;
            }
            throw new Exception($"Unsupported value: {new { newChannelsValue }}");
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static int Channels(this Channel obj) => obj.ToEnum().ChannelEnumToChannels();

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static Channel Channels(this Channel oldChannelEntity, int newChannelsValue, IContext context)
        {
            if (newChannelsValue == NoChannels) return null;
            if (newChannelsValue == MonoChannels) return ChannelEnum.Single.ToEntity(context);
            if (newChannelsValue == StereoChannels)
            {
                ChannelEnum oldChannelEnum = oldChannelEntity.ToEnum();
                switch (oldChannelEnum)
                {
                    case ChannelEnum.Left: return ChannelEnum.Left.ToEntity(context);
                    case ChannelEnum.Right: return ChannelEnum.Right.ToEntity(context);
                    case ChannelEnum.Single: return ChannelEnum.Left.ToEntity(context);
                    case ChannelEnum.Undefined: return null;
                    default: throw new ValueNotSupportedException(oldChannelEnum);
                }
            }
            
            throw new Exception($"Unsupported value: {new { newChannelsValue }}");
        }

        // Channel (Center/Left/Right)

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum Channel(this ChannelEnum thisObj, int? channel)
        {
            // Unspecified
            if (channel == null) return ChannelEnum.Undefined;

            // Helper variable
            int channels = thisObj.Channels();

            // Mono
            if (channels == MonoChannels)
            {
                return ChannelEnum.Single;
            }
            
            // Stereo case
            if (channels == StereoChannels)
            {
                if (channel == 0) return ChannelEnum.Left;
                if (channel == 1) return ChannelEnum.Right;
            }

            // Fallback: Tolerate inconsistent state for fluent switch between speaker setups.
            if (channels == NoChannels)
            {
                if (channel == 0) return ChannelEnum.Single;
                if (channel == 1) return ChannelEnum.Right;
            }
         
            throw new Exception($"Unsupported combination of values: {new{ channels, channel }}");
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static Channel Channel(this Channel thisObj, int? channel, IContext context)
        {
            // Unspecified
            if (channel == null) return ChannelEnum.Undefined.ToEntity(context);

            int channels = thisObj.Channels();

            // Mono
            if (channels == MonoChannels)
            {
                return ChannelEnum.Single.ToEntity(context);
            }
            
            // Stereo case
            if (channels == StereoChannels)
            {
                if (channel == 0) return ChannelEnum.Left.ToEntity(context);
                if (channel == 1) return ChannelEnum.Right.ToEntity(context);
            }

            // Fallback for inconsistent state for fluent switch between speaker setups.
            if (channels == NoChannels)
            {
                if (channel == 0) return ChannelEnum.Single.ToEntity(context);
                if (channel == 1) return ChannelEnum.Right.ToEntity(context);
            }
         
            throw new Exception($"Unsupported combination of values: {new{ channels, channel }}");
        }

        // Conversion To/From Channels (Mono/Stereo)
        
        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum ChannelsToChannelEnum(this int thisChannels, int? channelValue)
        {
            AssertChannels(thisChannels);
            AssertChannel(channelValue);
            
            if (thisChannels == MonoChannels) return ChannelEnum.Single;
            if (thisChannels == StereoChannels && channelValue == LeftChannel) return ChannelEnum.Left;
            if (thisChannels == StereoChannels && channelValue == RightChannel) return ChannelEnum.Right;
            if (thisChannels == StereoChannels && channelValue == ChannelEmpty) return ChannelEnum.Undefined;
            throw new Exception($"Unsupported combination of values {new { channels = thisChannels, channel = channelValue }}");
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static int ChannelEnumToChannels(this ChannelEnum channelEnum)
        {
            switch (channelEnum)
            {
                case ChannelEnum.Undefined: return NoChannels;
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
            => entity.ToEnum().ChannelEnumToChannels();

        // Conversion To/From Channel (Center/Left/Right)

        [Obsolete(ObsoleteMessage)] 
        public static ChannelEnum ChannelToEnum(this int? channel, int channels)
        {
            // Unspecified
            if (channel == null) return ChannelEnum.Undefined;

            // Mono
            if (channels == MonoChannels)
            {
                return ChannelEnum.Single;
            }
            
            // Stereo case
            if (channels == StereoChannels)
            {
                if (channel == 0) return ChannelEnum.Left;
                if (channel == 1) return ChannelEnum.Right;
            }

            // Fallback: Tolerate inconsistent state for fluent switch between speaker setups.
            if (channels == NoChannels)
            {
                if (channel == 0) return ChannelEnum.Single;
                if (channel == 1) return ChannelEnum.Right;
            }
         
            throw new Exception($"Unsupported combination of values: {new{ channels, channel }}");
        }
        
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
                    if (channel == ChannelEmpty) return ChannelEnum.Undefined;
                    break;
                case SpeakerSetupEnum.Undefined:
                    if (channel == ChannelEmpty) return ChannelEnum.Undefined;
                    // Tolerate inconsistent state for smooth switch between speaker setups.
                    if (channel == CenterChannel) return ChannelEnum.Single;
                    if (channel == LeftChannel) return ChannelEnum.Left;
                    if (channel == RightChannel) return ChannelEnum.Right;
                    break;
            }
            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { speakerSetupEnum, channel });
        }

        [Obsolete(ObsoleteMessage)] 
        public static Channel ChannelToEntity(this int? channel, int channels, IContext context)
            => ChannelToEnum(channel, channels).ToEntity(context);
        
        [Obsolete(ObsoleteMessage)] 
        public static Channel ChannelToEntity(this int? channel, SpeakerSetupEnum speakerSetupEnum, IContext context)
            => ChannelToEnum(channel, speakerSetupEnum).ToEntity(context);

        // Shorthand

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        // ReSharper disable once UnusedParameter.Global
        public static ChannelEnum Mono(this ChannelEnum obj) => ChannelEnum.Single;
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        // ReSharper disable once UnusedParameter.Global
        public static Channel Mono(this Channel obj, IContext context) => ChannelEnum.Single.ToEntity(context);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static ChannelEnum Stereo(this ChannelEnum obj)
        {
            switch (obj)
            {
                case ChannelEnum.Undefined: return ChannelEnum.Undefined;
                case ChannelEnum.Left: return ChannelEnum.Left;
                case ChannelEnum.Right: return ChannelEnum.Right;
                case ChannelEnum.Single: return ChannelEnum.Left;
                default: return ChannelEnum.Left; // Default for smoother switch between Mono/Stereo
            }
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static Channel Stereo(this Channel obj, IContext context) => obj.ToEnum().Stereo().ToEntity(context);
    }
}
