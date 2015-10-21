using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NodeValidator_Parent : FluentValidator<Node>
    {
        public NodeValidator_Parent(Node obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Curve, PropertyDisplayNames.Curve).NotNull();
        }
    }
}
