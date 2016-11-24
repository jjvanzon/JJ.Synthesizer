using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class Basic_ScaleValidator : VersatileValidator<Scale>
    {
        public Basic_ScaleValidator(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object.BaseFrequency.HasValue)
            {
                For(() => Object.BaseFrequency, PropertyDisplayNames.BaseFrequency).NotNaN().NotInfinity().GreaterThan(0);
            }

            For(() => Object.ScaleType, PropertyDisplayNames.ScaleType).NotNull();
        }
    }
}
