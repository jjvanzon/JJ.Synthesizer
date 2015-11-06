using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_SlowDown : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_SlowDown(Operator obj)
            : base(obj, OperatorTypeEnum.SlowDown, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
