using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal abstract class OperatorDtoBase_WithoutInputs : OperatorDtoBase
    {
        private static readonly IReadOnlyList<InputDto> _inputs = new InputDto[0];

        public sealed override IReadOnlyList<InputDto> Inputs
        {
            get => _inputs;
            set { }
        }
    }
}
