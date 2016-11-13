using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Add_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Add);

        public Add_OperatorDto(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }
}
