using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawUp_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDto_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);

        public SawUp_OperatorDto_ConstFrequency_WithOriginShifting(double frequency)
            : base(frequency)
        { }
    }
}