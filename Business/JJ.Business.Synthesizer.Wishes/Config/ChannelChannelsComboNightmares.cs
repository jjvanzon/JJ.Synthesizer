using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

// ReSharper disable UnusedParameter.Global
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable IDE0002

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ConfigNightmareExtension
    {
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelToEnum(this int? thisChannel, SpeakerSetupEnum speakerSetupEnumForContext)
        {
            return ConfigNightmares.ChannelToEnum(thisChannel, speakerSetupEnumForContext);
        }
        
        [Obsolete(ObsoleteMessage)] public static Channel ChannelToEntity(this int? thisChannel, SpeakerSetupEnum speakerSetupEnumForContext, IContext context)
        {
            return ConfigNightmares.ChannelToEntity(thisChannel, speakerSetupEnumForContext, context);
        }
    }

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public class ConfigNightmares
    {
        // Channels (Mono/Stereo)

        [Obsolete(ObsoleteMessage)] public static bool IsStereo(ChannelEnum channelEnum) 
            => channelEnum == ChannelEnum.Left || channelEnum == ChannelEnum.Right || channelEnum == ChannelEnum.Undefined; // Undefined = stereo signal with 2 channels = not a specific channel

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetChannels(ChannelEnum oldChannelEnum, int newChannelsValue)
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
        
        
                
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetStereo(ChannelEnum oldChannelEnum)
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
        [Obsolete(ObsoleteMessage)] public static Channel SetChannels(Channel thisChannelEntity, int channelsForContext, IContext context)
        {
            if (channelsForContext == NoChannels) return null;
            if (channelsForContext == MonoChannels) return ChannelEnum.Single.ToEntity(context);
            if (channelsForContext == StereoChannels)
            {
                ChannelEnum oldChannelEnum = thisChannelEntity.ToEnum();
                switch (oldChannelEnum)
                {
                    case ChannelEnum.Left: return ChannelEnum.Left.ToEntity(context);
                    case ChannelEnum.Right: return ChannelEnum.Right.ToEntity(context);
                    case ChannelEnum.Single: return ChannelEnum.Left.ToEntity(context);
                    case ChannelEnum.Undefined: return null;
                    default: throw new ValueNotSupportedException(oldChannelEnum);
                }
            }
            
            throw new Exception($"Unsupported value: {new { channelsForContext }}");
        }
        
        
        // Channel (Center/Left/Right)

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetChannel(ChannelEnum oldChannelEnumForContext, int? newChannelValue)
        {
            // Unspecified
            if (newChannelValue == null) return ChannelEnum.Undefined;

            // Helper variable
            int oldChannels = oldChannelEnumForContext.Channels();

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
         
            throw new Exception($"Unsupported combination of values: {new{ oldChannels, oldChannelEnumForContext, newChannelValue }}");
        }

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetChannel(Channel oldChannelEntity, int? newChannelValue, IContext context)
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

        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelsToChannelEnum(int theseChannels, int? channelForContext)
        {
            AssertChannels(theseChannels);
            AssertChannel(channelForContext);
            
            if (theseChannels == MonoChannels) return ChannelEnum.Single;
            if (theseChannels == StereoChannels && channelForContext == LeftChannel) return ChannelEnum.Left;
            if (theseChannels == StereoChannels && channelForContext == RightChannel) return ChannelEnum.Right;
            if (theseChannels == StereoChannels && channelForContext == ChannelEmpty) return ChannelEnum.Undefined;
            throw new Exception($"Unsupported combination of values {new { theseChannels, channelForContext }}");
        }
        
        [Obsolete(ObsoleteMessage)] public static int ChannelEnumToChannels(ChannelEnum channelEnum)
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
        
        // Conversion To/From Channel (Center/Left/Right)

        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelToEnum(int? thisChannel, int channelsForContext)
        {
            // Mono
            if (channelsForContext == MonoChannels)
            {
                return ChannelEnum.Single;
            }
            
            // Stereo case
            if (channelsForContext == StereoChannels)
            {
                if (thisChannel == null) return ChannelEnum.Undefined;
                if (thisChannel ==    0) return ChannelEnum.Left;
                if (thisChannel ==    1) return ChannelEnum.Right;
            }

            // Fallback: Tolerate inconsistent state for fluent switch between speaker setups.
            if (channelsForContext == NoChannels)
            {
                if (thisChannel == 0) return ChannelEnum.Single;
                if (thisChannel == 1) return ChannelEnum.Right;
            }
         
            throw new Exception($"Unsupported combination of values: {new{ channels = channelsForContext, channel = thisChannel }}");
        }
                
        [Obsolete(ObsoleteMessage)] public static Channel ChannelToEntity(int? thisChannel, SpeakerSetupEnum speakerSetupEnumForContext, IContext context)
        {
            return ConfigNightmares.ChannelToEnum(thisChannel, speakerSetupEnumForContext).ToEntity(context);
        }

        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelToEnum(int? thisChannel, SpeakerSetupEnum speakerSetupEnumForContext)
        {
            switch (speakerSetupEnumForContext)
            {
                case SpeakerSetupEnum.Mono:
                    return ChannelEnum.Single;
                
                case SpeakerSetupEnum.Stereo:
                    if (thisChannel == LeftChannel) return ChannelEnum.Left;
                    if (thisChannel == RightChannel) return ChannelEnum.Right;
                    if (thisChannel == ChannelEmpty) return ChannelEnum.Undefined;
                    break;
                    
                case SpeakerSetupEnum.Undefined:
                    if (thisChannel == ChannelEmpty) return ChannelEnum.Undefined;
                    // Tolerate inconsistent state for smooth switch between speaker setups.
                    if (thisChannel == CenterChannel) return ChannelEnum.Single;
                    if (thisChannel == LeftChannel) return ChannelEnum.Left;
                    if (thisChannel == RightChannel) return ChannelEnum.Right;
                    break;
            }
            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { speakerSetupEnumForContext, thisChannel });
        }
        
        public static int? GetChannel(AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.AudioFileOutputChannels == null) throw new NullException(() => obj.AudioFileOutputChannels);
            
            int channels = obj.Channels();
            int signalCount = obj.AudioFileOutputChannels.Count;
            int? firstChannelNumber = obj.AudioFileOutputChannels.ElementAtOrDefault(0)?.Channel();
            
            if (channels == MonoChannels)
            {
                if (firstChannelNumber.HasValue) 
                {
                    // Handles Right-Channel-Only case
                    return firstChannelNumber.Value;
                }
                
                // Mono has channel 0 only.
                return CenterChannel;
            }

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
        public static AudioFileOutput SetChannel(AudioFileOutput obj, int? channel, IContext context)
        {
            if (obj                         == null) throw new NullException(() => obj);
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

        public static AudioFileOutput SetChannels(AudioFileOutput obj, int value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(value, context);
            // Do not adjust channels, to accommodate Left-Only and Right-Only scenarios with 1 channel, but Stereo speaker setup.
            //CreateOrRemoveChannels(obj, value, context); 
            return obj;
        }
        
        // Stereo can be Right. Right not Mono.
        public static bool IsMono  (AudioFileOutput obj) => obj.GetChannels() == MonoChannels   && obj.GetChannel() != RightChannel;
        public static bool IsStereo(AudioFileOutput obj) => obj.GetChannels() == StereoChannels || obj.GetChannel() == RightChannel;
        public static bool IsMono  (Buff            obj) => obj.GetChannels() == MonoChannels   && obj.GetChannel() != RightChannel;
        public static bool IsStereo(Buff            obj) => obj.GetChannels() == StereoChannels || obj.GetChannel() == RightChannel;

        // TODO: Consider using this:
        // Draft of longer version but perhaps better readable?
        //public static bool IsMono(AudioFileOutput obj)
        //{
        //    if (obj.GetChannels() == MonoChannels)
        //    {
        //        // Stereo can be Right. Right not Mono.
        //        return obj.GetChannel() != RightChannel;
        //    }

        //    return false;
        //}

        //public static bool IsStereo(AudioFileOutput obj)
        //{
        //    if (obj.GetChannels() == StereoChannels)
        //    {
        //        return true; // Standard stereo case.
        //    }

        //    // Stereo can be Right. Right not Mono.
        //    if (obj.GetChannel() == RightChannel)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

    }
}
