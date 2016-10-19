using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Divide_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Divide_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
