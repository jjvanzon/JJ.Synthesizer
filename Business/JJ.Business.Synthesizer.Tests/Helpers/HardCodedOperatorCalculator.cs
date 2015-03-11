using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class HardCodedOperatorCalculator
    {
        private ISampleCalculator _sampleCalculator;

        public HardCodedOperatorCalculator(Sample sample)
        {
            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample);
        }

        public double CalculateTimePowerWithEcho(double time)
        {
            // Echo
            double value = 0;
            double cumulativeDenominator = 1;
            double cumulativeDelay = 0;

            for (int i = 0; i < 15; i++)
            {
                // TimeAdd
                double transformedTime = time - cumulativeDelay;

                // TimePower
                double exponent = 1.5;
                transformedTime = Math.Sign(transformedTime) * Math.Pow(Math.Abs(transformedTime), 1 / exponent);

                // Sample
                double value2 = _sampleCalculator.CalculateValue(0, transformedTime);

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

            for (int i = 0; i < 15; i++)
            {
                // TimeAdd
                double transformedTime = time - cumulativeDelay;

                // TimeMultiply
                double multiplier = 1.5;
                transformedTime = transformedTime /= multiplier;

                // Sample
                double value2 = _sampleCalculator.CalculateValue(0, transformedTime);

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
