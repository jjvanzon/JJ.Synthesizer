using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_GreaterThan : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_GreaterThan(Operator obj)
            : base(obj, OperatorTypeEnum.GreaterThan, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
