using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NodeValidator_WithoutParent : VersatileValidator
    {
        public NodeValidator_WithoutParent(Node node)
        {
            if (node == null) throw new NullException(() => node);

            For(() => node.NodeType, ResourceFormatter.NodeType).NotNull();
            For(() => node.X, ResourceFormatter.X).NotNaN().NotInfinity();
            For(() => node.Y, ResourceFormatter.Y).NotNaN().NotInfinity();
        }
    }
}
