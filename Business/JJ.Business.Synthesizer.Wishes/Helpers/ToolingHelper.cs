using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.Environment;
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class ToolingHelper
    {
        public static bool PlayAudioAllowed(string fileExtension)
        {
            if (!ConfigHelper.PlayAudioEnabled)
            {
                return false;
            }

            if (IsRunningInNCrunch && !ConfigHelper.NCrunch.PlayAudioEnabled)
            {
                return false;
            }

            if (IsRunningInAzurePipelines && !ConfigHelper.AzurePipelines.PlayAudioEnabled)
            {
                return false;
            }

            if (!string.Equals(fileExtension, ".wav", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static int? TryGetSamplingRateForNCrunch()
        {
            if (IsRunningInNCrunch)
            {
                if (CurrentTestIsInCategory(ConfigHelper.LongRunningTestCategory))
                {
                    return ConfigHelper.NCrunch.SamplingRateLongRunning;
                }
                else
                {
                    return ConfigHelper.NCrunch.SamplingRate;
                }
            }
            
            return null;
        }
        
        public static int? TryGetSamplingRateForAzurePipelines()
        {
            if (IsRunningInAzurePipelines)
            {
                if (CurrentTestIsInCategory(ConfigHelper.LongRunningTestCategory))
                {
                    return ConfigHelper.AzurePipelines.SamplingRateLongRunning;
                }
                else
                {
                    return ConfigHelper.AzurePipelines.SamplingRate;
                }
            }

            return null;
        }

        public static bool IsRunningInNCrunch
        {
            get
            {
                var lines = new List<string>();
                
                if (ConfigHelper.NCrunch.Pretend)
                {
                    lines.Add("Pretending to be NCrunch.");
                    SetEnvironmentVariable("NCrunch", "1");
                }

                string environmentVariable = GetEnvironmentVariable("NCrunch");
                bool isNCrunch = string.Equals(environmentVariable, "1");
                if (isNCrunch)
                { 
                    lines.Add($"Environment variable NCrunch = {environmentVariable}");
                }

                if (lines.Any())
                {
                    lines.ForEach(WriteLine);
                }

                return isNCrunch;
            }
        }

        public static bool IsRunningInAzurePipelines
        {
            get
            {
                if (ConfigHelper.AzurePipelines.Pretend)
                {
                    WriteLine("Pretending to be Azure Pipelines.");
                    SetEnvironmentVariable("TF_BUILD", "True");
                }

                string environmentVariable = GetEnvironmentVariable("TF_BUILD");
                bool isAzurePipelines = string.Equals(environmentVariable, "True");
                if (isAzurePipelines)
                { 
                    WriteLine($"Environment variable TF_BUILD = {environmentVariable} (Azure Pipelines)");
                }

                return isAzurePipelines;
            }
        }

        public static bool CurrentTestIsInCategory(string category)
        {
            bool isInCategory = new StackTrace().GetFrames()?
                                                .Select(stackFrame => stackFrame.GetMethod())
                                                .SelectMany(method => method.GetCustomAttributes(false)
                                                                            .Where(attr => attr.GetType().Name == "TestCategoryAttribute")
                                                                            .Select(attr => attr.GetType().GetProperty("TestCategories")?.GetValue(attr) as IEnumerable<string>))
                                                .Any(x => x.Contains(category)) ?? false;
            if (isInCategory)
            {
                WriteLine($"Test has category '{category}'.");
            }

            return isInCategory;
        }
    }
}
