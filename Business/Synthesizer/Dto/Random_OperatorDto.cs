using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Random_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Random);

        public OperatorDtoBase RateOperatorDto => InputOperatorDtos[0];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        public Random_OperatorDto(OperatorDtoBase rateOperatorDto)
            : base(new OperatorDtoBase[] { rateOperatorDto })
        { }
    }
}