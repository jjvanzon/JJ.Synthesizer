using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Add_OperatorDto : OperatorDtoBase_Vars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Add;
    }

    internal class Add_OperatorDto_Vars_Consts : OperatorDtoBase_Vars_Consts
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Add;
    }

    internal class Add_OperatorDto_Vars_NoConsts : OperatorDtoBase_Vars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Add;
    }

    internal class Add_OperatorDto_NoVars_NoConsts : OperatorDtoBase_WithoutInputDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Add;
    }

    internal class Add_OperatorDto_NoVars_Consts : OperatorDtoBase_Consts
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Add;
    }

    // Special Cases

    /// <summary>
    /// Only to be created in MathSimplification_OperatorDtoVisitor,
    /// not yet in ClassSpecialization_OperatorDtoVisitor.
    /// </summary>
    internal class Add_OperatorDto_Vars_1Const : OperatorDtoBase_Vars_1Const
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Add;
    }
}
