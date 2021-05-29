using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal abstract class OperatorDtoBase_WithSignal : OperatorDtoBase, IOperatorDto_WithSignal
    {
        public InputDto Signal { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal };
            set => Signal = value.ElementAtOrDefault(0);
        }
    }
}
