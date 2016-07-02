using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SpeedUp_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public SpeedUp_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
