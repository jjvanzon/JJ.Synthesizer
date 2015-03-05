using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class NodeValidator : FluentValidator<Node>
    {
        public NodeValidator(Node obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            For(() => Object.Curve, PropertyDisplayNames.Curve)
                .NotNull();

            For(() => Object.NodeType, PropertyDisplayNames.NodeType)
                .NotNull();

            if (Object.NodeType != null)
            {
                For(() => Object.NodeType.ID, PropertyDisplayNames.NodeType)
                    .IsEnumValue<NodeTypeEnum>()
                    .IsNot(NodeTypeEnum.Undefined);
            }
        }
    }
}
