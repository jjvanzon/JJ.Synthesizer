using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInlets_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInlets;

        public InputDto Input { get; set; }
        public IList<InputDto> Items { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => Input.Union(Items);
            set
            {
                Input = value.FirstOrDefault();
                Items = value.Skip(1).ToArray();
            }
        }
    }

    internal class ClosestOverInlets_OperatorDto_ConstInput_ConstItems : ClosestOverInlets_OperatorDto
    { }

    internal class ClosestOverInlets_OperatorDto_VarInput_VarItems : ClosestOverInlets_OperatorDto
    { }

    internal class ClosestOverInlets_OperatorDto_VarInput_ConstItems : ClosestOverInlets_OperatorDto
    { }

    /// <summary> For Machine Optimization </summary>
    internal class ClosestOverInlets_OperatorDto_VarInput_2ConstItems : ClosestOverInlets_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInlets;

        public InputDto Item1 { get => Items[0]; set => Items[0] = value; }
        public InputDto Item2 { get => Items[1]; set => Items[1] = value; }
    }
}