using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryGridPresenter : GridPresenterBase<LibraryGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentManager _documentManager;

        public LibraryGridPresenter([NotNull] RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentRepository = repositories.DocumentRepository;

            _documentManager = new DocumentManager(repositories);
        }

        protected override LibraryGridViewModel CreateViewModel(LibraryGridViewModel userInput)
        {
            // GetEntity
            Document document = _documentRepository.Get(userInput.HigherDocumentID);

            // ToViewModel
            LibraryGridViewModel viewModel = document.ToLibraryGridViewModel();

            return viewModel;
        }

        public LibraryGridViewModel Remove(LibraryGridViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(userInput, viewModel =>
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
