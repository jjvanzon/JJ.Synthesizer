using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class NotchFilter_OperatorDto : NotchFilter_OperatorDto_AllVars
    { }

    internal class NotchFilter_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.NotchFilter);
    }

    internal class NotchFilter_OperatorDto_AllVars : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.NotchFilter);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase CenterFrequencyOperatorDto { get; set; }
        public OperatorDtoBase BandWidthOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, CenterFrequencyOperatorDto, BandWidthOperatorDto }; }
            set { SignalOperatorDto = value[0]; CenterFrequencyOperatorDto = value[1]; BandWidthOperatorDto = value[2]; }
        }
    }

    internal class NotchFilter_OperatorDto_ManyConsts : OperatorDtoBase_VarSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowPassFilter);

        public double CenterFrequency { get; set; }
        public double BandWidth { get; set; }
    }
}