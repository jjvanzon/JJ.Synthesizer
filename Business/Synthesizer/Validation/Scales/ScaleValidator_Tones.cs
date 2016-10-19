using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
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
                ExecuteValidator(new ToneValidator(tone), messagePrefix);
            }
        }
    }
}
