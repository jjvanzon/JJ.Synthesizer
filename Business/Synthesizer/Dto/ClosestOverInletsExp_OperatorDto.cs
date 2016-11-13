using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInletsExp_OperatorDto : ClosestOverInlets_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);

        public ClosestOverInletsExp_OperatorDto(
            OperatorDtoBase inputOperatorDto, 
            IList<OperatorDtoBase> itemOperatorDtos) 
            : base(inputOperatorDto, itemOperatorDtos)
        { }
    }
}
