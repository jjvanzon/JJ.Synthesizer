//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.CopiedCode.FromFramework;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//	/// <summary>
//	/// A weakness though is, that the sampling rate is remembered until the next sample,
//	/// which may work poorly when a very low sampling rate is provided.
//	/// </summary>
//	internal class Interpolate_OperatorCalculator_Cubic_LagBehind_SamplingRateOne : OperatorCalculatorBase_WithChildCalculators
//	{
//		private readonly OperatorCalculatorBase _signalCalculator;
//		private readonly OperatorCalculatorBase _positionInputCalculator;
//		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

//		private double _x0;
//		private double _yMinus1;
//		private double _y0;
//		private double _y1;
//		private double _y2;
//		private double _a;
//		private double _b;
//		private double _c;

//		public Interpolate_OperatorCalculator_Cubic_LagBehind_SamplingRateOne(
//			OperatorCalculatorBase signalCalculator,
//			OperatorCalculatorBase positionInputCalculator,
//			VariableInput_OperatorCalculator positionOutputCalculator)
//			: base(new[] { signalCalculator, positionInputCalculator, positionOutputCalculator })
//		{
//			_signalCalculator = signalCalculator;
//			_positionInputCalculator = positionInputCalculator;
//			_positionOutputCalculator = positionOutputCalculator;

//			ResetNonRecursive();
//		}

//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public override double Calculate()
//		{
//			double x = _positionInputCalculator.Calculate();

//			// TODO: What if position goes in reverse?
//			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
//			// When x goes past _x1 you must shift things.
//			double x1 = _x0 + 1; // Addition probably faster than field load
//			if (x > x1)
//			{
//				// Shift the samples to the left.
//				_yMinus1 = _y0;
//				_y0 = _y1;
//				_y1 = _y2;

//				_x0++;
//				double x2 = x1 + 1; // Addition probably faster than field load

//				// Determine next sample
//				_positionOutputCalculator._value = x2;
//				_y2 = _signalCalculator.Calculate();

//				// Precalculate variables
//				(_a, _b, _c) = Interpolator.Cubic_SmoothSlope_DistanceOne_PrecalculateVariables(_yMinus1, _y0, _y1, _y2);
//			}

//			double y = Interpolator.Cubic_SmoothSlope_FromPrecalculatedVariables(_x0, _y0, _a, _b, _c, x);
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

//			double x0 = x - 1;

//			(_a, _b, _c) = Interpolator.Cubic_SmoothSlope_DistanceOne_PrecalculateVariables(y, y, y, y);

//			_yMinus1 = _y0 = _y1 = _y2 = y;
//			_x0 = x0;
//		}
//	}
//}
