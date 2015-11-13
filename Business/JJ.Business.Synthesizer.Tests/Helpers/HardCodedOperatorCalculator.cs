using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Api;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class HardCodedOperatorCalculator
    {
        private ISampleCalculator _sampleCalculator;

        public HardCodedOperatorCalculator(Sample sample, byte[] bytes)
        {
            _sampleCalculator = SampleApi.CreateCalculator(sample, bytes);
        }

        public double CalculateTimePowerWithEcho(double time)
        {
            // Echo
            double value = 0;
            double cumulativeDenominator = 1;
            double cumulativeDelay = 0;
            double exponent = 1.5;

            for (int i = 0; i < 15; i++)
            {
                // TimeAdd
                double transformedTime = time - cumulativeDelay;

                // TimePower
                transformedTime = Math.Sign(transformedTime) * Math.Pow(Math.Abs(transformedTime), 1 / exponent);

                // Sample
                double value2 = _sampleCalculator.CalculateValue(transformedTime, 0);

                // Divide
                value2 /= cumulativeDenominator;

                // Adder
                value += value2;

                cumulativeDenominator *= 1.5;
                cumulativeDelay += 0.25;
            }

            return value;
        }

        public double CalculateMultiplyWithEcho(double time)
        {
            // Echo
            double value = 0;
            double cumulativeDenominator = 1;
            double cumulativeDelay = 0;
            double multiplier = 1.5;

            for (int i = 0; i < 15; i++)
            {
                // TimeAdd
                double transformedTime = time - cumulativeDelay;

                // Sample
                double value2 = _sampleCalculator.CalculateValue(transformedTime, 0);

                // Multiply
                value2 *= multiplier;

                // Divide
                value2 /= cumulativeDenominator;

                // Adder
                value += value2;

                cumulativeDenominator *= 1.5;
                cumulativeDelay += 0.25;
            }

            return value;
        }
    }
}
