using System;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithoutInputDtos : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public sealed override IEnumerable<InputDto> InputDtos => new InputDto[0];
    }
}
