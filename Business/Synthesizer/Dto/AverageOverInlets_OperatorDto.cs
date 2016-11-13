using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageOverInlets_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageOverDimension);

        public AverageOverInlets_OperatorDto(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }
}
