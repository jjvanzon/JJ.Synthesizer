using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using Lomont;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Spectrum_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _startTime;
        private readonly double _endTime;
        private readonly int _frequencyCount;
        private readonly LomontFFT _lomontFFT;

        private double[] _frequencies;

        private int _channelIndex;
        private int _frequencyCountTimesTwo;

        public Spectrum_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double startTime,
            double endTime,
            int frequencyCount)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (frequencyCount < 2) throw new LessThanException(() => frequencyCount, 2);
            if (frequencyCount % 2 != 0) throw new Exception("frequencyCount must be a multiple of 2");

            _signalCalculator = signalCalculator;
            _startTime = startTime;
            _endTime = endTime;
            _frequencyCount = frequencyCount;

            _frequencyCountTimesTwo = _frequencyCount * 2;
            _lomontFFT = new LomontFFT();
            _frequencies = new double[frequencyCount];

            ResetState();
        }

        public override double Calculate(double time, int channelIndex)
        {
            _channelIndex = channelIndex;

            if (time < 0) time = 0;
            if (time > _frequencies.Length - 1) time = _frequencies.Length - 1;

            int i = (int)time;

            double frequency = _frequencies[i];

            return frequency;
        }

        public override void ResetState()
        {
            // API requires an array size twice the number of points.
            double[] data = new double[_frequencyCountTimesTwo];
            //double dt = _endTime - _startTime / (_frequencyCount - 1); // -1, because 10 points = 9 'pieces'.
            double dt = _endTime - _startTime / _frequencyCount;

            double t = _startTime;
            for (int i = 0; i < _frequencyCountTimesTwo; i += 2)
            {
                double value = _signalCalculator.Calculate(t, _channelIndex);
                data[i] = value;

                t += dt;
            }

            // TODO: Not sure which is faster: forward or backward.
            _lomontFFT.RealFFT(data, forward: true);

            int j = 0;
            for (int i = 0; i < _frequencyCount; i++)
            {
                _frequencies[i] = data[j];
                j += 2;
            }
        }
    }
}