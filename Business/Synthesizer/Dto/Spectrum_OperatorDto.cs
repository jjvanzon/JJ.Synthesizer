using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Spectrum_OperatorDto : Spectrum_OperatorDto_AllVars
    { }

    internal class Spectrum_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Spectrum;
    }

    internal class Spectrum_OperatorDto_AllVars : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Spectrum;

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase StartOperatorDto { get; set; }
        public OperatorDtoBase EndOperatorDto { get; set; }
        public OperatorDtoBase FrequencyCountOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, StartOperatorDto, EndOperatorDto, FrequencyCountOperatorDto }; } 
            set
            {
                SignalOperatorDto = value[0];
                StartOperatorDto = value[1];
                EndOperatorDto = value[2];
                FrequencyCountOperatorDto = value[3];
            }
        }
    }
}