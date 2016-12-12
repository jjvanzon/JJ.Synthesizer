using System.Collections.Generic;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Framework.Common;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioOutputPropertiesPresenter : PresenterBase<AudioOutputPropertiesViewModel>
    {
        private readonly IAudioOutputRepository _audioOutputRepository;

        private readonly AudioOutputManager _audioOutputManager;

        public AudioOutputPropertiesPresenter(
            IAudioOutputRepository audioOutputRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IIDRepository idRepository)
        {
            if (audioOutputRepository == null) throw new NullException(() => audioOutputRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _audioOutputRepository = audioOutputRepository;

            _audioOutputManager = new AudioOutputManager(_audioOutputRepository, speakerSetupRepository, idRepository);
        }

        public AudioOutputPropertiesViewModel Show(AudioOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            AudioOutput entity = _audioOutputRepository.Get(userInput.Entity.ID);

            // ToViewModel
            AudioOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public AudioOutputPropertiesViewModel Refresh(AudioOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            AudioOutput entity = _audioOutputRepository.Get(userInput.Entity.ID);

            // ToViewModel
            AudioOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public AudioOutputPropertiesViewModel Close(AudioOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            AudioOutputPropertiesViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public AudioOutputPropertiesViewModel LoseFocus(AudioOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            AudioOutputPropertiesViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private AudioOutputPropertiesViewModel Update(AudioOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            AudioOutput entity = _audioOutputRepository.Get(userInput.Entity.ID);

            // Business
            VoidResult result = _audioOutputManager.Save(entity);

            // ToViewModel
            AudioOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}