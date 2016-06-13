// MIND THE HACKS IN THIS FILE! IT MAY BE THE CAUSE OF YOUR PROBLEMS!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Random_OperatorCalculator_OtherInterpolationTypes : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _resampleOperator;

        public Random_OperatorCalculator_OtherInterpolationTypes(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            // HACK in a piece of patch, to reuse the Resample_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                randomCalculator, randomCalculatorOffset, rateCalculator, phaseShiftCalculator, dimensionStack);

            // Lead their outputs to a Resample operator calculator
            _resampleOperator = OperatorCalculatorFactory.CreateResample_OperatorCalculator(
                resampleInterpolationTypeEnum,
                signalCalculator: randomCalculator2,
                samplingRateCalculator: rateCalculator,
                dimensionStack: dimensionStack);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _resampleOperator.Calculate();
        }

        public override void Reset()
        {
            // HACK
            _resampleOperator.Reset();
        }
    }
}