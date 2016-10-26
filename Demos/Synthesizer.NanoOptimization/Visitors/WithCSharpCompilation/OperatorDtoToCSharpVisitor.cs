using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.Common;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithCSharpCompilation
{
    internal class OperatorDtoToCSharpVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        private class ValueInfo
        {
            public string Name { get; set; }
            public double? Value { get; set; }

            public string GetLiteral()
            {
                if (Value.HasValue)
                {
                    double value = Value.Value;

                    if (Double.IsNaN(value) || Double.IsInfinity(value))
                    {
                        return "Double.NaN";
                    }
                    else
                    {
                        // TODO: Consider if this produces a complete literal, with exponent, decimal symbol, full precision.
                        string formattedValue = value.ToString(_formattingCulture);
                        return formattedValue;
                    }
                }
                else
                {
                    return Name;
                }
            }
        }

        private const string MULTIPLY_OPERATOR_SYMBOL = "*";
        private const string ADD_OPERATOR_SYMBOL = "+";
        private const string VARIABLE_PREFIX = "v";
        private const string INPUT_VARIABLE_PREFIX = "input";
        private const string DIMENSION_VARIABLE_PREFIX = "t";

        private static readonly CultureInfo _formattingCulture = new CultureInfo("en-US");

        private Stack<ValueInfo> _stack;
        private StringBuilder _sb;
        private Dictionary<VariableInput_OperatorDto, string> _variableInput_OperatorDto_To_VariableName_Dictionary;
        private int _variableCounter;
        private int _inputVariableCounter;

        public string Execute(OperatorDto dto)
        {
            _stack = new Stack<ValueInfo>();
            _sb = new StringBuilder();
            _variableInput_OperatorDto_To_VariableName_Dictionary = new Dictionary<VariableInput_OperatorDto, string>();
            _variableCounter = 0;
            _inputVariableCounter = 0;

            var preProcessingVisitor = new PreProcessing_OperatorDtoVisitor();
            dto = preProcessingVisitor.Execute(dto);

            Visit_OperatorDto_Polymorphic(dto);

            return _sb.ToString();
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

            ProcessMultiVarOperator(dto.Vars.Count, ADD_OPERATOR_SYMBOL);

            return dto;
        }

        protected override OperatorDto Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            ProcessNumber(dto.ConstValue);
            ProcessMultiVarOperator(dto.Vars.Count + 1, ADD_OPERATOR_SYMBOL);

            return dto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            ProcessNumber(dto.B);
            ProcessBinaryOperatorDto(MULTIPLY_OPERATOR_SYMBOL);

            return dto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_VarB(dto);

            ProcessBinaryOperatorDto(MULTIPLY_OPERATOR_SYMBOL);

            return dto;
        }

        protected override OperatorDto Visit_Number_OperatorDto_ConcreteOrPolymorphic(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto_ConcreteOrPolymorphic(dto);

            ProcessNumber(dto.Number);

            return dto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_ConstDistance(dto);

            ValueInfo signalValueInfo = _stack.Pop();

            ProcessNumber(dto.Distance);
            ValueInfo distanceValueInfo = _stack.Pop();

            // TODO: Repeated code (see other Visit_Shift_? method).
            string sourceDimensionVariableName = GetDimensionVariableName(dto.StackLevel);
            string destDimensionVariableName = GetDimensionVariableName(dto.StackLevel + 1);

            string codeLine = String.Format(
                "{0} = {1} {2} {3};",
                destDimensionVariableName,
                sourceDimensionVariableName,
                ADD_OPERATOR_SYMBOL,
                distanceValueInfo.GetLiteral());

            _sb.AppendLine(codeLine);

            _stack.Push(signalValueInfo);

            return dto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_VarDistance(dto);

            ValueInfo signalValueInfo = _stack.Pop();
            ValueInfo distanceValueInfo = _stack.Pop();

            string sourceDimensionVariableName = GetDimensionVariableName(dto.StackLevel);
            string destDimensionVariableName = GetDimensionVariableName(dto.StackLevel + 1); 

            string codeLine = String.Format(
                "{0} = {1} {2} {3};",
                destDimensionVariableName,
                sourceDimensionVariableName,
                ADD_OPERATOR_SYMBOL,
                distanceValueInfo.GetLiteral());

            _sb.AppendLine(codeLine);

            _stack.Push(signalValueInfo);

            return dto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            // Temporary, unfinished implementation to make visitation succeed.

            ValueInfo frequencyValueInfo = _stack.Pop();

            _stack.Push(frequencyValueInfo);

            return dto;
        }

        protected override OperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            string inputVariableName = GetInputVariableName(dto);

            var valueInfo = new ValueInfo { Name = inputVariableName };

            _stack.Push(valueInfo);

            return dto;
        }

        // Generalized Methods

        private void ProcessBinaryOperatorDto(string operatorSymbol)
        {
            ValueInfo aValueInfo = _stack.Pop();
            ValueInfo bValueInfo = _stack.Pop();

            string newVariableName = NewVariableName();

            string codeLine = String.Format("double {0} = {1} {2} {3};", newVariableName, aValueInfo.GetLiteral(), operatorSymbol, bValueInfo.GetLiteral());
            _sb.AppendLine(codeLine);

            var resultValueInfo = new ValueInfo { Name = newVariableName };
            _stack.Push(resultValueInfo);
        }

        private void ProcessMultiVarOperator(int varCount, string operatorSymbol)
        {
            string newVariableName = NewVariableName();

            _sb.AppendFormat("double {0} =", newVariableName);

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

            var resultValueInfo = new ValueInfo { Name = newVariableName };
            _stack.Push(resultValueInfo);
        }

        private void ProcessNumber(double value)
        {
            _stack.Push(new ValueInfo { Value = value });
        }

        // Helpers

        private string NewVariableName()
        {
            string variableName = String.Format("{0}{1}", VARIABLE_PREFIX, _variableCounter++);
            return variableName;
        }

        private string NewInputVariableName()
        {
            string inputVariableName = String.Format("{0}{1}", INPUT_VARIABLE_PREFIX, _inputVariableCounter++);
            return inputVariableName;
        }

        private string GetDimensionVariableName(int stackLevel)
        {
            string variableName = String.Format("{0}{1}", DIMENSION_VARIABLE_PREFIX, stackLevel);
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
    }
}
