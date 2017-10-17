//using System;
//using JJ.Business.Synthesizer.Calculation;

//namespace JJ.Business.Synthesizer.Tests.Helpers
//{
//    internal class HardCodedOperatorCalculator
//    {
//        private readonly ICalculatorWithPosition _sampleCalculator;

//        public HardCodedOperatorCalculator(ICalculatorWithPosition sampleCalculator)
//        {
//            _sampleCalculator = sampleCalculator ?? throw new ArgumentNullException(nameof(sampleCalculator));
//        }

//        public double CalculateTimePowerWithEcho(double time)
//        {
//            // Echo
//            double value = 0;
//            double cumulativeDenominator = 1;
//            double cumulativeDelay = 0;
//            const double exponent = 1.5;

//            for (int i = 0; i < 15; i++)
//            {
//                // TimeAdd
//                double transformedTime = time - cumulativeDelay;

//                // TimePower
//                transformedTime = Math.Sign(transformedTime) * Math.Pow(Math.Abs(transformedTime), 1 / exponent);

//                // Sample
//                double value2 = _sampleCalculator.Calculate(transformedTime);

//                // Divide
//                value2 /= cumulativeDenominator;

//                // Adder
//                value += value2;

//                cumulativeDenominator *= 1.5;
//                cumulativeDelay += 0.25;
//            }

//            return value;
//        }

//        public double CalculateMultiplyWithEcho(double time)
//        {
//            // Echo
//            double value = 0;
//            double cumulativeDenominator = 1;
//            double cumulativeDelay = 0;
//            const double multiplier = 1.5;

//            for (int i = 0; i < 15; i++)
//            {
//                // TimeAdd
//                double transformedTime = time - cumulativeDelay;

//                // Sample
//                double value2 = _sampleCalculator[0].Calculate(transformedTime);

//                // Multiply
//                value2 *= multiplier;

//                // Divide
//                value2 /= cumulativeDenominator;

//                // Adder
//                value += value2;

//                cumulativeDenominator *= 1.5;
//                cumulativeDelay += 0.25;
//            }

//            return value;
//        }
//    }
//}
