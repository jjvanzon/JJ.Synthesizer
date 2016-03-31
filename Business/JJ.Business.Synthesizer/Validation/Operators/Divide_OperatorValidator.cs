using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Divide_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Divide_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Divide, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
