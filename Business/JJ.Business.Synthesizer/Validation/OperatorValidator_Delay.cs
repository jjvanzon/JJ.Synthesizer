using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Delay : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Delay(Operator obj)
            : base(obj, OperatorTypeEnum.Delay, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}