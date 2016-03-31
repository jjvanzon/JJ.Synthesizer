using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Narrower_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Narrower_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Narrower, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
