using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using NAudio.Wave;

namespace JJ.Infrastructure.Synthesizer
{
    public class AudioOutputProcessor : IDisposable
    {
        // TODO: Make this 10.
        private int DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS = 100;

        private SampleProvider _sampleProvider;
        private WaveOut _waveOut;

        public AudioOutputProcessor(IPatchCalculator patchCalculator)
        {
            _sampleProvider = new SampleProvider(patchCalculator);
        }

        public AudioOutputProcessor()
        {
            _sampleProvider = new SampleProvider();
        }

        ~AudioOutputProcessor()
        {
            Dispose();
        }

        public void Dispose()
        {
            Stop();

            GC.SuppressFinalize(this);
        }

        // TODO: I might create _waveOut in the constructor in the future again.

        public void Play()
        {
            Stop();

            _waveOut = CreateWaveOut(_sampleProvider);
            _waveOut.Play();
        }

        public void Stop()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
                _waveOut = null;
                _sampleProvider.ResetTime();
            }
        }

        public void ResetTime()
        {
            _sampleProvider.ResetTime();
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
