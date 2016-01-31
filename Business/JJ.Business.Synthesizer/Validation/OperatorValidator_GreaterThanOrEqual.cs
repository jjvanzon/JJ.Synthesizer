using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_GreaterThanOrEqual : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_GreaterThanOrEqual(Operator obj)
            : base(obj, OperatorTypeEnum.GreaterThanOrEqual, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
