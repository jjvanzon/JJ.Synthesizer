using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_SpeedUp : OperatorWarningValidator_Base_FirstXInletsNotFilledIn
    {
        public OperatorWarningValidator_SpeedUp(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
