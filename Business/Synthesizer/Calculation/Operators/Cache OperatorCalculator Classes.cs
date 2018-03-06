using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Cache_OperatorCalculator_SingleChannel<TArrayCalculator> : OperatorCalculatorBase_WithChildCalculators
		where TArrayCalculator : ICalculatorWithPosition
	{
		private readonly TArrayCalculator _arrayCalculator;
		private readonly OperatorCalculatorBase _positionCalculator;

		public Cache_OperatorCalculator_SingleChannel(
			TArrayCalculator arrayCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(new[] { positionCalculator })
		{
			_arrayCalculator = arrayCalculator;
			_positionCalculator = positionCalculator;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double time = _positionCalculator.Calculate();

			return _arrayCalculator.Calculate(time);
		}
	}

	internal class Cache_OperatorCalculator_MultiChannel<TArrayCalculator> : OperatorCalculatorBase_WithChildCalculators
		where TArrayCalculator : ICalculatorWithPosition
	{
		private readonly TArrayCalculator[] _arrayCalculators;
		private readonly double _arrayCalculatorsMaxIndexDouble;
		private readonly OperatorCalculatorBase _positionCalculator;
		private readonly OperatorCalculatorBase _channelPositionCalculator;

		public Cache_OperatorCalculator_MultiChannel(
			IList<TArrayCalculator> arrayCalculators,
			OperatorCalculatorBase positionCalculator,
			OperatorCalculatorBase channelPositionCalculator)
			: base(new[] { positionCalculator, channelPositionCalculator})
		{
			if (arrayCalculators == null) throw new NullException(() => arrayCalculators);

			_arrayCalculators = arrayCalculators.ToArray();
			_arrayCalculatorsMaxIndexDouble = _arrayCalculators.Length - 1;
			_positionCalculator = positionCalculator;
			_channelPositionCalculator = channelPositionCalculator;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double channelDouble = _channelPositionCalculator.Calculate();

			if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelDouble, _arrayCalculatorsMaxIndexDouble))
			{
				return 0.0;
			}
			int channelInt = (int)channelDouble;

			double position = _positionCalculator.Calculate();

			return _arrayCalculators[channelInt].Calculate(position);
		}
	}
}