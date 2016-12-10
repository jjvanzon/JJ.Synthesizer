using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Dto
{
    internal class Sine_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sine);
    }

    internal class Sine_OperatorDto_ZeroFrequency : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sine);
    }

    internal class Sine_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sine);
    }

    internal class Sine_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sine);
    }

    internal class Sine_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sine);
    }

    internal class Sine_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sine);
    }
}
