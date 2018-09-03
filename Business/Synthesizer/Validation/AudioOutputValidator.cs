using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class AudioOutputValidator : VersatileValidator
	{
		public AudioOutputValidator(AudioOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);

			ExecuteValidator(new IDValidator(entity.ID));

			For(entity.SpeakerSetup, ResourceFormatter.SpeakerSetup).NotNull();
			For(entity.SamplingRate, ResourceFormatter.SamplingRate).GreaterThan(0);
			For(entity.MaxConcurrentNotes, ResourceFormatter.MaxConcurrentNotes).GreaterThan(0);

			For(entity.DesiredBufferDuration, ResourceFormatter.DesiredBufferDuration)
				.NotNaN()
				.NotInfinity()
				.GreaterThan(0);
		}
	}
}