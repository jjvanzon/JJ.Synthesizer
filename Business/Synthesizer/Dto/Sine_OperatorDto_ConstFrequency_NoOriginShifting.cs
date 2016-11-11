using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Sine_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDto_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sine);

        public Sine_OperatorDto_ConstFrequency_NoOriginShifting(double frequency)
            : base(frequency)
        { }
    }
}