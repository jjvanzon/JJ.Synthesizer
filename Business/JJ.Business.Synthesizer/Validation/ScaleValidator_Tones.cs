using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ScaleValidator_Tones : FluentValidator<Scale>
    {
        public ScaleValidator_Tones(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            foreach (Tone tone in Object.Tones)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(tone);
                Execute(new ToneValidator(tone), messagePrefix);
            }
        }
    }
}
