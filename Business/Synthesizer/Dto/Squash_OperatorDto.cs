using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    // Const-Const-Zero does not exist.
    // Const-Var-Zero does not exist.

    internal abstract class Squash_OperatorDto : StretchOrSquash_OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }

    internal abstract class Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin : StretchOrSquash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }

    internal abstract class Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin: StretchOrSquash_OperatorDto_VarSignal_VarFactor_ZeroOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }

    // Const-Const-Const does not exist.
    // Const-Const-Var does not exist.
    // Const-Var-Const does not exist.
    // Const-Var-Var does not exist.

    internal abstract class Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin: StretchOrSquash_OperatorDto_VarSignal_ConstFactor_ConstOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }

    internal abstract class Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin: StretchOrSquash_OperatorDto_VarSignal_ConstFactor_VarOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }

    internal abstract class Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin: StretchOrSquash_OperatorDto_VarSignal_VarFactor_ConstOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }

    internal abstract class Squash_OperatorDto_VarSignal_VarFactor_VarOrigin: StretchOrSquash_OperatorDto_VarSignal_VarFactor_VarOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }

    // For Time Dimension

    internal abstract class Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking: StretchOrSquash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }

    internal abstract class Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting: StretchOrSquash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Squash);
    }
}
