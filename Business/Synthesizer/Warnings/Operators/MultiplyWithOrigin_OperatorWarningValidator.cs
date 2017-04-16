using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MultiplyWithOrigin_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public MultiplyWithOrigin_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
