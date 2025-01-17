using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    public static class ConfigCoalesceExtensionWishes
    {
        // Coalesce Defaults

        // Primary Audio Properties

        public static int CoalesceBits(this int? bits, int? defaultValue = null) => ConfigWishes.CoalesceBits(bits, defaultValue);
        public static int CoalesceChannels(this int? channels, int? defaultValue = null) => ConfigWishes.CoalesceChannels(channels, defaultValue);
        public static int CoalesceSamplingRate(this int? samplingRate, int? defaultValue = null) => ConfigWishes.CoalesceSamplingRate(samplingRate, defaultValue);
        public static AudioFileFormatEnum Coalesce(this AudioFileFormatEnum? audioFormat, AudioFileFormatEnum? defaultValue = null) => ConfigWishes.Coalesce(audioFormat, defaultValue);
        public static InterpolationTypeEnum Coalesce(this InterpolationTypeEnum? interpolation, InterpolationTypeEnum? defaultValue = null) => ConfigWishes.Coalesce(interpolation, defaultValue);
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
            => (Has(bits) ? bits.Value : Has(defaultValue) ? defaultValue.Value : DefaultBits).AssertBits();
        
        public static int CoalesceChannels(int? channels, int? defaultValue = null)
            => (Has(channels) ? channels.Value : Has(defaultValue) ? defaultValue.Value : DefaultChannels).AssertChannels();

        public static (int channels, int? channel) CoalesceChannelsChannelCombo((int? channels, int? channel) tuple)
            => CoalesceChannelsChannelCombo(tuple.channels, tuple.channel);

        public static (int channels, int? channel) CoalesceChannelsChannelCombo(int? channels, int? channel)
        {
            channels = (Has(channels) ? channels : DefaultChannels).AssertChannels();
            
            channel = AssertChannels(channel);
            
            if (channels == MonoChannels) return (MonoChannels, CenterChannel);
            if (channels == StereoChannels) return (StereoChannels, channel);
            
            throw new Exception($"Unsupported combination of values: {new { channels, channel }}");
        }
        
        public static int CoalesceSamplingRate(int? samplingRate, int? defaultValue = null) 
            => (Has(samplingRate) ? samplingRate.Value : Has(defaultValue) ? defaultValue.Value : DefaultSamplingRate).AssertSamplingRate();

        public static AudioFileFormatEnum Coalesce(AudioFileFormatEnum? audioFormat, AudioFileFormatEnum? defaultValue = null)
            => (Has(audioFormat) ? audioFormat.Value : Has(defaultValue) ? defaultValue.Value : DefaultAudioFormat).Assert();
        
        public static InterpolationTypeEnum Coalesce(InterpolationTypeEnum? interpolation, InterpolationTypeEnum? defaultValue = null)
            => (Has(interpolation) ? interpolation.Value : Has(defaultValue) ? defaultValue.Value : DefaultInterpolation).Assert();
        
        public static int CoalesceCourtesyFrames(int? courtesyFrames, int? defaultValue = null)
            => (courtesyFrames ?? defaultValue ?? DefaultCourtesyFrames).AssertCourtesyFrames();
        
        // Derived Audio Properties
        
        public static int CoalesceSizeOfBitDepth(int? sizeOfBitDepth, int? defaultValue = default)
            => (Has(sizeOfBitDepth) ? sizeOfBitDepth.Value : Has(defaultValue) ? defaultValue.Value : DefaultSizeOfBitDepth).AssertSizeOfBitDepth();

        public static double CoalesceMaxAmplitude(double? maxAmplitude, double? defaultValue = default)
            => (Has(maxAmplitude) ? maxAmplitude.Value : Has(defaultValue) ? defaultValue.Value : DefaultMaxAmplitude).AssertMaxAmplitude();

        public static int CoalesceFrameSize(int? frameSize, int? defaultValue = default)
            => (Has(frameSize) ? frameSize.Value : Has(defaultValue) ? defaultValue.Value : DefaultFrameSize).AssertFrameSize();
        
        public static int CoalesceFrameSize(int? frameSize, int? bits, int? channels)
            => (Has(frameSize) ? frameSize.Value : Has(bits) && Has(channels) ? FrameSize(bits, channels) : DefaultFrameSize).AssertFrameSize();

        public static int CoalesceHeaderLength(int? headerLength, int? defaultValue = default)
            => (Has(headerLength) ? headerLength.Value : Has(defaultValue) ? defaultValue.Value : DefaultHeaderLength).AssertHeaderLength();
        
        public static string CoalesceFileExtension(string fileExtension, string defaultValue = default)
            => AssertFileExtension(Has(fileExtension) ? fileExtension : Has(defaultValue) ? defaultValue : DefaultFileExtension);
        
        public static int CoalesceCourtesyBytes(int? courtesyBytes, int? defaultValue = default)
            => (Has(courtesyBytes) ? courtesyBytes.Value : Has(defaultValue) ? defaultValue.Value : DefaultCourtesyBytes).AssertCourtesyBytes();
 
        // Durations
        
        public static double CoalesceAudioLength(double? audioLength, double? defaultValue = default)
            => (Has(audioLength) ? audioLength.Value : Has(defaultValue) ? defaultValue.Value : DefaultAudioLength).AssertAudioLength();
 
        public static int CoalesceFrameCount(int? frameCount, int? defaultValue = default)
            => (Has(frameCount) ? frameCount.Value : Has(defaultValue) ? defaultValue.Value : DefaultFrameCount).AssertFrameCount();
 
        public static int CoalesceByteCount(int? byteCount, int? defaultValue = default)
            => (Has(byteCount) ? byteCount.Value : Has(defaultValue) ? defaultValue.Value : DefaultByteCount).AssertByteCount();
   }
}