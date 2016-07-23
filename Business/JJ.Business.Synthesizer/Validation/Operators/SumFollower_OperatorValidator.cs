using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SumFollower_OperatorValidator : OperatorValidator_Base
    {
        public SumFollower_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SumFollower, expectedInletCount: 3, expectedOutletCount: 1, expectedDataKeys: new string[0])
        { }
    }
}
