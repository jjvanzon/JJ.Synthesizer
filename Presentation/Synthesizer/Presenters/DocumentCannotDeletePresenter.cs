using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentCannotDeletePresenter : PresenterBase<DocumentCannotDeleteViewModel>
    {
        private IDocumentRepository _documentRepository;

        public DocumentCannotDeletePresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public DocumentCannotDeleteViewModel Show(int id, IList<Message> messages)
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
