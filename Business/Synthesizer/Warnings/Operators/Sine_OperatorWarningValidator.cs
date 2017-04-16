using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Sine_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Sine_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
