using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NodeValidator_WithoutParent : FluentValidator<Node>
    {
        public NodeValidator_WithoutParent(Node obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.NodeType, PropertyDisplayNames.NodeType).NotNull();

            if (Object.NodeType != null)
            {
                For(() => Object.NodeType.ID, PropertyDisplayNames.NodeType)
                    .IsEnum<NodeTypeEnum>()
                    .IsNot(NodeTypeEnum.Undefined);
            }

            For(() => Object.X, PropertyDisplayNames.X).NotNaN().NotInfinity();
            For(() => Object.Y, PropertyDisplayNames.Y).NotNaN().NotInfinity();
        }
    }
}
