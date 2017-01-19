using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;

namespace GeneratedCSharp
{
    public class GeneratedPatchCalculator : PatchCalculatorBase
    {
        // Fields

        private double _phase0;
        private double _phase1;
        private double _prevPos0;
        private double _prevPos1;
        private double _input0;

        // Constructor

        public GeneratedPatchCalculator(int samplingRate, int channelCount, int channelIndex)
            : base(samplingRate, channelCount, channelIndex)
        {
            _input0 = 1.0E0;

            Reset(time: 0.0);
        }

        // Calculate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Calculate(float[] buffer, int frameCount, double startTime)
        {
            double frameDuration = _frameDuration;
            int channelCount = _channelCount;
            int channelIndex = _channelIndex;
            int valueCount = frameCount * channelCount;

            double phase0 = _phase0;
            double phase1 = _phase1;
            double prevPos0 = _prevPos0;
            double prevPos1 = _prevPos1;
            double input0 = _input0;

            double time_sd_a0 = startTime;

            for (int i = channelIndex; i < valueCount; i += channelCount)
            {
                // Sine
                phase0 += (time_sd_a0 - prevPos0) * input0;
                prevPos0 = time_sd_a0;
                double sine_op0 = SineCalculator.Sin(phase0);

                // Multiply
                double multiply_op0 = sine_op0 * 8.8E2;

                // Sine
                phase1 += (time_sd_a0 - prevPos1) * multiply_op0;
                prevPos1 = time_sd_a0;
                double sine_op1 = SineCalculator.Sin(phase1);

                // Accumulate
                double value = sine_op1;

                if (double.IsNaN(value))
                {
                    value = 0;
                }

                float floatValue = (float)value;

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                time_sd_a0 += frameDuration;
            }

            _phase0 = phase0;
            _phase1 = phase1;
            _prevPos0 = prevPos0;
            _prevPos1 = prevPos1;
            _input0 = input0;
        }

        // Values

        public override double GetValue(int listIndex)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(int listIndex, double value)
        {
            base.SetValue(listIndex, value);

            switch (listIndex)
            {
                case 0:
                    _input0 = value;
                    break;

            }
        }

        public override void SetValue(DimensionEnum dimensionEnum, double value)
        {
            base.SetValue(dimensionEnum, value);

            switch (dimensionEnum)
            {
                case DimensionEnum.VibratoSpeed:
                    _input0 = value;
                    break;

            }
        }

        // Reset

        public override void Reset(double time)
        {
            _phase0 = 0.0;
            _phase1 = 0.0;
            _prevPos0 = time;
            _prevPos1 = time;
        }
    }
}
