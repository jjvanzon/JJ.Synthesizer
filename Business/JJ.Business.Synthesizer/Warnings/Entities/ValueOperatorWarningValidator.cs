using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings.Entities
{
    public class ValueOperatorWarningValidator : FluentValidator<Operator>
    {
        public ValueOperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            if (Object.Outlets.Count > 0)
            {
                Outlet resultOutlet = Object.Outlets[0];

                if (resultOutlet != null)
                {
                    double value = resultOutlet.Value;

                    if (value == 0)
                    {
                        ValidationMessages.Add("ValueOperatorValueIs0", MessagesFormatter.ValueOperatorValueIs0(Object.Name)); // TODO: Use a better propertyKey. with an expression?
                    }
                }
            }
        }
    }
}
