using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Interpolate_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Interpolate_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
