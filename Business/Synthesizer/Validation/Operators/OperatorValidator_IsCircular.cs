using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_IsCircular : VersatileValidator
    {
        public OperatorValidator_IsCircular(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            if (op.HasCircularInputOutput())
            {
                Messages.Add(ResourceFormatter.CircularInputOutputReference);
            }

            if (op.HasInvalidCircularUnderlyingPatch())
            {
                Messages.Add(ResourceFormatter.UnderlyingPatchIsCircular);
            }
        }
    }
}
