using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ToneValidator : VersatileValidator<Tone>
    {
        public ToneValidator(Tone obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Scale, PropertyDisplayNames.Scale).NotNull();
            For(() => Object.Number, PropertyDisplayNames.Number).NotNaN().NotInfinity();
        }
    }
}
