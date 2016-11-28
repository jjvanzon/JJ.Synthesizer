using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInlets_OperatorDto : ClosestOverInlets_OperatorDto_VarInput_VarItems
    { }

    internal class ClosestOverInlets_OperatorDto_VarInput_VarItems : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInlets);

        public OperatorDtoBase InputOperatorDto { get; set; }
        public IList<OperatorDtoBase> ItemOperatorDtos { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return InputOperatorDto.Union(ItemOperatorDtos).ToArray(); }
            set { InputOperatorDto = value[0]; ItemOperatorDtos = value.Skip(1).ToArray(); }
        }
    }

    internal class ClosestOverInlets_OperatorDto_VarInput_ConstItems : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInlets);

        public OperatorDtoBase InputOperatorDto { get; set; }
        public IList<double> Items { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { InputOperatorDto }; }
            set { InputOperatorDto = value[0]; }
        }
    }

    internal class ClosestOverInlets_OperatorDto_VarInput_2ConstItems : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInlets);

        public OperatorDtoBase InputOperatorDto { get; set; }
        public double Item1 { get; set; }
        public double Item2 { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { InputOperatorDto }; }
            set { InputOperatorDto = value[0]; }
        }
    }
}