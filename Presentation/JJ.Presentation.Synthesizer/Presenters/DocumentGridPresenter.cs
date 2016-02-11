using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentGridPresenter
    {
        private IDocumentRepository _documentRepository;

        public DocumentGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public DocumentGridViewModel Show(DocumentGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntities
            IList<Document> documents = _documentRepository.GetRootDocumentsOrderedByName();

            // ToViewModel
            DocumentGridViewModel viewModel = documents.ToGridViewModel();

            // Non-Persisted
            viewModel.Visible = true;

            return viewModel;
        }

        public DocumentGridViewModel Refresh(DocumentGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntities
            IList<Document> documents = _documentRepository.GetRootDocumentsOrderedByName();

            // ToViewModel
            DocumentGridViewModel viewModel = documents.ToGridViewModel();

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            // Known bug, not easily solvable and also not a large problem: 
            // A renamed, uncommitted document will not end up in a new place in the list,
            // because the sorting done by the data store, which is not ware of the new name.

            return viewModel;
        }

        public DocumentGridViewModel Close(DocumentGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntities
            IList<Document> documents = _documentRepository.GetRootDocumentsOrderedByName();

            // ToViewModel
            DocumentGridViewModel viewModel = documents.ToGridViewModel();

            // Non-Persisted
            viewModel.Visible = false;

            // Known bug, not easily solvable and also not a large problem: 
            // A renamed, uncommitted document will not end up in a new place in the list,
            // because the sorting done by the data store, which is not ware of the new name.

            return viewModel;
        }
    }
}
