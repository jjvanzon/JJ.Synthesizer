using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Squash_OperatorDto : StretchOrSquash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_ConstSignal : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin : StretchOrSquash_OperatorDtoBase_ZeroOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin : StretchOrSquash_OperatorDtoBase_ZeroOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin : StretchOrSquash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin : StretchOrSquash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin : StretchOrSquash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarSignal_VarFactor_VarOrigin : StretchOrSquash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    // For Time Dimension

    internal class Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting : StretchOrSquash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking : StretchOrSquash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }
}
