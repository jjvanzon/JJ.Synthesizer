using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NodeValidator_Parent : VersatileValidator<Node>
    {
        public NodeValidator_Parent(Node obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.Curve, ResourceFormatter.Curve).NotNull();
        }
    }
}
