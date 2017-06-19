using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_IsCircular : VersatileValidator
    {
        public OperatorValidator_IsCircular(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            if (op.HasCircularInputOutput())
            {
                ValidationMessages.Add(() => op, ResourceFormatter.CircularInputOutputReference);
            }

            if (op.HasCircularUnderlyingPatch())
            {
                ValidationMessages.Add(() => op, ResourceFormatter.UnderlyingPatchIsCircular);
            }
        }
    }
}
