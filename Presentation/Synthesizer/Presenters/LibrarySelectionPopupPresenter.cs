using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibrarySelectionPopupPresenter : PresenterBase<LibrarySelectionPopupViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentManager _documentManager;

        public LibrarySelectionPopupPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentManager = new DocumentManager(repositories);
            _documentRepository = repositories.DocumentRepository;
        }

        public LibrarySelectionPopupViewModel Show(LibrarySelectionPopupViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            LibrarySelectionPopupViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public LibrarySelectionPopupViewModel Cancel(LibrarySelectionPopupViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToViewModel
            LibrarySelectionPopupViewModel viewModel = ViewModelHelper.CreateEmptyLibrarySelectionPopupViewModel();
            // HACK: Do it in ToViewModel
            viewModel.HigherDocumentID = userInput.HigherDocumentID;

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public LibrarySelectionPopupViewModel OK(LibrarySelectionPopupViewModel userInput, int? lowerDocumentID)
        {
            // TODO: Handle with validation message.
            if (!lowerDocumentID.HasValue) throw new NullException(() => lowerDocumentID);

            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            Document higherDocument = _documentRepository.Get(userInput.HigherDocumentID);
            Document lowerDocument = _documentRepository.Get(lowerDocumentID.Value);

            // Business
            Result<DocumentReference> result = _documentManager.CreateDocumentReference(higherDocument, lowerDocument);

            LibrarySelectionPopupViewModel viewModel;
            if (result.Successful)
            {
                // ToViewModel
                viewModel = ViewModelHelper.CreateEmptyLibrarySelectionPopupViewModel();
                // HACK: Do it in ToViewModel
                viewModel.HigherDocumentID = userInput.HigherDocumentID;
            }
            else
            {
                // CreateViewModel
                viewModel = CreateViewModel(userInput);
            }

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public LibrarySelectionPopupViewModel Refresh(LibrarySelectionPopupViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            LibrarySelectionPopupViewModel viewModel;
            if (!userInput.Visible)
            {
                // ToViewModel
                viewModel = ViewModelHelper.CreateEmptyLibrarySelectionPopupViewModel();
                // HACK: Do it in ToViewModel
                viewModel.HigherDocumentID = userInput.HigherDocumentID;
            }
            else
            {
                // CreateViewModel
                viewModel = CreateViewModel(userInput);
            }

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        private LibrarySelectionPopupViewModel CreateViewModel(LibrarySelectionPopupViewModel userInput)
        {
            // GetEntity
            Document higherDocument = _documentRepository.Get(userInput.HigherDocumentID);

            // Business
            IList<Document> potentialLowerDocuments = _documentManager.GetLowerDocumentCandidates(higherDocument);

            // ToViewModel
            LibrarySelectionPopupViewModel viewModel = higherDocument.ToLibrarySelectionPopupViewModel(potentialLowerDocuments);
            return viewModel;
        }
    }
}
