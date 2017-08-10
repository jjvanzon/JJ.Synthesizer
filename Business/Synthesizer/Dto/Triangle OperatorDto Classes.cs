using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Triangle_OperatorDto : OperatorDtoBase_WithFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Triangle;
    }

    internal class Triangle_OperatorDto_ZeroFrequency : OperatorDtoBase_ZeroFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Triangle;
    }

    internal class Triangle_OperatorDto_ConstFrequency_NoOriginShifting : Triangle_OperatorDto
    { }

    internal class Triangle_OperatorDto_ConstFrequency_WithOriginShifting : Triangle_OperatorDto
    { }

    internal class Triangle_OperatorDto_VarFrequency_NoPhaseTracking : Triangle_OperatorDto
    { }

    internal class Triangle_OperatorDto_VarFrequency_WithPhaseTracking : Triangle_OperatorDto
    { }
}
