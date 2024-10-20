using System;
using JJ.Framework.Configuration;
// ReSharper disable MemberHidesStaticFromOuterClass

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <summary>
    /// This ConfigurationHelper internally handles null-tolerance for the data missing from the app.config file.
    /// It returns defaults if config items are missing, to make it easier to use SynthWishes.
    /// </summary>
    public static class ConfigHelper
    {
        private const int DEFAULT_SAMPLING_RATE = 48000;
        private const bool DEFAULT_PLAY_AUDIO_ENABLED = true;
        private const string DEFAULT_LONG_RUNNING_TEST_CATEGORY = "Long";
        private const int DEFAULT_TOOLING_SAMPLING_RATE = 150;
        private const int DEFAULT_SAMPLING_RATE_LONG_RUNNING = 30;
        private const bool DEFAULT_TOOLING_PLAY_AUDIO_ENABLED = false;

        private static readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        public static int DefaultSamplingRate 
            => _section.DefaultSamplingRate ?? DEFAULT_SAMPLING_RATE;

        public static bool PlayAudioEnabled 
            => _section.PlayAudioEnabled ?? DEFAULT_PLAY_AUDIO_ENABLED;

        public static string LongRunningTestCategory
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_section.LongRunningTestCategory))
                {
                    return _section.LongRunningTestCategory;
                }
                
                return DEFAULT_LONG_RUNNING_TEST_CATEGORY;
            }
        }

        public static ToolingConfigWithDefaults AzurePipelines { get; } = new ToolingConfigWithDefaults(_section.AzurePipelines);
        public static ToolingConfigWithDefaults NCrunch { get; } = new ToolingConfigWithDefaults(_section.NCrunch);

        public class ToolingConfigWithDefaults
        {
            private readonly ToolingConfiguration _baseConfig;
            
            internal ToolingConfigWithDefaults(ToolingConfiguration baseConfig) 
                => _baseConfig = baseConfig;
            
            public int SamplingRate => _baseConfig.SamplingRate ?? DEFAULT_TOOLING_SAMPLING_RATE;
            public int SamplingRateLongRunning => _baseConfig.SamplingRateLongRunning ?? DEFAULT_SAMPLING_RATE_LONG_RUNNING;
            public bool PlayAudioEnabled => _baseConfig.PlayAudioEnabled ?? DEFAULT_TOOLING_PLAY_AUDIO_ENABLED;
        }
        
        /// <summary> This null-tolerant version is missing in JJ.Framework.Configuration for now. </summary>
        public static T TryGetSection<T>()
            where T: class, new()
        {
            T config = null;

            try
            {
                config = CustomConfigurationManager.GetSection<T>();
            }
            catch (Exception ex)
            {
                // Allow 'Not Found' Exception
                string configSectionName = NameHelper.GetAssemblyName<T>().ToLower();
                string allowedMessage = $"Configuration section '{configSectionName}' not found.";
                bool messageIsAllowed = string.Equals(ex.Message, allowedMessage);
                bool messageIsAllowed2 = string.Equals(ex.InnerException?.Message, allowedMessage);
                bool mustThrow = !messageIsAllowed && !messageIsAllowed2;
                
                if (mustThrow)
                {
                    throw;
                }
            }

            return config;
        }

    }
}
