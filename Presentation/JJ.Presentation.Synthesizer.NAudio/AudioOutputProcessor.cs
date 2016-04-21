using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using NAudio.Wave;

namespace JJ.Presentation.Synthesizer.NAudio
{
    /// <summary>
    /// Most of the work is done by the AudioOutputSampleProvider.
    /// </summary>
    public class AudioOutputProcessor
    {
        private const int DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS = 100; // TODO: Make this 10 in the future?

        private readonly AudioOutputSampleProvider _sampleProvider;

        private WaveOut _waveOut;

        public AudioOutputProcessor(IPatchCalculatorContainer patchCalculatorContainer, AudioOutput audioOutput)
        {
            _sampleProvider = new AudioOutputSampleProvider(patchCalculatorContainer, audioOutput);
        }

        public double Time { get { return _sampleProvider._time; } }

        /// <summary>
        /// Initializes and then immediately pauses, to prevent calculations at startup,
        /// but also a hick-up upon the first note.
        /// </summary>
        public void StartAndPause()
        {
            Start();
            Pause();
        }

        public void Start()
        {
            _waveOut = CreateWaveOut(_sampleProvider);

            _sampleProvider._time = 0;
            _sampleProvider._isRunning = true;

            _waveOut.Play();
        }

        public void Continue()
        {
            _sampleProvider._isRunning = true;
        }

        public void Pause()
        {
            // TODO: It is unintuitive that Pause does not work as an alternative way to start the calculations.
            _sampleProvider._isRunning = false;
        }

        public void Stop()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
            }

            _sampleProvider._isRunning = false;
            _sampleProvider._time = 0;
        }

        // Helpers

        private static WaveOut CreateWaveOut(ISampleProvider sampleProvider)
        {
            var waveOut = new WaveOut();
            waveOut.DesiredLatency = DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS;
            waveOut.Init(sampleProvider);
            return waveOut;
        }
    }
}
