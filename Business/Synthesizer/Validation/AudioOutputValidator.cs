using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioOutputValidator : VersatileValidator<AudioOutput>
    {
        public AudioOutputValidator(AudioOutput obj)
            : base(obj)
        { 
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