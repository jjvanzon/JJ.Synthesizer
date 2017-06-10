using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class AudioOutputWarningValidator : VersatileValidator<AudioOutput>
    {
        public AudioOutputWarningValidator(AudioOutput obj)
            : base(obj)
        { 
            For(() => obj.DesiredBufferDuration, ResourceFormatter.DesiredBufferDuration).LessThan(5);
        }
    }
}