using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithCSharpCompilation;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors.WithCSharpCompilation
{
    /// <summary>
    /// This is the older version of the experiment. Newer experiments use the other visitor:
    /// OperatorDtoToPatchCalculatorCSharpVisitor.
    /// </summary>
    internal class OperatorDtoToOperatorCalculatorCSharpVisitor : OperatorDtoToCSharpVisitorBase
    {
        private const int RAW_CALCULATION_INDENT_LEVEL = 3;

        public override string Execute(OperatorDtoBase dto, string generatedNameSpace, string generatedClassName)
        {
            // Optimize Calculation Dto
            var preProcessingVisitor = new OperatorDtoVisitor_PreProcessing();
            dto = preProcessingVisitor.Execute(dto);

            // Initialize Fields
            _stack = new Stack<ValueInfo>();
            _inputVariableInfoDictionary = new Dictionary<string, ValueInfo>();
            _positionVariableNamesCamelCaseHashSet = new HashSet<string>();
            _previousPositionVariableNamesCamelCaseHashSet = new HashSet<string>();
            _phaseVariableNamesCamelCaseHashSet = new HashSet<string>();
            _variableInput_OperatorDto_To_VariableName_Dictionary = new Dictionary<VariableInput_OperatorDto, string>();
            _camelCaseOperatorTypeName_To_VariableCounter_Dictionary = new Dictionary<string, int>();
            _inputVariableCounter = FIRST_VARIABLE_NUMBER;
            _phaseVariableCounter = FIRST_VARIABLE_NUMBER;
            _previousPositionVariableCounter = FIRST_VARIABLE_NUMBER;

            // Build up Method Body through Visitation
            _sb = new StringBuilderWithIndentation(TAB_STRING);
            _sb.IndentLevel = RAW_CALCULATION_INDENT_LEVEL;
            Visit_OperatorDto_Polymorphic(dto);
            string rawCalculationCode = _sb.ToString();
            // Pick up some other output from visitation.
            ValueInfo returnValueInfo = _stack.Pop();
            string returnValueLiteral = returnValueInfo.GetLiteral();

            // Build up Code File
            _sb = new StringBuilderWithIndentation(TAB_STRING);

            _sb.AppendLine("using System;");
            _sb.AppendLine("using System.Runtime.CompilerServices;");
            _sb.AppendLine("using " + typeof(IOperatorCalculator).Namespace + ";");
            _sb.AppendLine("using " + typeof(SineCalculator).Namespace + ";");
            _sb.AppendLine();
            _sb.AppendLine($"namespace {generatedNameSpace}");
            _sb.AppendLine("{");
            _sb.Indent();
            {
                _sb.AppendLine($"public class {generatedClassName} : IOperatorCalculator");
                _sb.AppendLine("{");
                _sb.Indent();
                {
                    _sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    _sb.AppendLine("public double Calculate()");
                    _sb.AppendLine("{");
                    _sb.Indent();
                    {
                        // Variable Declarations
                        IEnumerable<string> variableNames = _positionVariableNamesCamelCaseHashSet.Union(_phaseVariableNamesCamelCaseHashSet)
                                                                                                  .Union(_positionVariableNamesCamelCaseHashSet)
                                                                                                  .Union(_previousPositionVariableNamesCamelCaseHashSet)
                                                                                                  .Union(_inputVariableInfoDictionary.Keys);
                        foreach (string variableName in variableNames)
                        {
                            _sb.AppendLine($"double {variableName} = 0.0;");
                        }

                        // Method Body
                        _sb.Append(rawCalculationCode);

                        // Return statement
                        _sb.AppendLine();
                        _sb.AppendLine($"return {returnValueLiteral};");

                        _sb.Unindent();
                    }
                    _sb.AppendLine("}");
                    _sb.Unindent();
                }
                _sb.AppendLine("}");
                _sb.Unindent();
            }
            _sb.AppendLine("}");

            string csharp = _sb.ToString();
            return csharp;
        }

        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }
    }
}
