using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using JJ.Business.Synthesizer.Calculation.Patches;
using System.Threading;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class AudioOutputSampleProvider : ISampleProvider
    {
        private readonly WaveFormat _waveFormat;
        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly double _frameDuration;
        private readonly int _channelCount;

        /// <summary> Public field for performance. </summary>
        public double _time;
        public volatile bool _isRunning;

        public AudioOutputSampleProvider(IPatchCalculatorContainer patchCalculatorContainer, AudioOutput audioOutput)
        {
            if (patchCalculatorContainer == null) throw new NullException(() => patchCalculatorContainer);
            if (audioOutput == null) throw new NullException(() => audioOutput);

            _patchCalculatorContainer = patchCalculatorContainer;
            _frameDuration = audioOutput.GetFrameDuration();
            _channelCount = audioOutput.SpeakerSetup.SpeakerSetupChannels.Count;
            _waveFormat = CreateWaveFormat(audioOutput);
        }

        private WaveFormat CreateWaveFormat(AudioOutput audioOutput)
        {
            WaveFormat waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(
                audioOutput.SamplingRate, 
                audioOutput.SpeakerSetup.SpeakerSetupChannels.Count);

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

                if (!_isRunning || patchCalculator == null)
                {
                    Array.Clear(buffer, 0, buffer.Length);
                    return count;
                }

                int frameCount = count / _channelCount;

                patchCalculator.Calculate(buffer, frameCount, _time);

                _time += _frameDuration * frameCount;

                return count;
            }
            finally
            {
                lck.ExitReadLock();
            }
        }
    }
}