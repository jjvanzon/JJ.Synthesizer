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

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
        {
            dto.SamplingRate = _samplingRate;
            dto.NyquistFrequency = _nyquistFrequency;

            return base.Visit_LowPassFilter_OperatorDto(dto);
        }
    }
}
