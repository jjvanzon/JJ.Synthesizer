using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Wave;

namespace JJ.Infrastructure.Synthesizer
{
    public class AudioOutputProcessor : ISampleProvider, IDisposable
    {
        private const int DEFAULT_CHANNEL_COUNT = 1;
        private const int DEFAULT_CHANNEL_INDEX = 0;
        private const int DEFAULT_SAMPLE_RATE = 44100;
        private const int DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS = 100; // TODO: Make this 10 in the future?
        private const double SAMPLE_TIME = 1.0 / DEFAULT_SAMPLE_RATE;

        private static WaveFormat _waveFormat = CreateWaveFormat();

        private readonly IPatchCalculator _patchCalculator;
        private readonly WaveOut _waveOut;

        private double _time;
        private volatile bool _isRunning;

        public AudioOutputProcessor(IPatchCalculator patchCalculator)
        {
            if (patchCalculator == null) throw new NullException(() => patchCalculator);

            _patchCalculator = patchCalculator;
            _waveOut = CreateWaveOut(this);
        }

        ~AudioOutputProcessor()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public double Time
        {
            get { return _time; }
        }

        public void Start()
        {
            _time = 0;
            _isRunning = true;

            _waveOut.Play();
        }

        public void Continue()
        {
            _isRunning = true;
        }

        public void Pause()
        {
            // TODO: It is unintuitive that Pause does not work as an alternative way to start the calculations.
            _isRunning = false;
        }

        public void Stop()
        {
            _waveOut.Stop();

            _isRunning = false;
            _time = 0;
        }

        // ISampleProvider

        WaveFormat ISampleProvider.WaveFormat
        {
            get { return _waveFormat; }
        }

        int ISampleProvider.Read(float[] buffer, int offset, int count)
        {
            if (!_isRunning)
            {
                Array.Clear(buffer, 0, buffer.Length);
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

        // Helpers

        private static WaveFormat CreateWaveFormat()
        {
            WaveFormat waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(DEFAULT_SAMPLE_RATE, DEFAULT_CHANNEL_COUNT);
            return waveFormat;
        }

        private WaveOut CreateWaveOut(ISampleProvider sampleProvider)
        {
            var waveOut = new WaveOut();
            waveOut.DesiredLatency = DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS;
            waveOut.Init(sampleProvider);
            return waveOut;
        }
    }
}
