using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
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
        private readonly AutoPatcher _autoPatcher;

        public LibraryPatchGridPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _autoPatcher = new AutoPatcher(_repositories);
        }

        protected override LibraryPatchGridViewModel CreateViewModel(LibraryPatchGridViewModel userInput)
        {
            // GetEntity
            DocumentReference lowerDocumentReference = _repositories.DocumentReferenceRepository.Get(userInput.LowerDocumentReferenceID);

            // Business
            IList<Patch> patches = PatchGrouper.GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(lowerDocumentReference.LowerDocument.Patches, userInput.Group, mustIncludeHidden: false);

            // ToViewModel
            LibraryPatchGridViewModel viewModel = lowerDocumentReference.ToLibraryPatchGridViewModel(patches, userInput.Group);

            return viewModel;
        }

        public LibraryPatchGridViewModel Play(LibraryPatchGridViewModel userInput, int patchID)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Patch patch = _repositories.PatchRepository.Get(patchID);

                // Business
                Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(patch);
                Outlet outlet = result.Data;
                if (outlet != null)
                {
                    _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                }

                // Non-Persisted
                viewModel.OutletIDToPlay = outlet?.ID;
                viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                viewModel.Successful = result.Successful;
            });
        }

        public LibraryPatchGridViewModel OpenItemExternally(LibraryPatchGridViewModel userInput, int patchID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Patch patch = _repositories.PatchRepository.Get(patchID);

                    // Non-Persisted
                    viewModel.DocumentToOpenExternally = patch.Document.ToIDAndName();
                    viewModel.PatchToOpenExternally = patch.ToIDAndName();
                });
        }
    }
}
