using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NodeValidator_Parent : VersatileValidator<Node>
    {
        public NodeValidator_Parent(Node node)
            : base(node)
        { 
            For(() => node.Curve, ResourceFormatter.Curve).NotNull();
        }
    }
}
