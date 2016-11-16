using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinOverInlets_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    internal class MinOverInlets_OperatorDto_Vars_1Const : OperatorDtoBase_Vars_1Const
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    internal class MinOverInlets_OperatorDto_AllVars : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    internal class MinOverInlets_OperatorDto_1Var_1Const : OperatorDtoBase_VarA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    internal class MinOverInlets_OperatorDto_2Vars : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }
}
