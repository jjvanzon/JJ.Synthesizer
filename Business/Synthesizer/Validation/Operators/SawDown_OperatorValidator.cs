using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SawDown_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SawDown_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.SawDown,
                new DimensionEnum[] { DimensionEnum.Frequency },
                new DimensionEnum[] { DimensionEnum.Signal })
        { }
    }
}