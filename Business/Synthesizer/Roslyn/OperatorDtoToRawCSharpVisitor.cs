using System;
using System.Collections.Generic;
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
using JJ.Framework.Configuration;

namespace JJ.Business.Synthesizer.Roslyn
{
    internal class OperatorDtoToRawCSharpVisitor : OperatorDtoVisitorBase_AfterDtoPreprocessing
    {
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum MinOrMaxEnum
        {
            Undefined,
            Min,
            Max
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
        private const string REMAINDER_SYMBOL = "%";
        private const string SUBTRACT_SYMBOL = "-";

        private const string ARRAY_CALCULATOR_MNEMONIC = "arraycalculator";
        private const string ARRAY_MNEMONIC = "array";
        private const string CALCULATE_MNEMONIC = "calculate";
        private const string DEFAULT_INPUT_MNEMONIC = "input";
        private const string OFFSET_MNEMONIC = "offset";
        private const string RESET_MNEMONIC = "reset";

        private static readonly CalculationMethodEnum _calculationMethodEnum = CustomConfigurationManager.GetSection<ConfigurationSection>().CalculationMethod;

        private readonly int _calculationIndentLevel;
        private readonly int _resetIndentLevel;

        private Stack<string> _stack;
        private StringBuilderWithIndentation _stringBuilderForCalculate;
        private StringBuilderWithIndentation _stringBuilderForReset;
        private int _counter;
        private Stack<bool> _holdOperatorIsActiveStack;

        private VariableCollections _variableInfo;
        private Dictionary<IOperatorDto, string> _resultReuse_Dictionary;

        // We need both a GeneratedMethodInfo list and a stack. A stack for where we are in the processing,
        // a list to not lose generated methods, when they are popped from the stack.

        private Stack<GeneratedMethodInfo> _generatedMethodInfoStack;
        private Dictionary<string, GeneratedMethodInfo> _operationIdentity_To_GeneratedMethodInfo_Dictionary;

        public OperatorDtoToRawCSharpVisitor(int calculationIndentLevel, int resetIndentLevel)
        {
            _calculationIndentLevel = calculationIndentLevel;
            _resetIndentLevel = resetIndentLevel;
            _variableInfo = new VariableCollections();
        }

        public OperatorDtoToCSharpVisitorResult Execute(IOperatorDto dto)
        {
            _stack = new Stack<string>();
            _variableInfo = new VariableCollections();
            _generatedMethodInfoStack = new Stack<GeneratedMethodInfo>();
            _operationIdentity_To_GeneratedMethodInfo_Dictionary = new Dictionary<string, GeneratedMethodInfo>();
            _resultReuse_Dictionary = new Dictionary<IOperatorDto, string>();
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

            Visit_OperatorDto_Polymorphic(dto);

            // Gather up output
            string rawCalculationCode = _stringBuilderForCalculate.ToString();
            string rawResetCode = _stringBuilderForReset.ToString();
            string returnValueLiteral = _stack.Pop();

            // Get some more variable info
            string firstTimeVariableNameCamelCase =
                _variableInfo.VariableName_To_InputVariableInfo_Dictionary
                             .Where(x => x.Value.DimensionEnum == DimensionEnum.Time && x.Value.Position == 0 && x.Value.CanonicalName == "")
                             .Select(x => x.Key)
                             .DefaultIfEmpty(GetLongLivedVariableName(nameof(DimensionEnum.Time)))
                             .First();

            IList<InputVariableInfo> inputVariableInfos = _variableInfo.VariableName_To_InputVariableInfo_Dictionary.Values.ToArray();
            IList<ArrayCalculationInfo> arrayCalculationInfos = _variableInfo.ArrayDto_To_ArrayCalculationInfo_Dictionary.Values.ToArray();

            // ReSharper disable once InvokeAsExtensionMethod
            IList<string> calculationMethodCodeList = _operationIdentity_To_GeneratedMethodInfo_Dictionary.Values.Select(x => x.MethodCodeForCalculate).ToArray();
            IList<string> resetMethodCodeList = _operationIdentity_To_GeneratedMethodInfo_Dictionary.Values.Select(x => x.MethodCodeForReset).ToArray();

            return new OperatorDtoToCSharpVisitorResult(
                rawCalculationCode,
                rawResetCode,
                returnValueLiteral,
                firstTimeVariableNameCamelCase,
                _variableInfo.LongLivedDoubleVariableNamesCamelCase,
                inputVariableInfos,
                arrayCalculationInfos,
                _variableInfo.LongLivedDoubleArrayVariableInfos,
                calculationMethodCodeList,
                resetMethodCodeList);
        }

        /*[DebuggerHidden]*/
        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            if (_resultReuse_Dictionary.TryGetValue(dto, out string variableName))
            {
                _stack.Push(variableName);
            }
            else
            {
                VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

                _resultReuse_Dictionary.Add(dto, _stack.Peek());
            }

            return dto;
        }

        protected override IOperatorDto Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
        {
            string number = GetLiteralFromInputDto(dto.Number);
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {number};");
            AppendLine($"if ({output} < 0.0) {output} = -{output};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            return ProcessMultiVarOperator(dto, PLUS_SYMBOL);
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetAllPassFilterVariables));
        }

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
        {
            return ProcessBinaryBoolOperator(dto, AND_SYMBOL);
        }

        protected override IOperatorDto Visit_AverageFollower_OperatorDto(AverageFollower_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_CollectionRecalculationContinuous(
            AverageOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_AverageOverDimension_OperatorDto_CollectionRecalculationUponReset(
            AverageOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_AverageOverInlets_OperatorDto(AverageOverInlets_OperatorDto dto)
        {
            IList<string> items = dto.Inputs.Select(x => GetLiteralFromInputDto(x)).ToArray();
            string sum = GetVariableName(nameof(sum));
            string output = GetVariableName(dto.OperatorTypeEnum);
            string countLiteral = CompilationHelper.FormatValue(items.Count);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {sum} = {string.Join(" + ", items)};");
            AppendLine($"double {output} = {sum} / {countLiteral};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst(
            BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar(
            BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetBandPassFilterConstantPeakGainVariables));
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst(
            BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar(
            BandPassFilterConstantTransitionGain_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetBandPassFilterConstantTransitionGainVariables));
        }

        protected override IOperatorDto Visit_BooleanToDouble_OperatorDto(BooleanToDouble_OperatorDto dto)
        {
            string input = GetLiteralFromInputDto(dto.Input);
            string output = GetVariableName(dto.OperatorTypeEnum);

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

        protected override IOperatorDto Visit_ChangeTrigger_OperatorDto(ChangeTrigger_OperatorDto dto)
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

        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
        {
            return Process_ClosestOverInlets(dto, isExp: false);
        }

        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
        {
            return Process_ClosestOverInlets(dto, isExp: true);
        }

        private IOperatorDto Process_ClosestOverInlets(ClosestOverInlets_OperatorDto dto, bool isExp)
        {
            string input = GetLiteralFromInputDto(dto.Input);
            IList<string> items = dto.Items.Select(x => GetLiteralFromInputDto(x)).ToArray();
            int itemCount = items.Count;
            string firstItem = items.First();
            string smallestDistance = GetVariableName(nameof(smallestDistance));
            string closestItem = GetVariableName(nameof(closestItem));
            string output = GetVariableName(dto.OperatorTypeEnum);
            string transformedInput = GetVariableName(nameof(transformedInput));
            string transformedFirstItem = GetVariableName(nameof(transformedFirstItem));
            const string geometry = nameof(Geometry);
            const string absoluteDistance = nameof(Geometry.AbsoluteDistance);

            AppendOperatorTitleComment(dto);

            if (isExp)
            {
                AppendLine($"double {transformedInput} = Math.Log({input});");
                AppendLine($"double {transformedFirstItem} = Math.Log({firstItem});");
            }
            else
            {
                AppendLine($"double {transformedInput} = {input};");
                AppendLine($"double {transformedFirstItem} = {firstItem};");
            }
            AppendLine();

            AppendLine($"double {smallestDistance} = {geometry}.{absoluteDistance}({transformedInput}, {transformedFirstItem});");
            AppendLine($"double {closestItem} = {firstItem};");
            AppendLine();

            // NOTE: i = 1.
            for (int i = 1; i < itemCount; i++)
            {
                string item = items[i];
                string transformedItem = GetVariableName(nameof(transformedItem));
                string distance = GetVariableName(nameof(distance));

                if (isExp)
                {
                    AppendLine($"double {transformedItem} = Math.Log({item});");
                }
                else
                {
                    AppendLine($"double {transformedItem} = {item};");
                }

                AppendLine($"double {distance} = {geometry}.{absoluteDistance}({transformedInput}, {transformedItem});");

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

        protected override IOperatorDto Visit_Curve_OperatorDto_NoOriginShifting(Curve_OperatorDto_NoOriginShifting dto)
        {
            string calculatorName = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDto);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string position = GetLiteralFromInputDto(dto.Position);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {calculatorName}.Calculate({position});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Curve_OperatorDto_WithOriginShifting(Curve_OperatorDto_WithOriginShifting dto)
        {
            string calculatorName = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDto);
            string position = GetLiteralFromInputDto(dto.Position);
            string origin = GetLongLivedVariableName(nameof(origin));
            string phase = GetVariableName(nameof(phase)); // Does not need to be long-lived.
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLineToReset($"{origin} = {position};");

            AppendLine($"double {phase} = ({position} - {origin});");
            AppendLine($"double {output} = {calculatorName}.Calculate({phase});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Divide_OperatorDto(Divide_OperatorDto dto)
        {
            return ProcessBinaryDoubleOperator(dto, DIVIDE_SYMBOL);
        }

        protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto(DoubleToBoolean_OperatorDto dto)
        {
            string number = GetLiteralFromInputDto(dto.Number);
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);
            AppendLine($"bool {output} = {number} == 0.0 ? false : true;");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Equal_OperatorDto(Equal_OperatorDto dto)
        {
            return ProcessBinaryBoolOperator(dto, EQUALS_SYMBOL);
        }

        protected override IOperatorDto Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
        {
            return ProcessBinaryBoolOperator(dto, GREATER_THAN_SYMBOL);
        }

        protected override IOperatorDto Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
        {
            return ProcessBinaryBoolOperator(dto, GREATER_THAN_OR_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetHighPassFilterVariables));
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetHighShelfFilterVariables));
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_Hold_OperatorDto(Hold_OperatorDto dto)
        {
            // The calculate procedure should only use the held variable. 
            // The Reset procedure should do a calculation of that held variable. 
            // But that Reset procedure should not execute the reset code lines.
            // So the Calculate procedure should not be written at all, 
            // and the Reset procedure should get the reset code lines, but does have to get the calculate code lines.
            // In the methods that delegate to StringBuilders, 
            // the _holdOperatorIsActiveStack is inspected to see to which StringBuilder to write, during visitation.

            _holdOperatorIsActiveStack.Push(true);

            string signal = GetLiteralFromInputDto(dto.Signal);
            string output = GetLongLivedVariableName(dto.OperatorTypeEnum);

            _holdOperatorIsActiveStack.Pop();
            _holdOperatorIsActiveStack.Push(false);

            AppendLineToReset(GetOperatorTitleComment(dto));
            AppendLineToReset($"{output} = {signal};");
            AppendLineToReset();

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
        {
            string condition = GetLiteralFromInputDto(dto.Condition);

            string then = GetVariableName(nameof(then));
            string @else = GetVariableName(nameof(@else));
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendLine($"{GetOperatorTitleComment(dto)} (begin)");
            AppendLine();

            // TODO: Lower priority: Putting the then and else clauses in separate methods should be dependent on the if being 'long', 
            // which should be determined in the DTO pre-processing.

            string thenLeftSide;
            if (dto.Then.IsVar)
            {
                BeginGenerateMethod(dto.Then.Var.OperationIdentity, nameof(then));
                thenLeftSide = GetLiteralFromInputDto(dto.Then);
                thenLeftSide = EndGenerateMethod(thenLeftSide);
            }
            else
            {
                thenLeftSide = GetLiteralFromInputDto(dto.Then);
            }
            AppendLine($"{GetOperatorTitleComment(dto)} ({nameof(then)})");
            AppendLine($"double {then} = {thenLeftSide};");
            AppendLine();

            string elseLeftSide;
            if (dto.Else.IsVar)
            {
                BeginGenerateMethod(dto.Else.Var.OperationIdentity, nameof(@else));
                elseLeftSide = GetLiteralFromInputDto(dto.Else);
                elseLeftSide = EndGenerateMethod(elseLeftSide);
            }
            else
            {
                elseLeftSide = GetLiteralFromInputDto(dto.Else);
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

        // ReSharper disable once SuggestBaseTypeForParameter
        private IOperatorDto Process_InletsToDimension(InletsToDimension_OperatorDto dto, bool isStripe)
        {
            string position = GetLiteralFromInputDto(dto.Position);
            string transformedPosition = GetVariableName(nameof(transformedPosition));
            string castedPosition = GetVariableName(nameof(castedPosition));
            string output = GetVariableName(dto.OperatorTypeEnum);

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
                int count = dto.InputsExceptPosition.Count - 1;

                for (int i = 0; i < count; i++)
                {
                    AppendLine($"case {i}:");
                    Indent();
                    {
                        string operand = GetLiteralFromInputDto(dto.InputsExceptPosition[i]);

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

        protected override IOperatorDto Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
        {
            return ProcessBinaryBoolOperator(dto, LESS_THAN_SYMBOL);
        }

        protected override IOperatorDto Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
        {
            return ProcessBinaryBoolOperator(dto, LESS_THAN_OR_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_Loop_OperatorDto_AllVars(Loop_OperatorDto_AllVars dto)
        {
            return Process_Loop_OperatorDto(dto);
        }

        private IOperatorDto Process_Loop_OperatorDto(Loop_OperatorDto dto)
        {
            string output = GetVariableName(dto.OperatorTypeEnum);
            string sourcePosition = GetLiteralFromInputDto(dto.Position);
            string origin = GetLongLivedVariableName(nameof(origin));
            string nullableInputPosition = GetVariableName(nameof(nullableInputPosition));

            AppendOperatorTitleComment(dto);

            AppendLineToReset($"{origin} = {sourcePosition};");

            AppendLine($"double {output};");

            // Ported from Loop_OperatorCalculator_Helper.GetTransformedPosition.
            // The reason the generated code does not just call that helper method,
            // is because then you would have to retrieve all the inputs first,
            // while if you inline it, you could get inputs only when you need them,
            // which is more efficient.

            string outputPosition = GetVariableName(nameof(outputPosition));
            string inputPosition = GetVariableName(nameof(inputPosition));
            string isBeforeAttack = GetVariableName(nameof(isBeforeAttack));
            string isInAttack = GetVariableName(nameof(isInAttack));
            string cycleLength = GetVariableName(nameof(cycleLength));
            string outputLoopStart = GetVariableName(nameof(outputLoopStart));
            string noteEndPhase = GetVariableName(nameof(noteEndPhase));
            string outputLoopEnd = GetVariableName(nameof(outputLoopEnd));
            string isInLoop = GetVariableName(nameof(isInLoop));
            string phase = GetVariableName(nameof(phase));
            string releaseLength = GetVariableName(nameof(releaseLength));
            string outputReleaseEndPosition = GetVariableName(nameof(outputReleaseEndPosition));
            string isInRelease = GetVariableName(nameof(isInRelease));
            string positionInRelease = GetVariableName(nameof(positionInRelease));
            AppendLine($"double? {nullableInputPosition};");
            AppendLine();
            AppendLine($"double {outputPosition} = {sourcePosition};");
            AppendLine($"double {inputPosition} = {outputPosition};");
            AppendLine();
            AppendLine($"{inputPosition} -= {origin};");
            AppendLine();
            AppendLine("// BeforeAttack");
            AppendLine();
            string skip = GetLiteralFromInputDto(dto.Skip);
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
                string loopStartMarker = GetLiteralFromInputDto(dto.LoopStartMarker);
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
                    string loopEndMarker = GetLiteralFromInputDto(dto.LoopEndMarker);
                    AppendLine($"double {cycleLength} = {loopEndMarker} - {loopStartMarker};");
                    AppendLine();
                    AppendLine("// Round up end of loop to whole cycles.");
                    AppendLine($"double {outputLoopStart} = {loopStartMarker} - {skip};");
                    AppendLine();
                    string noteDuration = GetLiteralFromInputDto(dto.NoteDuration);
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
                        string releaseEndMarker = GetLiteralFromInputDto(dto.ReleaseEndMarker);
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
                AppendLine($"{output} = {nullableInputPosition}.Value;");
                Unindent();
            }
            AppendLine("}");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetLowPassFilterVariables));
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetLowShelfFilterVariables));
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_MaxFollower_OperatorDto(MaxFollower_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_CollectionRecalculationContinuous(
            MaxOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MaxOverDimension_OperatorDto_CollectionRecalculationUponReset(
            MaxOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto)
        {
            if (dto.Inputs.Count == 2)
            {
                return Process_MinOrMaxOverInlets_With2Inlets(dto, MinOrMaxEnum.Min);
            }
            else
            {
                return Process_MinOrMaxOverInlets_MoreThan2Inlets(dto, MinOrMaxEnum.Min);
            }
        }

        protected override IOperatorDto Visit_MinFollower_OperatorDto(MinFollower_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_CollectionRecalculationContinuous(
            MinOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MinOverDimension_OperatorDto_CollectionRecalculationUponReset(
            MinOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto)
        {
            if (dto.Inputs.Count == 2)
            {
                return Process_MinOrMaxOverInlets_With2Inlets(dto, MinOrMaxEnum.Min);
            }
            else
            {
                return Process_MinOrMaxOverInlets_MoreThan2Inlets(dto, MinOrMaxEnum.Min);
            }
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            return ProcessMultiVarOperator(dto, MULTIPLY_SYMBOL);
        }

        protected override IOperatorDto Visit_Negative_OperatorDto(Negative_OperatorDto dto)
        {
            string number = GetLiteralFromInputDto(dto.Number);
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = -{number};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto)
        {
            string position = GetLiteralFromInputDto(dto.Position);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string offset = GetRandomOrNoiseOffsetVariableNameCamelCase(dto.OperationIdentity);
            string arrayCalculator = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDto);
            const string noiseCalculatorHelper = nameof(NoiseCalculatorHelper);
            const string generateOffset = nameof(NoiseCalculatorHelper.GenerateOffset);

            AppendOperatorTitleComment(dto);

            AppendLineToReset($"{offset} = {noiseCalculatorHelper}.{generateOffset}();");

            // TODO: Low priority: Just assigning offset to position in the reset operation would be slightly faster.
            AppendLine($"double {output} = {arrayCalculator}.Calculate({position} + {offset});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto)
        {
            string input = GetLiteralFromInputDto(dto.Number);
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"bool {output} = !{input};");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetNotchFilterVariables));
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(NotchFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
        {
            return ProcessBinaryBoolOperator(dto, NOT_EQUAL_SYMBOL);
        }

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            _stack.Push(CompilationHelper.FormatValue(dto.Number));

            return dto;
        }

        protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
        {
            return ProcessBinaryBoolOperator(dto, OR_SYMBOL);
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsVar dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(dto, nameof(BiQuadFilterWithoutFields.SetPeakingEQFilterVariables));
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst(PeakingEQFilter_OperatorDto_SoundVarOrConst_OtherInputsConst dto)
        {
            return Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(dto);
        }

        protected override IOperatorDto Visit_Power_OperatorDto(Power_OperatorDto dto)
        {
            string @base = GetLiteralFromInputDto(dto.Base);
            string exponent = GetLiteralFromInputDto(dto.Exponent);
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = Math.Pow({@base}, {exponent});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_PulseTrigger_OperatorDto(PulseTrigger_OperatorDto dto)
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
            string rate = GetLiteralFromInputDto(dto.Rate);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string offset = GetRandomOrNoiseOffsetVariableNameCamelCase(dto.OperationIdentity);
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
            string from = GetLiteralFromInputDto(dto.From);
            string till = GetLiteralFromInputDto(dto.Till);
            string step = GetLiteralFromInputDto(dto.Step);
            string position = GetLiteralFromInputDto(dto.Position);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string tillPlusStep = GetVariableName(nameof(tillPlusStep));
            string valueNonRounded = GetVariableName(nameof(valueNonRounded));
            string upperBound = GetVariableName(nameof(upperBound));
            string valueNonRoundedCorrected = GetVariableName(nameof(valueNonRoundedCorrected));
            string stepDividedBy2 = GetVariableName(nameof(stepDividedBy2));
            string valueRounded = GetVariableName(nameof(valueRounded));
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
            string from = GetLiteralFromInputDto(dto.From);
            string till = GetLiteralFromInputDto(dto.Till);
            string step = GetLiteralFromInputDto(dto.Step);
            string position = GetLiteralFromInputDto(dto.Position);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string valueNonRounded = GetVariableName(nameof(valueNonRounded));
            string upperBound = GetVariableName(nameof(upperBound));
            string valueNonRoundedCorrected = GetVariableName(nameof(valueNonRoundedCorrected));
            string valueRounded = GetVariableName(nameof(valueRounded));
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
            string from = GetLiteralFromInputDto(dto.From);
            string till = GetLiteralFromInputDto(dto.Till);
            string position = GetLiteralFromInputDto(dto.Position);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string tillPlusOne = GetVariableName(nameof(tillPlusOne));
            string valueNonRounded = GetVariableName(nameof(valueNonRounded));
            string upperBound = GetVariableName(nameof(upperBound));
            string valueNonRoundedCorrected = GetVariableName(nameof(valueNonRoundedCorrected));
            string valueRounded = GetVariableName(nameof(valueRounded));
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

        protected override IOperatorDto Visit_Remainder_OperatorDto(Remainder_OperatorDto dto)
        {
            return ProcessBinaryDoubleOperator(dto, REMAINDER_SYMBOL);
        }

        protected override IOperatorDto Visit_Reset_OperatorDto(Reset_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Round_OperatorDto_StepOne_ZeroOffset(Round_OperatorDto_StepOne_ZeroOffset dto)
        {
            string signal = GetLiteralFromInputDto(dto.Signal);
            string output = GetVariableName(dto.OperatorTypeEnum);
            const string math = nameof(Math);
            const string round = nameof(Math.Round);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {math}.{round}({signal}, MidpointRounding.AwayFromZero);");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_WithOffset(Round_OperatorDto_WithOffset dto)
        {
            string signal = GetLiteralFromInputDto(dto.Signal);
            string step = GetLiteralFromInputDto(dto.Step);
            string offset = GetLiteralFromInputDto(dto.Offset);
            string output = GetVariableName(dto.OperatorTypeEnum);
            const string mathHelper = nameof(MathHelper);
            const string roundWithStep = nameof(MathHelper.RoundWithStep);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {mathHelper}.{roundWithStep}({signal}, {step}, {offset});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Round_OperatorDto_ZeroOffset(Round_OperatorDto_ZeroOffset dto)
        {
            string signal = GetLiteralFromInputDto(dto.Signal);
            string step = GetLiteralFromInputDto(dto.Step);
            string output = GetVariableName(dto.OperatorTypeEnum);
            const string mathHelper = nameof(MathHelper);
            const string roundWithStep = nameof(MathHelper.RoundWithStep);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {mathHelper}.{roundWithStep}({signal}, {step});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_MonoToStereo(SampleWithRate1_OperatorDto_MonoToStereo dto)
        {
            ArrayDto arrayDto = dto.ArrayDtos.Single();
            string calculatorName = GetArrayCalculatorVariableNameCamelCaseAndCache(arrayDto);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string position = GetLiteralFromInputDto(dto.Position);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {calculatorName}.Calculate({position});"); // Return the single channel for both channels.

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_NoChannelConversion(SampleWithRate1_OperatorDto_NoChannelConversion dto)
        {
            IList<ArrayDto> arrayDtos = dto.ArrayDtos;
            string channelIndexDouble = GetLiteralFromInputDto(dto.Channel);
            string channelIndex = GetVariableName(DimensionEnum.Channel);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string position = GetLiteralFromInputDto(dto.Position);

            AppendOperatorTitleComment(dto);

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
                        AppendLine($"{output} = {calculatorName}.Calculate({position});");
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

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_StereoToMono(SampleWithRate1_OperatorDto_StereoToMono dto)
        {
            string calculatorName1 = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDtos[0]);
            string calculatorName2 = GetArrayCalculatorVariableNameCamelCaseAndCache(dto.ArrayDtos[1]);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string position = GetLiteralFromInputDto(dto.Position);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} =");
            Indent();
            {
                AppendLine($"{calculatorName1}.Calculate({position}) +");
                AppendLine($"{calculatorName2}.Calculate({position});");
                Unindent();
            }

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_SineWithRate1_OperatorDto(SineWithRate1_OperatorDto dto)
        {
            string position = GetLiteralFromInputDto(dto.Position);
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = SineCalculator.Sin({position});");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_CollectionRecalculationContinuous(
            SortOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_SortOverDimension_OperatorDto_CollectionRecalculationUponReset(
            SortOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_SortOverInlets_Outlet_OperatorDto(SortOverInlets_Outlet_OperatorDto dto)
        {
            string position = GetLiteralFromInputDto(dto.Position);
            string items = GetLongLivedDoubleArrayVariableName(dto.Inputs.Count);
            string output = GetVariableName(dto.OperatorTypeEnum);
            const string conversionHelper = nameof(ConversionHelper);
            const string canCastToNonNegativeInt32WithMax = nameof(ConversionHelper.CanCastToNonNegativeInt32WithMax);
            string maxIndex = CompilationHelper.FormatValue(dto.Inputs.Count - 1);

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

            for (int i = 0; i < dto.Inputs.Count; i++)
            {
                string item = GetLiteralFromInputDto(dto.Inputs[i]);

                AppendLine($"{items}[{i}] = {item};");
            }

            AppendLine();
            AppendLine($"Array.Sort({items});");
            AppendLine();
            AppendLine($"{output} = {items}[(int){position}];");

            return GenerateOperatorWrapUp(dto, output);
        }

        protected override IOperatorDto Visit_Spectrum_OperatorDto(Spectrum_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_WithOrigin(Squash_OperatorDto_WithOrigin dto)
        {
            string factor = GetLiteralFromInputDto(dto.Factor);
            string origin = GetLiteralFromInputDto(dto.Origin);
            string sourcePosition = GetLiteralFromInputDto(dto.Position);
            string destPosition = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);
            AppendLine($"double {destPosition} = ({sourcePosition} - {origin}) * {factor} + {origin};");

            return GenerateOperatorWrapUp(dto, destPosition);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_ConstFactor_WithOriginShifting(Squash_OperatorDto_ConstFactor_WithOriginShifting dto)
        {
            string factor = GetLiteralFromInputDto(dto.Factor);
            string sourcePosition = GetLiteralFromInputDto(dto.Position);
            string destPosition = GetVariableName(dto.OperatorTypeEnum);
            string origin = GetLongLivedVariableName(nameof(origin));

            AppendOperatorTitleComment(dto);
            AppendLineToReset($"{origin} = {sourcePosition};");
            AppendLine($"double {destPosition} = ({sourcePosition} - {origin}) * {factor} + {origin};");

            return GenerateOperatorWrapUp(dto, destPosition);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_ZeroOrigin(Squash_OperatorDto_ZeroOrigin dto)
        {
            string factor = GetLiteralFromInputDto(dto.Factor);
            string sourcePosition = GetLiteralFromInputDto(dto.Position);
            string destPosition = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);
            AppendLine($"double {destPosition} = {sourcePosition} * {factor};");
            
            return GenerateOperatorWrapUp(dto, destPosition);
        }

        protected override IOperatorDto Visit_Squash_OperatorDto_VarFactor_WithPhaseTracking(Squash_OperatorDto_VarFactor_WithPhaseTracking dto)
        {
            string factor = GetLiteralFromInputDto(dto.Factor);
            string phase = GetLongLivedVariableName(nameof(phase));
            string previousPosition = GetLongLivedVariableName(nameof(previousPosition));
            string sourcePosition = GetLiteralFromInputDto(dto.Position);
            string destPosition = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);
            string positionTranformationLine = $"double {destPosition} = {phase} + ({sourcePosition} - {previousPosition}) * {factor};";

            AppendLineToCalculate(positionTranformationLine);
            AppendLineToCalculate($"{previousPosition} = {sourcePosition};");
            AppendLineToCalculate($"{phase} = {destPosition};");
            // I need two different variables for destPosition and phase, because destPosition is reused by different uses of the same stack level, while phase needs to be uniquely used by the operator instance.

            AppendLineToReset($"{phase} = 0.0;");
            AppendLineToReset($"{previousPosition} = {sourcePosition};");
            AppendLineToReset(positionTranformationLine);

            return GenerateOperatorWrapUp(dto, destPosition);
        }

        protected override IOperatorDto Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
        {
            return ProcessBinaryDoubleOperator(dto, SUBTRACT_SYMBOL);
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto_AllVars(SumFollower_OperatorDto_AllVars dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_CollectionRecalculationContinuous(
            SumOverDimension_OperatorDto_CollectionRecalculationContinuous dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_SumOverDimension_OperatorDto_CollectionRecalculationUponReset(
            SumOverDimension_OperatorDto_CollectionRecalculationUponReset dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_ToggleTrigger_OperatorDto(ToggleTrigger_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override IOperatorDto Visit_TriangleWithRate1_OperatorDto(TriangleWithRate1_OperatorDto dto)
        {
            string position = GetLiteralFromInputDto(dto.Position);

            AppendOperatorTitleComment(dto);

            string shiftedPhase = GetVariableName(nameof(shiftedPhase));
            string relativePhase = GetVariableName(nameof(relativePhase));
            string output = GetVariableName(dto.OperatorTypeEnum);

            // Correct the phase shift, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            AppendLine($"double {shiftedPhase} = {position} + 0.25;");
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

            return GenerateOperatorWrapUp(dto, inputVariable);
        }

        // Generalized Methods

        private IOperatorDto Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsVar(OperatorDtoBase_Filter_VarSound dto, string biQuadFilterSetFilterVariablesMethodName)
        {
            string sound = GetLiteralFromInputDto(dto.Sound);
            // TODO: I would expect a dto.Frequency in the abstract type, instead of resorting to dto.Inputs.ElementAt(1).
            string frequency =  GetLiteralFromInputDto(dto.Inputs.ElementAt(1));
            IList<string> additionalFilterParameters = dto.Inputs.Skip(2).Select(x => GetLiteralFromInputDto(x)).ToArray();

            AppendOperatorTitleComment(dto);

            string output = GetVariableName(dto.OperatorTypeEnum);

            string x1 = GetLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x1)}");
            string x2 = GetLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x2)}");
            string y1 = GetLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y1)}");
            string y2 = GetLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y2)}");
            string a0 = GetVariableName($"{dto.OperatorTypeEnum}{nameof(a0)}");
            string a1 = GetVariableName($"{dto.OperatorTypeEnum}{nameof(a1)}");
            string a2 = GetVariableName($"{dto.OperatorTypeEnum}{nameof(a2)}");
            string a3 = GetVariableName($"{dto.OperatorTypeEnum}{nameof(a3)}");
            string a4 = GetVariableName($"{dto.OperatorTypeEnum}{nameof(a4)}");

            string nyquistFrequency = CompilationHelper.FormatValue(dto.NyquistFrequency);
            string samplingRate = CompilationHelper.FormatValue(dto.TargetSamplingRate);
            string limitedFrequency = GetVariableName(nameof(limitedFrequency));
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

        private IOperatorDto Process_Filter_OperatorDto_SoundVarOrConst_OtherInputsConst(OperatorDtoBase_Filter_SoundVarOrConst_OtherInputsConst dto)
        {
            string sound = GetLiteralFromInputDto(dto.Sound);

            AppendOperatorTitleComment(dto);

            string x1 = GetLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x1)}");
            string x2 = GetLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(x2)}");
            string y1 = GetLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y1)}");
            string y2 = GetLongLivedVariableName($"{dto.OperatorTypeEnum}{nameof(y2)}");
            string output = GetVariableName(dto.OperatorTypeEnum);

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

        private IOperatorDto ProcessBinaryDoubleOperator(OperatorDtoBase_WithAAndB dto, string operatorSymbol)
        {
            string a = GetLiteralFromInputDto(dto.A);
            string b = GetLiteralFromInputDto(dto.B);
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {a} {operatorSymbol} {b};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessBinaryBoolOperator(OperatorDtoBase_WithAAndB dto, string operatorSymbol)
        {
            string a = GetLiteralFromInputDto(dto.A);
            string b = GetLiteralFromInputDto(dto.B);
            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"bool {output} = {a} {operatorSymbol} {b};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto Process_MinOrMaxOverInlets_MoreThan2Inlets(IOperatorDto dto, MinOrMaxEnum minOrMaxEnum)
        {
            IList<string> values = dto.Inputs.Select(x => GetLiteralFromInputDto(x)).ToArray();
            int valueCount = values.Count;
            string firstValue = values.First();
            string output = GetVariableName(dto.OperatorTypeEnum);
            string operatorSymbol = GetOperatorSymbol(minOrMaxEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {firstValue};");

            // NOTE: i = 1.
            for (int i = 1; i < valueCount; i++)
            {
                string item = values[i];

                AppendLine($"if ({output} {operatorSymbol} {item}) {output} = {item};");
            }

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto Process_MinOrMaxOverInlets_With2Inlets(IOperatorDto dto, MinOrMaxEnum minOrMaxEnum)
        {
            string a = GetLiteralFromInputDto(dto.Inputs[0]);
            string b = GetLiteralFromInputDto(dto.Inputs[1]);
            string output = GetVariableName(dto.OperatorTypeEnum);
            string operatorSymbol = GetOperatorSymbol(minOrMaxEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {a} {operatorSymbol} {b} ? {a} : {b};");

            return GenerateOperatorWrapUp(dto, output);
        }

        private IOperatorDto ProcessMultiVarOperator(IOperatorDto dto, string operatorSymbol)
        {
            IList<string> values = dto.Inputs.Select(x => GetLiteralFromInputDto(x)).ToArray();

            string output = GetVariableName(dto.OperatorTypeEnum);

            AppendOperatorTitleComment(dto);

            AppendLine($"double {output} = {string.Join(" " + operatorSymbol + " ", values)};");

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

        /// <param name="operationIdentity">key for a unique method</param>
        private void BeginGenerateMethod(string operationIdentity, object mnemonic)
        {
            switch (_calculationMethodEnum)
            {
                case CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters:
                    GeneratedMethodInfo generatedMethodInfo;
                    if (!_operationIdentity_To_GeneratedMethodInfo_Dictionary.TryGetValue(operationIdentity, out generatedMethodInfo))
                    {
                        generatedMethodInfo = new GeneratedMethodInfo
                        {
                            MethodNameForCalculate = GetMethodName($"{CALCULATE_MNEMONIC}{mnemonic}"),
                            MethodNameForReset = GetMethodName($"{RESET_MNEMONIC}{mnemonic}"),
                        };

                        _operationIdentity_To_GeneratedMethodInfo_Dictionary[operationIdentity] = generatedMethodInfo;
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

                    string output = GetVariableName(generatedMethodInfo.MethodNameForCalculate);

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

        /// <summary> Appends an empty line, pushes the output, adds to _resultReuse_Dictionary and returns dto. </summary>
        private IOperatorDto GenerateOperatorWrapUp(IOperatorDto dto, string output)
        {
            AppendLine();

            _stack.Push(output);

            return dto;
        }

        /// <summary> Returns the phase literal. </summary>
        private string GeneratePhaseCalculationWithPhaseTracking(IOperatorDto_PositionReader dto, string rate)
        {
            string position = GetLiteralFromInputDto(dto.Position);
            string previousPosition = GetLongLivedVariableName(nameof(previousPosition));
            string phase = GetLongLivedVariableName(nameof(phase));

            AppendLineToReset($"{previousPosition} = {position};");
            AppendLineToReset($"{phase} = 0.0;");

            AppendLineToCalculate($"{phase} += ({position} - {previousPosition}) * {rate};");
            AppendLineToCalculate($"{previousPosition} = {position};");

            return phase;
        }

        private string GetRandomOrNoiseOffsetVariableNameCamelCase(string operationIdentity)
        {
            // ReSharper disable once InvertIf
            if (!_variableInfo.RandomOrNoiseOperationIdentity_To_OffsetVariableNameCamelCase_Dictionary.TryGetValue(operationIdentity, out string variableNameCamelCase))
            {
                variableNameCamelCase = GetLongLivedVariableName(OFFSET_MNEMONIC);
            }

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.RandomOrNoiseOperationIdentity_To_OffsetVariableNameCamelCase_Dictionary[operationIdentity] = variableNameCamelCase;
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
                string nameCamelCase = GetVariableName(ARRAY_CALCULATOR_MNEMONIC);

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

        private string GetInputName(VariableInput_OperatorDto dto)
        {
            InputVariableInfo inputVariableInfo = null;

            if (_variableInfo.VariableInput_OperatorDto_To_VariableName_Dictionary.TryGetValue(dto, out string variableName))
            {
                _variableInfo.VariableName_To_InputVariableInfo_Dictionary.TryGetValue(variableName, out inputVariableInfo);
            }

            if (string.IsNullOrEmpty(variableName) || inputVariableInfo == null)
            {
                object mnemonic;
                if (dto.StandardDimensionEnum != DimensionEnum.Undefined)
                {
                    mnemonic = dto.StandardDimensionEnum;
                }
                else if (!string.IsNullOrEmpty(dto.CanonicalCustomDimensionName))
                {
                    mnemonic = dto.CanonicalCustomDimensionName;
                }
                else
                {
                    mnemonic = DEFAULT_INPUT_MNEMONIC;
                }

                variableName = GetVariableName(mnemonic);
                inputVariableInfo = new InputVariableInfo(variableName, dto.CanonicalCustomDimensionName, dto.StandardDimensionEnum, dto.Position, dto.DefaultValue);
            }

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.VariableInput_OperatorDto_To_VariableName_Dictionary[dto] = variableName;
                variableInfo.VariableName_To_InputVariableInfo_Dictionary[variableName] = inputVariableInfo;
            }

            return variableName;
        }

        private string GetLiteralFromInputDto(InputDto inputDto)
        {
            if (inputDto.Var != null)
            {
                Visit_OperatorDto_Polymorphic(inputDto.Var);
                string literal = _stack.Pop();
                return literal;
            }
            else
            {
                return CompilationHelper.FormatValue(inputDto.Const);
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

        /// <param name="mnemonic">
        /// Will be incorporated into the variable name. It will be converted to string.
        /// It will also be put into a (non-unique) form that will be valid in C#.
        /// Also underscores are removed from it, because that is a separator character in our variable names.
        /// </param>
        private string GetVariableName(object mnemonic)
        {
            string arbitraryString = Convert.ToString(mnemonic);

            if (string.IsNullOrWhiteSpace(arbitraryString)) throw new NullOrWhiteSpaceException(() => arbitraryString);

            string convertedName = NameHelper.ToCanonical(arbitraryString).ToCamelCase().Replace("_", "");

            string nonUniqueNameInCode = convertedName;

            int uniqueNumber = GetUniqueNumber();

            string variableName = $"{nonUniqueNameInCode}_{uniqueNumber}";

            return variableName;
        }

        private string GetLongLivedVariableName(object mnemonic)
        {
            string variableName = GetVariableName(mnemonic);

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.LongLivedDoubleVariableNamesCamelCase.Add(variableName);
            }

            return variableName;
        }

        private string GetLongLivedDoubleArrayVariableName(int arrayLength)
        {
            string variableName = GetVariableName(ARRAY_MNEMONIC);

            foreach (VariableCollections variableInfo in GetVariableInfoList())
            {
                variableInfo.LongLivedDoubleArrayVariableInfos.Add(new DoubleArrayVariableInfo { NameCamelCase = variableName, ArrayLength = arrayLength });
            }

            return variableName;
        }

        private string GetMethodName(object mnemonic)
        {
            string uniqueNameCamelCase = GetVariableName(mnemonic);
            string uniqueNamePascalCase = uniqueNameCamelCase.Left(1).ToUpper() + uniqueNameCamelCase.TrimStart(1);
            return uniqueNamePascalCase;
        }

        private int GetUniqueNumber() => _counter++;

        private IList<VariableCollections> GetVariableInfoList()
        {
            IList<VariableCollections> list = _generatedMethodInfoStack.Select(x => x.VariableInfo).Union(_variableInfo).ToArray();
            return list;
        }
    }
}