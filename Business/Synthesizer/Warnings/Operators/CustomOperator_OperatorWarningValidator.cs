using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class CustomOperator_OperatorWarningValidator : VersatileValidator<Operator>
    {
        public CustomOperator_OperatorWarningValidator(Operator op)
            : base(op)
        { 
            For(() => op.UnderlyingPatch, ResourceFormatter.UnderlyingPatch).NotNull();
        }
    }
}