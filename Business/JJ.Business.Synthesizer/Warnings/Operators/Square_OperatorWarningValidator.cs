using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Square_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Square_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
