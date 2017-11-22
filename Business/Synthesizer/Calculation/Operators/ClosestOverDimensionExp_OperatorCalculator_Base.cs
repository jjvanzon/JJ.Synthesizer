using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class ClosestOverDimensionExp_OperatorCalculator_Base : ClosestOverDimension_OperatorCalculator_Base
	{
		public ClosestOverDimensionExp_OperatorCalculator_Base(
			OperatorCalculatorBase inputCalculator,
			OperatorCalculatorBase collectionCalculator,
			OperatorCalculatorBase fromCalculator,
			OperatorCalculatorBase tillCalculator,
			OperatorCalculatorBase stepCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(
				inputCalculator,
				collectionCalculator,
				fromCalculator,
				tillCalculator,
				stepCalculator,
				positionInputCalculator,
				positionOutputCalculator)
		{ }

		protected override void RecalculateCollection()
		{
			base.RecalculateCollection();

			if (_count != 0)
			{
				for (int i = 0; i < _samples.Length; i++)
				{
					_samples[i] = Math.Log(_samples[i]);
				}

				_min = Math.Log(_min);
				_max = Math.Log(_max);
			}
			else
			{
				_min = 0.0;
				_max = 0.0;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double input = _inputCalculator.Calculate();
			double logInput = Math.Log(input);


			// Fields are log'ed already.
			CollectionHelper.BinarySearchInexact(
				_samples,
				_halfCount,
				_min,
				_max,
				logInput,
				out double logValueBefore,
				out double logValueAfter);

			double logDistanceBefore = Geometry.AbsoluteDistance(logInput, logValueBefore);
			double logDistanceAfter = Geometry.AbsoluteDistance(logInput, logValueAfter);

			if (logDistanceBefore <= logDistanceAfter)
			{
				return Math.Exp(logValueBefore);
			}
			else
			{
				return Math.Exp(logValueAfter);
			}
		}
	}
}