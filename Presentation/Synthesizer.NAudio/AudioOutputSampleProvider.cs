using System;
using NAudio.Wave;
using JJ.Business.Synthesizer.Calculation.Patches;
using System.Threading;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class AudioOutputSampleProvider : ISampleProvider
    {
        private readonly WaveFormat _waveFormat;
        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly double _frameDuration;
        private readonly int _channelCount;

        public bool IsRunning { get; set; }

        public AudioOutputSampleProvider(IPatchCalculatorContainer patchCalculatorContainer, AudioOutput audioOutput)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (audioOutput == null) throw new NullException(() => audioOutput);
            
            _patchCalculatorContainer = patchCalculatorContainer ?? throw new NullException(() => patchCalculatorContainer);
            _frameDuration = audioOutput.GetFrameDuration();
            _channelCount = audioOutput.GetChannelCount();
            _waveFormat = CreateWaveFormat(audioOutput);
        }

        private WaveFormat CreateWaveFormat(AudioOutput audioOutput)
        {
            WaveFormat waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(
                audioOutput.SamplingRate, 
                audioOutput.GetChannelCount());

            return waveFormat;
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