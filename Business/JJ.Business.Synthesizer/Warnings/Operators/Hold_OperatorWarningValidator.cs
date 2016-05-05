using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Hold_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilled
    {
        public Hold_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
