using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class ChangeTrigger_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public ChangeTrigger_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
