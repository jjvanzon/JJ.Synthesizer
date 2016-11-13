using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxOverInlets_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MaxOverInlets);

        public MaxOverInlets_OperatorDto(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }
}
