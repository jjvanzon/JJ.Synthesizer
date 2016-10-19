using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Curves
{
    internal class CurveValidator_WithoutNodes : FluentValidator<Curve>
    {
        public CurveValidator_WithoutNodes(Curve obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Curve curve = Object;

            ExecuteValidator(new NameValidator(curve.Name, required: false));
        }
    }
}
