using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentTreePresenter
    {
        private IDocumentRepository _documentRepository;

        private DocumentTreeViewModel _viewModel;

        public DocumentTreePresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Can return DocumentTreeViewModel or NotFoundViewModel.
        /// </summary>
        public object Show(int id)
        {
            Document document = _documentRepository.Get(id);
            if (document == null)
            {
                var notFoundPresenter = new NotFoundPresenter();
                NotFoundViewModel viewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
                return viewModel;
            }
            else
            {
                _viewModel = document.ToTreeViewModel();
                return _viewModel;
            }
        }

        public object Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyDocumentTreeViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }

        /// <summary>
        /// Can return DocumentPropertiesViewModel or NotFoundViewModel.
        /// </summary>
        public object ShowDocumentProperties(int id)
        {
            var presenter2 = new DocumentPropertiesPresenter(_documentRepository);
            object viewModel2 = presenter2.Show(id);
            return viewModel2;
        }
    }
}
