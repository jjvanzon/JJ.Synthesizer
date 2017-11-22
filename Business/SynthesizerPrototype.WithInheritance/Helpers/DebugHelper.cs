using JJ.Business.SynthesizerPrototype.WithInheritance.Calculation;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Helpers
{
	internal static class DebugHelper
	{
		public static string GetDebuggerDisplay(OperatorCalculatorBase operatorCalculator)
		{
			if (operatorCalculator == null) throw new NullException(() => operatorCalculator);

			return operatorCalculator.GetType().Name;
		}
	}
}
