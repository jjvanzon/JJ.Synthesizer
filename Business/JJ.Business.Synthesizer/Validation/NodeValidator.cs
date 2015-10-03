using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    public class NodeValidator : FluentValidator<Node>
    {
        public NodeValidator(Node obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Curve, PropertyDisplayNames.Curve).NotNull();
            For(() => Object.NodeType, PropertyDisplayNames.NodeType).NotNull();

            if (Object.NodeType != null)
            {
                For(() => Object.NodeType.ID, PropertyDisplayNames.NodeType)
                    .IsEnum<NodeTypeEnum>()
                    .IsNot(NodeTypeEnum.Undefined);
            }
        }
    }
}
