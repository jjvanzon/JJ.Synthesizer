using System;
using JJ.Framework.Configuration;
using static JJ.Business.Synthesizer.Wishes.docs;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <inheritdoc cref="_confighelper"/>
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
        
        /// <inheritdoc cref="_trygetsection"/>
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
