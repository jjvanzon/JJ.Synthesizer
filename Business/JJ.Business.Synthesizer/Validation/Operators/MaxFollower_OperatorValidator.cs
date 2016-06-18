using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MaxFollower_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public MaxFollower_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MaxFollower, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
