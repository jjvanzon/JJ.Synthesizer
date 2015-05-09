using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    /// <summary>
    /// Panels in the application are so intricately coordinated
    /// that one action in one part of the screen can
    /// affect several other panels on the screen.
    /// So you cannot manage each part of the screen individually.
    /// 
    /// That is why all panels are managed in a single presenter and view model.
    /// Otherwise you would get difficult coordination of application navigation 
    /// in the platform-specific application code, where it does not belong.
    /// 
    /// Also: most non-visible parts of the view model must be kept alive inside the view model,
    /// because the whole document will not be persistent until you hit save,
    /// and until that time, all the data must be kept inside the view models.
    /// </summary>
    public class MainPresenter
    {
        private RepositoryWrapper _repositoryWrapper;
        private MainViewModel _viewModel;

        public MainPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositoryWrapper = repositoryWrapper;
        }

        // A lot of times nothing is done with viewModel parameter.
        // This is because this presenter does not concern itself with statelessness yet.
        // If it would, then it would use the incoming view model parameter,
        // instead of the _viewModel field.
        // In this stateful situation, the only time the viewModel parameter is used,
        // is when some incoming data is saved or validated.

        // General

        public MainViewModel Open()
        {
            MainViewModel viewModel = ViewModelHelper.CreateEmptyMainViewModel();

            var menuPresenter = new MenuPresenter();
            viewModel.Menu = menuPresenter.Show();

            var documentListPresenter = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            viewModel.DocumentList = documentListPresenter.Show(1);

            _viewModel = viewModel;

            return viewModel;
        }

        public MainViewModel NotFoundOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new NotFoundPresenter();
            NotFoundViewModel viewModel2 = presenter2.OK(_viewModel.NotFound);

            _viewModel.NotFound = viewModel2;

            return _viewModel;
        }

        // Document 

        public MainViewModel ShowDocumentList(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            _viewModel.DocumentList = presenter2.Show(pageNumber);

            return _viewModel;
        }

        public MainViewModel CloseDocumentList(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            _viewModel.DocumentList.Visible = false;

            return _viewModel;
        }

        public MainViewModel CreateDocument(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository);
            _viewModel.DocumentDetails = presenter2.Create();

            return _viewModel;
        }

        public MainViewModel CloseDocumentDetails(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            _viewModel.DocumentDetails.Visible = false;

            return _viewModel;
        }

        public MainViewModel SaveDocumentDetails(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository);
            object viewModel2 = presenter2.Save(viewModel.DocumentDetails);

            var detailsViewModel = viewModel2 as DocumentDetailsViewModel;
            if (detailsViewModel != null)
            {
                _viewModel.DocumentDetails = detailsViewModel;
                return _viewModel;
            }

            var previousViewModel = viewModel2 as PreviousViewModel;
            if (previousViewModel != null)
            {
                _viewModel.DocumentDetails.Visible = false;
                RefreshDocumentList();
                return _viewModel;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        public MainViewModel OpenDocument(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository);
            object viewModel2 = presenter2.Show(id);

            var treeViewModel = viewModel2 as DocumentTreeViewModel;
            if (treeViewModel != null)
            {
                _viewModel.DocumentTree = treeViewModel;
                _viewModel.DocumentList.Visible = false;
                return _viewModel;

                // TODO: I probably need to fill in more data in the view model,
                // since all state of the document must be in the view model.
            }

            var notFoundViewModel = viewModel2 as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                _viewModel.NotFound = notFoundViewModel;
                RefreshDocumentList();
                return _viewModel;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        public MainViewModel DeleteDocument(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentConfirmDeletePresenter(_repositoryWrapper);
            object viewModel2 = presenter2.Show(id);

            var notFoundViewModel = viewModel2 as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                _viewModel.NotFound = notFoundViewModel;
                RefreshDocumentList();
                return _viewModel;
            }

            var cannotDeleteViewModel = viewModel2 as DocumentCannotDeleteViewModel;
            if (cannotDeleteViewModel != null)
            {
                _viewModel.DocumentCannotDelete = cannotDeleteViewModel;
                return _viewModel;
            }

            var confirmDeleteViewModel = viewModel2 as DocumentConfirmDeleteViewModel;
            if (confirmDeleteViewModel != null)
            {
                _viewModel.DocumentConfirmDelete = confirmDeleteViewModel;
                return _viewModel;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        public MainViewModel DocumentCannotDeleteOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentCannotDeletePresenter();
            DocumentCannotDeleteViewModel viewModel2 = presenter2.OK(_viewModel.DocumentCannotDelete);

            _viewModel.DocumentCannotDelete = viewModel2;

            return _viewModel;
        }

        public MainViewModel ConfirmDeleteDocument(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentConfirmDeletePresenter(_repositoryWrapper);
            object viewModel2 = presenter2.Confirm(_viewModel.DocumentConfirmDelete);

            var notFoundViewModel = viewModel2 as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                _viewModel.NotFound = notFoundViewModel;
                RefreshDocumentList();
                return _viewModel;
            }

            var deleteConfirmedViewModel = viewModel2 as DocumentDeleteConfirmedViewModel;
            if (deleteConfirmedViewModel != null)
            {
                _viewModel.DocumentDeleteConfirmed = deleteConfirmedViewModel;
                _viewModel.DocumentConfirmDelete.Visible = false;
                _viewModel.DocumentDetails.Visible = false;
                return _viewModel;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        public MainViewModel DocumentDeleteConfirmedOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDeleteConfirmedPresenter();
            _viewModel.DocumentDeleteConfirmed = presenter2.OK();

            RefreshDocumentList();

            return _viewModel;
        }

        public MainViewModel CancelConfirmDeleteDocument(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentConfirmDeletePresenter(_repositoryWrapper);
            DocumentConfirmDeleteViewModel viewModel2 = presenter2.Cancel(_viewModel.DocumentConfirmDelete);
            _viewModel.DocumentConfirmDelete = viewModel2;

            return _viewModel;
        }

        // Private Methods

        private void RefreshDocumentList()
        {
            var presenter2 = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            bool originalVisible = _viewModel.DocumentList.Visible;
            int originalPageNumber = _viewModel.DocumentList.Pager.PageNumber;
            _viewModel.DocumentList = presenter2.Show(originalPageNumber);
            _viewModel.DocumentList.Visible = originalVisible;
        }

        private void TemporarilyAssertViewModelField()
        {
            if (_viewModel == null)
            {
                // TODO: ViewModel should be converted to entities and back to view model again,
                // to work in a stateless environment.
                throw new Exception("_viewModel field is not assigned and code is not adapted to work in a stateless environment.");
            }
        }
    }
}
