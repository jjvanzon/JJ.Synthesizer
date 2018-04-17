//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//	public class InletsToDimension_OperatorWrapper : OperatorWrapper
//	{
//		public InletsToDimension_OperatorWrapper(Operator op)
//			: base(op)
//		{ }

//		public ResampleInterpolationTypeEnum InterpolationType
//		{
//			get => DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, nameof(InterpolationType));
//			set => DataPropertyParser.SetValue(WrappedOperator, nameof(InterpolationType), value);
//		}
//	}
//}