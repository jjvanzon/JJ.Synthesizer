using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Stretch_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Stretch_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
