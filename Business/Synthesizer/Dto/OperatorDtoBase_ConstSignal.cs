using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ConstSignal : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double Signal { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(Signal) };
    }
}
