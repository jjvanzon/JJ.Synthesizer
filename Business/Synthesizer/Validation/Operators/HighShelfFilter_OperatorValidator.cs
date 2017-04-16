using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class HighShelfFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public HighShelfFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.HighShelfFilter,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}