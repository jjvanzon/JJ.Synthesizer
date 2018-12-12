using System;
using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Framework.IO;

namespace JJ.Business.Synthesizer.Calculation
{
	internal static class SampleReader
	{
		private const double BYTE_VALUE_DIVIDER = 128.0;
		private const double INT16_VALUE_DIVIDER = -short.MinValue;

		/// <summary> In the returned array, the first array index is channel index, the second array index is the frame. </summary>
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

		/// <summary> In the returned array, the first array index is channel index, the second array index is the frame. </summary>
		private static double[][] ReadInt16Samples(Sample sample, byte[] bytes) => ReadSamplesTemplateMethod(sample, bytes, ReadInt16Value);

	    /// <summary> In the returned array, the first array index is channel index, the second array index is the frame. </summary>
		private static double ReadInt16Value(BinaryReader binaryReader)
		{
			short int16 = binaryReader.ReadInt16();
			// ReSharper disable once RedundantCast
			double value = (double)int16 / INT16_VALUE_DIVIDER;
			return value;
		}

		/// <summary> In the returned array, the first array index is channel index, the second array index is the frame. </summary>
		private static double[][] ReadByteSamples(Sample sample, byte[] bytes) => ReadSamplesTemplateMethod(sample, bytes, ReadByteValue);

	    private static double ReadByteValue(BinaryReader binaryReader)
		{
			byte b = binaryReader.ReadByte();
			double value = b - 128.0;
			value /= BYTE_VALUE_DIVIDER;
			return value;
		}

		/// <summary> In the returned array, the first array index is channel index, the second array index is the frame. </summary>
		private static double[][] ReadSamplesTemplateMethod(
			Sample sample, 
			byte[] bytes, 
			Func<BinaryReader, double> readValueDelegate)
		{
			int channelCount = sample.GetChannelCount();
			int bytesPerValue = sample.SampleDataType.SizeOf();
			double amplifier = sample.Amplifier;

			// First read out the doubles.
			using (Stream stream = StreamHelper.BytesToStream(bytes))
			{
				double[] doubles;
				using (var reader = new BinaryReader(stream))
				{
					int headerLength;
					long dataLength;

					AudioFileFormatEnum audioFileFormatEnum = sample.GetAudioFileFormatEnum();
					switch (audioFileFormatEnum)
					{
						case AudioFileFormatEnum.Raw:
							headerLength = 0;
							dataLength = stream.Length;
							break;

						case AudioFileFormatEnum.Wav:
							var wavHeaderStruct = reader.ReadStruct<WavHeaderStruct>();
							AudioFileInfo audioFileInfo = WavHeaderFacade.GetAudioFileInfoFromWavHeaderStruct(wavHeaderStruct);
							headerLength = WavHeaderConstants.WAV_HEADER_LENGTH;
							dataLength = audioFileInfo.FrameCount * audioFileInfo.ChannelCount * audioFileInfo.BytesPerValue;
							break;

						default:
							throw new ValueNotSupportedException(audioFileFormatEnum);
					}

					dataLength -= sample.BytesToSkip;

					// Correct for faulty stuff in the file.
					long maxDataLengthThatFitsInStream = stream.Length - headerLength - sample.BytesToSkip;
					if (dataLength > maxDataLengthThatFitsInStream)
					{
						dataLength = maxDataLengthThatFitsInStream;
					}

					long valueCount = dataLength / bytesPerValue;
					doubles = new double[valueCount];

					// Correct for faulty stuff in the file:
					// If sample does not exactly contain a multiple of the sample size,
					// do not read the whole stream, but just until the last possible sample.
					dataLength = doubles.Length * bytesPerValue;
					long lengthToRead = headerLength + sample.BytesToSkip + dataLength;

					// Skip header
					stream.Position += headerLength;
					stream.Position += sample.BytesToSkip;

					var i = 0;
					while (stream.Position < lengthToRead)
					{
						double d = readValueDelegate(reader);
						doubles[i] = d;
						i++;
					}
				}

				// Then split the doubles into channels.
				int frameCount = doubles.Length / channelCount;

				// ReSharper disable once SuggestVarOrType_Elsewhere
				double[][] samples = new double[channelCount][];
				for (var channelIndex = 0; channelIndex < channelCount; channelIndex++)
				{
					samples[channelIndex] = new double[frameCount];
				}

				var valueIndex = 0;
				for (var frameIndex = 0; frameIndex < frameCount; frameIndex++)
				{
					for (var channelIndex = 0; channelIndex < channelCount; channelIndex++)
					{
						double d = doubles[valueIndex];
						samples[channelIndex][frameIndex] = d * amplifier;

						valueIndex++;
					}
				}

				return samples;
			}
		}
	}
}