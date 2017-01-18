using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Roslyn.Helpers;
using JJ.Business.SynthesizerPrototype.Visitors;
using JJ.Framework.Common;

namespace JJ.Business.SynthesizerPrototype.Roslyn.Visitors
{
    internal class OperatorDtoToCSharpVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        private const string TAB_STRING = "    ";
        private const int FIRST_VARIABLE_NUMBER = 0;

        private const string MULTIPLY_SYMBOL = "*";
        private const string PLUS_SYMBOL = "+";
        private const string PHASE_VARIABLE_PREFIX = "phase";
        private const string PREVIOUS_POSITION_VARIABLE_PREFIX = "prevPos";
        private const string INPUT_VARIABLE_PREFIX = "input";
        private const string POSITION_VARIABLE_PREFIX = "t";

        protected Stack<string> _stack;
        protected StringBuilderWithIndentation _sb;

        /// <summary> Dictionary for unicity. Key is variable name camel-case. </summary>
        protected Dictionary<string, VariableInputInfo> _inputVariableInfoDictionary;
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
            _stack = new Stack<string>();
            _inputVariableInfoDictionary = new Dictionary<string, VariableInputInfo>();
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
            string returnValueLiteral = _stack.Pop();

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

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            ProcessNumber(dto.B);
            ProcessBinaryOperator(dto.OperatorTypeName, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_VarB(dto);

            ProcessBinaryOperator(dto.OperatorTypeName, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto(dto);

            ProcessNumberOperator(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            base.Visit_Number_OperatorDto_NaN(dto);

            ProcessNumberOperator(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            base.Visit_Number_OperatorDto_One(dto);

            ProcessNumberOperator(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            base.Visit_Number_OperatorDto_Zero(dto);

            ProcessNumberOperator(dto);

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

            string distanceLiteral = _stack.Pop();
            string sourcePosName = GeneratePositionVariableNameCamelCase(shiftOperatorDto.DimensionStackLevel);
            string destPosName = GeneratePositionVariableNameCamelCase(shiftOperatorDto.DimensionStackLevel + 1);

            _sb.AppendLine();
            _sb.AppendLine("// " + shiftOperatorDto.OperatorTypeName);
            _sb.AppendLine($"{destPosName} = {sourcePosName} {PLUS_SYMBOL} {distanceLiteral};");

            Visit_OperatorDto_Polymorphic(signalOperatorDto);
            string signalLiteral = _stack.Pop();

            _stack.Push(signalLiteral);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            string frequencyLiteral = _stack.Pop();
            string phaseName = GeneratePhaseVariableNameCamelCase();
            string posName = GeneratePositionVariableNameCamelCase(dto.DimensionStackLevel);
            string prevPosName = GeneratePreviousPositionVariableNameCamelCase();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine();
            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{phaseName} += ({posName} - {prevPosName}) * {frequencyLiteral};");
            _sb.AppendLine($"{prevPosName} = {posName};");
            _sb.AppendLine($"double {outputName} = SineCalculator.Sin({phaseName});");

            _stack.Push(outputName);

            return dto;
        }

        protected override OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            string inputVariableName = GetInputVariableNameCamelCase(dto);

            _stack.Push(inputVariableName);

            return dto;
        }

        // Generalized Methods

        private void ProcessBinaryOperator(string operatorTypeName, string operatorSymbol)
        {
            string aLiteral = _stack.Pop();
            string bLiteral = _stack.Pop();
            string outputName = GenerateOutputNameCamelCase(operatorTypeName);

            _sb.AppendLine();
            _sb.AppendLine("// " + operatorTypeName);
            _sb.AppendLine($"double {outputName} = {aLiteral} {operatorSymbol} {bLiteral};");

            _stack.Push(outputName);
        }

        private void ProcessMultiVarOperator(string operatorTypeName, int varCount, string operatorSymbol)
        {
            string outputName = GenerateOutputNameCamelCase(operatorTypeName);

            _sb.AppendLine();
            _sb.AppendLine("// " + operatorTypeName);
            _sb.AppendTabs();
            _sb.Append($"double {outputName} =");

            for (int i = 0; i < varCount; i++)
            {
                string valueLiteral = _stack.Pop();

                _sb.Append(' ');
                _sb.Append(valueLiteral);

                bool isLast = i == varCount - 1;
                if (!isLast)
                {
                    _sb.Append(' ');
                    _sb.Append(operatorSymbol);
                }
            }

            _sb.Append(';');
            _sb.Append(Environment.NewLine);

            _stack.Push(outputName);
        }

        private void ProcessNumber(double value)
        {
            _stack.Push(CompilationHelper.FormatValue(value));
        }

        private void ProcessNumberOperator(Number_OperatorDto dto)
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

            VariableInputInfo valueInfo = GenerateInputVariableInfo(dto);

            _variableInput_OperatorDto_To_VariableName_Dictionary[dto] = valueInfo.NameCamelCase;

            return valueInfo.NameCamelCase;
        }

        private VariableInputInfo GenerateInputVariableInfo(VariableInput_OperatorDto dto)
        {
            string variableName = String.Format("{0}{1}", INPUT_VARIABLE_PREFIX, _inputVariableCounter++);
            var valueInfo = new VariableInputInfo(variableName, dto.DefaultValue);

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
