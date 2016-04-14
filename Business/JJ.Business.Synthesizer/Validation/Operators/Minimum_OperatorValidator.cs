using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Minimum_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public Minimum_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Minimum, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
