using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Converters
{
	internal static class WavHeaderStructToAudioFileInfoConverter
	{
		public static AudioFileInfo Convert(WavHeaderStruct wavHeaderStruct)
		{
			if (wavHeaderStruct.ChannelCount == 0) throw new ZeroException(() => wavHeaderStruct.ChannelCount);
			if (wavHeaderStruct.BitsPerValue == 0) throw new ZeroException(() => wavHeaderStruct.BitsPerValue);

			var audioFileInfo = new AudioFileInfo
			{
				BytesPerValue = wavHeaderStruct.BitsPerValue / 8,
				ChannelCount = wavHeaderStruct.ChannelCount,
				SamplingRate = wavHeaderStruct.SamplingRate,
				FrameCount = wavHeaderStruct.SubChunk2Size / wavHeaderStruct.ChannelCount / (wavHeaderStruct.BitsPerValue / 8)
			};

			return audioFileInfo;
		}
	}
}