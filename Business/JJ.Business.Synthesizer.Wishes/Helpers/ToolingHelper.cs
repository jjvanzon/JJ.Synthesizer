using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using static System.Environment;
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal class ToolingHelper
    {
        private readonly ConfigResolver _configResolver;
        
        public ToolingHelper(ConfigResolver configResolver)
        {
            _configResolver = configResolver ?? throw new ArgumentNullException(nameof(configResolver));
        }
        
        public bool PlayAllowed(string fileExtension)
        {
            if (!_configResolver.GetAudioPlayBack)
            {
                return false;
            }
            
            bool underNCrunch = IsRunningInNCrunch;
            if (underNCrunch && !ConfigHelper.NCrunch.AudioPlayBack)
            {
                return false;
            }
            
            var underAzurePipelines = IsRunningInAzurePipelines;
            if (underAzurePipelines && !ConfigHelper.AzurePipelines.AudioPlayBack)
            {
                return false;
            }
            
            if (!string.Equals(fileExtension, ".wav", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            
            return true;
        }

        public int? TryGetSamplingRateForAzurePipelines()
        {
            bool underAzurePipelines = IsRunningInAzurePipelines;
            
            if (underAzurePipelines)
            {
                bool testIsLong = CurrentTestIsInCategory(_configResolver.GetLongTestCategory);
                
                if (testIsLong)
                {
                    return ConfigHelper.AzurePipelines.SamplingRateLongRunning;
                }
                else
                {
                    return ConfigHelper.AzurePipelines.SamplingRate;
                }
            }
            
            return default;
        }
        
        public int? TryGetSamplingRateForNCrunch()
        {
            bool underNCrunch = IsRunningInNCrunch;
            
            if (underNCrunch)
            {
                bool testIsLong = CurrentTestIsInCategory(_configResolver.GetLongTestCategory);
                
                if (testIsLong)
                {
                    return ConfigHelper.NCrunch.SamplingRateLongRunning;
                }
                else
                {
                    return ConfigHelper.NCrunch.SamplingRate;
                }
                
            }
            
            return default;
        }
        
        public static bool IsRunningInTooling => IsRunningInNCrunch || IsRunningInAzurePipelines;
        
        public static bool IsRunningInNCrunch
        {
            get
            {
                if (ConfigHelper.NCrunch.Impersonate)
                {
                    //SetEnvironmentVariable("NCrunch", "1");
                    return true;
                }
                
                string environmentVariable = GetEnvironmentVariable("NCrunch");
                bool underNCrunch = string.Equals(environmentVariable, "1");
                return underNCrunch;
            }
        }
        
        public static bool IsRunningInAzurePipelines
        {
            get
            {
                if (ConfigHelper.AzurePipelines.Impersonate)
                {
                    //SetEnvironmentVariable("TF_BUILD", "True");
                    return true;
                }                
                string environmentVariable = GetEnvironmentVariable("TF_BUILD");
                bool underAzurePipelines = string.Equals(environmentVariable, "True");
                
                return underAzurePipelines;
            }
        }
        
        // ReSharper disable AssignNullToNotNullAttribute
        public static bool CurrentTestIsInCategory(string category)
        {
            var methodQuery = new StackTrace().GetFrames().Select(x => x.GetMethod());

            var attributeQuery =
                methodQuery.SelectMany(method => method.GetCustomAttributes()
                                                       .Union(method.DeclaringType?.GetCustomAttributes()));
            var categoryQuery =
                attributeQuery.Where(attr => attr.GetType().Name == "TestCategoryAttribute")
                              .Select(attr => attr.GetType().GetProperty("TestCategories")?.GetValue(attr))
                              .OfType<IEnumerable<string>>()
                              .SelectMany(x => x);


            bool isInCategory = categoryQuery.Any(x => string.Equals(x, category, StringComparison.OrdinalIgnoreCase));

            return isInCategory;
        }

        // Warnings
        
        public static IList<string> GetRunningInNCrunchWarnings()
        {
            var lines = new List<string>();
            
            if (ConfigHelper.NCrunch.Impersonate)
            {
                lines.Add("Pretending to be NCrunch.");
            }
            
            if (IsRunningInNCrunch)
            {
                lines.Add($"Environment variable NCrunch = 1");
            }
            
            return lines;
        }
        
        public static IList<string> GetRunningInAzurePipelinesWarnings()
        {
            var lines = new List<string>();
            
            if (ConfigHelper.AzurePipelines.Impersonate)
            {
                lines.Add("Pretending to be Azure Pipelines.");
            }
            
            if (IsRunningInAzurePipelines)
            {
                lines.Add($"Environment variable TF_BUILD = True (Azure Pipelines)");
            }
            
            return lines;
        }

        public IList<string> GetTestIsLongWarnings()
        {
            bool isLong = CurrentTestIsInCategory(_configResolver.GetLongTestCategory);
            if (isLong)
            {
                return new List<string> { $"Test has category '{_configResolver.GetLongTestCategory}'" };
            }
            return new List<string>();
        }
        
        public IList<string> GetPlayAllowedWarnings(string fileExtension)
        {
            var list = new List<string>();
            
            if (!_configResolver.GetAudioPlayBack)
            {
                list.Add("Audio disabled (in config file)");
                return list;
            }
            
            else if (IsRunningInNCrunch && !ConfigHelper.NCrunch.AudioPlayBack)
            {
                list.Add("Audio disabled (in NCrunch)");
                return list;
            }
            
            else if (IsRunningInAzurePipelines && !ConfigHelper.AzurePipelines.AudioPlayBack)
            {
                list.Add("Audio disabled (in Azure Pipelines)");
                return list;
            }
            
            else if (!string.Equals(fileExtension, ".wav", StringComparison.OrdinalIgnoreCase))
            {
                list.Add("Audio disabled (file type not WAV).");
                return list;
            }
            
            return list;
        }
    }
}
