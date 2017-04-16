using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Basic_OperatorValidator : VersatileValidator<Operator>
    {
        public Basic_OperatorValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            ExecuteValidator(new NameValidator(Obj.Name, required: false));

            For(() => Obj.OperatorType, ResourceFormatter.OperatorType).NotNull();
        }
    }
}