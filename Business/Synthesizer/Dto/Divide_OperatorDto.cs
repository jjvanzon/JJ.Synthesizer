using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Divide_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase DenominatorOperatorDto => InputOperatorDtos[1];

        public Divide_OperatorDto(OperatorDtoBase numeratorOperatorDto, OperatorDtoBase denominatorOperatorDto)
            : base(new OperatorDtoBase[] { numeratorOperatorDto, denominatorOperatorDto })
        { }
    }
}
