using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class CurveValidator_WithoutNodes : VersatileValidator
	{
		public CurveValidator_WithoutNodes(Curve entity)
		{
			if (entity == null) throw new NullException(() => entity);

			ExecuteValidator(new IDValidator(entity.ID));
			ExecuteValidator(new NameValidator(entity.Name, required: false));
		}
	}
}
