using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDetailsPresenter
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
            var document = new Document();
            document.ID = _repositories.IDRepository.GetID();
            _repositories.DocumentRepository.Insert(document);

            // ToViewModel
            DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

            // Non-Persisted
            viewModel.IDVisible = false;
            viewModel.CanDelete = false;
            viewModel.Visible = true;

            return viewModel;
        }

        public DocumentDetailsViewModel Save(DocumentDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // ToEntity
            Document document = userInput.ToEntity(_repositories.DocumentRepository);

            // Business
            VoidResult result = _documentManager.ValidateNonRecursive(document);
            if (!result.Successful)
            {
                // ToViewModel
                DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

                // Non-Persisted
                viewModel.ValidationMessages = result.Messages;

                return viewModel;
            }
            else
            {
                // TODO: Perhaps report success and leave Committing to the MainPresenter.
                _repositories.DocumentRepository.Commit();

                // ToViewModel
                DocumentDetailsViewModel viewModel = ViewModelHelper.CreateEmptyDocumentDetailsViewModel();

                // Non-Persisted
                viewModel.Visible = false;

                return viewModel;
            }
        }

        public DocumentDetailsViewModel Close(DocumentDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // ToEntity
            Document document = userInput.ToEntity(_repositories.DocumentRepository);

            // ToViewModel
            DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

            // Non-Persisted
            viewModel.Visible = false;

            return viewModel;
        }
    }
}
