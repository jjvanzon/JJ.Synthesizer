using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OneOverX_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public OneOverX_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.OneOverX, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}
