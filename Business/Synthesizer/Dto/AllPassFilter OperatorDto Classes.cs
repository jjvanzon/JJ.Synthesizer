using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AllPassFilter_OperatorDto : AllPassFilter_OperatorDto_AllVars
    { }

    internal class AllPassFilter_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;
    }

    internal class AllPassFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;

        public OperatorDtoBase CenterFrequencyOperatorDto { get; set; }
        public OperatorDtoBase BandWidthOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, CenterFrequencyOperatorDto, BandWidthOperatorDto }; }
            set { SignalOperatorDto = value[0]; CenterFrequencyOperatorDto = value[1]; BandWidthOperatorDto = value[2]; }
        }
    }

    internal class AllPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithBandWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;
        public override double Frequency => CenterFrequency;

        public double CenterFrequency { get; set; }
    }
}