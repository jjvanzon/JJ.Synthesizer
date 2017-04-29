using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using Canonicals = JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Framework.Collections;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryGridPresenter : GridPresenterBase<LibraryGridViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        public LibraryGridPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);

            _documentManager = new DocumentManager(repositories);
        }

        protected override LibraryGridViewModel CreateViewModel(LibraryGridViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.HigherDocumentID);

            // ToViewModel
            LibraryGridViewModel viewModel = document.ToLibraryGridViewModel();

            return viewModel;
        }

        public LibraryGridViewModel Remove(LibraryGridViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // Business
                    Canonicals.VoidResult result = _documentManager.DeleteDocumentReference(documentReferenceID);

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages = result.Messages;
                });
        }

        public LibraryGridViewModel Play(LibraryGridViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(documentReferenceID);

                    // Business
                    var patchManager = new PatchManager(new PatchRepositories(_repositories));
                    Result<Outlet> result = patchManager.TryAutoPatchFromDocumentRandomly(documentReference.LowerDocument);
                    Outlet outlet = result.Data;

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }
    }
}