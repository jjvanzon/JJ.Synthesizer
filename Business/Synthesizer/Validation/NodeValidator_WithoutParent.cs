using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NodeValidator_WithoutParent : VersatileValidator<Node>
    {
        public NodeValidator_WithoutParent(Node obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.NodeType, PropertyDisplayNames.NodeType).NotNull();
            For(() => Obj.X, PropertyDisplayNames.X).NotNaN().NotInfinity();
            For(() => Obj.Y, PropertyDisplayNames.Y).NotNaN().NotInfinity();
        }
    }
}
