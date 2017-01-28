using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Visitors;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Framework.Common;
using System.Diagnostics;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Roslyn.Visitors
{
    internal class OperatorDtoToCSharpVisitor : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private const string TAB_STRING = "    ";

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

        private const string PHASE_MNEMONIC = "phase";
        private const string PREVIOUS_POSITION_MNEMONIC = "prevPos";
        private const string DEFAULT_INPUT_MNEMONIC = "input";
        private const string ORIGIN_MNEMONIC = "origin";

        /// <summary> {0} = phase </summary>
        private const string SAW_DOWN_FORMULA_FORMAT = "1.0 - (2.0 * {0} % 2.0)";
        /// <summary> {0} = phase </summary>
        private const string SAW_UP_FORMULA_FORMAT = "-1.0 + (2.0 * {0} % 2.0)";
        /// <summary> {0} = phase </summary>
        private const string SINE_FORMULA_FORMAT = "SineCalculator.Sin({0})";
        /// <summary> {0} = phase </summary>
        private const string SQUARE_FORMULA_FORMAT = "{0} % 1.0 < 0.5 ? 1.0 : -1.0";

        private readonly int _indentLevel;

        private Stack<string> _stack;
        private StringBuilderWithIndentation _sb;
        private int _counter;

        // Simple Sets of Variable Names

        /// <summary> HashSet for unicity. </summary>
        private HashSet<string> _positionVariableNamesCamelCaseHashSet;
        private IList<string> _longLivedPreviousPositionVariableNamesCamelCase;
        private IList<string> _longLivedPhaseVariableNamesCamelCase;
        private IList<string> _longLivedOriginVariableNamesCamelCase;
        private IList<string> _longLivedMiscVariableNamesCamelCase;

        // Information for Input Variables

        /// <summary> Dictionary for unicity. Key is variable name camel-case. </summary>
        private Dictionary<string, ExtendedVariableInfo> _variableName_To_InputVariableInfo_Dictionary;
        /// <summary> To maintain instance integrity of input variables when converting from DTO to C# code. </summary>
        private Dictionary<VariableInput_OperatorDto, string> _variableInput_OperatorDto_To_VariableName_Dictionary;

        // Information for Dimension Values

        private Dictionary<Tuple<DimensionEnum, string, int>, ExtendedVariableInfo> _dimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary;
        private Dictionary<Tuple<DimensionEnum, string>, string> _standardDimensionEnumAndCanonicalCustomDimensionName_To_Alias_Dictionary;

        public OperatorDtoToCSharpVisitor(int rawCalculationIndentLevel)
        {
            _indentLevel = rawCalculationIndentLevel;
        }

        public OperatorDtoToCSharpVisitorResult Execute(OperatorDtoBase dto)
        {
            _stack = new Stack<string>();
            _variableName_To_InputVariableInfo_Dictionary = new Dictionary<string, ExtendedVariableInfo>();
            _positionVariableNamesCamelCaseHashSet = new HashSet<string>();
            _longLivedPreviousPositionVariableNamesCamelCase = new List<string>();
            _longLivedPhaseVariableNamesCamelCase = new List<string>();
            _longLivedOriginVariableNamesCamelCase = new List<string>();
            _longLivedMiscVariableNamesCamelCase = new List<string>();
            _variableInput_OperatorDto_To_VariableName_Dictionary = new Dictionary<VariableInput_OperatorDto, string>();
            _standardDimensionEnumAndCanonicalCustomDimensionName_To_Alias_Dictionary = new Dictionary<Tuple<DimensionEnum, string>, string>();
            _dimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary = new Dictionary<Tuple<DimensionEnum, string, int>, ExtendedVariableInfo>();
            _counter = 0;

            _sb = new StringBuilderWithIndentation(TAB_STRING)
            {
                IndentLevel = _indentLevel
            };

            Visit_OperatorDto_Polymorphic(dto);

            string generatedCode = _sb.ToString();
            string returnValue = _stack.Pop();

            // Get some more variable info
            string firstTimeVariableNameCamelCase = GeneratePositionName(0, DimensionEnum.Time);

            IList<ExtendedVariableInfo> longLivedDimensionVariableInfos = 
                _dimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary.Values
                                                                                                  .Where(x => x.ListIndex == 0)
                                                                                                  .Except(x => string.Equals(x.VariableNameCamelCase, firstTimeVariableNameCamelCase))
                                                                                                  .ToArray();
            IList<string> localDimensionVariableNamesCamelCase =
                _dimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary.Values
                                                                                                  .Except(longLivedDimensionVariableInfos)
                                                                                                  .Select(x => x.VariableNameCamelCase)
                                                                                                  .ToArray();
            return new OperatorDtoToCSharpVisitorResult(
                generatedCode, 
                returnValue,
                firstTimeVariableNameCamelCase,
                _variableName_To_InputVariableInfo_Dictionary.Values.ToArray(),
                _positionVariableNamesCamelCaseHashSet.ToArray(),
                _longLivedPreviousPositionVariableNamesCamelCase,
                _longLivedPhaseVariableNamesCamelCase,
                _longLivedOriginVariableNamesCamelCase,
                longLivedDimensionVariableInfos,
                localDimensionVariableNamesCamelCase,
                _longLivedMiscVariableNamesCamelCase);
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
            string variable = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
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

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_AllVars(AllPassFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetAllPassFilterVariables));
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto_ManyConsts(AllPassFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
        {
            return ProcessLogicalBinaryOperator(dto, AND_SYMBOL);
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth(BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetBandPassFilterConstantPeakGainVariables));
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth(BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetBandPassFilterConstantTransitionGainVariables));
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth(BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
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

        protected override OperatorDtoBase Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            string position = GeneratePositionName(dto);

            _stack.Push(position);

            return dto;
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

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_AllVars(HighPassFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetHighPassFilterVariables));
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto_ManyConsts(HighPassFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_AllVars(HighShelfFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetHighShelfFilterVariables));
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto_ManyConsts(HighShelfFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
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

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_AllVars(LowPassFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetLowPassFilterVariables));
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto_ManyConsts(LowPassFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_AllVars(LowShelfFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetLowShelfFilterVariables));
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto_ManyConsts(LowShelfFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
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
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = -{x};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Not_OperatorDto_VarX(Not_OperatorDto_VarX dto)
        {
            Visit_OperatorDto_Polymorphic(dto.XOperatorDto);

            string x = _stack.Pop();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {x} == 0.0 ? 1.0 : 0.0;");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_AllVars(NotchFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetNotchFilterVariables));
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto_ManyConsts(NotchFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
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

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto_VarX(OneOverX_OperatorDto_VarX dto)
        {
            Visit_OperatorDto_Polymorphic(dto.XOperatorDto);

            string x = _stack.Pop();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = 1.0 / {x};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
        {
            return ProcessLogicalBinaryOperator(dto, OR_SYMBOL);
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_AllVars(PeakingEQFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetPeakingEQFilterVariables));
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto_ManyConsts(PeakingEQFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
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
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {@base} * {@base};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_Exponent3(Power_OperatorDto_VarBase_Exponent3 dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            string @base = _stack.Pop();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {@base} * {@base} * {@base};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto_VarBase_Exponent4(Power_OperatorDto_VarBase_Exponent4 dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            string @base = _stack.Pop();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {@base} * {@base};");
            _sb.AppendLine($"{output} *= {output};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting dto)
        {
            ProcessNumber(dto.Width);
            ProcessNumber(dto.Frequency);

            return Process_Pulse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting dto)
        {
            ProcessNumber(dto.Width);
            ProcessNumber(dto.Frequency);

            return Process_Pulse_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            ProcessNumber(dto.Frequency);

            return Process_Pulse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            ProcessNumber(dto.Frequency);

            return Process_Pulse_WithOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking dto)
        {
            ProcessNumber(dto.Width);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking dto)
        {
            ProcessNumber(dto.Width);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_WithPhaseTracking(dto);
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_ConstSpeed_NoOriginShifting(Reverse_OperatorDto_ConstSpeed_NoOriginShifting dto)
        {
            ProcessNumber(dto.Speed);

            return ProcessReverse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_ConstSpeed_WithOriginShifting(Reverse_OperatorDto_ConstSpeed_WithOriginShifting dto)
        {
            ProcessNumber(dto.Speed);

            string speed = _stack.Pop();
            string sourcePos = GeneratePositionName(dto);
            string destPos = GeneratePositionName(dto, dto.DimensionStackLevel + 1);
            string origin = GenerateLongLivedOriginName();

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{destPos} = ({sourcePos} - {origin}) * -{speed} + {origin};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);

            return dto;
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_VarSpeed_NoPhaseTracking(Reverse_OperatorDto_VarSpeed_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.SpeedOperatorDto);

            return ProcessReverse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto_VarSpeed_WithPhaseTracking(Reverse_OperatorDto_VarSpeed_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.SpeedOperatorDto);

            string speed = _stack.Pop();
            string phase = GenerateLongLivedPhaseName();
            string previousPosition = GenerateLongLivedPreviousPositionName();
            string sourcePosition = GeneratePositionName(dto);
            string destPosition = GeneratePositionName(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{destPosition} = {phase} + ({sourcePosition} - {previousPosition}) * -{speed};");
            _sb.AppendLine($"{previousPosition} = {sourcePosition};");

            // I need two different variables for destPos and phase, because destPos is reused by different uses of the same stack level,
            // while phase needs to be uniquely used by the operator instance.
            _sb.AppendLine($"{phase} = {destPosition};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);

            return dto;
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto)
        {
            // This DTO is not optimal for this kind of calculation engine. Do some optimization in-place here.
            MathPropertiesDto offsetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OffsetOperatorDto);

            if (offsetMathPropertiesDto.IsConstZero)
            {
                return ProcessRoundZeroOffset(dto, signalValue: dto.Signal, stepOperatorDto: dto.StepOperatorDto);
            }
            else
            {
                return ProcessRoundWithOffset(dto, signalValue: dto.Signal, stepOperatorDto: dto.StepOperatorDto, offsetOperatorDto: dto.OffsetOperatorDto);
            }
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto)
        {
            return ProcessRoundWithOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepValue: dto.Step, offsetValue: dto.Offset);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto)
        {
            return ProcessRoundWithOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepValue: dto.Step, offsetOperatorDto: dto.OffsetOperatorDto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto)
        {
            return ProcessRoundZeroOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepValue: dto.Step);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto)
        {
            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);

            string signal = _stack.Pop();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);
            string math = nameof(Math);
            string round = nameof(Math.Round);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {math}.{round}({signal}, MidpointRounding.AwayFromZero);");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto)
        {
            return ProcessRoundWithOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepOperatorDto: dto.StepOperatorDto, offsetValue: dto.Offset);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto)
        {
            return ProcessRoundWithOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepOperatorDto: dto.StepOperatorDto, offsetOperatorDto: dto.OffsetOperatorDto);
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto)
        {
            return ProcessRoundZeroOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepOperatorDto: dto.StepOperatorDto);
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

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto_VarPassThrough_ConstValue(SetDimension_OperatorDto_VarPassThrough_ConstValue dto)
        {
            return ProcessSetDimension(dto, value: dto.Value);
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto_VarPassThrough_VarValue(SetDimension_OperatorDto_VarPassThrough_VarValue dto)
        {
            return ProcessSetDimension(dto, valueOperatorDto: dto.ValueOperatorDto);
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

            Process_StretchOrSquash_WithOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_WithOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            ProcessNumber(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_WithOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_WithOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_ZeroOrigin(dto, MULTIPLY_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_ZeroOrigin(dto, MULTIPLY_SYMBOL);

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

            Process_StretchOrSquash_WithOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_WithOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            ProcessNumber(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_WithOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_WithOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            ProcessNumber(dto.Factor);

            Process_StretchOrSquash_ZeroOrigin(dto, DIVIDE_SYMBOL);

            return dto;
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            Process_StretchOrSquash_ZeroOrigin(dto, DIVIDE_SYMBOL);

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
            string position = GeneratePositionName(dto);
            string phase = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {phase} = {position} * {frequency};");

            string output = Write_TriangleCode_AfterDeterminePhase(phase);

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            ProcessNumber(dto.Frequency);

            string frequency = _stack.Pop();

            string position = GeneratePositionName(dto);
            string origin = GenerateLongLivedOriginName();
            string phase = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {phase} = ({position} - {origin}) * {frequency};");
            string output = Write_TriangleCode_AfterDeterminePhase(phase);

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            string frequency = _stack.Pop();
            string position = GeneratePositionName(dto);
            string phase = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {phase} = {position} * {frequency};");
            string output = Write_TriangleCode_AfterDeterminePhase(phase);

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            string frequency = _stack.Pop();

            string phase = GenerateLongLivedPhaseName();
            string posisition = GeneratePositionName(dto);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{phase} = {posisition} * {frequency};");
            // From here the code is the same as the method above.
            // TODO: You could prevent the first addition in the code written in the method called here,
            // by initializing phase with 0.5 for at the beginning of the chunk calculation.
            string output = Write_TriangleCode_AfterDeterminePhase(phase);

            _stack.Push(output);

            return dto;
        }

        protected override OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            string inputVariable = GetInputName(dto);

            _stack.Push(inputVariable);

            return dto;
        }

        // Generalized Methods

        private OperatorDtoBase ProcessBinaryOperator(OperatorDtoBase dto, string operatorSymbol)
        {
            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {a} {operatorSymbol} {b};");
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
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {a} {operatorSymbol} {b} ? 1.0 : 0.0;");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessDivideZeroOrigin(OperatorDtoBase dto)
        {
            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {a} / {b};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase Process_Filter_OperatorDto_AllVars(OperatorDtoBase_Filter_VarSignal dto, string biQuadFilterSetFilterVariablesMethodName)
        {
            Visit_OperatorDto_Base(dto);

            string signal = _stack.Pop();
            string frequency = _stack.Pop();

            IList<string> additionalFilterParameters = dto.InputOperatorDtos.Skip(2).Select(x => _stack.Pop()).ToArray();

            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            string x1 = GenerateUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x1)}");
            string x2 = GenerateUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x2)}");
            string y1 = GenerateUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y1)}");
            string y2 = GenerateUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y2)}");
            string a0 = GenerateUniqueVariableName($"{dto.OperatorTypeEnum}{nameof(a0)}");
            string a1 = GenerateUniqueVariableName($"{dto.OperatorTypeEnum}{nameof(a1)}");
            string a2 = GenerateUniqueVariableName($"{dto.OperatorTypeEnum}{nameof(a2)}");
            string a3 = GenerateUniqueVariableName($"{dto.OperatorTypeEnum}{nameof(a3)}");
            string a4 = GenerateUniqueVariableName($"{dto.OperatorTypeEnum}{nameof(a4)}");

            string nyquistFrequency = CompilationHelper.FormatValue(dto.NyquistFrequency);
            string samplingRate = CompilationHelper.FormatValue(dto.SamplingRate);
            string limitedFrequency = GenerateUniqueVariableName(nameof(limitedFrequency));
            const string biQuadFilterClassName = nameof(BiQuadFilterWithoutFields);
            string setFilterVariablesMethodName = biQuadFilterSetFilterVariablesMethodName;
            const string transformMethodName = nameof(BiQuadFilterWithoutFields.Transform);

            string concatinatedAdditionalFilterParameters = string.Join(", ", additionalFilterParameters);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {limitedFrequency} = {frequency};");
            _sb.AppendLine($"if ({limitedFrequency} > {nyquistFrequency}) {limitedFrequency} = {nyquistFrequency};");
            _sb.AppendLine();
            _sb.AppendLine($"double {a0}, {a1}, {a2}, {a3}, {a4};");
            _sb.AppendLine();
            _sb.AppendLine($"{biQuadFilterClassName}.{setFilterVariablesMethodName}(");
            _sb.Indent();
            {
                _sb.AppendLine($"{samplingRate}, {limitedFrequency}, {concatinatedAdditionalFilterParameters}, ");
                _sb.AppendLine($"out {a0}, out {a1}, out {a2}, out {a3}, out {a4});");
                _sb.Unindent();
            }
            _sb.AppendLine();
            _sb.AppendLine($"double {output} = {biQuadFilterClassName}.{transformMethodName}(");
            {
                _sb.Indent();
                _sb.AppendLine($"{signal}, {a0}, {a1}, {a2}, {a3}, {a4},");
                _sb.AppendLine($"ref {x1}, ref {x2}, ref {y1}, ref {y2});");
                _sb.Unindent();
            }
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase Process_Filter_OperatorDto_ManyConsts(OperatorDtoBase_Filter_ManyConsts dto)
        {
            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            string x1 = GenerateUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x1)}");
            string x2 = GenerateUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x2)}");
            string y1 = GenerateUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y1)}");
            string y2 = GenerateUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y2)}");
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            string a0 = CompilationHelper.FormatValue(dto.A0);
            string a1 = CompilationHelper.FormatValue(dto.A1);
            string a2 = CompilationHelper.FormatValue(dto.A2);
            string a3 = CompilationHelper.FormatValue(dto.A3);
            string a4 = CompilationHelper.FormatValue(dto.A4);

            const string biQuadFilterClassName = nameof(BiQuadFilterWithoutFields);
            const string transformMethodName = nameof(BiQuadFilterWithoutFields.Transform);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {biQuadFilterClassName}.{transformMethodName}(");
            {
                _sb.Indent();
                _sb.AppendLine($"{signal}, {a0}, {a1}, {a2}, {a3}, {a4},");
                _sb.AppendLine($"ref {x1}, ref {x2}, ref {y1}, ref {y2});");
                _sb.Unindent();
            }
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
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {a} != 0.0 {operatorSymbol} {b} != 0.0 ? 1.0 : 0.0;");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessMultiplyOrDivideWithOrigin(OperatorDtoBase dto, string operatorSymbol)
        {
            string a = _stack.Pop();
            string b = _stack.Pop();
            string origin = _stack.Pop();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = ({a} - {origin}) {operatorSymbol} {b} + {origin};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase Process_Math_Pow(OperatorDtoBase dto)
        {
            string @base = _stack.Pop();
            string exponent = _stack.Pop();
            string variable = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {variable} = Math.Pow({@base}, {exponent});");
            _sb.AppendLine();

            _stack.Push(variable);

            return dto;
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

        private OperatorDtoBase ProcessMultiVarOperator_Vars_NoConsts(OperatorDtoBase_Vars dto, string operatorSymbol)
        {
            dto.Vars.ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return ProcessMultiVarOperator(dto, dto.Vars.Count, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiVarOperator_Vars_1Const(OperatorDtoBase_Vars_1Const dto, string operatorSymbol)
        {
            ProcessNumber(dto.ConstValue);
            dto.Vars.ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return ProcessMultiVarOperator(dto, dto.Vars.Count + 1, operatorSymbol);
        }

        private OperatorDtoBase ProcessMultiVarOperator(OperatorDtoBase dto, int varCount, string operatorSymbol)
        {
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");

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
            _sb.AppendLine($"// {dto.OperatorTypeEnum}");

            ProcessNumber(dto.Number);

            _sb.AppendLine();

            return dto;
        }

        private OperatorDtoBase ProcessOriginShifter(OperatorDtoBase_ConstFrequency dto, Func<string, string> getRightHandFormulaDelegate)
        {
            ProcessNumber(dto.Frequency);

            string frequency = _stack.Pop();
            string position = GeneratePositionName(dto);
            string origin = GenerateLongLivedOriginName();
            string variable = GenerateUniqueVariableName(dto.OperatorTypeEnum);
            string rightHandFormula = getRightHandFormulaDelegate(variable);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {variable} = ({position} - {origin}) * {frequency};");
            _sb.AppendLine($"{variable} = {rightHandFormula};");
            _sb.AppendLine();

            _stack.Push(variable);

            return dto;
        }

        private OperatorDtoBase ProcessPhaseTracker(OperatorDtoBase_VarFrequency dto, Func<string, string> getRightHandFormulaDelegate)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            string frequency = _stack.Pop();
            string phase = GenerateLongLivedPhaseName();
            string position = GeneratePositionName(dto);
            string previousPosition = GenerateLongLivedPreviousPositionName();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);
            string rightHandFormula = getRightHandFormulaDelegate(phase);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{phase} += ({position} - {previousPosition}) * {frequency};");
            _sb.AppendLine($"{previousPosition} = {position};");
            _sb.AppendLine($"double {output} = {rightHandFormula};");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase Process_Pulse_WithPhaseTracking(OperatorDtoBase_VarFrequency dto)
        {
            string frequency = _stack.Pop();
            string width = _stack.Pop();
            string phase = GenerateLongLivedPhaseName();
            string position = GeneratePositionName(dto);
            string previousPosition = GenerateLongLivedPreviousPositionName();
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{phase} += ({position} - {previousPosition}) * {frequency};");
            _sb.AppendLine($"{previousPosition} = {position};");
            _sb.AppendLine($"double {output} = {phase} % 1.0 < {width} ? 1.0 : -1.0;");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase Process_Pulse_WithOriginShifting(OperatorDtoBase_ConstFrequency dto)
        {
            string frequency = _stack.Pop();
            string width = _stack.Pop();
            string position = GeneratePositionName(dto);
            string origin = GenerateLongLivedOriginName();
            string variable = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {variable} = ({position} - {origin}) * {frequency};");
            _sb.AppendLine($"{variable} = {variable} % 1.0 < {width} ? 1.0 : -1.0;");
            _sb.AppendLine();

            _stack.Push(variable);

            return dto;
        }

        private OperatorDtoBase Process_Pulse_NoPhaseTrackingOrOriginShifting(IOperatorDto_WithDimension dto)
        {
            string frequency = _stack.Pop();
            string width = _stack.Pop();
            string position = GeneratePositionName(dto);
            string variable = GenerateUniqueVariableName(dto.OperatorTypeEnum);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {variable} = {position} * {frequency};");
            _sb.AppendLine($"{variable} = {variable} % 1.0 < {width} ? 1.0 : -1.0;");
            _sb.AppendLine();

            _stack.Push(variable);

            return (OperatorDtoBase)dto;
        }

        private OperatorDtoBase ProcessReverse_NoPhaseTrackingOrOriginShifting(IOperatorDto_VarSignal_WithDimension dto)
        {
            string speed = _stack.Pop();
            string sourcePos = GeneratePositionName(dto);
            string destPos = GeneratePositionName(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{destPos} = {sourcePos} * -{speed};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);

            return (OperatorDtoBase)dto;
        }

        private OperatorDtoBase ProcessRoundWithOffset(
            OperatorDtoBase dto,
            OperatorDtoBase signalOperatorDto = null,
            double? signalValue = null,
            OperatorDtoBase stepOperatorDto = null,
            double? stepValue = null,
            OperatorDtoBase offsetOperatorDto = null,
            double? offsetValue = null)
        {
            string signal = GetLiteralFromOperatorDtoOrValue(signalOperatorDto, signalValue);
            string step = GetLiteralFromOperatorDtoOrValue(stepOperatorDto, stepValue);
            string offset = GetLiteralFromOperatorDtoOrValue(offsetOperatorDto, offsetValue);
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);
            string mathHelper = nameof(MathHelper);
            string roundWithStep = nameof(MathHelper.RoundWithStep);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {mathHelper}.{roundWithStep}({signal}, {step}, {offset});");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessRoundZeroOffset(
            OperatorDtoBase dto,
            OperatorDtoBase signalOperatorDto = null,
            double? signalValue = null,
            OperatorDtoBase stepOperatorDto = null,
            double? stepValue = null)
        {
            string signal = GetLiteralFromOperatorDtoOrValue(signalOperatorDto, signalValue);
            string step = GetLiteralFromOperatorDtoOrValue(stepOperatorDto, stepValue);
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);
            string mathHelper = nameof(MathHelper);
            string roundWithStep = nameof(MathHelper.RoundWithStep);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {mathHelper}.{roundWithStep}({signal}, {step});");
            _sb.AppendLine();

            _stack.Push(output);

            return dto;
        }

        private OperatorDtoBase ProcessSetDimension(IOperatorDto_VarSignal_WithDimension dto, OperatorDtoBase valueOperatorDto = null, double? value = null)
        {
            // Do not call base: Base will visit the inlets in one blow. We need to visit the inlets one by one.

            string valueLiteral = GetLiteralFromOperatorDtoOrValue(valueOperatorDto, value);
            string position = GeneratePositionName(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{position} = {valueLiteral};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            _stack.Push(signal);

            return (OperatorDtoBase)dto; // Dirty. Refactor away if IOperatorDtoBase is the deepest base type.
        }

        private OperatorDtoBase ProcessShift(IOperatorDto_VarSignal_WithDimension dto, OperatorDtoBase distanceOperatorDto = null, double? distance = null)
        {
            // Do not call base: Base will visit the inlets in one blow. We need to visit the inlets one by one.

            string distanceLiteral = GetLiteralFromOperatorDtoOrValue(distanceOperatorDto, distance);
            string sourcePos = GeneratePositionName(dto);
            string destPos = GeneratePositionName(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{destPos} = {sourcePos} {PLUS_SYMBOL} {distanceLiteral};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            _stack.Push(signal);

            return (OperatorDtoBase)dto; // Dirty. Refactor away if IOperatorDtoBase is the deepest base type.
        }

        private void Process_StretchOrSquash_WithOrigin(IOperatorDto_VarSignal_WithDimension dto, string divideOrMultiplySymbol)
        {
            string factor = _stack.Pop();
            string origin = _stack.Pop();
            string sourcePos = GeneratePositionName(dto);
            string destPos = GeneratePositionName(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{destPos} = ({sourcePos} - {origin}) {divideOrMultiplySymbol} {factor} + {origin};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);
        }

        private void Process_StretchOrSquash_ZeroOrigin(IOperatorDto_VarSignal_WithDimension dto, string divideOrMultiplySymbol)
        {
            string factor = _stack.Pop();
            string sourcePos = GeneratePositionName(dto);
            string destPos = GeneratePositionName(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{destPos} = {sourcePos} {divideOrMultiplySymbol} {factor};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);
        }

        private void Process_StretchOrSquash_WithOriginShifting(IOperatorDto_VarSignal_WithDimension dto, string divideOrMultiplySymbol)
        {
            string factor = _stack.Pop();
            string sourcePos = GeneratePositionName(dto);
            string destPos = GeneratePositionName(dto, dto.DimensionStackLevel + 1);
            string origin = GenerateLongLivedOriginName();

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"{destPos} = ({sourcePos} - {origin}) {divideOrMultiplySymbol} {factor} + {origin};");
            _sb.AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();
            _stack.Push(signal);
        }

        private void Process_StretchOrSquash_WithPhaseTracking(IOperatorDto_VarSignal_WithDimension dto, string divideOrMultiplySymbol)
        {
            string factor = _stack.Pop();
            string phase = GenerateLongLivedPhaseName();
            string previousPosition = GenerateLongLivedPreviousPositionName();
            string sourcePosition = GeneratePositionName(dto);
            string destPosition = GeneratePositionName(dto, dto.DimensionStackLevel + 1);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
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
            string position = GeneratePositionName(dto);
            string output = GenerateUniqueVariableName(dto.OperatorTypeEnum);
            string rightHandFormula = getRightHandFormulaDelegate(output);

            _sb.AppendLine($"// {dto.OperatorTypeEnum}");
            _sb.AppendLine($"double {output} = {position} * {frequency};");
            _sb.AppendLine($"{output} = {rightHandFormula};");
            _sb.AppendLine();

            _stack.Push(output);

            return (OperatorDtoBase)dto;
        }

        /// <summary> Returns output variable name. </summary>
        private string Write_TriangleCode_AfterDeterminePhase(string phase)
        {
            string shiftedPhase = GenerateUniqueVariableName(nameof(shiftedPhase));
            string relativePhase = GenerateUniqueVariableName(nameof(relativePhase));
            string output = GenerateUniqueVariableName(OperatorTypeEnum.Triangle);

            // Correct the phase shift, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            _sb.AppendLine($"double {shiftedPhase} = {phase} + 0.25;");
            _sb.AppendLine($"double {relativePhase} = {shiftedPhase} % 1.0;");
            _sb.AppendLine($"double {output};");
            // Starts going up at a rate of 2 up over 1/2 a cycle.
            _sb.AppendLine($"if ({relativePhase} < 0.5) {output} = -1.0 + 4.0 * {relativePhase};");
            // And then going down at phase 1/2.
            // (Extending the line to x = 0 it ends up at y = 3.)
            _sb.AppendLine($"else {output} = 3.0 - 4.0 * {relativePhase};");
            _sb.AppendLine();

            return output;
        }

        // Helpers

        private string Convert_DisplayName_To_NonUniqueNameInCode_WithoutUnderscores(string arbitraryString)
        {
            string convertedName = NameHelper.ToCanonical(arbitraryString).ToCamelCase().Replace("_", "");
            return convertedName;
        }

        private ExtendedVariableInfo GenerateInputVariableInfo(VariableInput_OperatorDto dto)
        {
            object mnemonic;
            if (dto.DimensionEnum != DimensionEnum.Undefined)
            {
                mnemonic = dto.DimensionEnum;
            }
            else if (!String.IsNullOrEmpty(dto.CanonicalName))
            {
                mnemonic = dto.CanonicalName;
            }
            else
            {
                mnemonic = DEFAULT_INPUT_MNEMONIC;
            }

            string variableName = GenerateUniqueVariableName(mnemonic);

            var valueInfo = new ExtendedVariableInfo(variableName, dto.CanonicalName, dto.DimensionEnum, dto.ListIndex, dto.DefaultValue);

            _variableName_To_InputVariableInfo_Dictionary.Add(variableName, valueInfo);

            return valueInfo;
        }

        private string GeneratePositionName(IOperatorDto_WithDimension dto, int? alternativeStackIndexLevel = null)
        {
            string canonicalCustomDimensionName = dto.CanonicalCustomDimensionName;
            DimensionEnum standardDimensionEnum = dto.StandardDimensionEnum;
            int stackLevel = alternativeStackIndexLevel ?? dto.DimensionStackLevel;

            return GeneratePositionName(stackLevel, standardDimensionEnum, canonicalCustomDimensionName);
        }

        private string GeneratePositionName(int stackLevel, DimensionEnum standardDimensionEnum = DimensionEnum.Undefined, string canonicalCustomDimensionName = "")
        {
            // Get DimensionAlias
            string dimensionAlias = GetDimensionAlias(standardDimensionEnum, canonicalCustomDimensionName);

            // Format PositionVariableNAme
            string positionVariableName = $"{dimensionAlias}_{stackLevel}";
            _positionVariableNamesCamelCaseHashSet.Add(positionVariableName);

            // Manage Dictionary with Dimension Info
            var key = new Tuple<DimensionEnum, string, int>(standardDimensionEnum, canonicalCustomDimensionName, stackLevel);
            ExtendedVariableInfo variableInfo;
            if (!_dimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary.TryGetValue(key, out variableInfo))
            {
                variableInfo = new ExtendedVariableInfo(positionVariableName, canonicalCustomDimensionName, standardDimensionEnum, stackLevel, defaultValue: null);

                _dimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary[key] = variableInfo;
            }

            return positionVariableName;
        }

        /// <summary>
        /// Formats the dimension into a string that is close to the dimension name + a unique character sequence.
        /// E.g.: "prettiness_1"
        /// </summary>
        private string GetDimensionAlias(DimensionEnum dimensionEnum, string canonicalCustomDimensionName)
        {
            var key = new Tuple<DimensionEnum, string>(dimensionEnum, canonicalCustomDimensionName);
            string alias;
            if (!_standardDimensionEnumAndCanonicalCustomDimensionName_To_Alias_Dictionary.TryGetValue(key, out alias))
            {
                object mnemonic;
                if (dimensionEnum != DimensionEnum.Undefined)
                {
                    mnemonic = dimensionEnum;
                }
                else
                {
                    mnemonic = canonicalCustomDimensionName;
                }
                alias = GenerateUniqueDimensionAlias(mnemonic);

                _standardDimensionEnumAndCanonicalCustomDimensionName_To_Alias_Dictionary[key] = alias;
            }
            return alias;
        }

        private string GenerateUniqueLongLivedVariableName(object mnemonic)
        {
            string variableName = GenerateUniqueVariableName(mnemonic);

            _longLivedMiscVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        private string GenerateLongLivedPhaseName()
        {
            string variableName = GenerateUniqueVariableName(PHASE_MNEMONIC);

            _longLivedPhaseVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        private string GenerateLongLivedPreviousPositionName()
        {
            string variableName = GenerateUniqueVariableName(PREVIOUS_POSITION_MNEMONIC);

            _longLivedPreviousPositionVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        private string GenerateLongLivedOriginName()
        {
            string variableName = GenerateUniqueVariableName(ORIGIN_MNEMONIC);

            _longLivedOriginVariableNamesCamelCase.Add(variableName);

            return variableName;
        }

        /// <param name="mnemonic">
        /// Will be incorporated into the variable name. It will be converted to string.
        /// It will also be put into a (non-unique) form that will be valid in C#.
        /// Also underscores are removed from it, because that is a separator character in our variable names.
        /// </param>
        private string GenerateUniqueVariableName(object mnemonic)
        {
            string nonUniqueNameInCode = Convert_DisplayName_To_NonUniqueNameInCode_WithoutUnderscores(Convert.ToString(mnemonic));
            int uniqueNumber = GenerateUniqueNumber();

            string variableName = $"{nonUniqueNameInCode}_{uniqueNumber}";
            return variableName;
        }

        private string GenerateUniqueDimensionAlias(object mnemonic)
        {
            string nonUniqueNameInCode = Convert_DisplayName_To_NonUniqueNameInCode_WithoutUnderscores(Convert.ToString(mnemonic));
            string uniqueLetterSequence = GenerateUniqueLetterSequence();

            string variableName = $"{nonUniqueNameInCode}_{uniqueLetterSequence}";
            return variableName;
        }

        private int GenerateUniqueNumber()
        {
            return _counter++;
        }

        private string GenerateUniqueLetterSequence()
        {
            return NumberingSystems.ToLetterSequence(_counter++, firstChar: 'a', lastChar: 'z');
        }

        private string GetInputName(VariableInput_OperatorDto dto)
        {
            string name;
            if (_variableInput_OperatorDto_To_VariableName_Dictionary.TryGetValue(dto, out name))
            {
                return name;
            }

            ExtendedVariableInfo inputVariableInfo = GenerateInputVariableInfo(dto);

            _variableInput_OperatorDto_To_VariableName_Dictionary[dto] = inputVariableInfo.VariableNameCamelCase;

            return inputVariableInfo.VariableNameCamelCase;
        }

        private string GetLiteralFromOperatorDtoOrValue(OperatorDtoBase valueOperatorDto = null, double? value = null)
        {
            bool xor = valueOperatorDto != null ^ value.HasValue;
            if (!xor)
            {
                throw new Exception($"Either {nameof(value)} or {nameof(valueOperatorDto)} must be filled in, but not both at the same time.");
            }

            if (valueOperatorDto != null)
            {
                Visit_OperatorDto_Polymorphic(valueOperatorDto);
            }
            else
            {
                ProcessNumber(value.Value);
            }

            string literal = _stack.Pop();
            return literal;
        }
    }
}
