using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LessThanOrEqual_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LessThanOrEqual_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.LessThanOrEqual, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
