using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentCannotDeletePresenter : PresenterBase<DocumentCannotDeleteViewModel>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentCannotDeletePresenter(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository ?? throw new NullException(() => documentRepository);
        }

        public DocumentCannotDeleteViewModel Show(int id, IList<string> messages)
        {
            // GetEntity
            Document document = _documentRepository.Get(id);

            // ToViewModel
            DocumentCannotDeleteViewModel viewModel = document.ToCannotDeleteViewModel(messages);

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentCannotDeleteViewModel OK(DocumentCannotDeleteViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.Document.ID);

            // ToViewModel
            DocumentCannotDeleteViewModel viewModel = document.ToCannotDeleteViewModel(userInput.ValidationMessages);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}
