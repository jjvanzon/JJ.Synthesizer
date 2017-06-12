using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_IsCircular : VersatileValidator<Operator>
    {
        public OperatorValidator_IsCircular(Operator op)
            : base(op)
        {
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
