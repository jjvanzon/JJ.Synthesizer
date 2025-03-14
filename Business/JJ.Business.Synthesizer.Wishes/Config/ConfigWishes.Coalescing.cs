using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Core.Common;
using static JJ.Framework.Core.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    public static class ConfigCoalesceExtensionWishes
    {
        // Coalesce Defaults
        
        // Primary Audio Properties

        public static int CoalesceBits(this int? bits, int? defaultValue = null) => ConfigWishes.CoalesceBits(bits, defaultValue);
        public static int CoalesceChannels(this int? channels, int? defaultValue = null) => ConfigWishes.CoalesceChannels(channels, defaultValue);
        public static int CoalesceSamplingRate(this int? samplingRate, int? defaultValue = null) => ConfigWishes.CoalesceSamplingRate(samplingRate, defaultValue);
        public static AudioFileFormatEnum Coalesce(this AudioFileFormatEnum? audioFormat, AudioFileFormatEnum? defaultValue = null) => ConfigWishes.CoalesceAudioFormat(audioFormat, defaultValue);
        public static InterpolationTypeEnum Coalesce(this InterpolationTypeEnum? interpolation, InterpolationTypeEnum? defaultValue = null) => ConfigWishes.CoalesceInterpolation(interpolation, defaultValue);
        public static int CoalesceCourtesyFrames(this int? courtesyFrames, int? defaultValue = null) => ConfigWishes.CoalesceCourtesyFrames(courtesyFrames, defaultValue);

        // Derived Audio Properties

        public static int CoalesceSizeOfBitDepth(this int? sizeOfBitDepth, int? defaultValue = null) => ConfigWishes.CoalesceSizeOfBitDepth(sizeOfBitDepth, defaultValue);
        public static double CoalesceMaxAmplitude(this double? maxAmplitude, double? defaultValue = null) => ConfigWishes.CoalesceMaxAmplitude(maxAmplitude, defaultValue);
        public static int CoalesceFrameSize(this int? frameSize, int? defaultValue = null) => ConfigWishes.CoalesceFrameSize(frameSize, defaultValue);
        public static int CoalesceFrameSize(this int? frameSize, int? bits, int? channels) => ConfigWishes.CoalesceFrameSize(frameSize, bits, channels);
        public static int CoalesceHeaderLength(this int? headerLength, int? defaultValue = null) => ConfigWishes.CoalesceHeaderLength(headerLength, defaultValue);
        public static string CoalesceFileExtension(this string fileExtension, string defaultValue = null) => ConfigWishes.CoalesceFileExtension(fileExtension, defaultValue);
        public static int CoalesceCourtesyBytes(this int? courtesyBytes, int? defaultValue = null) => ConfigWishes.CoalesceCourtesyBytes(courtesyBytes, defaultValue);

        // Durations

        public static double CoalesceAudioLength(this double? audioLength, double? defaultValue = null) => ConfigWishes.CoalesceAudioLength(audioLength, defaultValue);
        public static int CoalesceFrameCount(this int? frameCount, int? defaultValue = null) => ConfigWishes.CoalesceFrameCount(frameCount, defaultValue);
        public static int CoalesceByteCount(this int? byteCount, int? defaultValue = null) => ConfigWishes.CoalesceByteCount(byteCount, defaultValue);
    }

    public partial class ConfigWishes
    {
        // Coalesce Defaults
        

        // Primary Audio Properties
        
        public static int CoalesceBits(int? bits, int? defaultValue = null)
            => Coalesce(bits, defaultValue, DefaultBits).AssertBits();
        
        public static int CoalesceChannels(int? channels, int? defaultValue = null)
            => Coalesce(channels, defaultValue, DefaultChannels).AssertChannels();

        public static (int channels, int? channel) CoalesceChannelsChannelCombo(int? channels, int? channel, int? defaultChannels = null, int? defaultChannel = null)
        {
            channels = CoalesceChannels(channels, defaultChannels);
            channel = AssertChannel(channel ?? defaultChannel);
            
            if (channels == MonoChannels) return (MonoChannels, CenterChannel);
            if (channels == StereoChannels) return (StereoChannels, channel);
            
            throw new Exception($"Unsupported combination of values: {new { channels, channel }}");
        }
        
        public static int CoalesceSamplingRate(int? samplingRate, int? defaultValue = null) 
            => Coalesce(samplingRate, defaultValue, DefaultSamplingRate).AssertSamplingRate();

        public static AudioFileFormatEnum CoalesceAudioFormat(AudioFileFormatEnum? audioFormat, AudioFileFormatEnum? defaultValue = null)
            => Coalesce(audioFormat, defaultValue, DefaultAudioFormat).AssertAudioFormat();
        
        public static InterpolationTypeEnum CoalesceInterpolation(InterpolationTypeEnum? interpolation, InterpolationTypeEnum? defaultValue = null)
            => Coalesce(interpolation, defaultValue, DefaultInterpolation).AssertInterpolation();
        
        public static int CoalesceCourtesyFrames(int? courtesyFrames, int? defaultValue = null)
            => (courtesyFrames ?? defaultValue ?? DefaultCourtesyFrames).AssertCourtesyFrames();
        
        // Derived Audio Properties
        
        public static int CoalesceSizeOfBitDepth(int? sizeOfBitDepth, int? defaultValue = default)
            => Coalesce(sizeOfBitDepth, defaultValue, DefaultSizeOfBitDepth).AssertSizeOfBitDepth();

        public static double CoalesceMaxAmplitude(double? maxAmplitude, double? defaultValue = default)
            => Coalesce(maxAmplitude, defaultValue, DefaultMaxAmplitude).AssertMaxAmplitude();

        public static int CoalesceFrameSize(int? frameSize, int? defaultValue = default)
            => Coalesce(frameSize, defaultValue, DefaultFrameSize).AssertFrameSize();
        
        public static int CoalesceFrameSize(int? frameSize, int? bits, int? channels)
            => (Has(frameSize) ? frameSize.Value : Has(bits) && Has(channels) ? FrameSize(bits, channels) : DefaultFrameSize).AssertFrameSize();

        public static int CoalesceHeaderLength(int? headerLength, int? defaultValue = default)
            => (headerLength ?? defaultValue ?? DefaultHeaderLength).AssertHeaderLength();
        
        public static string CoalesceFileExtension(string fileExtension, string defaultValue = default)
            => AssertFileExtension(Has(fileExtension) ? fileExtension : Has(defaultValue) ? defaultValue : DefaultFileExtension);
        
        public static int CoalesceCourtesyBytes(int? courtesyBytes, int? defaultValue = default)
            => (courtesyBytes ?? defaultValue ?? DefaultCourtesyBytes).AssertCourtesyBytes();
 
        // Durations
        
        public static double CoalesceAudioLength(double? audioLength, double? defaultValue = default)
            => (audioLength ?? defaultValue ?? DefaultAudioLength).AssertAudioLength();
 
        public static int CoalesceFrameCount(int? frameCount, int? defaultValue = default)
            => (frameCount ?? defaultValue ?? DefaultFrameCount).AssertFrameCount();
 
        public static int CoalesceByteCount(int? byteCount, int? defaultValue = default)
            => (byteCount ?? defaultValue ?? DefaultByteCount).AssertByteCount();

        // Stringy Coalesces

        public static string CoalesceBits           (int?    bits,           string defaultText) => $"{CoalesceBits          (bits)          }".Coalesce(defaultText);
        public static string CoalesceChannels       (int?    channels,       string defaultText) => $"{CoalesceChannels      (channels)      }".Coalesce(defaultText);
        public static string CoalesceSamplingRate   (int?    samplingRate,   string defaultText) => $"{CoalesceSamplingRate  (samplingRate)  }".Coalesce(defaultText);
        public static string CoalesceCourtesyFrames (int?    courtesyFrames, string defaultText) => $"{CoalesceCourtesyFrames(courtesyFrames)}".Coalesce(defaultText);
        public static string CoalesceSizeOfBitDepth (int?    sizeOfBitDepth, string defaultText) => $"{CoalesceSizeOfBitDepth(sizeOfBitDepth)}".Coalesce(defaultText);
        public static string CoalesceMaxAmplitude   (double? maxAmplitude,   string defaultText) => $"{CoalesceMaxAmplitude  (maxAmplitude)  }".Coalesce(defaultText);
        public static string CoalesceFrameSize      (int?    frameSize,      string defaultText) => $"{CoalesceFrameSize     (frameSize)     }".Coalesce(defaultText);
        public static string CoalesceHeaderLength   (int?    headerLength,   string defaultText) => $"{CoalesceHeaderLength  (headerLength)  }".Coalesce(defaultText);
        public static string CoalesceCourtesyBytes  (int?    courtesyBytes,  string defaultText) => $"{CoalesceCourtesyBytes (courtesyBytes) }".Coalesce(defaultText);
        public static string CoalesceAudioLength    (double? audioLength,    string defaultText) => $"{CoalesceAudioLength   (audioLength)   }".Coalesce(defaultText);
        public static string CoalesceFrameCount     (int?    frameCount,     string defaultText) => $"{CoalesceFrameCount    (frameCount)    }".Coalesce(defaultText);
        public static string CoalesceByteCount      (int?    byteCount,      string defaultText) => $"{CoalesceByteCount     (byteCount)     }".Coalesce(defaultText);
        public static string CoalesceAudioFormat    (AudioFileFormatEnum? audioFormat,         string defaultValue) => CoalesceAudioFormat   (audioFormat)              .Coalesce(defaultValue);
        public static string CoalesceInterpolation  (InterpolationTypeEnum? interpolation,     string defaultValue) => CoalesceInterpolation (interpolation)            .Coalesce(defaultValue);
        public static string CoalesceFrameSize      (int? frameSize, int? bits, int? channels, string defaultValue) => CoalesceFrameSize     (frameSize, bits, channels).Coalesce(defaultValue);
    }
}