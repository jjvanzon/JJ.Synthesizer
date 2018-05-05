//using System;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//	internal abstract class Interpolate_OperatorCalculator_Base_LookAhead : Interpolate_OperatorCalculator_Base
//	{
//		protected readonly VariableInput_OperatorCalculator _positionOutputCalculator;

//		public Interpolate_OperatorCalculator_Base_LookAhead(
//			OperatorCalculatorBase signalCalculator,
//			OperatorCalculatorBase samplingRateCalculator,
//			OperatorCalculatorBase positionInputCalculator,
//			VariableInput_OperatorCalculator positionOutputCalculator)
//			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
//		{
//			_positionOutputCalculator = positionOutputCalculator ?? throw new ArgumentNullException(nameof(positionOutputCalculator));
//		}
//	}
//}