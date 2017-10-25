using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputPropertiesPresenter 
        : EntityWritePresenterBase<AudioFileOutput, AudioFileOutputPropertiesViewModel>
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
            return ExecuteAction(
                userInput,
                entity =>
                {
                    _audioFileOutputManager.Delete(entity.ID);
                    return null;
                });
        }
    }
}