using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithFrequency : OperatorDtoBase_WithDimension
    {
        public InputDto Frequency { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Frequency };
            set => Frequency = value.ElementAt(0);
        }
    }
}
