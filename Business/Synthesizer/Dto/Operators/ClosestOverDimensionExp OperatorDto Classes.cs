using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class ClosestOverDimensionExp_OperatorDto : ClosestOverDimension_OperatorDto
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverDimensionExp;
	}

	internal class ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous : ClosestOverDimensionExp_OperatorDto
	{ }

	internal class ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset : ClosestOverDimensionExp_OperatorDto
	{ }
}
