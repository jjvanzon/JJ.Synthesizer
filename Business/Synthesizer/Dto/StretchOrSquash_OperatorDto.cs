using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class StretchOrSquash_OperatorDto : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase FactorOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase OriginOperatorDto => InputOperatorDtos[2];

        public StretchOrSquash_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase factorOperatorDto,
            OperatorDtoBase originOperatorDto)
            : base(new OperatorDtoBase[] 
            {
                signalOperatorDto,
                factorOperatorDto,
                originOperatorDto
            })
        { }
    }
}