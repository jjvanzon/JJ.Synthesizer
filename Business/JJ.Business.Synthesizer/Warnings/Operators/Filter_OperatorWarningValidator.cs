using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Filter_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Filter_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
