using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace GeneratedCSharp
{
    public class GeneratedPatchCalculator : PatchCalculatorBase
    {
        // Fields

        private double _origin_2;
        private double _origin_5;

        private readonly ArrayCalculator_MinPositionZero_Line _curvecalculator1498745_3;


        // Constructor

        public GeneratedPatchCalculator(
            int samplingRate,
            int channelCount,
            int channelIndex,
            Dictionary<int, double[]> arrays,
            Dictionary<int, double> arrayRates
            )
            : base(samplingRate, channelCount, channelIndex)
        {
            _curvecalculator1498745_3 = new ArrayCalculator_MinPositionZero_Line(arrays[1498745], arrayRates[1498745]);

            Reset(time: 0.0);
        }

        // Calculate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Calculate(float[] buffer, int frameCount, double startTime)
        {
            double frameDuration = _frameDuration;

            double origin_2 = _origin_2;
            double origin_5 = _origin_5;

            var curvecalculator1498745_3 = _curvecalculator1498745_3;

            double time_b_0;

            int valueCount = frameCount * 1;
            time_b_0 = startTime;

            for (int i = 0; i < valueCount; i += 1)
            {
                // Curve
                double phase_0 = time_b_0 - origin_2;
                double curve_4 = curvecalculator1498745_3.Calculate(phase_0);

                // Sine
                double sine_6 = (time_b_0 - origin_5) * 8.8E2;
                sine_6 = SineCalculator.Sin(sine_6);

                // Multiply
                double multiply_7 = sine_6 * curve_4;

                // Accumulate
                double value = multiply_7;

                if (double.IsNaN(value))
                {
                    value = 0;
                }

                float floatValue = (float)value;

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                time_b_0 += frameDuration;
            }

            _origin_2 = origin_2;
            _origin_5 = origin_5;
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
