﻿using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Api
{
    public static class AudioOutputApi
    {
        private static AudioOutputManager _audioOutputManager = CreateAudioOutputManager();

        private static AudioOutputManager CreateAudioOutputManager()
        {
            return new AudioOutputManager(RepositoryHelper.Repositories.AudioOutputRepository, RepositoryHelper.Repositories.SpeakerSetupRepository, RepositoryHelper.Repositories.IDRepository);
        }

        public static AudioOutput Create()
        {
            return _audioOutputManager.Create();
        }
    }
}