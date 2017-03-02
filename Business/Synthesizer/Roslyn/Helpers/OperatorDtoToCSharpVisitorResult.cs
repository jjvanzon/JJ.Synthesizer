using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class OperatorDtoToCSharpVisitorResult
    {
        public string RawCalculationCode { get; }
        public string RawResetCode { get; }
        public string ReturnValueLiteral { get; }
        public string FirstTimeVariableNameCamelCase { get; }
        public IList<string> LongLivedDoubleVariableNamesCamelCase { get; }
        public IList<string> LocallyReusedDoubleVariableNamesCamelCase { get; }
        public IList<ExtendedVariableInfo> InputVariableInfos { get; }
        public IList<ExtendedVariableInfo> LongLivedDimensionVariableInfos { get; }
        public IList<ArrayCalculationInfo> ArrayCalculationInfos { get; }
        public IList<DoubleArrayVariableInfo> LongLivedDoubleArrayVariableInfos { get; }
        public IList<string> CalculationMethodCodeList { get; }
        public IList<string> ResetMethodCodeList { get; }

        public OperatorDtoToCSharpVisitorResult(
            string rawCalculationCode,
            string rawResetCode,
            string returnValueLiteral,
            string firstTimeVariableNameCamelCase,
            IList<string> longLivedPreviousPositionVariableNamesCamelCase,
            IList<string> locallyReusedDoubleVariableNamesCamelCase,
            IList<ExtendedVariableInfo> inputVariableInfos,
            IList<ExtendedVariableInfo> longLivedDimensionVariableInfos,
            IList<ArrayCalculationInfo> arrayCalculationInfos,
            IList<DoubleArrayVariableInfo> longLivedDoubleArrayVariableInfos,
            IList<string> calculationMethodCodeList,
            IList<string> resetMethodCodeList)
        {
            RawCalculationCode = rawCalculationCode;
            RawResetCode = rawResetCode;
            ReturnValueLiteral = returnValueLiteral;
            FirstTimeVariableNameCamelCase = firstTimeVariableNameCamelCase;
            LongLivedDoubleVariableNamesCamelCase = longLivedPreviousPositionVariableNamesCamelCase;
            LocallyReusedDoubleVariableNamesCamelCase = locallyReusedDoubleVariableNamesCamelCase;
            InputVariableInfos = inputVariableInfos;
            LongLivedDimensionVariableInfos = longLivedDimensionVariableInfos;
            ArrayCalculationInfos = arrayCalculationInfos;
            LongLivedDoubleArrayVariableInfos = longLivedDoubleArrayVariableInfos;
            CalculationMethodCodeList = calculationMethodCodeList;
            ResetMethodCodeList = resetMethodCodeList;
        }
    }
}