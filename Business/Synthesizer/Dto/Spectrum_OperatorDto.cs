using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Spectrum_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Spectrum);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase StartOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase EndOperatorDto => InputOperatorDtos[2];
        public OperatorDtoBase FrequencyCountOperatorDto => InputOperatorDtos[3];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public Spectrum_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase startOperatorDto,
            OperatorDtoBase endOperatorDto,
            OperatorDtoBase frequencyCountOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, startOperatorDto, endOperatorDto, frequencyCountOperatorDto })
        { }
    }
}