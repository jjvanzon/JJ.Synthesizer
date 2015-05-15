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
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.ID != id;

            if (mustCreateViewModel)
            {
                Document document = _documentRepository.TryGet(id);
                if (document == null)
                {
                    var notFoundPresenter = new NotFoundPresenter();
                    NotFoundViewModel viewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
                    return viewModel;
                }
                else
                {
                    _viewModel = document.ToTreeViewModel();
                }
            }
            else
            {
                _viewModel.Visible = true;
            }

            return _viewModel;
        }

        public object Refresh(DocumentTreeViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Document document = _documentRepository.TryGet(viewModel.ID);
            if (document == null)
            {
                var notFoundPresenter = new NotFoundPresenter();
                NotFoundViewModel viewModel2 = notFoundPresenter.Show(PropertyDisplayNames.Document);
                return viewModel2;
            }
            else
            {
                _viewModel = document.ToTreeViewModel();

                _viewModel.Visible = viewModel.Visible;
            }

            return _viewModel;
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
        public object Properties(int id)
        {
            var presenter2 = new DocumentPropertiesPresenter(_documentRepository);
            object viewModel2 = presenter2.Show(id);
            return viewModel2;
        }
    }
}
