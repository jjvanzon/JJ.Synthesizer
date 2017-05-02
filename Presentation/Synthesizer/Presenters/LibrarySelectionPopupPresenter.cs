using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using Canonicals = JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibrarySelectionPopupPresenter : PresenterBase<LibrarySelectionPopupViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        public LibrarySelectionPopupPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories;
            if (repositories == null) throw new NullException(() => repositories);

            _documentManager = new DocumentManager(repositories);
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
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.Add(new Message(PropertyNames.LowerDocument, ResourceFormatter.SelectALibraryFirst).ToCanonical());
            }
            else
            {
                // GetEntities
                Document higherDocument = _repositories.DocumentRepository.Get(userInput.HigherDocumentID);
                Document lowerDocument = _repositories.DocumentRepository.Get(lowerDocumentID.Value);

                // Business
                Canonicals.ResultDto<DocumentReference> result = _documentManager.CreateDocumentReference(higherDocument, lowerDocument);

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
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result.Messages);

                // Successful?
                viewModel.Successful = result.Successful;
            }

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

        public LibrarySelectionPopupViewModel Play(LibrarySelectionPopupViewModel userInput, int documentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            LibrarySelectionPopupViewModel viewModel = CreateViewModel(userInput);

            // GetEntity
            Document lowerDocument = _repositories.DocumentRepository.Get(documentID);

            // Business
            var patchManager = new PatchManager(new PatchRepositories(_repositories));
            Result<Outlet> result = patchManager.TryAutoPatchFromDocumentRandomly(lowerDocument, hidden: false);
            Outlet outlet = result.Data;

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
            viewModel.OutletIDToPlay = outlet?.ID;

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        private LibrarySelectionPopupViewModel CreateEmptyViewModel(LibrarySelectionPopupViewModel userInput)
        {
            // GetEntity
            Document higherDocument = _repositories.DocumentRepository.Get(userInput.HigherDocumentID);

            // ToViewModel
            LibrarySelectionPopupViewModel viewModel = higherDocument.ToEmptyLibrarySelectionPopupViewModel();
            return viewModel;
        }

        private LibrarySelectionPopupViewModel CreateViewModel(LibrarySelectionPopupViewModel userInput)
        {
            // GetEntity
            Document higherDocument = _repositories.DocumentRepository.Get(userInput.HigherDocumentID);

            // Business
            IList<Document> potentialLowerDocuments = _documentManager.GetLowerDocumentCandidates(higherDocument);

            // ToViewModel
            LibrarySelectionPopupViewModel viewModel = higherDocument.ToLibrarySelectionPopupViewModel(potentialLowerDocuments);
            return viewModel;
        }
    }
}
