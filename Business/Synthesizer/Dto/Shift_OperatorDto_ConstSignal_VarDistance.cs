using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
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
}
