using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Power_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Power_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
