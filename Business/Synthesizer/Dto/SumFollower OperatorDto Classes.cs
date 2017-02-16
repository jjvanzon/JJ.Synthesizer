using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumFollower_OperatorDto : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollower;
    }

    internal class SumFollower_OperatorDto_AllVars : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollower;
    }

    /// <summary> Slice length does not matter in this case. </summary>
    internal class SumFollower_OperatorDto_ConstSignal_VarSampleCount : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollower;

        public double Signal { get; set; }
        public IOperatorDto SampleCountOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SampleCountOperatorDto }; }
            set { SampleCountOperatorDto = value[0]; }
        }
    }

    /// <summary> Slice length does not matter in this case. </summary>
    internal class SumFollower_OperatorDto_ConstSignal_ConstSampleCount : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollower;

        public double Signal { get; set; }
        public double SampleCount { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];
    }
}