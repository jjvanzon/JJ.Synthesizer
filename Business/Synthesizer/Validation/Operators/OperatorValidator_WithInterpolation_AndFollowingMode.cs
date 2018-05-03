using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
	internal class OperatorValidator_WithInterpolation_AndFollowingMode : OperatorValidator_Basic
	{
		public OperatorValidator_WithInterpolation_AndFollowingMode(Operator op)
			: base(
				op,
				expectedDataKeys: new[]
				{
					nameof(OperatorWrapper_WithInterpolation.InterpolationType),
					nameof(OperatorWrapper_WithInterpolation_AndFollowingMode.FollowingMode)
				})
		{
			ExecuteValidator(new InterpolationType_DataProperty_Validator(op.Data));
			ExecuteValidator(new FollowingMode_DataProperty_Validator(op.Data));
			ExecuteValidator(new OperatorValidator_CurveAndSampleAreNull(op));
		}
	}
}