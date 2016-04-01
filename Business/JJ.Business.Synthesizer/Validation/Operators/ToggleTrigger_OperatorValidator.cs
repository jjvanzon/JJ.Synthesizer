using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ToggleTrigger_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public ToggleTrigger_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.ToggleTrigger, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
