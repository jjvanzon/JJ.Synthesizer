using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_LessThanOrEqual : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_LessThanOrEqual(Operator obj)
            : base(obj, OperatorTypeEnum.LessThanOrEqual, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
