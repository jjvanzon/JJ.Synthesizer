using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetOutletCount_WithOperatorType_ExceptCustomOperator : VersatileValidator
    {
        public OperatorValidator_SetOutletCount_WithOperatorType_ExceptCustomOperator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            bool canSetOutletCount = op.CanSetOutletCount();
            if (!canSetOutletCount)
            {
                string message = ResourceFormatter.CannotSetOutletCountForOperatorType(ResourceFormatter.GetDisplayName(op.OperatorType));
                ValidationMessages.Add(() => canSetOutletCount, message);
            }
        }
    }
}