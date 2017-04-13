using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentTreePresenter : PresenterBase<DocumentTreeViewModel>
    {
        private readonly PatchRepositories _repositories;
        private readonly PatchManager _patchManager;

        public DocumentTreePresenter(PatchRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);

            _patchManager = new PatchManager(_repositories);
        }

        public DocumentTreeViewModel Show(DocumentTreeViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            DocumentTreeViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentTreeViewModel Refresh(DocumentTreeViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            DocumentTreeViewModel viewModel = CreateViewModel(userInput);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentTreeViewModel Close(DocumentTreeViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            DocumentTreeViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        // Helpers

        private DocumentTreeViewModel CreateViewModel(DocumentTreeViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.ID);

            // ToViewModel
            var converter = new RecursiveToDocumentTreeViewModelFactory();
            DocumentTreeViewModel viewModel = converter.ToTreeViewModel(document, _repositories);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
        }
    }
}
