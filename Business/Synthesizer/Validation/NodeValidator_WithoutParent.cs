using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NodeValidator_WithoutParent : VersatileValidator<Node>
    {
        public NodeValidator_WithoutParent(Node node)
            : base(node)
        { 
            For(() => node.NodeType, ResourceFormatter.NodeType).NotNull();
            For(() => node.X, ResourceFormatter.X).NotNaN().NotInfinity();
            For(() => node.Y, ResourceFormatter.Y).NotNaN().NotInfinity();
        }
    }
}
