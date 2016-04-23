using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Wave;

namespace JJ.Presentation.Synthesizer.NAudio
{
    /// <summary> Most of the work is done by the AudioOutputSampleProvider. </summary>
    public class AudioOutputProcessor
    {
        private int _desiredLatencyInMilliseconds;

        private readonly AudioOutputSampleProvider _sampleProvider;
        private WaveOut _waveOut;

        public AudioOutputProcessor(IPatchCalculatorContainer patchCalculatorContainer, AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            _desiredLatencyInMilliseconds = (int)(audioOutput.BufferDuration * 1000.0);

            _sampleProvider = new AudioOutputSampleProvider(patchCalculatorContainer, audioOutput);
        }

        public double Time => _sampleProvider._time;

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
            _waveOut = CreateWaveOut();

            _sampleProvider._time = 0;
            _sampleProvider._isRunning = true;

            _waveOut.Play();
        }

        public void Continue() => _sampleProvider._isRunning = true;

        public bool IsRunning => _sampleProvider._isRunning;

        // TODO: It is unintuitive that Pause does not work as an alternative way to start the calculations.
        public void Pause() => _sampleProvider._isRunning = false;

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

        private WaveOut CreateWaveOut()
        {
            var waveOut = new WaveOut();
            waveOut.DesiredLatency = _desiredLatencyInMilliseconds;
            waveOut.Init(_sampleProvider);
            return waveOut;
        }
    }
}
