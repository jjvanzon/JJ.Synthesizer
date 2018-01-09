using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class EntityPositionValidator : VersatileValidator
	{
		public EntityPositionValidator(EntityPosition entityPosition)
		{
			if (entityPosition == null) throw new ArgumentNullException(nameof(entityPosition));

			ExecuteValidator(new IDValidator(entityPosition.ID));
		}
	}
}
