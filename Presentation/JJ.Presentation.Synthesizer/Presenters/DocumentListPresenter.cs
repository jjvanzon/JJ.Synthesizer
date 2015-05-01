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

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentListPresenter
    {
        private IDocumentRepository _documentRepository;

        private static int _pageSize;
        private static int _maxVisiblePageNumbers;

        public DocumentListPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
            _maxVisiblePageNumbers = config.MaxVisiblePageNumbers;
        }

        public DocumentListViewModel Show(int pageNumber)
        {
            int pageIndex = pageNumber - 1;
            IList<Document> documents = _documentRepository.GetPage(pageIndex * _pageSize, _pageSize);

            int count = _documentRepository.Count();

            DocumentListViewModel viewModel = new DocumentListViewModel
            {
                List = documents.Select(x => x.ToIDName()).ToArray(),
                Pager = PagerViewModelFactory.Create(pageIndex, _pageSize, count, _maxVisiblePageNumbers)
            };

            return viewModel;
        }

        public DocumentDetailsViewModel Create()
        {
            var presenter2 = new DocumentDetailsPresenter(_documentRepository);
            DocumentDetailsViewModel viewModel2 = presenter2.Create();
            return viewModel2;
        }

        //public DocumentListViewModel Add(DocumentListViewModel viewModel)
        //{
        //    if (viewModel == null) throw new NullException(() => viewModel);

        //    foreach (
            
        //    throw new NotImplementedException();
        //}
    }
}
