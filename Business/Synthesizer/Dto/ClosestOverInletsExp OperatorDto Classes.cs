using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInletsExp_OperatorDto : ClosestOverInlets_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInletsExp;
    }

    internal class ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems : ClosestOverInletsExp_OperatorDto
    { }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_VarItems : ClosestOverInletsExp_OperatorDto
    { }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_ConstItems : ClosestOverInletsExp_OperatorDto
    { }

    /// <summary> For Machine Optimization </summary>
    internal class ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems : ClosestOverInlets_OperatorDto_VarInput_2ConstItems
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInletsExp;
    }
}
