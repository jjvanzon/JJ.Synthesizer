using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ZeroFrequency : OperatorDtoBase
    {
        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(0) };
    }
}