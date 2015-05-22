using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_IsCircular : FluentValidator<Operator>
    {
        public OperatorValidator_IsCircular(Operator op)
            : base(op)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            if (op == null) throw new NullException(() => op);

            if (op.IsCircular())
            {
                ValidationMessages.Add(() => op, Messages.OperatorIsCircular);
            }
        }
    }
}
