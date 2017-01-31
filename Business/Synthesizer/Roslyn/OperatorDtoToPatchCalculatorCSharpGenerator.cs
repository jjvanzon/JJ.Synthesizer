using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Roslyn
{
    internal class OperatorDtoToPatchCalculatorCSharpGenerator
    {
        private const string TAB_STRING = "    ";
        private const int RAW_CALCULATION_INDENT_LEVEL = 4;

        private readonly int _channelCount;
        private readonly int _channelIndex;
        private readonly CalculatorCache _calculatorCache;
        private readonly ICurveRepository _curveRepository;
        private readonly IOperatorRepository _operatorRepository;

        public OperatorDtoToPatchCalculatorCSharpGenerator(
            int channelCount, 
            int channelIndex, 
            CalculatorCache calculatorCache, 
            ICurveRepository curveRepository, 
            IOperatorRepository operatorRepository)
        {
            if (calculatorCache == null) throw new NullException(() => calculatorCache);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);

            _channelCount = channelCount;
            _channelIndex = channelIndex;
            _calculatorCache = calculatorCache;
            _curveRepository = curveRepository;
            _operatorRepository = operatorRepository;
        }

        public OperatorDtoToPatchCalculatorCSharpGeneratorResult Execute(OperatorDtoBase dto, string generatedNameSpace, string generatedClassName)
        {
            if (string.IsNullOrEmpty(generatedNameSpace)) throw new NullOrEmptyException(() => generatedNameSpace);
            if (string.IsNullOrEmpty(generatedClassName)) throw new NullOrEmptyException(() => generatedClassName);

            // Build up Method Body
            var visitor = new OperatorDtoToRawCSharpVisitor(RAW_CALCULATION_INDENT_LEVEL, _calculatorCache, _curveRepository, _operatorRepository);
            OperatorDtoToCSharpVisitorResult visitorResult = visitor.Execute(dto);

            // Build up Code File
            var sb = new StringBuilderWithIndentation(TAB_STRING);

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Runtime.CompilerServices;");
            sb.AppendLine("using " + typeof(Dictionary<,>).Namespace + ";");
            sb.AppendLine("using " + typeof(PatchCalculatorBase).Namespace + ";");
            sb.AppendLine("using " + typeof(SineCalculator).Namespace + ";");
            sb.AppendLine("using " + typeof(DimensionEnum).Namespace + ";");
            sb.AppendLine("using " + typeof(NameHelper).Namespace + ";");
            sb.AppendLine("using " + typeof(MathHelper).Namespace + ";");
            sb.AppendLine("using " + typeof(ArrayCalculatorBase).Namespace + ";");
            sb.AppendLine();
            sb.AppendLine($"namespace {generatedNameSpace}");
            sb.AppendLine("{");
            sb.Indent();
            {
                sb.AppendLine($"public class {generatedClassName} : PatchCalculatorBase");
                sb.AppendLine("{");
                sb.Indent();
                {
                    // Fields
                    sb.AppendLine("// Fields");
                    sb.AppendLine();
                    WriteFields(sb, visitorResult);
                    sb.AppendLine();

                    // Constructor
                    sb.AppendLine("// Constructor");
                    sb.AppendLine();
                    WriteConstructor(sb, visitorResult, generatedClassName);
                    sb.AppendLine();

                    // Calculate Method
                    sb.AppendLine("// Calculate");
                    sb.AppendLine();
                    WriteCalculateMethod(sb, visitorResult);
                    sb.AppendLine();

                    // Values
                    sb.AppendLine("// Values");
                    sb.AppendLine();
                    WriteSetValue_ByListIndex(sb, visitorResult);
                    sb.AppendLine();
                    WriteSetValue_ByDimensionEnum(sb, visitorResult);
                    sb.AppendLine();
                    WriteSetValue_ByName(sb, visitorResult);
                    sb.AppendLine();
                    WriteSetValue_ByDimensionEnumAndListIndex(sb, visitorResult);
                    sb.AppendLine();
                    WriteSetValue_ByNameAndListIndex(sb, visitorResult);
                    sb.AppendLine();

                    // Reset
                    sb.AppendLine("// Reset");
                    sb.AppendLine();
                    WriteResetMethod(sb, visitorResult);
                    sb.Unindent();
                }
                sb.AppendLine("}");
                sb.Unindent();
            }
            sb.AppendLine("}");

            string generatedCode = sb.ToString();

            var result = new OperatorDtoToPatchCalculatorCSharpGeneratorResult
            {
                GeneratedCode = generatedCode,
                CurveCalculatorVariableInfos = visitorResult.CalculatorVariableInfos
            };

            return result;
        }

        private void WriteFields(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
        {
            IList<string> instanceVariableNamesCamelCase = GetInstanceVariableNamesCamelCase(visitorResult);

            if (instanceVariableNamesCamelCase.Any())
            {
                foreach (string variableName in instanceVariableNamesCamelCase)
                {
                    sb.AppendLine($"private double _{variableName};");
                }
                sb.AppendLine();
            }

            if (visitorResult.CalculatorVariableInfos.Any())
            {
                foreach (CalculatorVariableInfo variableInfo in visitorResult.CalculatorVariableInfos)
                {
                    sb.AppendLine($"private readonly {variableInfo.TypeName} _{variableInfo.NameCamelCase};");
                }
                sb.AppendLine();
            }
        }

        private void WriteConstructor(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult, string generatedClassName)
        {
            sb.AppendLine($"public {generatedClassName}(");
            sb.Indent();
            {
                sb.AppendLine("int samplingRate,");
                sb.AppendLine("int channelCount,");
                sb.AppendLine("int channelIndex,");
                sb.AppendLine("Dictionary<int, double[]> arrays,");
                sb.AppendLine("Dictionary<int, double> arrayRates");
                sb.AppendLine(")");
                sb.Unindent();
            }
            sb.Indent();
            {
                sb.AppendLine(": base(samplingRate, channelCount, channelIndex)");
                sb.Unindent();
            }
            sb.AppendLine("{");
            sb.Indent();
            {
                if (visitorResult.InputVariableInfos.Any())
                {
                    foreach (ExtendedVariableInfo inputVariableInfo in visitorResult.InputVariableInfos)
                    {
                        sb.AppendLine($"_{inputVariableInfo.VariableNameCamelCase} = {CompilationHelper.FormatValue(inputVariableInfo.Value ?? 0.0)};");
                    }
                    sb.AppendLine();
                }

                if (visitorResult.CalculatorVariableInfos.Any())
                {
                    foreach (CalculatorVariableInfo variableInfo in visitorResult.CalculatorVariableInfos)
                    {
                        var id = variableInfo.EntityID;
                        string type = variableInfo.TypeName;
                        string nameCamelCase = variableInfo.NameCamelCase;

                        sb.AppendLine($"_{nameCamelCase} = new {type}(arrays[{id}], arrayRates[{id}]);");
                    }
                    sb.AppendLine();
                }

                sb.AppendLine("Reset(time: 0.0);");
                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        private void WriteCalculateMethod(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
        {
            IList<string> instanceVariableNamesCamelCase = GetInstanceVariableNamesCamelCase(visitorResult);

            sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            sb.AppendLine("public override void Calculate(float[] buffer, int frameCount, double startTime)");
            sb.AppendLine("{");
            sb.Indent();
            {
                // Copy Fields to Local
                sb.AppendLine("double frameDuration = _frameDuration;");
                sb.AppendLine();

                if (instanceVariableNamesCamelCase.Any())
                {
                    foreach (string variableName in instanceVariableNamesCamelCase)
                    {
                        sb.AppendLine($"double {variableName} = _{variableName};");
                    }
                    sb.AppendLine();
                }

                if (visitorResult.CalculatorVariableInfos.Any())
                {
                    foreach (CalculatorVariableInfo variableInfo in visitorResult.CalculatorVariableInfos)
                    {
                        sb.AppendLine($"var {variableInfo.NameCamelCase} = _{variableInfo.NameCamelCase};");
                    }
                    sb.AppendLine();
                }

                // Declare Locally Reused Variables
                foreach (string positionVariableName in visitorResult.LocalDimensionVariableNamesCamelCase)
                {
                    sb.AppendLine($"double {positionVariableName};"); 
                }
                sb.AppendLine();

                // Initialize ValueCount variable
                sb.AppendLine($"int valueCount = frameCount * {_channelCount};");

                // Initialize First Time Variable
                sb.AppendLine($"{visitorResult.FirstTimeVariableNameCamelCase} = startTime;");
                sb.AppendLine();

                // Loop
                sb.AppendLine($"for (int i = {_channelIndex}; i < valueCount; i += {_channelCount})"); // Writes values in an interleaved way to the buffer."
                sb.AppendLine("{");
                sb.Indent();
                {
                    // Raw Calculation
                    sb.Append(visitorResult.RawCalculationCode);

                    // Accumulate
                    sb.AppendLine("// Accumulate");
                    sb.AppendLine($"double value = {visitorResult.ReturnValueLiteral};");
                    sb.AppendLine();
                    sb.AppendLine("if (double.IsNaN(value))"); // winmm will trip over NaN.
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        sb.AppendLine("value = 0;");
                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                    sb.AppendLine();
                    sb.AppendLine("float floatValue = (float)value;"); // TODO: This seems unsafe. What happens if the cast to float is invalid?
                    sb.AppendLine();
                    sb.AppendLine("PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);");

                    sb.AppendLine();
                    sb.AppendLine($"{visitorResult.FirstTimeVariableNameCamelCase} += frameDuration;");

                    sb.Unindent();
                }
                sb.AppendLine("}");
                sb.AppendLine();

                // Copy Local to Fields
                foreach (string variableName in instanceVariableNamesCamelCase)
                {
                    sb.AppendLine($"_{variableName} = {variableName};");
                }

                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        private void WriteSetValue_ByListIndex(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
        {
            IList<ExtendedVariableInfo> inputVariableInfos = visitorResult.InputVariableInfos.OrderBy(x => x.ListIndex).ToArray();

            sb.AppendLine("public override void SetValue(int listIndex, double value)");
            sb.AppendLine("{");
            sb.Indent();
            {
                sb.AppendLine("base.SetValue(listIndex, value);");
                sb.AppendLine();

                if (inputVariableInfos.Any())
                {
                    sb.AppendLine("switch (listIndex)");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        int i = 0;
                        foreach (string inputVariableName in inputVariableInfos.Select(x => x.VariableNameCamelCase))
                        {
                            sb.AppendLine($"case {i}:");
                            sb.Indent();
                            {
                                sb.AppendLine($"_{inputVariableName} = value;");
                                sb.AppendLine("break;");
                                sb.AppendLine();
                                sb.Unindent();
                            }
                            i++;
                        }

                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                }
                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        private void WriteSetValue_ByDimensionEnum(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
        {
            IList<ExtendedVariableInfo> variableInfos = visitorResult.LongLivedDimensionVariableInfos
                                                                     .Union(visitorResult.InputVariableInfos)
                                                                     .Where(x => x.DimensionEnum != DimensionEnum.Undefined || IsAnonymousDimension(x))
                                                                     .ToArray();

            sb.AppendLine("public override void SetValue(DimensionEnum dimensionEnum, double value)");
            sb.AppendLine("{");
            sb.Indent();
            {
                sb.AppendLine("base.SetValue(dimensionEnum, value);");
                sb.AppendLine();

                WriteFieldAssignments_ByDimensionEnum(sb, variableInfos);

                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        private void WriteSetValue_ByName(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
        {
            IList<ExtendedVariableInfo> variableInfos = visitorResult.LongLivedDimensionVariableInfos
                                                                     .Union(visitorResult.InputVariableInfos)
                                                                     .Where(x => !string.IsNullOrEmpty(x.CanonicalName) || IsAnonymousDimension(x))
                                                                     .ToArray();

            sb.AppendLine("public override void SetValue(string name, double value)");
            sb.AppendLine("{");
            sb.Indent();
            {
                sb.AppendLine("base.SetValue(name, value);");
                sb.AppendLine();

                sb.AppendLine("string canonicalName = NameHelper.ToCanonical(name);");
                sb.AppendLine();

                WriteFieldAssignments_ByCanonicalName(sb, variableInfos);

                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        private void WriteSetValue_ByDimensionEnumAndListIndex(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
        {
            IList<ExtendedVariableInfo> longLivedDimensionVariableInfosToInclude =
                visitorResult.LongLivedDimensionVariableInfos
                             .Where(x => x.DimensionEnum != DimensionEnum.Undefined || IsAnonymousDimension(x))
                             .OrderBy(x => x.ListIndex)
                             .ToArray();

            IList<ExtendedVariableInfo> inputVariableInfosToInclude =
                visitorResult.InputVariableInfos
                             .Where(x => x.DimensionEnum != DimensionEnum.Undefined || IsAnonymousDimension(x))
                             .OrderBy(x => x.ListIndex)
                             .ToArray();

            sb.AppendLine("public override void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)");
            sb.AppendLine("{");
            sb.Indent();
            {
                sb.AppendLine("base.SetValue(dimensionEnum, listIndex, value);");
                sb.AppendLine();

                // Dimension Variables
                WriteFieldAssignments_ByDimensionEnum(sb, longLivedDimensionVariableInfosToInclude);
                sb.AppendLine();

                // Input Variables
                WriteFieldAssignments_ByDimensionEnumAndListIndex(sb, inputVariableInfosToInclude);

                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        private void WriteSetValue_ByNameAndListIndex(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
        {
            IList<ExtendedVariableInfo> longLivedDimensionVariableInfosToInclude =
                visitorResult.LongLivedDimensionVariableInfos
                             .Where(x => !string.IsNullOrEmpty(x.CanonicalName) || IsAnonymousDimension(x))
                             .OrderBy(x => x.ListIndex)
                             .ToArray();

            IList<ExtendedVariableInfo> inputVariableInfosToInclude =
                visitorResult.InputVariableInfos
                             .Where(x => !string.IsNullOrEmpty(x.CanonicalName) || IsAnonymousDimension(x))
                             .OrderBy(x => x.ListIndex)
                             .ToArray();

            sb.AppendLine("public override void SetValue(string name, int listIndex, double value)");
            sb.AppendLine("{");
            sb.Indent();
            {
                sb.AppendLine("base.SetValue(name, listIndex, value);");
                sb.AppendLine();
                sb.AppendLine("string canonicalName = NameHelper.ToCanonical(name);");
                sb.AppendLine();

                // Dimension Variables
                WriteFieldAssignments_ByCanonicalName(sb, longLivedDimensionVariableInfosToInclude);
                sb.AppendLine();

                // Input Variables
                WriteFieldAssignments_ByCanonicalNameAndListIndex(sb, inputVariableInfosToInclude);

                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        /// <summary> Assumes that the variable dimensionEnum is already declared in the generated code. </summary>
        private void WriteFieldAssignments_ByDimensionEnum(StringBuilderWithIndentation sb, IList<ExtendedVariableInfo> variableInfos)
        {
            var groups = variableInfos.GroupBy(x => x.DimensionEnum);

            if (!groups.Any())
            {
                return;
            }

            sb.AppendLine("switch (dimensionEnum)");
            sb.AppendLine("{");
            sb.Indent();
            {
                foreach (var group in groups)
                {
                    sb.AppendLine($"case {nameof(DimensionEnum)}.{@group.Key}:");
                    sb.Indent();
                    {
                        foreach (ExtendedVariableInfo variableInfo in @group)
                        {
                            sb.AppendLine($"_{variableInfo.VariableNameCamelCase} = value;");
                        }
                        sb.AppendLine("break;");
                        sb.AppendLine();
                        sb.Unindent();
                    }
                }

                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        /// <summary> 
        /// Assumes that the variable canonicalName is already declared in the generated code.
        /// </summary>
        private void WriteFieldAssignments_ByCanonicalName(StringBuilderWithIndentation sb, IList<ExtendedVariableInfo> variableInfos)
        {
            var groups = variableInfos.GroupBy(x => x.CanonicalName);
            if (!groups.Any())
            {
                return;
            }

            foreach (var group in groups)
            {
                sb.AppendLine($"if (String.Equals(canonicalName, \"{@group.Key}\", StringComparison.Ordinal))");
                sb.AppendLine("{");
                sb.Indent();
                {
                    foreach (ExtendedVariableInfo variableInfo in @group)
                    {
                        sb.AppendLine($"_{variableInfo.VariableNameCamelCase} = value;");
                    }
                    sb.Unindent();
                }
                sb.AppendLine("}");
                sb.AppendLine();
            }
        }

        /// <summary> 
        /// Assumes that the variable dimensionEnum is already declared in the generated code.
        /// Assumes variableInfos are already order by ListIndex.
        /// </summary>
        private void WriteFieldAssignments_ByDimensionEnumAndListIndex(StringBuilderWithIndentation sb, IList<ExtendedVariableInfo> variableInfos)
        {
            var groups = variableInfos.GroupBy(x => x.DimensionEnum);
            foreach (var group in groups)
            {
                int i = 0;
                foreach (ExtendedVariableInfo inputVariableInfo in group)
                {
                    sb.AppendLine($"if (dimensionEnum == {nameof(DimensionEnum)}.{group.Key} && listIndex == {i})");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        sb.AppendLine($"_{inputVariableInfo.VariableNameCamelCase} = value;");
                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                    sb.AppendLine();

                    i++;
                }
            }
        }

        /// <summary> 
        /// Assumes that the variable canonicalName is already declared in the generated code.
        /// Assumes variableInfos are already order by ListIndex.
        /// </summary>
        private void WriteFieldAssignments_ByCanonicalNameAndListIndex(StringBuilderWithIndentation sb, IList<ExtendedVariableInfo> variableInfos)
        {
            // Input Variables
            var groups = variableInfos.GroupBy(x => x.CanonicalName);
            foreach (var group in groups)
            {
                int i = 0;
                foreach (ExtendedVariableInfo inputVariableInfo in group)
                {
                    sb.AppendLine($"if (String.Equals(canonicalName, \"{group.Key}\") && listIndex == {i})");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        sb.AppendLine($"_{inputVariableInfo.VariableNameCamelCase} = value;");
                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                    sb.AppendLine();

                    i++;
                }
            }
        }

        private void WriteResetMethod(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
        {
            sb.AppendLine("public override void Reset(double time)");
            sb.AppendLine("{");
            sb.Indent();
            {
                foreach (string variableName in visitorResult.LongLivedPhaseVariableNamesCamelCase)
                {
                    sb.AppendLine($"_{variableName} = 0.0;");
                }

                foreach (string variableName in visitorResult.LongLivedPreviousPositionVariableNamesCamelCase)
                {
                    // DIRTY: Phase of a partial does not have to be related to the time-dimension!
                    sb.AppendLine($"_{variableName} = time;");
                }

                sb.Unindent();
            }
            sb.AppendLine("}");
        }

        private IList<string> GetInstanceVariableNamesCamelCase(OperatorDtoToCSharpVisitorResult visitorResult)
        {
            return visitorResult.LongLivedPhaseVariableNamesCamelCase.Union(visitorResult.LongLivedPreviousPositionVariableNamesCamelCase)
                                                                     .Union(visitorResult.LongLivedOriginVariableNamesCamelCase)
                                                                     .Union(visitorResult.LongLivedMiscVariableNamesCamelCase)
                                                                     .Union(visitorResult.LongLivedDimensionVariableInfos.Select(x => x.VariableNameCamelCase))
                                                                     .Union(visitorResult.InputVariableInfos.Select(x => x.VariableNameCamelCase))
                                                                     .ToArray();
        }

        private bool IsAnonymousDimension(ExtendedVariableInfo extendedVariableInfo)
        {
            bool isAnonymousDimension = extendedVariableInfo.DimensionEnum == DimensionEnum.Undefined && string.IsNullOrEmpty(extendedVariableInfo.CanonicalName);
            return isAnonymousDimension;
        }
    }
}
