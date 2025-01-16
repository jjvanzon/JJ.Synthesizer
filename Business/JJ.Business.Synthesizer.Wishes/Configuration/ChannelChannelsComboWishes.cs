using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ChannelChannelsComboExtensionWishes
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
        public static ChannelEnum Channel(this ChannelEnum oldChannelEnum, int? newChannelValue)
        {
            // Unspecified
            if (newChannelValue == null) return ChannelEnum.Undefined;

            // Helper variable
            int oldChannels = oldChannelEnum.Channels();

            // Mono
            if (oldChannels == MonoChannels)
            {
                return ChannelEnum.Single;
            }
            
            // Stereo case
            if (oldChannels == StereoChannels)
            {
                if (newChannelValue == 0) return ChannelEnum.Left;
                if (newChannelValue == 1) return ChannelEnum.Right;
            }

            // Fallback: Tolerate inconsistent state for fluent switch between speaker setups.
            if (oldChannels == NoChannels)
            {
                if (newChannelValue == 0) return ChannelEnum.Single;
                if (newChannelValue == 1) return ChannelEnum.Right;
            }
         
            throw new Exception($"Unsupported combination of values: {new{ oldChannels, oldChannelEnum, newChannelValue }}");
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static Channel Channel(this Channel oldChannelEntity, int? newChannelValue, IContext context)
        {
            // Unspecified
            if (newChannelValue == null) return ChannelEnum.Undefined.ToEntity(context);

            int oldChannels = oldChannelEntity.Channels();

            // Mono
            if (oldChannels == MonoChannels)
            {
                return ChannelEnum.Single.ToEntity(context);
            }
            
            // Stereo case
            if (oldChannels == StereoChannels)
            {
                if (newChannelValue == 0) return ChannelEnum.Left.ToEntity(context);
                if (newChannelValue == 1) return ChannelEnum.Right.ToEntity(context);
            }

            // Fallback for inconsistent state for fluent switch between speaker setups.
            if (oldChannels == NoChannels)
            {
                if (newChannelValue == 0) return ChannelEnum.Single.ToEntity(context);
                if (newChannelValue == 1) return ChannelEnum.Right.ToEntity(context);
            }
         
            throw new Exception($"Unsupported combination of values: {new{ oldChannels, oldChannelEntity_Name = oldChannelEntity.Name, newChannelValue }}");
        }

        // Conversion To/From Channels (Mono/Stereo)
        
        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum ChannelsToChannelEnum(this int oldChannels, int? newChannelValue)
        {
            AssertChannels(oldChannels);
            AssertChannel(newChannelValue);
            
            if (oldChannels == MonoChannels) return ChannelEnum.Single;
            if (oldChannels == StereoChannels && newChannelValue == LeftChannel) return ChannelEnum.Left;
            if (oldChannels == StereoChannels && newChannelValue == RightChannel) return ChannelEnum.Right;
            if (oldChannels == StereoChannels && newChannelValue == ChannelEmpty) return ChannelEnum.Undefined;
            throw new Exception($"Unsupported combination of values {new { oldChannels, newChannelValue }}");
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static int ChannelEnumToChannels(this ChannelEnum channelEnum)
        {
            switch (channelEnum)
            {
                case ChannelEnum.Single: return MonoChannels;
                case ChannelEnum.Left: return StereoChannels;
                case ChannelEnum.Right: return StereoChannels;
                case ChannelEnum.Undefined: return StereoChannels; // Undefined = stereo signal with 2 channels = not a specific channel
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
            // Mono
            if (channels == MonoChannels)
            {
                return ChannelEnum.Single;
            }
            
            // Stereo case
            if (channels == StereoChannels)
            {
                if (channel == null) return ChannelEnum.Undefined;
                if (channel ==    0) return ChannelEnum.Left;
                if (channel ==    1) return ChannelEnum.Right;
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
        public static ChannelEnum Mono(this ChannelEnum oldChannelEnum) => ChannelEnum.Single;
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        // ReSharper disable once UnusedParameter.Global
        public static Channel Mono(this Channel oldChannelEntity, IContext context) => ChannelEnum.Single.ToEntity(context);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static ChannelEnum Stereo(this ChannelEnum oldChannelEnum)
        {
            switch (oldChannelEnum)
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
        public static Channel Stereo(this Channel oldChannelEntity, IContext context) => oldChannelEntity.ToEnum().Stereo().ToEntity(context);
    }
}
