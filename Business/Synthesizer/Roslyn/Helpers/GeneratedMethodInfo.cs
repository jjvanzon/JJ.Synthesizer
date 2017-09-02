using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class GeneratedMethodInfo
    {
        private const string TAB_STRING = "    ";
        private const int METHOD_BODY_INDENT_LEVEL = 3;

        public string MethodNameForCalculate { get; set; }
        public string MethodNameForReset { get; set; }
        public string MethodCodeForCalculate { get; set; }
        public string MethodCodeForReset { get; set; }
        public bool MethodGenerationIsComplete { get; set; }

        public StringBuilderWithIndentation MethodBodyStringBuilderForCalculate { get; } = new StringBuilderWithIndentation(TAB_STRING)
        {
            IndentLevel = METHOD_BODY_INDENT_LEVEL
        };

        public StringBuilderWithIndentation MethodBodyStringBuilderForReset { get; } = new StringBuilderWithIndentation(TAB_STRING)
        {
            IndentLevel = METHOD_BODY_INDENT_LEVEL
        };

        public VariableCollections VariableInfo { get; set; } = new VariableCollections();

        public IList<GeneratedParameterInfo> GetGeneratedParameterInfos()
        {
            // TODO: Repeated code from Execute method.
            IList<ExtendedVariableInfo> inputVariableInfos = VariableInfo.VariableName_To_InputVariableInfo_Dictionary.Values.ToArray();
            IList<ArrayCalculationInfo> arrayCalculationInfos = VariableInfo.ArrayDto_To_ArrayCalculationInfo_Dictionary.Values.ToArray();

            IList<string> longLivedDoubleVariableNamesCamelCase = VariableInfo.LongLivedDoubleVariableNamesCamelCase.ToArray();
            DoubleArrayVariableInfo[] longLivedDoubleArrayVariableInfos = VariableInfo.LongLivedDoubleArrayVariableInfos.ToArray();

            IList<GeneratedParameterInfo> list =
                longLivedDoubleVariableNamesCamelCase.Select(y => new GeneratedParameterInfo { NameCamelCase = y, TypeName = nameof(Double) })
                                                     .Union(
                                                         inputVariableInfos.Select(
                                                             y => new GeneratedParameterInfo { NameCamelCase = y.VariableNameCamelCase, TypeName = nameof(Double) }))
                                                     .Union(
                                                         arrayCalculationInfos.Select(
                                                             y => new GeneratedParameterInfo { NameCamelCase = y.NameCamelCase, TypeName = y.TypeName }))
                                                     .Union(
                                                         longLivedDoubleArrayVariableInfos.Select(
                                                             y => new GeneratedParameterInfo { NameCamelCase = y.NameCamelCase, TypeName = nameof(Double) + "[]" }))
                                                     .ToArray();

            return list;
        }
    }
}