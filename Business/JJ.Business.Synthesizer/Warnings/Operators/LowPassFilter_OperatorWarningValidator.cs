using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class LowPassFilter_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledIn
    {
        public LowPassFilter_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
