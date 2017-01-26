using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LowShelfFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LowShelfFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.LowShelfFilter,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}