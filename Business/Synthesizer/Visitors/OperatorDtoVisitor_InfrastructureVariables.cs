using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_InfrastructureVariables : OperatorDtoVisitorBase
    {
        private readonly int _targetSamplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _targetChannelCount;

        public OperatorDtoVisitor_InfrastructureVariables(int targetSamplingRate, int targetChannelCount)
        {
            _targetSamplingRate = targetSamplingRate;
            _targetChannelCount = targetChannelCount;
            _nyquistFrequency = targetSamplingRate / 2.0;
        }

        public void Execute(IOperatorDto dto)
        {
            Visit_OperatorDto_Polymorphic(dto);
        }

        // GetDimension

        protected override IOperatorDto Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            dto.SamplingRate = _targetSamplingRate;

            return base.Visit_GetDimension_OperatorDto(dto);
        }

        // Filters

        protected override IOperatorDto Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        protected override IOperatorDto Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        protected override IOperatorDto Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        protected override IOperatorDto Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        protected override IOperatorDto Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        protected override IOperatorDto Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        protected override IOperatorDto Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        protected override IOperatorDto Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto)
        {
            return Process_Filter_OperatorDto_VarSound(dto);
        }

        private IOperatorDto Process_Filter_OperatorDto_VarSound(OperatorDtoBase_Filter_VarSound dto)
        {
            dto.TargetSamplingRate = _targetSamplingRate;
            dto.NyquistFrequency = _nyquistFrequency;

            return base.Visit_OperatorDto_Base(dto);
        }

        // Samples

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto(SampleWithRate1_OperatorDto dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_MonoToStereo(SampleWithRate1_OperatorDto_MonoToStereo dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_NoChannelConversion(SampleWithRate1_OperatorDto_NoChannelConversion dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_NoSample(SampleWithRate1_OperatorDto_NoSample dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_SampleWithRate1_OperatorDto_StereoToMono(SampleWithRate1_OperatorDto_StereoToMono dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        private IOperatorDto Process_OperatorDto_WithTargetChannelCount(IOperatorDto_WithTargetChannelCount dto)
        {
            dto.TargetChannelCount = _targetChannelCount;

            return dto;
        }
    }
}
