using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LessThan_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LessThan_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.LessThan, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
