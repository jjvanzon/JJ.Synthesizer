using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
    public static class WavHeaderManager
    {
        public static WavHeaderStruct CreateWavHeaderStruct(AudioFileInfo audioFileInfo)
        {
            WavHeaderStruct wavHeaderStruct = AudioFileInfoToWavHeaderStructConverter.Convert(audioFileInfo);

            IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
            validator.Assert();

            return wavHeaderStruct;
        }

        public static AudioFileInfo GetAudioFileInfoFromWavHeaderStruct(WavHeaderStruct wavHeaderStruct)
        {
            IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
            validator.Assert();

            AudioFileInfo audioFileInfo = WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct);
            return audioFileInfo;
        }
    }
}
