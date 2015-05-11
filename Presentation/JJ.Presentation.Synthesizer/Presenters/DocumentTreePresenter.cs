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
                DocumentTreeViewModel viewModel = document.ToTreeViewModel();
                return viewModel;
            }
        }

        public object Close(int id)
        {
            // TODO: Handle view model better (stateless / stateful hybrid).

            Document document = _documentRepository.Get(id);
            if (document == null)
            {
                var notFoundPresenter = new NotFoundPresenter();
                NotFoundViewModel viewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
                return viewModel;
            }
            else
            {
                DocumentTreeViewModel viewModel = document.ToTreeViewModel();
                viewModel.Visible = false;
                return viewModel;
            }
        }

        /// <summary>
        /// Can return DocumentPropertiesViewModel or NotFoundViewModel.
        /// </summary>
        public object ShowDocumentProperties(DocumentTreeViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            var presenter2 = new DocumentPropertiesPresenter(_documentRepository);
            object viewModel2 = presenter2.Show(viewModel.ID);
            return viewModel2;
        }
    }
}
