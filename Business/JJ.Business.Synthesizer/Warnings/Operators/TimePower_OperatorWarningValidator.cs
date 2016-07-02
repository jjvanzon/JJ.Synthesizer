using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class TimePower_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public TimePower_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
