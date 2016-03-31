using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Equal_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Equal_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Equal, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
