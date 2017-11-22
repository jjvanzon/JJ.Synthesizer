using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
	internal class Number_OperatorCalculator_NaN : OperatorCalculatorBase
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			return double.NaN;
		}
	}
}
