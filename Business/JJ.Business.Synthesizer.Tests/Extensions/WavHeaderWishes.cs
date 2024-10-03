using System;
using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Common;
using JJ.Framework.IO;

namespace JJ.Business.Synthesizer.Tests.Extensions
{
	/// <summary> I wish these things were in JJ.Synthesizer </summary>
	public static class WavHeaderWishes
	{
		// WriteWavHeader

		/// <summary> Overload that takes &lt;TSampleDataType&gt; and SpeakerSetupEnum. </summary>
		public static void WriteWavHeader<TSampleDataType>(
			this BinaryWriter bw,
			SpeakerSetupEnum speakerSetupEnum,
			int samplingRate,
			int sampleCount)
			=> WriteWavHeader(bw, GetSampleDataTypeEnum<TSampleDataType>(),
							  speakerSetupEnum, samplingRate, sampleCount);

		/// <summary> Overload that takes &lt;TSampleDataType&gt;. </summary>
		public static void WriteWavHeader<TSampleDataType>(
			this BinaryWriter bw,
			int channelCount,
			int samplingRate,
			int sampleCount)
			=> WriteWavHeader(bw, GetSampleDataTypeEnum<TSampleDataType>(), 
							  channelCount, samplingRate, sampleCount);

		/// <summary> Overload that takes SpeakerSetupEnum. </summary>
		public static void WriteWavHeader(
			this BinaryWriter bw,
			SampleDataTypeEnum sampleDataTypeEnum,
			SpeakerSetupEnum speakerSetupEnum,
			int samplingRate,
			int sampleCount)
			=> WriteWavHeader(bw, sampleDataTypeEnum, speakerSetupEnum.ToChannelCount(), samplingRate, sampleCount);

		public static void WriteWavHeader(
			this BinaryWriter bw,
			SampleDataTypeEnum sampleDataTypeEnum,
			int channelCount,
			int samplingRate,
			int sampleCount)
		{
			var audioFileInfo = new AudioFileInfo
			{
				SamplingRate = samplingRate,
				SampleCount = sampleCount,
				BytesPerValue = SampleDataTypeHelper.SizeOf(sampleDataTypeEnum),
				ChannelCount = channelCount
			};

			var wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);

			bw.WriteStruct(wavHeaderStruct);
		}

		// Conversions

		public static int ToChannelCount(this SpeakerSetupEnum speakerSetupEnum)
		{
			switch (speakerSetupEnum)
			{
				case SpeakerSetupEnum.Mono: return 1;
				case SpeakerSetupEnum.Stereo: return 2;
				default: throw new ValueNotSupportedException(speakerSetupEnum);
			};
		}

		public static SpeakerSetupEnum ToSpeakerSetupEnum(this int channelCount)
		{
			switch (channelCount)
			{
				case 1: return SpeakerSetupEnum.Mono;
				case 2: return SpeakerSetupEnum.Stereo;
				default: throw new ValueNotSupportedException(channelCount);
			};
		}

		public static SampleDataTypeEnum GetSampleDataTypeEnum<TSampleDataType>()
		{
			if (typeof(TSampleDataType) == typeof(Int16))
			{
				return SampleDataTypeEnum.Int16;
			}

			if (typeof(TSampleDataType) == typeof(Byte))
			{
				return SampleDataTypeEnum.Byte;
			}

			throw new ValueNotSupportedException(typeof(TSampleDataType));
		}
	}
}