using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
	internal class Curve_OperatorValidator : OperatorValidator_Basic
	{
		public Curve_OperatorValidator(Operator op)
			: base(op)
		{
			For(op.Curve, ResourceFormatter.Curve).NotNull();
			For(op.Sample, ResourceFormatter.Sample).IsNull();
		}
	}
}