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
    public class SamplerOperatorValidator : FluentValidator<CurveIn>
    {
        public SamplerOperatorValidator(CurveIn obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            For(() => Object.Operator, PropertyDisplayNames.Operator)
                .NotNull();
        }
    }
}
