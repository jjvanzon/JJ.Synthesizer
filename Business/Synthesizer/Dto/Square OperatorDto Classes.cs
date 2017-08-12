using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Square_OperatorDto : OperatorDtoBase_WithFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Square;
    }

    internal class Square_OperatorDto_ConstFrequency_NoOriginShifting : Square_OperatorDto
    { }

    internal class Square_OperatorDto_ConstFrequency_WithOriginShifting : Square_OperatorDto
    { }

    internal class Square_OperatorDto_VarFrequency_NoPhaseTracking : Square_OperatorDto
    { }

    internal class Square_OperatorDto_VarFrequency_WithPhaseTracking : Square_OperatorDto
    { }
}
