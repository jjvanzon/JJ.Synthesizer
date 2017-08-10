using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithoutInputs : OperatorDtoBase
    {
        private static readonly IEnumerable<InputDto> _inputs = new InputDto[0];

        public sealed override IEnumerable<InputDto> Inputs
        {
            get => _inputs;
            set { }
        }
    }
}
