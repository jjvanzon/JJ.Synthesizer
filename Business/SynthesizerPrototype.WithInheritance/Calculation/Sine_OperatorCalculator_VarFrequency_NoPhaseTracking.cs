using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
	internal class Sine_OperatorCalculator_VarFrequency_NoPhaseTracking : OperatorCalculatorBase
	{
		private readonly OperatorCalculatorBase _frequencyCalculator;
		private readonly DimensionStack _dimensionStack;

		public Sine_OperatorCalculator_VarFrequency_NoPhaseTracking(
			OperatorCalculatorBase frequencyCalculator,
			DimensionStack dimensionStack)
		{
			_frequencyCalculator = frequencyCalculator ?? throw new NullException(() => frequencyCalculator);
			_dimensionStack = dimensionStack ?? throw new NullException(() => dimensionStack);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double position = _dimensionStack.Get();

			double frequency = _frequencyCalculator.Calculate();

			double phase = position * frequency;
			double value = SineCalculator.Sin(phase);

			return value;
		}
	}
}
