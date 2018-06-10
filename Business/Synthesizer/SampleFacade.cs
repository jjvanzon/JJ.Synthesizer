using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Framework.IO;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
	internal class SampleFacade
	{
		private readonly SampleRepositories _repositories;

		public SampleFacade(SampleRepositories repositories) => _repositories = repositories ?? throw new NullException(() => repositories);

	    // Create

		public SampleInfo CreateSample(byte[] bytes, AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Undefined)
		{
			if (bytes == null) throw new NullException(() => bytes);
			Stream stream = StreamHelper.BytesToStream(bytes);
			return CreateSample(stream, bytes, audioFileFormatEnum);
		}

		// ReSharper disable once UnusedMember.Global
		public SampleInfo CreateSample(Stream stream, AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Undefined)
		{
			if (stream == null) throw new NullException(() => stream);

			stream.Position = 0;
			byte[] bytes = StreamHelper.StreamToBytes(stream);
			return CreateSample(stream, bytes, audioFileFormatEnum);
		}

		// Misc

		/// <summary> Returns a calculator for each channel. </summary>
		// ReSharper disable once UnusedMember.Global
		public IList<ICalculatorWithPosition> CreateCalculators(Sample sample, byte[] bytes)
		{
			IList<ArrayDto> dtos = SampleArrayDtoFactory.CreateArrayDtos(sample, bytes);
			IList<ICalculatorWithPosition> calculators = dtos.Select(ArrayCalculatorFactory.CreateArrayCalculator).ToArray();
			return calculators;
		}

		// Private Methods

		private SampleInfo CreateSample(Stream stream, byte[] bytes, AudioFileFormatEnum audioFileFormatEnum)
		{
			if (stream == null) throw new NullException(() => stream);

			if (audioFileFormatEnum == AudioFileFormatEnum.Undefined)
			{
				audioFileFormatEnum = DetectAudioFileFormat(stream, bytes);
			}

			switch (audioFileFormatEnum)
			{
				case AudioFileFormatEnum.Wav:
					return CreateWavSample(stream, bytes);

				case AudioFileFormatEnum.Raw:
					return CreateRawSample(bytes);

				default:
					throw new ValueNotSupportedException(audioFileFormatEnum);
			}
		}

		private AudioFileFormatEnum DetectAudioFileFormat(Stream stream, byte[] bytes)
		{
			// Detect wav header
			if (bytes.Length >= WavHeaderConstants.WAV_HEADER_LENGTH)
			{
				stream.Position = 0;
				var reader = new BinaryReader(stream);
				var wavHeaderStruct = reader.ReadStruct<WavHeaderStruct>();

				IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
				if (validator.IsValid)
				{
					return AudioFileFormatEnum.Wav;
				}
			}

			return AudioFileFormatEnum.Raw;
		}

		private SampleInfo CreateWavSample(Stream stream, byte[] bytes)
		{
			if (bytes.Length < WavHeaderConstants.WAV_HEADER_LENGTH)
			{
				throw new Exception($"A WAV file must be at least {WavHeaderConstants.WAV_HEADER_LENGTH} bytes.");
			}

			// Read header
			stream.Position = 0;
			var reader = new BinaryReader(stream);
			var wavHeaderStruct = reader.ReadStruct<WavHeaderStruct>();
			stream.Position = 0;

			// Create Sample
			Sample sample = CreateWavSampleFromHeader(wavHeaderStruct);
			_repositories.SampleRepository.SetBytes(sample.ID, bytes);

			return new SampleInfo
			{
				Sample = sample,
				Bytes = bytes
			};
		}

		private Sample CreateWavSampleFromHeader(WavHeaderStruct wavHeaderStruct)
		{
			AudioFileInfo audioFileInfo = WavHeaderFacade.GetAudioFileInfoFromWavHeaderStruct(wavHeaderStruct);

			Sample sample = CreateSampleEntity();

			sample.SetAudioFileFormatEnum(AudioFileFormatEnum.Wav, _repositories.AudioFileFormatRepository);

			sample.SamplingRate = audioFileInfo.SamplingRate;

			switch (audioFileInfo.ChannelCount)
			{
				case 1:
					sample.SetSpeakerSetupEnum(SpeakerSetupEnum.Mono, _repositories.SpeakerSetupRepository);
					break;

				case 2:
					sample.SetSpeakerSetupEnum(SpeakerSetupEnum.Stereo, _repositories.SpeakerSetupRepository);
					break;

				default:
					throw new Exception($"audioFileInfo.ChannelCount value '{audioFileInfo.ChannelCount}' not supported.");
			}

			switch (audioFileInfo.BytesPerValue)
			{
				case 1:
					sample.SetSampleDataTypeEnum(SampleDataTypeEnum.Byte, _repositories.SampleDataTypeRepository);
					break;

				case 2:
					sample.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _repositories.SampleDataTypeRepository);
					break;

				default:
					throw new Exception($"audioFileInfo.BytesPerValue value '{audioFileInfo.BytesPerValue}' not supported.");
			}

			return sample;
		}

		private SampleInfo CreateRawSample(byte[] bytes)
		{
			Sample sample = CreateSampleEntity();
			sample.SetAudioFileFormatEnum(AudioFileFormatEnum.Raw, _repositories.AudioFileFormatRepository);
			_repositories.SampleRepository.SetBytes(sample.ID, bytes);

			return new SampleInfo
			{
				Sample = sample,
				Bytes = bytes
			};
		}

		/// <summary> Creates a Sample and sets its defaults. </summary>
		private Sample CreateSampleEntity()
		{
			var sample = new Sample { ID = _repositories.IDRepository.GetID() };
			_repositories.SampleRepository.Insert(sample);

			new Sample_SideEffect_SetDefaults(sample, _repositories).Execute();

			return sample;
		}
	}
}