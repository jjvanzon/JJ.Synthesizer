using static JJ.Framework.Configuration.CustomConfigurationManager;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public static class ConfigurationHelper
    {
        private static readonly ConfigurationSection _section = GetSection<ConfigurationSection>();

        public static double DefaultOutputVolume { get; } = _section.DefaultOutputVolume;
        public static double DefaultOutputDuration { get; } = _section.DefaultOutputDuration;
        public static int DefaultSamplingRate { get; } = _section.DefaultSamplingRate;
        public static int AzurePipelinesSamplingRate { get; } = _section.AzurePipelinesSamplingRate;
        public static int NCrunchSamplingRate { get; } = _section.NCrunchSamplingRate;
    }
}