using System.Collections.Generic;
using JJ.Business.Synthesizer.Roslyn.Helpers;

namespace JJ.Business.Synthesizer.Roslyn.Visitors
{
    internal class OperatorDtoToCSharpVisitorResult
    {
        public OperatorDtoToCSharpVisitorResult(
            string rawCalculationCode,
            string returnValueLiteral,
            IList<InputVariableInfo> variableInputValueInfos,
            IList<string> positionVariableNamesCamelCase,
            IList<string> longLivedPreviousPositionVariableNamesCamelCase,
            IList<string> longLivedPhaseVariableNamesCamelCase,
            IList<string> longLivedOriginVariableNamesCamelCase)
        {
            RawCalculationCode = rawCalculationCode;
            ReturnValueLiteral = returnValueLiteral;
            VariableInputValueInfos = variableInputValueInfos;
            PositionVariableNamesCamelCase = positionVariableNamesCamelCase;
            LongLivedPreviousPositionVariableNamesCamelCase = longLivedPreviousPositionVariableNamesCamelCase;
            LongLivedPhaseVariableNamesCamelCase = longLivedPhaseVariableNamesCamelCase;
            LongLivedOriginVariableNamesCamelCase = longLivedOriginVariableNamesCamelCase;
        }

        public string RawCalculationCode { get; }
        public string ReturnValueLiteral { get; }
        public IList<InputVariableInfo> VariableInputValueInfos { get; }
        public IList<string> PositionVariableNamesCamelCase { get; }
        public IList<string> LongLivedPreviousPositionVariableNamesCamelCase { get; }
        public IList<string> LongLivedPhaseVariableNamesCamelCase { get; }
        public IList<string> LongLivedOriginVariableNamesCamelCase { get; }
    }
}