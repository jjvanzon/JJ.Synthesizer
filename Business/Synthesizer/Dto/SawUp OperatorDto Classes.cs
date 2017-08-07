using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawUp_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SawUp;
    }

    internal class SawUp_OperatorDto_ZeroFrequency : OperatorDtoBase_ZeroFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SawUp;
    }

    internal class SawUp_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SawUp;
    }

    internal class SawUp_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SawUp;
    }

    internal class SawUp_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SawUp;
    }

    internal class SawUp_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SawUp;
    }
}
