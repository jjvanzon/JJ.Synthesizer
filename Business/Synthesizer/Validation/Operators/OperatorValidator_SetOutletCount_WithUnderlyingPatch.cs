using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetOutletCount_WithUnderlyingPatch : VersatileValidator
    {
        public OperatorValidator_SetOutletCount_WithUnderlyingPatch(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            
            if (!op.Outlets.Where(x => x.IsRepeating).Any())
            {
                ValidationMessages.Add(nameof(Outlet), ResourceFormatter.CannotSetOutletCountWithoutRepeatingOutlets);
            }
        }
    }
}