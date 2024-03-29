﻿//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.CopiedCode.FromFramework;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//	/// <summary>
//	/// A weakness though is, that the sampling rate is remembered until the next sample,
//	/// which may work poorly when a very low sampling rate is provided.
//	/// </summary>
//	internal class Interpolate_OperatorCalculator_Cubic_LagBehind_ConstSamplingRate : OperatorCalculatorBase_WithChildCalculators
//	{
//		private readonly OperatorCalculatorBase _signalCalculator;
//		private readonly double _dx;
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

//		public Interpolate_OperatorCalculator_Cubic_LagBehind_ConstSamplingRate(
//			OperatorCalculatorBase signalCalculator,
//			double samplingRate,
//			OperatorCalculatorBase positionInputCalculator,
//			VariableInput_OperatorCalculator positionOutputCalculator)
//			: base(new[] { signalCalculator, positionInputCalculator, positionOutputCalculator })
//		{
//			_signalCalculator = signalCalculator;
//			_dx = 1.0 / samplingRate;
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
//			double x1 = _x0 + _dx; // Addition probably faster than field load
//			if (x > x1)
//			{
//				// Shift the samples to the left.
//				_x0 += _dx;
//				_yMinus1 = _y0;
//				_y0 = _y1;
//				_y1 = _y2;
//				double x2 = x1 + _dx; // Addition probably faster than field load

//				_positionOutputCalculator._value = x2;

//				_y2 = _signalCalculator.Calculate();

//				(_a, _b, _c) = Interpolator.Interpolate_Cubic_SmoothSlope_Equidistant_PrecalculateVariables(_dx, _yMinus1, _y0, _y1, _y2);
//			}

//			double t = (x - _x0) / _dx;

//			double y = Interpolator.Interpolate_Cubic_SmoothSlope_Equidistant_FromPrecalculatedVariables(_y0, _a, _b, _c, t);
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

//			_x0 = x - _dx;

//			double y = _signalCalculator.Calculate();

//			_yMinus1 = _y0 = _y1 = _y2 = y;

//			(_a, _b, _c) = Interpolator.Interpolate_Cubic_SmoothSlope_Equidistant_PrecalculateVariables(_dx, y, y, y, y);
//		}
//	}
//}
