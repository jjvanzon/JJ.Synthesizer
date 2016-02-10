using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Random_OperatorCalculator_OtherInterpolations : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _resampleOperator;

        public Random_OperatorCalculator_OtherInterpolations(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase valueDurationCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { valueDurationCalculator, phaseShiftCalculator })
        {
            // Hack in a piece of patch, to reuse the Resample_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_VarFrequency_VarPhaseShift_OperatorCalculator(
                randomCalculator, randomCalculatorOffset, valueDurationCalculator, phaseShiftCalculator);

            // Create a second Random operator calculator.
            // But now with a higher rate than the resample rate,
            // to prevent alignment issues.
            //var multiplyCalculator = new Multiply_VarOperandA_ConstOperandB_ConstOrigin_OperatorCalculator(
            //    valueDurationCalculator, 1.0 / 172.5162467243, 0.0);
            //var randomCalculator2 = new Random_VarFrequency_VarPhaseShift_OperatorCalculator(
            //    randomCalculator, randomCalculatorOffset, multiplyCalculator, phaseShiftCalculator);

            // Create a divide calculator, to convert a value duration to a sampling rate.
            var divideCalculator = new Divide_WithoutOrigin_WithConstNumerator_OperatorCalculator(
                1.0, valueDurationCalculator);

            // Lead their outputs to a Resample operator calculator
            _resampleOperator = new Resample_OperatorCalculator_CubicSmoothInclination(
                signalCalculator: randomCalculator2,
                samplingRateCalculator: divideCalculator);
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _resampleOperator.Calculate(time, channelIndex);
        }
    }
}