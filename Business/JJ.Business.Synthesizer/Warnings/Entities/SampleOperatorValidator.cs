using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class SampleOperatorValidator : FluentValidator<SampleOperator>
    {
        public SampleOperatorValidator(SampleOperator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            For(() => Object.Sample, PropertyDisplayNames.Sample)
                .NotNull();
        }
    }
}