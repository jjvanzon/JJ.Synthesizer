using System;
using System.Threading;
using JJ.Framework.Exceptions;
using NAudio.Wave;

namespace JJ.Presentation.Synthesizer.NAudio
{
    /// <summary> Thread-safe. Most of the work is done by the AudioOutputSampleProvider. </summary>
    internal class AudioOutputProcessor
    {
        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly TimeProvider _timeProvider;
        private readonly AudioOutputSampleProvider _sampleProvider;
        private readonly object _lock = new object();

        private int _desiredLatencyInMilliseconds;
        private WaveOut _waveOut;

        /// <summary>
        /// Even though this constructor initializes a lot of things,
        /// you can still mutate some of the things using the UpdateAudioProperties method.
        /// </summary>
        public AudioOutputProcessor(
            IPatchCalculatorContainer patchCalculatorContainer,
            TimeProvider timeProvider,
            int samplingRate,
            int channelCount,
            double desiredBufferDuration)
        {
            _patchCalculatorContainer = patchCalculatorContainer ?? throw new NullException(() => patchCalculatorContainer);
            _timeProvider = timeProvider ?? throw new NullException(() => timeProvider);

            _desiredLatencyInMilliseconds = ConvertDurationToMilliseconds(desiredBufferDuration);
            _sampleProvider = new AudioOutputSampleProvider(_patchCalculatorContainer, _timeProvider, samplingRate, channelCount);
        }

        public void UpdateAudioProperties(int samplingRate, int channelCount, double desiredBufferDuration)
        {
            lock (_lock)
            {
                _desiredLatencyInMilliseconds = ConvertDurationToMilliseconds(desiredBufferDuration);
                _sampleProvider.SetAudioProperties(samplingRate, channelCount);
            }
        }

        public Thread StartThread()
        {
            var thread = new Thread(Start);
            thread.Start();

            // Starting AudioOutputProcessor on another thread seems to 
            // start and keep alive a new Windows message loop,
            // but that does not mean that the thread keeps running.

            return thread;
        }

        private void Start()
        {
            lock (_lock)
            {
                _waveOut = new WaveOut
                {
                    DesiredLatency = _desiredLatencyInMilliseconds
                };

                _waveOut.Init(_sampleProvider);

                _sampleProvider.IsRunning = true;

                _waveOut.Play();
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (_waveOut != null)
                {
                    _waveOut.Stop();
                    _waveOut.Dispose();
                }

                _sampleProvider.IsRunning = false;
            }
        }

        // Helpers

        private int ConvertDurationToMilliseconds(double duration)
        {
            if (duration <= 0.0) throw new LessThanOrEqualException(() => duration, 0.0);
            int milliseconds = (int)Math.Ceiling(duration * 1000.0);
            return milliseconds;
        }
    }
}
