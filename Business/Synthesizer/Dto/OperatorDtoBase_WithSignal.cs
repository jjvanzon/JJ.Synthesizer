using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithSignal : OperatorDtoBase, IOperatorDto_WithSignal
    {
        public InputDto Signal { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Signal };
            set => Signal = value.ElementAt(0);
        }
    }
}
