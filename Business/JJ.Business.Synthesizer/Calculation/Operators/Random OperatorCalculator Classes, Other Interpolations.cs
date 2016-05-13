// MIND THE HACKS IN THIS FILE! IT MAY BE THE CAUSE OF YOUR PROBLEMS!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Random_OperatorCalculator_LineRememberT0 : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _resampleOperator;

        public Random_OperatorCalculator_LineRememberT0(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            // HACK in a piece of patch, to reuse the Resample_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                randomCalculator, randomCalculatorOffset, rateCalculator, phaseShiftCalculator, dimensionEnum, dimensionStack);

            // Lead their outputs to a Resample operator calculator
            _resampleOperator = new Resample_OperatorCalculator_LineRememberT0(
                signalCalculator: randomCalculator2,
                samplingRateCalculator: rateCalculator,
                dimensionEnum: dimensionEnum,
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

    internal class Random_OperatorCalculator_LineRememberT1 : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _resampleOperator;

        public Random_OperatorCalculator_LineRememberT1(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            // HACK in a piece of patch, to reuse the Resample_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                randomCalculator, randomCalculatorOffset, rateCalculator, phaseShiftCalculator, dimensionEnum, dimensionStack);

            // Lead their outputs to a Resample operator calculator
            _resampleOperator = new Resample_OperatorCalculator_LineRememberT1(
                signalCalculator: randomCalculator2,
                samplingRateCalculator: rateCalculator,
                dimensionEnum: dimensionEnum,
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

    internal class Random_OperatorCalculator_CubicEquidistant : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _resampleOperator;

        public Random_OperatorCalculator_CubicEquidistant(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            // HACK in a piece of patch, to reuse the Resample_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                randomCalculator, randomCalculatorOffset, rateCalculator, phaseShiftCalculator, dimensionEnum, dimensionStack);

            // Lead their outputs to a Resample operator calculator
            _resampleOperator = new Resample_OperatorCalculator_CubicEquidistant(
                signalCalculator: randomCalculator2,
                samplingRateCalculator: rateCalculator,
                dimensionEnum: dimensionEnum,
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

    internal class Random_OperatorCalculator_CubicAbruptSlope : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _resampleOperator;

        public Random_OperatorCalculator_CubicAbruptSlope(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            // HACK in a piece of patch, to reuse the Resample_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                randomCalculator, randomCalculatorOffset, rateCalculator, phaseShiftCalculator, dimensionEnum, dimensionStack);

            // Lead their outputs to a Resample operator calculator
            _resampleOperator = new Resample_OperatorCalculator_CubicAbruptSlope(
                signalCalculator: randomCalculator2,
                samplingRateCalculator: rateCalculator, 
                dimensionEnum: dimensionEnum,
                dimensionStack : dimensionStack);
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

    internal class Random_OperatorCalculator_CubicSmoothSlope : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _resampleOperator;

        public Random_OperatorCalculator_CubicSmoothSlope(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            // HACK in a piece of patch, to reuse the Resample_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                randomCalculator, randomCalculatorOffset, rateCalculator, phaseShiftCalculator, dimensionEnum, dimensionStack);

            // Lead their outputs to a Resample operator calculator
            _resampleOperator = new Resample_OperatorCalculator_CubicSmoothSlope(
                signalCalculator: randomCalculator2,
                samplingRateCalculator: rateCalculator,
                dimensionEnum: dimensionEnum,
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

    internal class Random_OperatorCalculator_Hermite : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _resampleOperator;

        public Random_OperatorCalculator_Hermite(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            // HACK in a piece of patch, to reuse the Resample_OperatorCalculator's capability of
            // handling many types of interpolation.

            // Create a second Random operator calculator.
            var randomCalculator2 = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                randomCalculator, randomCalculatorOffset, rateCalculator, phaseShiftCalculator, dimensionEnum, dimensionStack);

            // Lead their outputs to a Resample operator calculator
            _resampleOperator = new Resample_OperatorCalculator_Hermite(
                signalCalculator: randomCalculator2,
                samplingRateCalculator: rateCalculator,
                dimensionEnum: dimensionEnum,
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