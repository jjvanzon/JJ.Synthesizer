using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SawUp_OperatorCalculator_ConstFrequency_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public SawUp_OperatorCalculator_ConstFrequency_WithOriginShifting(
            double frequency,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double phase = (position - _origin) * _frequency;
            double value = -1.0 + (2.0 * phase % 2.0);

            return value;
        }

        public override void Reset()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            base.Reset();
        }
    }

    internal class SawUp_OperatorCalculator_ConstFrequency_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SawUp_OperatorCalculator_ConstFrequency_NoOriginShifting(
            double frequency,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertFrequency(frequency);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequency = frequency;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double phase = position * _frequency;
            double value = -1.0 + (2.0 * phase % 2.0);

            return value;
        }
    }

    internal class SawUp_OperatorCalculator_VarFrequency_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public SawUp_OperatorCalculator_VarFrequency_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            _phase = 0.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;
            double value = -1.0 + (2.0 * _phase % 2.0);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class SawUp_OperatorCalculator_VarFrequency_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public SawUp_OperatorCalculator_VarFrequency_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double frequency = _frequencyCalculator.Calculate();

            double phase = position * frequency;
            double value = -1.0 + (2.0 * phase % 2.0);

            return value;
        }
    }
}
