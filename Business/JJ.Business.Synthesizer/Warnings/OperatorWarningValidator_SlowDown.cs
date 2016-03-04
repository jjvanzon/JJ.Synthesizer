using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_SlowDown : OperatorWarningValidator_Base_AllInletsFilled
    {
        public OperatorWarningValidator_SlowDown(Operator obj)
            : base(obj)
        { }
    }
}
