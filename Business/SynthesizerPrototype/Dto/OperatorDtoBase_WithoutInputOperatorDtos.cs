using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public abstract class OperatorDtoBase_WithoutInputOperatorDtos : OperatorDtoBase
    {
        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];
    }
}
