using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Visitors;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Framework.Common;
using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Roslyn.Visitors
{
    internal class OperatorDtoToCSharpVisitor : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private const string TAB_STRING = "    ";
        private const int FIRST_VARIABLE_NUMBER = 0;

        private const string AND_SYMBOL = "&&";
        private const string DIVIDE_SYMBOL = "/";
        private const string EQUALS_SYMBOL = "==";
        private const string GREATER_THAN_SYMBOL = ">";
        private const string GREATER_THAN_OR_EQUAL_SYMBOL = ">=";
        private const string LESS_THAN_SYMBOL = "<";
        private const string LESS_THAN_OR_EQUAL_SYMBOL = "<=";
        private const string MULTIPLY_SYMBOL = "*";
        private const string NOT_EQUAL_SYMBOL = "!=";
        private const string OR_SYMBOL = "||";
        private const string PLUS_SYMBOL = "+";
        private const string SUBTRACT_SYMBOL = "-";
        private const string PHASE_VARIABLE_PREFIX = "phase";
        private const string PREVIOUS_POSITION_VARIABLE_PREFIX = "prevPos";
        private const string INPUT_VARIABLE_PREFIX = "input";
        private const string STANDARD_DIMENSION_VARIABLE_PREFIX = "s_";
        private const string CUSTOM_DIMENSION_VARIABLE_PREFIX = "c_";
        private const string ORIGIN_VARIABLE_PREFIX = "origin";
        /// <summary> {0} = phase  </summary>
        private const string SAW_DOWN_FORMULA_FORMAT = "1.0 - (2.0 * {0} % 2.0)";
        /// <summary> {0} = phase  </summary>
        private const string SAW_UP_FORMULA_FORMAT = "-1.0 + (2.0 * {0} % 2.0)";
        /// <summary> {0} = phase  </summary>
        private const string SINE_FORMULA_FORMAT = "SineCalculator.Sin({0})";
        /// <summary> {0} = phase  </summary>
        private const string SQUARE_FORMULA_FORMAT = "{0} % 1.0 < 0.5 ? 1.0 : -1.0";

        private Stack<string> _stack;
        private StringBuilderWithIndentation _sb;
        /// <summary> Dictionary for unicity. Key is variable name camel-case. </summary>
        private Dictionary<string, InputVariableInfo> _inputVariableInfoDictionary;
        /// <summary> HashSet for unicity. </summary>
        private HashSet<string> _positionVariableNamesCamelCaseHashSet;
        private IList<string> _previousPositionVariableNamesCamelCase;
        private IList<string> _phaseVariableNamesCamelCase;
        private IList<string> _originVariableNamesCamelCase;

        /// <summary> To maintain instance integrity of input variables when converting from DTO to C# code. </summary>
        private Dictionary<VariableInput_OperatorDto, string> _variableInput_OperatorDto_To_VariableName_Dictionary;

        /// <summary> To maintain a counter for numbers to add to a variable names. Each operator type will get its own counter. </summary>
        private Dictionary<string, int> _camelCaseOperatorTypeName_To_VariableCounter_Dictionary;
        private int _inputVariableCounter;
        private int _phaseVariableCounter;
        private int _previousPositionVariableCounter;
        private int _originVariableCounter;

        public OperatorDtoToCSharpVisitorResult Execute(OperatorDtoBase dto, int intialIndentLevel)
        {
            _stack = new Stack<string>();
            _inputVariableInfoDictionary = new Dictionary<string, InputVariableInfo>();
            _positionVariableNamesCamelCaseHashSet = new HashSet<string>();
            _previousPositionVariableNamesCamelCase = new List<string>();
            _phaseVariableNamesCamelCase = new List<string>();
            _originVariableNamesCamelCase = new List<string>();
            _variableInput_OperatorDto_To_VariableName_Dictionary = new Dictionary<VariableInput_OperatorDto, string>();
            _camelCaseOperatorTypeName_To_VariableCounter_Dictionary = new Dictionary<string, int>();
            _inputVariableCounter = FIRST_VARIABLE_NUMBER;
            _phaseVariableCounter = FIRST_VARIABLE_NUMBER;
            _previousPositionVariableCounter = FIRST_VARIABLE_NUMBER;
            _originVariableCounter = FIRST_VARIABLE_NUMBER;

            _sb = new StringBuilderWithIndentation(TAB_STRING);
            _sb.IndentLevel = intialIndentLevel;

            Visit_OperatorDto_Polymorphic(dto);

            string generatedCode = _sb.ToString();
            string returnValue = _stack.Pop();

            return new OperatorDtoToCSharpVisitorResult(
                generatedCode, 
                returnValue,
                _inputVariableInfoDictionary.Values.ToArray(),
                _positionVariableNamesCamelCaseHashSet.ToArray(),
                _previousPositionVariableNamesCamelCase.ToArray(),
                _phaseVariableNamesCamelCase.ToArray(),
                _originVariableNamesCamelCase.ToArray());
        }

        [DebuggerHidden]
        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }

        protected override OperatorDtoBase Visit_Absolute_OperatorDto_VarX(Absolute_OperatorDto_VarX dto)
        {
            Visit_OperatorDto_Polymorphic(dto.XOperatorDto);

            string x = _stack.Pop();
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {variable} = {x};");
            _sb.AppendLine($"if ({variable} < 0.0) {variable} = -{variable};");
            _sb.AppendLine();

            _stack.Push(variable);

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

        protected override OperatorDtoBase Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
        {
            return ProcessLogicalBinaryOperator(dto, AND_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
        {
            return ProcessLogicalBinaryOperator(dto, OR_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_ConstB_VarOrigin(Divide_OperatorDto_ConstA_ConstB_VarOrigin dto)
        {
            return ProcessMultiplyOrDivide_ConstA_ConstB_VarOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ConstOrigin(Divide_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            return ProcessMultiplyOrDivide_ConstA_VarB_ConstOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_VarOrigin(Divide_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            return ProcessMultiplyOrDivide_ConstA_VarB_VarOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ConstOrigin(Divide_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            return ProcessMultiplyOrDivide_VarA_ConstB_ConstOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_VarOrigin(Divide_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            return ProcessMultiplyOrDivide_VarA_ConstB_VarOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ConstOrigin(Divide_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            return ProcessMultiplyOrDivide_VarA_VarB_ConstOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_VarOrigin(Divide_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            return ProcessMultiplyOrDivide_VarA_VarB_VarOrigin(dto, DIVIDE_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_ConstA_VarB_ZeroOrigin(Divide_OperatorDto_ConstA_VarB_ZeroOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            ProcessNumber(dto.A);

            return ProcessDivideZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_ConstB_ZeroOrigin(Divide_OperatorDto_VarA_ConstB_ZeroOrigin dto)
        {
            ProcessNumber(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessDivideZeroOrigin(dto);
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto_VarA_VarB_ZeroOrigin(Divide_OperatorDto_VarA_VarB_ZeroOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

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
            return ProcessMultiplyOrDivide_ConstA_ConstB_VarOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin dto)
        {
            return ProcessMultiplyOrDivide_ConstA_VarB_ConstOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin dto)
        {
            return ProcessMultiplyOrDivide_ConstA_VarB_VarOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin dto)
        {
            return ProcessMultiplyOrDivide_VarA_ConstB_ConstOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin dto)
        {
            return ProcessMultiplyOrDivide_VarA_ConstB_VarOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin dto)
        {
            return ProcessMultiplyOrDivide_VarA_VarB_ConstOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin(MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin dto)
        {
            return ProcessMultiplyOrDivide_VarA_VarB_VarOrigin(dto, MULTIPLY_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Negative_OperatorDto_VarX(Negative_OperatorDto_VarX dto)
        {
            Visit_OperatorDto_Polymorphic(dto.XOperatorDto);

            string x = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = -{x};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Not_OperatorDto_VarX(Not_OperatorDto_VarX dto)
        {
            Visit_OperatorDto_Polymorphic(dto.XOperatorDto);

            string x = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {x} == 0.0 ? 1.0 : 0.0;");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto_VarX(OneOverX_OperatorDto_VarX dto)
        {
            Visit_OperatorDto_Polymorphic(dto.XOperatorDto);

            string x = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = 1.0 / {x};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_VarExponent(Power_OperatorDto_VarBase_VarExponent dto)
        {
            Visit_OperatorDto_Polymorphic(dto.ExponentOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            return Process_Math_Pow(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_ConstBase_VarExponent(Power_OperatorDto_ConstBase_VarExponent dto)
        {
            Visit_OperatorDto_Polymorphic(dto.ExponentOperatorDto);
            ProcessNumber(dto.Base);

            return Process_Math_Pow(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
        {
            ProcessNumber(dto.Exponent);
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            return Process_Math_Pow(dto);
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_Exponent2(Power_OperatorDto_VarBase_Exponent2 dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            string @base = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {@base} * {@base};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_Exponent3(Power_OperatorDto_VarBase_Exponent3 dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            string @base = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {@base} * {@base} * {@base};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_Exponent4(Power_OperatorDto_VarBase_Exponent4 dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            string @base = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {@base} * {@base};");
            _sb.AppendLine($"{output} *= {output};");
            _sb.AppendLine();

            _stack.Push(output);

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

        protected override OperatorDtoBase Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            throw new NotImplementedException();
            //string position = GeneratePositionVariableNameCamelCase(dto.DimensionStackLevel);
            //string noiseCalculator = GenerateNoiseCalculatorNameCamelCase(dto.OperatorID);
            //string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            //_sb.AppendLine("// " + dto.OperatorTypeName);
            //_sb.AppendLine($"double {output} = _{noiseCalculator}.GetValue({position});");
            //_sb.AppendLine();

            //return dto;
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

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting dto)
        {
            ProcessNumber(dto.Width);
            ProcessNumber(dto.Frequency);

            return Process_Pulse_OperatorDto_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting dto)
        {
            ProcessNumber(dto.Width);
            ProcessNumber(dto.Frequency);

            return Process_Pulse_OperatorDto_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            ProcessNumber(dto.Frequency);

            return Process_Pulse_OperatorDto_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            ProcessNumber(dto.Frequency);

            return Process_Pulse_OperatorDto_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking dto)
        {
            ProcessNumber(dto.Width);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_OperatorDto_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking dto)
        {
            ProcessNumber(dto.Width);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_OperatorDto_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_OperatorDto_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_OperatorDto_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(SawDown_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            ProcessNumber(dto.Frequency);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => String.Format(SAW_DOWN_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(SawDown_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessOriginShifter(dto, x => String.Format(SAW_DOWN_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(SawDown_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => String.Format(SAW_DOWN_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(SawDown_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessPhaseTracker(dto, x => String.Format(SAW_DOWN_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(SawUp_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            ProcessNumber(dto.Frequency);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => String.Format(SAW_UP_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(SawUp_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessOriginShifter(dto, x => String.Format(SAW_UP_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(SawUp_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => String.Format(SAW_UP_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(SawUp_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessPhaseTracker(dto, x => String.Format(SAW_UP_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            return ProcessShift(dto, distance: dto.Distance);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            return ProcessShift(dto, dto.DistanceOperatorDto);
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            ProcessNumber(dto.Frequency);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => String.Format(SINE_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessOriginShifter(dto, x => String.Format(SINE_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => String.Format(SINE_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessPhaseTracker(dto, x => String.Format(SINE_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(Square_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            ProcessNumber(dto.Frequency);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => String.Format(SQUARE_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(Square_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessOriginShifter(dto, x => String.Format(SQUARE_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(Square_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => String.Format(SQUARE_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(Square_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessPhaseTracker(dto, x => String.Format(SQUARE_FORMULA_FORMAT, x));
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            ProcessNumber(dto.Origin);
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_OperatorDto_WithOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_OperatorDto_WithOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            ProcessNumber(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_OperatorDto_WithOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_OperatorDto_WithOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_OperatorDto_ZeroOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_OperatorDto_ZeroOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_WithOriginShifting(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_WithPhaseTracking(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            ProcessNumber(dto.Origin);
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_OperatorDto_WithOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_OperatorDto_WithOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            ProcessNumber(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_OperatorDto_WithOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_OperatorDto_WithOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_OperatorDto_ZeroOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_OperatorDto_ZeroOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_WithOriginShifting(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_WithPhaseTracking(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_ConstA_VarB(Subtract_OperatorDto_ConstA_VarB dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            ProcessNumber(dto.A);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_ConstB(Subtract_OperatorDto_VarA_ConstB dto)
        {
            ProcessNumber(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto_VarA_VarB(Subtract_OperatorDto_VarA_VarB dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            ProcessNumber(dto.Frequency);

            string frequency = _stack.Pop();
            string position = GeneratePositionVariableNameCamelCase(dto);
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine($"// {dto.OperatorTypeName}");
            _sb.AppendLine($"double {variable} = {position} * {frequency};");
            Write_TriangleCode_AfterDeterminePhase(variable);

            _stack.Push(variable);

            return dto;
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            ProcessNumber(dto.Frequency);

            string frequency = _stack.Pop();

            string position = GeneratePositionVariableNameCamelCase(dto);
            string origin = GenerateOriginVariableNameCamelCase();
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine($"// {dto.OperatorTypeName}");
            _sb.AppendLine($"double {variable} = ({position} - {origin}) * {frequency};");
            Write_TriangleCode_AfterDeterminePhase(variable);

            _stack.Push(variable);

            return dto;
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            string frequency = _stack.Pop();
            string position = GeneratePositionVariableNameCamelCase(dto);
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine($"// {dto.OperatorTypeName}");
            _sb.AppendLine($"double {variable} = {position} * {frequency};");
            Write_TriangleCode_AfterDeterminePhase(variable);

            _stack.Push(variable);

            return dto;
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            string frequency = _stack.Pop();

            string phase = GenerateLongLivedPhaseVariableNameCamelCase();
            string posisition = GeneratePositionVariableNameCamelCase(dto);
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine($"// {dto.OperatorTypeName}");
            _sb.AppendLine($"{phase} = {posisition} * {frequency};");
            // From here the code is the same as the method above.
            // TODO: You could prevent the first addition in the code written in the method called here,
            // by initializing phase with 0.5 for at the beginning of the chunk calculation.
            _sb.AppendLine($"double {variable} = {phase};");
            Write_TriangleCode_AfterDeterminePhase(variable);

            _stack.Push(variable);

            return dto;
        }

        protected override OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            string inputVariable = GetInputVariableNameCamelCase(dto);

            _stack.Push(inputVariable);

            return dto;
        }

        // Generalized Methods

        private OperatorDtoBase ProcessBinaryOperator(OperatorDtoBase dto, string operatorSymbol)
        {
            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {a} {operatorSymbol} {b};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessLogicalBinaryOperator(OperatorDtoBase_VarA_VarB dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {a} != 0.0 {operatorSymbol} {b} != 0.0 ? 1.0 : 0.0;");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessComparativeOperator_VarA_ConstB(OperatorDtoBase_VarA_ConstB dto, string operatorSymbol)
        {
            ProcessNumber(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessComparativeOperator(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessComparativeOperator_VarA_VarB(OperatorDtoBase_VarA_VarB dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessComparativeOperator(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessComparativeOperator(OperatorDtoBase dto, string operatorSymbol)
        {
            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {a} {operatorSymbol} {b} ? 1.0 : 0.0;");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessDivideZeroOrigin(OperatorDtoBase dto)
        {
            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {a} / {b};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessMultiplyOrDivideWithOrigin(OperatorDtoBase dto, string operatorSymbol)
        {
            string a = _stack.Pop();
            string b = _stack.Pop();
            string origin = _stack.Pop();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine($"// {dto.OperatorTypeName}");
            _sb.AppendLine($"double {output} = ({a} - {origin}) {operatorSymbol} {b} + {origin};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase Process_Math_Pow(OperatorDtoBase dto)
        {
            string @base = _stack.Pop();
            string exponent = _stack.Pop();
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {variable} = Math.Pow({@base}, {exponent});");
            _sb.AppendLine();

            _stack.Push(variable);

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
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

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

        private Number_OperatorDto ProcessNumberOperatorDto(Number_OperatorDto dto)
        {
            _sb.AppendLine("// " + dto.OperatorTypeName);

            ProcessNumber(dto.Number);

            _sb.AppendLine();

            return dto;
        }

        private OperatorDtoBase ProcessPhaseTracker(OperatorDtoBase_VarFrequency dto, Func<string, string> getRightHandFormulaDelegate)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            string frequency = _stack.Pop();
            string phase = GenerateLongLivedPhaseVariableNameCamelCase();
            string position = GeneratePositionVariableNameCamelCase(dto);
            string previousPosition = GeneratePreviousPositionVariableNameCamelCase();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);
            string rightHandFormula = getRightHandFormulaDelegate(phase);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{phase} += ({position} - {previousPosition}) * {frequency};");
            _sb.AppendLine($"{previousPosition} = {position};");
            _sb.AppendLine($"double {output} = {rightHandFormula};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase Process_Pulse_OperatorDto_WithPhaseTracking(OperatorDtoBase_VarFrequency dto)
        {
            string frequency = _stack.Pop();
            string width = _stack.Pop();
            string phase = GenerateLongLivedPhaseVariableNameCamelCase();
            string position = GeneratePositionVariableNameCamelCase(dto);
            string previousPosition = GeneratePreviousPositionVariableNameCamelCase();
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{phase} += ({position} - {previousPosition}) * {frequency};");
            _sb.AppendLine($"{previousPosition} = {position};");
            _sb.AppendLine($"double {output} = {phase} % 1.0 < {width} ? 1.0 : -1.0;");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase Process_Pulse_OperatorDto_WithOriginShifting(OperatorDtoBase_ConstFrequency dto)
        {
            string frequency = _stack.Pop();
            string width = _stack.Pop();
            string position = GeneratePositionVariableNameCamelCase(dto);
            string origin = GenerateOriginVariableNameCamelCase();
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {variable} = ({position} - {origin}) * {frequency};");
            _sb.AppendLine($"{variable} = {variable} % 1.0 < {width} ? 1.0 : -1.0;");
            _sb.AppendLine();

            _stack.Push(variable);

            return dto;
        }

        private OperatorDtoBase Process_Pulse_OperatorDto_NoPhaseTrackingOrOriginShifting(IOperatorDto_WithDimension dto)
        {
            string frequency = _stack.Pop();
            string width = _stack.Pop();
            string position = GeneratePositionVariableNameCamelCase(dto);
            string origin = GenerateOriginVariableNameCamelCase();
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {variable} = {position} * {frequency};");
            _sb.AppendLine($"{variable} = {variable} % 1.0 < {width} ? 1.0 : -1.0;");
            _sb.AppendLine();

            _stack.Push(variable);

            return (OperatorDtoBase)dto;
        }

        private OperatorDtoBase ProcessShift(IOperatorDto_VarSignal_WithDimension dto, OperatorDtoBase distanceOperatorDto = null, double? distance = null)
        {
            // Do not call base: Base will visit the inlets in one blow. We need to visit the inlets one by one.

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
            string sourcePos = GeneratePositionVariableNameCamelCase(dto);
            string destPos = GeneratePositionVariableNameCamelCase(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{destPos} = {sourcePos} {PLUS_SYMBOL} {distanceLiteral};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            _stack.Push(signal);

            return (OperatorDtoBase)dto; // Dirty. Refactor away if IOperatorDtoBase is the deepest base type.
        }

        private void Process_StretchOrSquash_OperatorDto_WithOrigin(IOperatorDto_VarSignal_WithDimension dto, string divideOrMultiplySymbol)
        {
            string factor = _stack.Pop();
            string origin = _stack.Pop();
            string sourcePos = GeneratePositionVariableNameCamelCase(dto);
            string destPos = GeneratePositionVariableNameCamelCase(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{destPos} = ({sourcePos} - {origin}) {divideOrMultiplySymbol} {factor} + {origin};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);
        }

        private void Process_StretchOrSquash_OperatorDto_ZeroOrigin(IOperatorDto_VarSignal_WithDimension dto, string divideOrMultiplySymbol)
        {
            string factor = _stack.Pop();
            string sourcePos = GeneratePositionVariableNameCamelCase(dto);
            string destPos = GeneratePositionVariableNameCamelCase(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{destPos} = {sourcePos} {divideOrMultiplySymbol} {factor};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);
        }

        private void Process_StretchOrSquash_WithOriginShifting(IOperatorDto_VarSignal_WithDimension dto, string divideOrMultiplySymbol)
        {
            string factor = _stack.Pop();
            string sourcePos = GeneratePositionVariableNameCamelCase(dto);
            string destPos = GeneratePositionVariableNameCamelCase(dto, dto.DimensionStackLevel + 1);
            string origin = GenerateOriginVariableNameCamelCase();

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{destPos} = ({sourcePos} - {origin}) {divideOrMultiplySymbol} {factor} + {origin};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);
        }

        private void Process_StretchOrSquash_WithPhaseTracking(IOperatorDto_VarSignal_WithDimension dto, string divideOrMultiplySymbol)
        {
            string factor = _stack.Pop();
            string phase = GenerateLongLivedPhaseVariableNameCamelCase();
            string previousPosition = GeneratePreviousPositionVariableNameCamelCase();
            string sourcePosition = GeneratePositionVariableNameCamelCase(dto);
            string destPosition = GeneratePositionVariableNameCamelCase(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"{destPosition} = {phase} + ({sourcePosition} - {previousPosition}) {divideOrMultiplySymbol} {factor};");
            _sb.AppendLine($"{previousPosition} = {sourcePosition};");

            // I need two different variables for destPos and phase, because destPos is reused by different uses of the same stack level,
            // while phase needs to be uniquely used by the operator instance.
            _sb.AppendLine($"{phase} = {destPosition};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);
        }

        private OperatorDtoBase ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(IOperatorDto_WithDimension dto, Func<string, string> getRightHandFormulaDelegate)
        {
            string frequency = _stack.Pop();
            string position = GeneratePositionVariableNameCamelCase(dto);
            string output = GenerateOutputNameCamelCase(dto.OperatorTypeName);
            string rightHandFormula = getRightHandFormulaDelegate(output);

            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {output} = {position} * {frequency};");
            _sb.AppendLine($"{output} = {rightHandFormula};");
            _sb.AppendLine();

            _stack.Push(output);

            return (OperatorDtoBase)dto;
        }

        private OperatorDtoBase ProcessMultiplyOrDivide_ConstA_ConstB_VarOrigin(OperatorDtoBase_ConstA_ConstB_VarOrigin dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            ProcessNumber(dto.B);
            ProcessNumber(dto.A);

            return ProcessMultiplyOrDivideWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiplyOrDivide_ConstA_VarB_ConstOrigin(OperatorDtoBase_ConstA_VarB_ConstOrigin dto, string operatorSymbol)
        {
            ProcessNumber(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            ProcessNumber(dto.A);

            return ProcessMultiplyOrDivideWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiplyOrDivide_ConstA_VarB_VarOrigin(OperatorDtoBase_ConstA_VarB_VarOrigin dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            ProcessNumber(dto.A);

            return ProcessMultiplyOrDivideWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiplyOrDivide_VarA_ConstB_ConstOrigin(OperatorDtoBase_VarA_ConstB_ConstOrigin dto, string operatorSymbol)
        {
            ProcessNumber(dto.Origin);
            ProcessNumber(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessMultiplyOrDivideWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiplyOrDivide_VarA_ConstB_VarOrigin(OperatorDtoBase_VarA_ConstB_VarOrigin dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            ProcessNumber(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessMultiplyOrDivideWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiplyOrDivide_VarA_VarB_ConstOrigin(OperatorDtoBase_VarA_VarB_ConstOrigin dto, string operatorSymbol)
        {
            ProcessNumber(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessMultiplyOrDivideWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiplyOrDivide_VarA_VarB_VarOrigin(OperatorDtoBase_VarA_VarB_VarOrigin dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessMultiplyOrDivideWithOrigin(dto, operatorSymbol);
        }

        private OperatorDtoBase ProcessOriginShifter(OperatorDtoBase_ConstFrequency dto, Func<string, string> getRightHandFormulaDelegate)
        {
            ProcessNumber(dto.Frequency);

            string frequency = _stack.Pop();
            string position = GeneratePositionVariableNameCamelCase(dto);
            string origin = GenerateOriginVariableNameCamelCase();
            string variable = GenerateOutputNameCamelCase(dto.OperatorTypeName);
            string rightHandFormula = getRightHandFormulaDelegate(variable);
            
            _sb.AppendLine("// " + dto.OperatorTypeName);
            _sb.AppendLine($"double {variable} = ({position} - {origin}) * {frequency};");
            _sb.AppendLine($"{variable} = {rightHandFormula};");
            _sb.AppendLine();

            _stack.Push(variable);

            return dto;
        }

        private void Write_TriangleCode_AfterDeterminePhase(string variableNameInitializedWithPhase)
        {
            string x = variableNameInitializedWithPhase;

            _sb.AppendLine($"{x} = {x} + 0.25;");
            _sb.AppendLine($"{x} = {x} % 1.0;");
            _sb.AppendLine($"if ({x} < 0.5) {x} = -1.0 + 4.0 * {x};");
            _sb.AppendLine($"else {x} = 3.0 - 4.0 * {x};");
            _sb.AppendLine();
        }

        // Helpers

        private string GenerateOutputNameCamelCase(string operatorTypeName)
        {
            string canonicalOperatorTypeNameInCode = ToCanonicalNameInCode(operatorTypeName);

            int counter;
            if (!_camelCaseOperatorTypeName_To_VariableCounter_Dictionary.TryGetValue(canonicalOperatorTypeNameInCode, out counter))
            {
                counter = FIRST_VARIABLE_NUMBER;
            }

            string variableName = String.Format("{0}{1}", canonicalOperatorTypeNameInCode, counter++);

            _camelCaseOperatorTypeName_To_VariableCounter_Dictionary[canonicalOperatorTypeNameInCode] = counter;

            return variableName;
        }

        private string GetInputVariableNameCamelCase(VariableInput_OperatorDto dto)
        {
            string name;
            if (_variableInput_OperatorDto_To_VariableName_Dictionary.TryGetValue(dto, out name))
            {
                return name;
            }

            InputVariableInfo valueInfo = GenerateInputVariableInfo(dto);

            _variableInput_OperatorDto_To_VariableName_Dictionary[dto] = valueInfo.NameCamelCase;

            return valueInfo.NameCamelCase;
        }

        private InputVariableInfo GenerateInputVariableInfo(VariableInput_OperatorDto dto)
        {
            string variableName = String.Format("{0}{1}", INPUT_VARIABLE_PREFIX, _inputVariableCounter++);
            var valueInfo = new InputVariableInfo(variableName, dto.DimensionEnum, dto.ListIndex, dto.DefaultValue);

            _inputVariableInfoDictionary.Add(variableName, valueInfo);

            return valueInfo;
        }

        private string GenerateLongLivedPhaseVariableNameCamelCase()
        {
            string variableName = String.Format("{0}{1}", PHASE_VARIABLE_PREFIX, _phaseVariableCounter++);

            _phaseVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        private string GeneratePreviousPositionVariableNameCamelCase()
        {
            string variableName = String.Format("{0}{1}", PREVIOUS_POSITION_VARIABLE_PREFIX, _previousPositionVariableCounter++);

            _previousPositionVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        private string GeneratePositionVariableNameCamelCase(IOperatorDto_WithDimension dto, int? alternativeStackIndexLevel = null)
        {
            string variableName;
            if (dto.StandardDimensionEnum != DimensionEnum.Undefined)
            {
                string dimensionNameCamelCase = ToCanonicalNameInCode(dto.StandardDimensionEnum.ToString());
                variableName = string.Format("{0}{1}{2}", STANDARD_DIMENSION_VARIABLE_PREFIX, dimensionNameCamelCase, alternativeStackIndexLevel ?? dto.DimensionStackLevel);
            }
            else
            {
                string dimensionNameCamelCase = ToCanonicalNameInCode(dto.CustomDimensionName);
                variableName = string.Format("{0}{1}{2}", CUSTOM_DIMENSION_VARIABLE_PREFIX, dimensionNameCamelCase, alternativeStackIndexLevel ?? dto.DimensionStackLevel);
            }

            _positionVariableNamesCamelCaseHashSet.Add(variableName);

            return variableName;
        }

        private string GenerateOriginVariableNameCamelCase()
        {
            string variableName = String.Format("{0}{1}", ORIGIN_VARIABLE_PREFIX, _originVariableCounter++);

            _originVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        private static string ToCanonicalNameInCode(string name)
        {
            string convertedName = NameHelper.ToCanonical(name).ToCamelCase();

            // There is not clash with keywords because coincidentally a numbers is always
            // appended to the name, but if it becomes a problem, 
            // you could always add an underscore at the end to prevent any clash with keywords.
            //convertedName += '_';

            // HACK
            if (String.IsNullOrEmpty(convertedName))
            {
                convertedName = "u0000";
            }

            return convertedName;
        }
    }
}
