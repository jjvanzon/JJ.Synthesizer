using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class NodeValidator_WithoutParent : VersatileValidator
	{
		public NodeValidator_WithoutParent(Node entity)
		{
			if (entity == null) throw new NullException(() => entity);

			ExecuteValidator(new IDValidator(entity.ID));

			For(entity.NodeType, ResourceFormatter.NodeType).NotNull();
			For(entity.X, ResourceFormatter.X).NotNaN().NotInfinity();
			For(entity.Y, ResourceFormatter.Y).NotNaN().NotInfinity();
		}
	}
}
