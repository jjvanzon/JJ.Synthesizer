using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;
using JJ.Framework.IO;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class SampleCalculatorHelper
    {
        private const double BYTE_VALUE_DIVIDER = 128.0;
        private const double INT16_VALUE_DIVIDER = (double)-short.MinValue;

        public static double[][] ReadSamples(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);
            if (bytes == null) throw new NullException(() => bytes);

            SampleDataTypeEnum sampleDataTypeEnum = sample.GetSampleDataTypeEnum();

            switch (sampleDataTypeEnum)
            {
                case SampleDataTypeEnum.Byte:
                    return ReadByteSamples(sample, bytes);

                case SampleDataTypeEnum.Int16:
                    return ReadInt16Samples(sample, bytes);

                default:
                    throw new ValueNotSupportedException(sampleDataTypeEnum);
            }
        }

        private static double[][] ReadInt16Samples(Sample sample, byte[] bytes)
        {
            return ReadSamplesTemplateMethod(sample, bytes, ReadInt16Value);
        }

        private static double ReadInt16Value(BinaryReader binaryReader)
        {
            short shrt = binaryReader.ReadInt16();
            double value = (double)shrt / INT16_VALUE_DIVIDER;
            return value;
        }

        private static double[][] ReadByteSamples(Sample sample, byte[] bytes)
        {
            return ReadSamplesTemplateMethod(sample, bytes, ReadByteValue);
        }

        private static double ReadByteValue(BinaryReader binaryReader)
        {
            byte b = binaryReader.ReadByte();
            double value = b - 128.0;
            value /= BYTE_VALUE_DIVIDER;
            return value;
        }

        private static double[][] ReadSamplesTemplateMethod(
            Sample sample, byte[] bytes, Func<BinaryReader, double> readValueDelegate)
        {
            int channelCount = sample.GetChannelCount();
            int bytesPerSample = SampleDataTypeHelper.SizeOf(sample.SampleDataType);
            double amplifier = sample.Amplifier;
            double[] doubles;

            // First read out the doubles.
            using (Stream stream = StreamHelper.BytesToStream(bytes))
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

                long valueCount = stream.Length / bytesPerSample;
                doubles = new double[valueCount];

                // For tollerance if sample does not exactly contain a multiple of the sample size,
                // do not read the whole stream, but just until the last possible sample.
                int lengthToRead = doubles.Length * bytesPerSample;

                using (var reader = new BinaryReader(stream))
                {
                    int i = 0;
                    while (stream.Position < lengthToRead)
                    {
                        double d = readValueDelegate(reader);
                        doubles[i] = d;
                        i++;
                    }
                }
            }

            // Then split the doubles into channels.
            int sampleCount = doubles.Length / channelCount;

            double[][] samples = new double[channelCount][];

            int k = 0;
            for (int i = 0; i < channelCount; i++)
            {
                samples[i] = new double[sampleCount];

                for (int j = 0; j < sampleCount; j++)
                {
                    double d = doubles[k];
                    samples[i][j] = d * amplifier;

                    k++;
                }
            }

            return samples;
        }
    }
}
