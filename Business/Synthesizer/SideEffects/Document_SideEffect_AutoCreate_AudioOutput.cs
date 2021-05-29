using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Document_SideEffect_AutoCreate_AudioOutput : ISideEffect
    {
        private readonly Document _document;
        private readonly AudioOutputFacade _audioOutputFacade;

        public Document_SideEffect_AutoCreate_AudioOutput(
            Document document,
            IAudioOutputRepository audioOutputRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IIDRepository idRepository)
        {
            _document = document ?? throw new NullException(() => document);

            _audioOutputFacade = new AudioOutputFacade(audioOutputRepository, speakerSetupRepository, idRepository);
        }

        public void Execute()
        {
            if (_document.AudioOutput == null)
            {
                _audioOutputFacade.CreateWithDefaults(_document);
            }
        }
    }
}
