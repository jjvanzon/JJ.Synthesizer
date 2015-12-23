//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Api;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Data.Synthesizer;
//using NAudio.Wave;

//namespace JJ.Infrastructure.Synthesizer
//{
//    internal class SampleProvider : ISampleProvider
//    {
//        private const int DEFAULT_SAMPLE_RATE = 44100;
//        //private const double SAMPLE_TIME = 1.0 / DEFAULT_SAMPLE_RATE
//        private const int DEFAULT_CHANNEL_COUNT = 1;
//        private const double DEFAULT_FREQUENCY = 440.0;
//        private const int DEFAULT_CHANNEL_INDEX = 0;

//        private WaveFormat _waveFormat;
//        private IPatchCalculator _patchCalculator;

//        private double _time;

//        public SampleProvider()
//        {
//            _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(DEFAULT_SAMPLE_RATE, DEFAULT_CHANNEL_COUNT);
//            _patchCalculator = CreateTestPatchCalculator();
//        }

//        public WaveFormat WaveFormat
//        {
//            get { return _waveFormat; }
//        }

//        public int Read(float[] buffer, int offset, int count)
//        {
//            double step = (double)count / (double)DEFAULT_SAMPLE_RATE;

//            for (int i = offset; i < count; i++)
//            {
//                _time += step;

//                double value = _patchCalculator.Calculate(_time, DEFAULT_CHANNEL_INDEX);

//                buffer[i] = (float)value;
//            }

//            return count;
//        }

//        private IPatchCalculator CreateTestPatchCalculator()
//        {
//            var x = new PatchApi();
//            Outlet outlet = x.Sine(x.Number(DEFAULT_FREQUENCY));
//            IPatchCalculator calculator = x.CreateOptimizedCalculator(outlet);
//            return calculator;
//        }
//    }
//}
