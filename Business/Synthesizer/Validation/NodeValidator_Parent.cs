using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Validation
{
	internal class NodeValidator_Parent : VersatileValidator
	{
		public NodeValidator_Parent(Node node)
		{
			if (node == null) throw new NullException(() => node);

			For(node.Curve, ResourceFormatter.Curve).NotNull();
		}
	}
}
