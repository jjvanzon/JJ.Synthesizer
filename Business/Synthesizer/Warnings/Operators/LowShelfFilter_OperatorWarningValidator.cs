using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class LowShelfFilter_OperatorWarningValidator
        : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public LowShelfFilter_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
