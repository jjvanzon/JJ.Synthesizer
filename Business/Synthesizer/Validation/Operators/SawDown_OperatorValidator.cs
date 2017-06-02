using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SawDown_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SawDown_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.SawDown,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Sound })
        { }
    }
}