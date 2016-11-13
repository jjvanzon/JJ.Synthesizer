using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PeakingEQFilter_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.PeakingEQFilter);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase CenterFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase BandWidthOperatorDto => InputOperatorDtos[2];
        public OperatorDtoBase DBGainOperatorDto => InputOperatorDtos[3];

        public PeakingEQFilter_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase centerFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto,
            OperatorDtoBase dbGainOperatorDto)
            : base(new OperatorDtoBase[] 
            {
                signalOperatorDto,
                centerFrequencyOperatorDto,
                bandWidthOperatorDto,
                dbGainOperatorDto
            })
        { }
    }
}
