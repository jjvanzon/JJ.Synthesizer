using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Document_SideEffect_AutoCreateAudioOutput : ISideEffect
    {
        private readonly Document _document;
        private readonly AudioOutputManager _audioOutputManager;

        public Document_SideEffect_AutoCreateAudioOutput(
            Document document,
            IAudioOutputRepository audioOutputRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IIDRepository idRepository)
        {
            _document = document ?? throw new NullException(() => document);

            _audioOutputManager = new AudioOutputManager(
                audioOutputRepository, 
                speakerSetupRepository, 
                idRepository);
        }

        public void Execute()
        {
            if (_document.AudioOutput == null)
            {
                _audioOutputManager.Create(_document);
            }
        }
    }
}
