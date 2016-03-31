using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Basic_OperatorValidator : FluentValidator<Operator>
    {
        public Basic_OperatorValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Execute(new NameValidator(Object.Name, required: false));

            For(() => Object.OperatorType, PropertyDisplayNames.OperatorType).NotNull();
            For(() => Object.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).IsEnum<OperatorTypeEnum>();
        }
    }
}