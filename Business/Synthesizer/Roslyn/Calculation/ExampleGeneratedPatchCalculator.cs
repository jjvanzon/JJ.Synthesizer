using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace GeneratedCSharp
{
    public class GeneratedPatchCalculator : PatchCalculatorBase
    {
        // Fields

        private double _phase_6;
        private double _prevpos_7;
        private double _something_1;
        private double _frequency_4;

        // Constructor

        public GeneratedPatchCalculator(int samplingRate, int channelCount, int channelIndex)
            : base(samplingRate, channelCount, channelIndex)
        {
            _something_1 = 1.0E0;
            _frequency_4 = 4.4E2;

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

            double phase_6 = _phase_6;
            double prevpos_7 = _prevpos_7;
            double something_1 = _something_1;
            double frequency_4 = _frequency_4;

            double time_0_0 = startTime;
            double u0021blu00e0_2_0 = 0;

            for (int i = channelIndex; i < valueCount; i += channelCount)
            {
                // SawDown
                double sawdown_3 = u0021blu00e0_2_0 * something_1;
                sawdown_3 = 1.0 - (2.0 * sawdown_3 % 2.0);

                // Multiply
                double multiply_5 = frequency_4 * sawdown_3;

                // Sine
                phase_6 += (time_0_0 - prevpos_7) * multiply_5;
                prevpos_7 = time_0_0;
                double sine_8 = SineCalculator.Sin(phase_6);

                // Accumulate
                double value = sine_8;

                if (double.IsNaN(value))
                {
                    value = 0;
                }

                float floatValue = (float)value;

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                time_0_0 += frameDuration;
            }

            _phase_6 = phase_6;
            _prevpos_7 = prevpos_7;
            _something_1 = something_1;
            _frequency_4 = frequency_4;
        }

        // Values

        public override void SetValue(int listIndex, double value)
        {
            base.SetValue(listIndex, value);

            switch (listIndex)
            {
                case 0:
                    _something_1 = value;
                    break;

                case 1:
                    _frequency_4 = value;
                    break;

            }
        }

        public override void SetValue(DimensionEnum dimensionEnum, double value)
        {
            base.SetValue(dimensionEnum, value);

            switch (dimensionEnum)
            {
                case DimensionEnum.Undefined:
                    _something_1 = value;
                    break;

                case DimensionEnum.Frequency:
                    _frequency_4 = value;
                    break;

            }
        }

        public override void SetValue(string name, double value)
        {
            base.SetValue(name, value);

            string canonicalName = NameHelper.ToCanonical(name);

            if (String.Equals(name, "something", StringComparison.Ordinal))
            {
                _something_1 = value;
            }
            if (String.Equals(name, "", StringComparison.Ordinal))
            {
                _frequency_4 = value;
            }
        }

        // Reset

        public override void Reset(double time)
        {
            _phase_6 = 0.0;
            _prevpos_7 = time;
        }
    }
}
