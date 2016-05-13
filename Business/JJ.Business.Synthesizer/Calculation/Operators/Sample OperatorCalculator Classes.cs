using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal static class Sample_OperatorCalculator_Helper
    {
        public const double BASE_FREQUENCY = 440.0;
    }

    internal class Sample_WithVarFrequency_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly DimensionStack _channelDimensionStack;
        private readonly int _channelCount;

        private double _phase;
        private double _previousPosition;

        public Sample_WithVarFrequency_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            if (channelDimensionStack == null) throw new NullException(() => channelDimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;

            _channelCount = _sampleCalculator.ChannelCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double channelIndexDouble = _channelDimensionStack.Get();
            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _channelCount))
            {
                return 0.0;
            }
            int channelIndex = (int)channelIndexDouble;

            double position = _dimensionStack.Get();

            double frequency = _frequencyCalculator.Calculate();
            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
            
            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * rate;

            _phase = phase;

            double value = _sampleCalculator.CalculateValue(_phase, channelIndex);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get();

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Sample_WithConstFrequency_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;
        private readonly DimensionStack _channelDimensionStack;
        private readonly int _channelCount;

        private double _phase;
        private double _previousPosition;

        public Sample_WithConstFrequency_OperatorCalculator(
            double frequency,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            if (channelDimensionStack == null) throw new NullException(() => channelDimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
            _channelDimensionStack = channelDimensionStack;

            _channelCount = sampleCalculator.ChannelCount;
            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double channelIndexDouble = _channelDimensionStack.Get();
            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _channelCount))
            {
                return 0.0;
            }
            int channelIndex = (int)channelIndexDouble;

            double position = _dimensionStack.Get();

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * _rate;

            _phase = phase;

            double value = _sampleCalculator.CalculateValue(_phase, channelIndex);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get();

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Sample_WithVarFrequency_MonoToStereo_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Sample_WithVarFrequency_MonoToStereo_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator, 
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get();

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
            double position = _dimensionStack.Get();

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Sample_WithConstFrequency_MonoToStereo_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly int _dimensionIndex;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Sample_WithConstFrequency_MonoToStereo_OperatorCalculator(
            double frequency, 
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * _rate;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(_phase, 0);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get();

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Sample_WithVarFrequency_StereoToMono_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Sample_WithVarFrequency_StereoToMono_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get();

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
            double position = _dimensionStack.Get();

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Sample_WithConstFrequency_StereoToMono_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Sample_WithConstFrequency_StereoToMono_OperatorCalculator(
            double frequency, 
            ISampleCalculator sampleCalculator,
            DimensionStack dimensionStack)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _sampleCalculator = sampleCalculator;
            _dimensionStack = dimensionStack;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * _rate;

            double value0 = _sampleCalculator.CalculateValue(_phase, 0);
            double value1 = _sampleCalculator.CalculateValue(_phase, 1);

            _previousPosition = position;

            return value0 + value1;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get();

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }
}
