using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioOutputPropertiesPresenter 
        : PropertiesPresenterBase<AudioOutput, AudioOutputPropertiesViewModel>
    {
        private readonly IAudioOutputRepository _audioOutputRepository;
        private readonly AudioOutputManager _audioOutputManager;

        public AudioOutputPropertiesPresenter(
            IAudioOutputRepository audioOutputRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IIDRepository idRepository)
        {
            _audioOutputRepository = audioOutputRepository ?? throw new NullException(() => audioOutputRepository);
            _audioOutputManager = new AudioOutputManager(_audioOutputRepository, speakerSetupRepository, idRepository);
        }

        protected override AudioOutput GetEntity(AudioOutputPropertiesViewModel userInput)
        {
            return _audioOutputRepository.Get(userInput.Entity.ID);
        }

        protected override IResult Save(AudioOutput entity)
        {
            return _audioOutputManager.Save(entity);
        }

        protected override AudioOutputPropertiesViewModel ToViewModel(AudioOutput entity)
        {
            return entity.ToPropertiesViewModel();
        }
    }
}