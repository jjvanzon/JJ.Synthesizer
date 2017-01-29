using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Roslyn.Helpers;
using JJ.Business.SynthesizerPrototype.Visitors;
using JJ.Framework.Collections;
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
        private const string POSITION_VARIABLE_PREFIX = "position";

        protected Stack<string> _stack;
        protected StringBuilderWithIndentation _sb;

        /// <summary> Dictionary for unicity. Key is variable name camel-case. </summary>
        protected Dictionary<string, VariableInputInfo> _inputVariableInfoDictionary;
        /// <summary> HashSet for unicity. </summary>
        protected HashSet<string> _positionVariableNamesCamelCaseHashSet;
        /// <summary> HashSet for unicity. </summary>
        protected IList<string> _previousPositionVariableNamesCamelCase;
        /// <summary> HashSet for unicity. </summary>
        protected IList<string> _phaseVariableNamesCamelCase;

        /// <summary> To maintain instance integrity of input variables when converting from DTO to C# code. </summary>
        protected Dictionary<VariableInput_OperatorDto, string> _variableInput_OperatorDto_To_VariableName_Dictionary;

        /// <summary> To maintain a counter for numbers to add to a variable names. Each operator type will get its own counter. </summary>
        protected Dictionary<string, int> _camelCaseOperatorTypeName_To_VariableCounter_Dictionary;
        protected int _inputVariableCounter;
        protected int _phaseVariableCounter;
        protected int _previousPositionVariableCounter;

        public static string PREVIOUS_POSITION_VARIABLE_PREFIX1
        {
            get
            {
                return PREVIOUS_POSITION_VARIABLE_PREFIX;
            }
        }

        public OperatorDtoToCSharpVisitorResult Execute(OperatorDtoBase dto, int intialIndentLevel)
        {
            _stack = new Stack<string>();
            _inputVariableInfoDictionary = new Dictionary<string, VariableInputInfo>();
            _positionVariableNamesCamelCaseHashSet = new HashSet<string>();
            _previousPositionVariableNamesCamelCase = new List<string>();
            _phaseVariableNamesCamelCase = new List<string>();
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
                _previousPositionVariableNamesCamelCase,
                _phaseVariableNamesCamelCase);
        }

        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            dto.Vars.ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return ProcessMultiVarOperator(dto, dto.Vars.Count, PLUS_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            ProcessNumber(dto.ConstValue);
            dto.Vars.ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return ProcessMultiVarOperator(dto, dto.Vars.Count + 1, PLUS_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            ProcessNumber(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessBinaryOperator(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessBinaryOperator(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            return ProcessNumberOperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            return ProcessNumberOperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            return ProcessNumberOperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            return ProcessNumberOperatorDto(dto);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            // Do not call base: It will visit the inlets in one blow. We need to visit the inlets one by one.

            return ProcessShift(dto, distance: dto.Distance);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            // Do not call base: It will visit the inlets in one blow. We need to visit the inlets one by one.

            return ProcessShift(dto, dto.DistanceOperatorDto);
        }

        private OperatorDtoBase ProcessShift(IOperatorDto_VarSignal dto, OperatorDtoBase distanceOperatorDto = null, double? distance = null)
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

            string formattedDistance = _stack.Pop();
            string sourcePosition = GeneratePositionVariableName(dto.DimensionStackLevel);
            string destPosition = GeneratePositionVariableName(dto.DimensionStackLevel + 1);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{destPosition} = {sourcePosition} {PLUS_SYMBOL} {formattedDistance};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signalLiteral = _stack.Pop();

            _stack.Push(signalLiteral);

            return (OperatorDtoBase)dto; // Dirty. Refactor away if IOperatorDtoBase is the deepest base type.
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            string frequency = _stack.Pop();
            string phase = GeneratePhaseVariableName();
            string position = GeneratePositionVariableName(dto.DimensionStackLevel);
            string prevPosious = GeneratePreviousPositionVariableName();
            string output = GenerateOutputName(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{phase} += ({position} - {prevPosious}) * {frequency};");
            _sb.AppendLine($"{prevPosious} = {position};");
            _sb.AppendLine($"double {output} = SineCalculator.Sin({phase});");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            string inputVariableName = GetInputVariableName(dto);

            _stack.Push(inputVariableName);

            return dto;
        }

        // Generalized Methods

        private OperatorDtoBase ProcessBinaryOperator(OperatorDtoBase dto, string operatorSymbol)
        {
            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GenerateOutputName(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {a} {operatorSymbol} {b};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessMultiVarOperator(OperatorDtoBase dto, int varCount, string operatorSymbol)
        {
            string output = GenerateOutputName(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendTabs();
            _sb.Append($"double {output} =");

            for (int i = 0; i < varCount; i++)
            {
                string value = _stack.Pop();

                _sb.Append(' ');
                _sb.Append(value);

                bool isLast = i == varCount - 1;
                if (!isLast)
                {
                    _sb.Append(' ');
                    _sb.Append(operatorSymbol);
                }
            }

            _sb.Append(';');
            _sb.Append(Environment.NewLine);

            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private void ProcessNumber(double value)
        {
            _stack.Push(CompilationHelper.FormatValue(value));
        }

        private OperatorDtoBase ProcessNumberOperatorDto(Number_OperatorDto dto)
        {
            _sb.AppendLine("// " + dto.OperatorTypeName);

            ProcessNumber(dto.Number);

            _sb.AppendLine();

            return dto;
        }

        // Helpers

        private string GenerateOutputName(string operatorTypeName)
        {
            string camelCaseOperatorTypeName = operatorTypeName.ToCamelCase();

            int counter;
            if (!_camelCaseOperatorTypeName_To_VariableCounter_Dictionary.TryGetValue(camelCaseOperatorTypeName, out counter))
            {
                counter = FIRST_VARIABLE_NUMBER;
            }

            string variableName = string.Format("{0}{1}", camelCaseOperatorTypeName, counter++);

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

            VariableInputInfo valueInfo = GenerateInputVariableInfo(dto);

            _variableInput_OperatorDto_To_VariableName_Dictionary[dto] = valueInfo.NameCamelCase;

            return valueInfo.NameCamelCase;
        }

        private VariableInputInfo GenerateInputVariableInfo(VariableInput_OperatorDto dto)
        {
            string variableName = string.Format("{0}{1}", INPUT_VARIABLE_PREFIX, _inputVariableCounter++);
            var valueInfo = new VariableInputInfo(variableName, dto.DefaultValue);

            _inputVariableInfoDictionary.Add(variableName, valueInfo);

            return valueInfo;
        }

        private string GeneratePhaseVariableName()
        {
            string variableName = string.Format("{0}{1}", PHASE_VARIABLE_PREFIX, _phaseVariableCounter++);

            _phaseVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        private string GeneratePreviousPositionVariableName()
        {
            string variableName = string.Format("{0}{1}", PREVIOUS_POSITION_VARIABLE_PREFIX1, _previousPositionVariableCounter++);

            _previousPositionVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        private string GeneratePositionVariableName(int stackLevel)
        {
            string variableName = string.Format("{0}{1}", POSITION_VARIABLE_PREFIX, stackLevel);

            _positionVariableNamesCamelCaseHashSet.Add(variableName);

            return variableName;
        }

        private IList<string> GetInstanceVariableNamesCamelCase()
        {
            IList<string> list = _phaseVariableNamesCamelCase.Union(_previousPositionVariableNamesCamelCase)
                                                                    .Union(_inputVariableInfoDictionary.Keys)
                                                                    .ToArray();
            return list;
        }
    }
}
