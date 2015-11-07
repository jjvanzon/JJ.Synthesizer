using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_TimeSubtract : OperatorWarningValidator_Base_FirstXInletsNotFilledIn
    {
        public OperatorWarningValidator_TimeSubtract(Operator obj)
            : base(obj)
        { }
    }
}
