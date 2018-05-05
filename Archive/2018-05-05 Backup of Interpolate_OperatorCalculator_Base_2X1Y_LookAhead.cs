//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//	internal abstract class Interpolate_OperatorCalculator_Base_2X1Y_LookAhead : Interpolate_OperatorCalculator_Base_2X1Y
//	{
//		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

//		public Interpolate_OperatorCalculator_Base_2X1Y_LookAhead(
//			OperatorCalculatorBase signalCalculator,
//			OperatorCalculatorBase samplingRateCalculator,
//			OperatorCalculatorBase positionInputCalculator,
//			VariableInput_OperatorCalculator positionOutputCalculator)
//			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
//			=> _positionOutputCalculator = positionOutputCalculator ?? throw new ArgumentNullException(nameof(positionOutputCalculator));

//		protected sealed override void SetNextSample()
//		{
//			_x1 += Dx();
//			SetY0();
//		}

//		protected sealed override void SetPreviousSample()
//		{
//			_x0 -= Dx();
//			SetY0();
//		}

//		private void SetY0()
//		{
//			double originalValue = _positionOutputCalculator._value;

//			// TODO: This does not work for Stripe_LookAhead, because in stripe x0 is really xMinusHalf,
//			// but here we need the actual x0 for Stripe_LookAhead.

//			_positionOutputCalculator._value = _x0;

//			_y0 = _signalCalculator.Calculate();

//			_positionOutputCalculator._value = originalValue;
//		}
//	}
//}
