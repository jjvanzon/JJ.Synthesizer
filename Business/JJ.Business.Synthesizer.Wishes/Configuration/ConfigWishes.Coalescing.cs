using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    public partial class ConfigWishes
    {
        // Coalesce Defaults
        
        // Primary Audio Properties
        
        public static int CoalesceBits(int? bits, int? defaultValue = null)
            => AssertBits(Has(bits) ? bits.Value : Has(defaultValue) ? defaultValue.Value : DefaultBits);
        
        public static int CoalesceChannels(int? channels, int? defaultValue = null)
            => AssertChannels(Has(channels) ? channels.Value : Has(defaultValue) ? defaultValue.Value : DefaultChannels);
                            
        public static int CoalesceSamplingRate(int? samplingRate, int? defaultValue = null) 
            => AssertSamplingRate(Has(samplingRate) ? samplingRate.Value : Has(defaultValue) ? defaultValue.Value : DefaultSamplingRate);

        public static AudioFileFormatEnum Coalesce(AudioFileFormatEnum? audioFormat, AudioFileFormatEnum? defaultValue = null)
            => Assert(Has(audioFormat) ? audioFormat.Value : Has(defaultValue) ? defaultValue.Value : DefaultAudioFormat);
        
        public static InterpolationTypeEnum Coalesce(InterpolationTypeEnum? interpolation, InterpolationTypeEnum? defaultValue = null)
            => Assert(Has(interpolation) ? interpolation.Value : Has(defaultValue) ? defaultValue.Value : DefaultInterpolation);
        
        public static int CoalesceCourtesyFrames(int? courtesyFrames, int? defaultValue = null)
            => AssertCourtesyFrames(courtesyFrames ?? defaultValue ?? DefaultCourtesyFrames);
        
        // Derived Audio Properties
        
        public static int CoalesceSizeOfBitDepth(int? sizeOfBitDepth, int? defaultValue = null)
            => AssertSizeOfBitDepth(Has(sizeOfBitDepth) ? sizeOfBitDepth.Value : Has(defaultValue) ? defaultValue.Value : DefaultSizeOfBitDepth);

        public static double CoalesceMaxAmplitude(double? maxAmplitude, double? defaultValue = null)
            => AssertMaxAmplitude(Has(maxAmplitude) ? maxAmplitude.Value : Has(defaultValue) ? defaultValue.Value : DefaultMaxAmplitude);

        public static int CoalesceFrameSize(int? frameSize, int? defaultValue)
            => AssertFrameSize(Has(frameSize) ? frameSize.Value : Has(defaultValue) ? defaultValue.Value : DefaultFrameSize);
        
        public static int CoalesceFrameSize(int? frameSize, int? bits, int? channels)
            => AssertFrameSize(Has(frameSize) ? frameSize.Value : Has(bits) && Has(channels) ? FrameSize(bits, channels) : DefaultFrameSize);

        public static int CoalesceHeaderLength(int? headerLength, int? defaultValue)
            => AssertHeaderLength(Has(headerLength) ? headerLength.Value : Has(defaultValue) ? defaultValue.Value : DefaultHeaderLength);
        
        public static string CoalesceFileExtension(string fileExtension, string defaultValue = default)
            => AssertFileExtension(Has(fileExtension) ? fileExtension : Has(defaultValue) ? defaultValue : DefaultFileExtension);
        
        public static int CoalesceCourtesyBytes(int? courtesyBytes, int? defaultValue = default)
            => AssertCourtesyBytes(Has(courtesyBytes) ? courtesyBytes.Value : Has(defaultValue) ? defaultValue.Value : DefaultCourtesyBytes);
 
        // Durations
        
        public static double CoalesceAudioLength(double? audioLength, double? defaultValue)
            => AssertAudioLength(Has(audioLength) ? audioLength.Value : Has(defaultValue) ? defaultValue.Value : DefaultAudioLength);
 
        public static int CoalesceFrameCount(int? frameCount, int? defaultValue)
            => AssertFrameCount(Has(frameCount) ? frameCount.Value : Has(defaultValue) ? defaultValue.Value : DefaultFrameCount);
 
        public static int CoalesceByteCount(int? byteCount, int? defaultValue)
            => AssertByteCount(Has(byteCount) ? byteCount.Value : Has(defaultValue) ? defaultValue.Value : DefaultByteCount);
   }
}