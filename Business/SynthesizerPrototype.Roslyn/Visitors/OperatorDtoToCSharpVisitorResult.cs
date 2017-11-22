using System.Collections.Generic;
using JJ.Business.SynthesizerPrototype.Roslyn.Helpers;

namespace JJ.Business.SynthesizerPrototype.Roslyn.Visitors
{
	internal class OperatorDtoToCSharpVisitorResult
	{
		public OperatorDtoToCSharpVisitorResult(
			string rawCalculationCode,
			string returnValueLiteral,
			IList<VariableInputInfo> variableInputValueInfos,
			IList<string> positionVariableNamesCamelCase,
			IList<string> previousPositionVariableNamesCamelCase,
			IList<string> phaseVariableNamesCamelCase)
		{
			RawCalculationCode = rawCalculationCode;
			ReturnValueLiteral = returnValueLiteral;
			VariableInputValueInfos = variableInputValueInfos;
			PositionVariableNamesCamelCase = positionVariableNamesCamelCase;
			PreviousPositionVariableNamesCamelCase = previousPositionVariableNamesCamelCase;
			PhaseVariableNamesCamelCase = phaseVariableNamesCamelCase;
		}

		public string RawCalculationCode { get; }
		public string ReturnValueLiteral { get; }
		public IList<VariableInputInfo> VariableInputValueInfos { get; }
		public IList<string> PositionVariableNamesCamelCase { get; }
		public IList<string> PreviousPositionVariableNamesCamelCase { get; }
		public IList<string> PhaseVariableNamesCamelCase { get; }
	}
}