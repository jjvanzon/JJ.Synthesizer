using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Multiply_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Multiply_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Multiply, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
