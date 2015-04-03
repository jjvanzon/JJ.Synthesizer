using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Managers
{
    public static class WavHeaderManager
    {
        public static WavHeaderStruct CreateWavHeaderStruct(AudioFileInfo audioFileInfo)
        {
            if (audioFileInfo == null) throw new NullException(() => audioFileInfo);

            WavHeaderStruct wavHeaderStruct = AudioFileInfoToWavHeaderStructConverter.Convert(audioFileInfo);

            IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
            validator.Verify();

            return wavHeaderStruct;
        }

        public static AudioFileInfo GetAudioFileInfoFromWavHeaderStruct(WavHeaderStruct wavHeaderStruct)
        {
            IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
            validator.Verify();

            AudioFileInfo audioFileInfo = WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct);
            return audioFileInfo;
        }
    }
}
