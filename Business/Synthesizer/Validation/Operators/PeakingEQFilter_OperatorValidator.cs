using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PeakingEQFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public PeakingEQFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.PeakingEQFilter,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}