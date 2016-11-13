using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Triangle_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Triangle);

        public Triangle_OperatorDto_ConstFrequency_WithOriginShifting(double frequency)
            : base(frequency)
        { }
    }
}