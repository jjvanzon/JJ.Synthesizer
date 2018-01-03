
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class AudioOutputValidator : VersatileValidator
	{
		public AudioOutputValidator(AudioOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);

			For(entity.ID, CommonResourceFormatter.ID).GreaterThan(0);
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