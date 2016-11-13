using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Triangle_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Triangle);

        public Triangle_OperatorDto(OperatorDtoBase frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }

    internal class Triangle_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Triangle);

        public Triangle_OperatorDto_ConstFrequency_NoOriginShifting(double frequency)
            : base(frequency)
        { }
    }

    internal class Triangle_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Triangle);

        public Triangle_OperatorDto_ConstFrequency_WithOriginShifting(double frequency)
            : base(frequency)
        { }
    }

    internal class Triangle_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Triangle);

        public Triangle_OperatorDto_VarFrequency_NoPhaseTracking(OperatorDtoBase frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }

    internal class Triangle_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Triangle);

        public Triangle_OperatorDto_VarFrequency_WithPhaseTracking(OperatorDtoBase frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}
