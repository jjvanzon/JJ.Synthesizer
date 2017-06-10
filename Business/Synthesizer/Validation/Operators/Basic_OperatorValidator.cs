using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Basic_OperatorValidator : VersatileValidator<Operator>
    {
        public Basic_OperatorValidator(Operator op)
            : base(op)
        { 
            ExecuteValidator(new NameValidator(op.Name, required: false));
        }
    }
}