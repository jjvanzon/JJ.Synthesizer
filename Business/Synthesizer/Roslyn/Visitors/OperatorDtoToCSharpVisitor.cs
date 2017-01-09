using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Visitors;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Framework.Common;
using System.Diagnostics;

namespace JJ.Business.Synthesizer.Roslyn.Visitors
{
    internal class OperatorDtoToCSharpVisitor : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private const string TAB_STRING = "    ";
        private const int FIRST_VARIABLE_NUMBER = 0;

        private const string DIVIDE_SYMBOL = "/";
        private const string EQUALS_SYMBOL = "==";
        private const string GREATER_THAN_SYMBOL = ">";
        private const string GREATER_THAN_OR_EQUAL_SYMBOL = ">=";
        private const string LESS_THAN_SYMBOL = "<";
        private const string LESS_THAN_OR_EQUAL_SYMBOL = "<=";
        private const string MULTIPLY_SYMBOL = "*";
        private const string NOT_EQUAL_SYMBOL = "!=";
        private const string PLUS_SYMBOL = "+";
        private const string SUBTRACT_SYMBOL = "-";
        private const string PHASE_VARIABLE_PREFIX = "phase";
        private const string PREVIOUS_POSITION_VARIABLE_PREFIX = "prevPos";
        private const string INPUT_VARIABLE_PREFIX = "input";
        private const string POSITION_VARIABLE_PREFIX = "pos";
        private const string ORIGIN_VARIABLE_PREFIX = "origin";

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
        /// <summary> HashSet for unicity. </summary>
        protected HashSet<string> _originVariableNamesCamelCaseHashSet;

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
            _originVariableNamesCamelCaseHashSet = new HashSet<string>();
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
                _phaseVariableNamesCamelCaseHashSet.ToArray(),
                _originVariableNamesCamelCaseHashSet.ToArray());
        }

        [DebuggerHidden]
        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }

        protected override OperatorDtoBase Visit_Absolute_OperatorDto_VarX(Absolute_OperatorDto_VarX dto)
        {
            base.Visit_Absolute_OperatorDto_VarX(dto);

            ValueInfo xValueInfo = _stack.Pop();

            string variableName = GenerateOutputNameCamelCase(dto.OperatorTypeName);
            string xLiteral = xValueInfo.GetLiteral();

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {variableName} = {xLiteral};");
            _sb.AppendLine($"if ({variableName} < 0.0) {variableName} = -{variableName};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(variableName));

            return dto;
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessMultiVarOperator_Vars_NoConsts(dto, PLUS_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            return ProcessMultiVarOperator_Vars_1Const(dto, PLUS_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_VarOrigin(Divide_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            return Process_ConstA_ConstB_VarOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ConstOrigin(Divide_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            return Process_ConstA_VarB_ConstOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_VarOrigin(Divide_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            return Process_ConstA_VarB_VarOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ConstOrigin(Divide_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            return Process_VarA_ConstB_ConstOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_VarOrigin(Divide_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            return Process_VarA_ConstB_VarOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ConstOrigin(Divide_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            return Process_VarA_VarB_ConstOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_VarOrigin(Divide_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            return Process_VarA_VarB_VarOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ZeroOrigin(Divide_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            base.Visit_Divide_OperatorDto_ConstA_VarB_ZeroOrigin(dto);
            ProcessNumber(dto.A);

            return ProcessDivideZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ZeroOrigin(Divide_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            ProcessNumber(dto.B);
            base.Visit_Divide_OperatorDto_VarA_ConstB_ZeroOrigin(dto);

            return ProcessDivideZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ZeroOrigin(Divide_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            base.Visit_Divide_OperatorDto_VarA_VarB_ZeroOrigin(dto);

            return ProcessDivideZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_VarA_ConstB(Equal_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, EQUALS_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto_VarA_VarB(Equal_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, EQUALS_SYMBOL);
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_VarA_ConstB(GreaterThan_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, GREATER_THAN_SYMBOL);
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto_VarA_VarB(GreaterThan_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, GREATER_THAN_SYMBOL);
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(GreaterThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, GREATER_THAN_OR_EQUAL_SYMBOL);
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(GreaterThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, GREATER_THAN_OR_EQUAL_SYMBOL);
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_VarA_ConstB(LessThan_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, LESS_THAN_SYMBOL);
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto_VarA_VarB(LessThan_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, LESS_THAN_SYMBOL);
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(LessThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, LESS_THAN_OR_EQUAL_SYMBOL);
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto_VarA_VarB(LessThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, LESS_THAN_OR_EQUAL_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessMultiVarOperator_Vars_NoConsts(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
        {
            return ProcessMultiVarOperator_Vars_1Const(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            return Process_ConstA_ConstB_VarOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            return Process_ConstA_VarB_ConstOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            return Process_ConstA_VarB_VarOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            return Process_VarA_ConstB_ConstOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            return Process_VarA_ConstB_VarOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            return Process_VarA_VarB_ConstOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            return Process_VarA_VarB_VarOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Negative_OperatorDto_VarX(Negative_OperatorDto_VarX dto)
        {
            base.Visit_Negative_OperatorDto_VarX(dto);

            ValueInfo xValueInfo = _stack.Pop();

            string xLiteral = xValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {outputName} = -{xLiteral};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto_VarX(OneOverX_OperatorDto_VarX dto)
        {
            base.Visit_OneOverX_OperatorDto_VarX(dto);

            ValueInfo xValueInfo = _stack.Pop();

            string xLiteral = xValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {outputName} = 1.0 / {xLiteral};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_VarExponent(Power_OperatorDto_VarBase_VarExponent dto)
        {
            base.Visit_Power_OperatorDto_VarBase_VarExponent(dto);

            return Process_Math_Pow(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_ConstBase_VarExponent(Power_OperatorDto_ConstBase_VarExponent dto)
        {
            base.Visit_Power_OperatorDto_ConstBase_VarExponent(dto);
            ProcessNumber(dto.Base);

            return Process_Math_Pow(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
        {
            ProcessNumber(dto.Exponent);
            base.Visit_Power_OperatorDto_VarBase_ConstExponent(dto);

            return Process_Math_Pow(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_Exponent2(Power_OperatorDto_VarBase_Exponent2 dto)
        {
            base.Visit_Power_OperatorDto_VarBase_Exponent2(dto);

            ValueInfo baseValueInfo = _stack.Pop();

            string baseLiteral = baseValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {outputName} = {baseLiteral} * {baseLiteral};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_Exponent3(Power_OperatorDto_VarBase_Exponent3 dto)
        {
            base.Visit_Power_OperatorDto_VarBase_Exponent3(dto);

            ValueInfo baseValueInfo = _stack.Pop();

            string baseLiteral = baseValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {outputName} = {baseLiteral} * {baseLiteral} * {baseLiteral};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_Exponent4(Power_OperatorDto_VarBase_Exponent4 dto)
        {
            base.Visit_Power_OperatorDto_VarBase_Exponent4(dto);

            ValueInfo baseValueInfo = _stack.Pop();

            string baseLiteral = baseValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {outputName} = {baseLiteral} * {baseLiteral};");
            _sb.AppendLine($"{outputName} *= {outputName};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        private OperatorDtoBase Process_Math_Pow(OperatorDtoBase dto)
        {
            ValueInfo baseValueInfo = _stack.Pop();
            ValueInfo exponentValueInfo = _stack.Pop();

            string baseLiteral = baseValueInfo.GetLiteral();
            string exponentLiteral = exponentValueInfo.GetLiteral();
            string variableName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {variableName} = Math.Pow({baseLiteral}, {exponentLiteral});");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(variableName));

            return dto;
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_VarA_ConstB(NotEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, NOT_EQUAL_SYMBOL);
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto_VarA_VarB(NotEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, NOT_EQUAL_SYMBOL);
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
            return ProcessShift(dto, dto.SignalOperatorDto, distance: dto.Distance);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            return ProcessShift(dto, dto.SignalOperatorDto, dto.DistanceOperatorDto);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(dto);
            ProcessNumber(dto.Frequency);

            ValueInfo frequencyValueInfo = _stack.Pop();

            string phaseName = GeneratePhaseVariableNameCamelCase();
            string posName = GeneratePositionVariableNameCamelCase(dto.DimensionStackLevel);
            string originName = GenerateOriginVariableNameCamelCase(dto.DimensionStackLevel);
            string frequencyLiteral = frequencyValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{phaseName} = ({posName} - {originName}) * {frequencyLiteral};");
            _sb.AppendLine($"double {outputName} = SineCalculator.Sin({phaseName});");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            ValueInfo frequencyValueInfo = _stack.Pop();

            string phaseName = GeneratePhaseVariableNameCamelCase();
            string posName = GeneratePositionVariableNameCamelCase(dto.DimensionStackLevel);
            string prevPosName = GeneratePreviousPositionVariableNameCamelCase();
            string frequencyLiteral = frequencyValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{phaseName} += ({posName} - {prevPosName}) * {frequencyLiteral};");
            _sb.AppendLine($"{prevPosName} = {posName};");
            _sb.AppendLine($"double {outputName} = SineCalculator.Sin({phaseName});");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_ConstA_VarB(Subtract_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Subtract_OperatorDto_ConstA_VarB(dto);
            ProcessNumber(dto.A);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_ConstB(Subtract_OperatorDto_VarA_ConstB dto)
        {
            ProcessNumber(dto.B);
            base.Visit_Subtract_OperatorDto_VarA_ConstB(dto);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_VarB(Subtract_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Subtract_OperatorDto_VarA_VarB(dto);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            string inputVariableName = GetInputVariableNameCamelCase(dto);

            _stack.Push(new ValueInfo(inputVariableName));

            return dto;
        }

        // Generalized Methods

        private OperatorDtoBase ProcessBinaryOperator(OperatorDtoBase dto, string operatorSymbol)
        {
            ValueInfo aValueInfo = _stack.Pop();
            ValueInfo bValueInfo = _stack.Pop();

            string aLiteral = aValueInfo.GetLiteral();
            string bLiteral = bValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {outputName} = {aLiteral} {operatorSymbol} {bLiteral};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        private OperatorDtoBase ProcessComparativeOperator_VarA_ConstB(OperatorDtoBase_VarA_ConstB dto, string operatorSymbol)
        {
            ProcessNumber(dto.B);
            base.Visit_OperatorDto_Base(dto);

            return ProcessComparativeOperator(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessComparativeOperator_VarA_VarB(OperatorDtoBase_VarA_VarB dto, string operatorSymbol)
        {
            base.Visit_OperatorDto_Base(dto);

            return ProcessComparativeOperator(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessComparativeOperator(OperatorDtoBase dto, string operatorSymbol)
        {
            ValueInfo aValueInfo = _stack.Pop();
            ValueInfo bValueInfo = _stack.Pop();

            string aLiteral = aValueInfo.GetLiteral();
            string bLiteral = bValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {outputName} = {aLiteral} {operatorSymbol} {bLiteral} ? 1.0 : 0.0;");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        private OperatorDtoBase ProcessDivideZeroOrigin(OperatorDtoBase dto)
        {
            ValueInfo aValueInfo = _stack.Pop();
            ValueInfo bValueInfo = _stack.Pop();

            string aLiteral = aValueInfo.GetLiteral();
            string bLiteral = bValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {outputName} = {aLiteral} / {bLiteral};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        private OperatorDtoBase ProcessWithOrigin(OperatorDtoBase dto, string operatorSymbol)
        {
            ValueInfo aValueInfo = _stack.Pop();
            ValueInfo bValueInfo = _stack.Pop();
            ValueInfo originValueInfo = _stack.Pop();

            string aLiteral = aValueInfo.GetLiteral();
            string bLiteral = bValueInfo.GetLiteral();
            string originLiteral = originValueInfo.GetLiteral();
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine($"// {dto.OperatorTypeName}");
            _sb.AppendLine($"double {outputName} = ({aLiteral} - {originLiteral}) {operatorSymbol} {bLiteral} + {originLiteral};");
            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        private OperatorDtoBase ProcessMultiVarOperator_Vars_NoConsts(OperatorDtoBase_Vars dto, string operatorSymbol)
        {
            base.Visit_OperatorDto_Base(dto);

            return ProcessMultiVarOperator(dto, dto.Vars.Count, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiVarOperator_Vars_1Const(OperatorDtoBase_Vars_1Const dto, string operatorSymbol)
        {
            base.Visit_OperatorDto_Base(dto);
            ProcessNumber(dto.ConstValue);

            return ProcessMultiVarOperator(dto, dto.Vars.Count + 1, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiVarOperator(OperatorDtoBase dto, int varCount, string operatorSymbol)
        {
            string outputName = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);

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

            _sb.AppendLine();

            _stack.Push(new ValueInfo(outputName));

            return dto;
        }

        private void ProcessNumber(double value)
        {
            _stack.Push(new ValueInfo(value));
        }

        private Number_OperatorDto ProcessNumberOperatorDto(Number_OperatorDto dto)
        {
            base.Visit_OperatorDto_Base(dto);

            _sb.AppendLine("// " + dto.OperatorTypeName);

            ProcessNumber(dto.Number);

            _sb.AppendLine();

            return dto;
        }

        private OperatorDtoBase ProcessShift(OperatorDtoBase dto, OperatorDtoBase signalOperatorDto, OperatorDtoBase distanceOperatorDto = null, double? distance = null)
        {
            // Do not call base: It will visit the inlets in one blow. We need to visit the inlets one by one.

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

            string sourcePosName = GeneratePositionVariableNameCamelCase(dto.DimensionStackLevel);
            string destPosName = GeneratePositionVariableNameCamelCase(dto.DimensionStackLevel + 1);
            string distanceLiteral = distanceValueInfo.GetLiteral();

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{destPosName} = {sourcePosName} {PLUS_SYMBOL} {distanceLiteral};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(signalOperatorDto);
            ValueInfo signalValueInfo = _stack.Pop();

            _stack.Push(signalValueInfo);

            return dto;
        }

        private OperatorDtoBase Process_ConstA_ConstB_VarOrigin(OperatorDtoBase_ConstA_ConstB_VarOrigin dto, string operatorSymbol)
        {
            base.Visit_OperatorDto_Base(dto);
            ProcessNumber(dto.B);
            ProcessNumber(dto.A);

            return ProcessWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase Process_ConstA_VarB_ConstOrigin(OperatorDtoBase_ConstA_VarB_ConstOrigin dto, string operatorSymbol)
        {
            ProcessNumber(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            ProcessNumber(dto.A);

            return ProcessWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase Process_ConstA_VarB_VarOrigin(OperatorDtoBase_ConstA_VarB_VarOrigin dto, string operatorSymbol)
        {
            base.Visit_OperatorDto_Base(dto);
            ProcessNumber(dto.A);

            return ProcessWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase Process_VarA_ConstB_ConstOrigin(OperatorDtoBase_VarA_ConstB_ConstOrigin dto, string operatorSymbol)
        {
            ProcessNumber(dto.Origin);
            ProcessNumber(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase Process_VarA_ConstB_VarOrigin(OperatorDtoBase_VarA_ConstB_VarOrigin dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            ProcessNumber(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase Process_VarA_VarB_ConstOrigin(OperatorDtoBase_VarA_VarB_ConstOrigin dto, string operatorSymbol)
        {
            ProcessNumber(dto.Origin);
            base.Visit_OperatorDto_Base(dto);

            return ProcessWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase Process_VarA_VarB_VarOrigin(OperatorDtoBase_VarA_VarB_VarOrigin dto, string operatorSymbol)
        {
            base.Visit_OperatorDto_Base(dto);

            return ProcessWithOrigin(dto, operatorSymbol);
        }

        // Helpers

        private string GenerateOutputNameCamelCase(string operatorTypeName)
        {
            // TODO: You need an actual ToCamelCase for any name to be turned into code.
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

        private string GenerateOriginVariableNameCamelCase(int stackLevel)
        {
            string variableName = String.Format("{0}{1}", ORIGIN_VARIABLE_PREFIX, stackLevel);

            _originVariableNamesCamelCaseHashSet.Add(variableName);

            return variableName;
        }
    }
}
