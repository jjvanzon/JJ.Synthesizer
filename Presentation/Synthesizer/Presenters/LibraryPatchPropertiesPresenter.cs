using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
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

        /// <summary> This view is read-only, so just recreate the view model. </summary>
        protected override LibraryPatchPropertiesViewModel UpdateEntity(LibraryPatchPropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel => CreateViewModel(userInput));
        }
    }
}
