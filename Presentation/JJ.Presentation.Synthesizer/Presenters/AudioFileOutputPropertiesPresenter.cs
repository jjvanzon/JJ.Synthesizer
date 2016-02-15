using System.Collections.Generic;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputPropertiesPresenter
    {
        private AudioFileOutputRepositories _repositories;
        private AudioFileOutputManager _audioFileOutputManager;

        public AudioFileOutputPropertiesPresenter(AudioFileOutputRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _audioFileOutputManager = new AudioFileOutputManager(_repositories);
        }

        public AudioFileOutputPropertiesViewModel Show(AudioFileOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            AudioFileOutput entity = _repositories.AudioFileOutputRepository.Get(userInput.Entity.ID);

            // ToViewModel
            AudioFileOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel(
                _repositories.AudioFileFormatRepository,
                _repositories.SampleDataTypeRepository,
                _repositories.SpeakerSetupRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public AudioFileOutputPropertiesViewModel Refresh(AudioFileOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            AudioFileOutput entity = _repositories.AudioFileOutputRepository.Get(userInput.Entity.ID);

            // ToViewModel
            AudioFileOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel(
                _repositories.AudioFileFormatRepository,
                _repositories.SampleDataTypeRepository,
                _repositories.SpeakerSetupRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public AudioFileOutputPropertiesViewModel Close(AudioFileOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            AudioFileOutputPropertiesViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public AudioFileOutputPropertiesViewModel LoseFocus(AudioFileOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            AudioFileOutputPropertiesViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private AudioFileOutputPropertiesViewModel Update(AudioFileOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            AudioFileOutput entity = _repositories.AudioFileOutputRepository.Get(userInput.Entity.ID);

            // Business
            VoidResult result = _audioFileOutputManager.Validate(entity);

            // ToViewModel
            AudioFileOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel(
                _repositories.AudioFileFormatRepository,
                _repositories.SampleDataTypeRepository,
                _repositories.SpeakerSetupRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        private void CopyNonPersistedProperties(AudioFileOutputPropertiesViewModel sourceViewModel, AudioFileOutputPropertiesViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}