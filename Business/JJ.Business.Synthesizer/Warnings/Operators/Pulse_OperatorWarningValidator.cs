using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Pulse_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Pulse_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
