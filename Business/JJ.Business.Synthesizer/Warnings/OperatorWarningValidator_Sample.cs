using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Sample : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_Sample(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Data, PropertyDisplayNames.Sample)
                .NotNull();
        }
    }
}
