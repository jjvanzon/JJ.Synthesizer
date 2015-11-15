using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
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
