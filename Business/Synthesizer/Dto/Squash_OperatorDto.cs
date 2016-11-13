using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Squash_OperatorDto : StretchOrSquash_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);

        public Squash_OperatorDto(OperatorDto signalOperatorDto, OperatorDto factorOperatorDto, OperatorDto originOperatorDto)
            : base(signalOperatorDto, factorOperatorDto, originOperatorDto)
        { }
    }
}