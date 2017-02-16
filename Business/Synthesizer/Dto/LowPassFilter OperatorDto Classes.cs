using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowPassFilter_OperatorDto : LowPassFilter_OperatorDto_AllVars
    { }

    internal class LowPassFilter_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;
    }

    internal class LowPassFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;

        public IOperatorDto MaxFrequencyOperatorDto { get; set; }
        public IOperatorDto BandWidthOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, MaxFrequencyOperatorDto, BandWidthOperatorDto }; }
            set { SignalOperatorDto = value[0]; MaxFrequencyOperatorDto = value[1]; BandWidthOperatorDto = value[2]; }
        }
    }

    internal class LowPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithBandWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;
        public override double Frequency => MaxFrequency;

        public double MaxFrequency { get; set; }
    }
}