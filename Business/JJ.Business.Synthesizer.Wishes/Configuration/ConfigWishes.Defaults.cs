using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Configuration.TimeOutActionEnum;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    public partial class ConfigWishes
    {
                
        // Defaults

        // Audio Quality
        
        public const int                   DefaultBits          = 32;
        public const int                   DefaultChannels      = 1;
        public static readonly int?        DefaultChannel       = null;
        public const int                   DefaultSamplingRate  = 48000;
        public const AudioFileFormatEnum   DefaultAudioFormat   = Wav;
        public const InterpolationTypeEnum DefaultInterpolation = Line;

        // Audio Lengths
        
        /// <inheritdoc cref="docs._notelength" />
        public const double DefaultNoteLength      = 0.50;
        public const double DefaultBarLength       = 1.00;
        public const double DefaultBeatLength      = 0.25;
        public const double DefaultAudioLength     = 1.00;
        public const double DefaultLeadingSilence  = 0.25;
        public const double DefaultTrailingSilence = 0.25;
        
        // Feature Toggles
        
        public const bool   DefaultAudioPlayback      = true;
        public const bool   DefaultDiskCache          = false;
        public const bool   DefaultMathBoost          = true;
        public const bool   DefaultParallelProcessing = true;
        public const bool   DefaultPlayAllTapes       = false;

        // Tooling
        
        public const bool   DefaultToolingAudioPlayback                  = false;
        public const bool   DefaultToolingImpersonation                  = false;
        public const int    DefaultNCrunchSamplingRate                   = 150;
        public const int    DefaultNCrunchSamplingRateLongRunning        = 8;
        public const int    DefaultAzurePipelinesSamplingRate            = 1500;
        public const int    DefaultAzurePipelinesSamplingRateLongRunning = 100;
        
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public const double DefaultLeafCheckTimeOut         = 60;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public const TimeOutActionEnum DefaultTimeOutAction = Continue;
        public const int    DefaultCourtesyFrames           = 4;
        public const int    DefaultFileExtensionMaxLength   = 4;
        public const string DefaultLongTestCategory         = "Long";

        // Derive Defaults
        
        public static int    DefaultFrameSize     => DefaultBits / 8 * DefaultChannels;
        public static string DefaultFileExtension => DefaultAudioFormat.FileExtension();
    }
}
