using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Squash_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public Squash_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Squash, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
