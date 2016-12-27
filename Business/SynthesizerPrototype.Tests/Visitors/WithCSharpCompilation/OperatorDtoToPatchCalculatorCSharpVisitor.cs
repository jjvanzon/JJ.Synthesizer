using System.Collections.Generic;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Tests.Calculation;
using JJ.Business.SynthesizerPrototype.Tests.Calculation.WithCSharpCompilation;
using JJ.Business.SynthesizerPrototype.Tests.Dto;
using JJ.Framework.Common;

namespace JJ.Business.SynthesizerPrototype.Tests.Visitors.WithCSharpCompilation
{
    internal class OperatorDtoToPatchCalculatorCSharpVisitor : OperatorDtoToCSharpVisitorBase
    {
        private const int RAW_CALCULATION_INDENT_LEVEL = 4;

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
            IList<string> instanceVariableNames = GetInstanceVariableNamesCamelCase();

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
                _sb.AppendLine($"public class {generatedClassName} : IPatchCalculator");
                _sb.AppendLine("{");
                _sb.Indent();
                {
                    // Fields
                    //_sb.AppendLine("private double[] _values;");
                    _sb.AppendLine("private int _framesPerChunk;");
                    _sb.AppendLine();

                    foreach (string instanceVariableName in instanceVariableNames)
                    {
                        _sb.AppendLine($"private double _{instanceVariableName};");
                    }

                    // Constructor
                    _sb.AppendLine();
                    _sb.AppendLine($"public {generatedClassName}(int framesPerChunk)");
                    _sb.AppendLine("{");
                    _sb.Indent();
                    {
                        _sb.AppendLine("_framesPerChunk = framesPerChunk;");
                        //_sb.AppendLine("_values = new double[_framesPerChunk];");
                        _sb.AppendLine("");

                        foreach (ValueInfo variableInputValueInfo in _inputVariableInfoDictionary.Values)
                        {
                            _sb.AppendLine($"_{variableInputValueInfo.NameCamelCase} = {variableInputValueInfo.FormatValue()};");
                        }
                        _sb.AppendLine("");

                        _sb.AppendLine("Reset();");
                        _sb.Unindent();
                    }
                    _sb.AppendLine("}");
                    _sb.AppendLine();

                    // Reset Method
                    _sb.AppendLine("public void Reset()");
                    _sb.AppendLine("{");
                    _sb.Indent();
                    {
                        foreach (string variableName in _phaseVariableNamesCamelCaseHashSet.Union(_previousPositionVariableNamesCamelCaseHashSet))
                        {
                            _sb.AppendLine($"_{variableName} = 0.0;");
                        }

                        _sb.Unindent();
                    }
                    _sb.AppendLine("}");
                    _sb.AppendLine();

                    // SetInput Method
                    _sb.AppendLine("public void SetInput(int listIndex, double input)");
                    _sb.AppendLine("{");
                    _sb.Indent();
                    {
                        _sb.AppendLine("switch (listIndex)");
                        _sb.AppendLine("{");
                        _sb.Indent();
                        {
                            int i = 0;
                            foreach (string inputVariableName in _inputVariableInfoDictionary.Keys)
                            {
                                _sb.AppendLine($"case {i}:");
                                _sb.Indent();
                                {
                                    _sb.AppendLine($"_{inputVariableName} = input;");
                                    _sb.AppendLine("break;");
                                    _sb.Unindent();
                                }
                                i++;
                            }

                            _sb.Unindent();
                        }
                        _sb.AppendLine("}");
                        _sb.Unindent();
                    }
                    _sb.AppendLine("}");
                    _sb.AppendLine();

                    // Calculate Method
                    _sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    _sb.AppendLine("public double[] Calculate(double startTime, double frameDuration)");
                    _sb.AppendLine("{");
                    _sb.Indent();
                    {
                        // Copy Fields to Local

                        // NOTE:
                        // Array access is excluded from the test, 
                        // because otherwise it would not be a fair performance comparison.

                        //_sb.AppendLine("double[] values = _values;");
                        _sb.AppendLine("int framesPerChunk = _framesPerChunk;");
                        _sb.AppendLine();
                        foreach (string instanceVariableName in instanceVariableNames)
                        {
                            _sb.AppendLine($"double {instanceVariableName} = _{instanceVariableName};");
                        }
                        _sb.AppendLine();

                        // Position Variables
                        string firstPositionVariableName = _positionVariableNamesCamelCaseHashSet.FirstOrDefault();
                        if (firstPositionVariableName != null)
                        {
                            _sb.AppendLine($"double {firstPositionVariableName} = startTime;");
                        }

                        foreach (string positionVariableName in _positionVariableNamesCamelCaseHashSet.Skip(1))
                        {
                            _sb.AppendLine($"double {positionVariableName};");
                        }
                        _sb.AppendLine();

                        // Loop
                        _sb.AppendLine("for (int i = 0; i < framesPerChunk; i++)");
                        _sb.AppendLine("{");
                        _sb.Indent();
                        {
                            // Raw Calculation
                            _sb.Append(rawCalculationCode);
                            _sb.AppendLine();

                            // Accumulate
                            _sb.AppendLine($"double value = {returnValueLiteral};");
                            _sb.AppendLine();
                            //_sb.AppendLine("values[i] = value;");

                            if (firstPositionVariableName != null)
                            {
                                _sb.AppendLine();
                                _sb.AppendLine($"{firstPositionVariableName} += frameDuration;");
                            }

                            _sb.Unindent();
                        }
                        _sb.AppendLine("}");
                        _sb.AppendLine();

                        // Copy Local to Fields
                        foreach (string variableName in instanceVariableNames)
                        {
                            _sb.AppendLine($"_{variableName} = {variableName};");
                        }

                        // Return statement
                        _sb.AppendLine();
                        //_sb.AppendLine($"return values;");
                        _sb.AppendLine($"return null;");

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

        private IList<string> GetInstanceVariableNamesCamelCase()
        {
            IList<string> list = _phaseVariableNamesCamelCaseHashSet.Union(_previousPositionVariableNamesCamelCaseHashSet)
                                                                    .Union(_inputVariableInfoDictionary.Keys)
                                                                    .ToArray();
            return list;
        }

        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }
    }
}
