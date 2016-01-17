using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Wave;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public static class AudioOutputProcessor
    {
        private const int DEFAULT_BUFFER_LENGTH_IN_MILLISECONDS = 100; // TODO: Make this 10 in the future?

        private static readonly AudioOutputSampleProvider_MultiThreaded _sampleProvider = 
                            new AudioOutputSampleProvider_MultiThreaded();

        private static WaveOut _waveOut;

        public static double Time { get { return _sampleProvider._time; } }

        /// <summary>
        /// Initializes and then immediately pauses, to prevent calculations at startup,
        /// but also a hick-up upon the first note.
        /// </summary>
        public static void StartAndPause()
        {
            Start();
            Pause();
        }

        public static void Start()
        {
            _waveOut = CreateWaveOut(_sampleProvider);

            _sampleProvider._time = 0;
            _sampleProvider._isRunning = true;

            _waveOut.Play();
        }

        public static void Continue()
        {
            _sampleProvider._isRunning = true;
        }

        public static void Pause()
        {
            // TODO: It is unintuitive that Pause does not work as an alternative way to start the calculations.
            _sampleProvider._isRunning = false;
        }

        public static void Stop()
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

        private static void AssertWaveOut()
        {
            if (_waveOut == null) throw new NullException(() => _waveOut);
        }
    }
}
