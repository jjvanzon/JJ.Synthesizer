using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Helpers;

namespace GeneratedCSharp
{
    public class GeneratedPatchCalculator : PatchCalculatorBase
    {
        // Fields

        private double _channel_a_0;
        private double __e_0;

        private readonly ArrayCalculator_MinPositionZero_Line _arraycalculator_5;


        // Constructor

        public GeneratedPatchCalculator(
            int samplingRate,
            int channelCount,
            int channelIndex,
            Dictionary<string, double[]> arrays,
            Dictionary<string, double> arrayRates
            )
            : base(samplingRate, channelCount, channelIndex)
        {
            _arraycalculator_5 = new ArrayCalculator_MinPositionZero_Line(arrays["arraycalculator_5"], arrayRates["arraycalculator_5"]);

            Reset(time: 0.0);
        }

        // Calculate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Calculate(float[] buffer, int frameCount, double startTime)
        {
            double frameDuration = _frameDuration;

            double channel_a_0 = _channel_a_0;
            double _e_0 = __e_0;

            var arraycalculator_5 = _arraycalculator_5;

            double time_g_0;

            int valueCount = frameCount * 1;
            time_g_0 = startTime;

            for (int i = 0; i < valueCount; i += 1)
            {
                // Sample
                double sample_2 = 0.0;
                if (ConversionHelper.CanCastToInt32(channel_a_0))
                {
                    int channel_1 = (int)channel_a_0;
                    double phase_3 = _e_0 * 1.0E0;
                    switch (channel_1)
                    {
                        case 0:
                            sample_2 = arraycalculator_5.Calculate(phase_3);
                            break;
                    }
                }

                // Accumulate
                double value = sample_2;

                if (double.IsNaN(value))
                {
                    value = 0;
                }

                float floatValue = (float)value;

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                time_g_0 += frameDuration;
            }

            _channel_a_0 = channel_a_0;
            __e_0 = _e_0;
        }

        // Values

        public override void SetValue(int listIndex, double value)
        {
            base.SetValue(listIndex, value);

        }

        public override void SetValue(DimensionEnum dimensionEnum, double value)
        {
            base.SetValue(dimensionEnum, value);

            switch (dimensionEnum)
            {
                case DimensionEnum.Channel:
                    _channel_a_0 = value;
                    break;

                case DimensionEnum.Undefined:
                    __e_0 = value;
                    break;

            }
        }

        public override void SetValue(string name, double value)
        {
            base.SetValue(name, value);

            string canonicalName = NameHelper.ToCanonical(name);

            if (String.Equals(canonicalName, "", StringComparison.Ordinal))
            {
                __e_0 = value;
            }

        }

        public override void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)
        {
            base.SetValue(dimensionEnum, listIndex, value);

            switch (dimensionEnum)
            {
                case DimensionEnum.Channel:
                    _channel_a_0 = value;
                    break;

                case DimensionEnum.Undefined:
                    __e_0 = value;
                    break;

            }

        }

        public override void SetValue(string name, int listIndex, double value)
        {
            base.SetValue(name, listIndex, value);

            string canonicalName = NameHelper.ToCanonical(name);

            if (String.Equals(canonicalName, "", StringComparison.Ordinal))
            {
                __e_0 = value;
            }


        }

        // Reset

        public override void Reset(double time)
        {
        }
    }
}
