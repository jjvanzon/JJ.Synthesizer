using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Pulse : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Pulse(Operator obj)
            : base(obj, OperatorTypeEnum.Pulse, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}