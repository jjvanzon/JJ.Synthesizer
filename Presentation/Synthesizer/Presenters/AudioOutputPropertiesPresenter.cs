using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioOutputPropertiesPresenter 
        : EntityPresenterWithSaveBase<AudioOutput, AudioOutputPropertiesViewModel>
    {
        private readonly IAudioOutputRepository _audioOutputRepository;
        private readonly AudioOutputFacade _audioOutputFacade;

        public AudioOutputPropertiesPresenter(
            IAudioOutputRepository audioOutputRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IIDRepository idRepository)
        {
            _audioOutputRepository = audioOutputRepository ?? throw new NullException(() => audioOutputRepository);
            _audioOutputFacade = new AudioOutputFacade(_audioOutputRepository, speakerSetupRepository, idRepository);
        }

        protected override AudioOutput GetEntity(AudioOutputPropertiesViewModel userInput) => _audioOutputRepository.Get(userInput.Entity.ID);

        protected override IResult Save(AudioOutput entity, AudioOutputPropertiesViewModel userInput) => _audioOutputFacade.Save(entity);

        protected override AudioOutputPropertiesViewModel ToViewModel(AudioOutput entity) => entity.ToPropertiesViewModel();
    }
}