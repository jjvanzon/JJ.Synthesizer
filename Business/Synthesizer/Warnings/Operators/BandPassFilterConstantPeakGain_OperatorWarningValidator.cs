using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

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
