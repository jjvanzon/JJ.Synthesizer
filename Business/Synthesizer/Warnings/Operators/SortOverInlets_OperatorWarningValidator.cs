using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SortOverInlets_OperatorWarningValidator : OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults
    {
        public SortOverInlets_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }
    }
}
