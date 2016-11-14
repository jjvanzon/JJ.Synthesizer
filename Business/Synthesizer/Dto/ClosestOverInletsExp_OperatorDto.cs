using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInletsExp_OperatorDto : ClosestOverInlets_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);

        public ClosestOverInletsExp_OperatorDto(OperatorDtoBase inputOperatorDto, IList<OperatorDtoBase> itemOperatorDtos) 
            : base(inputOperatorDto, itemOperatorDtos)
        { }
    }

    internal class ClosestOverInletsExp_OperatorDto_AllVars : ClosestOverInlets_OperatorDto_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);

        public ClosestOverInletsExp_OperatorDto_AllVars(OperatorDtoBase inputOperatorDto, IList<OperatorDtoBase> itemOperatorDtos)
            : base(inputOperatorDto, itemOperatorDtos)
        { }
    }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_ConstItems : ClosestOverInlets_OperatorDto_VarInput_ConstItems
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);

        public ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(OperatorDtoBase inputOperatorDto, IList<double> items)
            : base(inputOperatorDto, items)
        { }
    }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems : ClosestOverInlets_OperatorDto_VarInput_2ConstItems
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInletsExp);

        public ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems(OperatorDtoBase inputOperatorDto, double item1, double item2)
            : base(inputOperatorDto, item1, item2)
        { }
    }
}
