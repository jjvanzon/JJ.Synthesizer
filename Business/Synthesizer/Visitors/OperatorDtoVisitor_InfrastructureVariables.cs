using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_InfrastructureVariables : OperatorDtoVisitorBase
    {
        private readonly int _samplingRate;
        private readonly double _nyquistFrequency;
        private readonly int _targetChannelCount;

        public OperatorDtoVisitor_InfrastructureVariables(int samplingRate, int targetChannelCount)
        {
            _samplingRate = samplingRate;
            _targetChannelCount = targetChannelCount;
            _nyquistFrequency = samplingRate / 2.0;
        }

        public void Execute(IOperatorDto dto)
        {
            Visit_OperatorDto_Polymorphic(dto);
        }

        // Samples

        protected override IOperatorDto Visit_Sample_OperatorDto(Sample_OperatorDto dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Process_OperatorDto_WithTargetChannelCount(dto);
        }

        private IOperatorDto Process_OperatorDto_WithTargetChannelCount(IOperatorDto_WithTargetChannelCount dto)
        {
            dto.TargetChannelCount = _targetChannelCount;

            return dto;
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
            dto.SamplingRate = _samplingRate;
            dto.NyquistFrequency = _nyquistFrequency;

            return base.Visit_OperatorDto_Base(dto);
        }
    }
}
