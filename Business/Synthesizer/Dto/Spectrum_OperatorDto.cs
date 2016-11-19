using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Spectrum_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Spectrum);

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