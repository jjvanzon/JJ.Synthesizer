using System.Collections.Generic;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentPropertiesPresenter : PresenterBase<DocumentPropertiesViewModel>
    {
        private RepositoryWrapper _repositories;
        private DocumentManager _documentManager;

        public DocumentPropertiesPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _documentManager = new DocumentManager(repositories);
        }

        public DocumentPropertiesViewModel Show(DocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document entity = _repositories.DocumentRepository.Get(userInput.Entity.ID);

            // ToViewModel
            DocumentPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentPropertiesViewModel Refresh(DocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document entity = _repositories.DocumentRepository.Get(userInput.Entity.ID);

            // ToViewModel
            DocumentPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = userInput.Visible;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentPropertiesViewModel Close(DocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            DocumentPropertiesViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public DocumentPropertiesViewModel LoseFocus(DocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            DocumentPropertiesViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private DocumentPropertiesViewModel Update(DocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.Entity.ID);

            // Business
            VoidResult result = _documentManager.Save(document);

            // ToViewModel
            DocumentPropertiesViewModel viewModel = document.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}