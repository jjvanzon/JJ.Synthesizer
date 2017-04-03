using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
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
            For(() => Obj.SpeakerSetup, ResourceFormatter.SpeakerSetup).NotNull();
            For(() => Obj.SamplingRate, ResourceFormatter.SamplingRate).GreaterThan(0);
            For(() => Obj.MaxConcurrentNotes, ResourceFormatter.MaxConcurrentNotes).GreaterThan(0);

            For(() => Obj.DesiredBufferDuration, ResourceFormatter.DesiredBufferDuration)
                .NotNaN()
                .NotInfinity()
                .GreaterThan(0);
        }
    }
}