using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal static class Sample_OperatorCalculator_Helper
    {
        public const double BASE_FREQUENCY = 440.0;
    }

    internal class Sample_OperatorCalculator_VarFrequency_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly DimensionStack _channelDimensionStack;
        private readonly int _dimensionStackIndex;
        private readonly int _channelDimensionStackIndex;
        private readonly int _maxChannelIndex;

        private double _phase;
        private double _previousPosition;

        public Sample_OperatorCalculator_VarFrequency_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
            : base(new[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
            OperatorCalculatorHelper.AssertDimensionStack(channelDimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
            _channelDimensionStackIndex = channelDimensionStack.CurrentIndex;

            _maxChannelIndex = _sampleCalculator.ChannelCount - 1;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double channelIndexDouble = _channelDimensionStack.Get();
#else
            double channelIndexDouble = _channelDimensionStack.Get(_channelDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_channelDimensionStack, _channelDimensionStackIndex);
#endif
            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _maxChannelIndex))
            {
                return 0.0;
            }
            int channelIndex = (int)channelIndexDouble;

#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double frequency = _frequencyCalculator.Calculate();

            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * rate;

            double value = _sampleCalculator.CalculateValue(_phase, channelIndex);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
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
        }
    }

    internal class Sample_OperatorCalculator_ConstFrequency_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;
        private readonly DimensionStack _channelDimensionStack;
        private readonly int _maxChannelIndex;
        private readonly int _dimensionStackIndex;
        private readonly int _channelDimensionStackIndex;

        private double _origin;

        public Sample_OperatorCalculator_ConstFrequency_WithOriginShifting(
            double frequency,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
            OperatorCalculatorHelper.AssertDimensionStack(channelDimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
            _channelDimensionStackIndex = dimensionStack.CurrentIndex;

            _maxChannelIndex = sampleCalculator.ChannelCount - 1;
            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;

            ResetPrivate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double channelIndexDouble = _channelDimensionStack.Get();
#else
            double channelIndexDouble = _channelDimensionStack.Get(_channelDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_channelDimensionStack, _channelDimensionStackIndex);
#endif
            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _maxChannelIndex))
            {
                return 0.0;
            }
            int channelIndex = (int)channelIndexDouble;

#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double phase = (position - _origin) * _rate;

            double value = _sampleCalculator.CalculateValue(phase, channelIndex);

            return value;
        }

        public override void Reset()
        {
            ResetPrivate();
        }

        private void ResetPrivate()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
        }
    }

    internal class Sample_OperatorCalculator_VarFrequency_MonoToStereo_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Sample_OperatorCalculator_VarFrequency_MonoToStereo_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
            : base(new[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
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

            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * rate;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(_phase, 0);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
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
        }
    }

    internal class Sample_OperatorCalculator_ConstFrequency_MonoToStereo_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Sample_OperatorCalculator_ConstFrequency_MonoToStereo_WithOriginShifting(
            double frequency,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;

            ResetPrivate();
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

            double phase = (position - _origin) * _rate;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(phase, 0);

            return value;
        }

        public override void Reset()
        {
            ResetPrivate();
        }

        private void ResetPrivate()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
        }
    }

    internal class Sample_OperatorCalculator_VarFrequency_StereoToMono_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Sample_OperatorCalculator_VarFrequency_StereoToMono_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
            : base(new[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
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

            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * rate;

            double value0 = _sampleCalculator.CalculateValue(_phase, 0);
            double value1 = _sampleCalculator.CalculateValue(_phase, 1);

            _previousPosition = position;

            return value0 + value1;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
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
        }
    }

    internal class Sample_OperatorCalculator_ConstFrequency_StereoToMono_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Sample_OperatorCalculator_ConstFrequency_StereoToMono_WithOriginShifting(
            double frequency,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;

            ResetPrivate();
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

            double phase = (position - _origin) * _rate;

            double value0 = _sampleCalculator.CalculateValue(phase, 0);
            double value1 = _sampleCalculator.CalculateValue(phase, 1);

            return value0 + value1;
        }

        public override void Reset()
        {
            ResetPrivate();
        }

        private void ResetPrivate()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
        }
    }

    // Copies with Phase Tracking and Origin Shifting removed.

    internal class Sample_OperatorCalculator_VarFrequency_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly DimensionStack _channelDimensionStack;
        private readonly int _dimensionStackIndex;
        private readonly int _channelDimensionStackIndex;
        private readonly int _maxChannelIndex;

        public Sample_OperatorCalculator_VarFrequency_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
            : base(new[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
            OperatorCalculatorHelper.AssertDimensionStack(channelDimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
            _channelDimensionStackIndex = channelDimensionStack.CurrentIndex;

            _maxChannelIndex = _sampleCalculator.ChannelCount - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double channelIndexDouble = _channelDimensionStack.Get();
#else
            double channelIndexDouble = _channelDimensionStack.Get(_channelDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_channelDimensionStack, _channelDimensionStackIndex);
#endif

            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _maxChannelIndex))
            {
                return 0.0;
            }
            int channelIndex = (int)channelIndexDouble;

#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double frequency = _frequencyCalculator.Calculate();

            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
            double phase = position * rate;

            double value = _sampleCalculator.CalculateValue(phase, channelIndex);

            return value;
        }
    }

    internal class Sample_OperatorCalculator_ConstFrequency_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;
        private readonly DimensionStack _channelDimensionStack;
        private readonly int _maxChannelIndex;
        private readonly int _dimensionStackIndex;
        private readonly int _channelDimensionStackIndex;

        public Sample_OperatorCalculator_ConstFrequency_NoOriginShifting(
            double frequency,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
            OperatorCalculatorHelper.AssertDimensionStack(channelDimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
            _channelDimensionStackIndex = dimensionStack.CurrentIndex;

            _maxChannelIndex = sampleCalculator.ChannelCount - 1;
            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double channelIndexDouble = _channelDimensionStack.Get();
#else
            double channelIndexDouble = _channelDimensionStack.Get(_channelDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_channelDimensionStack, _channelDimensionStackIndex);
#endif

            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _maxChannelIndex))
            {
                return 0.0;
            }
            int channelIndex = (int)channelIndexDouble;

#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double phase = position * _rate;

            double value = _sampleCalculator.CalculateValue(phase, channelIndex);

            return value;
        }
    }

    internal class Sample_OperatorCalculator_VarFrequency_MonoToStereo_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sample_OperatorCalculator_VarFrequency_MonoToStereo_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
            : base(new[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
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

            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
            double phase = position * rate;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(phase, 0);

            return value;
        }
    }

    internal class Sample_OperatorCalculator_ConstFrequency_MonoToStereo_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sample_OperatorCalculator_ConstFrequency_MonoToStereo_NoOriginShifting(
            double frequency,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
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

            double phase = position * _rate;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(phase, 0);

            return value;
        }
    }

    internal class Sample_OperatorCalculator_VarFrequency_StereoToMono_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sample_OperatorCalculator_VarFrequency_StereoToMono_NoPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
            : base(new[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
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

            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
            double phase = position * rate;

            double value0 = _sampleCalculator.CalculateValue(phase, 0);
            double value1 = _sampleCalculator.CalculateValue(phase, 1);

            return value0 + value1;
        }
    }

    internal class Sample_OperatorCalculator_ConstFrequency_StereoToMono_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Sample_OperatorCalculator_ConstFrequency_StereoToMono_NoOriginShifting(
            double frequency,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
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

            double phase = position * _rate;

            double value0 = _sampleCalculator.CalculateValue(phase, 0);
            double value1 = _sampleCalculator.CalculateValue(phase, 1);

            return value0 + value1;
        }
    }
}
