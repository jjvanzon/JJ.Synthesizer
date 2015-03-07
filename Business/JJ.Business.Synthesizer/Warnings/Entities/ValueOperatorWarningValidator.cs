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
    public class ValueOperatorWarningValidator : OperatorWarningValidatorBase
    {
        public ValueOperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            if (Object.AsValueOperator != null) // For warnings I need null-tollerance.
            {
                if (Object.AsValueOperator.Value == 0)
                {
                    ValidationMessages.Add(() => Object.AsValueOperator.Value, MessagesFormatter.ValueOperatorValueIs0(Object.Name));
                }
            }
        }
    }
}
