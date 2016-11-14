using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInlets_OperatorDto : ClosestOverInlets_OperatorDto_AllVars
    {
        public ClosestOverInlets_OperatorDto(OperatorDtoBase inputOperatorDto, IList<OperatorDtoBase> itemOperatorDtos)
            : base(inputOperatorDto, itemOperatorDtos)
        { }
    }

    internal class ClosestOverInlets_OperatorDto_AllVars : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInlets);

        public OperatorDtoBase InputOperatorDto => InputOperatorDtos[0];
        public IList<OperatorDtoBase> ItemOperatorDtos => InputOperatorDtos.Skip(1).ToArray();

        public ClosestOverInlets_OperatorDto_AllVars(
            OperatorDtoBase inputOperatorDto,
            IList<OperatorDtoBase> itemOperatorDtos)
            : base(inputOperatorDto.Union(itemOperatorDtos).ToArray()) // TODO: Low priority: Not null-safe.
        { }
    }

    internal class ClosestOverInlets_OperatorDto_VarInput_ConstItems : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInlets);

        public OperatorDtoBase InputOperatorDto => InputOperatorDtos[0];
        public IList<double> Items { get; set; }

        public ClosestOverInlets_OperatorDto_VarInput_ConstItems(
            OperatorDtoBase inputOperatorDto,
            IList<double> items)
            : base(new OperatorDtoBase[] { inputOperatorDto })
        {
            if (items == null) throw new NullException(() => items);
            Items = items;
        }
    }

    internal class ClosestOverInlets_OperatorDto_VarInput_2ConstItems : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInlets);

        public OperatorDtoBase InputOperatorDto => InputOperatorDtos[0];
        public double Item1 { get; set; }
        public double Item2 { get; set; }

        public ClosestOverInlets_OperatorDto_VarInput_2ConstItems(
            OperatorDtoBase inputOperatorDto,
            double item1,
            double item2)
            : base(new OperatorDtoBase[] { inputOperatorDto })
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}