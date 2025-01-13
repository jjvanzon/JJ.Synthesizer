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
        
        public static int CoalesceBits(int? bits, int? defaultBits = null)
            => Has(bits) ? bits.Value : Has(defaultBits) ? defaultBits.Value : DefaultBits;
        
        public static int CoalesceChannels(int? channels, int? defaultChannels = null)
            => Has(channels) ? channels.Value : Has(defaultChannels) ? defaultChannels.Value : DefaultChannels;
        
        public static AudioFileFormatEnum Coalesce(AudioFileFormatEnum? audioFormat, AudioFileFormatEnum? defaultAudioFormat = null)
            => Has(audioFormat) ? audioFormat.Value : Has(defaultAudioFormat) ? defaultAudioFormat.Value : DefaultAudioFormat;
        
        public static InterpolationTypeEnum Coalesce(InterpolationTypeEnum? interpolation, InterpolationTypeEnum? defaultInterpolation = null)
            => Has(interpolation) ? interpolation.Value : Has(defaultInterpolation) ? defaultInterpolation.Value : DefaultInterpolation;
        
        public static int CoalesceCourtesyFrames(int? courtesyFrames, int? defaultCourtesyFrames = null)
            => courtesyFrames ?? defaultCourtesyFrames ?? DefaultCourtesyFrames;
        
        public static string CoalesceFileExtension(string fileExtension, string defaultFileExtension = default)
            => Has(fileExtension) ? fileExtension : Has(defaultFileExtension) ? defaultFileExtension : DefaultFileExtension;
        
        public static int CoalesceFrameSize(int? frameSize, int? bits, int? channels)
            => Has(frameSize) ? frameSize.Value : Has(bits) && Has(channels) ? bits.Value / 8 * channels.Value : DefaultFrameSize;
    }
}