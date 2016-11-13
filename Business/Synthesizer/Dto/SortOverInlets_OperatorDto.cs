using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SortOverInlets_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SortOverInlets);

        public SortOverInlets_OperatorDto(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }
}
