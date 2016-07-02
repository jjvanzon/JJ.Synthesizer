using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Random_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Random_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
