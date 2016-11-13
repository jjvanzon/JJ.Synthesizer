using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Random_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Random);

        public OperatorDto RateOperatorDto => InputOperatorDtos[0];

        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        public Random_OperatorDto(OperatorDto rateOperatorDto)
            : base(new OperatorDto[] { rateOperatorDto })
        { }
    }
}