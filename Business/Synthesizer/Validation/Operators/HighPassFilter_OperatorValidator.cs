using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

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
