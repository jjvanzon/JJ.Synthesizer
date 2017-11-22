using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions;
using JJ.Framework.IO;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
	internal abstract class AudioFileOutputCalculatorBase : IAudioFileOutputCalculator
	{
		private static readonly int _valueCountPerChunk = GetValueCountPerChunk();

		private readonly IPatchCalculator[] _patchCalculators;

		public AudioFileOutputCalculatorBase(IList<IPatchCalculator> patchCalculators)
		{
			_patchCalculators = patchCalculators?.ToArray() ?? throw new NullException(() => patchCalculators);
		}

		protected abstract double GetAmplifierAdjustedToSampleDataType(AudioFileOutput audioFileOutput);
		protected abstract void WriteValue(BinaryWriter binaryWriter, double value);

		public void WriteFile(AudioFileOutput audioFileOutput)
		{
			// Assert
			if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
			if (string.IsNullOrEmpty(audioFileOutput.FilePath)) throw new NullOrEmptyException(() => audioFileOutput.FilePath);
			int channelCount = audioFileOutput.GetChannelCount();
			if (_patchCalculators.Length != channelCount) throw new NotEqualException(() => _patchCalculators.Length, audioFileOutput.GetChannelCount());
			IValidator validator = new AudioFileOutputValidator(audioFileOutput);
			validator.Assert();

			// Prepare some variables
			double startTime = audioFileOutput.StartTime;
			double endTime = audioFileOutput.GetEndTime();
			double duration = audioFileOutput.Duration;
			double frameDuration = 1.0 / audioFileOutput.SamplingRate / audioFileOutput.TimeMultiplier;
			int frameCount = (int)(duration / frameDuration);
			int valueCount = frameCount * channelCount;
			int valueCountPerChunk = _valueCountPerChunk;

			// Calculate output and write file
			using (Stream stream = new FileStream(audioFileOutput.FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (var writer = new BinaryWriter(stream))
				{
					// Write header
					AudioFileFormatEnum audioFileFormatEnum = audioFileOutput.GetAudioFileFormatEnum();
					switch (audioFileFormatEnum)
					{
						case AudioFileFormatEnum.Wav:
							var audioFileInfo = new AudioFileInfo
							{
								SamplingRate = audioFileOutput.SamplingRate,
								BytesPerValue = SampleDataTypeHelper.SizeOf(audioFileOutput.SampleDataType),
								ChannelCount = channelCount,
								FrameCount = frameCount
							};

							WavHeaderStruct wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);
							writer.WriteStruct(wavHeaderStruct);
							break;

						case AudioFileFormatEnum.Raw:
							// Do nothing
							break;

						default:
							throw new ValueNotSupportedException(audioFileFormatEnum);
					}

					double adjustedAmplifier = GetAmplifierAdjustedToSampleDataType(audioFileOutput);

					// Write Samples
					var buffer = new float[valueCountPerChunk];
					int frameCountPerChunk = valueCountPerChunk / channelCount;
					double chunkDuration = frameDuration * frameCountPerChunk;

					int valueCounter = 0;

					for (double chunkStartTime = startTime; chunkStartTime <= endTime; chunkStartTime += chunkDuration)
					{
						Array.Clear(buffer, 0, buffer.Length);

						for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
						{
							_patchCalculators[channelIndex].Calculate(buffer, frameCountPerChunk, chunkStartTime);
						}

						// Post-process and write values
						for (int j = 0; j < valueCountPerChunk; j++)
						{
							if (valueCounter == valueCount)
							{
								break;
							}

							float floatValue = buffer[j];

							double doubleValue = floatValue;
							doubleValue = doubleValue * adjustedAmplifier;
							WriteValue(writer, doubleValue);

							valueCounter++;
						}
					}
				}
			}
		}

		private static int GetValueCountPerChunk()
		{
			int bufferSizeInBytes = CustomConfigurationManager.GetSection<ConfigurationSection>().AudioFileOutputBufferSizeInBytes;
			int valueCountPerChunk = bufferSizeInBytes / sizeof(float);
			return valueCountPerChunk;
		}

	}
}
