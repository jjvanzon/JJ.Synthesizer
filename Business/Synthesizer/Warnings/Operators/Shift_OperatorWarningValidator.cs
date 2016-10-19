using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Shift_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Shift_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
