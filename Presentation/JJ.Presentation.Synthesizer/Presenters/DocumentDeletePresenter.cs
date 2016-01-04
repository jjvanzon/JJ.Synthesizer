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

        public DocumentDeleteViewModel ViewModel { get; private set; }

        public DocumentDeletePresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _documentRepository = repositoryWrapper.DocumentRepository;
            _documentManager = new DocumentManager(repositoryWrapper);
        }

        /// <summary> return DocumentDeleteViewModel, NotFoundViewModel or DocumentCannotDeleteViewModel. </summary>
        public object Show(int id)
        {
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                return ViewModelHelper.CreateDocumentNotFoundViewModel();
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
                    ViewModel = document.ToDeleteViewModel();
                    ViewModel.Visible = true;
                    return ViewModel;
                }
            }
        }

        /// <summary> Can return DocumentDeletedViewModel or NotFoundViewModel. </summary>
        public object Confirm(int id)
        {
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                return ViewModelHelper.CreateDocumentNotFoundViewModel();
            }
            else
            {
                VoidResult result = _documentManager.DeleteWithRelatedEntities(document);

                if (!result.Successful)
                {
                    var presenter2 = new DocumentCannotDeletePresenter(_documentRepository);
                    object viewModel2 = presenter2.Show(id, result.Messages);
                    return viewModel2;
                }
                else
                {
                    var presenter2 = new DocumentDeletedPresenter();
                    presenter2.Show();
                    return presenter2.ViewModel;
                }
            }
        }

        public void Cancel()
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
