using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class BandPassFilterConstantTransitionGain_OperatorWarningValidator 
        : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public BandPassFilterConstantTransitionGain_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
