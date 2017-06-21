using JetBrains.Annotations;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryPatchPropertiesPresenter
        : PropertiesPresenterBase<(Patch patch, DocumentReference documentReference), LibraryPatchPropertiesViewModel>
    {
        private readonly IPatchRepository _patchRepository;
        private readonly IDocumentReferenceRepository _documentReferenceRepository;

        public LibraryPatchPropertiesPresenter([NotNull] IPatchRepository patchRepository, [NotNull] IDocumentReferenceRepository documentReferenceRepository)
        {
            _documentReferenceRepository = documentReferenceRepository ?? throw new NullException(() => documentReferenceRepository);
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);
        }

        protected override (Patch patch, DocumentReference documentReference) GetEntity(LibraryPatchPropertiesViewModel userInput)
        {
            Patch patch = _patchRepository.Get(userInput.PatchID);
            DocumentReference documentReference = _documentReferenceRepository.Get(userInput.DocumentReferenceID);
            return (patch, documentReference);
        }

        protected override LibraryPatchPropertiesViewModel ToViewModel(
            (Patch patch, DocumentReference documentReference) entities)
        {
            return entities.patch.ToLibraryPatchPropertiesViewModel(entities.documentReference);
        }

        public LibraryPatchPropertiesViewModel Play(LibraryPatchPropertiesViewModel userInput, RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            Outlet outlet = null;

            return TemplateAction(
                userInput,
                entities =>
                {
                    var autoPatcher = new AutoPatcher(repositories);
                    Result<Outlet> result = autoPatcher.AutoPatch_TryCombineSounds(entities.patch);
                    outlet = result.Data;
                    if (outlet != null)
                    {
                        autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }
                    return null;
                },
                viewModel =>
                { 
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }

        public LibraryPatchPropertiesViewModel OpenExternally(LibraryPatchPropertiesViewModel userInput)
        {
            return TemplateAction(
                userInput,
                viewModel =>
                {
                    Patch patch = GetEntity(userInput).patch;
                    viewModel.DocumentToOpenExternally = patch.Document.ToIDAndName();
                    viewModel.PatchToOpenExternally = patch.ToIDAndName();
                });
        }
    }
}