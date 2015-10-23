using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_SquareWave : OperatorWarningValidator_Base_FirstXInletsNotFilledIn
    {
        public OperatorWarningValidator_SquareWave(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
