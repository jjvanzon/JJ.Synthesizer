using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class NotEqual_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public NotEqual_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.NotEqual, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
