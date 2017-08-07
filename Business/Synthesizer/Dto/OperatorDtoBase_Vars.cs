using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Vars : OperatorDtoBase
    {
        public IList<IOperatorDto> Vars { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => Vars;
            set => Vars = value;
        }

        public override IEnumerable<InputDto> InputDtos => Vars.Select(x => new InputDto(x)).ToArray();
    }
}
