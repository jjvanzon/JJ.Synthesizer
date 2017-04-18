using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInlets_OperatorDto : ClosestOverInlets_OperatorDto_VarInput_VarItems
    { }

    internal class ClosestOverInlets_OperatorDto_ConstInput_ConstItems : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInlets;

        public double Input { get; set; }
        public IList<double> Items { get; set; }
    }

    internal class ClosestOverInlets_OperatorDto_VarInput_VarItems : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInlets;

        public IOperatorDto InputOperatorDto { get; set; }
        public IList<IOperatorDto> ItemOperatorDtos { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => InputOperatorDto.Union(ItemOperatorDtos).ToArray();
            set { InputOperatorDto = value[0]; ItemOperatorDtos = value.Skip(1).ToArray(); }
        }
    }

    internal class ClosestOverInlets_OperatorDto_VarInput_ConstItems : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInlets;

        public IOperatorDto InputOperatorDto { get; set; }
        public IList<double> Items { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { InputOperatorDto };
            set => InputOperatorDto = value[0];
        }
    }

    /// <summary> For Machine Optimization </summary>
    internal class ClosestOverInlets_OperatorDto_VarInput_2ConstItems : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInlets;

        public IOperatorDto InputOperatorDto { get; set; }
        public double Item1 { get; set; }
        public double Item2 { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { InputOperatorDto };
            set => InputOperatorDto = value[0];
        }
    }
}