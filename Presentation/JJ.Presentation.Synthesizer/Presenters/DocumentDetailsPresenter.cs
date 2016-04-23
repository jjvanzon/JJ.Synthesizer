using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDetailsPresenter : PresenterBase<DocumentDetailsViewModel>
    {
        private RepositoryWrapper _repositories;
        private DocumentManager _documentManager;

        public DocumentDetailsPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _documentManager = new DocumentManager(repositories);
        }

        public DocumentDetailsViewModel Create()
        {
            // Business
            Document document = _documentManager.Create();

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
            Document document = userInput.ToEntity(
                _repositories.DocumentRepository,
                _repositories.AudioOutputRepository,
                _repositories.SpeakerSetupRepository,
                _repositories.IDRepository);

            // Business
            VoidResult result = _documentManager.Save(document);
            if (!result.Successful)
            {
                // ToViewModel
                DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result.Messages);

                return viewModel;
            }
            else
            {
                // TODO: Perhaps report success and leave Committing to the MainPresenter.
                _repositories.DocumentRepository.Commit();

                // ToViewModel
                DocumentDetailsViewModel viewModel = ViewModelHelper.CreateEmptyDocumentDetailsViewModel();

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
            Document document = userInput.ToEntity(
                _repositories.DocumentRepository,
                _repositories.AudioOutputRepository,
                _repositories.SpeakerSetupRepository,
                _repositories.IDRepository);

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
