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
		public IList<InputVariableInfo> InputVariableInfos { get; }
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
			IList<InputVariableInfo> inputVariableInfos,
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
			InputVariableInfos = inputVariableInfos;
			ArrayCalculationInfos = arrayCalculationInfos;
			LongLivedDoubleArrayVariableInfos = longLivedDoubleArrayVariableInfos;
			CalculationMethodCodeList = calculationMethodCodeList;
			ResetMethodCodeList = resetMethodCodeList;
		}
	}
}