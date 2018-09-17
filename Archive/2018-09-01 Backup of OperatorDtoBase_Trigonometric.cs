using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal abstract class OperatorDtoBase_Trigonometric : OperatorDtoBase
    {
        public InputDto Radians { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Radians };
            set => Radians = value.ElementAtOrDefault(0);
        }
    }
}
