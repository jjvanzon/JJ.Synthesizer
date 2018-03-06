using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
	internal class ScaleValidator_Tones : VersatileValidator
	{
		public ScaleValidator_Tones(Scale obj)
		{
			if (obj == null) throw new NullException(() => obj);

			foreach (Tone tone in obj.Tones)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix(tone);
				ExecuteValidator(new ToneValidator(tone), messagePrefix);
			}
		}
	}
}
