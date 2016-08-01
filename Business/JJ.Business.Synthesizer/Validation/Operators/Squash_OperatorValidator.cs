using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Squash_OperatorValidator : OperatorValidator_Base
    {
        public Squash_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Squash, expectedDataKeys: new string[0], expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
