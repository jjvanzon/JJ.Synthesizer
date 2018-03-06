using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
	internal class AudioOutputWarningValidator : VersatileValidator
	{
		public AudioOutputWarningValidator(AudioOutput obj)
		{
			if (obj == null) throw new NullException(() => obj);

			For(obj.DesiredBufferDuration, ResourceFormatter.DesiredBufferDuration).LessThan(5);
		}
	}
}