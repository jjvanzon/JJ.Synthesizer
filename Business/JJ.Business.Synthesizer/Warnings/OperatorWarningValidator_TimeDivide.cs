using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_TimeDivide : OperatorWarningValidator_Base_FirstXInletsNotFilledIn
    {
        public OperatorWarningValidator_TimeDivide(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
