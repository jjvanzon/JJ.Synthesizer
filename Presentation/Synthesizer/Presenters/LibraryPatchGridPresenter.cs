using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryPatchGridPresenter : GridPresenterBase<LibraryPatchGridViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly PatchRepositories _patchRepositories;

        public LibraryPatchGridPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _patchRepositories = new PatchRepositories(repositories);
        }

        protected override LibraryPatchGridViewModel CreateViewModel(LibraryPatchGridViewModel userInput)
        {
            // GetEntity
            DocumentReference lowerDocumentReference = _repositories.DocumentReferenceRepository.Get(userInput.LowerDocumentReferenceID);

            // Business
            var patchManager = new PatchManager(_patchRepositories);
            IList<Patch> patches = patchManager.GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(lowerDocumentReference.LowerDocument.Patches, userInput.Group, hidden: false);

            // ToViewModel
            LibraryPatchGridViewModel viewModel = patches.ToLibraryPatchGridViewModel(userInput.LowerDocumentReferenceID, userInput.Group);

            return viewModel;
        }

        public LibraryPatchGridViewModel Play(LibraryPatchGridViewModel userInput, int patchID)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Patch patch = _repositories.PatchRepository.Get(patchID);

                // Business
                var patchManager = new PatchManager(patch, _patchRepositories);
                Result<Outlet> result = patchManager.AutoPatch_TryCombineSignals(patch);
                Outlet outlet = result.Data;

                // Non-Persisted
                viewModel.OutletIDToPlay = outlet?.ID;
                viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                viewModel.Successful = result.Successful;
            });
        }
    }
}
