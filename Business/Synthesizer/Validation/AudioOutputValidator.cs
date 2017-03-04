using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioOutputValidator : VersatileValidator<AudioOutput>
    {
        public AudioOutputValidator(AudioOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.SpeakerSetup, PropertyDisplayNames.SpeakerSetup).NotNull();
            For(() => Obj.SamplingRate, PropertyDisplayNames.SamplingRate).GreaterThan(0);
            For(() => Obj.MaxConcurrentNotes, PropertyDisplayNames.MaxConcurrentNotes).GreaterThan(0);

            For(() => Obj.DesiredBufferDuration, PropertyDisplayNames.DesiredBufferDuration)
                .NotNaN()
                .NotInfinity()
                .GreaterThan(0);
        }
    }
}