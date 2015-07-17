using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ChildDocumentListPresenter
    {
        private IDocumentRepository _documentRepository;

        public ChildDocumentListViewModel ViewModel { get; set; }

        public ChildDocumentListPresenter(IDocumentRepository documentRepository)
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
        /// Can return ChildDocumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Refresh()
        {
            AssertViewModel();

            Document parentDocument = _documentRepository.TryGet(ViewModel.Keys.RootDocumentID);
            if (parentDocument == null)
            {
                ViewModelHelper.CreateDocumentNotFoundViewModel();
            }

            IList<Document> childDocuments = ChildDocumentHelper.GetChildDocuments(parentDocument, ViewModel.Keys.ChildDocumentTypeEnum);

            bool visible = ViewModel.Visible;

            ViewModel = childDocuments.ToChildDocumentListViewModel(parentDocument.ID, ViewModel.Keys.ChildDocumentTypeEnum);

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
