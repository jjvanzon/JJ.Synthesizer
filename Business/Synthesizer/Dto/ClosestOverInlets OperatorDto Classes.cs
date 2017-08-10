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
                Input = value.First();
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
    internal class ClosestOverInlets_OperatorDto_VarInput_2ConstItems : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInlets;

        public InputDto Input { get; set; }
        public InputDto Item1 { get; set; }
        public InputDto Item2 { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Input, Item1, Item2 };
            set
            {
                var array = value.ToArray();
                Input = array[0];
                Item1 = array[1];
                Item2 = array[2];
            }
        }
    }
}