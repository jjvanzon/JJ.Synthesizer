﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Mathematics;
//using JJ.Framework.Reflection.Exceptions;
//using Lomont;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Spectrum_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly double _startTime;
//        private readonly double _endTime;
//        private readonly int _frequencyCount;
//        private readonly LomontFFT _lomontFFT;

//        private double[] _harmonicVolumes;

//        private int _channelIndex;
//        private int _frequencyCountTimesTwo;

//        public Spectrum_OperatorCalculator(
//            OperatorCalculatorBase signalCalculator,
//            double startTime,
//            double endTime,
//            int frequencyCount)
//            : base(new OperatorCalculatorBase[] { signalCalculator })
//        {
//            if (signalCalculator == null) throw new NullException(() => signalCalculator);
//            if (frequencyCount < 2) throw new LessThanException(() => frequencyCount, 2);
//            if (!Maths.IsPowerOf2(frequencyCount)) throw new Exception("frequencyCount must be a power of 2.");

//            _signalCalculator = signalCalculator;
//            _startTime = startTime;
//            _endTime = endTime;
//            _frequencyCount = frequencyCount;

//            _frequencyCountTimesTwo = _frequencyCount * 2;
//            _lomontFFT = new LomontFFT();
//            _harmonicVolumes = new double[frequencyCount];

//            ResetState();
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            _channelIndex = channelIndex;

//            if (time < 0) time = 0;
//            if (time > _harmonicVolumes.Length - 1) time = _harmonicVolumes.Length - 1;

//            int i = (int)time;

//            double frequency = _harmonicVolumes[i];

//            return frequency;
//        }

//        public override void ResetState()
//        {
//            // API requires an array size twice the number of points.
//            //double[] data = new double[_frequencyCountTimesTwo];
//            double[] data = new double[_frequencyCount];
//            //double dt = (_endTime - _startTime) / (_frequencyCount - 1); // -1, because 10 points = 9 'pieces'.
//            double dt = (_endTime - _startTime) / _frequencyCount;
//            //double dt = (_endTime - _startTime) / _frequencyCountTimesTwo;

//            double t = _startTime;
//            //for (int i = 0; i < _frequencyCountTimesTwo; i++)
//            for (int i = 0; i < _frequencyCount; i++)
//            {
//                double value = _signalCalculator.Calculate(t, _channelIndex);
//                data[i] = value;

//                t += dt;
//            }

//            // TODO: Not sure which is faster: forward or backward.
//            _lomontFFT.RealFFT(data, forward: true);

//            //_lomontFFT.FFT(data, forward: true);

//            //int j = 0;
//            //for (int i = 3; i < _frequencyCountTimesTwo; i += 2)
//            //{
//            //    _harmonicVolumes[j] = data[i];
//            //    j++;
//            //}

//            _harmonicVolumes = data;
//        }
//    }
//}