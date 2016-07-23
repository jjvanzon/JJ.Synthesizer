using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MinFollower_OperatorValidator : OperatorValidator_Base
    {
        public MinFollower_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.MinFollower,
                  expectedInletCount: 3,
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[0])
        { }
    }
}
