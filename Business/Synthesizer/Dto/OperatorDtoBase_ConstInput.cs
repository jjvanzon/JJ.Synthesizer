using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ConstInput : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double Input { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(Input) };
    }
}
