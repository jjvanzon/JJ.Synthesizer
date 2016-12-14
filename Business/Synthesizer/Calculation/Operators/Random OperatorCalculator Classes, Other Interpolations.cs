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
        private readonly OperatorCalculatorBase _interpolateOperator;

        public Random_OperatorCalculator_OtherInterpolationTypes(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator })
        {
            // HACK in a piece of patch, to reuse the Interpolate_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_OperatorCalculator_BlockAndStripe_VarFrequency(
                randomCalculator, randomCalculatorOffset, rateCalculator, dimensionStack);

            // Lead their outputs to a Interpolate operator calculator
            _interpolateOperator = OperatorCalculatorFactory.Create_Interpolate_OperatorCalculator(
                resampleInterpolationTypeEnum,
                signalCalculator: randomCalculator2,
                samplingRateCalculator: rateCalculator,
                dimensionStack: dimensionStack);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _interpolateOperator.Calculate();
        }

        public override void Reset()
        {
            // HACK
            _interpolateOperator.Reset();
        }
    }
}