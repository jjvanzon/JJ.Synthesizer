using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Shift_OperatorDto : Shift_OperatorDto_VarSignal_VarDistance
    { }

    internal class Shift_OperatorDto_ConstSignal_ConstDistance : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Shift;

        public double Signal { get; set; }
        public double Distance { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];
    }

    internal class Shift_OperatorDto_ConstSignal_VarDistance : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Shift;

        public double Signal { get; set; }
        public IOperatorDto DistanceOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { DistanceOperatorDto }; }
            set { DistanceOperatorDto = value[0]; }
        }
    }

    internal class Shift_OperatorDto_VarSignal_ConstDistance : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Shift;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double Distance { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }

    internal class Shift_OperatorDto_VarSignal_VarDistance : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Shift;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto DistanceOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, DistanceOperatorDto }; }
            set { SignalOperatorDto = value[0]; DistanceOperatorDto = value[1]; }
        }
    }
}
