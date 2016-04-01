using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ChangeTrigger_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public ChangeTrigger_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.ChangeTrigger, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
