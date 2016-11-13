using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Shift_OperatorDto : Shift_OperatorDto_VarSignal_VarDistance
    {
        public Shift_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase distanceOperatorDto)
            : base(signalOperatorDto, distanceOperatorDto)
        { }
    }

    internal class Shift_OperatorDto_ConstSignal_ConstDistance : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public double SignalValue { get; set; }
        public double Distance { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public Shift_OperatorDto_ConstSignal_ConstDistance(double signalValue, double distance)
            : base(new OperatorDtoBase[0])
        {
            SignalValue = signalValue;
            Distance = distance;
        }
    }

    internal class Shift_OperatorDto_ConstSignal_VarDistance : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public double SignalValue { get; set; }
        public OperatorDtoBase DistanceOperatorDto => InputOperatorDtos[0];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public Shift_OperatorDto_ConstSignal_VarDistance(double signalValue, OperatorDtoBase distanceOperatorDto)
            : base(new OperatorDtoBase[] { distanceOperatorDto })
        {
            SignalValue = signalValue;
        }
    }

    internal class Shift_OperatorDto_VarSignal_ConstDistance : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public double Distance { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public Shift_OperatorDto_VarSignal_ConstDistance(OperatorDtoBase signalOperatorDto, double distance)
            : base(new OperatorDtoBase[] { signalOperatorDto })
        {
            Distance = distance;
        }
    }

    internal class Shift_OperatorDto_VarSignal_VarDistance : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase DifferenceOperatorDto => InputOperatorDtos[1];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public Shift_OperatorDto_VarSignal_VarDistance(OperatorDtoBase signalOperatorDto, OperatorDtoBase differenceOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, differenceOperatorDto })
        { }
    }
}
