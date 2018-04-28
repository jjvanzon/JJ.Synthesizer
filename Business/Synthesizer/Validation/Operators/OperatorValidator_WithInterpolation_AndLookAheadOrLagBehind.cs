using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
	internal class OperatorValidator_WithInterpolation_AndLookAheadOrLagBehind : OperatorValidator_Basic
	{
		public OperatorValidator_WithInterpolation_AndLookAheadOrLagBehind(Operator op)
			: base(
				op,
				expectedDataKeys: new[]
				{
					nameof(OperatorWrapper_WithInterpolation_AndLookAheadOrLagBehind.InterpolationType)
					//nameof(OperatorWrapper_WithInterpolation_AndLookAheadOrLagBehind.LookAheadOrLagBehind)
				})
		{
			ExecuteValidator(new InterpolationType_DataProperty_Validator(op.Data));
			ExecuteValidator(new LookAheadOrLagBehind_DataProperty_Validator(op.Data));
			ExecuteValidator(new OperatorValidator_CurveAndSampleAreNull(op));
		}
	}
}