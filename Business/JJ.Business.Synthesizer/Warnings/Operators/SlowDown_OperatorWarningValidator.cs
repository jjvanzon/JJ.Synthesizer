using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SlowDown_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledIn
    {
        public SlowDown_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
