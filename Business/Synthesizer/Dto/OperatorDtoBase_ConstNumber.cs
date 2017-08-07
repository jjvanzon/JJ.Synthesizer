using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ConstNumber : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double Number { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(Number) };
    }
}
