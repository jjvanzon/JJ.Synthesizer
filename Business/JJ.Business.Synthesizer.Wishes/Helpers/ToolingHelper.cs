using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Environment;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkCommonWishes;
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal class ToolingHelper
    {
        private const string NCrunchEnvironmentVariableName = "NCrunch";
        private const string AzurePipelinesEnvironmentVariableValue = "True";
        private const string AzurePipelinesEnvironmentVariableName = "TF_BUILD";
        private const string NCrunchEnvironmentVariableValue = "1";
        
        private readonly ConfigResolver _configResolver;
        
        public ToolingHelper(ConfigResolver configResolver)
        {
            _configResolver = configResolver ?? throw new ArgumentNullException(nameof(configResolver));
        }
        
        public static bool IsRunningInTooling => IsUnderNCrunch || IsUnderAzurePipelines;
        
        public static bool IsUnderNCrunch
        {
            get
            {
                if (ConfigHelper.NCrunch.Impersonate)
                {
                    return true;
                }
                
                bool isUnderNCrunch = EnvironmentVariableIsDefined(NCrunchEnvironmentVariableName, NCrunchEnvironmentVariableValue);
                return isUnderNCrunch;
            }
        }
        
        public static bool IsUnderAzurePipelines
        {
            get
            {
                if (ConfigHelper.AzurePipelines.Impersonate)
                {
                    return true;
                }                
                bool isUnderAzurePipelines = EnvironmentVariableIsDefined(AzurePipelinesEnvironmentVariableName, AzurePipelinesEnvironmentVariableValue);
                return isUnderAzurePipelines;
            }
        }
        
        // ReSharper disable AssignNullToNotNullAttribute
        public static bool CurrentTestIsInCategory(string category)
        {
            var methodQuery = new StackTrace().GetFrames().Select(x => x.GetMethod());

            var attributeQuery 
                = methodQuery.SelectMany(method => method.GetCustomAttributes()
                                                         .Union(method.DeclaringType?.GetCustomAttributes()));
            var categoryQuery
                = attributeQuery.Where(attr => attr.GetType().Name == "TestCategoryAttribute")
                                .Select(attr => attr.GetType().GetProperty("TestCategories")?.GetValue(attr))
                                .OfType<IEnumerable<string>>()
                                .SelectMany(x => x);

            bool isInCategory = categoryQuery.Any(x => string.Equals(x, category, StringComparison.OrdinalIgnoreCase));

            return isInCategory;
        }
        
        // Warnings
        
        public IList<string> GetToolingWarnings(string fileExtension = null)
        {
            var list = new List<string>();
            
            // Running Under Tooling
            
            if (ConfigHelper.NCrunch.Impersonate)
            {
                list.Add("Pretending to be NCrunch.");
            }
            
            if (IsUnderNCrunch)
            {
                list.Add($"Environment variable {NCrunchEnvironmentVariableName} = {NCrunchEnvironmentVariableValue}");
            }
            
            if (ConfigHelper.AzurePipelines.Impersonate)
            {
                list.Add("Pretending to be Azure Pipelines.");
            }
            
            if (IsUnderAzurePipelines)
            {
                list.Add($"Environment variable {AzurePipelinesEnvironmentVariableName} = {AzurePipelinesEnvironmentVariableValue} (Azure Pipelines)");
            }

            // Long Running
            
            bool isLong = CurrentTestIsInCategory(_configResolver.GetLongTestCategory);
            if (isLong)
            {
                list.Add($"Test has category '{_configResolver.GetLongTestCategory}'");
            }
            
            // Audio Disabled
            
            if (!_configResolver.GetAudioPlayBack(fileExtension))
            {
                list.Add("Audio disabled");
            }
            
            return list;
        }
    }
}
