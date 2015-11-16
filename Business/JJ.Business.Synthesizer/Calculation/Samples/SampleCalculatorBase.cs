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
    /// <summary>
    /// Use the pre-calculated fields of the base class, when deriving from this class.
    /// There is null-tollerance towards the byte[],
    /// because it is considered optional in the entity model.
    /// You are playing around with data a lot and simply a not-loaded sample is only warning,
    /// so should not block starting calculations.
    /// </summary>
    internal abstract class SampleCalculatorBase : ISampleCalculator
    {
        protected Sample _sample;
        protected byte[] _bytes;
        /// <summary> SamplingRate divided by TimeMultiplier </summary>
        protected double _rate;
        protected double[,] _samples;

        /// <summary> For performance, so we can use this value directly. </summary>
        public int ChannelCount { get; private set; }

        public SampleCalculatorBase(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.TimeMultiplier == 0) throw new ZeroException(() => sample.TimeMultiplier);
            if (bytes == null) throw new Exception("bytes cannot be null. A null byte array can only be handled by a Zero_SampleCalculator.");
            if (!sample.IsActive) throw new Exception("sample.IsActive cannot be false, because it needs to be handled by a Zero_SampleCalculator.");

            IValidator validator = new SampleValidator(sample);
            validator.Verify();

            _sample = sample;
            _bytes = bytes;
            _rate = _sample.SamplingRate / _sample.TimeMultiplier;

            ChannelCount = _sample.GetChannelCount();

            _samples = ReadSamples(_sample, _bytes);
        }

        private double[,] ReadSamples(Sample sample, byte[] bytes)
        {
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
            for (int i = 0; i < ChannelCount; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    double d = doubles[k];
                    samples[i, j] = d * amplifier;

                    k++;
                }
            }

            return samples;
        }

        protected abstract double ReadValue(BinaryReader binaryReader);

        public abstract double CalculateValue(double time, int channelIndex);
    }
}
