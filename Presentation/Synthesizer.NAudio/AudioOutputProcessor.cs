using System;
using JJ.Framework.Exceptions;
using NAudio.Wave;

namespace JJ.Presentation.Synthesizer.NAudio
{
    /// <summary> Most of the work is done by the AudioOutputSampleProvider. </summary>
    public class AudioOutputProcessor
    {
        private readonly int _desiredLatencyInMilliseconds;

        private readonly AudioOutputSampleProvider _sampleProvider;
        private WaveOut _waveOut;

        public AudioOutputProcessor(
            IPatchCalculatorContainer patchCalculatorContainer, 
            int samplingRate,
            int channelCount,
            double desiredBufferDuration)
        {
            if (desiredBufferDuration <= 0.0) throw new LessThanOrEqualException(() => desiredBufferDuration, 0.0);

            _desiredLatencyInMilliseconds = (int)Math.Ceiling(desiredBufferDuration * 1000.0);
            _sampleProvider = new AudioOutputSampleProvider(patchCalculatorContainer, samplingRate, channelCount);
        }

        public void Start()
        {
            _waveOut = CreateWaveOut();

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

        private WaveOut CreateWaveOut()
        {
            var waveOut = new WaveOut
            {
                DesiredLatency = _desiredLatencyInMilliseconds 
            };

            waveOut.Init(_sampleProvider);

            return waveOut;
        }
    }
}
