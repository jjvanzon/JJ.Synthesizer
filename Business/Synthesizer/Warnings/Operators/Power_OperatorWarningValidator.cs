using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Power_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public Power_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
