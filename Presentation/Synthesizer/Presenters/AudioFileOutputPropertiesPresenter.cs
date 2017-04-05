using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using System;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputPropertiesPresenter 
        : PropertiesPresenterBase<AudioFileOutputPropertiesViewModel>
    {
        private readonly IAudioFileOutputRepository _audioFileOutputRepository;
        private readonly AudioFileOutputManager _audioFileOutputManager;

        public AudioFileOutputPropertiesPresenter(AudioFileOutputRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _audioFileOutputRepository = repositories.AudioFileOutputRepository;
            _audioFileOutputManager = new AudioFileOutputManager(repositories);
        }

        protected override AudioFileOutputPropertiesViewModel CreateViewModel(AudioFileOutputPropertiesViewModel userInput)
        {
            // GetEntity
            AudioFileOutput entity = _audioFileOutputRepository.Get(userInput.Entity.ID);

            // ToViewModel
            AudioFileOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            return viewModel;
        }

        protected override AudioFileOutputPropertiesViewModel UpdateEntity(AudioFileOutputPropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // ToEntity: was already done by the MainPresenter.

                // GetEntity
                AudioFileOutput entity = _audioFileOutputRepository.Get(userInput.Entity.ID);

                // Business
                VoidResult result = _audioFileOutputManager.Save(entity);

                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages);

                // Successful?
                viewModel.Successful = result.Successful;
            });
        }
    }
}