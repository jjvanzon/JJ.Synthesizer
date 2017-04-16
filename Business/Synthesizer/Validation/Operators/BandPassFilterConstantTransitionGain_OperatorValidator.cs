using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class BandPassFilterConstantTransitionGain_OperatorValidator : OperatorValidator_Base
    {
        public BandPassFilterConstantTransitionGain_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.BandPassFilterConstantTransitionGain,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal },
                expectedDataKeys: new string[0])
        { }
    }
}
