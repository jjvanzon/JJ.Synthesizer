using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawUp_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);
    }

    internal class SawUp_OperatorDto_ZeroFrequency : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);
    }

    internal class SawUp_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);
    }

    internal class SawUp_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);
    }

    internal class SawUp_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);
    }

    internal class SawUp_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);
    }
}
