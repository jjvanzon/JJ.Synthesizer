using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Stretch_OperatorDto : StretchOrSquash_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Stretch);

        public Stretch_OperatorDto(OperatorDto signalOperatorDto, OperatorDto factorOperatorDto, OperatorDto originOperatorDto)
            : base(signalOperatorDto, factorOperatorDto, originOperatorDto)
        { }
    }
}