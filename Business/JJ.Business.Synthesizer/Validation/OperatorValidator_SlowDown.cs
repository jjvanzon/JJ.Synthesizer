using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_SlowDown : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_SlowDown(Operator obj)
            : base(obj, OperatorTypeEnum.SlowDown, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
