using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
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
}
