using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Select_OperatorDto : Select_OperatorDto_VarPosition
    { }

    internal class Select_OperatorDto_VarPosition : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Select);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase PositionOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, PositionOperatorDto }; }
            set { SignalOperatorDto = value[0]; PositionOperatorDto = value[1]; }
        }
    }

    internal class Select_OperatorDto_ConstPosition : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Select);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Position { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }
}
