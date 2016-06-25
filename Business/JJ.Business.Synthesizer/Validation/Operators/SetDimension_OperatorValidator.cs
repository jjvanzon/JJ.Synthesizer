using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SetDimension_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public SetDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SetDimension, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
