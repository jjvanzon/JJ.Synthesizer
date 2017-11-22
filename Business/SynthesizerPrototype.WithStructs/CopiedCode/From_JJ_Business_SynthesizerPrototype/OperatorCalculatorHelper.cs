using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype
{
	/// <summary>
	/// These operations may seem to trivial to extract into methods,
	/// but it does test our delegation to helpers, which we will use a lot.
	/// </summary>
	internal static class OperatorCalculatorHelper
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Add(double a, double b)
		{
			return a + b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Multiply(double a, double b)
		{
			return a * b;
		}
	}
}
