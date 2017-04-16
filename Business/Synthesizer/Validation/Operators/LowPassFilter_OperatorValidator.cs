using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LowPassFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LowPassFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.LowPassFilter,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}
