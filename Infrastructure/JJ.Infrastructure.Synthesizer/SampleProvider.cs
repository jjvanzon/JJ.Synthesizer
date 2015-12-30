using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Api;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Wave;

namespace JJ.Infrastructure.Synthesizer
{
    // TODO: let AudioOutputProcessor implement Sampleprovider explicity?
    internal class SampleProvider : ISampleProvider
    {
        private const int DEFAULT_SAMPLE_RATE = 44100;
        private const double SAMPLE_TIME = 1.0 / DEFAULT_SAMPLE_RATE;
        private const int DEFAULT_CHANNEL_COUNT = 1;
        private const double DEFAULT_FREQUENCY = 440.0;
        private const int DEFAULT_CHANNEL_INDEX = 0;

        private static WaveFormat _waveFormat = CreateWaveFormat();
        private IPatchCalculator _patchCalculator;

        private double _time;
        private bool _isRunning;

        public SampleProvider(IPatchCalculator patchCalculator)
        {
            if (patchCalculator == null) throw new NullException(() => patchCalculator);

            _patchCalculator = patchCalculator;
        }

        public WaveFormat WaveFormat
        {
            get { return _waveFormat; }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (!_isRunning)
            {
                // TODO: Find a faster way to zero the buffer?
                for (int i = offset; i < count; i++)
                {
                    buffer[i] = 0;
                }
                return count;
            }

            for (int i = offset; i < count; i++)
            {
                double value = _patchCalculator.Calculate(_time, DEFAULT_CHANNEL_INDEX);

                buffer[i] = (float)value;

                _time += SAMPLE_TIME;
            }

            return count;
        }

        public void Start()
        {
            _time = 0;
            _isRunning = true;
        }

        public void Pause()
        {
            _isRunning = false;
        }

        public void Continue()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
            _time = 0;
        }

        public double Time
        {
            get { return _time; }
        }

        private static WaveFormat CreateWaveFormat()
        {
            WaveFormat waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(DEFAULT_SAMPLE_RATE, DEFAULT_CHANNEL_COUNT);
            return waveFormat;
        }
    }
}
