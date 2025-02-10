using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
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
        
        // AudioFileOutput Nightmares

        // Align with back-end requirements:
        // Stereo channel tapes are registered as Mono with 1 channel, where Index 0 = Left and Index 1 = Right.
        // This makes meaning clash between Mono/Center and Left channel, which we have to deal with.

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static int? GetChannel(AudioFileOutput obj) 
        {
            AssertEntities(obj);
            
            switch (obj.AudioFileOutputChannels.Count)
            {
                case 1:
                {
                    // Covers Mono/Center case (Index = 0), and Stereo Left (Index = 0) and Right (Index = 1).
                    return obj.AudioFileOutputChannels[0].Index; 
                }
                case 2: 
                {
                    // Stereo case (2 channels = no specific channel = null)
                    return null;
                }
                default: 
                {
                    // Signal count other than 1 or 2 not supported.
                    throw new Exception("obj.AudioFileOutputChannels.Count = " + obj.AudioFileOutputChannels.Count + " not supported.");
                }
            }
        }        

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetChannel(AudioFileOutput obj, int? channel, IContext context)
        {
            AssertEntities(obj);
            
            switch (channel)
            {
                case null: return SetChannelEmpty(obj, context);
                case    0: return SetCenter      (obj, context);
                case    1: return SetRight       (obj, context);
                default  : AssertChannel(channel); return default;
            } 
        }

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static int  GetChannels(AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
           
            SpeakerSetupEnum speakerSetupEnum = obj.GetSpeakerSetupEnum();
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono:   return 1;
                case SpeakerSetupEnum.Stereo: return 2;
                default: throw new ValueNotSupportedException(speakerSetupEnum);
            }
        }

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetChannels(AudioFileOutput obj, int value, IContext context)
        {
            // This is ambiguous: Channels = 2 is stereo,
            // But stereo can also be a single channel (Left of Right), which is Mono in the back-end.
            // Maybe here an explicit SetChannels should mean 1 = Mono, 2 = Stereo,
            // differentiating it from specific behavior in methods like SetLeft and SetRight.
            
            if (obj == null) throw new NullException(() => obj);
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(value, context);
            // Do not adjust channels, to accommodate Left-Only and Right-Only scenarios with 1 channel, but Stereo speaker setup.
            CreateOrRemoveChannels(obj, value, context); 
            return obj;
        }
        
        // TODO: SetMono and SetStereo should have a Nightmare implementation as well.
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetChannelEmpty(AudioFileOutput obj, IContext context)
        {
            int channelCount = 2;
            
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(channelCount, context);
            CreateOrRemoveChannels(obj, channelCount, context); 
            
            AssertEntities(obj, channelCount);
            
            obj.AudioFileOutputChannels[0].Index = 0;
            obj.AudioFileOutputChannels[1].Index = 1;
            
            return obj;
        }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetCenter(AudioFileOutput obj, IContext context)
        {
            int channelCount = 1;

            obj.SpeakerSetup = GetSubstituteSpeakerSetup(channelCount, context);
            CreateOrRemoveChannels(obj, channelCount, context); 

            AssertEntities(obj, channelCount);

            obj.AudioFileOutputChannels[0].Index = 0;
            
            return obj;
        }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetLeft(AudioFileOutput obj, IContext context)
        {
            int channelCount = 1;
            
            // Left channel is represented as mono.
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(channelCount, context);
            CreateOrRemoveChannels(obj, channelCount, context); 

            AssertEntities(obj, channelCount);

            obj.AudioFileOutputChannels[0].Index = 0;

            return obj;
        }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetRight(AudioFileOutput obj, IContext context)
        {
            int channelCount = 1;
            
            // Right channel is represented as mono.
            obj.SpeakerSetup = GetSubstituteSpeakerSetup(channelCount, context);
            CreateOrRemoveChannels(obj, channelCount, context); 
            
            AssertEntities(obj, channelCount);

            // Right channel has channel index 1.
            obj.AudioFileOutputChannels[0].Index = 1;

            return obj;
        }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsChannelEmpty(AudioFileOutput obj)
        {
            AssertEntities(obj);
            
            // ChannelEmpty means "no specific channel",
            // which is only the case in case of a Stereo situation with both channels in it.
            return obj.GetSpeakerSetupEnum() == SpeakerSetupEnum.Stereo && 
                   obj.AudioFileOutputChannels.Count == 2 && 
                   obj.AudioFileOutputChannels[0].Index == 0 &&
                   obj.AudioFileOutputChannels[1].Index == 1;
        }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsCenter(AudioFileOutput obj)
        {
            AssertEntities(obj);
            
            // Yes, Mono/Center channel in the back-end is indistinguishable from Stereo/Left channel.
            return obj.GetSpeakerSetupEnum() == SpeakerSetupEnum.Mono && 
                   obj.AudioFileOutputChannels.Count == 1 && 
                   obj.AudioFileOutputChannels[0].Index == 0;
        }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsLeft(AudioFileOutput obj)
        {
            AssertEntities(obj);

            // Yes, the Left channel in the back-end is Mono.
            return obj.GetSpeakerSetupEnum() == SpeakerSetupEnum.Mono && 
                   obj.AudioFileOutputChannels.Count == 1 && 
                   obj.AudioFileOutputChannels[0].Index == 0;
        }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsRight(AudioFileOutput obj)
        {
            AssertEntities(obj);
            
            // Yes, a Right channel in the back-end is Mono.
            return obj.GetSpeakerSetupEnum() == SpeakerSetupEnum.Mono && 
                   obj.AudioFileOutputChannels.Count == 1 && 
                   obj.AudioFileOutputChannels[0].Index == 1;
        }
        
        private static void AssertEntities(AudioFileOutput obj, int? channelCount = null)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.AudioFileOutputChannels == null) throw new NullException(() => obj.AudioFileOutputChannels);
            if (obj.AudioFileOutputChannels.Contains(null)) throw new Exception("obj.AudioFileOutputChannels contains nulls.");
            if (channelCount != null)
            {
                if (obj.AudioFileOutputChannels.Count != channelCount) throw new Exception("obj.AudioFileOutputChannels.Count != "+ channelCount);
            }
        }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsMono(AudioFileOutput obj) => IsCenter(obj);
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsStereo(AudioFileOutput obj) => IsChannelEmpty(obj) || IsLeft(obj) || IsRight(obj);
    }
}
