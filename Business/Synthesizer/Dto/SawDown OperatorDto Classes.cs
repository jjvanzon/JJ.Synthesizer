using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawDown_OperatorDto : OperatorDtoBase_WithFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SawDown;
    }

    internal class SawDown_OperatorDto_ConstFrequency_NoOriginShifting : SawDown_OperatorDto
    { }

    internal class SawDown_OperatorDto_ConstFrequency_WithOriginShifting : SawDown_OperatorDto
    { }

    internal class SawDown_OperatorDto_VarFrequency_NoPhaseTracking : SawDown_OperatorDto
    { }

    internal class SawDown_OperatorDto_VarFrequency_WithPhaseTracking : SawDown_OperatorDto
    { }
}
