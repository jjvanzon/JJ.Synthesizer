using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInletsExp_OperatorDto : ClosestOverInlets_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInletsExp;
    }

    internal class ClosestOverInletsExp_OperatorDto_ConstInput_ConstItems : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInletsExp;

        public double Input { get; set; }
        public IList<double> Items { get; set; }

        public override IEnumerable<InputDto> InputDtos
        {
            get
            {
                yield return new InputDto(Input);

                foreach (double item in Items)
                {
                    yield return new InputDto(item);
                }
            }
        }
    }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_VarItems : ClosestOverInlets_OperatorDto_VarInput_VarItems
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInletsExp;
    }

    internal class ClosestOverInletsExp_OperatorDto_VarInput_ConstItems : ClosestOverInlets_OperatorDto_VarInput_ConstItems
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInletsExp;
    }

    /// <summary> For Machine Optimization </summary>
    internal class ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems : ClosestOverInlets_OperatorDto_VarInput_2ConstItems
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInletsExp;
    }
}
