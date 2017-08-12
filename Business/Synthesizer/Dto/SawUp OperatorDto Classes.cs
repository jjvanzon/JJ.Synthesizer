using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawUp_OperatorDto : OperatorDtoBase_WithFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SawUp;
    }

    internal class SawUp_OperatorDto_ConstFrequency_NoOriginShifting : SawUp_OperatorDto
    { }

    internal class SawUp_OperatorDto_ConstFrequency_WithOriginShifting : SawUp_OperatorDto
    { }

    internal class SawUp_OperatorDto_VarFrequency_NoPhaseTracking : SawUp_OperatorDto
    { }

    internal class SawUp_OperatorDto_VarFrequency_WithPhaseTracking : SawUp_OperatorDto
    { }
}
