using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibrarySelectionPopupPresenter : PresenterBase<LibrarySelectionPopupViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;
        private readonly AutoPatcher _autoPatcher;

        public LibrarySelectionPopupPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);

            _documentManager = new DocumentManager(repositories);
            _autoPatcher = new AutoPatcher(_repositories);
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
                viewModel.ValidationMessages.Add(ResourceFormatter.SelectALibraryFirst);
            }
            else
            {
                // GetEntities
                Document higherDocument = _repositories.DocumentRepository.Get(userInput.HigherDocumentID);
                Document lowerDocument = _repositories.DocumentRepository.Get(lowerDocumentID.Value);

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

        public void OpenItemExternally(LibrarySelectionPopupViewModel viewModel, int lowerDocumentID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            // RefreshCounter
            viewModel.RefreshCounter++;

            // Business
            Document document = _repositories.DocumentRepository.Get(lowerDocumentID);

            // CreateViewModel
            viewModel.DocumentToOpenExternally = document.ToIDAndName();
        }

        public LibrarySelectionPopupViewModel Play(LibrarySelectionPopupViewModel userInput, int lowerDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            LibrarySelectionPopupViewModel viewModel = CreateViewModel(userInput);

            // GetEntity
            Document lowerDocument = _repositories.DocumentRepository.Get(lowerDocumentID);

            // Business
            Result<Outlet> result = _autoPatcher.TryAutoPatchFromDocumentRandomly(lowerDocument, mustIncludeHidden: false);
            Outlet outlet = result.Data;
            if (outlet != null)
            {
                _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
            }

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);
            viewModel.OutletIDToPlay = outlet?.ID;

            // Successful?
            viewModel.Successful = result.Successful;

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
