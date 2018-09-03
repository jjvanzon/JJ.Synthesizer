using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
	internal class AudioFileOutputWarningValidator : VersatileValidator
	{
		public AudioFileOutputWarningValidator(AudioFileOutput obj)
		{ 
			if (obj == null) throw new NullException(() => obj);

			AudioFileOutput audioFileOutput = obj;

			For(obj.Outlet, ResourceFormatter.Outlet).NotNull();
			For(audioFileOutput.Amplifier, ResourceFormatter.Amplifier).IsNot(0.0);
		}
	}
}