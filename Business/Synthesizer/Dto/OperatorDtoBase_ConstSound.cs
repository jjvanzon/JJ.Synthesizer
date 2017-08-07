using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ConstSound : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double Sound { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(Sound) };
    }
}
