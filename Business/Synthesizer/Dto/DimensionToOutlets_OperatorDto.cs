using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class DimensionToOutlets_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.DimensionToOutlets);

        public OperatorDtoBase OperandOperatorDto => InputOperatorDtos[0];

        public DimensionToOutlets_OperatorDto(OperatorDtoBase operandOperatorDto)
            : base(new OperatorDtoBase[] { operandOperatorDto })
        { }
    }
}
