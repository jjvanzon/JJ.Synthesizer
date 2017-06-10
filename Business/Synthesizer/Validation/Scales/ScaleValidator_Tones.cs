using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class ScaleValidator_Tones : VersatileValidator<Scale>
    {
        public ScaleValidator_Tones(Scale obj)
            : base(obj)
        { 
            foreach (Tone tone in obj.Tones)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(tone);
                ExecuteValidator(new ToneValidator(tone), messagePrefix);
            }
        }
    }
}
