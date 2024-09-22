using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Framework.Common;
using System.IO;
using JJ.Framework.IO;

namespace JJ.Business.Synthesizer.Managers
{
	/// <summary> I wish these things were in JJ.Synthesizer </summary>
	public static class WavHeaderWishes
	{
		public static void WriteWavHeader(
			this BinaryWriter bw,
			SampleDataTypeEnum sampleDataTypeEnum,
			SpeakerSetupEnum speakerSetupEnum,
			int samplingRate,
			int sampleCount) 
			=> WriteHeader(bw, sampleDataTypeEnum, speakerSetupEnum, samplingRate, sampleCount);

		public static void WriteHeader(
			BinaryWriter bw,
			SampleDataTypeEnum sampleDataTypeEnum,
			SpeakerSetupEnum speakerSetupEnum,
			int samplingRate,
			int sampleCount)
		{
			var audioFileInfo = new AudioFileInfo
			{
				SamplingRate = samplingRate,
				SampleCount = sampleCount,
				BytesPerValue = SampleDataTypeHelper.SizeOf(sampleDataTypeEnum),
				ChannelCount = speakerSetupEnum.GetChannelCount()
			};

			var wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);

			bw.WriteStruct(wavHeaderStruct);
		}

		public static int GetChannelCount(this SpeakerSetupEnum speakerSetupEnum)
		{
			switch (speakerSetupEnum)
			{
				case SpeakerSetupEnum.Mono: return 1;
				case SpeakerSetupEnum.Stereo: return 2;
				default: throw new ValueNotSupportedException(speakerSetupEnum);
			};
		}
	}
}