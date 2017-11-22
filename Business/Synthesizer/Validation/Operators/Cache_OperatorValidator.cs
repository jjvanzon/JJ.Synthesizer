using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
	internal class Cache_OperatorValidator : OperatorValidator_Basic
	{
		public Cache_OperatorValidator(Operator op)
			: base(
				op,
				expectedDataKeys: new[]
				{
					nameof(Cache_OperatorWrapper.InterpolationType),
					nameof(Cache_OperatorWrapper.SpeakerSetup)
				})
		{ 
			ExecuteValidator(new InterpolationType_DataProperty_Validator(op.Data));
			ExecuteValidator(new SpeakerSetup_DataProperty_Validator(op.Data));
			ExecuteValidator(new OperatorValidator_CurveAndSampleAreNull(op));
		}
	}
}