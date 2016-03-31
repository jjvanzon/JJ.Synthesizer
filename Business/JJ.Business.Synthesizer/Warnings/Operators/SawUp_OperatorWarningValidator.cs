using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SawUp_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public SawUp_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
