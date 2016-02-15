using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputGridPresenter
    {
        private IDocumentRepository _documentRepository;

        public AudioFileOutputGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public AudioFileOutputGridViewModel Show(AudioFileOutputGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            AudioFileOutputGridViewModel viewModel = document.ToAudioFileOutputGridViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public AudioFileOutputGridViewModel Refresh(AudioFileOutputGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            AudioFileOutputGridViewModel viewModel = document.ToAudioFileOutputGridViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public AudioFileOutputGridViewModel Close(AudioFileOutputGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            AudioFileOutputGridViewModel viewModel = document.ToAudioFileOutputGridViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        private void CopyNonPersistedProperties(AudioFileOutputGridViewModel sourceViewModel, AudioFileOutputGridViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}