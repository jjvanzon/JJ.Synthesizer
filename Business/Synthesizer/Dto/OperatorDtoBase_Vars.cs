using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Vars : OperatorDtoBase
    {
        public IList<InputDto> Vars { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => Vars.ToArray();
            set => Vars = value.ToArray();
        }
    }
}
