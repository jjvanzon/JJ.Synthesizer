using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Consts : OperatorDtoBase
    {
        public IList<InputDto> Consts { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => Consts;
            set => Consts = value.ToArray();
        }
    }
}
