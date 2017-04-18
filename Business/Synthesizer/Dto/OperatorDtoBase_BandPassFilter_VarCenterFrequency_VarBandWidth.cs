using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth : OperatorDtoBase_Filter_VarSignal
    {
        public IOperatorDto CenterFrequencyOperatorDto { get; set; }
        public IOperatorDto BandWidthOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, CenterFrequencyOperatorDto, BandWidthOperatorDto };
            set { SignalOperatorDto = value[0]; CenterFrequencyOperatorDto = value[1]; BandWidthOperatorDto = value[2]; }
        }
    }
}