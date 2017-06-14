using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal abstract class OperatorValidator_Base : VersatileValidator<Operator>
    {
        public OperatorValidator_Base([NotNull] Operator op) : base(op)
        {
            ExecuteValidator(new NameValidator(op.Name, required: false));
        }
    }
}
