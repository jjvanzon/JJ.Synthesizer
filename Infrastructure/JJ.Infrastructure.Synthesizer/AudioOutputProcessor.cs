using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using NAudio.Wave;

namespace JJ.Infrastructure.Synthesizer
{
    public class AudioOutputProcessor : IDisposable
    {
        private const int DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS = 100; // TODO: Make this 10 in the future?

        private readonly SampleProvider _sampleProvider;
        private readonly WaveOut _waveOut;

        public AudioOutputProcessor(IPatchCalculator patchCalculator)
        {
            _sampleProvider = new SampleProvider(patchCalculator);
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

        public void Start()
        {
            _waveOut.Play();
            _sampleProvider.Start();
        }

        public void Continue()
        {
            _sampleProvider.Continue();
        }

        public void Pause()
        {
            _sampleProvider.Pause();
        }

        public void Stop()
        {
            // TODO: Stop does not stop WaveOut, but when I did do that, the sound had a hiccup.
            _sampleProvider.Stop();
            _waveOut.Stop();
        }

        public double Time
        {
            get { return _sampleProvider.Time; }
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
