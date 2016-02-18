using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using System.IO;
using JJ.Framework.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    // TODO: Remove handling of extra sample count, because this is already handled by the ArrayCalculator.

    /// <summary> Use the pre-calculated fields of the base class, when deriving from this class. </summary>
    internal abstract class SampleCalculatorBase : ISampleCalculator
    {
        /// <summary> SamplingRate divided by TimeMultiplier </summary>
        protected readonly double _rate;

        /// <summary> First index is channel, second index is tick. </summary>
        protected readonly double[][] _samples;

        public SampleCalculatorBase(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.TimeMultiplier == 0.0) throw new ZeroException(() => sample.TimeMultiplier);
            if (!sample.IsActive) throw new Exception("sample.IsActive cannot be false, because it needs to be handled by a Zero_SampleCalculator.");
            if (bytes == null) throw new Exception("bytes cannot be null. A null byte array can only be handled by a Zero_SampleCalculator.");

            IValidator validator = new SampleValidator(sample);
            validator.Assert();

            _rate = sample.SamplingRate / sample.TimeMultiplier;

            _samples = ReadSamples(sample, bytes);
        }

        private double[][] ReadSamples(Sample sample, byte[] bytes)
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
                        double d = ReadValue(reader);
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

        protected abstract double ReadValue(BinaryReader binaryReader);

        public abstract double CalculateValue(double time, int channelIndex);
    }
}
