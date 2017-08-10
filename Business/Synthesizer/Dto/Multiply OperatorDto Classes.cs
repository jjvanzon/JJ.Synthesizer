using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Multiply_OperatorDto : OperatorDtoBase_Vars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Multiply;
    }

    internal class Multiply_OperatorDto_Vars_Consts : OperatorDtoBase_Vars_Consts
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Multiply;
    }

    internal class Multiply_OperatorDto_Vars_NoConsts : OperatorDtoBase_Vars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Multiply;
    }

    internal class Multiply_OperatorDto_NoVars_NoConsts : OperatorDtoBase_WithoutInputs
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Multiply;
    }

    internal class Multiply_OperatorDto_NoVars_Consts : OperatorDtoBase_Consts
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Multiply;
    }

    // Special Cases

    /// <summary>
    /// Only to be created in MathSimplification_OperatorDtoVisitor,
    /// not yet in ClassSpecialization_OperatorDtoVisitor.
    /// </summary>
    internal class Multiply_OperatorDto_Vars_1Const : OperatorDtoBase_Vars_1Const
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Multiply;
    }
}
