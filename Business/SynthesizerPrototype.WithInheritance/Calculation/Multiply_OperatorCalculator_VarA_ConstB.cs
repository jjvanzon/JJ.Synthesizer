using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
	internal class Multiply_OperatorCalculator_VarA_ConstB : OperatorCalculatorBase
	{
		private readonly OperatorCalculatorBase _aCalculator;
		private readonly double _b;

		public Multiply_OperatorCalculator_VarA_ConstB(
			OperatorCalculatorBase aCalculator,
			double b)
		{
			_aCalculator = aCalculator ?? throw new NullException(() => aCalculator);
			_b = b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double a = _aCalculator.Calculate();
			double result = OperatorCalculatorHelper.Multiply(a, _b);
			return result;
		}
	}
}
