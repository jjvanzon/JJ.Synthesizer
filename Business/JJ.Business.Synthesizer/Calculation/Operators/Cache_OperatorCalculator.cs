using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Cache_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ArrayCalculatorBase[] _arrayCalculator;

        public Cache_OperatorCalculator(ArrayCalculatorBase[] arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculator = arrayCalculators;
        }

        // TODO: Move this private method to the PatchCalculatorVisitor and call it.

        private ArrayCalculatorBase[] CreateArrayCalculators(
            OperatorCalculatorBase signalCalculator,
            int channelCount, double startTime, double endTime, double rate, 
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (channelCount < 1) throw new LessThanException(() => channelCount, 1);
            if (Double.IsNaN(endTime)) throw new NaNException(() => endTime);
            if (Double.IsInfinity(endTime)) throw new InfinityException(() => endTime);
            if (Double.IsNaN(startTime)) throw new NaNException(() => startTime);
            if (Double.IsInfinity(startTime)) throw new InfinityException(() => startTime);
            if (Double.IsNaN(rate)) throw new NaNException(() => rate);
            if (Double.IsInfinity(rate)) throw new InfinityException(() => rate);
            if (rate == 0.0) throw new ZeroException(() => rate);
            if (endTime <= startTime) throw new LessThanOrEqualException(() => endTime, () => startTime);

            double duration = endTime - startTime;
            int tickCount = (int)(duration / rate) + 1;
            double tickDuration = 1.0 / rate;

            var arrayCalculators = new ArrayCalculatorBase[channelCount];

            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
            {
                double[] samples = new double[tickCount];
                double time = startTime;
                for (int i = 0; i < tickCount; i++)
                {
                    double sample = signalCalculator.Calculate(time, channelIndex);
                    samples[i] = sample;

                    time += tickDuration;
                }

                ArrayCalculatorBase arrayCalculator = ArrayCalculatorFactory.CreateArrayCalculator(samples, rate, startTime, resampleInterpolationTypeEnum);
                arrayCalculators[channelIndex] = arrayCalculator;
            }

            return arrayCalculators;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator[channelIndex].CalculateValue(time);
        }
    }
}
