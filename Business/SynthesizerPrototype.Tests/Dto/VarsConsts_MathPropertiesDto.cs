using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Tests.Dto
{
    internal class VarsConsts_MathPropertiesDto
    {
        public IList<OperatorDtoBase> Vars { get; set; }
        public IList<double> Consts { get; set; }
        public bool HasVars { get; set; }
        public bool HasConsts { get; set; }
        public bool AllAreVar { get; set; }
        public bool AllAreConst { get; set; }
    }
}
