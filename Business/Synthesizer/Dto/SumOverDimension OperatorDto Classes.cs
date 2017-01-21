using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumOverDimension;
    }

    internal class SumOverDimension_OperatorDto_AllConsts : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumOverDimension;

        public double Signal { get; set; }
        public double From { get; set; }
        public double Till { get; set; }
        public double Step { get; set; }
    }

    internal class SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : SumOverDimension_OperatorDto
    { }

    internal class SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : SumOverDimension_OperatorDto
    { }
}
