using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class ScaleWarningValidator : FluentValidator<Scale>
    {
        public ScaleWarningValidator(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.BaseFrequency, PropertyDisplayNames.BaseFrequency).IsNull();
            For(() => Object.Tones.Count, PropertyDisplayNames.ToneCount).GreaterThan(0);
        }
    }
}