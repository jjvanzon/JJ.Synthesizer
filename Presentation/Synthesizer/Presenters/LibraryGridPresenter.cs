using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
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
        private readonly AutoPatcher _autoPatcher;

        public LibraryGridPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _documentManager = new DocumentManager(repositories);
            _autoPatcher = new AutoPatcher(_repositories);
        }

        protected override LibraryGridViewModel CreateViewModel(LibraryGridViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.HigherDocumentID);

            // ToViewModel
            LibraryGridViewModel viewModel = document.ToLibraryGridViewModel();

            return viewModel;
        }

        public LibraryGridViewModel OpenItemExternally(LibraryGridViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(documentReferenceID);

                    // Non-Persisted
                    viewModel.DocumentToOpenExternally = documentReference.LowerDocument.ToIDAndName();
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
                    Result<Outlet> result = _autoPatcher.TryAutoPatchFromDocumentRandomly(documentReference.LowerDocument, mustIncludeHidden: false);
                    Outlet outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages.AddRange(result.Messages);
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }

        public LibraryGridViewModel Remove(LibraryGridViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // Business
                    VoidResult result = _documentManager.DeleteDocumentReference(documentReferenceID);

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages = result.Messages;
                });
        }
    }
}