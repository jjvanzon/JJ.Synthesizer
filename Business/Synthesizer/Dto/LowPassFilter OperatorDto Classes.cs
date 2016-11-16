using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowPassFilter_OperatorDto : LowPassFilter_OperatorDto_AllVars
    { }

    internal class LowPassFilter_OperatorDto_AllVars : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowPassFilter);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase MixFrequencyOperatorDto { get; set; }
        public OperatorDtoBase BandWidthOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, MixFrequencyOperatorDto, BandWidthOperatorDto }; }
            set { SignalOperatorDto = value[0]; MixFrequencyOperatorDto = value[1]; BandWidthOperatorDto = value[2]; }
        }
    }

    internal class LowPassFilter_OperatorDto_ManyConst : OperatorDtoBase_VarSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowPassFilter);

        public double MixFrequency { get; set; }
        public double BandWidth { get; set; }
    }
}