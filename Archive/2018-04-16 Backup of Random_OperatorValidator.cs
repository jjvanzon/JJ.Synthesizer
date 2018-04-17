//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Validation.DataProperty;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//	internal class Random_OperatorValidator : OperatorValidator_Basic
//	{
//		public Random_OperatorValidator(Operator op)
//			: base(
//				op,
//				expectedDataKeys: new[] { nameof(Random_OperatorWrapper.InterpolationType) })
//		{ 
//			ExecuteValidator(new ResampleInterpolationType_DataProperty_Validator(op.Data));
//			ExecuteValidator(new OperatorValidator_CurveAndSampleAreNull(op));
//		}
//	}
//}