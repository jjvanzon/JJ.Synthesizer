﻿using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using System.IO;
using JJ.Framework.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class SampleCalculatorBase : ISampleCalculator
    {
        protected Sample _sample;
        protected double _rate;
        protected double[,] _samples;

        /// <summary>
        /// For performance, so we can use this value directly. 
        /// </summary>
        public int ChannelCount { get; private set; }

        public SampleCalculatorBase(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.TimeMultiplier == 0) throw new Exception("sample.TimeMultiplier cannot be 0.");

            _sample = sample;
            _rate = _sample.SamplingRate / _sample.TimeMultiplier;

            ChannelCount = _sample.GetChannelCount();

            _samples = ReadSamples(sample);
        }

        private double[,] ReadSamples(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            int bytesPerSample = SampleDataTypeHelper.SizeOf(sample.SampleDataType);
            double amplifier = sample.Amplifier;
            double[] doubles;

            // First read out the doubles.
            using (Stream stream = StreamHelper.BytesToStream(sample.Bytes))
            {
                // Skip header
                AudioFileFormatEnum audioFileFormatEnum = sample.GetAudioFileFormatEnum();
                switch (audioFileFormatEnum)
                {
                    case AudioFileFormatEnum.Raw:
                        break;

                    case AudioFileFormatEnum.Wav:
                        stream.Position = WavHeaderConstants.WAV_HEADER_LENGTH;
                        break;

                    default:
                        throw new ValueNotSupportedException(audioFileFormatEnum);
                }

                stream.Position += sample.BytesToSkip;

                doubles = new double[stream.Length / bytesPerSample];

                // For tollerance if sample does not exactly contain a multiple of the sample size,
                // do not read the whole stream, but just until the last possible sample.
                int lengthToRead = doubles.Length * bytesPerSample;

                using (var reader = new BinaryReader(stream))
                {
                    int i = 0;
                    while (stream.Position < lengthToRead)
                    {
                        double d = ReadValue(reader);
                        doubles[i] = d;
                        i++;
                    }
                }
            }

            // Then split the doubles into channels.
            long count = doubles.Length / ChannelCount;
            double[,] samples = new double[ChannelCount, count];

            int k = 0;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < ChannelCount; j++)
                {
                    double d = doubles[k];
                    samples[j, i] = d * amplifier;

                    k++;
                }
            }

            return samples;
        }

        protected abstract double ReadValue(BinaryReader binaryReader);

        public abstract double CalculateValue(int channelIndex, double time);
    }
}
