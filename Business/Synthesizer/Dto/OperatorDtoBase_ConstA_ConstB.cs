using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ConstA_ConstB : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double A { get; set; }
        public double B { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(A), new InputDto(B) };
    }
}
