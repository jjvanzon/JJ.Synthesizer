using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Multiply_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Multiply);

        public Multiply_OperatorDto(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }

    internal class Multiply_OperatorDto_Vars : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Multiply);

        public Multiply_OperatorDto_Vars(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }

    internal class Multiply_OperatorDto_Vars_1Const : OperatorDtoBase_Vars_1Const
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Multiply);

        public Multiply_OperatorDto_Vars_1Const(IList<OperatorDtoBase> vars, double constValue)
            : base(vars, constValue)
        { }
    }
}
