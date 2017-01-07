using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;

namespace GeneratedCSharp
{
    public class Calculator : PatchCalculatorBase
    {
        // Fields

        private double _phase0;
        private double _prevPos0;
        private double _input0;
        private double _input1;

        // Constructor

        public Calculator(int targetSamplingRate)
            : base(targetSamplingRate)
        {
            _input0 = 0.0E0;
            _input1 = 0.0E0;

            Reset(time: 0.0);
        }

        // Calculate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Calculate(float[] buffer, int framesPerChunk, double startTime)
        {
            double frameDuration = _frameDuration;

            double phase0 = _phase0;
            double prevPos0 = _prevPos0;
            double input0 = _input0;
            double input1 = _input1;

            double t0 = startTime;

            for (int i = 0; i < framesPerChunk; i++)
            {

                // Sine
                phase0 += (t0 - prevPos0) * input1;
                prevPos0 = t0;
                double sine0 = SineCalculator.Sin(phase0);

                // Multiply
                double multiply0 = sine0 * input0;

                double value = multiply0;

                if (double.IsNaN(value)) // winmm will trip over NaN.
                {
                    value = 0;
                }

                float floatValue = (float)value; // TODO: This seems unsafe. What happens if the cast is invalid?

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                t0 += frameDuration;
            }

            _phase0 = phase0;
            _prevPos0 = prevPos0;
            _input0 = input0;
            _input1 = input1;
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

                case 1:
                    _input1 = value;
                    break;
            }
        }

        public override void SetValue(DimensionEnum dimensionEnum, double value)
        {
            base.SetValue(dimensionEnum, value);

            switch (dimensionEnum)
            {
                case DimensionEnum.Frequency:
                    _input0 = value;
                    break;

                case DimensionEnum.Volume:
                    _input1 = value;
                    break;
            }
        }

        // Reset

        public override void Reset(double time)
        {
            _phase0 = 0.0;
            _prevPos0 = 0.0;
        }
    }
}
