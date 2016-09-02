using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LowPassFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LowPassFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.LowPassFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal })
        { }
    }
}
