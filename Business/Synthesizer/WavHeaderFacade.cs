using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer
{
	public static class WavHeaderFacade
	{
		public static WavHeaderStruct CreateWavHeaderStruct(AudioFileInfo audioFileInfo)
		{
			WavHeaderStruct wavHeaderStruct = AudioFileInfoToWavHeaderStructConverter.Convert(audioFileInfo);

			new WavHeaderStructValidator(wavHeaderStruct).Assert();

			return wavHeaderStruct;
		}

		public static AudioFileInfo GetAudioFileInfoFromWavHeaderStruct(WavHeaderStruct wavHeaderStruct)
		{
			new WavHeaderStructValidator(wavHeaderStruct).Assert();

			AudioFileInfo audioFileInfo = WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct);
			return audioFileInfo;
		}
	}
}
