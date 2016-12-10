using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal class VarsConsts_MathPropertiesDto
    {
        public IList<OperatorDtoBase> VarOperatorDtos { get; set; }
        public IList<double> Consts { get; set; }
        public bool HasVars { get; set; }
        public bool HasConsts { get; set; }
        public bool AllVars { get; set; }
        public bool AllConsts { get; set; }
    }
}
