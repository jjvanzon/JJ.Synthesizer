using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_TimeSubstract : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TimeSubstract(Operator obj)
            : base(obj, OperatorTypeEnum.TimeSubstract, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
