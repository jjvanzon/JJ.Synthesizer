using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_TimePower : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TimePower(Operator obj)
            : base(obj, OperatorTypeEnum.TimePower, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
