using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_TimeSubtract : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TimeSubtract(Operator obj)
            : base(obj, OperatorTypeEnum.TimeSubtract, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
