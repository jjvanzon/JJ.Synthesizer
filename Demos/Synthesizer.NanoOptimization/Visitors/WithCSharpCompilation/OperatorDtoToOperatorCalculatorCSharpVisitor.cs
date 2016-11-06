using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithCSharpCompilation;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithCSharpCompilation
{
    /// <summary>
    /// This is the older version of the experiment. Newer experiments use the other visitor:
    /// OperatorDtoToPatchCalculatorCSharpVisitor.
    /// </summary>
    internal class OperatorDtoToOperatorCalculatorCSharpVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        private const string MULTIPLY_SYMBOL = "*";
        private const string PLUS_SYMBOL = "+";
        private const string PHASE_VARIABLE_PREFIX = "phase";
        private const string PREVIOUS_POSITION_VARIABLE_PREFIX = "prevPos";
        private const string INPUT_VARIABLE_PREFIX = "input";
        private const string POSITION_VARIABLE_PREFIX = "t";
        private const string TAB_STRING = "    ";
        private const int FIRST_VARIABLE_NUMBER = 0;

        private Stack<ValueInfo> _stack;
        private StringBuilderWithIndentation _sb;
        /// <summary> HashSet for unicity. </summary>
        private HashSet<string> _variableNamesToDeclareHashSet;
        /// <summary> To maintain instance integrity of input variables when converting from DTO to C# code. </summary>
        private Dictionary<VariableInput_OperatorDto, string> _variableInput_OperatorDto_To_VariableName_Dictionary;
        /// <summary> To maintain a counter for numbers to add to a variable names. Each operator type will get its own counter. </summary>
        private Dictionary<string, int> _camelCaseOperatorTypeName_To_VariableCounter_Dictionary;
        private int _inputVariableCounter;
        private int _phaseVariableCounter;
        private int _previousPositionVariableCounter;

        public string Execute(OperatorDto dto, string generatedNameSpace, string generatedClassName)
        {
            // Optimize Calculation Dto
            var preProcessingVisitor = new PreProcessing_OperatorDtoVisitor();
            dto = preProcessingVisitor.Execute(dto);

            // Initialize Fields
            _stack = new Stack<ValueInfo>();
            _variableNamesToDeclareHashSet = new HashSet<string>();
            _variableInput_OperatorDto_To_VariableName_Dictionary = new Dictionary<VariableInput_OperatorDto, string>();
            _camelCaseOperatorTypeName_To_VariableCounter_Dictionary = new Dictionary<string, int>();
            _inputVariableCounter = FIRST_VARIABLE_NUMBER;
            _phaseVariableCounter = FIRST_VARIABLE_NUMBER;
            _previousPositionVariableCounter = FIRST_VARIABLE_NUMBER;

            // Build up Method Body through Visitation
            _sb = new StringBuilderWithIndentation(TAB_STRING);
            _sb.IndentLevel = 3;
            Visit_OperatorDto_Polymorphic(dto);
            if (_stack.Count != 1)
            {
                throw new NotEqualException(() => _stack.Count, 1);
            }
            string methodBody = _sb.ToString();

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
                        foreach (string variableName in _variableNamesToDeclareHashSet)
                        {
                            _sb.AppendLine($"double {variableName} = 0.0;");
                        }

                        // Method Body
                        _sb.Append(methodBody);

                        // Return statement
                        ValueInfo returnValueInfo = _stack.Pop();
                        string returnValueLiteral = returnValueInfo.GetLiteral();
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

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IList<OperatorDto> VisitChildOperatorDtos(IList<OperatorDto> operatorDtos)
        {
            // Reverse the order, so calculators pop off the stack in the right order.
            for (int i = operatorDtos.Count - 1; i >= 0; i--)
            {
                OperatorDto operatorDto = operatorDtos[i];
                VisitOperatorDto(operatorDto);
            }

            return operatorDtos;
        }

        protected override OperatorDto Visit_Add_OperatorDto_Vars(Add_OperatorDto_Vars dto)
        {
            base.Visit_Add_OperatorDto_Vars(dto);

            ProcessMultiVarOperator(dto.OperatorTypeName, dto.Vars.Count, PLUS_SYMBOL);

            return dto;
        }

        protected override OperatorDto Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            ProcessNumber(dto.ConstValue);
            ProcessMultiVarOperator(dto.OperatorTypeName, dto.Vars.Count + 1, PLUS_SYMBOL);

            return dto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            ProcessNumber(dto.B);
            ProcessBinaryOperatorDto(dto.OperatorTypeName, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_VarB(dto);

            ProcessBinaryOperatorDto(dto.OperatorTypeName, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDto Visit_Number_OperatorDto_ConcreteOrPolymorphic(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto_ConcreteOrPolymorphic(dto);

            _sb.AppendLine();
            _sb.AppendLine("// " + dto.OperatorTypeName);

            ProcessNumber(dto.Number);

            return dto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            // Do not call base: It will visit the inlets in one blow. We need to visit the inlets one by one.

            _sb.AppendLine();
            _sb.AppendLine("// " + dto.OperatorTypeName);

            ProcessNumber(dto.Distance);
            ValueInfo distanceValueInfo = _stack.Pop();

            // TODO: Repeated code (see other Visit_Shift_? method).
            string sourcePosName = GetPositionVariableName(dto.DimensionStackLevel);
            string destPosName = GetPositionVariableName(dto.DimensionStackLevel + 1);
            string distanceLiteral = distanceValueInfo.GetLiteral();

            string line = $"{destPosName} = {sourcePosName} {PLUS_SYMBOL} {distanceLiteral};";
            _sb.AppendLine(line);

            VisitOperatorDto(dto.SignalOperatorDto);
            ValueInfo signalValueInfo = _stack.Pop();

            _stack.Push(signalValueInfo);

            return dto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            // Do not call base: It will visit the inlets in one blow. We need to visit the inlets one by one.

            VisitOperatorDto(dto.DistanceOperatorDto);
            ValueInfo distanceValueInfo = _stack.Pop();

            _sb.AppendLine();
            _sb.AppendLine("// " + dto.OperatorTypeName);

            string sourcePosName = GetPositionVariableName(dto.DimensionStackLevel);
            string destPosName = GetPositionVariableName(dto.DimensionStackLevel + 1);
            string distanceLiteral = distanceValueInfo.GetLiteral();

            string line = $"{destPosName} = {sourcePosName} {PLUS_SYMBOL} {distanceLiteral};";
            _sb.AppendLine(line);

            VisitOperatorDto(dto.SignalOperatorDto);
            ValueInfo signalValueInfo = _stack.Pop();

            _stack.Push(signalValueInfo);

            return dto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            _sb.AppendLine();
            _sb.AppendLine("// " + dto.OperatorTypeName);

            ValueInfo frequencyValueInfo = _stack.Pop();

            string phaseName = NewPhaseVariableName();
            string posName = GetPositionVariableName(dto.DimensionStackLevel);
            string prevPosName = NewPreviousPositionVariableName();
            string outputName = NewOutputName(dto.OperatorTypeName);
            string frequencyLiteral = frequencyValueInfo.GetLiteral();

            string line1 = $"{phaseName} += ({posName} - {prevPosName}) * {frequencyLiteral};";
            _sb.AppendLine(line1);

            string line2 = $"{prevPosName} = {posName};";
            _sb.AppendLine(line2);

            string line3 = $"double {outputName} = SineCalculator.Sin({phaseName});";
            _sb.AppendLine(line3);

            var valueInfo = new ValueInfo(outputName);
            _stack.Push(valueInfo);

            return dto;
        }

        protected override OperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            string inputVariableName = GetInputVariableName(dto);

            var valueInfo = new ValueInfo(inputVariableName);

            _stack.Push(valueInfo);

            return dto;
        }

        // Generalized Methods

        private void ProcessBinaryOperatorDto(string operatorTypeName, string operatorSymbol)
        {
            ValueInfo aValueInfo = _stack.Pop();
            ValueInfo bValueInfo = _stack.Pop();

            _sb.AppendLine();
            _sb.AppendLine("// " + operatorTypeName);

            string outputName = NewOutputName(operatorTypeName);
            string aLiteral = aValueInfo.GetLiteral();
            string bLiteral = bValueInfo.GetLiteral();

            string line = $"double {outputName} = {aLiteral} {operatorSymbol} {bLiteral};";
            _sb.AppendLine(line);

            var resultValueInfo = new ValueInfo(outputName);
            _stack.Push(resultValueInfo);
        }

        private void ProcessMultiVarOperator(string operatorTypeName, int varCount, string operatorSymbol)
        {
            _sb.AppendLine();
            _sb.AppendLine("// " + operatorTypeName);

            string outputName = NewOutputName(operatorTypeName);

            _sb.AppendTabs();
            _sb.Append($"double {outputName} =");

            for (int i = 0; i < varCount; i++)
            {
                ValueInfo valueInfo = _stack.Pop();

                _sb.Append(' ');
                _sb.Append(valueInfo.GetLiteral());

                bool isLast = i == varCount - 1;
                if (!isLast)
                {
                    _sb.Append(' ');
                    _sb.Append(operatorSymbol);
                }
            }

            _sb.Append(';');
            _sb.Append(Environment.NewLine);

            var resultValueInfo = new ValueInfo(outputName);
            _stack.Push(resultValueInfo);
        }

        private void ProcessNumber(double value)
        {
            _stack.Push(new ValueInfo(value));
        }

        // Helpers

        private string NewOutputName(string operatorTypeName)
        {
            // TODO: Lower Priority: You need an actual ToCamelCase for any name to be turned into code.
            string camelCaseOperatorTypeName = operatorTypeName.StartWithLowerCase();

            int counter;
            if (!_camelCaseOperatorTypeName_To_VariableCounter_Dictionary.TryGetValue(camelCaseOperatorTypeName, out counter))
            {
                counter = FIRST_VARIABLE_NUMBER;
            }

            string variableName = String.Format("{0}{1}", camelCaseOperatorTypeName, counter++);

            _camelCaseOperatorTypeName_To_VariableCounter_Dictionary[camelCaseOperatorTypeName] = counter;

            return variableName;
        }

        private string GetInputVariableName(VariableInput_OperatorDto dto)
        {
            string name;
            if (_variableInput_OperatorDto_To_VariableName_Dictionary.TryGetValue(dto, out name))
            {
                return name;
            }

            name = NewInputVariableName();

            _variableInput_OperatorDto_To_VariableName_Dictionary[dto] = name;

            return name;
        }

        private string NewInputVariableName()
        {
            string variableName = String.Format("{0}{1}", INPUT_VARIABLE_PREFIX, _inputVariableCounter++);

            _variableNamesToDeclareHashSet.Add(variableName);

            return variableName;
        }

        private string NewPhaseVariableName()
        {
            string variableName = String.Format("{0}{1}", PHASE_VARIABLE_PREFIX, _phaseVariableCounter++);

            _variableNamesToDeclareHashSet.Add(variableName);

            return variableName;
        }

        private string NewPreviousPositionVariableName()
        {
            string variableName = String.Format("{0}{1}", PREVIOUS_POSITION_VARIABLE_PREFIX, _previousPositionVariableCounter++);

            _variableNamesToDeclareHashSet.Add(variableName);

            return variableName;
        }

        private string GetPositionVariableName(int stackLevel)
        {
            string variableName = String.Format("{0}{1}", POSITION_VARIABLE_PREFIX, stackLevel);

            _variableNamesToDeclareHashSet.Add(variableName);

            return variableName;
        }
    }
}
