using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class AudioFileOutputValidator : VersatileValidator
	{
		public AudioFileOutputValidator(AudioFileOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);

			ExecuteValidator(new IDValidator(entity.ID));

			For(entity.Amplifier, ResourceFormatter.Amplifier)
				.NotNaN()
				.NotInfinity();

			For(entity.StartTime, ResourceFormatter.StartTime)
				.NotNaN()
				.NotInfinity();

			For(entity.TimeMultiplier, ResourceFormatter.TimeMultiplier)
				.NotNaN()
				.NotInfinity()
				.IsNot(0);

			For(entity.Duration, ResourceFormatter.Duration)
				.NotNaN()
				.NotInfinity()
				.GreaterThan(0);

			For(entity.SamplingRate, ResourceFormatter.SamplingRate).GreaterThan(0);
			For(entity.AudioFileFormat, ResourceFormatter.AudioFileFormat).NotNull();
			For(entity.SampleDataType, ResourceFormatter.SampleDataType).NotNull();
			For(entity.SpeakerSetup, ResourceFormatter.SpeakerSetup).NotNull();

			TryValidateOutletReference(entity);
		}

		private void TryValidateOutletReference(AudioFileOutput audioFileOutput)
		{
			bool mustValidate = audioFileOutput.Outlet != null &&
								audioFileOutput.Document != null;

			if (!mustValidate)
			{
				return;
			}

			IEnumerable<Outlet> outletsEnumerable = 
				audioFileOutput.Document.Patches
							   .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet))
							   .SelectMany(x => x.Outlets);

			bool referenceIsValid = outletsEnumerable.Any(x => x.ID == audioFileOutput.Outlet.ID);

			if (!referenceIsValid)
			{
				Messages.AddNotInListMessage(ResourceFormatter.Outlet, audioFileOutput.Outlet.ID);
			}
		}
	}
}