using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    // TODO: Organize this differently. There should be a CurveValidator and a CurveValidator_Nodes,
    // that when combined validate both Curve's simple properties and its nodes.
    internal class CurveValidator_WithoutNodes : FluentValidator<Curve>
    {
        public CurveValidator_WithoutNodes(Curve obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Curve curve = Object;

            Execute(new NameValidator(curve.Name, required: false));
        }
    }
}
