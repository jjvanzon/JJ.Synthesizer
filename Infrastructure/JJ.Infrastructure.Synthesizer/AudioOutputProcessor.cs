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
            _waveOut = CreateWaveOut(_sampleProvider);
        }

        public AudioOutputProcessor()
        {
            _sampleProvider = new SampleProvider();
            _waveOut = CreateWaveOut(_sampleProvider);
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

        private WaveOut CreateWaveOut(ISampleProvider sampleProvider)
        {
            var waveOut = new WaveOut();
            waveOut.DesiredLatency = DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS;
            waveOut.Init(sampleProvider);
            return waveOut;
        }

        public void Play()
        {
            _waveOut.Play();
        }

        public void Stop()
        {
            _waveOut.Stop();
            _sampleProvider.ResetTime();
        }
    }
}
