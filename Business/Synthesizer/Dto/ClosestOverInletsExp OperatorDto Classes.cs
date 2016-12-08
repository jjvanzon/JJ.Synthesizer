using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInletsExp_OperatorDto : ClosestOverInlets_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);
    }

    internal class ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);

        public double Input { get; set; }
        public IList<double> Items { get; set; }
    }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_VarItems : ClosestOverInlets_OperatorDto_VarInput_VarItems
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);
    }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_ConstItems : ClosestOverInlets_OperatorDto_VarInput_ConstItems
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);
    }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems : ClosestOverInlets_OperatorDto_VarInput_2ConstItems
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);
    }
}
