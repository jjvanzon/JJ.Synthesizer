using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Squash_OperatorCalculator_ZeroOrigin_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _positionCalculator;
		private readonly OperatorCalculatorBase _factorCalculator;

		public Squash_OperatorCalculator_ZeroOrigin_WithPositionOutput(
			OperatorCalculatorBase positionCalculator,
			OperatorCalculatorBase factorCalculator)
			: base(new[]
			{
				positionCalculator,
				factorCalculator
			})
		{
			_positionCalculator = positionCalculator ?? throw new NullException(() => positionCalculator);
			_factorCalculator = factorCalculator ?? throw new NullException(() => factorCalculator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double position = _positionCalculator.Calculate();
			double factor = _factorCalculator.Calculate();

			// IMPORTANT: To squash things in the output, you have to stretch things in the input.
			double transformedPosition = position * factor;
			return transformedPosition;
		}
	}

	internal class Squash_OperatorCalculator_WithOrigin_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _positionCalculator;
		private readonly OperatorCalculatorBase _factorCalculator;
		private readonly OperatorCalculatorBase _originCalculator;

		public Squash_OperatorCalculator_WithOrigin_WithPositionOutput(
			OperatorCalculatorBase positionCalculator,
			OperatorCalculatorBase factorCalculator,
			OperatorCalculatorBase originCalculator)
			: base(new[]
			{
				positionCalculator,
				factorCalculator,
				originCalculator
			})
		{
			_positionCalculator = positionCalculator ?? throw new NullException(() => positionCalculator);
			_factorCalculator = factorCalculator ?? throw new NullException(() => factorCalculator);
			_originCalculator = originCalculator ?? throw new NullException(() => originCalculator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double position = _positionCalculator.Calculate();
			double factor = _factorCalculator.Calculate();
			double origin = _originCalculator.Calculate();

			// IMPORTANT: To squash things in the output, you have to stretch things in the input.
			double transformedPosition = (position - origin) * factor + origin;

			return transformedPosition;
		}
	}

	// For Time Dimension

	internal class Squash_OperatorCalculator_VarFactor_WithPhaseTracking_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _positionCalculator;
		private readonly OperatorCalculatorBase _factorCalculator;

		private double _phase;
		private double _previousPosition;

		public Squash_OperatorCalculator_VarFactor_WithPhaseTracking_WithPositionOutput(
			OperatorCalculatorBase positionCalculator,
			OperatorCalculatorBase factorCalculator)
			: base(new[] { positionCalculator, factorCalculator })
		{
			_positionCalculator = positionCalculator ?? throw new NullException(() => positionCalculator);
			_factorCalculator = factorCalculator ?? throw new NullException(() => factorCalculator);

			ResetNonRecursive();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double factor = _factorCalculator.Calculate();
			double position = _positionCalculator.Calculate();

			double distance = position - _previousPosition;

			// IMPORTANT: To squash things in the output, you have to stretch things in the input.
			double phase = _phase + distance * factor;

			_phase = phase;
			_previousPosition = _positionCalculator.Calculate();

			return _phase;
		}

		private void ResetNonRecursive()
		{
			// Phase Tracking
			_previousPosition = _positionCalculator.Calculate();
			_phase = 0.0;
		}
	}

	internal class Squash_OperatorCalculator_ConstFactor_WithOriginShifting_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _positionCalculator;
		private readonly double _factor;

		private double _origin;

		public Squash_OperatorCalculator_ConstFactor_WithOriginShifting_WithPositionOutput(
			OperatorCalculatorBase positionCalculator,
			OperatorCalculatorBase factorCalculator)
			: base(new[] { positionCalculator, factorCalculator })
		{
			_positionCalculator = positionCalculator ?? throw new NullException(() => positionCalculator);
			_factor = factorCalculator.Calculate();

			ResetNonRecursive();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double position = _positionCalculator.Calculate();

			// IMPORTANT: To squash things in the output, you have to stretch things in the input.
			double transformedPosition = (position - _origin) * _factor + _origin;

			return transformedPosition;
		}

		public override void Reset()
		{
			base.Reset();

			ResetNonRecursive();
		}

		private void ResetNonRecursive() => _origin = _positionCalculator.Calculate();
	}
}
