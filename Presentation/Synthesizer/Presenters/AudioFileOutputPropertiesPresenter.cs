using System;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputPropertiesPresenter 
        : PropertiesPresenterBase<AudioFileOutput, AudioFileOutputPropertiesViewModel>
    {
        private readonly AudioFileOutputManager _audioFileOutputManager;
        private readonly IAudioFileOutputRepository _audioFileOutputRepository;

        public AudioFileOutputPropertiesPresenter(AudioFileOutputManager audioFileOutputManager, IAudioFileOutputRepository audioFileOutputRepository)
        {
            _audioFileOutputRepository = audioFileOutputRepository ?? throw new ArgumentNullException(nameof(audioFileOutputRepository));
            _audioFileOutputManager = audioFileOutputManager ?? throw new ArgumentNullException(nameof(audioFileOutputManager));
        }

        protected override AudioFileOutput GetEntity(AudioFileOutputPropertiesViewModel userInput)
        {
            return _audioFileOutputRepository.Get(userInput.Entity.ID);
        }

        protected override IResult Save(AudioFileOutput entity)
        {
            return _audioFileOutputManager.Save(entity);
        }

        protected override AudioFileOutputPropertiesViewModel ToViewModel(AudioFileOutput entity)
        {
            return entity.ToPropertiesViewModel();
        }

        public AudioFileOutputPropertiesViewModel Delete(AudioFileOutputPropertiesViewModel userInput)
        {
            return TemplateAction(
                userInput,
                entity =>
                {
                    _audioFileOutputManager.Delete(entity.ID);
                    return null;
                });
        }
    }
}