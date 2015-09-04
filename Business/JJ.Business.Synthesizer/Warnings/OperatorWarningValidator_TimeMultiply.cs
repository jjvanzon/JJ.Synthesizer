using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_TimeMultiply : OperatorWarningValidator_Base_FirstXInletsNotFilledIn
    {
        public OperatorWarningValidator_TimeMultiply(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
