using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Api
{
    public static class AudioFileOutputApi
    {
        private static readonly AudioFileOutputManager _audioFileOutputManager = CreateAudioFileOutputManager();

        private static AudioFileOutputManager CreateAudioFileOutputManager()
        {
            return new AudioFileOutputManager(RepositoryHelper.AudioFileOutputRepositories);
        }
        
        public static AudioFileOutput CreateWithRelatedEntities()
        {
            return _audioFileOutputManager.Create();
        }

        public static void WriteFile(AudioFileOutput audioFileOutput, params IPatchCalculator[] patchCalculators)
        {
            _audioFileOutputManager.WriteFile(audioFileOutput, patchCalculators);
        }
    }
}
