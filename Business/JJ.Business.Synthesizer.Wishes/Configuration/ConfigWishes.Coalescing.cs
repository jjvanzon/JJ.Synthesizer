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
        
        public static int CoalesceBits(int? bits, int? defaultValue = null)
            => Has(bits) ? bits.Value : Has(defaultValue) ? defaultValue.Value : DefaultBits;
        
        public static int CoalesceChannels(int? channels, int? defaultValue = null)
            => Has(channels) ? channels.Value : Has(defaultValue) ? defaultValue.Value : DefaultChannels;
        
        public static AudioFileFormatEnum Coalesce(AudioFileFormatEnum? audioFormat, AudioFileFormatEnum? defaultValue = null)
            => Has(audioFormat) ? audioFormat.Value : Has(defaultValue) ? defaultValue.Value : DefaultAudioFormat;
        
        public static InterpolationTypeEnum Coalesce(InterpolationTypeEnum? interpolation, InterpolationTypeEnum? defaultValue = null)
            => Has(interpolation) ? interpolation.Value : Has(defaultValue) ? defaultValue.Value : DefaultInterpolation;
        
        public static int CoalesceCourtesyFrames(int? courtesyFrames, int? defaultValue = null)
            => courtesyFrames ?? defaultValue ?? DefaultCourtesyFrames;
        
        public static string CoalesceFileExtension(string fileExtension, string defaultValue = default)
            => Has(fileExtension) ? fileExtension : Has(defaultValue) ? defaultValue : DefaultFileExtension;
        
        public static int CoalesceFrameSize(int? frameSize, int? bits, int? channels)
            => Has(frameSize) ? frameSize.Value : Has(bits) && Has(channels) ? bits.Value / 8 * channels.Value : DefaultFrameSize;
        
        public static int CoalesceSizeOfBitDepth(int? sizeOfBitDepth, int? defaultValue = null)
            => Has(sizeOfBitDepth) ? sizeOfBitDepth.Value : Has(defaultValue) ? defaultValue.Value : DefaultSizeOfBitDepth;
        
        public static int CoalesceSamplingRate(int? samplingRate, int? defaultValue = null) 
            => Has(samplingRate) ? samplingRate.Value : Has(defaultValue) ? defaultValue.Value : DefaultSamplingRate;
    }
}