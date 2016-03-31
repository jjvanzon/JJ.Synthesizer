using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Square_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Square_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Square, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}