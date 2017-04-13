using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
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
            LibrarySelectionPopupViewModel viewModel = CreateEmptyViewModel(userInput);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public LibrarySelectionPopupViewModel OK(LibrarySelectionPopupViewModel userInput, int? lowerDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            LibrarySelectionPopupViewModel viewModel;

            // UserInput Validation
            if (!lowerDocumentID.HasValue)
            {
                // CreateViewModel
                viewModel = CreateViewModel(userInput);

                // Non-Persisted
                viewModel.ValidationMessages.Add(new Message { PropertyKey = PropertyNames.LowerDocument, Text = ResourceFormatter.SelectALibraryFirst });
            }
            else
            {
                // GetEntities
                Document higherDocument = _documentRepository.Get(userInput.HigherDocumentID);
                Document lowerDocument = _documentRepository.Get(lowerDocumentID.Value);

                // Business
                Result<DocumentReference> result = _documentManager.CreateDocumentReference(higherDocument, lowerDocument);

                if (result.Successful)
                {
                    // ToViewModel
                    viewModel = CreateEmptyViewModel(userInput);
                }
                else
                {
                    // CreateViewModel
                    viewModel = CreateViewModel(userInput);
                }

                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages);

                // Successful?
                viewModel.Successful = result.Successful;
            }

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

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
                viewModel = CreateEmptyViewModel(userInput);
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

        private LibrarySelectionPopupViewModel CreateEmptyViewModel(LibrarySelectionPopupViewModel userInput)
        {
            // GetEntity
            Document higherDocument = _documentRepository.Get(userInput.HigherDocumentID);

            // ToViewModel
            LibrarySelectionPopupViewModel viewModel = higherDocument.ToEmptyLibrarySelectionPopupViewModel();
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
