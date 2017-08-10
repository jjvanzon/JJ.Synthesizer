using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Sine_OperatorDto : OperatorDtoBase_WithFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sine;
    }

    internal class Sine_OperatorDto_ZeroFrequency : OperatorDtoBase_ZeroFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sine;
    }

    internal class Sine_OperatorDto_ConstFrequency_NoOriginShifting : Sine_OperatorDto
    { }

    internal class Sine_OperatorDto_ConstFrequency_WithOriginShifting : Sine_OperatorDto
    { }

    internal class Sine_OperatorDto_VarFrequency_NoPhaseTracking : Sine_OperatorDto
    { }

    internal class Sine_OperatorDto_VarFrequency_WithPhaseTracking : Sine_OperatorDto
    { }
}
