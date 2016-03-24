using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Maximum : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Maximum(Operator obj)
            : base(obj, OperatorTypeEnum.Maximum, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
