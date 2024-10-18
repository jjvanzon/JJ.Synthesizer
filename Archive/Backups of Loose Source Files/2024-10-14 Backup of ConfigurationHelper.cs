using static JJ.Framework.Configuration.CustomConfigurationManager;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public static class ConfigurationHelper
    {
        private static readonly ConfigSection _section = GetSection<ConfigSection>();

        public static double DefaultOutputVolume { get; } = _section.DefaultOutputVolume;
        public static double DefaultOutputDuration { get; } = _section.DefaultOutputDuration;
        public static int DefaultSamplingRate { get; } = _section.DefaultSamplingRate;
        public static int? SamplingRateAzurePipelines { get; } = _section.SamplingRateAzurePipelines;
        public static int? SamplingRateAzurePipelinesLong { get; } = _section.SamplingRateAzurePipelinesLong;
        public static int? SamplingRateNCrunch { get; } = _section.SamplingRateNCrunch;
        public static int? SamplingRateNCrunchLong { get; } = _section.SamplingRateNCrunchLong;
        public static string LongRunningTestCategory { get; } = _section.LongRunningTestCategory;
    }
}