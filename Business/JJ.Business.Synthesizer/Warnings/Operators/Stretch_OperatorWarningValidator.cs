using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Stretch_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Stretch_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
