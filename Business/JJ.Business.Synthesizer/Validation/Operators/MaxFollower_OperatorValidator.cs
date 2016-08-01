using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MaxFollower_OperatorValidator : OperatorValidator_Base
    {
        public MaxFollower_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.MaxFollower,
                expectedDataKeys: new string[0],
                expectedInletCount: 3,
                expectedOutletCount: 1)
        { }
    }
}