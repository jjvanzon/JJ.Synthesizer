using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.Environment;

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

        /// <summary>
        /// Optimizes the given <see cref="AudioFileOutput" /> for tooling environments such as NCrunch and Azure Pipelines.
        /// It can do this by lowering the audio sampling rate for instance.
        /// </summary>
        /// <param name="audioFileOutput"> The <see cref="AudioFileOutput" /> to be optimized. </param>
        public static void SetSamplingRateForTooling(AudioFileOutput audioFileOutput)
        {
            if (IsRunningInNCrunch)
            {
                audioFileOutput.SamplingRate = ConfigHelper.NCrunch.SamplingRate;

                if (CurrentTestIsInCategory(ConfigHelper.LongRunningTestCategory))
                {
                    WriteLine($"Test has category '{ConfigHelper.LongRunningTestCategory}'.");

                    audioFileOutput.SamplingRate = ConfigHelper.NCrunch.SamplingRateLongRunning;
                }

                WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate}.");
            }

            if (IsRunningInAzurePipelines)
            {
                audioFileOutput.SamplingRate = ConfigHelper.AzurePipelines.SamplingRate;

                if (CurrentTestIsInCategory(ConfigHelper.LongRunningTestCategory))
                {
                    WriteLine($"Test has category '{ConfigHelper.LongRunningTestCategory}'.");
                    
                    audioFileOutput.SamplingRate = ConfigHelper.AzurePipelines.SamplingRateLongRunning;
                }

                WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate}.");
            }
            
            WriteLine();
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
                    WriteLine();
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

        public static bool CurrentTestIsInCategory(string category) =>
            new StackTrace().GetFrames()?
                            .Select(stackFrame => stackFrame.GetMethod())
                            .SelectMany(method => method.GetCustomAttributes(false)
                                                        .Where(attr => attr.GetType().Name == "TestCategoryAttribute")
                                                        .Select(attr => attr.GetType().GetProperty("TestCategories")?.GetValue(attr) as IEnumerable<string>))
                            .Any(x => x.Contains(category)) ?? false;
    }
}
