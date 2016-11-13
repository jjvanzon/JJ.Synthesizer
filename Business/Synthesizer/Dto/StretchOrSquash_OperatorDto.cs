using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class StretchOrSquash_OperatorDto : OperatorDto
    {
        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto FactorOperatorDto => InputOperatorDtos[1];
        public OperatorDto OriginOperatorDto => InputOperatorDtos[2];

        public StretchOrSquash_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto factorOperatorDto,
            OperatorDto originOperatorDto)
            : base(new OperatorDto[] 
            {
                signalOperatorDto,
                factorOperatorDto,
                originOperatorDto
            })
        { }
    }
}