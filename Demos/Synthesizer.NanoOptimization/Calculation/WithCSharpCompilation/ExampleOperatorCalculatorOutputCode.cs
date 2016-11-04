using System;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithCSharpCompilation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithCSharpCompilation
{
    public class ExampleOperatorCalculatorOutputCode : IOperatorCalculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double t0 = 0.0;
            double t1 = 0.0;
            double input1 = 0.0;
            double phase1 = 0.0;
            double prevPos1 = 0.0;
            double phase2 = 0.0;
            double prevPos2 = 0.0;
            double phase3 = 0.0;
            double prevPos3 = 0.0;
            double phase4 = 0.0;
            double prevPos4 = 0.0;
            double phase5 = 0.0;
            double prevPos5 = 0.0;
            double phase6 = 0.0;
            double prevPos6 = 0.0;
            double phase7 = 0.0;
            double prevPos7 = 0.0;
            double phase8 = 0.0;
            double prevPos8 = 0.0;

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

            return add1;
        }
    }
}
