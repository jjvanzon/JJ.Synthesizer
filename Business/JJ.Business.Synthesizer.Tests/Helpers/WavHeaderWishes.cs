using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Framework.Common;
using System.IO;
using JJ.Framework.IO;

namespace JJ.Business.Synthesizer.Managers
{
	public static class WavHeaderWishes
	{
		/// <summary> I wish this was in the WavHeaderManager. </summary>
		public static void WriteWavHeader(
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

		/// <summary> I wish this extension existed in JJ.Synthesizer. </summary>
		private static int GetChannelCount(this SpeakerSetupEnum speakerSetupEnum)
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