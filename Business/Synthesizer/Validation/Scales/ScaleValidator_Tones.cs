using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class ScaleValidator_Tones : VersatileValidator<Scale>
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
