using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Hold_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _signalCalculator;
		private double _value;

		public Hold_OperatorCalculator(OperatorCalculatorBase signalCalculator)
			: base(new[] { signalCalculator })
		{
			_signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);

			ResetPrivate();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate() => _value;

		public override void Reset() => ResetPrivate();

	    private void ResetPrivate() => _value = _signalCalculator.Calculate();
	}
}