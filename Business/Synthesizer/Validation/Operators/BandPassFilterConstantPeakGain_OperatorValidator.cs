using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class BandPassFilterConstantPeakGain_OperatorValidator : OperatorValidator_Base_WithOperatorType
    {
        public BandPassFilterConstantPeakGain_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.BandPassFilterConstantPeakGain,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound },
                expectedDataKeys: new string[0])
        { }
    }
}
