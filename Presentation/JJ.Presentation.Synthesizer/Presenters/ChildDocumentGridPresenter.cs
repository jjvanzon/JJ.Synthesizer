using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ChildDocumentGridPresenter
    {
        private IDocumentRepository _documentRepository;

        public ChildDocumentGridViewModel ViewModel { get; set; }

        public ChildDocumentGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        /// <summary>
        /// Can return ChildDocumentGridViewModel or NotFoundViewModel.
        /// </summary>
        public object Refresh()
        {
            AssertViewModel();

            Document parentDocument = _documentRepository.TryGet(ViewModel.RootDocumentID);
            if (parentDocument == null)
            {
                ViewModelHelper.CreateDocumentNotFoundViewModel();
            }

            bool visible = ViewModel.Visible;

            ViewModel = parentDocument.ToChildDocumentGridViewModel(ViewModel.ChildDocumentTypeID);

            ViewModel.Visible = visible;

            return ViewModel;
        }

        public void Close()
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
