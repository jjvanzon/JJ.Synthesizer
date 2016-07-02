using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class NotchFilter_OperatorWarningValidator
        : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public NotchFilter_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
