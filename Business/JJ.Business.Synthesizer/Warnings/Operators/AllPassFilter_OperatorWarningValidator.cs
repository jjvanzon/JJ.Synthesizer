using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class AllPassFilter_OperatorWarningValidator
        : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public AllPassFilter_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
