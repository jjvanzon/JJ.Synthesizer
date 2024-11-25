using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class ToolingHelper
    {
        public const string NCrunchEnvironmentVariableName = "NCrunch";
        public const string AzurePipelinesEnvironmentVariableValue = "True";
        public const string AzurePipelinesEnvironmentVariableName = "TF_BUILD";
        public const string NCrunchEnvironmentVariableValue = "1";

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
    }
}
