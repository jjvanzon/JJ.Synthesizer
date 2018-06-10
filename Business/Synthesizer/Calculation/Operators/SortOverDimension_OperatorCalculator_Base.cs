using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class SortOverDimension_OperatorCalculator_Base
		: OperatorCalculatorBase_SamplerOverDimension
	{
		private int _i;
		protected int _count;
		protected double[] _samples;

		public SortOverDimension_OperatorCalculator_Base(
			OperatorCalculatorBase collectionCalculator,
			OperatorCalculatorBase fromCalculator,
			OperatorCalculatorBase tillCalculator,
			OperatorCalculatorBase stepCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(collectionCalculator, fromCalculator, tillCalculator, stepCalculator, positionInputCalculator, positionOutputCalculator)
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void ProcessFirstSample(double sample)
		{
			double countDouble = _length / _step;

			// 0-3 has length 3 in doubles, but length 4 in integers.
			// But adding 1 works for non-integer double values too.
			countDouble += 1;

			if (CalculationHelper.CanCastToNonNegativeInt32(countDouble))
			{
				_count = (int)countDouble;
			}
			else
			{
				_count = 0;
			}

			_samples = new double[_count];
			_samples[0] = sample;
			_i = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void ProcessNextSample(double sample)
		{
			// Prevent crash:
			// base class works with while loop with floating point imprecision,
			// which does not agree with exact integer amounts.
			if (_i < _count)
			{
				_samples[_i] = sample;
			}

			_i++;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void FinalizeSampling() => Array.Sort(_samples);
	}
}
