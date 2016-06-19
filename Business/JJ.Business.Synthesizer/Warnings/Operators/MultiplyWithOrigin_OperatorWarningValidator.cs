using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class MultiplyWithOrigin_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public MultiplyWithOrigin_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
