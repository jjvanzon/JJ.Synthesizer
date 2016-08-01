using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AverageFollower_OperatorValidator : OperatorValidator_Base
    {
        public AverageFollower_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.AverageFollower,
                expectedDataKeys: new string[0],
                expectedInletCount: 3,
                expectedOutletCount: 1)
        { }
    }
}