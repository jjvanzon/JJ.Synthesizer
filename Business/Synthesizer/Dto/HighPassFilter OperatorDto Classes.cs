using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class HighPassFilter_OperatorDto : HighPassFilter_OperatorDto_AllVars
    { }

    internal class HighPassFilter_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;
    }

    internal class HighPassFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;

        public OperatorDtoBase MinFrequencyOperatorDto { get; set; }
        public OperatorDtoBase BandWidthOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, MinFrequencyOperatorDto, BandWidthOperatorDto }; }
            set { SignalOperatorDto = value[0]; MinFrequencyOperatorDto = value[1]; BandWidthOperatorDto = value[2]; }
        }
    }

    internal class HighPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithBandWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;
        public override double Frequency => MinFrequency;

        public double MinFrequency { get; set; }
    }
}