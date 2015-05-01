using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    public class BasicOperatorValidator : FluentValidator<Operator>
    {
        public BasicOperatorValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            For(() => Object.Name, CommonTitles.Name)
                .NotInteger();
        }
    }
}
