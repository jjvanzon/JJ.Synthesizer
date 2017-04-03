using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class LessThanOrEqual_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public LessThanOrEqual_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
