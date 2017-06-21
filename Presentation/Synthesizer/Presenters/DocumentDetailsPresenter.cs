using JJ.Business.Canonical;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDetailsPresenter : PresenterBase<DocumentDetailsViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        public DocumentDetailsPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _documentManager = new DocumentManager(repositories);
        }

        public DocumentDetailsViewModel Create()
        {
            // Business
            Document document = _documentManager.CreateWithPatch();

            // ToViewModel
            DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

            // Non-Persisted
            viewModel.IDVisible = false;
            viewModel.CanDelete = false;
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentDetailsViewModel Save(DocumentDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document document = userInput.ToEntityWithAudioOutput(
                _repositories.DocumentRepository,
                _repositories.DocumentReferenceRepository,
                _repositories.AudioOutputRepository,
                _repositories.PatchRepository,
                _repositories.SpeakerSetupRepository);

            // Business
            IResult result = _documentManager.Save(document);
            if (!result.Successful)
            {
                // ToViewModel
                DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                viewModel.Successful = false;

                return viewModel;
            }
            else
            {
                // ToViewModel
                DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.Visible = false;

                // Successful
                viewModel.Successful = true;

                return viewModel;
            }
        }

        public DocumentDetailsViewModel Close(DocumentDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document document = userInput.ToEntityWithAudioOutput(
                _repositories.DocumentRepository,
                _repositories.DocumentReferenceRepository,
                _repositories.AudioOutputRepository,
                _repositories.PatchRepository,
                _repositories.SpeakerSetupRepository);

            // ToViewModel
            DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}
