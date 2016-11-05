using System;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithCSharpCompilation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithCSharpCompilation
{
    public class ExamplePatchCalculatorOutputCode : IPatchCalculator 
    {
        private double[] _values;
        private int _framesPerChunk;

        private double _input1;
        private double _phase1;
        private double _prevPos1;
        private double _phase2;
        private double _prevPos2;
        private double _phase3;
        private double _prevPos3;
        private double _phase4;
        private double _prevPos4;
        private double _phase5;
        private double _prevPos5;
        private double _phase6;
        private double _prevPos6;
        private double _phase7;
        private double _prevPos7;
        private double _phase8;
        private double _prevPos8;

        public ExamplePatchCalculatorOutputCode(int framesPerChunk)
        {
            _framesPerChunk = framesPerChunk;
            _values = new double[_framesPerChunk];

            Reset();
        }

        public void Reset()
        {
            _phase1 = 0.0;
            _prevPos1 = 0.0;
            _phase2 = 0.0;
            _prevPos2 = 0.0;
            _phase3 = 0.0;
            _prevPos3 = 0.0;
            _phase4 = 0.0;
            _prevPos4 = 0.0;
            _phase5 = 0.0;
            _prevPos5 = 0.0;
            _phase6 = 0.0;
            _prevPos6 = 0.0;
            _phase7 = 0.0;
            _prevPos7 = 0.0;
            _phase8 = 0.0;
            _prevPos8 = 0.0;
        }

        public void SetInput(int listIndex, double input)
        {
            switch (listIndex)
            {
                case 0:
                    _input1 = input;
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] Calculate(double startTime, double frameDuration)
        {
            double[] values = _values;
            int framesPerChunk = _framesPerChunk;

            double input1 = _input1;
            double phase1 = _phase1;
            double prevPos1 = _prevPos1;
            double phase2 = _phase2;
            double prevPos2 = _prevPos2;
            double phase3 = _phase3;
            double prevPos3 = _prevPos3;
            double phase4 = _phase4;
            double prevPos4 = _prevPos4;
            double phase5 = _phase5;
            double prevPos5 = _prevPos5;
            double phase6 = _phase6;
            double prevPos6 = _prevPos6;
            double phase7 = _phase7;
            double prevPos7 = _prevPos7;
            double phase8 = _phase8;
            double prevPos8 = _prevPos8;

            double t0 = startTime;
            double t1;

            for (int i = 0; i < framesPerChunk; i++)
            {
                // Shift
                t1 = t0 + 0.25;

                // Sine
                phase1 += (t1 - prevPos1) * input1;
                prevPos1 = t1;
                double sine1 = SineCalculator.Sin(phase1);

                // Multiply
                double multiply1 = 10 * sine1;

                // Shift
                t1 = t0 + 0.25;

                // Sine
                phase2 += (t1 - prevPos2) * input1;
                prevPos2 = t1;
                double sine2 = SineCalculator.Sin(phase2);

                // Multiply
                double multiply2 = 10 * sine2;

                // Shift
                t1 = t0 + 0.25;

                // Sine
                phase3 += (t1 - prevPos3) * input1;
                prevPos3 = t1;
                double sine3 = SineCalculator.Sin(phase3);

                // Multiply
                double multiply3 = 10 * sine3;

                // Shift
                t1 = t0 + 0.25;

                // Sine
                phase4 += (t1 - prevPos4) * input1;
                prevPos4 = t1;
                double sine4 = SineCalculator.Sin(phase4);

                // Multiply
                double multiply4 = 10 * sine4;

                // Shift
                t1 = t0 + 0.25;

                // Sine
                phase5 += (t1 - prevPos5) * input1;
                prevPos5 = t1;
                double sine5 = SineCalculator.Sin(phase5);

                // Multiply
                double multiply5 = 10 * sine5;

                // Shift
                t1 = t0 + 0.25;

                // Sine
                phase6 += (t1 - prevPos6) * input1;
                prevPos6 = t1;
                double sine6 = SineCalculator.Sin(phase6);

                // Multiply
                double multiply6 = 10 * sine6;

                // Shift
                t1 = t0 + 0.25;

                // Sine
                phase7 += (t1 - prevPos7) * input1;
                prevPos7 = t1;
                double sine7 = SineCalculator.Sin(phase7);

                // Multiply
                double multiply7 = 10 * sine7;

                // Shift
                t1 = t0 + 0.25;

                // Sine
                phase8 += (t1 - prevPos8) * input1;
                prevPos8 = t1;
                double sine8 = SineCalculator.Sin(phase8);

                // Multiply
                double multiply8 = 10 * sine8;

                // Add
                double add1 = multiply8 + multiply7 + multiply6 + multiply5 + multiply4 + multiply3 + multiply2 + multiply1;

                double value = add1;

                values[i] = value;

                t0 += frameDuration;
            }

            _input1 = input1;
            _phase1 = phase1;
            _prevPos1 = prevPos1;
            _phase2 = phase2;
            _prevPos2 = prevPos2;
            _phase3 = phase3;
            _prevPos3 = prevPos3;
            _phase4 = phase4;
            _prevPos4 = prevPos4;
            _phase5 = phase5;
            _prevPos5 = prevPos5;
            _phase6 = phase6;
            _prevPos6 = prevPos6;
            _phase7 = phase7;
            _prevPos7 = prevPos7;
            _phase8 = phase8;
            _prevPos8 = prevPos8;

            return values;
        }
    }
}