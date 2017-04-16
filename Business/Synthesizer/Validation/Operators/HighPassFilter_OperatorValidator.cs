using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class HighPassFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public HighPassFilter_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.HighPassFilter,
                  new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new[] { DimensionEnum.Signal })
        { }
    }
}
