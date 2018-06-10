using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Negative_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _numberCalculator;

		public Negative_OperatorCalculator(OperatorCalculatorBase numberCalculator)
			: base(new[] { numberCalculator })
		    => _numberCalculator = numberCalculator ?? throw new NullException(() => numberCalculator);

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double number = _numberCalculator.Calculate();
			return -number;
		}
	}
}