using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentGridPresenter
    {
        private IDocumentRepository _documentRepository;

        public DocumentGridViewModel ViewModel { get; set; }

        private static int _pageSize;

        public DocumentGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
        }

        public DocumentGridViewModel Show(int pageNumber = 1)
        {
            AssertViewModel();

            bool mustCreateViewModel = ViewModel.Pager.PageNumber != pageNumber;
            if (mustCreateViewModel)
            {
                int pageIndex = pageNumber - 1;

                IList<Document> documents = _documentRepository.GetPageOfRootDocumentsOrderedByName(pageIndex * _pageSize, _pageSize);
                int totalCount = _documentRepository.CountRootDocuments();

                ViewModel = documents.ToGridViewModel(pageIndex, _pageSize, totalCount);
            }

            ViewModel.Visible = true;

            return ViewModel;
        }

        public void Refresh()
        {
            AssertViewModel();

            int pageIndex = ViewModel.Pager.PageNumber - 1;

            IList<Document> documents = _documentRepository.GetPageOfRootDocumentsOrderedByName(pageIndex * _pageSize, _pageSize);
            int totalCount = _documentRepository.CountRootDocuments();

            bool visible = ViewModel.Visible;

            // Known bug, not easily solvable and also not a large problem: 
            // A renamed, uncommitted document will not end up in a new place in the list,
            // because the sorting done by the data store, which is not ware of the new name.

            ViewModel = documents.ToGridViewModel(pageIndex, _pageSize, totalCount);
            ViewModel.Visible = visible;
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
