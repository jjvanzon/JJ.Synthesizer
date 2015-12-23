using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;

namespace JJ.Infrastructure.Synthesizer
{
    public class AudioOutputProcessor
    {
        // TODO: Make this 10.
        private int DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS = 100;

        private ISampleProvider _sampleProvider;
        private WaveOut _waveOut;

        public AudioOutputProcessor()
        {
            _sampleProvider = new SampleProvider();

            _waveOut = new WaveOut();
            _waveOut.DesiredLatency = DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS;
            _waveOut.Init(_sampleProvider);
        }

        public void Play()
        {
            _waveOut.Play();
        }

        public void Stop()
        {
            _waveOut.Stop();
        }
    }
}
