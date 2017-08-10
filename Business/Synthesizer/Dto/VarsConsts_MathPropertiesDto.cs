using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal class VarsConsts_InputDto
    {
        public IList<InputDto> Vars { get; set; }
        public IList<InputDto> Consts { get; set; }
        public bool HasVars { get; set; }
        public bool HasConsts { get; set; }
        public bool AllAreVar { get; set; }
        public bool AllAreConst { get; set; }
    }
}
