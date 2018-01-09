using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class ToneValidator : VersatileValidator
	{
		public ToneValidator(Tone entity)
		{
			if (entity == null) throw new NullException(() => entity);

			ExecuteValidator(new IDValidator(entity.ID));

			For(entity.Scale, ResourceFormatter.Scale).NotNull();
			For(entity.Number, ResourceFormatter.Number).NotNaN().NotInfinity();
		}
	}
}
