using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class PeakingEQFilter_OperatorWarningValidator
        : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public PeakingEQFilter_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
