using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class ToneValidator : VersatileValidator
	{
		public ToneValidator(Tone obj)
		{
			if (obj == null) throw new NullException(() => obj);

			For(obj.Scale, ResourceFormatter.Scale).NotNull();
			For(obj.Number, ResourceFormatter.Number).NotNaN().NotInfinity();
		}
	}
}
