using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Basic : VersatileValidator
    {
        public OperatorValidator_Basic(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            ExecuteValidator(new NameValidator(op.Name, required: false));
        }
    }
}