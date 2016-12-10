using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinOverInlets_OperatorDto : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    internal class MinOverInlets_OperatorDto_Vars_Consts : OperatorDtoBase_Vars_Consts
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    internal class MinOverInlets_OperatorDto_Vars_NoConsts : OperatorDtoBase_Vars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    internal class MinOverInlets_OperatorDto_NoVars_NoConsts : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    internal class MinOverInlets_OperatorDto_NoVars_Consts : OperatorDtoBase_Consts
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    /// <summary> For Math Simplification </summary>
    internal class MinOverInlets_OperatorDto_Vars_1Const : OperatorDtoBase_Vars_1Const
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    /// <summary> For Machine Optimization </summary>
    internal class MinOverInlets_OperatorDto_1Var_1Const : OperatorDtoBase_VarA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }

    /// <summary> For Machine Optimization </summary>
    internal class MinOverInlets_OperatorDto_2Vars : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverInlets);
    }
}
