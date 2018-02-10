using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.WithInheritance.Calculation;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithInheritance.Visitors;

namespace JJ.Business.SynthesizerPrototype.Tests.Helpers.WithInheritance
{
	internal static class OperatorCalculatorFactory
	{
		public static OperatorCalculatorBase CreateOperatorCalculatorFromDto(IOperatorDto dto, DimensionStack dimensionStack)
		{
			var visitor = new OperatorDtoToOperatorCalculatorVisitor(dimensionStack);
			OperatorCalculatorBase calculator = visitor.Execute(dto);

			return calculator;
		}
	}
}
