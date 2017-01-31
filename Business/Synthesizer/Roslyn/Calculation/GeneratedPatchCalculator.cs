using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Calculation.Arrays;

namespace GeneratedCSharp
{
    public class GeneratedPatchCalculator : PatchCalculatorBase
    {
        // Fields

        private double _origin_1;
        private double _origin_7;

        private readonly ArrayCalculator_MinPositionZero_Line _curvecalculator1498745_8;


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
            _curvecalculator1498745_8 = new ArrayCalculator_MinPositionZero_Line(arrays[1498745], arrayRates[1498745]);

            Reset(time: 0.0);
        }

        // Calculate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Calculate(float[] buffer, int frameCount, double startTime)
        {
            double frameDuration = _frameDuration;

            double origin_1 = _origin_1;
            double origin_7 = _origin_7;

            var curvecalculator1498745_8 = _curvecalculator1498745_8;

            double time_a_0;

            int valueCount = frameCount * 1;
            time_a_0 = startTime;

            for (int i = 0; i < valueCount; i += 1)
            {
                // Triangle
                double triangle_2 = (time_a_0 - origin_1) * 8.8E2;
                double shiftedphase_3 = triangle_2 + 0.25;
                double relativephase_4 = shiftedphase_3 % 1.0;
                double triangle_5;
                if (relativephase_4 < 0.5) triangle_5 = -1.0 + 4.0 * relativephase_4;
                else triangle_5 = 3.0 - 4.0 * relativephase_4;

                // Curve
                double phase_6 = time_a_0 - origin_7;
                double curve_9 = curvecalculator1498745_8.Calculate(phase_6);

                // Multiply
                double multiply_10 = curve_9 * triangle_5;

                // Accumulate
                double value = multiply_10;

                if (double.IsNaN(value))
                {
                    value = 0;
                }

                float floatValue = (float)value;

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                time_a_0 += frameDuration;
            }

            _origin_1 = origin_1;
            _origin_7 = origin_7;
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
