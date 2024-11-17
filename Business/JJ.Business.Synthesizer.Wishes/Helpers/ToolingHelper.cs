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
    internal class ToolingHelper
    {
        private readonly ConfigResolver _configResolver;
        
        public ToolingHelper(ConfigResolver configResolver)
        {
            _configResolver = configResolver ?? throw new ArgumentNullException(nameof(configResolver));
        }
        
        public Result<bool> PlayAllowed(string fileExtension)
        {
            var result = new Result<bool>
            {
                Successful = true, 
                ValidationMessages = new List<ValidationMessage>()
            };
            
            if (!_configResolver.GetAudioPlayBack)
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

        public Result<int?> TryGetSamplingRateForAzurePipelines()
        {
            var result = new Result<int?> { Successful = true, ValidationMessages = new List<ValidationMessage>() };

            var isRunningInAzurePipelines = IsRunningInAzurePipelines;
            
            if (isRunningInAzurePipelines.Data)
            {
                bool currentTestIsInCategory = CurrentTestIsInCategory(_configResolver.GetLongTestCategory);
                
                result = result.Combine(isRunningInAzurePipelines);

                if (currentTestIsInCategory)
                {
                    result.Data = ConfigHelper.AzurePipelines.SamplingRateLongRunning;
                    result.ValidationMessages.AddRange(GetTestIsLongWarnings().ToCanonical());
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
        
        public Result<int?> TryGetSamplingRateForNCrunch()
        {
            var result = new Result<int?> { Successful = true, ValidationMessages = new List<ValidationMessage>() };

            var underNCrunch = IsRunningInNCrunch;
            
            if (underNCrunch.Data)
            {
                var testIsLong = CurrentTestIsInCategory(_configResolver.GetLongTestCategory);

                result.Combine(underNCrunch);

                if (testIsLong)
                {
                    result.Data = ConfigHelper.NCrunch.SamplingRateLongRunning;
                    result.ValidationMessages.AddRange(GetTestIsLongWarnings().ToCanonical());
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
                ValidationMessages = underNCrunch.ValidationMessages
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
        
        public IList<string> GetTestIsLongWarnings()
        {
            string category = _configResolver.GetLongTestCategory;
            bool isInCategory = CurrentTestIsInCategory(category);
            if (isInCategory)
            {
                return new List<string> { $"Test has category '{category}'" };
            }
            return new List<string>();
        }
    }
}
