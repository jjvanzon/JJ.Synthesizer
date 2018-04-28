using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
	public class OperatorWrapper_WithInterpolation_AndFollowingMode : OperatorWrapper_WithInterpolation 
	{
		public OperatorWrapper_WithInterpolation_AndFollowingMode(Operator op)
			: base(op)
		{ }

		public FollowingModeEnum FollowingMode
		{
			get => DataPropertyParser.GetEnum<FollowingModeEnum>(WrappedOperator, nameof(FollowingMode));
			set => DataPropertyParser.SetValue(WrappedOperator, nameof(FollowingMode), value);
		}
	}
}
