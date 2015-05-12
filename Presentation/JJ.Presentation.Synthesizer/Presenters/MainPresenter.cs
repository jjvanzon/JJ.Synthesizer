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
            _viewModel = ViewModelHelper.CreateEmptyMainViewModel();

            object viewModel2;

            var menuPresenter = new MenuPresenter();
            viewModel2 = menuPresenter.Show();

            DispatchViewModel(viewModel2);

            var documentListPresenter = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            viewModel2 = documentListPresenter.Show(1);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel NotFoundOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new NotFoundPresenter();
            object viewModel2 = presenter2.OK(_viewModel.NotFound);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        // Document 

        public MainViewModel DocumentListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            var viewModel2 = presenter2.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            var viewModel2 = presenter2.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDetailsCreate(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository);
            object viewModel2 = presenter2.Create();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDetailsClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository);
            object viewModel2 = presenter2.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDetailsSave(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository);
            object viewModel2 = presenter2.Save(viewModel.DocumentDetails);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentOpen(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository);
            object viewModel2 = presenter2.Show(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDelete(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDeletePresenter(_repositoryWrapper);
            object viewModel2 = presenter2.Show(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentCannotDeleteOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentCannotDeletePresenter();
            var viewModel2 = presenter2.OK(_viewModel.DocumentCannotDelete);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentConfirmDelete(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDeletePresenter(_repositoryWrapper);
            object viewModel2 = presenter2.Confirm(_viewModel.DocumentDelete);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentCancelDelete(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDeletePresenter(_repositoryWrapper);
            object viewModel2 = presenter2.Cancel(_viewModel.DocumentDelete);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDeletedOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentDeletedPresenter();
            object viewModel2 = presenter2.OK();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesShow(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentPropertiesPresenter(_repositoryWrapper.DocumentRepository);
            object viewModel2 = presenter2.Show(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentPropertiesPresenter(_repositoryWrapper.DocumentRepository);
            DocumentPropertiesViewModel viewModel2 = presenter2.Close(_viewModel.DocumentProperties);

            DispatchViewModel(viewModel2);

            bool isValid = !viewModel2.Visible; // TODO: It seems dirty to check success this way.
            if (isValid) 
            {
                RefreshDocumentList();
                RefreshDocumentTree();
            }

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesLoseFocus(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentPropertiesPresenter(_repositoryWrapper.DocumentRepository);
            DocumentPropertiesViewModel viewModel2 = presenter2.LooseFocus(_viewModel.DocumentProperties);

            DispatchViewModel(viewModel2);

            // TODO: You might only refresh if viewModel2 is valid and if they are visible.
            // TODO: I might yield over visibility of panes and other non-persisted things.
            // But that requires a structural solution for all actions.
            
            RefreshDocumentList();
            RefreshDocumentTree();

            return _viewModel;
        }

        public MainViewModel DocumentTreeClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository);
            object viewModel2 = presenter2.Close(_viewModel.DocumentTree.ID);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        // Temporary (List) Actions

        public MainViewModel AudioFileOutputListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new AudioFileOutputListPresenter(_repositoryWrapper.AudioFileOutputRepository);
            object viewModel2 = presenter2.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel AudioFileOutputListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new AudioFileOutputListPresenter(_repositoryWrapper.AudioFileOutputRepository);
            object viewModel2 = presenter2.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel AudioFileOutputDetailsEdit(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            AudioFileOutputDetailsPresenter presenter2 = CreateAudioFileOutputDetailsPresenter();
            object viewModel2 = presenter2.Edit(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel AudioFileOutputDetailsClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            AudioFileOutputDetailsPresenter presenter2 = CreateAudioFileOutputDetailsPresenter();
            object viewModel2 = presenter2.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel CurveListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new CurveListPresenter(_repositoryWrapper.CurveRepository);
            object viewModel2 = presenter2.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel CurveListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new CurveListPresenter(_repositoryWrapper.CurveRepository);
            object viewModel2 = presenter2.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new PatchListPresenter(_repositoryWrapper.PatchRepository);
            object viewModel2 = presenter2.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new PatchListPresenter(_repositoryWrapper.PatchRepository);
            object viewModel2 = presenter2.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchDetailsEdit(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            PatchDetailsPresenter presenter2 = CreatePatchDetailsPresenter();

            object viewModel2 = presenter2.Edit(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchDetailsClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            PatchDetailsPresenter presenter2 = CreatePatchDetailsPresenter();

            object viewModel2 = presenter2.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel SampleListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new SampleListPresenter(_repositoryWrapper.SampleRepository);
            object viewModel2 = presenter2.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel SampleListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var presenter2 = new SampleListPresenter(_repositoryWrapper.SampleRepository);
            object viewModel2 = presenter2.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        // Private Methods

        /// <summary>
        /// Applies a view model from a sub-presenter in the right way
        /// to the main view model.
        /// </summary>
        private void DispatchViewModel(object viewModel2)
        {
            // TODO: Low priority: Create a dictionary of delegates to make dispatching faster?

            var menuViewModel = viewModel2 as MenuViewModel;
            if (menuViewModel != null)
            {
                _viewModel.Menu = menuViewModel;
                return;
            }

            var documentListViewModel = viewModel2 as DocumentListViewModel;
            if (documentListViewModel != null)
            {
                _viewModel.DocumentList = documentListViewModel;
                return;
            }

            var notFoundViewModel = viewModel2 as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                _viewModel.NotFound = notFoundViewModel;

                // HACK: Checking visibility of the NotFound view model
                // prevents refreshing the DocumentList twice:
                // once when showing the NotFound view model,
                // a second time when clicking OK on it.

                // TODO: Low priority: Eventually the NotFoundViewModel will create even more ambiguity,
                // when it is reused for multiple entity types.

                if (notFoundViewModel.Visible)
                {
                    RefreshDocumentList();
                }
                return;
            }

            var documentDeletedViewModel = viewModel2 as DocumentDeletedViewModel;
            if (documentDeletedViewModel != null)
            {
                _viewModel.DocumentDeleted = documentDeletedViewModel;
                _viewModel.DocumentDelete.Visible = false;
                _viewModel.DocumentDetails.Visible = false;

                if (!documentDeletedViewModel.Visible)
                {
                    RefreshDocumentList();
                }

                return;
            }

            var documentCannotDeleteViewModel = viewModel2 as DocumentCannotDeleteViewModel;
            if (documentCannotDeleteViewModel != null)
            {
                _viewModel.DocumentCannotDelete = documentCannotDeleteViewModel;
                return;
            }

            var documentDeleteViewModel = viewModel2 as DocumentDeleteViewModel;
            if (documentDeleteViewModel != null)
            {
                _viewModel.DocumentDelete = documentDeleteViewModel;
                return;
            }

            var documentDetailsViewModel = viewModel2 as DocumentDetailsViewModel;
            if (documentDetailsViewModel != null)
            {
                _viewModel.DocumentDetails = documentDetailsViewModel;

                if (!_viewModel.DocumentDetails.Visible) // Refresh uppon close.
                {
                    RefreshDocumentList();
                }

                return;
            }

            var treeViewModel = viewModel2 as DocumentTreeViewModel;
            if (treeViewModel != null)
            {
                _viewModel.DocumentTree = treeViewModel;
                //_viewModel.DocumentID = treeViewModel.ID;

                // TODO: This is where generlized dispatching is going wrong.
                _viewModel.DocumentList.Visible = false;
                return;

                // TODO: I probably need to fill in more data in the view model,
                // since all state of the document must be in the view model.
            }

            var documentPropertiesViewModel = viewModel2 as DocumentPropertiesViewModel;
            if (documentPropertiesViewModel != null)
            {
                _viewModel.DocumentProperties = documentPropertiesViewModel;
                return;
            }

            var audioFileOutputListViewModel = viewModel2 as AudioFileOutputListViewModel;
            if (audioFileOutputListViewModel != null)
            {
                _viewModel.AudioFileOutputList = audioFileOutputListViewModel;
                return;
            }

            var curveListViewModel = viewModel2 as CurveListViewModel;
            if (curveListViewModel != null)
            {
                _viewModel.CurveList = curveListViewModel;
                return;
            }

            var patchListViewModel = viewModel2 as PatchListViewModel;
            if (patchListViewModel != null)
            {
                _viewModel.PatchList = patchListViewModel;
                return;
            }

            var sampleListViewModel = viewModel2 as SampleListViewModel;
            if (sampleListViewModel != null)
            {
                _viewModel.SampleList = sampleListViewModel;
                return;
            }

            var audioFileOutputDetailsViewModel = viewModel2 as AudioFileOutputDetailsViewModel;
            if (audioFileOutputDetailsViewModel != null)
            {
                _viewModel.AudioFileOutputDetails = audioFileOutputDetailsViewModel;
                return;
            }

            var patchDetailsViewModel = viewModel2 as PatchDetailsViewModel;
            if (patchDetailsViewModel != null)
            {
                _viewModel.PatchDetails = patchDetailsViewModel;
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        private void RefreshDocumentList()
        {
            var presenter2 = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);

            bool originalVisible = _viewModel.DocumentList.Visible;
            int originalPageNumber = _viewModel.DocumentList.Pager.PageNumber;

            _viewModel.DocumentList = presenter2.Show(originalPageNumber);
            _viewModel.DocumentList.Visible = originalVisible;
        }

        private void RefreshDocumentTree()
        {
            var presenter2 = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository);

            bool originalVisible = _viewModel.DocumentTree.Visible;
            int originalID = _viewModel.DocumentTree.ID;

            object viewModel2 = presenter2.Show(originalID);

            var treeViewModel = viewModel2 as DocumentTreeViewModel;
            if (treeViewModel == null)
            {
                treeViewModel.Visible = originalVisible;
            }

            DispatchViewModel(viewModel2);
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

        private PatchDetailsPresenter CreatePatchDetailsPresenter()
        {
            var presenter2 = new PatchDetailsPresenter(
                _repositoryWrapper.PatchRepository,
                _repositoryWrapper.OperatorRepository,
                _repositoryWrapper.InletRepository,
                _repositoryWrapper.OutletRepository,
                _repositoryWrapper.EntityPositionRepository,
                _repositoryWrapper.CurveRepository,
                _repositoryWrapper.SampleRepository);

            return presenter2;
        }

        private AudioFileOutputDetailsPresenter CreateAudioFileOutputDetailsPresenter()
        {
            var presenter2 = new AudioFileOutputDetailsPresenter(
                _repositoryWrapper.AudioFileOutputRepository,
                _repositoryWrapper.AudioFileFormatRepository,
                _repositoryWrapper.SampleDataTypeRepository,
                _repositoryWrapper.SpeakerSetupRepository);

            return presenter2;
        }
    }
}
