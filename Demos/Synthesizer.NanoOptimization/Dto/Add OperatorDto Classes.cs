using System.Collections.Generic;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Add_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => OperatorNames.Add;

        public Add_OperatorDto(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }

    internal class Add_OperatorDto_Vars : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => OperatorNames.Add;

        public Add_OperatorDto_Vars(IList<OperatorDtoBase> vars)
            : base(vars)
        { }
    }

    internal class Add_OperatorDto_Vars_1Const : OperatorDtoBase_Vars_1Const
    {
        public override string OperatorTypeName => OperatorNames.Add;

        public Add_OperatorDto_Vars_1Const(IList<OperatorDtoBase> vars, double constValue)
            : base(vars, constValue)
        { }
    }
}
