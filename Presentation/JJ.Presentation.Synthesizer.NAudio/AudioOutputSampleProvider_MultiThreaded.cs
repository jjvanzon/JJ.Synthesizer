using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using JJ.Business.Synthesizer.Calculation.Patches;
using System.Threading;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class AudioOutputSampleProvider_MultiThreaded : ISampleProvider
    {
        private const int DEFAULT_SAMPLE_RATE = 44100;
        private const int DEFAULT_CHANNEL_COUNT = 1;
        private const int DEFAULT_CHANNEL_INDEX = 0; // TODO: Make multi-channel later.
        private const double SAMPLE_DURATION = 1.0 / DEFAULT_SAMPLE_RATE;

        private static WaveFormat _waveFormat = CreateWaveFormat();

        public double _time;
        public volatile bool _isRunning;

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
            ReaderWriterLockSlim lck = PolyphonyCalculatorContainer.Lock;

            lck.EnterReadLock();
            try
            {
                PolyphonyCalculator calculator = PolyphonyCalculatorContainer.Calculator;

                if (!_isRunning || calculator == null)
                {
                    Array.Clear(buffer, 0, buffer.Length);
                    return count;
                }

                double[] values = calculator.Calculate(_time, DEFAULT_CHANNEL_INDEX);

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

                _time += SAMPLE_DURATION * count;

                return count;
            }
            finally
            {
                lck.ExitReadLock();
            }
        }
    }
}