using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDeletePresenter
    {
        private IDocumentRepository _documentRepository;
        private DocumentManager _documentManager;

        public DocumentDeletePresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _documentRepository = repositoryWrapper.DocumentRepository;
            _documentManager = new DocumentManager(repositoryWrapper);
        }

        /// <summary> return DocumentDeleteViewModel, NotFoundViewModel or DocumentCannotDeleteViewModel. </summary>
        public object Show(int id)
        {
            // GetEntity
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                // Redirect
                return ViewModelHelper.CreateDocumentNotFoundViewModel();
            }
            else
            {
                // Business
                VoidResult result = _documentManager.CanDelete(document);

                if (!result.Successful)
                {
                    // Redirect
                    var presenter2 = new DocumentCannotDeletePresenter(_documentRepository);
                    object viewModel2 = presenter2.Show(id, result.Messages);
                    return viewModel2;
                }
                else
                {
                    // ToViewModel
                    DocumentDeleteViewModel viewModel = document.ToDeleteViewModel();

                    // Non-Persisted
                    viewModel.Visible = true;

                    return viewModel;
                }
            }
        }

        /// <summary> Can return DocumentDeletedViewModel or NotFoundViewModel. </summary>
        public object Confirm(int id)
        {
            // GetEntity
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                // Redirect
                return ViewModelHelper.CreateDocumentNotFoundViewModel();
            }
            else
            {
                // Business
                VoidResult result = _documentManager.DeleteWithRelatedEntities(document);

                if (!result.Successful)
                {
                    // Redirect
                    var presenter2 = new DocumentCannotDeletePresenter(_documentRepository);
                    object viewModel = presenter2.Show(id, result.Messages);
                    return viewModel;
                }
                else
                {
                    // REdirect
                    var presenter2 = new DocumentDeletedPresenter();
                    object viewModel = presenter2.Show();
                    return viewModel;
                }
            }
        }

        public object Cancel(DocumentDeleteViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Document document = _documentRepository.TryGet(userInput.Document.ID);
            if (document == null)
            {
                // Redirect
                return ViewModelHelper.CreateDocumentNotFoundViewModel();
            }

            // ToViewModel
            DocumentDeleteViewModel viewModel = document.ToDeleteViewModel();

            // Non-Persisted
            viewModel.Visible = false;

            return viewModel;
        }
    }
}
