using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Curves
{
    internal class CurveValidator_WithoutNodes : VersatileValidator<Curve>
    {
        public CurveValidator_WithoutNodes(Curve curve)
            : base(curve)
        {
            ExecuteValidator(new NameValidator(curve.Name, required: false));
        }
    }
}
