using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Square_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Square;
    }

    internal class Square_OperatorDto_ZeroFrequency : OperatorDtoBase_ZeroFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Square;
    }

    internal class Square_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Square;
    }

    internal class Square_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Square;
    }

    internal class Square_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Square;
    }

    internal class Square_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Square;
    }
}
