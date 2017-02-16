using System.Collections.Generic;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Roslyn.Calculation;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Framework.Common;
using JJ.Business.SynthesizerPrototype.Roslyn.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.Roslyn.Visitors;

namespace JJ.Business.SynthesizerPrototype.Roslyn.Generators
{
    /// <summary>
    /// This is the older version of the experiment. Newer experiments use the other visitor:
    /// OperatorDtoToPatchCalculatorCSharpGenerator.
    /// </summary>
    internal class OperatorDtoToOperatorCalculatorCSharpGenerator
    {
        private const string TAB_STRING = "    ";
        private const int RAW_CALCULATION_INDENT_LEVEL = 3;

        public string Execute(IOperatorDto dto, string generatedNameSpace, string generatedClassName)
        {
            // Build up Method Body
            var visitor = new OperatorDtoToCSharpVisitor();
            OperatorDtoToCSharpVisitorResult visitorResult = visitor.Execute(dto, RAW_CALCULATION_INDENT_LEVEL);

            // Build up Code File
            var sb = new StringBuilderWithIndentation(TAB_STRING);

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Runtime.CompilerServices;");
            sb.AppendLine("using " + typeof(IOperatorCalculator).Namespace + ";");
            sb.AppendLine("using " + typeof(SineCalculator).Namespace + ";");
            sb.AppendLine();
            sb.AppendLine($"namespace {generatedNameSpace}");
            sb.AppendLine("{");
            sb.Indent();
            {
                sb.AppendLine($"public class {generatedClassName} : IOperatorCalculator");
                sb.AppendLine("{");
                sb.Indent();
                {
                    sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sb.AppendLine("public double Calculate()");
                    sb.AppendLine("{");
                    sb.Indent();
                    {
                        // Variable Declarations
                        IEnumerable<string> variableNames = visitorResult.PositionVariableNamesCamelCase.Union(visitorResult.PhaseVariableNamesCamelCase)
                                                                                                        .Union(visitorResult.PreviousPositionVariableNamesCamelCase)
                                                                                                        .Union(visitorResult.VariableInputValueInfos.Select(x => x.NameCamelCase));
                        foreach (string variableName in variableNames)
                        {
                            sb.AppendLine($"double {variableName} = 0.0;");
                        }

                        // Method Body
                        sb.Append(visitorResult.RawCalculationCode);

                        // Return statement
                        sb.AppendLine();
                        sb.AppendLine($"return {visitorResult.ReturnValueLiteral};");

                        sb.Unindent();
                    }
                    sb.AppendLine("}");
                    sb.Unindent();
                }
                sb.AppendLine("}");
                sb.Unindent();
            }
            sb.AppendLine("}");

            string csharp = sb.ToString();
            return csharp;
        }
    }
}
