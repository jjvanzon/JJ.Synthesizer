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
    /// <summary> Use the pre-calculated fields of the base class, when deriving from this class. </summary>
    internal abstract class SampleCalculatorBase : ISampleCalculator
    {
        private readonly Sample _sample;
        private readonly byte[] _bytes;

        /// <summary> SamplingRate divided by TimeMultiplier </summary>
        protected readonly double _rate;
        protected readonly double _duration;
        protected readonly double _valueBefore;
        protected readonly double _valueAfter;

        /// <summary>
        /// NOTE: Length does not have to correspond to _duration.
        /// The array is one sample longer,
        /// for interpolation, for not worrying about floating point imprecision,
        /// and to prevent checks in real-time.
        /// Beware of using _samples.Length.
        /// Use the other fields instead.
        /// </summary>
        protected readonly double[,] _samples;

        /// <summary> For performance, so we can use this value directly. </summary>
        public int ChannelCount { get; private set; }

        /// <param name="extraSampleCount">
        /// You can include extra 0-valued samples 
        /// for interpolation, not worrying about imprecision,
        /// and to prevent checks in real-time.
        /// </param>
        public SampleCalculatorBase(Sample sample, byte[] bytes, int extraSampleCount)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.TimeMultiplier == 0.0) throw new ZeroException(() => sample.TimeMultiplier);
            if (!sample.IsActive) throw new Exception("sample.IsActive cannot be false, because it needs to be handled by a Zero_SampleCalculator.");
            if (bytes == null) throw new Exception("bytes cannot be null. A null byte array can only be handled by a Zero_SampleCalculator.");
            if (extraSampleCount < 0) throw new LessThanException(() => extraSampleCount, 0);

            IValidator validator = new SampleValidator(sample);
            validator.Assert();

            _sample = sample;
            _bytes = bytes;
            _rate = _sample.SamplingRate / _sample.TimeMultiplier;

            ChannelCount = _sample.GetChannelCount();

            _samples = ReadSamples(_sample, _bytes, extraSampleCount);

            int sampleCount = _samples.Length - extraSampleCount;

            _duration = sampleCount / _rate / ChannelCount;
        }

        private double[,] ReadSamples(Sample sample, byte[] bytes, int extraSampleCount)
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
            int sampleCount = doubles.Length / ChannelCount;

            double[,] samples = new double[ChannelCount, sampleCount + extraSampleCount];

            int k = 0;
            for (int i = 0; i < ChannelCount; i++)
            {
                for (int j = 0; j < sampleCount; j++)
                {
                    double d = doubles[k];
                    samples[i, j] = d * amplifier;

                    k++;
                }

                // Add extra samples.
                for (int j = sampleCount; j < sampleCount + extraSampleCount; j++)
                {
                    samples[i, j] = 0.0;
                }
            }

            return samples;
        }

        protected abstract double ReadValue(BinaryReader binaryReader);

        public abstract double CalculateValue(double time, int channelIndex);
    }
}
