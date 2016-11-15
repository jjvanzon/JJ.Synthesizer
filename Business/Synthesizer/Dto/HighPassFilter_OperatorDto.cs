using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class HighPassFilter_OperatorDto : HighPassFilter_OperatorDto_AllVars
    { }

    internal class HighPassFilter_OperatorDto_AllVars : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.HighPassFilter);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase MinFrequencyOperatorDto { get; set; }
        public OperatorDtoBase BandWidthOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, MinFrequencyOperatorDto, BandWidthOperatorDto }; }
            set { SignalOperatorDto = value[0]; MinFrequencyOperatorDto = value[1]; BandWidthOperatorDto = value[2]; }
        }
    }

    internal class HighPassFilter_OperatorDto_ManyConst : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.HighPassFilter);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double MinFrequency { get; set; }
        public double BandWidth { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }
}