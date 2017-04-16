using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class BandPassFilterConstantPeakGain_OperatorValidator : OperatorValidator_Base
    {
        public BandPassFilterConstantPeakGain_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.BandPassFilterConstantPeakGain,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal },
                expectedDataKeys: new string[0])
        { }
    }
}
