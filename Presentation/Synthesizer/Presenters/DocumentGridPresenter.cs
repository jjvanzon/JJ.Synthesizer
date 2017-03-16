using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentGridPresenter : GridPresenterBase<DocumentGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        protected override DocumentGridViewModel CreateViewModel(DocumentGridViewModel userInput)
        {
            // Known bug, not easily solvable and also not a large problem: 
            // A renamed, uncommitted document will not end up in a new place in the list,
            // because the sorting done by the data store, which is not ware of the new name.

            // GetEntities
            IList<Document> documents = _documentRepository.OrderByName();

            // ToViewModel
            DocumentGridViewModel viewModel = documents.ToGridViewModel();

            return viewModel;
        }
    }
}
