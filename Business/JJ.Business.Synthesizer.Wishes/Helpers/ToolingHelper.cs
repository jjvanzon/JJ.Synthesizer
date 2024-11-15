using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JJ.Business.CanonicalModel;
using static System.Console;
using static System.Environment;
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class ToolingHelper
    {
        public static Result<bool> PlayAllowed(string fileExtension)
        {
            var result = new Result<bool>
            {
                Successful = true, 
                ValidationMessages = new List<ValidationMessage>()
            };
            
            if (!ConfigHelper.AudioPlayBack)
            {
                result.Data = false;
                return result;
            }

            var isRunningInNCrunch = IsRunningInNCrunch;
            result.ValidationMessages.AddRange(isRunningInNCrunch.ValidationMessages);
            
            if (isRunningInNCrunch.Data && !ConfigHelper.NCrunch.AudioPlayBack)
            {
                result.ValidationMessages.Add("Audio disabled".ToCanonical());
                result.Data = false;
                return result;
            }

            var isRunningInAzurePipelines = IsRunningInAzurePipelines;
            result.ValidationMessages.AddRange(isRunningInNCrunch.ValidationMessages);
            
            if (isRunningInAzurePipelines.Data && !ConfigHelper.AzurePipelines.AudioPlayBack)
            {
                result.ValidationMessages.Add("Audio disabled".ToCanonical());
                result.Data = false;
                return result;
            }

            if (!string.Equals(fileExtension, ".wav", StringComparison.OrdinalIgnoreCase))
            {
                result.Data = false;
                return result;
            }

            result.Data = true;
            return result;
        }

        public static Result<int?> TryGetSamplingRateForAzurePipelines()
        {
            var isRunningInAzurePipelines = IsRunningInAzurePipelines;
            
            if (isRunningInAzurePipelines.Data)
            {
                var currentTestIsInCategory = CurrentTestIsInCategory(ConfigHelper.LongRunningTestCategory);

                var result = isRunningInAzurePipelines.Combine<int?>(currentTestIsInCategory);

                if (currentTestIsInCategory.Data)
                {
                    result.Data = ConfigHelper.AzurePipelines.SamplingRateLongRunning;
                }
                else
                {
                    result.Data = ConfigHelper.AzurePipelines.SamplingRate;
                }
                
                return result;
            }

            return new Result<int?>
            {
                Successful = true,
                Data = null,
                ValidationMessages = isRunningInAzurePipelines.ValidationMessages
            };
        }
        
        public static Result<int?> TryGetSamplingRateForNCrunch()
        {
            var isRunningInNCrunch = IsRunningInNCrunch;
            
            if (isRunningInNCrunch.Data)
            {
                var currentTestIsInCategory = CurrentTestIsInCategory(ConfigHelper.LongRunningTestCategory);

                var result = isRunningInNCrunch.Combine<int?>(currentTestIsInCategory);

                if (currentTestIsInCategory.Data)
                {
                    result.Data = ConfigHelper.NCrunch.SamplingRateLongRunning;
                }
                else
                {
                    result.Data = ConfigHelper.NCrunch.SamplingRate;
                }
                
                return result;
            }

            return new Result<int?>
            {
                Successful = true,
                Data = null,
                ValidationMessages = isRunningInNCrunch.ValidationMessages
            };
        }

        public static Result<bool> IsRunningInTooling => IsRunningInNCrunch.Combine(IsRunningInAzurePipelines);

        public static Result<bool> IsRunningInNCrunch
        {
            get
            {
                var lines = new List<string>();
                
                if (ConfigHelper.NCrunch.Impersonate)
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

                return lines.ToResult<bool>(isNCrunch);
            }
        }

        public static Result<bool> IsRunningInAzurePipelines
        {
            get
            {
                var lines = new List<string>();

                if (ConfigHelper.AzurePipelines.Impersonate)
                {
                    lines.Add("Pretending to be Azure Pipelines.");
                    SetEnvironmentVariable("TF_BUILD", "True");
                }

                string environmentVariable = GetEnvironmentVariable("TF_BUILD");
                bool isAzurePipelines = string.Equals(environmentVariable, "True");
                if (isAzurePipelines)
                {
                    lines.Add($"Environment variable TF_BUILD = {environmentVariable} (Azure Pipelines)");
                }

                if (lines.Any()) lines.ForEach(WriteLine);

                return lines.ToResult<bool>(isAzurePipelines);
            }
        }

        // ReSharper disable AssignNullToNotNullAttribute
        public static Result<bool> CurrentTestIsInCategory(string category)
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

            var result = new Result<bool>
            {
                Successful = true,
                Data = isInCategory,
                ValidationMessages = new List<ValidationMessage>()
            };

            if (isInCategory)
            {
                result.ValidationMessages.Add($"Test has category '{category}'".ToCanonical());
            }
            
            return result;
        }
    }
}
