using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentCannotDeletePresenter
    {
        private IDocumentRepository _documentRepository;

        public DocumentCannotDeletePresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        /// <summary> Can return NotFoundViewModel or DocumentCannotDeleteViewModel. </summary>
        public object Show(int id, IList<Message> messages)
        {
            // GetEntity
            Document document = _documentRepository.Get(id);

            // ToViewModel
            DocumentCannotDeleteViewModel viewModel = document.ToCannotDeleteViewModel(messages);

            // Non-Persisted
            viewModel.Visible = true;

            return viewModel;
        }

        public object OK(DocumentCannotDeleteViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Document document = _documentRepository.Get(userInput.Document.ID);

            // ToViewModel
            DocumentCannotDeleteViewModel viewModel = document.ToCannotDeleteViewModel(userInput.ValidationMessages);

            // Non-Persisted
            viewModel.Visible = false;

            return viewModel;
        }
    }
}
