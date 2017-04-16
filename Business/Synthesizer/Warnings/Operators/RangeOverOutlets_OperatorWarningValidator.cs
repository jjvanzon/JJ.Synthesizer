using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class RangeOverOutlets_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public RangeOverOutlets_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}