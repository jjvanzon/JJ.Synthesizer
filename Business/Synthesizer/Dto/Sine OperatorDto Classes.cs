using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Sine_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sine;
    }

    internal class Sine_OperatorDto_ZeroFrequency : OperatorDtoBase_ZeroFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sine;
    }

    internal class Sine_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sine;
    }

    internal class Sine_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sine;
    }

    internal class Sine_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sine;
    }

    internal class Sine_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sine;
    }
}
