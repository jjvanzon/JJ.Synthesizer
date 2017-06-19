using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class CustomOperator_OperatorWarningValidator : VersatileValidator
    {
        public CustomOperator_OperatorWarningValidator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            For(() => op.UnderlyingPatch, ResourceFormatter.UnderlyingPatch).NotNull();
        }
    }
}