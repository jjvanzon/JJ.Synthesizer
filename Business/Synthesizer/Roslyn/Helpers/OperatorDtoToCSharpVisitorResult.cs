using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class OperatorDtoToCSharpVisitorResult
    {
        public string RawCalculationCode { get; }
        public string RawResetCode { get; }
        public string ReturnValueLiteral { get; }
        public string FirstTimeVariableNameCamelCase { get; }
        public IList<ExtendedVariableInfo> InputVariableInfos { get; }
        public IList<string> PositionVariableNamesCamelCase { get; }
        public IList<string> LongLivedPreviousPositionVariableNamesCamelCase { get; }
        public IList<string> LongLivedPhaseVariableNamesCamelCase { get; }
        public IList<string> LongLivedOriginVariableNamesCamelCase { get; }
        public IList<ExtendedVariableInfo> LongLivedDimensionVariableInfos { get; }
        public IList<string> LocalDimensionVariableNamesCamelCase { get; }
        public IList<string> LongLivedMiscVariableNamesCamelCase { get; }
        public IList<ArrayCalculatorVariableInfo> ArrayCalculatorVariableInfos { get; }
        public IList<string> NoiseCalculatorVariableNamesCamelCase { get; }

        public OperatorDtoToCSharpVisitorResult(
            string rawCalculationCode,
            string rawResetCode,
            string returnValueLiteral,
            string firstTimeVariableNameCamelCase,
            IList<ExtendedVariableInfo> inputVariableInfos,
            IList<string> positionVariableNamesCamelCase,
            IList<string> longLivedPreviousPositionVariableNamesCamelCase,
            IList<string> longLivedPhaseVariableNamesCamelCase,
            IList<string> longLivedOriginVariableNamesCamelCase,
            IList<ExtendedVariableInfo> longLivedDimensionVariableInfos,
            IList<string> localDimensionVariableNamesCamelCase,
            IList<string> longLivedMiscVariableNamesCamelCase,
            IList<ArrayCalculatorVariableInfo> arrayCalculatorVariableInfos,
            IList<string> noiseCalculatorVariableNamesCamelCase)
        {
            RawCalculationCode = rawCalculationCode;
            RawResetCode = rawResetCode;
            ReturnValueLiteral = returnValueLiteral;
            InputVariableInfos = inputVariableInfos;
            FirstTimeVariableNameCamelCase = firstTimeVariableNameCamelCase;
            PositionVariableNamesCamelCase = positionVariableNamesCamelCase;
            LongLivedPreviousPositionVariableNamesCamelCase = longLivedPreviousPositionVariableNamesCamelCase;
            LongLivedPhaseVariableNamesCamelCase = longLivedPhaseVariableNamesCamelCase;
            LongLivedOriginVariableNamesCamelCase = longLivedOriginVariableNamesCamelCase;
            LongLivedDimensionVariableInfos = longLivedDimensionVariableInfos;
            LocalDimensionVariableNamesCamelCase = localDimensionVariableNamesCamelCase;
            LongLivedMiscVariableNamesCamelCase = longLivedMiscVariableNamesCamelCase;
            ArrayCalculatorVariableInfos = arrayCalculatorVariableInfos;
            NoiseCalculatorVariableNamesCamelCase = noiseCalculatorVariableNamesCamelCase;
        }
    }
}