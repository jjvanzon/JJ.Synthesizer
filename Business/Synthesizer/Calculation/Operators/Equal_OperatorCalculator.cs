using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Equal_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _aCalculator;
		private readonly OperatorCalculatorBase _bCalculator;
		
		public Equal_OperatorCalculator(
			OperatorCalculatorBase aCalculator,
			OperatorCalculatorBase bCalculator)
			: base(new[] { aCalculator, bCalculator })
		{
			_aCalculator = aCalculator ?? throw new NullException(() => aCalculator);
			_bCalculator = bCalculator ?? throw new NullException(() => bCalculator);
		}
		 
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double a = _aCalculator.Calculate();
			double b = _bCalculator.Calculate();

			return a == b ? 1.0 : 0.0;
		}
	}
}