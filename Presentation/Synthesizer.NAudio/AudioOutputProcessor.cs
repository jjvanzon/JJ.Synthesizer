using System;
using JJ.Framework.Exceptions;
using NAudio.Wave;

namespace JJ.Presentation.Synthesizer.NAudio
{
    /// <summary> Most of the work is done by the AudioOutputSampleProvider. </summary>
    public class AudioOutputProcessor
    {
        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly AudioOutputSampleProvider _sampleProvider;

        private int _desiredLatencyInMilliseconds;
        private WaveOut _waveOut;

        public AudioOutputProcessor(
            IPatchCalculatorContainer patchCalculatorContainer,
            int samplingRate,
            int channelCount,
            double desiredBufferDuration)
        {
            _patchCalculatorContainer = patchCalculatorContainer;

            _desiredLatencyInMilliseconds = ConvertDurationToMilliseconds(desiredBufferDuration);
            _sampleProvider = new AudioOutputSampleProvider(_patchCalculatorContainer, samplingRate, channelCount);
        }

        public void UpdateAudioProperties(int samplingRate, int channelCount, double desiredBufferDuration)
        {
            _desiredLatencyInMilliseconds = ConvertDurationToMilliseconds(desiredBufferDuration);
            _sampleProvider.SetAudioProperties(samplingRate, channelCount);
        }

        public void Start()
        {
            _waveOut = new WaveOut
            {
                DesiredLatency = _desiredLatencyInMilliseconds 
            };

            _waveOut.Init(_sampleProvider);

            TimeProvider.Time = 0;
            _sampleProvider.IsRunning = true;

            _waveOut.Play();
        }

        public void Stop()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
            }

            _sampleProvider.IsRunning = false;
            TimeProvider.Time = 0;
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
