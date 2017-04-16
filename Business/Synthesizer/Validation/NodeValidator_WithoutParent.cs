using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NodeValidator_WithoutParent : VersatileValidator<Node>
    {
        public NodeValidator_WithoutParent(Node obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.NodeType, ResourceFormatter.NodeType).NotNull();
            For(() => Obj.X, ResourceFormatter.X).NotNaN().NotInfinity();
            For(() => Obj.Y, ResourceFormatter.Y).NotNaN().NotInfinity();
        }
    }
}
