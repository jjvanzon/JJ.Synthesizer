//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//	/// <summary> Not used. </summary>
//	internal class Interpolate_OperatorCalculator_Line_LagBehind_ConstSamplingRate : OperatorCalculatorBase_WithChildCalculators
//	{
//		private readonly OperatorCalculatorBase _signalCalculator;
//		private readonly OperatorCalculatorBase _positionInputCalculator;
//		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;
//		private readonly double _dx;

//		private double _x0;
//		private double _x1;
//		private double _y0;
//		private double _y1;
//		private double _a;

//		public Interpolate_OperatorCalculator_Line_LagBehind_ConstSamplingRate(
//			OperatorCalculatorBase signalCalculator,
//			double samplingRate,
//			OperatorCalculatorBase positionInputCalculator,
//			VariableInput_OperatorCalculator positionOutputCalculator)
//			: base(new[] { signalCalculator, positionInputCalculator, positionOutputCalculator })
//		{
//			_signalCalculator = signalCalculator;
//			_positionInputCalculator = positionInputCalculator;
//			_positionOutputCalculator = positionOutputCalculator;

//			_dx = 1.0 / samplingRate;

//			ResetNonRecursive();
//		}

//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public override double Calculate()
//		{
//			double x = _positionInputCalculator.Calculate();

//			if (x > _x1)
//			{
//				_x0 = _x1;
//				_y0 = _y1;
//				_x1 += _dx;

//				_positionOutputCalculator._value = _x1;

//				_y1 = _signalCalculator.Calculate();

//				double dy = _y1 - _y0;
//				_a = dy / _dx;
//			}
//			else if (x < _x0)
//			{
//				// Going in reverse, take sample in reverse position.
//				_x1 = _x0;
//				_y1 = _y0;
//				_x0 -= _dx;

//				_positionOutputCalculator._value = _x0;

//				_y0 = _signalCalculator.Calculate();

//				double dy = _y1 - _y0;
//				_a = dy / _dx;
//			}

//			double y = _y0 + _a * (x - _x0);

//			return y;
//		}

//		public override void Reset()
//		{
//			base.Reset();

//			ResetNonRecursive();
//		}

//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		private void ResetNonRecursive()
//		{
//			double x = _positionInputCalculator.Calculate();

//			double y = _signalCalculator.Calculate();

//			_x0 = x - _dx;
//			_x1 = x;

//			// Y's are just set at a slightly more practical default than 0.
//			_y0 = y;
//			_y1 = y;

//			_a = 0.0;
//		}
//	}
//}
