using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
	public class Number_OperatorWrapper : OperatorWrapper
	{
		public Number_OperatorWrapper(Operator op)
			: base(op)
		{ }

		public double Number
		{
			get => DataPropertyParser.GetDouble(WrappedOperator, nameof(Number));
			set => DataPropertyParser.SetValue(WrappedOperator, nameof(Number), value);
		}

		public static implicit operator double(Number_OperatorWrapper wrapper) => wrapper?.Number ?? 0.0;
	}
}
