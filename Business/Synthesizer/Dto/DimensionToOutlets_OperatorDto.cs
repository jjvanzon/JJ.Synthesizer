using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class DimensionToOutlets_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.DimensionToOutlets);

        public OperatorDtoBase OperandOperatorDto => InputOperatorDtos[0];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public DimensionToOutlets_OperatorDto(OperatorDtoBase operandOperatorDto)
            : base(new OperatorDtoBase[] { operandOperatorDto })
        { }
    }
}
