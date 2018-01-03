using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
	internal class Basic_ScaleValidator : VersatileValidator
	{
		public Basic_ScaleValidator(Scale entity)
		{
			if (entity == null) throw new NullException(() => entity);

			For(entity.ID, CommonResourceFormatter.ID).GreaterThan(0);

			if (entity.BaseFrequency.HasValue)
			{
				For(entity.BaseFrequency, ResourceFormatter.BaseFrequency)
					.NotNaN()
					.NotInfinity()
					.GreaterThan(0);
			}

			For(entity.ScaleType, ResourceFormatter.ScaleType).NotNull();
		}
	}
}
