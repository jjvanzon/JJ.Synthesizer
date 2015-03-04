using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class ValueOperatorWarningValidator : FluentValidator<Operator>
    {
        public ValueOperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            var valueOperator = new ValueOperator(Object);

            if (valueOperator == 0)
            {
                ValidationMessages.Add("ValueOperatorValueIs0", MessagesFormatter.ValueOperatorValueIs0(valueOperator.Name)); // TODO: Use a better propertyKey. with an expression?
            }
        }
    }
}
