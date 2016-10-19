using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class And_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public And_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
