using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AverageFollower_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public AverageFollower_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.AverageFollower, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
