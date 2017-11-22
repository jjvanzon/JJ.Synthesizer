using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
	internal class Reset_OperatorValidator : OperatorValidator_Basic
	{
		public Reset_OperatorValidator(Operator op)
			: base(op, expectedDataKeys: new[] { nameof(Reset_OperatorWrapper.Position) })
		{
			ExecuteValidator(new Position_DataProperty_Validator(op.Data));
			ExecuteValidator(new OperatorValidator_CurveAndSampleAreNull(op));
		}
	}
}