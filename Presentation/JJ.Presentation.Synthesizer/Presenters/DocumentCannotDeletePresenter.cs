using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentCannotDeletePresenter
    {
        private IDocumentRepository _documentRepository;

        private DocumentCannotDeleteViewModel _viewModel;

        public DocumentCannotDeletePresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Can return NotFoundViewModel or DocumentCannotDeleteViewModel.
        /// </summary>
        public object Show(int id, IList<Message> messages)
        {
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                var presenter2 = new NotFoundPresenter();
                NotFoundViewModel viewModel2 = presenter2.Show(PropertyDisplayNames.Document);
                return viewModel2;
            }
            else
            {
                _viewModel = document.ToCannotDeleteViewModel(messages);
                _viewModel.Visible = true;
                return _viewModel;
            }
        }

        public DocumentCannotDeleteViewModel OK()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyDocumentCannotDeleteViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
