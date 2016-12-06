using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumOverDimension);
    }

    internal class SumOverDimension_OperatorDto_AllConsts : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumOverDimension);

        public double Signal { get; set; }
        public double From { get; set; }
        public double Till { get; set; }
        public double Step { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }

    internal class SumOverDimension_OperatorDto_ConstSignal_VarFrom_VarTill_VarStep : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumOverDimension);

        public double Signal { get; set; }
        public OperatorDtoBase FromOperatorDto { get; set; }
        public OperatorDtoBase TillOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }

        public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FromOperatorDto, TillOperatorDto, StepOperatorDto }; }
            set { FromOperatorDto = value[0]; TillOperatorDto = value[1]; StepOperatorDto = value[2]; }
        }
    }

    internal class SumOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : SumOverDimension_OperatorDto
    { }

    internal class SumOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : SumOverDimension_OperatorDto
    { }
}
