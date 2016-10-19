using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class BandPassFilterConstantPeakGain_OperatorWarningValidator
        : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public BandPassFilterConstantPeakGain_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
