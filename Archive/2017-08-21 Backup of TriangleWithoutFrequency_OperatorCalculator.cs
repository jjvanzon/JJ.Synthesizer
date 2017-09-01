//using System;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class TriangleWithRate1_OperatorCalculator : OperatorCalculatorBase
//    {
//        private readonly DimensionStack _dimensionStack;

//        public TriangleWithRate1_OperatorCalculator(DimensionStack dimensionStack)
//        {
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();
//            double phase = position;

//            return TriangleCalculationHelper.CalculateTriangle(phase);
//        }
//    }

//    internal class TriangleWithRate1_OperatorCalculator_WithPositionInput : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;

//        public TriangleWithRate1_OperatorCalculator_WithPositionInput(OperatorCalculatorBase positionCalculator)
//        {
//            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();
//            double phase = position;

//            return TriangleCalculationHelper.CalculateTriangle(phase);
//        }
//    }

//    internal class TriangleCalculationHelper
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static double CalculateTriangle(double phase)
//        {
//            // Correct the phase shift, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
//            double shiftedPhase = phase + 0.25;
//            double relativePhase = shiftedPhase % 1.0;
//            if (relativePhase < 0.5)
//            {
//                // Starts going up at a rate of 2 up over 1/2 a cycle.
//                double value = -1.0 + 4.0 * relativePhase;
//                return value;
//            }
//            else
//            {
//                // And then going down at phase 1/2.
//                // (Extending the line to x = 0 it ends up at y = 3.)
//                double value = 3.0 - 4.0 * relativePhase;
//                return value;
//            }
//        }
//    }
//}
