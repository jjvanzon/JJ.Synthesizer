using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class BandPassFilterConstantTransitionGain_OperatorValidator : OperatorValidator_Base_WithOperatorType
    {
        public BandPassFilterConstantTransitionGain_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.BandPassFilterConstantTransitionGain,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound },
                expectedDataKeys: new string[0])
        { }
    }
}
