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
    public class DocumentConfirmDeletePresenter
    {
        private IDocumentRepository _documentRepository;
        private DocumentManager _documentManager;

        public DocumentConfirmDeletePresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _documentRepository = repositoryWrapper.DocumentRepository;
            _documentManager = new DocumentManager(repositoryWrapper);
        }

        /// <summary>
        /// Can return DocumentConfirmDeleteViewModel, DocumentNotFoundViewModel or DocumentCannotDeleteViewModel.
        /// </summary>
        public object Show(int id)
        {
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                var presenter2 = new NotFoundPresenter();
                NotFoundViewModel viewModel2 = presenter2.Show(typeof(Document).Name);
                return viewModel2;
            }
            else
            {
                VoidResult result = _documentManager.CanDelete(document);

                if (!result.Successful)
                {
                    var presenter2 = new DocumentCannotDeletePresenter();
                    DocumentCannotDeleteViewModel viewModel2 = presenter2.Show(document, result.Messages);
                    return viewModel2;
                }
                else
                {
                    DocumentConfirmDeleteViewModel viewModel2 = document.ToConfirmDeleteViewModel();
                    return viewModel2;
                }
            }
        }

        /// <summary>
        /// Can return DocumentDeleteConfirmedViewModel or NotFoundViewModel.
        /// </summary>
        public object Confirm(DocumentConfirmDeleteViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Document document = _documentRepository.TryGet(viewModel.Document.ID);
            if (document == null)
            {
                var presenter2 = new NotFoundPresenter();
                NotFoundViewModel viewModel2 = presenter2.Show(typeof(Document).Name);
                return viewModel2;
            }
            else
            {
                _documentManager.Delete(document);

                var presenter2 = new DocumentDeleteConfirmedPresenter();
                DocumentDeleteConfirmedViewModel viewModel2 = presenter2.Show();
                return viewModel2;
            }
        }

        public DocumentConfirmDeleteViewModel Cancel(DocumentConfirmDeleteViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            viewModel.Visible = false;
            return viewModel;
        }
    }
}
