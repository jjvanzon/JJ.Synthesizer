using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class NotchFilter_OperatorDto : NotchFilter_OperatorDto_AllVars
    { }

    internal class NotchFilter_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.NotchFilter;
    }

    internal class NotchFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.NotchFilter;

        public IOperatorDto CenterFrequencyOperatorDto { get; set; }
        public IOperatorDto BandWidthOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, CenterFrequencyOperatorDto, BandWidthOperatorDto }; }
            set { SignalOperatorDto = value[0]; CenterFrequencyOperatorDto = value[1]; BandWidthOperatorDto = value[2]; }
        }
    }

    internal class NotchFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithBandWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;
        public override double Frequency => CenterFrequency;

        public double CenterFrequency { get; set; }
    }
}