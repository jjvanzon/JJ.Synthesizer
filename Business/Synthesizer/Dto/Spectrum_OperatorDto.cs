using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Spectrum_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Spectrum);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto StartOperatorDto => InputOperatorDtos[1];
        public OperatorDto EndOperatorDto => InputOperatorDtos[2];
        public OperatorDto FrequencyCountOperatorDto => InputOperatorDtos[3];

        public Spectrum_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto startOperatorDto,
            OperatorDto endOperatorDto,
            OperatorDto frequencyCountOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, startOperatorDto, endOperatorDto, frequencyCountOperatorDto })
        { }
    }
}