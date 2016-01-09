using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class SampleProvider : ISampleProvider
    {
        private const int DEFAULT_SAMPLE_RATE = 44100;
        private const int DEFAULT_CHANNEL_COUNT = 1;
        private const int DEFAULT_CHANNEL_INDEX = 0;
        private const double SAMPLE_DURATION = 1.0 / DEFAULT_SAMPLE_RATE;
        private static WaveFormat _waveFormat = CreateWaveFormat();

        public static double _time;
        public static volatile bool _isRunning;

        private static WaveFormat CreateWaveFormat()
        {
            WaveFormat waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(DEFAULT_SAMPLE_RATE, DEFAULT_CHANNEL_COUNT);
            return waveFormat;
        }

        WaveFormat ISampleProvider.WaveFormat
        {
            get { return _waveFormat; }
        }

        int ISampleProvider.Read(float[] buffer, int offset, int count)
        {
            PatchCalculatorContainer.Lock.EnterReadLock();
            try
            {
                IPatchCalculator patchCalculator = PatchCalculatorContainer.PatchCalculator;

                if (!_isRunning || patchCalculator == null)
                {
                    Array.Clear(buffer, 0, buffer.Length);

                    // Recent change (2016-01-09): Time may not be running if it is off.
                    //_time += SAMPLE_DURATION * count;

                    return count;
                }

                for (int i = offset; i < count; i++)
                {
                    double value = patchCalculator.Calculate(_time, DEFAULT_CHANNEL_INDEX);

                    buffer[i] = (float)value;

                    _time += SAMPLE_DURATION;
                }

                return count;
            }
            finally
            {
                PatchCalculatorContainer.Lock.ExitReadLock();
            }
        }
    }
}