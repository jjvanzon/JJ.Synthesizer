using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
	/// <summary>
	/// I still need some kind of polymorphism, because I need to call calculators
	/// abstractly from another calculator.
	/// </summary>
	public interface IOperatorCalculator
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		double Calculate();
	}
}
