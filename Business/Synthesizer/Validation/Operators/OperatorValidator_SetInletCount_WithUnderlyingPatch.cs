using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetInletCount_WithUnderlyingPatch : VersatileValidator
    {
        public OperatorValidator_SetInletCount_WithUnderlyingPatch(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            if (!op.Inlets.Where(x => x.IsRepeating).Any())
            {
                ValidationMessages.Add(nameof(Inlet), ResourceFormatter.CannotSetInletCountWithoutRepeatingInlets);
            }
        }
    }
}