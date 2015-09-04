using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentCannotDeletePresenter
    {
        private IDocumentRepository _documentRepository;

        public DocumentCannotDeleteViewModel ViewModel { get; private set; }

        public DocumentCannotDeletePresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        /// <summary> Can return NotFoundViewModel or DocumentCannotDeleteViewModel. </summary>
        public object Show(int id, IList<Message> messages)
        {
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                return ViewModelHelper.CreateDocumentNotFoundViewModel();
            }
            else
            {
                ViewModel = document.ToCannotDeleteViewModel(messages);
                ViewModel.Visible = true;
                return ViewModel;
            }
        }

        public void OK()
        {
            AssertViewModel();

            ViewModel.Visible = false;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
