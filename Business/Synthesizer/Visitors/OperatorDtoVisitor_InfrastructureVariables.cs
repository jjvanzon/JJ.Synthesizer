using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_InfrastructureVariables : OperatorDtoVisitorBase
    {
        // TODO: Also fill in ChannelIndex, ChannelCount and other infra variables, now passed around through other processing code.
        // TODO: Add and fill in more infrastructure variables in more DTO's.

        private readonly int _samplingRate;
        private readonly double _nyquistFrequency;

        public OperatorDtoVisitor_InfrastructureVariables(int samplingRate)
        {
            _samplingRate = samplingRate;
            _nyquistFrequency = samplingRate / 2.0;
        }

        public void Execute(OperatorDtoBase dto)
        {
            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto)
        {
            return Process_OperatorDtoBase_Filter_VarSignal(dto);
        }

        private OperatorDtoBase Process_OperatorDtoBase_Filter_VarSignal(OperatorDtoBase_Filter_VarSignal dto)
        {
            dto.SamplingRate = _samplingRate;
            dto.NyquistFrequency = _nyquistFrequency;

            return base.Visit_OperatorDto_Base(dto);
        }
    }
}
