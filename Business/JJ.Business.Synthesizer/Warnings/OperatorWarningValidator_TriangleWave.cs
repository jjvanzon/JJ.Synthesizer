using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_TriangleWave : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_TriangleWave(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
