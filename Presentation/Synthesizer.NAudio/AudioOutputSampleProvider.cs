using System;
using NAudio.Wave;
using JJ.Business.Synthesizer.Calculation.Patches;
using System.Threading;
using JJ.Framework.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class AudioOutputSampleProvider : ISampleProvider
    {
        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly object _lock = new object();

        private WaveFormat _waveFormat;
        private double _frameDuration;
        private int _channelCount;

        public bool IsRunning { get; set; }

        public AudioOutputSampleProvider(IPatchCalculatorContainer patchCalculatorContainer, int samplingRate, int channelCount)
        {
            _patchCalculatorContainer = patchCalculatorContainer ?? throw new NullException(() => patchCalculatorContainer);

            SetAudioProperties(samplingRate, channelCount);
        }

        WaveFormat ISampleProvider.WaveFormat => _waveFormat;

        public void SetAudioProperties(int samplingRate, int channelCount)
        {
            lock (_lock)
            {
                if (samplingRate <= 0) throw new LessThanOrEqualException(() => samplingRate, 0);
                if (channelCount <= 0) throw new LessThanOrEqualException(() => channelCount, 0);

                _frameDuration = 1.0 / samplingRate;
                _channelCount = channelCount;
                _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(samplingRate, channelCount);
            }
        }

        int ISampleProvider.Read(float[] buffer, int offset, int count)
        {
            lock (_lock)
            {
                ReaderWriterLockSlim calculatorLock = _patchCalculatorContainer.Lock;

                calculatorLock.EnterReadLock();
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
                    calculatorLock.ExitReadLock();
                }
            }
        }
    }
}