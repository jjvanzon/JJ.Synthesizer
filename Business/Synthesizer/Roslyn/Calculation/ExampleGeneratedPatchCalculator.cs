using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace GeneratedCSharp
{
    public class GeneratedPatchCalculator : PatchCalculatorBase
    {
        // Fields

        private double _origin_1;
        private double _origin_5;
        private double _lowpassfilterx1_8;
        private double _lowpassfilterx2_9;
        private double _lowpassfiltery1_10;
        private double _lowpassfiltery2_11;
        private double _lowpassfiltera0_12;
        private double _lowpassfiltera1_13;
        private double _lowpassfiltera2_14;
        private double _lowpassfiltera3_15;
        private double _lowpassfiltera4_16;

        // Constructor

        public GeneratedPatchCalculator(int samplingRate, int channelCount, int channelIndex)
            : base(samplingRate, channelCount, channelIndex)
        {
            Reset(time: 0.0);
        }

        // Calculate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Calculate(float[] buffer, int frameCount, double startTime)
        {
            double frameDuration = _frameDuration;
            int channelCount = _channelCount;
            int channelIndex = _channelIndex;
            double samplingRate = _samplingRate;
            double nyquistFrequency = _nyquistFrequency;

            double origin_1 = _origin_1;
            double origin_5 = _origin_5;
            double lowpassfilterx1_8 = _lowpassfilterx1_8;
            double lowpassfilterx2_9 = _lowpassfilterx2_9;
            double lowpassfiltery1_10 = _lowpassfiltery1_10;
            double lowpassfiltery2_11 = _lowpassfiltery2_11;
            double lowpassfiltera0_12 = _lowpassfiltera0_12;
            double lowpassfiltera1_13 = _lowpassfiltera1_13;
            double lowpassfiltera2_14 = _lowpassfiltera2_14;
            double lowpassfiltera3_15 = _lowpassfiltera3_15;
            double lowpassfiltera4_16 = _lowpassfiltera4_16;

            double time_a_0;

            int valueCount = frameCount * channelCount;
            time_a_0 = startTime;

            for (int i = channelIndex; i < valueCount; i += channelCount)
            {
                // Number

                // Sine
                double sine_2 = (time_a_0 - origin_1) * 2.0E0;
                sine_2 = SineCalculator.Sin(sine_2);

                // Add
                double add_3 = sine_2 + 3.0E0;

                // Multiply
                double multiply_4 = add_3 * 4.33333333333333E2;

                // SawDown
                double sawdown_6 = (time_a_0 - origin_5) * 4.4E2;
                sawdown_6 = 1.0 - (2.0 * sawdown_6 % 2.0);

                // LowPassFilter
                double limitedmaxfrequency_17 = multiply_4;
                if (limitedmaxfrequency_17 > nyquistFrequency) limitedmaxfrequency_17 = nyquistFrequency;

                BiQuadFilterWithoutFields.SetLowPassFilterVariables(
                    samplingRate, limitedmaxfrequency_17, 1.0E0,
                    out lowpassfiltera0_12, out lowpassfiltera1_13, out lowpassfiltera2_14, out lowpassfiltera3_15, out lowpassfiltera4_16);

                double lowpassfilter_7 = BiQuadFilterWithoutFields.Transform(
                    sawdown_6, lowpassfiltera0_12, lowpassfiltera1_13, lowpassfiltera2_14, lowpassfiltera3_15, lowpassfiltera4_16,
                    ref lowpassfilterx1_8, ref lowpassfilterx2_9, ref lowpassfiltery1_10, ref lowpassfiltery2_11);

                // Accumulate
                double value = lowpassfilter_7;

                if (double.IsNaN(value))
                {
                    value = 0;
                }

                float floatValue = (float)value;

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                time_a_0 += frameDuration;
            }

            _origin_1 = origin_1;
            _origin_5 = origin_5;
            _lowpassfilterx1_8 = lowpassfilterx1_8;
            _lowpassfilterx2_9 = lowpassfilterx2_9;
            _lowpassfiltery1_10 = lowpassfiltery1_10;
            _lowpassfiltery2_11 = lowpassfiltery2_11;
            _lowpassfiltera0_12 = lowpassfiltera0_12;
            _lowpassfiltera1_13 = lowpassfiltera1_13;
            _lowpassfiltera2_14 = lowpassfiltera2_14;
            _lowpassfiltera3_15 = lowpassfiltera3_15;
            _lowpassfiltera4_16 = lowpassfiltera4_16;
        }

        // Values

        public override void SetValue(int listIndex, double value)
        {
            base.SetValue(listIndex, value);

        }

        public override void SetValue(DimensionEnum dimensionEnum, double value)
        {
            base.SetValue(dimensionEnum, value);

        }

        public override void SetValue(string name, double value)
        {
            base.SetValue(name, value);

            string canonicalName = NameHelper.ToCanonical(name);

        }

        public override void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)
        {
            base.SetValue(dimensionEnum, listIndex, value);


        }

        public override void SetValue(string name, int listIndex, double value)
        {
            base.SetValue(name, listIndex, value);

            string canonicalName = NameHelper.ToCanonical(name);


        }

        // Reset

        public override void Reset(double time)
        {
        }
    }
}
