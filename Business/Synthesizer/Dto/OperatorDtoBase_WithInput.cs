using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithInput : OperatorDtoBase
    {
        public InputDto Input { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Input };
            set => Input = value.ElementAt(0);
        }
    }
}
