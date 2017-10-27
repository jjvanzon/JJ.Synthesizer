using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibrarySelectionPopupPresenter : EntityPresenterWithoutSaveBase<Document, LibrarySelectionPopupViewModel>
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

        protected override Document GetEntity(LibrarySelectionPopupViewModel userInput) => _repositories.DocumentRepository.Get(userInput.HigherDocumentID);

        protected override LibrarySelectionPopupViewModel ToViewModel(Document higherDocument)
        {
            // Business
            IList<Document> potentialLowerDocuments = _documentManager.GetLowerDocumentCandidates(higherDocument);

            // ToViewModel
            LibrarySelectionPopupViewModel viewModel = higherDocument.ToLibrarySelectionPopupViewModel(potentialLowerDocuments);

            return viewModel;
        }

        /// <summary>
        /// Just to be clear there is both a Cancel button and a Close button in the window border,
        /// we have two actions, but really they do the same.
        /// </summary>
        public void Cancel(LibrarySelectionPopupViewModel userInput) => Close(userInput);

        /// <summary>
        /// Just to be clear there is both a Cancel button and a Close button in the window border,
        /// we have two actions, but really they do the same.
        /// </summary>
        public override void Close(LibrarySelectionPopupViewModel userInput)
        {
            ExecuteNonPersistedAction(userInput, () =>
            {
                userInput.Visible = false;
                userInput.List = new List<IDAndName>();
            });
        }

        public LibrarySelectionPopupViewModel Load(LibrarySelectionPopupViewModel userInput)
        {
            return ExecuteAction(userInput, x => { }, x => x.Visible = true);
        }

        public LibrarySelectionPopupViewModel OK(LibrarySelectionPopupViewModel userInput, int? lowerDocumentID)
        {
            return ExecuteAction(
                userInput,
                entity =>
                {
                    // Validation
                    if (!lowerDocumentID.HasValue)
                    {
                        return new VoidResult(ResourceFormatter.SelectALibraryFirst);
                    }

                    // GetEntity
                    Document lowerDocument = _repositories.DocumentRepository.Get(lowerDocumentID.Value);

                    // Business
                    IResult result = _documentManager.CreateDocumentReference(entity, lowerDocument);

                    return result;
                },
                viewModel =>
                {
                    if (viewModel.Successful)
                    {
                        viewModel.List = new List<IDAndName>();
                        viewModel.Visible = false;
                    }
                });
        }

        /// <see cref="PresenterBase{}.ExecuteNonPersistedAction"/>
        public void OpenItemExternally(LibrarySelectionPopupViewModel viewModel, int lowerDocumentID)
        {
            ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    // Business
                    Document potentialLowerDocument = _repositories.DocumentRepository.Get(lowerDocumentID);

                    // ToViewModel
                    viewModel.DocumentToOpenExternally = potentialLowerDocument.ToIDAndName();
                });
        }

        public LibrarySelectionPopupViewModel Play(LibrarySelectionPopupViewModel userInput, int lowerDocumentID)
        {
            Outlet outlet = null;

            return ExecuteAction(
                userInput,
                entity =>
                {
                    // GetEntity
                    Document lowerDocument = _repositories.DocumentRepository.Get(lowerDocumentID);

                    // Business
                    Result<Outlet> result = _autoPatcher.TryAutoPatchFromDocumentRandomly(lowerDocument, mustIncludeHidden: false);
                    outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }
                },
                viewModel =>
                {
                    // Non-Persisted
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }

        public override LibrarySelectionPopupViewModel Refresh(LibrarySelectionPopupViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                entity => { },
                viewModel =>
                {
                    if (viewModel.Successful)
                    {
                        viewModel.List = new List<IDAndName>();
                    }
                });
        }

        [Obsolete("Use Load instead.", true)]
        public override void Show(LibrarySelectionPopupViewModel viewModel)
        {
            throw new NotSupportedException("Call Load instead.");
        }
    }
}
