using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Hold_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Hold_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Hold, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}
