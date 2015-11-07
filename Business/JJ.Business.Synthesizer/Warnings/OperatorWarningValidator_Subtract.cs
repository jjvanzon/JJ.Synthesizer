using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Subtract : OperatorWarningValidator_Base_FirstXInletsNotFilledIn
    {
        public OperatorWarningValidator_Subtract(Operator obj)
            : base(obj)
        { }
    }
}
