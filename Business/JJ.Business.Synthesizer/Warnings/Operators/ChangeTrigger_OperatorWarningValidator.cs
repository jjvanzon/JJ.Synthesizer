using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class ChangeTrigger_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public ChangeTrigger_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
