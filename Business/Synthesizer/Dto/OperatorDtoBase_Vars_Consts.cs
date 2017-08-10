using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Vars_Consts : OperatorDtoBase
    {
        public IList<InputDto> Vars { get; set; }
        public IList<InputDto> Consts { get; set; }
           
        public override IEnumerable<InputDto> Inputs
        {
            get => Consts.Union(Vars);
            set
            {
                Consts = value.Where(x => x.IsConst).ToArray();
                Vars = value.Where(x => x.IsVar).ToArray();
            }
        }
    }
}
