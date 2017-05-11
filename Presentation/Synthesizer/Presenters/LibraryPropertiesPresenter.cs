using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryPropertiesPresenter : PropertiesPresenterBase<LibraryPropertiesViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        public LibraryPropertiesPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _documentManager = new DocumentManager(repositories);
        }

        protected override LibraryPropertiesViewModel CreateViewModel(LibraryPropertiesViewModel userInput)
        {
            // GetEntity
            DocumentReference entity = _repositories.DocumentReferenceRepository.Get(userInput.DocumentReferenceID);

            // ToViewModel
            LibraryPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            return viewModel;
        }

        protected override void UpdateEntity(LibraryPropertiesViewModel viewModel)
        {
            // ToEntity: was already done by the MainPresenter.

            // GetEntity
            DocumentReference entity = _repositories.DocumentReferenceRepository.Get(viewModel.DocumentReferenceID);

            // Business
            VoidResultDto result = _documentManager.SaveDocumentReference(entity);

            // Non-Persisted
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;
        }

        public LibraryPropertiesViewModel Open(LibraryPropertiesViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(documentReferenceID);

                    // Non-Persisted
                    viewModel.DocumentToOpen = documentReference.LowerDocument.ToIDAndName();

                    // Successful
                    viewModel.Successful = true;
                });
        }

        public LibraryPropertiesViewModel Play(LibraryPropertiesViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(documentReferenceID);

                    // Business
                    var patchManager = new PatchManager(new PatchRepositories(_repositories));
                    Framework.Business.Result<Outlet> result = patchManager.TryAutoPatchFromDocumentRandomly(documentReference.LowerDocument, mustIncludeHidden: false);
                    Outlet outlet = result.Data;

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }
    }
}
