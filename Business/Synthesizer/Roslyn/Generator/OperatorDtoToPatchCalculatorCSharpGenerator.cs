using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Business.Synthesizer.Roslyn.Visitors;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Roslyn.Generator
{
    internal class OperatorDtoToPatchCalculatorCSharpGenerator
    {
        private const int RAW_CALCULATION_INDENT_LEVEL = 4;
        private const string TAB_STRING = "    ";

        public string Execute(OperatorDtoBase dto, string generatedNameSpace, string generatedClassName)
        {
            // Build up Method Body
            var visitor = new OperatorDtoToCSharpVisitor();
            OperatorDtoToCSharpVisitorResult visitorResult = visitor.Execute(dto, RAW_CALCULATION_INDENT_LEVEL);

            IList<string> instanceVariableNamesCamelCase = 
                visitorResult.PhaseVariableNamesCamelCase.Union(visitorResult.PreviousPositionVariableNamesCamelCase)
                                                         .Union(visitorResult.OriginVariableNamesCamelCase)
                                                         .Union(visitorResult.VariableInputValueInfos.Select(x => x.NameCamelCase))
                                                         .ToArray();
            // Build up Code File
            var sb = new StringBuilderWithIndentation(TAB_STRING);

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Runtime.CompilerServices;");
            sb.AppendLine("using " + typeof(PatchCalculatorBase).Namespace + ";");
            sb.AppendLine("using " + typeof(SineCalculator).Namespace + ";");
            sb.AppendLine("using " + typeof(DimensionEnum).Namespace + ";");
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

                    foreach (string variableName in instanceVariableNamesCamelCase)
                    {
                        sb.AppendLine($"private double _{variableName};");
                    }
                    sb.AppendLine();

                    // Constructor
                    sb.AppendLine("// Constructor");
                    sb.AppendLine();
                    sb.AppendLine($"public {generatedClassName}(int samplingRate, int channelCount, int channelIndex)");
                    sb.Indent();
                    {
                        sb.AppendLine($": base(samplingRate, channelCount, channelIndex)");
                        sb.Unindent();
                    }
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        if (visitorResult.VariableInputValueInfos.Any())
                        {
                            foreach (InputVariableInfo variableInputValueInfo in visitorResult.VariableInputValueInfos)
                            {
                                sb.AppendLine($"_{variableInputValueInfo.NameCamelCase} = {CompilationHelper.FormatValue(variableInputValueInfo.Value ?? 0.0)};");
                            }
                            sb.AppendLine("");
                        }

                        sb.AppendLine("Reset(time: 0.0);");
                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                    sb.AppendLine();

                    // Calculate Method
                    sb.AppendLine("// Calculate");
                    sb.AppendLine();
                    sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sb.AppendLine("public override void Calculate(float[] buffer, int frameCount, double startTime)");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        // Copy Fields to Local
                        sb.AppendLine("double frameDuration = _frameDuration;");
                        sb.AppendLine("int channelCount = _channelCount;");
                        sb.AppendLine("int channelIndex = _channelIndex;");
                        sb.AppendLine("int valueCount = frameCount * channelCount;");

                        sb.AppendLine();
                        foreach (string variableName in instanceVariableNamesCamelCase)
                        {
                            sb.AppendLine($"double {variableName} = _{variableName};");
                        }
                        sb.AppendLine();

                        // Position Variables
                        // HACK
                        string time0VariableName = "time_a0";
                        if (time0VariableName != null)
                        {
                            sb.AppendLine($"double {time0VariableName} = startTime;");
                        }

                        foreach (string positionVariableName in visitorResult.PositionVariableNamesCamelCase.Except(time0VariableName))
                        {
                            // HACK: The = 0 is a hack. Later, only position 0 of the dimensions will be assigned here.
                            sb.AppendLine($"double {positionVariableName} = 0;");
                        }
                        sb.AppendLine();

                        // Loop
                        sb.AppendLine("for (int i = channelIndex; i < valueCount; i += channelCount)"); // Writes values in an interleaved way to the buffer."
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

                            if (time0VariableName != null)
                            {
                                sb.AppendLine();
                                sb.AppendLine($"{time0VariableName} += frameDuration;");
                            }

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
                    sb.AppendLine();

                    // Values
                    sb.AppendLine("// Values");
                    sb.AppendLine();
                    sb.AppendLine("public override double GetValue(int listIndex)");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        sb.AppendLine("throw new NotImplementedException();");
                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                    sb.AppendLine();
                    sb.AppendLine("public override void SetValue(int listIndex, double value)");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        sb.AppendLine("base.SetValue(listIndex, value);");
                        sb.AppendLine();

                        if (visitorResult.VariableInputValueInfos.Any())
                        {
                            sb.AppendLine("switch (listIndex)");
                            sb.AppendLine("{");
                            sb.Indent();
                            {
                                int i = 0;
                                foreach (string inputVariableName in visitorResult.VariableInputValueInfos.Select(x => x.NameCamelCase))
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
                    sb.AppendLine();
                    sb.AppendLine("public override void SetValue(DimensionEnum dimensionEnum, double value)");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        sb.AppendLine("base.SetValue(dimensionEnum, value);");
                        sb.AppendLine();

                        var groups = visitorResult.VariableInputValueInfos.GroupBy(x => x.DimensionEnum);
                        if (groups.Any())
                        {
                            sb.AppendLine("switch (dimensionEnum)");
                            sb.AppendLine("{");
                            sb.Indent();
                            {
                                foreach (var group in groups)
                                {
                                    sb.AppendLine($"case {nameof(DimensionEnum)}.{group.Key}:");
                                    sb.Indent();
                                    {
                                        foreach (InputVariableInfo valueInfo in group)
                                        {
                                            sb.AppendLine($"_{valueInfo.NameCamelCase} = value;");
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
                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                    sb.AppendLine();

                    // Reset Method
                    sb.AppendLine("// Reset");
                    sb.AppendLine();
                    sb.AppendLine("public override void Reset(double time)");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        foreach (string variableName in visitorResult.PhaseVariableNamesCamelCase)
                        {
                            sb.AppendLine($"_{variableName} = 0.0;");
                        }

                        foreach (string variableName in visitorResult.PreviousPositionVariableNamesCamelCase)
                        {
                            // DIRTY: Phase of a partial does not have to be related to the time-dimension!
                            sb.AppendLine($"_{variableName} = time;");
                        }

                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                    sb.Unindent();
                }
                sb.AppendLine("}");
                sb.Unindent();
            }
            sb.AppendLine("}");

            string generatedCode = sb.ToString();
            return generatedCode;
        }
    }
}
