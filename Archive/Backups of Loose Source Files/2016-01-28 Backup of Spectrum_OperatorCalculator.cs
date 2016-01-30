//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Reflection.Exceptions;
//using Lomont;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Spectrum_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly OperatorCalculatorBase _startTimeCalculator;
//        private readonly OperatorCalculatorBase _endTimeCalculator;
//        private readonly OperatorCalculatorBase _pointsCalculator;
//        private readonly LomontFFT _lomontFFT;

//        private double[] _frequencies;

//        private double _time;
//        private int _channelIndex;

//        public Spectrum_OperatorCalculator(
//            OperatorCalculatorBase signalCalculator,
//            OperatorCalculatorBase startTimeCalculator,
//            OperatorCalculatorBase endTimeCalculator,
//            OperatorCalculatorBase pointsCalculator)
//            : base(new OperatorCalculatorBase[] {
//                signalCalculator,
//                startTimeCalculator,
//                endTimeCalculator,
//                pointsCalculator })
//        {
//            if (signalCalculator == null) throw new NullException(() => signalCalculator);
//            if (startTimeCalculator == null) throw new NullException(() => startTimeCalculator);
//            if (endTimeCalculator == null) throw new NullException(() => endTimeCalculator);
//            if (pointsCalculator == null) throw new NullException(() => pointsCalculator);

//            _signalCalculator = signalCalculator;
//            _startTimeCalculator = startTimeCalculator;
//            _endTimeCalculator = endTimeCalculator;
//            _pointsCalculator = pointsCalculator;

//            _lomontFFT = new LomontFFT();
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            _time = time;
//            _channelIndex = channelIndex;

//            // TODO: It should emit a bundle. But how should it do that? It can't.
//            // Bundles do not exist in the calculators, just in the entities.
//            // Time also does not say anything about anything, does it?
//            // Temporarily using time.
//            // If it has a lot of frequencies, that is how you would plot it,
//            // using the time parameter.
//            // That does mean that the usage of _time inside ResetState is strange.

//            if (time < 0) time = 0;
//            if (time > _frequencies.Length - 1) time = _frequencies.Length - 1;

//            int i = (int)time;

//            double frequency = _frequencies[i];

//            return frequency;
//        }

//        public override void ResetState()
//        {
//            // _time was set in Calculate.
//            double pointCountDouble = _pointsCalculator.Calculate(_time, _channelIndex);
//            double startTime = _startTimeCalculator.Calculate(_time, _channelIndex);
//            double endTime = _endTimeCalculator.Calculate(_time, _channelIndex);

//            // TODO: Chance of overflow.
//            int pointCount = (int)pointCountDouble;
//            int pointCountTimesTwo = pointCount * 2;

//            // API requires an array size twice the number of points.
//            double[] data = new double[pointCountTimesTwo];
//            double dt = endTime - startTime / (pointCount - 1); // -1, because 10 points = 9 'pieces'.

//            double t = startTime;
//            for (int i = 0; i < pointCountTimesTwo; i += 2)
//            {
//                double value = _signalCalculator.Calculate(t, _channelIndex);
//                data[i] = value;

//                t += dt;
//            }

//            _lomontFFT.RealFFT(data, forward: true);

//            int j = 0;
//            for (int i = 0; i < pointCount; i++)
//            {
//                _frequencies[i] = data[j];
//                j += 2;
//            }
//        }
//    }
//}