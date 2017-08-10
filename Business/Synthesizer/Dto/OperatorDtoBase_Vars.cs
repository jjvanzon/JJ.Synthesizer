using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Vars : OperatorDtoBase
    {
        public IList<InputDto> Vars { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => Vars;
            set => Vars = value.ToArray();
        }
    }
}
