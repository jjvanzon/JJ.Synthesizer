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
        private static readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();

        public static int    DefaultSamplingRate => _section.DefaultSamplingRate ?? 48000;
        public static bool   PlayEnabled         => _section.PlayEnabled         ?? true;
        public static double PlayLeadingSilence  => _section.PlayLeadingSilence  ?? 0.2;
        public static double PlayTrailingSilence => _section.PlayTrailingSilence ?? 0.2;

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
