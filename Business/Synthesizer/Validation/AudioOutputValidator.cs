using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioOutputValidator : VersatileValidator
    {
        public AudioOutputValidator(AudioOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);

            For(() => obj.SpeakerSetup, ResourceFormatter.SpeakerSetup).NotNull();
            For(() => obj.SamplingRate, ResourceFormatter.SamplingRate).GreaterThan(0);
            For(() => obj.MaxConcurrentNotes, ResourceFormatter.MaxConcurrentNotes).GreaterThan(0);

            For(() => obj.DesiredBufferDuration, ResourceFormatter.DesiredBufferDuration)
                .NotNaN()
                .NotInfinity()
                .GreaterThan(0);
        }
    }
}