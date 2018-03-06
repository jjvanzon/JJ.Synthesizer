using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class SampleWithRate1_OperatorCalculator_NoChannelConversion : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly ICalculatorWithPosition[] _underlyingCalculators;
		private readonly OperatorCalculatorBase _positionCalculator;
		private readonly OperatorCalculatorBase _channelPositionCalculator;
		private readonly double _maxChannelIndexDouble;

		public SampleWithRate1_OperatorCalculator_NoChannelConversion(
			OperatorCalculatorBase positionCalculator,
			OperatorCalculatorBase channelPositionCalculator,
			IList<ICalculatorWithPosition> underlyingCalculators)
			: base(new[] { positionCalculator, channelPositionCalculator })
		{
			_positionCalculator = positionCalculator;
			_channelPositionCalculator = channelPositionCalculator ?? throw new ArgumentNullException(nameof(channelPositionCalculator));

			_underlyingCalculators = underlyingCalculators?.ToArray() ?? throw new NullException(() => underlyingCalculators);

			_maxChannelIndexDouble = _underlyingCalculators.Length - 1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double channelIndexDouble = _channelPositionCalculator.Calculate();

			if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _maxChannelIndexDouble))
			{
				return 0.0;
			}
			int channelIndex = (int)channelIndexDouble;

			double position = _positionCalculator.Calculate();

			double value = _underlyingCalculators[channelIndex].Calculate(position);
			return value;
		}
	}

	internal class SampleWithRate1_OperatorCalculator_MonoToStereo : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _positionCalculator;
		private readonly ICalculatorWithPosition _underlyingCalculator;

		public SampleWithRate1_OperatorCalculator_MonoToStereo(
			OperatorCalculatorBase positionCalculator,
			ICalculatorWithPosition underlyingCalculator)
			: base(new[] { positionCalculator })
		{
			_positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));

			_underlyingCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double position = _positionCalculator.Calculate();

			// Return the single channel for both channels.
			double value = _underlyingCalculator.Calculate(position);

			return value;
		}
	}

	internal class SampleWithRate1_OperatorCalculator_StereoToMono : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _positionCalculator;
		private readonly ICalculatorWithPosition[] _underlyingCalculators;

		public SampleWithRate1_OperatorCalculator_StereoToMono(
			OperatorCalculatorBase positionCalculator,
			IList<ICalculatorWithPosition> underlyingCalculators)
			: base(new[] { positionCalculator })
		{
			if (underlyingCalculators == null) throw new NullException(() => underlyingCalculators);
			if (underlyingCalculators.Count != 2) throw new NotEqualException(() => underlyingCalculators.Count, 2);

			_positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
			_underlyingCalculators = underlyingCalculators.ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double position = _positionCalculator.Calculate();

			double value =
				_underlyingCalculators[0].Calculate(position) +
				_underlyingCalculators[1].Calculate(position);

			return value;
		}
	}
}