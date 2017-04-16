using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SawDown_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public SawDown_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }
    }
}
