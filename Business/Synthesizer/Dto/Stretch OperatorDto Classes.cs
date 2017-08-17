using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Stretch_OperatorDto : StretchOrSquash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Stretch;
    }

    internal class Stretch_OperatorDto_WithOrigin : StretchOrSquash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Stretch;
    }

    internal class Stretch_OperatorDto_ZeroOrigin : StretchOrSquash_OperatorDtoBase_ZeroOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Stretch;
    }

    internal class Stretch_OperatorDto_ConstSignal : StretchOrSquash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Stretch;
    }

    // For Time Dimension

    internal class Stretch_OperatorDto_ConstFactor_WithOriginShifting : StretchOrSquash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Stretch;
    }

    internal class Stretch_OperatorDto_VarFactor_WithPhaseTracking : StretchOrSquash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Stretch;
    }
}
