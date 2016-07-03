using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LowPassFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LowPassFilter_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.LowPassFilter, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
