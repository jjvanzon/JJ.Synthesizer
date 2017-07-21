using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Business.Synthesizer.Visitors;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
// Class alias to prevent accidentally use other things from JJ.Framework.Mathematics, 
// for which the copy from JJ.Business.Synthesizer.CopiedCode.FromFramework should be used.
using NumberingSystems = JJ.Framework.Mathematics.NumberingSystems;
using JJ.Framework.Configuration;

namespace JJ.Business.Synthesizer.Roslyn
{
    internal class OperatorDtoToRawCSharpVisitor : OperatorDtoVisitorBase_AfterCodeGenerationSimplification
    {
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum MinOrMaxEnum
        {
            Undefined,
            Min,
            Max
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum StretchOrSquashEnum
        {
            Undefined,
            Stretch,
            Squash
        }

        private const string TAB_STRING = "    ";
        private const int METHOD_SIGNATURE_INDENT_LEVEL = 2;
        private const int MULTI_LINE_PARAMETER_LIST_INDENT_LEVEL = 3;

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

        private const string ANONYMOUS_MNEMONIC = "anynomous";
        private const string ARRAY_CALCULATOR_MNEMONIC = "arraycalculator";
        private const string ARRAY_MNEMONIC = "array";
        private const string CALCULATE_MNEMONIC = "calculate";
        private const string DEFAULT_INPUT_MNEMONIC = "input";
        private const string OFFSET_MNEMONIC = "offset";
        private const string ORIGIN_MNEMONIC = "origin";
        private const string PHASE_MNEMONIC = "phase";
        private const string PREVIOUS_POSITION_MNEMONIC = "prevpos";
        private const string RATE_MNEMONIC = "rate";
        private const string RESET_MNEMONIC = "reset";

        /// <summary> {0} = phase </summary>
        private const string SAW_DOWN_FORMULA_FORMAT = "1.0 - (2.0 * {0} % 2.0)";

        /// <summary> {0} = phase </summary>
        private const string SAW_UP_FORMULA_FORMAT = "-1.0 + (2.0 * {0} % 2.0)";

        /// <summary> {0} = phase </summary>
        private const string SINE_FORMULA_FORMAT = "SineCalculator.Sin({0})";

        /// <summary> {0} = phase </summary>
        private const string SQUARE_FORMULA_FORMAT = "{0} % 1.0 < 0.5 ? 1.0 : -1.0";

        private const double SAMPLE_BASE_FREQUENCY = 440.0;

        private static readonly CalculationMethodEnum _calculationMethodEnum = CustomConfigurationManager.GetSection<ConfigurationSection>().CalculationMethod;

        private readonly int _channelIndex;
        private readonly int _calculationIndentLevel;
        private readonly int _resetIndentLevel;

        private Stack<string> _stack;
        private StringBuilderWithIndentation _stringBuilderForCalculate;
        private StringBuilderWithIndentation _stringBuilderForReset;
        private int _counter;
        private Stack<bool> _holdOperatorIsActiveStack;

        private VariableCollections _variableInfo;

        // We need botha list and a stack. A stack for where we are in the processing,
        // a list to not lose generated methods, when they are popped from the stack.

        private Stack<GeneratedMethodInfo> _generatedMethodInfoStack;
        private Dictionary<int, GeneratedMethodInfo> _operatorID_To_GeneratedMethodInfo_Dictionary;

        public OperatorDtoToRawCSharpVisitor(int channelIndex, int calculationIndentLevel, int resetIndentLevel)
        {
            _channelIndex = channelIndex;
            _calculationIndentLevel = calculationIndentLevel;
            _resetIndentLevel = resetIndentLevel;
            _variableInfo = new VariableCollections();
        }

        public OperatorDtoToCSharpVisitorResult Execute(IOperatorDto dto)
        {
            _stack = new Stack<string>();
            _variableInfo = new VariableCollections();
            _generatedMethodInfoStack = new Stack<GeneratedMethodInfo>();
            _operatorID_To_GeneratedMethodInfo_Dictionary = new Dictionary<int, GeneratedMethodInfo>();
            _counter = 0;
            _holdOperatorIsActiveStack = new Stack<bool>();
            _holdOperatorIsActiveStack.Push(false);

            _stringBuilderForCalculate = new StringBuilderWithIndentation(TAB_STRING)
            {
                IndentLevel = _calculationIndentLevel
            };

            _stringBuilderForReset = new StringBuilderWithIndentation(TAB_STRING)
            {
                IndentLevel = _resetIndentLevel
            };

            AppendInitializeDimensionValues();

            Visit_OperatorDto_Polymorphic(dto);

            // Gather up output
            string rawCalculationCode = _stringBuilderForCalculate.ToString();
            string rawResetCode = _stringBuilderForReset.ToString();
            string returnValueLiteral = _stack.Pop();

            // Get some more variable info
            string firstTimeVariableNameCamelCase = GetPositionNameCamelCase(0, DimensionEnum.Time);

            IList<ExtendedVariableInfo> longLivedDimensionVariableInfos =
                _variableInfo.DimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary.Values
                             .Where(x => x.Position == 0)
                             .Except(x => string.Equals(x.VariableNameCamelCase, firstTimeVariableNameCamelCase))
                             .ToArray();

            IList<string> locallyReusedDoubleVariableNamesCamelCase =
                _variableInfo.DimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary.Values
                             .Except(longLivedDimensionVariableInfos)
                             .Select(x => x.VariableNameCamelCase)
                             .ToArray();

            IList<ExtendedVariableInfo> inputVariableInfos = _variableInfo.VariableName_To_InputVariableInfo_Dictionary.Values.ToArray();
            IList<ArrayCalculationInfo> arrayCalculationInfos = _variableInfo.ArrayDto_To_ArrayCalculationInfo_Dictionary.Values.ToArray();

            // ReSharper disable once InvokeAsExtensionMethod
            IList<string> calculationMethodCodeList = _operatorID_To_GeneratedMethodInfo_Dictionary.Values.Select(x => x.MethodCodeForCalculate).ToArray();
            IList<string> resetMethodCodeList = _operatorID_To_GeneratedMethodInfo_Dictionary.Values.Select(x => x.MethodCodeForReset).ToArray();

            return new OperatorDtoToCSharpVisitorResult(
                rawCalculationCode,
                rawResetCode,
                returnValueLiteral,
                firstTimeVariableNameCamelCase,
                _variableInfo.LongLivedDoubleVariableNamesCamelCase,
                locallyReusedDoubleVariableNamesCamelCase,
                inputVariableInfos,
                longLivedDimensionVariableInfos,
                arrayCalculationInfos,
                _variableInfo.LongLivedDoubleArrayVariableInfos,
                calculationMethodCodeList,
                resetMethodCodeList);
        }

        private void AppendInitializeDimensionValues()
        {
            AppendLineToReset("// Initialize Dimensions Values");

            string channel = GetPositionNameCamelCase(0, DimensionEnum.Channel);
            AppendLineToReset($"{channel} = {_channelIndex};");

            AppendLineToReset();
        }

        [DebuggerHidden]
        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }

        protected override IOperatorDto Visit_Absolute_OperatorDto_VarNumber(Absolute_OperatorDto_VarNumber dto)
        {
            Visit_OperatorDto_Polymorphic(dto.NumberOperatorDto);
            string number = _stack.Pop();

            string output = GetLocalOutputName(dto);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {number};");
            AppendLine($"if ({output} < 0.0) {output} = -{output};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            return ProcessMultiVarOperator_Vars_1Const(dto, PLUS_SYMBOL);
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessMultiVarOperator_Vars_NoConsts(dto, PLUS_SYMBOL);
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_AllVars(AllPassFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetAllPassFilterVariables));
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_ManyConsts(AllPassFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
        {
            return ProcessLogicalBinaryOperator(dto, AND_SYMBOL);
        }

        protected override IOperatorDto Visit_AverageFollower_OperatorDto_AllVars(AverageFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(
            AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(
            AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto_Vars(AverageOverInlets_OperatorDto_Vars dto)
        {
            dto.Vars.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));

            AppendOperatorTitleComment(dto);

            string sum = GetUniqueLocalVariableName(nameof(sum));
            string output = GetLocalOutputName(dto);
            int count = dto.Vars.Count;

            AppendTabs();
            Append($"double {sum} =");

            for (int i = 0; i < count; i++)
            {
                string value = _stack.Pop();

                Append(' ');
                Append(value);

                bool isLast = i == count - 1;
                if (isLast)
                {
                    break;
                }

                Append(" +");
            }

            Append(';');
            Append(Environment.NewLine);

            string countLiteral = CompilationHelper.FormatValue(count);

            AppendLine($"double {output} = {sum} / {countLiteral};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth(
            BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth(
            BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetBandPassFilterConstantPeakGainVariables));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth(
            BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth(
            BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetBandPassFilterConstantTransitionGainVariables));
        }

        protected override IOperatorDto Visit_BooleanToDouble_OperatorDto(BooleanToDouble_OperatorDto dto)
        {
            return Process_BooleanToDouble(dto, dto.InputOperatorDto, null);
        }

        protected override IOperatorDto Visit_BooleanToDouble_OperatorDto_ConstInput(BooleanToDouble_OperatorDto_ConstInput dto)
        {
            return Process_BooleanToDouble(dto, null, dto.Input);
        }

        protected override IOperatorDto Visit_BooleanToDouble_OperatorDto_VarInput(BooleanToDouble_OperatorDto_VarInput dto)
        {
            return Process_BooleanToDouble(dto, dto.InputOperatorDto, null);
        }

        private IOperatorDto Process_BooleanToDouble(IOperatorDto dto, IOperatorDto inputOperatorDto, double? inputValue)
        {
            string input = GetLiteralFromOperatorDtoOrValue(inputOperatorDto, inputValue);
            string output = GetLocalOutputName(dto);

            AppendOperatorTitleComment(dto);
            AppendLine($"double {output} = {input} ? 1.0 : 0.0;");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Block(Cache_OperatorDto_MultiChannel_Block dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Cubic(Cache_OperatorDto_MultiChannel_Cubic dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Hermite(Cache_OperatorDto_MultiChannel_Hermite dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Line(Cache_OperatorDto_MultiChannel_Line dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_MultiChannel_Stripe(Cache_OperatorDto_MultiChannel_Stripe dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Block(Cache_OperatorDto_SingleChannel_Block dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Cubic(Cache_OperatorDto_SingleChannel_Cubic dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Hermite(Cache_OperatorDto_SingleChannel_Hermite dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Line(Cache_OperatorDto_SingleChannel_Line dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Cache_OperatorDto_SingleChannel_Stripe(Cache_OperatorDto_SingleChannel_Stripe dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto_VarPassThrough_VarReset(ChangeTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous(
            ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset(
            ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous(
            ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset(
            ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_2ConstItems(ClosestOverInlets_OperatorDto_VarInput_2ConstItems dto)
        {
            PutNumberOnStack(dto.Item2);
            PutNumberOnStack(dto.Item1);
            Visit_OperatorDto_Polymorphic(dto.InputOperatorDto);

            return Process_ClosestOverInlets(dto, varCount: 2);
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
        {
            dto.Items.Reverse().ForEach(x => PutNumberOnStack(x));
            Visit_OperatorDto_Polymorphic(dto.InputOperatorDto);

            return Process_ClosestOverInlets(dto, dto.Items.Count);
        }

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_VarItems(ClosestOverInlets_OperatorDto_VarInput_VarItems dto)
        {
            dto.ItemOperatorDtos.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));
            Visit_OperatorDto_Polymorphic(dto.InputOperatorDto);

            return Process_ClosestOverInlets(dto, dto.ItemOperatorDtos.Count);
        }

        private IOperatorDto Process_ClosestOverInlets(IOperatorDto dto, int varCount)
        {
            AppendOperatorTitleComment(dto);

            string input = _stack.Pop();
            string firstItem = _stack.Pop();
            string smallestDistance = GetUniqueLocalVariableName(nameof(smallestDistance));
            string closestItem = GetUniqueLocalVariableName(nameof(closestItem));
            string output = GetLocalOutputName(dto);
            const string geometry = nameof(Geometry);
            const string absoluteDistance = nameof(Geometry.AbsoluteDistance);

            AppendLine($"double {smallestDistance} = {geometry}.{absoluteDistance}({input}, {firstItem});");
            AppendLine($"double {closestItem} = {firstItem};");
            AppendLine();

            // NOTE: i = 1.
            for (int i = 1; i < varCount; i++)
            {
                string item = _stack.Pop();
                string distance = GetUniqueLocalVariableName(nameof(distance));

                AppendLine($"double {distance} = {geometry}.{absoluteDistance}({input}, {item});");

                AppendLine($"if ({smallestDistance} > {distance})");
                AppendLine("{");
                Indent();
                {
                    AppendLine($"{smallestDistance} = {distance};");
                    AppendLine($"{closestItem} = {item};");
                    Unindent();
                }
                AppendLine("}");
                AppendLine();
            }

            AppendLine($"double {output} = {closestItem};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems dto)
        {
            PutNumberOnStack(dto.Item2);
            PutNumberOnStack(dto.Item1);
            Visit_OperatorDto_Polymorphic(dto.InputOperatorDto);

            return Process_ClosestOverInletsExp(dto, varCount: 2);
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
        {
            dto.Items.Reverse().ForEach(x => PutNumberOnStack(x));
            Visit_OperatorDto_Polymorphic(dto.InputOperatorDto);

            return Process_ClosestOverInletsExp(dto, dto.Items.Count);
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_VarItems(ClosestOverInletsExp_OperatorDto_VarInput_VarItems dto)
        {
            dto.ItemOperatorDtos.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));
            Visit_OperatorDto_Polymorphic(dto.InputOperatorDto);

            return Process_ClosestOverInletsExp(dto, dto.ItemOperatorDtos.Count);
        }

        private IOperatorDto Process_ClosestOverInletsExp(IOperatorDto dto, int varCount)
        {
            string input = _stack.Pop();
            string firstItem = _stack.Pop();

            AppendOperatorTitleComment(dto);

            string smallestDistance = GetUniqueLocalVariableName(nameof(smallestDistance));
            string closestItem = GetUniqueLocalVariableName(nameof(closestItem));
            string output = GetLocalOutputName(dto);
            string logInput = GetUniqueLocalVariableName(nameof(logInput));
            const string geometry = nameof(Geometry);
            const string absoluteDistance = nameof(Geometry.AbsoluteDistance);

            AppendLine($"double {logInput} = Math.Log({input});");
            AppendLine();

            AppendLine($"double {smallestDistance} = {geometry}.{absoluteDistance}({logInput}, Math.Log({firstItem}));");
            AppendLine($"double {closestItem} = {firstItem};");
            AppendLine();

            for (int i = 1; i < varCount; i++)
            {
                string item = _stack.Pop();
                string distance = GetUniqueLocalVariableName(nameof(distance));

                AppendLine($"double {distance} = {geometry}.{absoluteDistance}({logInput}, Math.Log({item}));");

                AppendLine($"if ({smallestDistance} > {distance})");
                AppendLine("{");
                Indent();
                {

                    AppendLine($"{smallestDistance} = {distance};");
                    AppendLine($"{closestItem} = {item};");
                    Unindent();
                }
                AppendLine("}");
                AppendLine();
            }

            AppendLine($"double {output} = {closestItem};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto)
        {
            return ProcessCurve_NoOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto)
        {
            return ProcessCurve_WithOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto)
        {
            return ProcessCurve_NoOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto)
        {
            return ProcessCurve_WithOriginShifting(dto);
        }

        private IOperatorDto ProcessCurve_NoOriginShifting(Curve_OperatorDtoBase_WithoutMinX dto)
        {
            AppendOperatorTitleComment(dto);

            string calculatorName = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDto);
            string output = GetLocalOutputName(dto);
            string position = GetPositionNameCamelCase(dto);

            AppendLine($"double {output} = {calculatorName}.Calculate({position});");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessCurve_WithOriginShifting(Curve_OperatorDtoBase_WithoutMinX dto)
        {
            string calculatorName = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDto);
            string output = GetLocalOutputName(dto);
            string defaultRate = CompilationHelper.FormatValue(1.0);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithOriginShifting(dto, defaultRate);
            AppendLine($"double {output} = {calculatorName}.Calculate({phase});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto)
        {
            string destPosition = GetPositionNameCamelCase(dto, dto.DimensionStackLevel + 1);
            string fixedPosition = CompilationHelper.FormatValue(dto.OutletPosition);
            string output = GetLocalOutputName(dto);

            AppendOperatorTitleComment(dto);

            AppendLine($"{destPosition} = {fixedPosition};");
            AppendLine();

            BeginGenerateMethod(dto.OperatorID, dto.OperatorTypeEnum);

            Visit_OperatorDto_Polymorphic(dto.OperandOperatorDto);
            string operand = _stack.Pop();

            operand = EndGenerateMethod(operand);

            AppendLine($"double {output} = {operand};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_ConstA_VarB(Divide_OperatorDto_ConstA_VarB dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            PutNumberOnStack(dto.A);

            return ProcessBinaryOperator(dto, DIVIDE_SYMBOL);
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_ConstB(Divide_OperatorDto_VarA_ConstB dto)
        {
            PutNumberOnStack(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessBinaryOperator(dto, DIVIDE_SYMBOL);
        }

        protected override IOperatorDto Visit_Divide_OperatorDto_VarA_VarB(Divide_OperatorDto_VarA_VarB dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessBinaryOperator(dto, DIVIDE_SYMBOL);
        }

        protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto(DoubleToBoolean_OperatorDto dto)
        {
            return Process_DoubleToBoolean(dto, dto.NumberOperatorDto, null);
        }

        protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto_ConstNumber(DoubleToBoolean_OperatorDto_ConstNumber dto)
        {
            return Process_DoubleToBoolean(dto, null, dto.Number);
        }

        protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto_VarNumber(DoubleToBoolean_OperatorDto_VarNumber dto)
        {
            return Process_DoubleToBoolean(dto, dto.NumberOperatorDto, null);
        }

        private IOperatorDto Process_DoubleToBoolean(IOperatorDto dto, IOperatorDto numberOperatorDto, double? numberValue)
        {
            string number = GetLiteralFromOperatorDtoOrValue(numberOperatorDto, numberValue);
            string output = GetLocalOutputName(dto);

            AppendOperatorTitleComment(dto);
            AppendLine($"bool {output} = {number} == 0.0 ? false : true;");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Equal_OperatorDto_VarA_ConstB(Equal_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, EQUALS_SYMBOL);
        }

        protected override IOperatorDto Visit_Equal_OperatorDto_VarA_VarB(Equal_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, EQUALS_SYMBOL);
        }
        
        protected override IOperatorDto Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            string position = GetPositionNameCamelCase(dto);

            _stack.Push(position);

            return dto;
        }

        protected override IOperatorDto Visit_GreaterThan_OperatorDto_VarA_ConstB(GreaterThan_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, GREATER_THAN_SYMBOL);
        }

        protected override IOperatorDto Visit_GreaterThan_OperatorDto_VarA_VarB(GreaterThan_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, GREATER_THAN_SYMBOL);
        }

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_VarA_ConstB(GreaterThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, GREATER_THAN_OR_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto_VarA_VarB(GreaterThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, GREATER_THAN_OR_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_AllVars(HighPassFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetHighPassFilterVariables));
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_ManyConsts(HighPassFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_AllVars(HighShelfFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetHighShelfFilterVariables));
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_ManyConsts(HighShelfFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_Hold_OperatorDto_VarSignal(Hold_OperatorDto_VarSignal dto)
        {
            // The calculate procedure should only use the held variable. 
            // The Reset procedure should do a calculation of that held variable. 
            // But that Reset procedure should not execute the reset code lines.
            // So the Calculate procedure should not be written at all, 
            // and the Reset procedure should get the reset code lines, but does have to get the calculate code lines.
            // In the methods that delegate to StringBuilders, 
            // the _holdOperatorIsActiveStack is inspected to see to which StringBuilder to write, during visitation.

            _holdOperatorIsActiveStack.Push(true);

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);

            _holdOperatorIsActiveStack.Pop();
            _holdOperatorIsActiveStack.Push(false);

            string signal = _stack.Pop();
            string output = GetUniqueLongLivedVariableName(dto.OperatorTypeEnum);

            AppendLineToReset(GetOperatorTitleComment(dto));
            AppendLineToReset($"{output} = {signal};");
            AppendLineToReset();
            _stack.Push(output);
            return dto;
        }

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(If_OperatorDto_VarCondition_ConstThen_ConstElse dto)
        {
            return Process_If_OperatorDto(dto, null, dto.Then, null, dto.Else);
        }

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(If_OperatorDto_VarCondition_ConstThen_VarElse dto)
        {
            return Process_If_OperatorDto(dto, null, dto.Then, dto.ElseOperatorDto, null);
        }

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(If_OperatorDto_VarCondition_VarThen_ConstElse dto)
        {
            return Process_If_OperatorDto(dto, dto.ThenOperatorDto, null, null, dto.Else);
        }

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_VarElse(If_OperatorDto_VarCondition_VarThen_VarElse dto)
        {
            return Process_If_OperatorDto(dto, dto.ThenOperatorDto, null, dto.ElseOperatorDto, null);
        }

        private IOperatorDto Process_If_OperatorDto(
            If_OperatorDtoBase_VarCondition dto,
            IOperatorDto thenOperatorDto,
            double? thenValue,
            IOperatorDto elseOperatorDto,
            double? elseValue)
        {
            Visit_OperatorDto_Polymorphic(dto.ConditionOperatorDto);
            string condition = _stack.Pop();

            string then = GetUniqueLocalVariableName(nameof(then));
            string @else = GetUniqueLocalVariableName(nameof(@else));
            string isTrue = GetUniqueLocalVariableName(nameof(isTrue));
            string output = GetLocalOutputName(dto);

            AppendLine($"{GetOperatorTitleComment(dto)} (begin)");
            AppendLine();

            // TODO: Lower priority: Putting the then and else clauses in separate methods should be dependent on the if being 'long', 
            // which should be determined in the DTO pre-processing.

            string thenLeftSide;
            if (thenOperatorDto != null)
            {
                BeginGenerateMethod(thenOperatorDto.OperatorID, nameof(then));
                thenLeftSide = GetLiteralFromOperatorDtoOrValue(thenOperatorDto, thenValue);
                thenLeftSide = EndGenerateMethod(thenLeftSide);
            }
            else
            {
                thenLeftSide = GetLiteralFromOperatorDtoOrValue(null, thenValue);
            }
            AppendLine($"{GetOperatorTitleComment(dto)} ({nameof(then)})");
            AppendLine($"double {then} = {thenLeftSide};");
            AppendLine();

            string elseLeftSide;
            if (elseOperatorDto != null)
            {
                BeginGenerateMethod(elseOperatorDto.OperatorID, nameof(@else));
                elseLeftSide = GetLiteralFromOperatorDtoOrValue(elseOperatorDto, elseValue);
                elseLeftSide = EndGenerateMethod(elseLeftSide);
            }
            else
            {
                elseLeftSide = GetLiteralFromOperatorDtoOrValue(null, elseValue);
            }
            AppendLine($"{GetOperatorTitleComment(dto)} ({nameof(@else)})");
            AppendLine($"double {@else} = {elseLeftSide};");
            AppendLine();

            AppendLine($"{GetOperatorTitleComment(dto)} (end)");
            AppendLine($"double {output} = {condition} ? {then} : {@else};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Block(InletsToDimension_OperatorDto_Block dto)
        {
            return Process_InletsToDimension(dto, isStripe: false);
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Stripe_LagBehind(InletsToDimension_OperatorDto_Stripe_LagBehind dto)
        {
            return Process_InletsToDimension(dto, isStripe: true);
        }

        private IOperatorDto Process_InletsToDimension(InletsToDimension_OperatorDto dto, bool isStripe)
        {
            string position = GetPositionNameCamelCase(dto);
            string transformedPosition = GetUniqueLocalVariableName(nameof(transformedPosition));
            string castedPosition = GetUniqueLocalVariableName(nameof(castedPosition));
            string output = GetLocalOutputName(dto);

            AppendOperatorTitleComment(dto);

            // Transform and cast position.
            AppendLine($"double {transformedPosition} = {position};");
            if (isStripe)
            {
                AppendLine($"{transformedPosition} += 0.5;");
            }
            AppendLine($"int {castedPosition} = (int){transformedPosition};");

            // Switch over position.
            AppendLine($"double {output} = 0.0;");
            AppendLine($"switch ({castedPosition})");
            AppendLine("{");
            Indent();
            {
                int count = dto.Vars.Count;

                for (int i = 0; i < count; i++)
                {
                    IOperatorDto operandDto = dto.Vars[i];
                    AppendLine($"case {i}:");
                    Indent();
                    {
                        Visit_OperatorDto_Polymorphic(operandDto);
                        string operand = _stack.Pop();

                        AppendLine($"{output} = {operand};");
                        AppendLine("break;");
                        AppendLine();
                        Unindent();
                    }
                }
                Unindent();
            }
            AppendLine("}");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Block(Interpolate_OperatorDto_Block dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicAbruptSlope(Interpolate_OperatorDto_CubicAbruptSlope dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicEquidistant(Interpolate_OperatorDto_CubicEquidistant dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_CubicSmoothSlope_LagBehind(Interpolate_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Hermite_LagBehind(Interpolate_OperatorDto_Hermite_LagBehind dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate(Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate(Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Interpolate_OperatorDto_Stripe_LagBehind(Interpolate_OperatorDto_Stripe_LagBehind dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_LessThan_OperatorDto_VarA_ConstB(LessThan_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, LESS_THAN_SYMBOL);
        }

        protected override IOperatorDto Visit_LessThan_OperatorDto_VarA_VarB(LessThan_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, LESS_THAN_SYMBOL);
        }

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto_VarA_ConstB(LessThanOrEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, LESS_THAN_OR_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto_VarA_VarB(LessThanOrEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, LESS_THAN_OR_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_AllVars(Loop_OperatorDto_AllVars dto)
        {
            return Process_Loop_OperatorDto(
                dto,
                dto.SkipOperatorDto,
                loopStartMarkerOperatorDto: dto.LoopStartMarkerOperatorDto,
                loopEndMarkerOperatorDto: dto.LoopEndMarkerOperatorDto,
                releaseEndMarkerOperatorDto: dto.ReleaseEndMarkerOperatorDto,
                noteDurationValue: CalculationHelper.VERY_HIGH_VALUE);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(
            Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration dto)
        {
            return Process_Loop_OperatorDto(
                dto,
                skipValue: dto.SkipAndLoopStartMarker,
                loopStartMarkerValue: dto.SkipAndLoopStartMarker,
                loopEndMarkerValue: dto.LoopEndMarker,
                releaseEndMarkerOperatorDto: dto.ReleaseEndMarkerOperatorDto,
                noteDurationValue: CalculationHelper.VERY_HIGH_VALUE);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(
            Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration dto)
        {
            return Process_Loop_OperatorDto(
                dto,
                skipValue: dto.SkipAndLoopStartMarker,
                loopStartMarkerValue: dto.SkipAndLoopStartMarker,
                loopEndMarkerOperatorDto: dto.LoopEndMarkerOperatorDto,
                releaseEndMarkerOperatorDto: dto.ReleaseEndMarkerOperatorDto,
                noteDurationValue: CalculationHelper.VERY_HIGH_VALUE);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_ManyConstants(Loop_OperatorDto_ManyConstants dto)
        {
            return Process_Loop_OperatorDto(
                dto,
                skipValue: dto.Skip,
                loopStartMarkerValue: dto.LoopStartMarker,
                loopEndMarkerValue: dto.LoopEndMarker,
                releaseEndMarkerValue: dto.ReleaseEndMarker,
                noteDurationOperatorDto: dto.NoteDurationOperatorDto);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease(Loop_OperatorDto_NoSkipOrRelease dto)
        {
            return Process_Loop_OperatorDto(
                dto,
                skipValue: 0.0,
                loopStartMarkerOperatorDto: dto.LoopStartMarkerOperatorDto,
                loopEndMarkerOperatorDto: dto.LoopEndMarkerOperatorDto,
                releaseEndMarkerValue: 0.0,
                noteDurationOperatorDto: dto.NoteDurationOperatorDto);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_NoSkipOrRelease_ManyConstants(Loop_OperatorDto_NoSkipOrRelease_ManyConstants dto)
        {
            return Process_Loop_OperatorDto(
                dto,
                skipValue: 0.0,
                loopStartMarkerValue: dto.LoopStartMarker,
                loopEndMarkerValue: dto.LoopEndMarker,
                releaseEndMarkerValue: 0.0,
                noteDurationOperatorDto: dto.NoteDurationOperatorDto);
        }

        private IOperatorDto Process_Loop_OperatorDto<TDto>(
            TDto dto,
            IOperatorDto skipOperatorDto = null,
            double? skipValue = null,
            IOperatorDto loopStartMarkerOperatorDto = null,
            double? loopStartMarkerValue = null,
            IOperatorDto loopEndMarkerOperatorDto = null,
            double? loopEndMarkerValue = null,
            IOperatorDto releaseEndMarkerOperatorDto = null,
            double? releaseEndMarkerValue = null,
            IOperatorDto noteDurationOperatorDto = null,
            double? noteDurationValue = null)
            where TDto : IOperatorDto_WithDimension, IOperatorDto_VarSignal
        {
            string output = GetLocalOutputName(dto);
            string sourcePosition = GetPositionNameCamelCase(dto);
            string destPosition = GetPositionNameCamelCase(dto, dto.DimensionStackLevel + 1);
            string origin = GetLongLivedOriginName();
            string nullableInputPosition = GetUniqueLocalVariableName(nameof(nullableInputPosition));

            AppendOperatorTitleComment(dto);

            AppendLineToReset($"{origin} = {sourcePosition};");

            AppendLine($"double {output};");

            // Ported from Loop_OperatorCalculator_Helper.GetTransformedPosition.
            string outputPosition = GetUniqueLocalVariableName(nameof(outputPosition));
            string inputPosition = GetUniqueLocalVariableName(nameof(inputPosition));
            string isBeforeAttack = GetUniqueLocalVariableName(nameof(isBeforeAttack));
            string isInAttack = GetUniqueLocalVariableName(nameof(isInAttack));
            string cycleLength = GetUniqueLocalVariableName(nameof(cycleLength));
            string outputLoopStart = GetUniqueLocalVariableName(nameof(outputLoopStart));
            string noteEndPhase = GetUniqueLocalVariableName(nameof(noteEndPhase));
            string outputLoopEnd = GetUniqueLocalVariableName(nameof(outputLoopEnd));
            string isInLoop = GetUniqueLocalVariableName(nameof(isInLoop));
            string phase = GetUniqueLocalVariableName(nameof(phase));
            string releaseLength = GetUniqueLocalVariableName(nameof(releaseLength));
            string outputReleaseEndPosition = GetUniqueLocalVariableName(nameof(outputReleaseEndPosition));
            string isInRelease = GetUniqueLocalVariableName(nameof(isInRelease));
            string positionInRelease = GetUniqueLocalVariableName(nameof(positionInRelease));
            AppendLine($"double? {nullableInputPosition};");
            AppendLine();
            AppendLine($"double {outputPosition} = {sourcePosition};");
            AppendLine($"double {inputPosition} = {outputPosition};");
            AppendLine();
            AppendLine($"{inputPosition} -= {origin};");
            AppendLine();
            AppendLine("// BeforeAttack");
            AppendLine();
            string skip = GetLiteralFromOperatorDtoOrValue(skipOperatorDto, skipValue);
            AppendLine($"{inputPosition} += {skip};");
            AppendLine($"bool {isBeforeAttack} = {inputPosition} < {skip};");
            AppendLine($"if ({isBeforeAttack})");
            AppendLine("{");
            Indent();
            {
                AppendLine($"{nullableInputPosition} = null;");
                Unindent();
            }
            AppendLine("}");
            AppendLine("else");
            AppendLine("{");
            Indent();
            {
                AppendLine("// InAttack");
                AppendLine();
                string loopStartMarker = GetLiteralFromOperatorDtoOrValue(loopStartMarkerOperatorDto, loopStartMarkerValue);
                AppendLine($"bool {isInAttack} = {inputPosition} < {loopStartMarker};");
                AppendLine($"if ({isInAttack})");
                AppendLine("{");
                Indent();
                {
                    AppendLine($"{nullableInputPosition} = {inputPosition};");
                    Unindent();
                }
                AppendLine("}");
                AppendLine("else");
                AppendLine("{");
                Indent();
                {
                    AppendLine("// InLoop");
                    AppendLine();
                    string loopEndMarker = GetLiteralFromOperatorDtoOrValue(loopEndMarkerOperatorDto, loopEndMarkerValue);
                    AppendLine($"double {cycleLength} = {loopEndMarker} - {loopStartMarker};");
                    AppendLine();
                    AppendLine("// Round up end of loop to whole cycles.");
                    AppendLine($"double {outputLoopStart} = {loopStartMarker} - {skip};");
                    AppendLine();
                    string noteDuration = GetLiteralFromOperatorDtoOrValue(noteDurationOperatorDto, noteDurationValue);
                    AppendLine($"double {noteEndPhase} = ({noteDuration} - {outputLoopStart}) / {cycleLength};");
                    AppendLine($"double {outputLoopEnd} = {outputLoopStart} + Math.Ceiling({noteEndPhase}) * {cycleLength};");
                    AppendLine();
                    AppendLine($"bool {isInLoop} = {outputPosition} < {outputLoopEnd};");
                    AppendLine($"if ({isInLoop})");
                    AppendLine("{");
                    Indent();
                    {
                        AppendLine($"double {phase} = ({inputPosition} - {loopStartMarker}) % {cycleLength};");
                        AppendLine($"{inputPosition} = {loopStartMarker} + {phase};");
                        AppendLine($"{nullableInputPosition} = {inputPosition};");
                        Unindent();
                    }
                    AppendLine("}");
                    AppendLine("else");
                    AppendLine("{");
                    Indent();
                    {
                        AppendLine("// InRelease");
                        AppendLine();
                        string releaseEndMarker = GetLiteralFromOperatorDtoOrValue(releaseEndMarkerOperatorDto, releaseEndMarkerValue);
                        AppendLine($"double {releaseLength} = {releaseEndMarker} - {loopEndMarker};");
                        AppendLine($"double {outputReleaseEndPosition} = {outputLoopEnd} + {releaseLength};");
                        AppendLine($"bool {isInRelease} = {outputPosition} < {outputReleaseEndPosition};");
                        AppendLine($"if ({isInRelease})");
                        AppendLine("{");
                        Indent();
                        {
                            AppendLine($"double {positionInRelease} = {outputPosition} - {outputLoopEnd};");
                            AppendLine($"{inputPosition} = {loopEndMarker} + {positionInRelease};");
                            AppendLine($"{nullableInputPosition} = {inputPosition};");
                            Unindent();
                        }
                        AppendLine("}");
                        AppendLine("else");
                        AppendLine("{");
                        Indent();
                        {
                            AppendLine("// AfterRelease");
                            AppendLine($"{nullableInputPosition} = null;");
                            Unindent();
                        }
                        AppendLine("}");
                        Unindent();
                    }
                    AppendLine("}");
                    Unindent();
                }
                AppendLine("}");
                Unindent();
            }
            AppendLine("}");
            AppendLine();

            AppendLine($"if (!{nullableInputPosition}.HasValue)");
            AppendLine("{");
            Indent();
            {
                AppendLine($"{output} = 0.0;");
                Unindent();
            }
            AppendLine("}");
            AppendLine("else");
            AppendLine("{");
            Indent();
            {
                AppendLine($"{destPosition} = {nullableInputPosition}.Value;");
                AppendLine();

                Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
                string signal = _stack.Pop();

                AppendLine($"{output} = {signal};");
                Unindent();
            }
            AppendLine("}");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_AllVars(LowPassFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetLowPassFilterVariables));
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_ManyConsts(LowPassFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_AllVars(LowShelfFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetLowShelfFilterVariables));
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_ManyConsts(LowShelfFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_MaxFollower_OperatorDto_AllVars(MaxFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(
            MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(
            MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_1Var_1Const(MaxOverInlets_OperatorDto_1Var_1Const dto)
        {
            PutNumberOnStack(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return Process_MinOrMaxOverInlets_With2Inlets(dto, MinOrMaxEnum.Max);
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_2Vars(MaxOverInlets_OperatorDto_2Vars dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return Process_MinOrMaxOverInlets_With2Inlets(dto, MinOrMaxEnum.Max);
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
        {
            PutNumberOnStack(dto.ConstValue);
            dto.Vars.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return Process_MinOrMaxOverInlets_MoreThan2Inlets(dto, MinOrMaxEnum.Max, dto.Vars.Count + 1);
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            dto.Vars.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return Process_MinOrMaxOverInlets_MoreThan2Inlets(dto, MinOrMaxEnum.Max, dto.Vars.Count);
        }

        protected override IOperatorDto Visit_MinFollower_OperatorDto_AllVars(MinFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(
            MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(
            MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_1Var_1Const(MinOverInlets_OperatorDto_1Var_1Const dto)
        {
            PutNumberOnStack(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return Process_MinOrMaxOverInlets_With2Inlets(dto, MinOrMaxEnum.Min);
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_2Vars(MinOverInlets_OperatorDto_2Vars dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return Process_MinOrMaxOverInlets_With2Inlets(dto, MinOrMaxEnum.Min);
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
        {
            PutNumberOnStack(dto.ConstValue);
            dto.Vars.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return Process_MinOrMaxOverInlets_MoreThan2Inlets(dto, MinOrMaxEnum.Min, dto.Vars.Count + 1);
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
        {
            dto.Vars.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return Process_MinOrMaxOverInlets_MoreThan2Inlets(dto, MinOrMaxEnum.Min, dto.Vars.Count);
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_1Const(Multiply_OperatorDto_Vars_1Const dto)
        {
            return ProcessMultiVarOperator_Vars_1Const(dto, MULTIPLY_SYMBOL);
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_Vars_NoConsts(Multiply_OperatorDto_Vars_NoConsts dto)
        {
            return ProcessMultiVarOperator_Vars_NoConsts(dto, MULTIPLY_SYMBOL);
        }

        protected override IOperatorDto Visit_Negative_OperatorDto_VarNumber(Negative_OperatorDto_VarNumber dto)
        {
            Visit_OperatorDto_Polymorphic(dto.NumberOperatorDto);

            AppendOperatorTitleComment(dto);

            string number = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"double {output} = -{number};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            string output = GetLocalOutputName(dto);
            string position = GetPositionNameCamelCase(dto);
            string offset = GetRandomOrNoiseOffsetVariableNameCamelCase(dto.OperatorID);
            string arrayCalculator = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDto);
            const string noiseCalculatorHelper = nameof(NoiseCalculatorHelper);
            const string generateOffset = nameof(NoiseCalculatorHelper.GenerateOffset);

            AppendOperatorTitleComment(dto);

            AppendLineToReset($"{offset} = {noiseCalculatorHelper}.{generateOffset}();");

            // TODO: Low priority: Just assigning offset to position in the reset operation would be slightly faster.
            AppendLine($"double {output} = {arrayCalculator}.Calculate({position} + {offset});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Not_OperatorDto_VarNumber(Not_OperatorDto_VarNumber dto)
        {
            Visit_OperatorDto_Polymorphic(dto.NumberOperatorDto);

            AppendOperatorTitleComment(dto);

            string input = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"bool {output} = !{input};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_AllVars(NotchFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetNotchFilterVariables));
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_ManyConsts(NotchFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_NotEqual_OperatorDto_VarA_ConstB(NotEqual_OperatorDto_VarA_ConstB dto)
        {
            return ProcessComparativeOperator_VarA_ConstB(dto, NOT_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_NotEqual_OperatorDto_VarA_VarB(NotEqual_OperatorDto_VarA_VarB dto)
        {
            return ProcessComparativeOperator_VarA_VarB(dto, NOT_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            return ProcessNumberOperatorDto(dto);
        }

        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            return ProcessNumberOperatorDto(dto);
        }

        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            return ProcessNumberOperatorDto(dto);
        }

        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            return ProcessNumberOperatorDto(dto);
        }

        private Number_OperatorDto ProcessNumberOperatorDto(Number_OperatorDto dto)
        {
            PutNumberOnStack(dto.Number);

            return dto;
        }

        protected override IOperatorDto Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
        {
            return ProcessLogicalBinaryOperator(dto, OR_SYMBOL);
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_AllVars(PeakingEQFilter_OperatorDto_AllVars dto)
        {
            return Process_Filter_OperatorDto_AllVars(dto, nameof(BiQuadFilterWithoutFields.SetPeakingEQFilterVariables));
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_ManyConsts(PeakingEQFilter_OperatorDto_ManyConsts dto)
        {
            return Process_Filter_OperatorDto_ManyConsts(dto);
        }

        protected override IOperatorDto Visit_Power_OperatorDto_ConstBase_VarExponent(Power_OperatorDto_ConstBase_VarExponent dto)
        {
            Visit_OperatorDto_Polymorphic(dto.ExponentOperatorDto);
            PutNumberOnStack(dto.Base);

            return Process_Math_Pow(dto);
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
        {
            PutNumberOnStack(dto.Exponent);
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            return Process_Math_Pow(dto);
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_Exponent2(Power_OperatorDto_VarBase_Exponent2 dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            AppendOperatorTitleComment(dto);

            string @base = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"double {output} = {@base} * {@base};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_Exponent3(Power_OperatorDto_VarBase_Exponent3 dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            AppendOperatorTitleComment(dto);

            string @base = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"double {output} = {@base} * {@base} * {@base};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_Exponent4(Power_OperatorDto_VarBase_Exponent4 dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            AppendOperatorTitleComment(dto);

            string @base = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"double {output} = {@base} * {@base};");
            AppendLine($"{output} *= {output};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_VarExponent(Power_OperatorDto_VarBase_VarExponent dto)
        {
            Visit_OperatorDto_Polymorphic(dto.ExponentOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.BaseOperatorDto);

            return Process_Math_Pow(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting(
            Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting dto)
        {
            PutNumberOnStack(dto.Width);
            PutNumberOnStack(dto.Frequency);

            return Process_Pulse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting(
            Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting dto)
        {
            PutNumberOnStack(dto.Width);
            PutNumberOnStack(dto.Frequency);

            return Process_Pulse_WithOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting(Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            PutNumberOnStack(dto.Frequency);

            return Process_Pulse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting(
            Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            PutNumberOnStack(dto.Frequency);

            return Process_Pulse_WithOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking dto)
        {
            PutNumberOnStack(dto.Width);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking(
            Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking dto)
        {
            PutNumberOnStack(dto.Width);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_WithPhaseTracking(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_NoPhaseTrackingOrOriginShifting(dto);
        }

        protected override IOperatorDto Visit_Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking(Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.WidthOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return Process_Pulse_WithPhaseTracking(dto);
        }

        private IOperatorDto Process_Pulse_NoPhaseTrackingOrOriginShifting(IOperatorDto_WithDimension dto)
        {
            AppendOperatorTitleComment(dto);

            string frequency = _stack.Pop();

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, frequency);

            string width = _stack.Pop();
            string output = GetLocalOutputName(dto);
            AppendLine($"double {output} = {phase} % 1.0 < {width} ? 1.0 : -1.0;");

            return GenerateOperatorWrapUp(dto, output);
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private IOperatorDto Process_Pulse_WithOriginShifting(OperatorDtoBase_ConstFrequency dto)
        {
            AppendOperatorTitleComment(dto);

            string frequency = _stack.Pop();

            string phase = GeneratePhaseCalculationWithOriginShifting(dto, frequency);

            string width = _stack.Pop();
            string output = GetLocalOutputName(dto);
            AppendLine($"double {output} = {phase} % 1.0 < {width} ? 1.0 : -1.0;");

            return GenerateOperatorWrapUp(dto, output);
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private IOperatorDto Process_Pulse_WithPhaseTracking(OperatorDtoBase_VarFrequency dto)
        {
            AppendOperatorTitleComment(dto);

            string frequency = _stack.Pop();

            string phase = GeneratePhaseCalculationWithPhaseTracking(dto, frequency);

            string width = _stack.Pop();
            string output = GetLocalOutputName(dto);
            AppendLine($"double {output} = {phase} % 1.0 < {width} ? 1.0 : -1.0;");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_PulseTrigger_OperatorDto_VarPassThrough_VarReset(PulseTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
        {
            return Process_Random_OperatorDto(dto);
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto)
        {
            return Process_Random_OperatorDto(dto);
        }

        private IOperatorDto Process_Random_OperatorDto(Random_OperatorDto dto)
        {
            Visit_OperatorDto_Polymorphic(dto.RateOperatorDto);

            string rate = _stack.Pop();
            string output = GetLocalOutputName(dto);
            string offset = GetRandomOrNoiseOffsetVariableNameCamelCase(dto.OperatorID);
            string arrayCalculator = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDto);
            const string randomCalculatorHelper = nameof(RandomCalculatorHelper);
            const string generateOffset = nameof(RandomCalculatorHelper.GenerateOffset);

            AppendOperatorTitleComment(dto);

            AppendLineToReset($"{offset} = {randomCalculatorHelper}.{generateOffset}();");
            string phase = GeneratePhaseCalculationWithPhaseTracking(dto, rate);

            // TODO: Low priority: Just assigning offset to phase in the reset operation would be slightly faster,
            // however, this might make the GeneratePhaseCalculationWithPhaseTracking not reusable here.
            AppendLine($"double {output} = {arrayCalculator}.Calculate({phase} + {offset});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
        {
            string from = CompilationHelper.FormatValue(dto.From);
            string till = CompilationHelper.FormatValue(dto.Till);
            string step = CompilationHelper.FormatValue(dto.Step);
            string position = GetPositionNameCamelCase(dto);
            string output = GetLocalOutputName(dto);
            string tillPlusStep = GetUniqueLocalVariableName(nameof(tillPlusStep));
            string valueNonRounded = GetUniqueLocalVariableName(nameof(valueNonRounded));
            string upperBound = GetUniqueLocalVariableName(nameof(upperBound));
            string valueNonRoundedCorrected = GetUniqueLocalVariableName(nameof(valueNonRoundedCorrected));
            string stepDividedBy2 = GetUniqueLocalVariableName(nameof(stepDividedBy2));
            string valueRounded = GetUniqueLocalVariableName(nameof(valueRounded));
            const string mathHelper = nameof(MathHelper);
            const string roundWithStep = nameof(MathHelper.RoundWithStep);

            AppendOperatorTitleComment(dto);

            AppendLine($"const double {tillPlusStep} = {till} + {step};");
            AppendLine($"const double {stepDividedBy2} = {step} / 2.0;");
            AppendLine($"double {output};");
            AppendLine($"if ({position} < 0.0)");
            AppendLine("{");
            Indent();
            {
                AppendLine($"{output} = 0.0;");
                Unindent();
            }
            AppendLine("}");
            AppendLine("else");
            AppendLine("{");
            Indent();
            {
                AppendLine($"double {valueNonRounded} = {from} + {position} * {step};");
                AppendLine($"double {upperBound} = {tillPlusStep};"); // Sustain last value for the length a of step.
                AppendLine($"if ({valueNonRounded} > {upperBound})");
                AppendLine("{");
                Indent();
                {
                    AppendLine($"{output} = 0.0;");
                    Unindent();
                }
                AppendLine("}");
                AppendLine("else");
                AppendLine("{");
                Indent();
                {
                    // Correct so that we round down and never up.
                    AppendLine($"double {valueNonRoundedCorrected} = {valueNonRounded} - {stepDividedBy2};");
                    AppendLine($"double {valueRounded} = {mathHelper}.{roundWithStep}({valueNonRoundedCorrected}, {step});");
                    AppendLine($"{output} = {valueRounded};");
                    Unindent();
                }
                AppendLine("}");
                Unindent();
            }
            AppendLine("}");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyVars(RangeOverDimension_OperatorDto_OnlyVars dto)
        {
            Visit_OperatorDto_Polymorphic(dto.StepOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.TillOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FromOperatorDto);

            string from = _stack.Pop();
            string till = _stack.Pop();
            string step = _stack.Pop();
            string position = GetPositionNameCamelCase(dto);
            string output = GetLocalOutputName(dto);
            string valueNonRounded = GetUniqueLocalVariableName(nameof(valueNonRounded));
            string upperBound = GetUniqueLocalVariableName(nameof(upperBound));
            string valueNonRoundedCorrected = GetUniqueLocalVariableName(nameof(valueNonRoundedCorrected));
            string valueRounded = GetUniqueLocalVariableName(nameof(valueRounded));
            const string mathHelper = nameof(MathHelper);
            const string roundWithStep = nameof(MathHelper.RoundWithStep);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output};");
            AppendLine($"if ({position} < 0.0)");
            AppendLine("{");
            Indent();
            {
                AppendLine($"{output} = 0.0;");
                Unindent();
            }
            AppendLine("}");
            AppendLine("else");
            AppendLine("{");
            Indent();
            {
                AppendLine($"double {valueNonRounded} = {from} + {position} * {step};");
                AppendLine($"double {upperBound} = {till} + {step};"); // Sustain last value for the length a of step.
                AppendLine($"if ({valueNonRounded} > {upperBound})");
                AppendLine("{");
                Indent();
                {
                    AppendLine($"{output} = 0.0;");
                    Unindent();
                }
                AppendLine("}");
                AppendLine("else");
                AppendLine("{");
                Indent();
                {
                    // Correct so that we round down and never up.
                    AppendLine($"double {valueNonRoundedCorrected} = {valueNonRounded} - {step} / 2.0;");
                    AppendLine($"double {valueRounded} = {mathHelper}.{roundWithStep}({valueNonRoundedCorrected}, {step});");
                    AppendLine($"{output} = {valueRounded};");
                    Unindent();
                }
                AppendLine("}");
                Unindent();
            }
            AppendLine("}");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_WithConsts_AndStepOne(RangeOverDimension_OperatorDto_WithConsts_AndStepOne dto)
        {
            string from = CompilationHelper.FormatValue(dto.From);
            string till = CompilationHelper.FormatValue(dto.Till);
            string position = GetPositionNameCamelCase(dto);
            string output = GetLocalOutputName(dto);
            string tillPlusOne = GetUniqueLocalVariableName(nameof(tillPlusOne));
            string valueNonRounded = GetUniqueLocalVariableName(nameof(valueNonRounded));
            string upperBound = GetUniqueLocalVariableName(nameof(upperBound));
            string valueNonRoundedCorrected = GetUniqueLocalVariableName(nameof(valueNonRoundedCorrected));
            string valueRounded = GetUniqueLocalVariableName(nameof(valueRounded));
            const string math = nameof(Math);
            const string round = nameof(Math.Round);
            const string midpointRounding = nameof(MidpointRounding);
            const string awayFromZero = nameof(MidpointRounding.AwayFromZero);

            AppendOperatorTitleComment(dto);

            AppendLine($"const double {tillPlusOne} = {till} + 1.0;");
            AppendLine($"double {output};");
            AppendLine($"if ({position} < 0.0)");
            AppendLine("{");
            Indent();
            {
                AppendLine($"{output} = 0.0;");
                Unindent();
            }
            AppendLine("}");
            AppendLine("else");
            AppendLine("{");
            Indent();
            {
                AppendLine($"double {valueNonRounded} = {from} + {position};");
                AppendLine($"double {upperBound} = {tillPlusOne};"); // Sustain last value for the length a of step.
                AppendLine($"if ({valueNonRounded} > {upperBound})");
                AppendLine("{");
                Indent();
                {
                    AppendLine($"{output} = 0.0;");
                    Unindent();
                }
                AppendLine("}");
                AppendLine("else");
                AppendLine("{");
                Indent();
                {
                    // Correct so that we round down and never up.
                    AppendLine($"double {valueNonRoundedCorrected} = {valueNonRounded} - 0.5;");
                    AppendLine($"double {valueRounded} = {math}.{round}({valueNonRoundedCorrected}, {midpointRounding}.{awayFromZero});");
                    AppendLine($"{output} = {valueRounded};");
                    Unindent();
                }
                AppendLine("}");
                Unindent();
            }
            AppendLine("}");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep dto)
        {
            Visit_OperatorDto_Polymorphic(dto.StepOperatorDto);
            PutNumberOnStack(dto.From);

            return Process_RangeOverOutlets_Outlet(dto);
        }

        protected override IOperatorDto Visit_RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep(RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep dto)
        {
            Visit_OperatorDto_Polymorphic(dto.StepOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FromOperatorDto);

            return Process_RangeOverOutlets_Outlet(dto);
        }

        private IOperatorDto Process_RangeOverOutlets_Outlet(IRangeOverOutlets_Outlet_OperatorDto_WithOutletPosition dto)
        {
            AppendOperatorTitleComment(dto);

            string from = _stack.Pop();
            string step = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"double {output} = {from} + {step} * {dto.OutletPosition};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Round_OperatorDto_ConstSignal(Round_OperatorDto_ConstSignal dto)
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

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ConstOffset(Round_OperatorDto_VarSignal_ConstStep_ConstOffset dto)
        {
            return ProcessRoundWithOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepValue: dto.Step, offsetValue: dto.Offset);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_VarOffset(Round_OperatorDto_VarSignal_ConstStep_VarOffset dto)
        {
            return ProcessRoundWithOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepValue: dto.Step, offsetOperatorDto: dto.OffsetOperatorDto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_ConstStep_ZeroOffset(Round_OperatorDto_VarSignal_ConstStep_ZeroOffset dto)
        {
            return ProcessRoundZeroOffset(dto, signalOperatorDto: dto.SignalOperatorDto, stepValue: dto.Step);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_StepOne_OffsetZero(Round_OperatorDto_VarSignal_StepOne_OffsetZero dto)
        {
            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);

            AppendOperatorTitleComment(dto);

            string signal = _stack.Pop();
            string output = GetLocalOutputName(dto);
            const string math = nameof(Math);
            const string round = nameof(Math.Round);

            AppendLine($"double {output} = {math}.{round}({signal}, MidpointRounding.AwayFromZero);");

            return GenerateOperatorWrapUp(dto, signal);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ConstOffset(Round_OperatorDto_VarSignal_VarStep_ConstOffset dto)
        {
            return ProcessRoundWithOffset(dto, dto.SignalOperatorDto, stepOperatorDto: dto.StepOperatorDto, offsetValue: dto.Offset);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_VarOffset(Round_OperatorDto_VarSignal_VarStep_VarOffset dto)
        {
            return ProcessRoundWithOffset(dto, dto.SignalOperatorDto, stepOperatorDto: dto.StepOperatorDto, offsetOperatorDto: dto.OffsetOperatorDto);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_VarSignal_VarStep_ZeroOffset(Round_OperatorDto_VarSignal_VarStep_ZeroOffset dto)
        {
            return ProcessRoundZeroOffset(dto, dto.SignalOperatorDto, stepOperatorDto: dto.StepOperatorDto);
        }

        private IOperatorDto ProcessRoundWithOffset(
            IOperatorDto dto,
            IOperatorDto signalOperatorDto = null,
            double? signalValue = null,
            IOperatorDto stepOperatorDto = null,
            double? stepValue = null,
            IOperatorDto offsetOperatorDto = null,
            double? offsetValue = null)
        {
            AppendOperatorTitleComment(dto);

            string signal = GetLiteralFromOperatorDtoOrValue(signalOperatorDto, signalValue);
            string step = GetLiteralFromOperatorDtoOrValue(stepOperatorDto, stepValue);
            string offset = GetLiteralFromOperatorDtoOrValue(offsetOperatorDto, offsetValue);
            string output = GetLocalOutputName(dto);
            const string mathHelper = nameof(MathHelper);
            const string roundWithStep = nameof(MathHelper.RoundWithStep);

            AppendLine($"double {output} = {mathHelper}.{roundWithStep}({signal}, {step}, {offset});");

            return GenerateOperatorWrapUp(dto, signal);
        }

        private IOperatorDto ProcessRoundZeroOffset(
            IOperatorDto dto,
            IOperatorDto signalOperatorDto = null,
            double? signalValue = null,
            IOperatorDto stepOperatorDto = null,
            double? stepValue = null)
        {
            AppendOperatorTitleComment(dto);

            string signal = GetLiteralFromOperatorDtoOrValue(signalOperatorDto, signalValue);
            string step = GetLiteralFromOperatorDtoOrValue(stepOperatorDto, stepValue);
            string output = GetLocalOutputName(dto);
            const string mathHelper = nameof(MathHelper);
            const string roundWithStep = nameof(MathHelper.RoundWithStep);

            AppendLine($"double {output} = {mathHelper}.{roundWithStep}({signal}, {step});");

            return GenerateOperatorWrapUp(dto, signal);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(
            Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
        {
            string rate = GenerateConstRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, rate);

            return GenerateSampleMonoToStereoEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(
            Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
        {
            string rate = GenerateConstRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithOriginShifting(dto, rate);

            return GenerateSampleMonoToStereoEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            string rate = GenerateConstRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, rate);

            return GenerateSampleChannelSwitchEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(
            Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
        {
            string rate = GenerateConstRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, rate);

            return GenerateSampleStereoToMonoEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(
            Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
        {
            string rate = GenerateConstRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithOriginShifting(dto, rate);

            return GenerateSampleStereoToMonoEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            string rate = GenerateConstRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithOriginShifting(dto, rate);

            return GenerateSampleChannelSwitchEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(
            Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
        {
            string rate = GenerateVarRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, rate);

            return GenerateSampleMonoToStereoEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(
            Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
        {
            string rate = GenerateVarRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithPhaseTracking(dto, rate);

            return GenerateSampleMonoToStereoEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            string rate = GenerateVarRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, rate);

            return GenerateSampleChannelSwitchEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(
            Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
        {
            string rate = GenerateVarRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, rate);

            return GenerateSampleStereoToMonoEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(
            Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
        {
            string rate = GenerateVarRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithPhaseTracking(dto, rate);

            return GenerateSampleStereoToMonoEnd(dto, phase);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            string rate = GenerateVarRateCalculationForSample(dto);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithPhaseTracking(dto, rate);

            return GenerateSampleChannelSwitchEnd(dto, phase);
        }

        /// <summary> Returns the rate literal. </summary>
        private string GenerateConstRateCalculationForSample(OperatorDtoBase_ConstFrequency dto)
        {
            string rate = CompilationHelper.FormatValue(dto.Frequency / SAMPLE_BASE_FREQUENCY);
            return rate;
        }

        /// <summary> Returns the rate literal. </summary>
        private string GenerateVarRateCalculationForSample(OperatorDtoBase_VarFrequency dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);
            string frequency = _stack.Pop();
            string rate = GetUniqueLocalVariableName(RATE_MNEMONIC);

            AppendLine($"double {rate} = {frequency} / {SAMPLE_BASE_FREQUENCY};");

            return rate;
        }

        private IOperatorDto GenerateSampleChannelSwitchEnd(ISample_OperatorDto_WithSampleID dto, string phase)
        {
            IList<ArrayDto> arrayDtos = dto.ArrayDtos;

            int channnelDimensionStackLevel = dto.ChannelDimensionStackLevel;
            string channelIndexDouble = GetPositionNameCamelCase(channnelDimensionStackLevel, DimensionEnum.Channel);
            string channelIndex = GetUniqueLocalVariableName(DimensionEnum.Channel);
            string output = GetLocalOutputName(dto);

            AppendLine($"int {channelIndex} = (int){channelIndexDouble};");
            AppendLine($"double {output} = 0.0;");
            AppendLine($"switch ({channelIndex})");
            AppendLine("{");
            Indent();
            {
                for (int i = 0; i < arrayDtos.Count; i++)
                {
                    ArrayDto arrayDto = arrayDtos[i];
                    string calculatorName = GetArrayCalculatorVariableNameCamelCaseAndCache(arrayDto);

                    AppendLine($"case {i}:");
                    Indent();
                    {
                        AppendLine($"{output} = {calculatorName}.Calculate({phase});");
                        AppendLine("break;");
                        AppendLine();
                        Unindent();
                    }
                }
                Unindent();
            }
            AppendLine("}");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto GenerateSampleMonoToStereoEnd(ISample_OperatorDto_WithSampleID dto, string phase)
        {
            // Array
            ArrayDto arrayDto = dto.ArrayDtos.Single();
            string calculatorName = GetArrayCalculatorVariableNameCamelCaseAndCache(arrayDto);
            string output = GetLocalOutputName(dto);

            AppendLine($"double {output} = {calculatorName}.Calculate({phase});"); // Return the single channel for both channels.

            // Wrap-Up
            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto GenerateSampleStereoToMonoEnd(ISample_OperatorDto_WithSampleID dto, string phase)
        {
            // Array
            string calculatorName1 = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDtos[0]);
            string calculatorName2 = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDtos[1]);

            string output = GetLocalOutputName(dto);
            AppendLine($"double {output} =");
            Indent();
            {
                AppendLine($"{calculatorName1}.Calculate({phase}) +");
                AppendLine($"{calculatorName2}.Calculate({phase});");
                Unindent();
            }

            // Wrap-Up
            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_ConstFrequency_NoOriginShifting(SawDown_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            PutNumberOnStack(dto.Frequency);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => string.Format(SAW_DOWN_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_ConstFrequency_WithOriginShifting(SawDown_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessOriginShifter(dto, x => string.Format(SAW_DOWN_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_VarFrequency_NoPhaseTracking(SawDown_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => string.Format(SAW_DOWN_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SawDown_OperatorDto_VarFrequency_WithPhaseTracking(SawDown_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessPhaseTrackingOperator(dto, x => string.Format(SAW_DOWN_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_ConstFrequency_NoOriginShifting(SawUp_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            PutNumberOnStack(dto.Frequency);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => string.Format(SAW_UP_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_ConstFrequency_WithOriginShifting(SawUp_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessOriginShifter(dto, x => string.Format(SAW_UP_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_VarFrequency_NoPhaseTracking(SawUp_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => string.Format(SAW_UP_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SawUp_OperatorDto_VarFrequency_WithPhaseTracking(SawUp_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessPhaseTrackingOperator(dto, x => string.Format(SAW_UP_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_ConstNumber(SetDimension_OperatorDto_VarPassThrough_ConstNumber dto)
        {
            return ProcessSetDimension(dto, x: dto.Number);
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto_VarPassThrough_VarNumber(SetDimension_OperatorDto_VarPassThrough_VarNumber dto)
        {
            return ProcessSetDimension(dto, dto.NumberOperatorDto);
        }

        private IOperatorDto ProcessSetDimension(IOperatorDto_VarSignal_WithDimension dto, IOperatorDto xOperatorDto = null, double? x = null)
        {
            string xLiteral = GetLiteralFromOperatorDtoOrValue(xOperatorDto, x);
            string position = GetPositionNameCamelCase(dto, dto.DimensionStackLevel + 1);

            AppendOperatorTitleComment(dto);

            AppendLine($"{position} = {xLiteral};");
            AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            _stack.Push(signal);
            return dto;
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            PutNumberOnStack(dto.Frequency);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => string.Format(SINE_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessOriginShifter(dto, x => string.Format(SINE_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => string.Format(SINE_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessPhaseTrackingOperator(dto, x => string.Format(SINE_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(
            SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(
            SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto)
        {
            string position = GetPositionNameCamelCase(dto);
            string items = GetLongLivedDoubleArrayVariableName(dto.Vars.Count);
            string output = GetLocalOutputName(dto);
            const string conversionHelper = nameof(ConversionHelper);
            const string canCastToNonNegativeInt32WithMax = nameof(ConversionHelper.CanCastToNonNegativeInt32WithMax);
            string maxIndex = CompilationHelper.FormatValue(dto.Vars.Count - 1);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output};");
            AppendLine($"if (!{conversionHelper}.{canCastToNonNegativeInt32WithMax}({position}, {maxIndex}))");
            AppendLine("{");
            Indent();
            {
                AppendLine($"{output} = 0.0;");
                Unindent();
            }
            AppendLine("}");
            AppendLine();

            for (int i = 0; i < dto.Vars.Count; i++)
            {
                IOperatorDto operandDto = dto.Vars[i];

                Visit_OperatorDto_Polymorphic(operandDto);
                string item = _stack.Pop();

                AppendLine($"{items}[{i}] = {item};");
            }

            AppendLine();
            AppendLine($"Array.Sort({items});");
            AppendLine();
            AppendLine($"{output} = {items}[(int){position}];");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Spectrum_OperatorDto_AllVars(Spectrum_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Square_OperatorDto_ConstFrequency_NoOriginShifting(Square_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            PutNumberOnStack(dto.Frequency);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => string.Format(SQUARE_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_Square_OperatorDto_ConstFrequency_WithOriginShifting(Square_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return ProcessOriginShifter(dto, x => string.Format(SQUARE_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_Square_OperatorDto_VarFrequency_NoPhaseTracking(Square_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            return ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(dto, x => string.Format(SQUARE_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_Square_OperatorDto_VarFrequency_WithPhaseTracking(Square_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return ProcessPhaseTrackingOperator(dto, x => string.Format(SQUARE_FORMULA_FORMAT, x));
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            PutNumberOnStack(dto.Origin);
            PutNumberOnStack(dto.Factor);

            return Process_StretchOrSquash_WithOrigin(dto, StretchOrSquashEnum.Squash);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            PutNumberOnStack(dto.Factor);

            return Process_StretchOrSquash_WithOrigin(dto, StretchOrSquashEnum.Squash);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(
            Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            PutNumberOnStack(dto.Factor);

            return Process_StretchOrSquash_WithOriginShifting(dto, StretchOrSquashEnum.Squash);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            PutNumberOnStack(dto.Factor);

            return Process_StretchOrSquash_ZeroOrigin(dto, StretchOrSquashEnum.Squash);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            PutNumberOnStack(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            return Process_StretchOrSquash_WithOrigin(dto, StretchOrSquashEnum.Squash);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_VarOrigin(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            return Process_StretchOrSquash_WithOrigin(dto, StretchOrSquashEnum.Squash);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            return Process_StretchOrSquash_WithPhaseTracking(dto, StretchOrSquashEnum.Squash);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            return Process_StretchOrSquash_ZeroOrigin(dto, StretchOrSquashEnum.Squash);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin dto)
        {
            PutNumberOnStack(dto.Origin);
            PutNumberOnStack(dto.Factor);

            return Process_StretchOrSquash_WithOrigin(dto, StretchOrSquashEnum.Stretch);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            PutNumberOnStack(dto.Factor);

            return Process_StretchOrSquash_WithOrigin(dto, StretchOrSquashEnum.Stretch);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting(
            Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting dto)
        {
            PutNumberOnStack(dto.Factor);

            return Process_StretchOrSquash_WithOriginShifting(dto, StretchOrSquashEnum.Stretch);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin dto)
        {
            PutNumberOnStack(dto.Factor);

            return Process_StretchOrSquash_ZeroOrigin(dto, StretchOrSquashEnum.Stretch);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin dto)
        {
            PutNumberOnStack(dto.Origin);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            return Process_StretchOrSquash_WithOrigin(dto, StretchOrSquashEnum.Stretch);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.OriginOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            return Process_StretchOrSquash_WithOrigin(dto, StretchOrSquashEnum.Stretch);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            return Process_StretchOrSquash_WithPhaseTracking(dto, StretchOrSquashEnum.Stretch);
        }

        protected override IOperatorDto Visit_Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FactorOperatorDto);

            return Process_StretchOrSquash_ZeroOrigin(dto, StretchOrSquashEnum.Stretch);
        }

        /// <summary> Assumes all inlets except the signal inlet were already pushed onto the stack. </summary>
        private IOperatorDto Process_StretchOrSquash_WithOrigin(IOperatorDto_VarSignal_WithDimension dto, StretchOrSquashEnum stretchOrSquashEnum)
        {
            string factor = _stack.Pop();
            string origin = _stack.Pop();
            string sourcePosition = GetPositionNameCamelCase(dto);
            string destPosition = GetPositionNameCamelCase(dto, dto.DimensionStackLevel + 1);
            string operatorSymbol = GetOperatorSymbol(stretchOrSquashEnum);

            AppendOperatorTitleComment(dto);
            AppendLine($"{destPosition} = ({sourcePosition} - {origin}) {operatorSymbol} {factor} + {origin};");
            AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            _stack.Push(signal);
            return dto;
        }

        /// <summary> Assumes all inlets except the signal inlet were already pushed onto the stack. </summary>
        private IOperatorDto Process_StretchOrSquash_WithOriginShifting(IOperatorDto_VarSignal_WithDimension dto, StretchOrSquashEnum stretchOrSquashEnum)
        {
            string factor = _stack.Pop();
            string sourcePosition = GetPositionNameCamelCase(dto);
            string destPosition = GetPositionNameCamelCase(dto, dto.DimensionStackLevel + 1);
            string origin = GetLongLivedOriginName();
            string operatorSymbol = GetOperatorSymbol(stretchOrSquashEnum);

            AppendOperatorTitleComment(dto);
            AppendLineToReset($"{origin} = {sourcePosition};");
            AppendLine($"{destPosition} = ({sourcePosition} - {origin}) {operatorSymbol} {factor} + {origin};");
            AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            _stack.Push(signal);
            return dto;
        }

        /// <summary> Assumes all inlets except the signal inlet were already pushed onto the stack. </summary>
        private IOperatorDto Process_StretchOrSquash_WithPhaseTracking(IOperatorDto_VarSignal_WithDimension dto, StretchOrSquashEnum stretchOrSquashEnum)
        {
            string factor = _stack.Pop();
            string phase = GetLongLivedPhaseName();
            string previousPosition = GetLongLivedPreviousPositionName();
            string sourcePosition = GetPositionNameCamelCase(dto);
            string destPosition = GetPositionNameCamelCase(dto, dto.DimensionStackLevel + 1);
            string operatorSymbol = GetOperatorSymbol(stretchOrSquashEnum);

            AppendOperatorTitleComment(dto);
            string positionTranformationLine = $"{destPosition} = {phase} + ({sourcePosition} - {previousPosition}) {operatorSymbol} {factor};";

            AppendLineToCalculate(positionTranformationLine);
            AppendLineToCalculate($"{previousPosition} = {sourcePosition};");
            AppendLineToCalculate($"{phase} = {destPosition};");
                // I need two different variables for destPosition and phase, because destPosition is reused by different uses of the same stack level, while phase needs to be uniquely used by the operator instance.
            AppendLineToCalculate();

            AppendLineToReset($"{phase} = 0.0;");
            AppendLineToReset($"{previousPosition} = {sourcePosition};");
            AppendLineToReset(positionTranformationLine);
            AppendLineToReset();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            _stack.Push(signal);
            return dto;
        }

        /// <summary> Assumes all inlets except the signal inlet were already pushed onto the stack. </summary>
        private IOperatorDto Process_StretchOrSquash_ZeroOrigin(IOperatorDto_VarSignal_WithDimension dto, StretchOrSquashEnum stretchOrSquashEnum)
        {
            string factor = _stack.Pop();
            string sourcePosition = GetPositionNameCamelCase(dto);
            string destPosition = GetPositionNameCamelCase(dto, dto.DimensionStackLevel + 1);
            string operatorSymbol = GetOperatorSymbol(stretchOrSquashEnum);

            AppendOperatorTitleComment(dto);
            AppendLine($"{destPosition} = {sourcePosition} {operatorSymbol} {factor};");
            AppendLine();

            Visit_OperatorDto_Polymorphic(dto.SignalOperatorDto);
            string signal = _stack.Pop();

            _stack.Push(signal);
            return dto;
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto_ConstA_VarB(Subtract_OperatorDto_ConstA_VarB dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            PutNumberOnStack(dto.A);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto_VarA_ConstB(Subtract_OperatorDto_VarA_ConstB dto)
        {
            PutNumberOnStack(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto_VarA_VarB(Subtract_OperatorDto_VarA_VarB dto)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessBinaryOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous(
            SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset(
            SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto_VarPassThrough_VarReset(ToggleTrigger_OperatorDto_VarPassThrough_VarReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_NoOriginShifting(Triangle_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            string frequency = CompilationHelper.FormatValue(dto.Frequency);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, frequency);

            return Generate_TriangleCode_AfterDeterminePhase(dto, phase);
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_ConstFrequency_WithOriginShifting(Triangle_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            string frequency = CompilationHelper.FormatValue(dto.Frequency);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithOriginShifting(dto, frequency);

            return Generate_TriangleCode_AfterDeterminePhase(dto, phase);
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_NoPhaseTracking(Triangle_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            AppendOperatorTitleComment(dto);

            string frequency = _stack.Pop();

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, frequency);

            return Generate_TriangleCode_AfterDeterminePhase(dto, phase);
        }

        protected override IOperatorDto Visit_Triangle_OperatorDto_VarFrequency_WithPhaseTracking(Triangle_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            AppendOperatorTitleComment(dto);

            string frequency = _stack.Pop();

            string phase = GeneratePhaseCalculationWithPhaseTracking(dto, frequency);

            // TODO: You could prevent the first addition in the code written in the method called here,
            // by initializing phase with 0.5 for at the beginning of the chunk calculation.

            return Generate_TriangleCode_AfterDeterminePhase(dto, phase);
        }

        /// <summary> Returns output variable name. </summary>
        private IOperatorDto Generate_TriangleCode_AfterDeterminePhase(IOperatorDto dto, string phase)
        {
            string shiftedPhase = GetUniqueLocalVariableName(nameof(shiftedPhase));
            string relativePhase = GetUniqueLocalVariableName(nameof(relativePhase));
            string output = GetLocalOutputName(dto);

            // Correct the phase shift, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            AppendLine($"double {shiftedPhase} = {phase} + 0.25;");
            AppendLine($"double {relativePhase} = {shiftedPhase} % 1.0;");
            AppendLine($"double {output};");
            // Starts going up at a rate of 2 up over 1/2 a cycle.
            AppendLine($"if ({relativePhase} < 0.5) {output} = -1.0 + 4.0 * {relativePhase};");
            // And then going down at phase 1/2.
            // (Extending the line to x = 0 it ends up at y = 3.)
            AppendLine($"else {output} = 3.0 - 4.0 * {relativePhase};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            string inputVariable = GetInputName(dto);

            _stack.Push(inputVariable);

            return dto;
        }

        // Generalized Methods

        private IOperatorDto Process_Filter_OperatorDto_AllVars(OperatorDtoBase_Filter_VarSound dto, string biQuadFilterSetFilterVariablesMethodName)
        {
            Visit_OperatorDto_Base(dto);

            AppendOperatorTitleComment(dto);

            string sound = _stack.Pop();
            string frequency = _stack.Pop();
            IList<string> additionalFilterParameters = dto.InputOperatorDtos.Skip(2).Select(x => _stack.Pop()).ToArray();
            string output = GetLocalOutputName(dto);

            string x1 = GetUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x1)}");
            string x2 = GetUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x2)}");
            string y1 = GetUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y1)}");
            string y2 = GetUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y2)}");
            string a0 = GetUniqueLocalVariableName($"{dto.OperatorTypeEnum}{nameof(a0)}");
            string a1 = GetUniqueLocalVariableName($"{dto.OperatorTypeEnum}{nameof(a1)}");
            string a2 = GetUniqueLocalVariableName($"{dto.OperatorTypeEnum}{nameof(a2)}");
            string a3 = GetUniqueLocalVariableName($"{dto.OperatorTypeEnum}{nameof(a3)}");
            string a4 = GetUniqueLocalVariableName($"{dto.OperatorTypeEnum}{nameof(a4)}");

            string nyquistFrequency = CompilationHelper.FormatValue(dto.NyquistFrequency);
            string samplingRate = CompilationHelper.FormatValue(dto.SamplingRate);
            string limitedFrequency = GetUniqueLocalVariableName(nameof(limitedFrequency));
            const string biQuadFilterClassName = nameof(BiQuadFilterWithoutFields);
            string setFilterVariablesMethodName = biQuadFilterSetFilterVariablesMethodName;
            const string transformMethodName = nameof(BiQuadFilterWithoutFields.Transform);
            string concatinatedAdditionalFilterParameters = string.Join(", ", additionalFilterParameters);

            AppendFilterReset(x1, y1, x2, y2);

            AppendLine($"double {limitedFrequency} = {frequency};");
            AppendLine($"if ({limitedFrequency} > {nyquistFrequency}) {limitedFrequency} = {nyquistFrequency};");
            AppendLine();
            AppendLine($"double {a0}, {a1}, {a2}, {a3}, {a4};");
            AppendLine();
            AppendLine($"{biQuadFilterClassName}.{setFilterVariablesMethodName}(");
            Indent();
            {
                AppendLine($"{samplingRate}, {limitedFrequency}, {concatinatedAdditionalFilterParameters}, ");
                AppendLine($"out {a0}, out {a1}, out {a2}, out {a3}, out {a4});");
                Unindent();
            }
            AppendLine();
            AppendLine($"double {output} = {biQuadFilterClassName}.{transformMethodName}(");
            {
                Indent();
                AppendLine($"{sound}, {a0}, {a1}, {a2}, {a3}, {a4},");
                AppendLine($"ref {x1}, ref {x2}, ref {y1}, ref {y2});");
                Unindent();
            }

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto Process_Filter_OperatorDto_ManyConsts(OperatorDtoBase_Filter_ManyConsts dto)
        {
            Visit_OperatorDto_Polymorphic(dto.SoundOperatorDto);

            AppendOperatorTitleComment(dto);

            string sound = _stack.Pop();

            string x1 = GetUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x1)}");
            string x2 = GetUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x2)}");
            string y1 = GetUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y1)}");
            string y2 = GetUniqueLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y2)}");
            string output = GetLocalOutputName(dto);

            string a0 = CompilationHelper.FormatValue(dto.A0);
            string a1 = CompilationHelper.FormatValue(dto.A1);
            string a2 = CompilationHelper.FormatValue(dto.A2);
            string a3 = CompilationHelper.FormatValue(dto.A3);
            string a4 = CompilationHelper.FormatValue(dto.A4);

            const string biQuadFilterClassName = nameof(BiQuadFilterWithoutFields);
            const string transformMethodName = nameof(BiQuadFilterWithoutFields.Transform);

            AppendFilterReset(x1, y1, x2, y2);

            AppendLine($"double {output} = {biQuadFilterClassName}.{transformMethodName}(");
            {
                Indent();
                AppendLine($"{sound}, {a0}, {a1}, {a2}, {a3}, {a4},");
                AppendLine($"ref {x1}, ref {x2}, ref {y1}, ref {y2});");
                Unindent();
            }

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto Process_Math_Pow(IOperatorDto dto)
        {
            AppendOperatorTitleComment(dto);

            string @base = _stack.Pop();
            string exponent = _stack.Pop();
            string variable = GetLocalOutputName(dto);

            AppendLine($"double {variable} = Math.Pow({@base}, {exponent});");

            return GenerateOperatorWrapUp(dto, variable);
        }

        private IOperatorDto ProcessBinaryOperator(IOperatorDto dto, string operatorSymbol)
        {
            AppendOperatorTitleComment(dto);

            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"double {output} = {a} {operatorSymbol} {b};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessComparativeOperator(IOperatorDto dto, string operatorSymbol)
        {
            AppendOperatorTitleComment(dto);

            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"bool {output} = {a} {operatorSymbol} {b};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessComparativeOperator_VarA_ConstB(OperatorDtoBase_VarA_ConstB dto, string operatorSymbol)
        {
            PutNumberOnStack(dto.B);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessComparativeOperator(dto, operatorSymbol);
        }

        private IOperatorDto ProcessComparativeOperator_VarA_VarB(OperatorDtoBase_VarA_VarB dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            return ProcessComparativeOperator(dto, operatorSymbol);
        }

        private IOperatorDto ProcessLogicalBinaryOperator(OperatorDtoBase_VarA_VarB dto, string operatorSymbol)
        {
            Visit_OperatorDto_Polymorphic(dto.BOperatorDto);
            Visit_OperatorDto_Polymorphic(dto.AOperatorDto);

            AppendOperatorTitleComment(dto);

            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GetLocalOutputName(dto);

            AppendLine($"bool {output} = {a} {operatorSymbol} {b};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto Process_MinOrMaxOverInlets_MoreThan2Inlets(IOperatorDto dto, MinOrMaxEnum minOrMaxEnum, int varCount)
        {
            AppendOperatorTitleComment(dto);

            string firstValue = _stack.Pop();
            string output = GetLocalOutputName(dto);
            string operatorSymbol = GetOperatorSymbol(minOrMaxEnum);

            AppendLine($"double {output} = {firstValue};");

            // NOTE: i = 1.
            for (int i = 1; i < varCount; i++)
            {
                string item = _stack.Pop();

                AppendLine($"if ({output} {operatorSymbol} {item}) {output} = {item};");
            }

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto Process_MinOrMaxOverInlets_With2Inlets(IOperatorDto dto, MinOrMaxEnum minOrMaxEnum)
        {
            AppendOperatorTitleComment(dto);

            string a = _stack.Pop();
            string b = _stack.Pop();
            string output = GetLocalOutputName(dto);
            string operatorSymbol = GetOperatorSymbol(minOrMaxEnum);

            AppendLine($"double {output} = {a} {operatorSymbol} {b} ? {a} : {b};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessMultiVarOperator(IOperatorDto dto, int varCount, string operatorSymbol)
        {
            AppendOperatorTitleComment(dto);

            string output = GetLocalOutputName(dto);

            AppendTabs();
            Append($"double {output} =");

            for (int i = 0; i < varCount; i++)
            {
                string value = _stack.Pop();

                Append(' ');
                Append(value);

                bool isLast = i == varCount - 1;
                if (isLast)
                {
                    break;
                }

                Append(' ');
                Append(operatorSymbol);
            }

            Append(';');
            Append(Environment.NewLine);

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessMultiVarOperator_Vars_1Const(OperatorDtoBase_Vars_1Const dto, string operatorSymbol)
        {
            PutNumberOnStack(dto.ConstValue);
            dto.Vars.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return ProcessMultiVarOperator(dto, dto.Vars.Count + 1, operatorSymbol);
        }

        private IOperatorDto ProcessMultiVarOperator_Vars_NoConsts(OperatorDtoBase_Vars dto, string operatorSymbol)
        {
            dto.Vars.Reverse().ForEach(x => Visit_OperatorDto_Polymorphic(x));

            return ProcessMultiVarOperator(dto, dto.Vars.Count, operatorSymbol);
        }

        private IOperatorDto ProcessOriginShifter(OperatorDtoBase_ConstFrequency dto, Func<string, string> getRightHandFormulaDelegate)
        {
            string frequency = CompilationHelper.FormatValue(dto.Frequency);

            AppendOperatorTitleComment(dto);

            string phase = GeneratePhaseCalculationWithOriginShifting(dto, frequency);
            string output = GetLocalOutputName(dto);
            string rightHandFormula = getRightHandFormulaDelegate(phase);

            AppendLine($"double {output} = {rightHandFormula};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessPhaseTrackingOperator(OperatorDtoBase_VarFrequency dto, Func<string, string> getRightHandFormulaDelegate)
        {
            Visit_OperatorDto_Polymorphic(dto.FrequencyOperatorDto);

            AppendOperatorTitleComment(dto);

            string frequency = _stack.Pop();
            string phase = GeneratePhaseCalculationWithPhaseTracking(dto, frequency);
            string output = GetLocalOutputName(dto);
            string rightHandFormula = getRightHandFormulaDelegate(phase);

            AppendLine($"double {output} = {rightHandFormula};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessWithFrequency_WithoutPhaseTrackingOrOriginShifting(IOperatorDto_WithDimension dto, Func<string, string> getRightHandFormulaDelegate)
        {
            AppendOperatorTitleComment(dto);

            string frequency = _stack.Pop();

            string phase = GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(dto, frequency);

            string rightHandFormula = getRightHandFormulaDelegate(phase);
            string output = GetLocalOutputName(dto);
            AppendLine($"double {output} = {rightHandFormula};");

            return GenerateOperatorWrapUp(dto, output);
        }

        // StringBuilder Helpers

        private void AppendLine(string line = null)
        {
            AppendLineToCalculate(line);
            AppendLineToReset(line);
        }

        private void AppendLineToCalculate(string line = null) => TryGetStringBuilderForWritingCalculation()?.AppendLine(line);
        private void AppendLineToReset(string line = null) => TryGetStringBuilderForWritingReset()?.AppendLine(line);

        private void Indent()
        {
            IndentCalculate();
            IndentReset();
        }

        private void IndentCalculate() => TryGetStringBuilderForWritingCalculation()?.Indent();
        private void IndentReset() => TryGetStringBuilderForWritingReset()?.Indent();

        private void Unindent()
        {
            UnindentCalculate();
            UnindentReset();
        }

        private void UnindentCalculate() => TryGetStringBuilderForWritingCalculation()?.Unindent();
        private void UnindentReset() => TryGetStringBuilderForWritingReset()?.Unindent();

        private void Append(char chr)
        {
            AppendCalculate(chr);
            AppendReset(chr);
        }

        private void AppendCalculate(char chr) => TryGetStringBuilderForWritingCalculation()?.Append(chr);
        private void AppendReset(char chr) => TryGetStringBuilderForWritingReset()?.Append(chr);

        private void Append(string text)
        {
            AppendCalculate(text);
            AppendReset(text);
        }

        private void AppendCalculate(string text) => TryGetStringBuilderForWritingCalculation()?.Append(text);
        private void AppendReset(string text) => TryGetStringBuilderForWritingReset()?.Append(text);

        private void AppendTabs()
        {
            AppendTabsCalculate();
            AppendTabsReset();
        }

        private void AppendTabsCalculate() => TryGetStringBuilderForWritingCalculation()?.AppendTabs();
        private void AppendTabsReset() => TryGetStringBuilderForWritingReset()?.AppendTabs();

        private StringBuilderWithIndentation TryGetStringBuilderForWritingCalculation()
        {
            // Hold operator just does the whole calculation in the Reset method.
            bool holdOperatorIsActive = _holdOperatorIsActiveStack.Peek();
            if (holdOperatorIsActive)
            {
                return _stringBuilderForReset;
            }

            // When you are in a separate generated method, you have to write to that.
            GeneratedMethodInfo generatedMethodInfo = _generatedMethodInfoStack.PeekOrDefault();
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (generatedMethodInfo != null)
            {
                return generatedMethodInfo.MethodBodyStringBuilderForCalculate;
            }

            // Otherwise, write to the main calculate method.
            return _stringBuilderForCalculate;
        }
        
        private StringBuilderWithIndentation TryGetStringBuilderForWritingReset()
        {
            bool holdOperatorIsActive = _holdOperatorIsActiveStack.Peek();
            if (holdOperatorIsActive)
            {
                return null;
            }

            // When you are in a separate generated method, you have to write to that.
            GeneratedMethodInfo generatedMethodInfo = _generatedMethodInfoStack.PeekOrDefault();
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (generatedMethodInfo != null)
            {
                return generatedMethodInfo.MethodBodyStringBuilderForReset;
            }

            return _stringBuilderForReset;
        }

        /// <param name="operatorID">key for a unique method</param>
        private void BeginGenerateMethod(int operatorID, object mnemonic)
        {
            switch (_calculationMethodEnum)
            {
                case CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters:
                    GeneratedMethodInfo generatedMethodInfo;
                    if (!_operatorID_To_GeneratedMethodInfo_Dictionary.TryGetValue(operatorID, out generatedMethodInfo))
                    {
                        generatedMethodInfo = new GeneratedMethodInfo
                        {
                            MethodNameForCalculate = GetMethodName($"{CALCULATE_MNEMONIC}{mnemonic}"),
                            MethodNameForReset = GetMethodName($"{RESET_MNEMONIC}{mnemonic}"),
                        };

                        _operatorID_To_GeneratedMethodInfo_Dictionary[operatorID] = generatedMethodInfo;
                    }
                    else
                    {
                        // For an already written method, create a new argument list.
                        generatedMethodInfo.VariableInfo = new VariableCollections();
                    }

                    _generatedMethodInfoStack.Push(generatedMethodInfo);
                    break;

                case CalculationMethodEnum.Roslyn:
                    break;

                default:
                    throw new ValueNotSupportedException(_calculationMethodEnum);
            }
        }

        /// <summary> Returns a right side for a variable assignment. </summary>
        private string EndGenerateMethod(string expressionToReturn)
        {
            switch (_calculationMethodEnum)
            {
                case CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters:

                    GeneratedMethodInfo generatedMethodInfo = _generatedMethodInfoStack.Peek();
                    IList<GeneratedParameterInfo> generatedParameterInfos = generatedMethodInfo.GetGeneratedParameterInfos();

                    if (!generatedMethodInfo.MethodGenerationIsComplete)
                    {
                        generatedMethodInfo.MethodGenerationIsComplete = true;

                        // Build Parameter List Code
                        string parameterListCode;
                        {
                            var sb = new StringBuilderWithIndentation(TAB_STRING) { IndentLevel = MULTI_LINE_PARAMETER_LIST_INDENT_LEVEL };

                            foreach (GeneratedParameterInfo generatedParameterInfo in generatedParameterInfos)
                            {
                                if (_calculationMethodEnum == CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters)
                                {
                                    sb.AppendLine($"{generatedParameterInfo.TypeName} {generatedParameterInfo.NameCamelCase},");

                                    sb.AppendTabs();
                                    sb.Append($"out {generatedParameterInfo.TypeName} {generatedParameterInfo.NameCamelCase}_out");
                                        // DIRTY: "_out" suffix does not create ambiguity, but only by accident because the last part of an identifier is always a number.
                                }
                                else if (_calculationMethodEnum == CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters)
                                {
                                    sb.AppendTabs();
                                    sb.Append($"ref {generatedParameterInfo.TypeName} {generatedParameterInfo.NameCamelCase}");
                                }
                                else
                                {
                                    throw new ValueNotSupportedException(_calculationMethodEnum);
                                }

                                bool isLast = generatedParameterInfo == generatedParameterInfos.Last();
                                if (!isLast)
                                {
                                    sb.Append(',');
                                }
                                else
                                {
                                    sb.Append(')');
                                }

                                sb.AppendLine();
                            }
                            parameterListCode = sb.ToString();
                        }

                        // Write last bit of raw method code.
                        if (_calculationMethodEnum == CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters)
                        {
                            foreach (GeneratedParameterInfo generatedParameterInfo in generatedParameterInfos)
                            {
                                AppendLine($"{generatedParameterInfo.NameCamelCase}_out = {generatedParameterInfo.NameCamelCase};");
                                    // DIRTY: "_out" suffix does not create ambiguity, but only by accident because the last part of an identifier is always a number.
                            }
                            AppendLine();
                        }

                        AppendLine($"return {expressionToReturn};");

                        // Wrap raw Calculate code in a method.
                        {
                            var sb = new StringBuilderWithIndentation(TAB_STRING) { IndentLevel = METHOD_SIGNATURE_INDENT_LEVEL };
                            sb.AppendLine("[MethodImpl(MethodImplOptions.NoInlining)]");
                            sb.AppendLine($"private double {generatedMethodInfo.MethodNameForCalculate}(");
                            sb.Append(parameterListCode);
                            sb.AppendLine("{");
                            sb.Indent();
                            {
                                sb.Append(generatedMethodInfo.MethodBodyStringBuilderForCalculate.ToString());
                                sb.Unindent();
                            }
                            sb.AppendLine("}");
                            generatedMethodInfo.MethodCodeForCalculate = sb.ToString();
                        }

                        // Wrap raw Reset code in a method.
                        {
                            var sb = new StringBuilderWithIndentation(TAB_STRING) { IndentLevel = METHOD_SIGNATURE_INDENT_LEVEL };
                            sb.AppendLine($"private double {generatedMethodInfo.MethodNameForReset}(");
                            sb.Append(parameterListCode);
                            sb.AppendLine("{");
                            sb.Indent();
                            {
                                sb.Append(generatedMethodInfo.MethodBodyStringBuilderForReset.ToString());
                                sb.Unindent();
                            }
                            sb.AppendLine("}");
                            generatedMethodInfo.MethodCodeForReset = sb.ToString();
                        }
                    }

                    // Pop to parent level.
                    _generatedMethodInfoStack.Pop();

                    // Write method calls
                    string argumentListCode;
                    if (_calculationMethodEnum == CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters)
                    {
                        argumentListCode = string.Join(
                            ", ",
                            generatedMethodInfo.GetGeneratedParameterInfos().Select(x => $"{x.NameCamelCase}, out {x.NameCamelCase}"));
                    }
                    else if (_calculationMethodEnum == CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters)
                    {
                        argumentListCode = string.Join(
                            ", ",
                            generatedMethodInfo.GetGeneratedParameterInfos().Select(x => $"ref {x.NameCamelCase}"));
                    }
                    else
                    {
                        throw new ValueNotSupportedException(_calculationMethodEnum);
                    }

                    string output = GetUniqueLocalVariableName(generatedMethodInfo.MethodNameForCalculate);

                    AppendLineToCalculate($"double {output} = {generatedMethodInfo.MethodNameForCalculate}({argumentListCode});");
                    AppendLineToReset($"double {output} = {generatedMethodInfo.MethodNameForReset}({argumentListCode});");

                    return output;

                case CalculationMethodEnum.Roslyn:
                    return expressionToReturn;

                default:
                    throw new ValueNotSupportedException(_calculationMethodEnum);
            }
        }

        // Helpers

        private void AppendFilterReset(string x1, string x2, string y1, string y2)
        {
            AppendLineToReset($"{x1} = 0;");
            AppendLineToReset($"{x2} = 0;");
            AppendLineToReset($"{y1} = 0;");
            AppendLineToReset($"{y2} = 0;");
        }

        private void AppendOperatorTitleComment(IOperatorDto dto)
        {
            string line = GetOperatorTitleComment(dto);
            AppendLine(line);
        }

        private string Convert_DisplayName_To_NonUniqueNameInCode_WithoutUnderscores(string arbitraryString)
        {
            if (string.IsNullOrWhiteSpace(arbitraryString)) throw new NullOrWhiteSpaceException(() => arbitraryString);

            string convertedName = NameHelper.ToCanonical(arbitraryString).ToCamelCase().Replace("_", "");
            return convertedName;
        }

        /// <summary> Appends an empty line, pushes the output and returns dto casted to IOperatorDto. </summary>
        private IOperatorDto GenerateOperatorWrapUp(IOperatorDto dto, string output)
        {
            AppendLine();
            _stack.Push(output);
            return dto;
        }

        /// <summary> Returns the phase literal. </summary>
        private string GeneratePhaseCalculationNoPhaseTrackingOrOriginShifting(IOperatorDto_WithDimension dto, string rate)
        {
            string position = GetPositionNameCamelCase(dto);
            string phase = GetLocalPhaseName();

            AppendLine($"double {phase} = {position} * {rate};");

            return phase;
        }

        /// <summary> Returns the phase literal. </summary>
        private string GeneratePhaseCalculationWithOriginShifting(IOperatorDto_WithDimension dto, string rate)
        {
            string position = GetPositionNameCamelCase(dto);
            string origin = GetLongLivedOriginName();
            string phase = GetLocalPhaseName();

            AppendLineToReset($"{origin} = {position};");

            AppendLine($"double {phase} = ({position} - {origin}) * {rate};");

            return phase;
        }

        /// <summary> Returns the phase literal. </summary>
        private string GeneratePhaseCalculationWithPhaseTracking(IOperatorDto_WithDimension dto, string rate)
        {
            string position = GetPositionNameCamelCase(dto);
            string previousPosition = GetLongLivedPreviousPositionName();
            string phase = GetLongLivedPhaseName();

            AppendLineToReset($"{previousPosition} = {position};");
            AppendLineToReset($"{phase} = 0.0;");

            AppendLineToCalculate($"{phase} += ({position} - {previousPosition}) * {rate};");
            AppendLineToCalculate($"{previousPosition} = {position};");

            return phase;
        }

        private string GetRandomOrNoiseOffsetVariableNameCamelCase(int operatorID)
        {
            // ReSharper disable once InvertIf
            if (!_variableInfo.RandomOrNoiseOperatorID_To_OffsetVariableNameCamelCase_Dictionary.TryGetValue(operatorID, out string variableNameCamelCase))
            {
                variableNameCamelCase = GetUniqueLongLivedVariableName(OFFSET_MNEMONIC);
            }

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.RandomOrNoiseOperatorID_To_OffsetVariableNameCamelCase_Dictionary[operatorID] = variableNameCamelCase;
            }

            return variableNameCamelCase;
        }

        private string GetArrayCalculatorVariableNameCamelCaseAndCache(ArrayDto arrayDto)
        {
            // ReSharper disable once InvertIf
            if (!_variableInfo.ArrayDto_To_ArrayCalculationInfo_Dictionary.TryGetValue(arrayDto, out ArrayCalculationInfo arrayCalculationInfo))
            {
                // Do not call GenerateUniqueLongLivedVariableName. In a later stage those are all assumed to be double variables. 
                // The array calculator variables are not doubles.
                string nameCamelCase = GetUniqueVariableNameCamelCase(ARRAY_CALCULATOR_MNEMONIC);

                ICalculatorWithPosition arrayCalculator = ArrayCalculatorFactory.CreateArrayCalculator(arrayDto);

                arrayCalculationInfo = new ArrayCalculationInfo
                {
                    NameCamelCase = nameCamelCase,
                    TypeName = arrayCalculator.GetType().Name,
                    ArrayDto = arrayDto
                };
            }

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.ArrayDto_To_ArrayCalculationInfo_Dictionary[arrayDto] = arrayCalculationInfo;
            }

            return arrayCalculationInfo.NameCamelCase;
        }

        /// <summary>
        /// Formats the dimension into a string that is close to the dimension name + a unique character sequence.
        /// E.g.: "prettiness_1"
        /// </summary>
        private string GetDimensionAlias(DimensionEnum dimensionEnum, string canonicalCustomDimensionName)
        {
            var key = new Tuple<DimensionEnum, string>(dimensionEnum, canonicalCustomDimensionName);

            if (!_variableInfo.StandardDimensionEnumAndCanonicalCustomDimensionName_To_Alias_Dictionary.TryGetValue(key, out string alias))
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
                alias = GetUniqueDimensionAlias(mnemonic);
            }

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.StandardDimensionEnumAndCanonicalCustomDimensionName_To_Alias_Dictionary[key] = alias;
            }

            return alias;
        }

        private string GetInputName(VariableInput_OperatorDto dto)
        {
            ExtendedVariableInfo inputVariableInfo = null;

            if (_variableInfo.VariableInput_OperatorDto_To_VariableName_Dictionary.TryGetValue(dto, out string variableName))
            {
                _variableInfo.VariableName_To_InputVariableInfo_Dictionary.TryGetValue(variableName, out inputVariableInfo);
            }

            if (string.IsNullOrEmpty(variableName) || inputVariableInfo == null)
            {
                object mnemonic;
                if (dto.DimensionEnum != DimensionEnum.Undefined)
                {
                    mnemonic = dto.DimensionEnum;
                }
                else if (!string.IsNullOrEmpty(dto.CanonicalName))
                {
                    mnemonic = dto.CanonicalName;
                }
                else
                {
                    mnemonic = DEFAULT_INPUT_MNEMONIC;
                }

                variableName = GetUniqueVariableNameCamelCase(mnemonic);
                inputVariableInfo = new ExtendedVariableInfo(variableName, dto.CanonicalName, dto.DimensionEnum, dto.Position, dto.DefaultValue);
            }

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.VariableInput_OperatorDto_To_VariableName_Dictionary[dto] = variableName;
                variableInfo.VariableName_To_InputVariableInfo_Dictionary[variableName] = inputVariableInfo;
            }

            return variableName;
        }

        private string GetLiteralFromOperatorDtoOrValue(IOperatorDto valueOperatorDto = null, double? value = null)
        {
            bool xor = valueOperatorDto != null ^ value.HasValue;
            if (!xor)
            {
                throw new Exception($"Either {nameof(value)} or {nameof(valueOperatorDto)} must be filled in, but not both at the same time.");
            }

            if (valueOperatorDto != null)
            {
                Visit_OperatorDto_Polymorphic(valueOperatorDto);
                string literal = _stack.Pop();
                return literal;
            }
            else
            {
                return CompilationHelper.FormatValue(value.Value);
            }
        }

        private string GetLocalOutputName(IOperatorDto dto)
        {
            return GetUniqueVariableNameCamelCase(dto.OperatorTypeEnum);
        }

        private string GetLocalPhaseName()
        {
            string variableName = GetUniqueVariableNameCamelCase(PHASE_MNEMONIC);
            return variableName;
        }

        private string GetLongLivedOriginName() => GetUniqueLongLivedVariableName(ORIGIN_MNEMONIC);
        private string GetLongLivedPhaseName() => GetUniqueLongLivedVariableName(PHASE_MNEMONIC);
        private string GetLongLivedPreviousPositionName() => GetUniqueLongLivedVariableName(PREVIOUS_POSITION_MNEMONIC);

        /// <summary>'/' for Stretch, '*' for Squash</summary>
        private string GetOperatorSymbol(StretchOrSquashEnum stretchOrSquashEnum)
        {
            switch (stretchOrSquashEnum)
            {
                case StretchOrSquashEnum.Stretch:
                    return DIVIDE_SYMBOL;

                case StretchOrSquashEnum.Squash:
                    return MULTIPLY_SYMBOL;

                default:
                    throw new ValueNotSupportedException(stretchOrSquashEnum);
            }
        }

        /// <summary> '&gt;' for Min, '&lt;' for Max </summary>
        private string GetOperatorSymbol(MinOrMaxEnum minOrMaxEnum)
        {
            switch (minOrMaxEnum)
            {
                case MinOrMaxEnum.Min:
                    return GREATER_THAN_SYMBOL;

                case MinOrMaxEnum.Max:
                    return LESS_THAN_SYMBOL;

                default:
                    throw new ValueNotSupportedException(minOrMaxEnum);
            }
        }

        private string GetOperatorTitleComment(IOperatorDto dto)
        {
            string generalIdentifier = dto.OperatorTypeEnum.ToString();
            string variationIdentifier = dto.GetType().Name.Replace("_OperatorDto", "").Replace($"{generalIdentifier}_", "");
            if (string.Equals(generalIdentifier, variationIdentifier))
            {
                string line = $"// {generalIdentifier}";
                return line;
            }
            else
            {
                string line = $"// {generalIdentifier} ({variationIdentifier})";
                return line;
            }
        }

        /// <summary>
        /// If it is the first level dimension position, the variable will be long lived.
        /// Higher level dimension positions will become local variables.
        /// </summary>
        private string GetPositionNameCamelCase(IOperatorDto_WithDimension dto, int? alternativeStackIndexLevel = null)
        {
            string canonicalCustomDimensionName = dto.CanonicalCustomDimensionName;
            DimensionEnum standardDimensionEnum = dto.StandardDimensionEnum;
            int stackLevel = alternativeStackIndexLevel ?? dto.DimensionStackLevel;

            return GetPositionNameCamelCase(stackLevel, standardDimensionEnum, canonicalCustomDimensionName);
        }

        /// <summary>
        /// If it is the first level dimension position, the variable will be long lived.
        /// Higher level dimension positions will become local variables.
        /// </summary>
        private string GetPositionNameCamelCase(int stackLevel, DimensionEnum standardDimensionEnum = DimensionEnum.Undefined, string canonicalCustomDimensionName = "")
        {
            // Get DimensionAlias
            string dimensionAlias = GetDimensionAlias(standardDimensionEnum, canonicalCustomDimensionName);

            // Format PositionVariableNAme
            string positionVariableName = $"{dimensionAlias}_{stackLevel}";

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.PositionVariableNamesCamelCaseHashSet.Add(positionVariableName);
            }

            // Manage Dictionary with Dimension Info
            var key = new Tuple<DimensionEnum, string, int>(standardDimensionEnum, canonicalCustomDimensionName, stackLevel);

            if (!_variableInfo.DimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary.TryGetValue(key, out ExtendedVariableInfo extendedVariableInfo))
            {
                extendedVariableInfo = new ExtendedVariableInfo(
                    positionVariableName,
                    canonicalCustomDimensionName,
                    standardDimensionEnum,
                    stackLevel,
                    defaultValue: null);
            }

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.DimensionEnumCustomDimensionNameAndStackLevel_To_DimensionVariableInfo_Dictionary[key] = extendedVariableInfo;
            }

            return positionVariableName;
        }

        private string GetUniqueDimensionAlias(object mnemonic)
        {
            string convertedMnemonic = Convert.ToString(mnemonic);
            if (string.IsNullOrWhiteSpace(convertedMnemonic))
            {
                convertedMnemonic = ANONYMOUS_MNEMONIC;
            }

            string nonUniqueNameInCode = Convert_DisplayName_To_NonUniqueNameInCode_WithoutUnderscores(convertedMnemonic);
            string uniqueLetterSequence = GetUniqueLetterSequence();

            string variableName = $"{nonUniqueNameInCode}_{uniqueLetterSequence}";
            return variableName;
        }

        private string GetUniqueLetterSequence()
        {
            return NumberingSystems.ToLetterSequence(_counter++, firstChar: 'a', lastChar: 'z');
        }

        private string GetUniqueLocalVariableName(object mnemonic) => GetUniqueVariableNameCamelCase(mnemonic);

        /// <param name="mnemonic">
        /// Will be incorporated into the variable name. It will be converted to string.
        /// It will also be put into a (non-unique) form that will be valid in C#.
        /// Also underscores are removed from it, because that is a separator character in our variable names.
        /// </param>
        private string GetUniqueVariableNameCamelCase(object mnemonic)
        {
            string nonUniqueNameInCode = Convert_DisplayName_To_NonUniqueNameInCode_WithoutUnderscores(Convert.ToString(mnemonic));
            int uniqueNumber = GetUniqueNumber();

            string variableName = $"{nonUniqueNameInCode}_{uniqueNumber}";
            return variableName;
        }

        private string GetUniqueLongLivedVariableName(object mnemonic)
        {
            string variableName = GetUniqueVariableNameCamelCase(mnemonic);

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.LongLivedDoubleVariableNamesCamelCase.Add(variableName);
            }

            return variableName;
        }

        private string GetLongLivedDoubleArrayVariableName(int arrayLength)
        {
            string variableName = GetUniqueVariableNameCamelCase(ARRAY_MNEMONIC);

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.LongLivedDoubleArrayVariableInfos.Add(new DoubleArrayVariableInfo { NameCamelCase = variableName, ArrayLength = arrayLength });
            }

            return variableName;
        }

        private string GetMethodName(object mnemonic)
        {
            string uniqueNameCamelCase = GetUniqueVariableNameCamelCase(mnemonic);
            string uniqueNamePascalCase = uniqueNameCamelCase.Left(1).ToUpper() + uniqueNameCamelCase.TrimStart(1);
            return uniqueNamePascalCase;
        }

        private int GetUniqueNumber() => _counter++;

        private void PutNumberOnStack(double value)
        {
            _stack.Push(CompilationHelper.FormatValue(value));
        }

        private IList<VariableCollections> GetVariableInfoList()
        {
            IList<VariableCollections> list = _generatedMethodInfoStack.Select(x => x.VariableInfo).Union(_variableInfo).ToArray();
            return list;
        }
    }
}