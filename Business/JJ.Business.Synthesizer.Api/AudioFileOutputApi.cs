using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Business.Synthesizer.Api
{
    public static class AudioFileOutputApi
    {
        private static AudioFileOutputManager _audioFileOutputManager = CreateAudioFileOutputManager();

        private static AudioFileOutputManager CreateAudioFileOutputManager()
        {
            return new AudioFileOutputManager(RepositoryHelper.AudioFileOutputRepositories);
        }
        
        public static AudioFileOutput CreateWithRelatedEntities()
        {
            return _audioFileOutputManager.CreateWithRelatedEntities();
        }

        public static void WriteFile(AudioFileOutput audioFileOutput, IPatchCalculator patchCalculator)
        {
            _audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
        }
    }
}
