using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Shift_OperatorDto : Shift_OperatorDto_VarSignal_VarDistance
    { }

    internal class Shift_OperatorDto_ConstSignal_ConstDistance : OperatorDtoBase_WithoutInputOperatorDtos, IOperatorDto_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public double SignalValue { get; set; }
        public double Distance { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }

    internal class Shift_OperatorDto_ConstSignal_VarDistance : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public double SignalValue { get; set; }
        public OperatorDtoBase DistanceOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { DistanceOperatorDto }; }
            set { DistanceOperatorDto = value[0]; }
        }
    }

    internal class Shift_OperatorDto_VarSignal_ConstDistance : OperatorDtoBase_VarSignal, IOperatorDto_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public double Distance { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }

    internal class Shift_OperatorDto_VarSignal_VarDistance : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase DifferenceOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, DifferenceOperatorDto }; }
            set { SignalOperatorDto = value[0]; DifferenceOperatorDto = value[1]; }
        }
    }
}
