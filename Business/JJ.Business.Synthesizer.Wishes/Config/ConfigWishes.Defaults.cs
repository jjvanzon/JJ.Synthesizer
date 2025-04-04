using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Logging.Core.Config;
using JJ.Framework.Logging.Core.Mappers;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection.Core;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Config.TimeOutActionEnum;
using static JJ.Framework.Logging.Core.Config.LoggerConfigFetcher;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    public partial class ConfigWishes
    {
        // Defaults

        // Primary Audio Properties
        
        public const int                   DefaultBits           = 32;
        public const int                   DefaultChannels       = 1;
        public const int                   DefaultSamplingRate   = 48000;
        public const AudioFileFormatEnum   DefaultAudioFormat    = Wav;
        public const InterpolationTypeEnum DefaultInterpolation  = Line;
        public const int                   DefaultCourtesyFrames = 4;

        // Durations
        
        /// <inheritdoc cref="_notelength" />
        public const double DefaultNoteLength      = 0.20;
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
        
        public const  bool   DefaultToolingAudioPlayback                  = false;
        public static bool?  DefaultToolingImpersonationMode              = null;
        public const  int    DefaultNCrunchSamplingRate                   = 10;
        public const  int    DefaultNCrunchSamplingRateLongRunning        = 5;
        public const  int    DefaultAzurePipelinesSamplingRate            = 1000;
        public const  int    DefaultAzurePipelinesSamplingRateLongRunning = 100;
        public const  string DefaultLongTestCategory                      = "Long";
        
        // Technical
        
        /// <inheritdoc cref="_leafchecktimeout" />
        public const double            DefaultLeafCheckTimeOut       = 60;
        /// <inheritdoc cref="_leafchecktimeout" />
        public const TimeOutActionEnum DefaultTimeOutAction          = Continue;
        public const int               DefaultFileExtensionMaxLength = 4;

        // Derived Defaults
        
        public static int    DefaultCourtesyBytes  { get; } = CourtesyBytes(DefaultCourtesyFrames, DefaultBits, DefaultChannels);
        public static string DefaultFileExtension  { get; } = FileExtension(DefaultAudioFormat);
        public static int    DefaultFrameSize      { get; } = FrameSize(DefaultBits, DefaultChannels);
        public static int    DefaultHeaderLength   { get; } = HeaderLength(DefaultAudioFormat);
        public static double DefaultMaxAmplitude   { get; } = MaxAmplitude(DefaultBits);
        public static int    DefaultSizeOfBitDepth { get; } = SizeOfBitDepth(DefaultBits);
        public static int    DefaultFrameCount     { get; } = FrameCount(DefaultAudioLength, DefaultSamplingRate);
        public static int    DefaultByteCount      { get; } = ByteCount(DefaultFrameCount, DefaultFrameSize, DefaultHeaderLength);
        
        internal static PersistenceConfiguration CreateDefaultInMemoryConfiguration()
            => new PersistenceConfiguration
            {
                ContextType = "Memory",
                ModelAssembly = ReflectionHelperCore.GetAssemblyName<Persistence.Synthesizer.Operator>(),
                MappingAssembly = ReflectionHelperCore.GetAssemblyName<Persistence.Synthesizer.Memory.Mappings.OperatorMapping>(),
                RepositoryAssemblies = new[]
                {
                    ReflectionHelperCore.GetAssemblyName<Persistence.Synthesizer.Memory.Repositories.NodeTypeRepository>(), ReflectionHelperCore.GetAssemblyName<Persistence.Synthesizer.DefaultRepositories.OperatorRepository>()
                }
            };
        
        private const string DefaultLogFormat = "{0:HH:mm:ss.fff} [{1}] {2}";
        
        internal static ConfigSection CreateDefaultConfigSection() => new()
        {
            Logging = CreateDefaultRootLoggerXml(),
            NCrunch = new ConfigToolingElement
            {
                Logging = new RootLoggerXml { Type = "Debug", Format = DefaultLogFormat }
            },
            AzurePipelines = new ConfigToolingElement
            {
                Logging = new RootLoggerXml { Type = "Console", Categories = "File;Memory", Format = DefaultLogFormat }
            }
        };
        
        internal static RootLoggerXml CreateDefaultRootLoggerXml()
        {
            #if DEBUG
            return new RootLoggerXml { Type = "Debug", Format = DefaultLogFormat };
            #else
            return new RootLoggerXml { Type = "Console", Format = DefaultLogFormat };
            #endif
        }
    }
}
