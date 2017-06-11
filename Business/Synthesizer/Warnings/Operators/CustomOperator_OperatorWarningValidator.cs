using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class CustomOperator_OperatorWarningValidator : OperatorWarningValidator_BootStrapped
    {
        public CustomOperator_OperatorWarningValidator(Operator op)
            : base(op)
        { 
            For(() => op.UnderlyingPatch, ResourceFormatter.UnderlyingPatch).NotNull();
        }
    }
}