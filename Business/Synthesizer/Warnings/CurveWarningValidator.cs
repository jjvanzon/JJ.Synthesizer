using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class CurveWarningValidator : VersatileValidator<Curve>
    {
        public CurveWarningValidator(Curve obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            // Placeholder. No validations yet.
        }
    }
}
