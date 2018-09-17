using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class NodeValidator_WithoutParent : VersatileValidator
	{
		public NodeValidator_WithoutParent(Node entity)
		{
			if (entity == null) throw new NullException(() => entity);

			ExecuteValidator(new IDValidator(entity.ID));

			For(entity.X, ResourceFormatter.X).NotNaN().NotInfinity();
			For(entity.Y, ResourceFormatter.Y).NotNaN().NotInfinity();
		}
	}
}
