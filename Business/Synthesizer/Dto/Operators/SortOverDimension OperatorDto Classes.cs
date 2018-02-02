using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class SortOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SortOverDimension;
	}

	internal class SortOverDimension_OperatorDto_ConstSignal : SortOverDimension_OperatorDto
	{ }

	internal class SortOverDimension_OperatorDto_CollectionRecalculationContinuous : SortOverDimension_OperatorDto
	{ }

	internal class SortOverDimension_OperatorDto_CollectionRecalculationUponReset : SortOverDimension_OperatorDto
	{ }
}
