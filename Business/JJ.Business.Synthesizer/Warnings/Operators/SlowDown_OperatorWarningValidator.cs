using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SlowDown_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilled
    {
        public SlowDown_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
