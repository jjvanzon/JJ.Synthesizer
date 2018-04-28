using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
	public class OperatorWrapper_WithInterpolation_AndLookAheadOrLagBehind : OperatorWrapper_WithInterpolation 
	{
		public OperatorWrapper_WithInterpolation_AndLookAheadOrLagBehind(Operator op)
			: base(op)
		{ }

		public LookAheadOrLagBehindEnum LookAheadOrLagBehind
		{
			get => DataPropertyParser.GetEnum<LookAheadOrLagBehindEnum>(WrappedOperator, nameof(LookAheadOrLagBehind));
			set => DataPropertyParser.SetValue(WrappedOperator, nameof(LookAheadOrLagBehind), value);
		}
	}
}
