using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class HighShelfFilter_OperatorWarningValidator
        : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public HighShelfFilter_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
