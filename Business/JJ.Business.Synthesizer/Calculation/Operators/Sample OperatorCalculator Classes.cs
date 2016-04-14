using System;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Enums;
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
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public Sample_WithVarFrequency_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);
            // TODO: Cast to int can fail.
            int channelIndex = (int)dimensionStack.Get(DimensionEnum.Channel);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);
            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
            
            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * rate;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double value = _sampleCalculator.CalculateValue(_phase, channelIndex);

            _previousPosition = position;

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Sample_WithConstFrequency_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly double _rate;
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public Sample_WithConstFrequency_OperatorCalculator(
            double frequency,
            ISampleCalculator sampleCalculator,
            DimensionEnum dimensionEnum)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _sampleCalculator = sampleCalculator;
            _dimensionIndex = (int)dimensionEnum;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);
            // TODO: Cast to int can fail.
            int channelIndex = (int)dimensionStack.Get(DimensionEnum.Channel);

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * _rate;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double value = _sampleCalculator.CalculateValue(_phase, channelIndex);

            _previousPosition = position;

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Sample_WithVarFrequency_MonoToStereo_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public Sample_WithVarFrequency_MonoToStereo_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator, 
            ISampleCalculator sampleCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);
            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * rate;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(_phase, 0);

            _previousPosition = position;

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Sample_WithConstFrequency_MonoToStereo_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly int _dimensionIndex;
        private readonly double _rate;

        private double _phase;
        private double _previousPosition;

        public Sample_WithConstFrequency_MonoToStereo_OperatorCalculator(
            double frequency, 
            ISampleCalculator sampleCalculator,
            DimensionEnum dimensionEnum)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _sampleCalculator = sampleCalculator;
            _dimensionIndex = (int)dimensionEnum;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * _rate;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            // Return the single channel for both channels.
            double value = _sampleCalculator.CalculateValue(_phase, 0);

            _previousPosition = position;

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Sample_WithVarFrequency_StereoToMono_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly ISampleCalculator _sampleCalculator;
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public Sample_WithVarFrequency_StereoToMono_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            ISampleCalculator sampleCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _frequencyCalculator = frequencyCalculator;
            _sampleCalculator = sampleCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate(dimensionStack);
            double rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * rate;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double value0 = _sampleCalculator.CalculateValue(_phase, 0);
            double value1 = _sampleCalculator.CalculateValue(_phase, 1);

            _previousPosition = position;

            return value0 + value1;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }

    internal class Sample_WithConstFrequency_StereoToMono_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ISampleCalculator _sampleCalculator;
        private readonly int _dimensionIndex;
        private readonly double _rate;

        private double _phase;
        private double _previousPosition;

        public Sample_WithConstFrequency_StereoToMono_OperatorCalculator(
            double frequency, 
            ISampleCalculator sampleCalculator,
            DimensionEnum dimensionEnum)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);

            _sampleCalculator = sampleCalculator;
            _dimensionIndex = (int)dimensionEnum;

            _rate = frequency / Sample_OperatorCalculator_Helper.BASE_FREQUENCY;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * _rate;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double value0 = _sampleCalculator.CalculateValue(_phase, 0);
            double value1 = _sampleCalculator.CalculateValue(_phase, 1);

            _previousPosition = position;

            return value0 + value1;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset(dimensionStack);
        }
    }
}
