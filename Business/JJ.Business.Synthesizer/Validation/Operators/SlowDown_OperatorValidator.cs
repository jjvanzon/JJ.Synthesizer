using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SlowDown_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public SlowDown_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SlowDown, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
