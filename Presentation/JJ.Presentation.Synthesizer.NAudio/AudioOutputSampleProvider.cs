using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using JJ.Business.Synthesizer.Calculation.Patches;
using System.Threading;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation;
using JJ.Data.Synthesizer;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class AudioOutputSampleProvider : ISampleProvider
    {
        private readonly WaveFormat _waveFormat;
        private readonly IPatchCalculatorContainer _patchCalculatorContainer;
        private readonly double _sampleDuration;

        /// <summary> Public field for performance. </summary>
        public double _time;
        public volatile bool _isRunning;

        public AudioOutputSampleProvider(IPatchCalculatorContainer patchCalculatorContainer, AudioOutput audioOutput)
        {
            if (patchCalculatorContainer == null) throw new NullException(() => patchCalculatorContainer);
            if (audioOutput == null) throw new NullException(() => audioOutput);
            if (audioOutput.SamplingRate == 0) throw new ZeroException(() => audioOutput.SamplingRate);

            _patchCalculatorContainer = patchCalculatorContainer;
            _sampleDuration = 1.0 / (double)audioOutput.SamplingRate;

            _waveFormat = CreateWaveFormat(audioOutput);
        }

        private WaveFormat CreateWaveFormat(AudioOutput audioOutput)
        {
            WaveFormat waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(
                audioOutput.SamplingRate, 
                audioOutput.SpeakerSetup.SpeakerSetupChannels.Count);

            return waveFormat;
        }

        WaveFormat ISampleProvider.WaveFormat
        {
            get { return _waveFormat; }
        }

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

                double[] values = patchCalculator.Calculate(_time, _sampleDuration, count, new DimensionStack());

                for (int i = offset; i < count; i++)
                {
                    double value = values[i];

                    // winmm will trip over NaN.
                    if (Double.IsNaN(value))
                    {
                        value = 0;
                    }

                    buffer[i] = (float)value;
                }

                _time += _sampleDuration * count;

                return count;
            }
            finally
            {
                lck.ExitReadLock();
            }
        }
    }
}