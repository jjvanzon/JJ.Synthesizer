using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

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