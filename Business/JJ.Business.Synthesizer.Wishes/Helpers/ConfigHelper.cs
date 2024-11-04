using JJ.Business.Synthesizer.Enums;
using System.Xml.Serialization;
using static JJ.Business.Synthesizer.Wishes.docs;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <inheritdoc cref="_confighelper"/>
    public static class ConfigHelper
    {
        private static readonly ConfigSection _section = ConfigWishes.TryGetSection<ConfigSection>() ?? new ConfigSection();

        // Even the defaults have defaults, to not require a config file.
        public static int                   DefaultSamplingRate  => _section.DefaultSamplingRate  ?? 48000;
        public static SpeakerSetupEnum      DefaultSpeakerSetup  => _section.DefaultSpeakerSetup  ?? SpeakerSetupEnum.Mono;
        public static SampleDataTypeEnum    DefaultBitDepth      => (_section.DefaultBitDepth ?? 16).ToBitDepth();
        public static AudioFileFormatEnum   DefaultAudioFormat   => _section.DefaultAudioFormat   ?? AudioFileFormatEnum.Wav;
        public static InterpolationTypeEnum DefaultInterpolation => _section.DefaultInterpolation ?? InterpolationTypeEnum.Line;
        public static double                DefaultAudioLength   => _section.DefaultAudioLength   ?? 1;
        public static bool                  PlayEnabled          => _section.PlayEnabled          ?? true;
        public static double                PlayLeadingSilence   => _section.PlayLeadingSilence   ?? 0.2;
        public static double                PlayTrailingSilence  => _section.PlayTrailingSilence  ?? 0.2;
        public static bool                  ParallelEnabled      => _section.ParallelEnabled      ?? true;
        public static bool                  InMemoryProcessing   => _section.InMemoryProcessing   ?? true;

        public static string LongRunningTestCategory
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_section.LongRunningTestCategory))
                {
                    return _section.LongRunningTestCategory;
                }
                
                return "Long";
            }
        }

        public static ToolingConfigWithDefaults AzurePipelines { get; } = new ToolingConfigWithDefaults(_section.AzurePipelines);
        public static ToolingConfigWithDefaults NCrunch { get; } = new ToolingConfigWithDefaults(_section.NCrunch);

        public class ToolingConfigWithDefaults
        {
            private readonly ToolingConfiguration _baseConfig;
            
            internal ToolingConfigWithDefaults(ToolingConfiguration baseConfig) => _baseConfig = baseConfig;
            
            public int  SamplingRate            => _baseConfig.SamplingRate            ?? 150;
            public int  SamplingRateLongRunning => _baseConfig.SamplingRateLongRunning ?? 30;
            public bool PlayEnabled             => _baseConfig.PlayEnabled             ?? false;
            public bool Pretend                 => _baseConfig.Pretend                 ?? false;
        }
        

    }
}
