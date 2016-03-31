using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Multiply_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Multiply_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
