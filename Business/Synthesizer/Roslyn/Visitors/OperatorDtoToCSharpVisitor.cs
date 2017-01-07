using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Visitors;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Roslyn.Visitors
{
    internal class OperatorDtoToCSharpVisitor : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private const string TAB_STRING = "    ";
        private const int FIRST_VARIABLE_NUMBER = 0;

        private const string MULTIPLY_SYMBOL = "*";
        private const string PLUS_SYMBOL = "+";
        private const string PHASE_VARIABLE_PREFIX = "phase";
        private const string PREVIOUS_POSITION_VARIABLE_PREFIX = "prevPos";
        private const string INPUT_VARIABLE_PREFIX = "input";
        private const string POSITION_VARIABLE_PREFIX = "t";

        protected Stack<ValueInfo> _stack;
        protected StringBuilderWithIndentation _sb;

        /// <summary> Dictionary for unicity. Key is variable name camel-case. </summary>
        protected Dictionary<string, ValueInfo> _inputVariableInfoDictionary;
        /// <summary> HashSet for unicity. </summary>
        protected HashSet<string> _positionVariableNamesCamelCaseHashSet;
        /// <summary> HashSet for unicity. </summary>
        protected HashSet<string> _previousPositionVariableNamesCamelCaseHashSet;
        /// <summary> HashSet for unicity. </summary>
        protected HashSet<string> _phaseVariableNamesCamelCaseHashSet;

        /// <summary> To maintain instance integrity of input variables when converting from DTO to C# code. </summary>
        protected Dictionary<VariableInput_OperatorDto, string> _variableInput_OperatorDto_To_VariableName_Dictionary;

        /// <summary> To maintain a counter for numbers to add to a variable names. Each operator type will get its own counter. </summary>
        protected Dictionary<string, int> _camelCaseOperatorTypeName_To_VariableCounter_Dictionary;
        protected int _inputVariableCounter;
        protected int _phaseVariableCounter;
        protected int _previousPositionVariableCounter;

        public OperatorDtoToCSharpVisitorResult Execute(OperatorDtoBase dto, int intialIndentLevel)
        {
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

            _sb = new StringBuilderWithIndentation(TAB_STRING);
            _sb.IndentLevel = intialIndentLevel;

            Visit_OperatorDto_Polymorphic(dto);

            string csharpCode = _sb.ToString();
            ValueInfo returnValueInfo = _stack.Pop();
            string returnValueLiteral = returnValueInfo.GetLiteral();

            return new OperatorDtoToCSharpVisitorResult(
                csharpCode, 
                returnValueLiteral,
                _inputVariableInfoDictionary.Values.ToArray(),
                _positionVariableNamesCamelCaseHashSet.ToArray(),
                _previousPositionVariableNamesCamelCaseHashSet.ToArray(),
                _phaseVariableNamesCamelCaseHashSet.ToArray());
        }

        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_Add_OperatorDto_Vars_NoConsts(dto);

            ProcessMultiVarOperator(dto.OperatorTypeName, dto.Vars.Count, PLUS_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            ProcessNumber(dto.ConstValue);
            ProcessMultiVarOperator(dto.OperatorTypeName, dto.Vars.Count + 1, PLUS_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_Multiply_OperatorDto_Vars_NoConsts(dto);

            ProcessMultiVarOperator(dto.OperatorTypeName, dto.Vars.Count, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Multiply_OperatorDto_Vars_1Const(dto);

            ProcessNumber(dto.ConstValue);
            ProcessMultiVarOperator(dto.OperatorTypeName, dto.Vars.Count + 1, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto(dto);

            ProcessNumberOperatorDto(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            base.Visit_Number_OperatorDto_NaN(dto);

            ProcessNumberOperatorDto(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            base.Visit_Number_OperatorDto_One(dto);

            ProcessNumberOperatorDto(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            base.Visit_Number_OperatorDto_Zero(dto);

            ProcessNumberOperatorDto(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            // Do not call base: It will visit the inlets in one blow. We need to visit the inlets one by one.

            ProcessShift(dto, dto.SignalOperatorDto, distance: dto.Distance);

            return dto;
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            // Do not call base: It will visit the inlets in one blow. We need to visit the inlets one by one.

            ProcessShift(dto, dto.SignalOperatorDto, dto.DistanceOperatorDto);

            return dto;
        }

        private void ProcessShift(OperatorDtoBase shiftOperatorDto, OperatorDtoBase signalOperatorDto, OperatorDtoBase distanceOperatorDto = null, double? distance = null)
        {
            _sb.AppendLine();
            _sb.AppendLine("// " + shiftOperatorDto.OperatorTypeName);

            if (distanceOperatorDto != null)
            {
                Visit_OperatorDto_Polymorphic(distanceOperatorDto);
            }
            else if (distance.HasValue)
            {
                ProcessNumber(distance.Value);
            }
            else
            {
                throw new Exception($"{nameof(distanceOperatorDto)} and {nameof(distance)} cannot both be null.");
            }
            ValueInfo distanceValueInfo = _stack.Pop();

            string sourcePosName = GeneratePositionVariableNameCamelCase(shiftOperatorDto.DimensionStackLevel);
            string destPosName = GeneratePositionVariableNameCamelCase(shiftOperatorDto.DimensionStackLevel + 1);
            string distanceLiteral = distanceValueInfo.GetLiteral();

            string line = $"{destPosName} = {sourcePosName} {PLUS_SYMBOL} {distanceLiteral};";
            _sb.AppendLine(line);

            Visit_OperatorDto_Polymorphic(signalOperatorDto);
            ValueInfo signalValueInfo = _stack.Pop();

            _stack.Push(signalValueInfo);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            _sb.AppendLine();
            _sb.AppendLine("// " + dto.OperatorTypeName);

            ValueInfo frequencyValueInfo = _stack.Pop();

            string phaseName = GeneratePhaseVariableNameCamelCase();
            string posName = GeneratePositionVariableNameCamelCase(dto.DimensionStackLevel);
            string prevPosName = GeneratePreviousPositionVariableNameCamelCase();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);
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

        protected override OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            string inputVariableName = GetInputVariableNameCamelCase(dto);

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

            string outputName = GenerateOutputNameCamelCase(operatorTypeName);
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

            string outputName = GenerateOutputNameCamelCase(operatorTypeName);

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

        private void ProcessNumberOperatorDto(Number_OperatorDto dto)
        {
            _sb.AppendLine();
            _sb.AppendLine("// " + dto.OperatorTypeName);

            ProcessNumber(dto.Number);
        }

        // Helpers

        private string GenerateOutputNameCamelCase(string operatorTypeName)
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

        private string GetInputVariableNameCamelCase(VariableInput_OperatorDto dto)
        {
            string name;
            if (_variableInput_OperatorDto_To_VariableName_Dictionary.TryGetValue(dto, out name))
            {
                return name;
            }

            ValueInfo valueInfo = GenerateInputVariableInfo(dto);

            _variableInput_OperatorDto_To_VariableName_Dictionary[dto] = valueInfo.NameCamelCase;

            return valueInfo.NameCamelCase;
        }

        private ValueInfo GenerateInputVariableInfo(VariableInput_OperatorDto dto)
        {
            string variableName = String.Format("{0}{1}", INPUT_VARIABLE_PREFIX, _inputVariableCounter++);
            var valueInfo = new ValueInfo(variableName, dto.DimensionEnum, dto.ListIndex, dto.DefaultValue);

            _inputVariableInfoDictionary.Add(variableName, valueInfo);

            return valueInfo;
        }

        private string GeneratePhaseVariableNameCamelCase()
        {
            string variableName = String.Format("{0}{1}", PHASE_VARIABLE_PREFIX, _phaseVariableCounter++);

            _phaseVariableNamesCamelCaseHashSet.Add(variableName);

            return variableName;
        }

        private string GeneratePreviousPositionVariableNameCamelCase()
        {
            string variableName = String.Format("{0}{1}", PREVIOUS_POSITION_VARIABLE_PREFIX, _previousPositionVariableCounter++);

            _previousPositionVariableNamesCamelCaseHashSet.Add(variableName);

            return variableName;
        }

        private string GeneratePositionVariableNameCamelCase(int stackLevel)
        {
            string variableName = String.Format("{0}{1}", POSITION_VARIABLE_PREFIX, stackLevel);

            _positionVariableNamesCamelCaseHashSet.Add(variableName);

            return variableName;
        }

        private IList<string> GetInstanceVariableNamesCamelCase()
        {
            IList<string> list = _phaseVariableNamesCamelCaseHashSet.Union(_previousPositionVariableNamesCamelCaseHashSet)
                                                                    .Union(_inputVariableInfoDictionary.Keys)
                                                                    .ToArray();
            return list;
        }
    }
}
