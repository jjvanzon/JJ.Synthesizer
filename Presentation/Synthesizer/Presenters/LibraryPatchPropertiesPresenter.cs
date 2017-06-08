using JetBrains.Annotations;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryPatchPropertiesPresenter : PropertiesPresenterBase<LibraryPatchPropertiesViewModel>
    {
        private readonly IPatchRepository _patchRepository;
        private readonly IDocumentReferenceRepository _documentReferenceRepository;

        public LibraryPatchPropertiesPresenter([NotNull] IPatchRepository patchRepository, [NotNull] IDocumentReferenceRepository documentReferenceRepository)
        {
            _documentReferenceRepository = documentReferenceRepository ?? throw new NullException(() => documentReferenceRepository);
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);
        }

        protected override LibraryPatchPropertiesViewModel CreateViewModel(LibraryPatchPropertiesViewModel userInput)
        {
            // Get Entities
            Patch patch = _patchRepository.Get(userInput.PatchID);
            DocumentReference documentReference = _documentReferenceRepository.Get(userInput.DocumentReferenceID);

            // ToViewModel
            LibraryPatchPropertiesViewModel viewModel = patch.ToLibraryPatchPropertiesViewModel(documentReference);

            return viewModel;
        }

        public LibraryPatchPropertiesViewModel Play(LibraryPatchPropertiesViewModel userInput, [NotNull] RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Patch patch = repositories.PatchRepository.Get(userInput.PatchID);

                // Business
                var patchManager = new PatchManager(patch, repositories);
                Result<Outlet> result = patchManager.AutoPatch_TryCombineSounds(patch);
                Outlet outlet = result.Data;

                // Non-Persisted
                viewModel.OutletIDToPlay = outlet?.ID;
                userInput.ValidationMessages.AddRange(result.Messages.ToCanonical());
                userInput.Successful = result.Successful;
            });
        }

        public LibraryPatchPropertiesViewModel OpenExternally(LibraryPatchPropertiesViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Patch patch = _patchRepository.Get(userInput.PatchID);

                    // Non-Persisted
                    viewModel.DocumentToOpenExternally = patch.Document.ToIDAndName();
                    viewModel.PatchToOpenExternally = patch.ToIDAndName();
                });
        }
    }
}
