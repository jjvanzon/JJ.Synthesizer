using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class PulseTrigger_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public PulseTrigger_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
