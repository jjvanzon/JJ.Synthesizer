using System.Runtime.CompilerServices;
using System.Threading;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
	/// <summary>
	/// The property of this code file: 'Copy to Output - Copy if newer'
	/// is so that Roslyn runtim compilation can use this code to compile right into the runtime assembly
	/// for inlining.
	/// </summary>
	internal static class PatchCalculatorHelper
	{
		// Source: http://stackoverflow.com/questions/1400465/why-is-there-no-overload-of-interlocked-add-that-accepts-doubles-as-parameters
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void InterlockedAdd(ref float location1, float value)
		{
			float newCurrentValue = 0;
			while (true)
			{
				float currentValue = newCurrentValue;
				float newValue = currentValue + value;
				newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
				// ReSharper disable once CompareOfFloatsByEqualityOperator
				if (newCurrentValue == currentValue)
				{
					return;
				}
			}
		}
	}
}