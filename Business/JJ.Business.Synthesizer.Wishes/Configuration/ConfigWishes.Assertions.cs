using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    public partial class ConfigWishes
    {
        private static string BitsNotSupportedMessage(int? bits) => $"Bits = {bits} not supported. Supported values: 8, 16, 32.";
        private static string ChannelsNotSupportedMessage(int? channels) => $"Channels = {channels} not supported. Supported values: 1, 2.";
        private static string ChannelNotSupportedMessage(int? channel) => $"Channel = {channel} not supported. Supported values: 0, 1.";
        
        [AssertionMethod]
        public static int AssertBits(int bits)
        {
            switch (bits)
            {
                case 32: case 16: case 8: break; 
                default: throw new Exception(BitsNotSupportedMessage(bits)); 
            }
            return bits;
        }
        
        [AssertionMethod]
        public static int? AssertBits(int? bits)
        {
            switch (bits)
            {
                case null: case 0: case 32: case 16: case 8:  break; 
                default: throw new Exception(BitsNotSupportedMessage(bits)); 
            }
            return bits;
        }
                
        [AssertionMethod]
        public static int AssertChannels(int channels)
        {
            switch (channels)
            {
                case 1: case 2: break; 
                default: throw new Exception(ChannelsNotSupportedMessage(channels)); 
            }
            return channels;
        }
                       
        [AssertionMethod]
        public static int? AssertChannels(int? channels)
        {
            switch (channels)
            {
                case null: case 0: case 1: case 2: break; 
                default: throw new Exception(ChannelsNotSupportedMessage(channels)); 
            }
            return channels;
        }
                
        [AssertionMethod]
        public static int AssertChannel(int channel)
        {
            switch (channel)
            {
                case 0: case 1: break; 
                default: throw new Exception(ChannelNotSupportedMessage(channel)); 
            }
            return channel;
        }
        
        [AssertionMethod]
        public static int? AssertChannel(int? channel)
        {
            switch (channel)
            {
                case null: case 0: case 1: break; 
                default: throw new Exception(ChannelNotSupportedMessage(channel)); 
            }
            return channel;
        }

        [AssertionMethod]
        public static AudioFileFormatEnum Assert(AudioFileFormatEnum audioFormat)
        {
            switch (audioFormat)
            {
                case Raw: case Wav: break; 
                default: throw new ValueNotSupportedException(audioFormat); 
            }
            return audioFormat;
        }
                
        [AssertionMethod]
        internal static AudioFileFormatEnum? Assert(AudioFileFormatEnum? audioFormat)
        {
            switch (audioFormat)
            {
                case null: case AudioFileFormatEnum.Undefined: case Raw: case Wav: break; 
                default: throw new ValueNotSupportedException(audioFormat); 
            }
            return audioFormat;
        }

        [AssertionMethod]
        public static InterpolationTypeEnum Assert(InterpolationTypeEnum interpolation)
        {
            switch (interpolation)
            {
                case Line: case Block: break; 
                default: throw new ValueNotSupportedException(interpolation); 
            }
            return interpolation;
        }
                
        [AssertionMethod]
        public static InterpolationTypeEnum? Assert(InterpolationTypeEnum? interpolation)
        {
            switch (interpolation)
            {
                case null: case InterpolationTypeEnum.Undefined: case Line: case Block: break; 
                default: throw new ValueNotSupportedException(interpolation); 
            }
            return interpolation;
        }
                        
        [AssertionMethod]
        internal static int AssertSamplingRate(int samplingRate)
        {
            if (samplingRate <= 0) throw new Exception($"SamplingRate {samplingRate} should be greater than 0.");
            return samplingRate;
        }
                       
        [AssertionMethod]
        public static int? AssertSamplingRate(int? samplingRate)
        {
            if (!Has(samplingRate)) return samplingRate;
            return AssertSamplingRate(samplingRate.Value);
        }
                        
        [AssertionMethod]
        public static int AssertCourtesyFrames(int courtesyFrames)
        {
            if (courtesyFrames <= 0) throw new Exception($"CourtesyFrames {courtesyFrames} should be greater than 0.");
            return courtesyFrames;
        }
                       
        [AssertionMethod]
        public static int? AssertCourtesyFrames(int? courtesyFrames)
        {
            if (courtesyFrames == null) return null;
            return AssertCourtesyFrames(courtesyFrames.Value);
        }

    }
}
