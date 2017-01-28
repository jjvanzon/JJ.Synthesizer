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

            double origin_1 = _origin_1;
            double origin_5 = _origin_5;
            double lowpassfilterx1_8 = _lowpassfilterx1_8;
            double lowpassfilterx2_9 = _lowpassfilterx2_9;
            double lowpassfiltery1_10 = _lowpassfiltery1_10;
            double lowpassfiltery2_11 = _lowpassfiltery2_11;

            double time_a_0;

            int valueCount = frameCount * 2;
            time_a_0 = startTime;

            for (int i = 1; i < valueCount; i += 2)
            {
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
                double limitedfrequency_17 = multiply_4;
                if (limitedfrequency_17 > 2.205E4) limitedfrequency_17 = 2.205E4;

                double lowpassfiltera0_12, lowpassfiltera1_13, lowpassfiltera2_14, lowpassfiltera3_15, lowpassfiltera4_16;

                BiQuadFilterWithoutFields.SetLowPassFilterVariables(
                    4.41E4, limitedfrequency_17, 1.0E0,
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
