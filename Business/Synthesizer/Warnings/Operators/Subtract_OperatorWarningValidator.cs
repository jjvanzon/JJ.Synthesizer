using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Subtract_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Subtract_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
