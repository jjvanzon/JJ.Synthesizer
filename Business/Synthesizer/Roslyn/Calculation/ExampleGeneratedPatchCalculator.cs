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
        private double _u0021blu00e0_0_0;
        private double _something_2;
        private double _frequency_4;

        // Constructor

        public GeneratedPatchCalculator(int samplingRate, int channelCount, int channelIndex)
            : base(samplingRate, channelCount, channelIndex)
        {
            _something_2 = 1.0E0;
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

            double phase_6 = _phase_6;
            double prevpos_7 = _prevpos_7;
            double u0021blu00e0_0_0 = _u0021blu00e0_0_0;
            double something_2 = _something_2;
            double frequency_4 = _frequency_4;

            double u0021blu00e0_0_1;
            double time_1_0;
            double time_1_1;

            int valueCount = frameCount * channelCount;
            time_1_0 = startTime;

            for (int i = channelIndex; i < valueCount; i += channelCount)
            {
                // Squash
                u0021blu00e0_0_1 = u0021blu00e0_0_0 * 2.0E0;

                // Shift
                time_1_1 = time_1_0 + 5.0E-1;

                // SawDown
                double sawdown_3 = u0021blu00e0_0_1 * something_2;
                sawdown_3 = 1.0 - (2.0 * sawdown_3 % 2.0);

                // Multiply
                double multiply_5 = frequency_4 * sawdown_3;

                // Sine
                phase_6 += (time_1_1 - prevpos_7) * multiply_5;
                prevpos_7 = time_1_1;
                double sine_8 = SineCalculator.Sin(phase_6);

                // Accumulate
                double value = sine_8;

                if (double.IsNaN(value))
                {
                    value = 0;
                }

                float floatValue = (float)value;

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                time_1_0 += frameDuration;
            }

            _phase_6 = phase_6;
            _prevpos_7 = prevpos_7;
            _u0021blu00e0_0_0 = u0021blu00e0_0_0;
            _something_2 = something_2;
            _frequency_4 = frequency_4;
        }

        // Values

        public override void SetValue(int listIndex, double value)
        {
            base.SetValue(listIndex, value);

            switch (listIndex)
            {
                case 0:
                    _something_2 = value;
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
                case DimensionEnum.Frequency:
                    _frequency_4 = value;
                    break;

            }
        }

        public override void SetValue(string name, double value)
        {
            base.SetValue(name, value);

            string canonicalName = NameHelper.ToCanonical(name);

            if (String.Equals(canonicalName, "!blà", StringComparison.Ordinal))
            {
                _u0021blu00e0_0_0 = value;
            }

            if (String.Equals(canonicalName, "something", StringComparison.Ordinal))
            {
                _something_2 = value;
            }

        }

        public override void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)
        {
            base.SetValue(dimensionEnum, listIndex, value);


            if (dimensionEnum == DimensionEnum.Frequency && listIndex == 0)
            {
                _frequency_4 = value;
            }

        }

        public override void SetValue(string name, int listIndex, double value)
        {
            base.SetValue(name, listIndex, value);

            string canonicalName = NameHelper.ToCanonical(name);

            if (String.Equals(canonicalName, "!blà", StringComparison.Ordinal))
            {
                _u0021blu00e0_0_0 = value;
            }


            if (String.Equals(canonicalName, "something") && listIndex == 0)
            {
                _something_2 = value;
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
