using System;
using NAudio.Wave;
using JJ.Business.Synthesizer.Calculation.Patches;
using System.Threading;
using JJ.Framework.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class AudioOutputSampleProvider : ISampleProvider
    {
        private readonly WaveFormat _waveFormat;
        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly double _frameDuration;
        private readonly int _channelCount;

        public bool IsRunning { get; set; }

        public AudioOutputSampleProvider(IPatchCalculatorContainer patchCalculatorContainer, int samplingRate, int channelCount)
        {
            if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);
            if (channelCount <= 0) throw new LessThanOrEqualException(() => channelCount, 0);

            _patchCalculatorContainer = patchCalculatorContainer ?? throw new NullException(() => patchCalculatorContainer);
            _frameDuration = 1.0 / samplingRate;
            _channelCount = channelCount;
            _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(samplingRate, channelCount);
        }

        WaveFormat ISampleProvider.WaveFormat => _waveFormat;

        int ISampleProvider.Read(float[] buffer, int offset, int count)
        {
            ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;

            lck.EnterReadLock();
            try
            {
                IPatchCalculator patchCalculator = _patchCalculatorContainer.Calculator;

                if (!IsRunning || patchCalculator == null)
                {
                    Array.Clear(buffer, 0, buffer.Length);
                    return count;
                }

                int frameCount = count / _channelCount;

                patchCalculator.Calculate(buffer, frameCount, TimeProvider.Time);

                TimeProvider.Time += _frameDuration * frameCount;

                return count;
            }
            finally
            {
                lck.ExitReadLock();
            }
        }
    }
}