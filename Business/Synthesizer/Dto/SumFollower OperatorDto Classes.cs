using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumFollower_OperatorDto : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumFollower);
    }

    internal class SumFollower_OperatorDto_AllVars : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumFollower);
    }

    internal class SumFollower_OperatorDto_ConstSignal_VarSampleCount : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumFollower);

        public double Signal { get; set; }
        public OperatorDtoBase SampleCountOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SampleCountOperatorDto }; }
            set { SampleCountOperatorDto = value[0]; }
        }
    }

    internal class SumFollower_OperatorDto_ConstSignal_ConstSampleCount : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumFollower);

        public double Signal { get; set; }
        public double SampleCount { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }
}