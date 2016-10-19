using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Reverse_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Reverse_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
