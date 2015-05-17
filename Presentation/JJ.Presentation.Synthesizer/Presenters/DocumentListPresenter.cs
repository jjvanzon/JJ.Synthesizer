using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Configuration;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Presentation;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentListPresenter
    {
        private IDocumentRepository _documentRepository;
        private DocumentListViewModel _viewModel;

        private static int _pageSize;

        public DocumentListPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
        }

        public DocumentListViewModel Show(int pageNumber = 1)
        {
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.Pager.PageNumber != pageNumber;

            if (mustCreateViewModel)
            {
                int pageIndex = pageNumber - 1;

                IList<Document> documents = _documentRepository.GetPageOfRootDocuments(pageIndex * _pageSize, _pageSize);
                int totalCount = _documentRepository.CountRootDocuments();

                _viewModel = documents.ToListViewModel(pageIndex, _pageSize, totalCount);

                _documentRepository.Rollback();
            }
            else
            {
                _viewModel.Visible = true;
            }

            return _viewModel;
        }

        public DocumentListViewModel Refresh(DocumentListViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            int pageIndex = viewModel.Pager.PageNumber - 1;

            IList<Document> documents = _documentRepository.GetPageOfRootDocuments(pageIndex * _pageSize, _pageSize);
            int totalCount = _documentRepository.CountRootDocuments();

            _viewModel = documents.ToListViewModel(pageIndex, _pageSize, totalCount);

            _viewModel.Visible = viewModel.Visible;

            _documentRepository.Rollback();

            return _viewModel;
        }

        public DocumentDetailsViewModel Create()
        {
            var presenter2 = new DocumentDetailsPresenter(_documentRepository);
            DocumentDetailsViewModel viewModel2 = presenter2.Create();
            return viewModel2;
        }

        /// <summary>
        /// Can return DocumentTreeViewModel or NotFoundViewModel.
        /// </summary>
        public object OpenDocument(int id)
        {
            var presenter2 = new DocumentTreePresenter(_documentRepository);
            object viewModel2 = presenter2.Show(id);
            return viewModel2;
        }

        /// <summary>
        /// Can return DocumentConfirmDeleteViewModel, NotFoundViewModel or DocumentCannotDeleteViewModel.
        /// </summary>
        public object Delete(int id, RepositoryWrapper repositoryWrapper)
        {
            var presenter2 = new DocumentDeletePresenter(repositoryWrapper);
            object viewModel2 = presenter2.Show(id);
            return viewModel2;
        }

        public DocumentListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyDocumentListViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
