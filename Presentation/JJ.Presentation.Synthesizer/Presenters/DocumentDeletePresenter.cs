using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Presentation;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentDeletePresenter
    {
        private IDocumentRepository _documentRepository;
        private DocumentManager _documentManager;
        private DocumentDeleteViewModel _viewModel;

        public DocumentDeletePresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _documentRepository = repositoryWrapper.DocumentRepository;
            _documentManager = new DocumentManager(repositoryWrapper);
        }

        /// <summary>
        /// Can return DocumentConfirmDeleteViewModel, NotFoundViewModel or DocumentCannotDeleteViewModel.
        /// </summary>
        public object Show(int id)
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
                VoidResult result = _documentManager.CanDelete(document);

                if (!result.Successful)
                {
                    var presenter2 = new DocumentCannotDeletePresenter(_documentRepository);
                    object viewModel2 = presenter2.Show(id, result.Messages);
                    return viewModel2;
                }
                else
                {
                    _viewModel = document.ToDeleteViewModel();
                    return _viewModel;
                }
            }
        }

        /// <summary>
        /// Can return DocumentDeleteConfirmedViewModel or NotFoundViewModel.
        /// </summary>
        public object Confirm(int id)
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
                _documentManager.DeleteWithRelatedEntities(document);

                var presenter2 = new DocumentDeletedPresenter();
                DocumentDeletedViewModel viewModel2 = presenter2.Show();
                return viewModel2;
            }
        }

        public DocumentDeleteViewModel Cancel()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyDocumentDeleteViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
