using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PulseTrigger_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public PulseTrigger_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.PulseTrigger, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
