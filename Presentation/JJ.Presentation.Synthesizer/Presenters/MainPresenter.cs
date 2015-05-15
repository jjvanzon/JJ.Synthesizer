using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Resources;
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

        private AudioFileOutputDetailsPresenter _audioFileOutputDetailsPresenter;
        private AudioFileOutputListPresenter _audioFileOutputListPresenter;
        private CurveListPresenter _curveListPresenter;
        private DocumentCannotDeletePresenter _documentCannotDeletePresenter;
        private DocumentDeletedPresenter _documentDeletedPresenter;
        private DocumentDeletePresenter _documentDeletePresenter;
        private DocumentDetailsPresenter _documentDetailsPresenter;
        private DocumentListPresenter _documentListPresenter;
        private EffectListPresenter _effectListPresenter;
        private InstrumentListPresenter _instrumentListPresenter;
        private DocumentPropertiesPresenter _documentPropertiesPresenter;
        private DocumentTreePresenter _documentTreePresenter;
        private MenuPresenter _menuPresenter;
        private NotFoundPresenter _notFoundPresenter;
        private PatchDetailsPresenter _patchDetailsPresenter;
        private PatchListPresenter _patchListPresenter;
        private SampleListPresenter _sampleListPresenter;

        private MainViewModel _viewModel;

        public MainPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositoryWrapper = repositoryWrapper;

            _audioFileOutputDetailsPresenter = CreateAudioFileOutputDetailsPresenter();
            _audioFileOutputListPresenter = new AudioFileOutputListPresenter(_repositoryWrapper.AudioFileOutputRepository);
            _curveListPresenter = new CurveListPresenter(_repositoryWrapper.CurveRepository);
            _documentCannotDeletePresenter = new DocumentCannotDeletePresenter(_repositoryWrapper.DocumentRepository);
            _documentDeletedPresenter = new DocumentDeletedPresenter();
            _documentDeletePresenter = new DocumentDeletePresenter(_repositoryWrapper);
            _documentDetailsPresenter = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository);
            _documentListPresenter = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            _documentPropertiesPresenter = new DocumentPropertiesPresenter(_repositoryWrapper.DocumentRepository);
            _documentTreePresenter = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository);
            _effectListPresenter = new EffectListPresenter(_repositoryWrapper.DocumentRepository);
            _instrumentListPresenter = new InstrumentListPresenter(_repositoryWrapper);
            _menuPresenter = new MenuPresenter();
            _notFoundPresenter = new NotFoundPresenter();
            _patchDetailsPresenter = CreatePatchDetailsPresenter();
            _patchListPresenter = new PatchListPresenter(_repositoryWrapper.PatchRepository);
            _sampleListPresenter = new SampleListPresenter(_repositoryWrapper.SampleRepository);

            _dispatchDelegateDictionary = CreateDispatchDelegateDictionary();
        }

        // A lot of times nothing is done with viewModel parameter.
        // This is because this presenter does not concern itself with statelessness yet.
        // If it would, then it would use the incoming view model parameter,
        // instead of the _viewModel field.
        // In this stateful situation, the only time the viewModel parameter is used,
        // is when some incoming data is saved or validated.

        // General

        public MainViewModel Show()
        {
            _viewModel = ViewModelHelper.CreateEmptyMainViewModel();

            MenuViewModel menuViewModel = _menuPresenter.Show();
            DispatchViewModel(menuViewModel);

            DocumentListViewModel documentListViewModel = _documentListPresenter.Show();
            DispatchViewModel(documentListViewModel);

            _viewModel.Title = Titles.ApplicationName;

            return _viewModel;
        }

        public MainViewModel NotFoundOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            object viewModel2 = _notFoundPresenter.OK();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        // Document List

        public MainViewModel DocumentListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var viewModel2 = _documentListPresenter.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var viewModel2 = _documentListPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDetailsCreate(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentDetailsPresenter.Create();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDetailsClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentDetailsPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDetailsSave(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentDetailsPresenter.Save(viewModel.DocumentDetails);

            DispatchViewModel(viewModel2);

            RefreshDocumentList();

            return _viewModel;
        }

        public MainViewModel DocumentDelete(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentDeletePresenter.Show(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentCannotDeleteOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            var viewModel2 = _documentCannotDeletePresenter.OK();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentConfirmDelete(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentDeletePresenter.Confirm(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentCancelDelete(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentDeletePresenter.Cancel();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentDeletedOK(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentDeletedPresenter.OK();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        // The Open Document

        public MainViewModel DocumentOpen(MainViewModel viewModel, int documentID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            object viewModel2 = _documentTreePresenter.Show(documentID);

            var treeViewModel = viewModel2 as DocumentTreeViewModel;
            if (treeViewModel == null)
            {
                DispatchViewModel(viewModel2);
                return _viewModel; 
            }
            _viewModel.DocumentTree = treeViewModel;

            _viewModel.DocumentList.Visible = false;
            _viewModel.DocumentID = documentID;

            Document document = _repositoryWrapper.DocumentRepository.Get(documentID);
            _viewModel.Title = String.Format("{0} - {1}", document.Name, Titles.ApplicationName);

            DocumentPropertiesViewModel documentPropertiesViewModel = (DocumentPropertiesViewModel)_documentPropertiesPresenter.Show(documentID);
            documentPropertiesViewModel.Visible = false;
            _viewModel.DocumentProperties = documentPropertiesViewModel;

            InstrumentListViewModel instrumentsViewModel = (InstrumentListViewModel)_instrumentListPresenter.Show(documentID);
            instrumentsViewModel.Visible = false;
            _viewModel.Instruments = instrumentsViewModel;
            
            EffectListViewModel effectsViewModel = _effectListPresenter.Show(documentID);
            effectsViewModel.Visible = false;
            _viewModel.Effects = effectsViewModel;

            SampleListViewModel sampleListViewModel = _sampleListPresenter.Show(documentID);
            sampleListViewModel.Visible = false;
            _viewModel.Samples = sampleListViewModel;

            CurveListViewModel curveListViewModel = _curveListPresenter.Show(documentID);
            curveListViewModel.Visible = false;
            _viewModel.Curves = curveListViewModel;

            PatchListViewModel patchListViewModel = _patchListPresenter.Show(documentID);
            patchListViewModel.Visible = false;
            _viewModel.Patches = patchListViewModel;

            AudioFileOutputListViewModel audioFileOutputListViewModel = _audioFileOutputListPresenter.Show(documentID);
            audioFileOutputListViewModel.Visible = false;
            _viewModel.AudioFileOutputs = audioFileOutputListViewModel;

            return _viewModel;
        }

        public MainViewModel DocumentTreeShow(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            object viewModel2 = _documentTreePresenter.Show(_viewModel.DocumentID);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel InstrumentListShow(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            object viewModel2 = _instrumentListPresenter.Show(viewModel.DocumentID);
            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel InstrumentListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            TemporarilyAssertViewModelField();

            object viewModel2 = _instrumentListPresenter.Close();
            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel InstrumentListCreate(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _instrumentListPresenter.Create(_viewModel.DocumentID);
            DispatchViewModel(viewModel2);

            RefreshDocumentTree();

            return _viewModel;
        }

        public MainViewModel DocumentTreeClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentTreePresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesShow(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _documentPropertiesPresenter.Show(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.Close(_viewModel.DocumentProperties);

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

            DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.LooseFocus(_viewModel.DocumentProperties);

            DispatchViewModel(viewModel2);

            // TODO: You might only refresh if viewModel2 is valid and if they are visible.
            
            RefreshDocumentList();
            RefreshDocumentTree();

            return _viewModel;
        }

        // Temporary (List) Actions

        public MainViewModel AudioFileOutputListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _audioFileOutputListPresenter.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel AudioFileOutputListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _audioFileOutputListPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel AudioFileOutputDetailsEdit(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _audioFileOutputDetailsPresenter.Edit(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel AudioFileOutputDetailsClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _audioFileOutputDetailsPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel CurveListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _curveListPresenter.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel CurveListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _curveListPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _patchListPresenter.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _patchListPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchDetailsEdit(MainViewModel viewModel, int id)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _patchDetailsPresenter.Edit(id);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchDetailsClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _patchDetailsPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel SampleListShow(MainViewModel viewModel, int pageNumber)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _sampleListPresenter.Show(pageNumber);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel SampleListClose(MainViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            TemporarilyAssertViewModelField();

            object viewModel2 = _sampleListPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        // Private Methods

        private Dictionary<Type, Action<object>> _dispatchDelegateDictionary;

        private Dictionary<Type, Action<object>> CreateDispatchDelegateDictionary()
        {
            var dictionary = new Dictionary<Type, Action<object>>
            {
                 { typeof(MenuViewModel), TryDispatchMenuViewModel },
                 { typeof(DocumentListViewModel), TryDispatchDocumentListViewModel },
                 { typeof(DocumentCannotDeleteViewModel), TryDispatchDocumentCannotDeleteViewModel },
                 { typeof(DocumentDeleteViewModel), TryDispatchDocumentDeleteViewModel },
                 { typeof(DocumentTreeViewModel), TryDispatchDocumentTreeViewModel },
                 { typeof(DocumentPropertiesViewModel), TryDispatchDocumentPropertiesViewModel },
                 { typeof(AudioFileOutputListViewModel), TryDispatchAudioFileOutputListViewModel },
                 { typeof(CurveListViewModel), TryDispatchCurveListViewModel },
                 { typeof(PatchListViewModel), TryDispatchPatchListViewModel },
                 { typeof(SampleListViewModel), TryDispatchSampleListViewModel },
                 { typeof(AudioFileOutputDetailsViewModel), TryDispatchAudioFileOutputDetailsViewModel },
                 { typeof(PatchDetailsViewModel), TryDispatchPatchDetailsViewModel },
                 { typeof(NotFoundViewModel), DispatchNotFoundViewModel },
                 { typeof(DocumentDeletedViewModel), DispatchDocumentDeletedViewModel },
                 { typeof(DocumentDetailsViewModel), DispatchDocumentDetailsViewModel },
                 { typeof(InstrumentListViewModel), DispatchInstrumentListViewModel }
            };

            return dictionary;
        }

        /// <summary>
        /// Applies a view model from a sub-presenter in the right way
        /// to the main view model.
        /// </summary>
        private void DispatchViewModel(object viewModel2)
        {
            if (viewModel2 == null) throw new NullException(() => viewModel2);

            Type viewModelType = viewModel2.GetType();

            Action<object> dispatchDelegate;
            if (!_dispatchDelegateDictionary.TryGetValue(viewModelType, out dispatchDelegate))
            {
                throw new UnexpectedViewModelTypeException(viewModel2);
            }

            dispatchDelegate(viewModel2);
        }

        private void DispatchInstrumentListViewModel(object viewModel2)
        {
            _viewModel.Instruments = (InstrumentListViewModel)viewModel2;
        }

        private void DispatchDocumentDetailsViewModel(object viewModel2)
        {
            _viewModel.DocumentDetails = (DocumentDetailsViewModel)viewModel2;
        }
        
        private void TryDispatchMenuViewModel(object viewModel2)
        {
            _viewModel.Menu = (MenuViewModel)viewModel2;
        }

        private void TryDispatchDocumentListViewModel(object viewModel2)
        {
            _viewModel.DocumentList = (DocumentListViewModel)viewModel2;
        }

        private void TryDispatchDocumentCannotDeleteViewModel(object viewModel2)
        {
            _viewModel.DocumentCannotDelete = (DocumentCannotDeleteViewModel)viewModel2;
        }

        private void TryDispatchDocumentDeleteViewModel(object viewModel2)
        {
            _viewModel.DocumentDelete = (DocumentDeleteViewModel)viewModel2;
        }

        private void TryDispatchDocumentTreeViewModel(object viewModel2)
        {
            _viewModel.DocumentTree = (DocumentTreeViewModel)viewModel2;
        }

        private void TryDispatchDocumentPropertiesViewModel(object viewModel2)
        {
            _viewModel.DocumentProperties = (DocumentPropertiesViewModel)viewModel2;
        }

        private void TryDispatchAudioFileOutputListViewModel(object viewModel2)
        {
            _viewModel.AudioFileOutputs = (AudioFileOutputListViewModel)viewModel2;
        }

        private void TryDispatchCurveListViewModel(object viewModel2)
        {
            _viewModel.Curves = (CurveListViewModel)viewModel2;
        }

        private void TryDispatchPatchListViewModel(object viewModel2)
        {
            _viewModel.Patches = (PatchListViewModel)viewModel2;
        }

        private void TryDispatchSampleListViewModel(object viewModel2)
        {
            _viewModel.Samples = (SampleListViewModel)viewModel2;
        }

        private void TryDispatchAudioFileOutputDetailsViewModel(object viewModel2)
        {
            _viewModel.TemporaryAudioFileOutputDetails = (AudioFileOutputDetailsViewModel)viewModel2;
        }

        private void TryDispatchPatchDetailsViewModel(object viewModel2)
        {
            _viewModel.TemporaryPatchDetails = (PatchDetailsViewModel)viewModel2;
        }

        private void DispatchDocumentDeletedViewModel(object viewModel2)
        {
            var documentDeletedViewModel = (DocumentDeletedViewModel)viewModel2;

            _viewModel.DocumentDeleted = documentDeletedViewModel;

            _viewModel.DocumentDelete.Visible = false;
            _viewModel.DocumentDetails.Visible = false;

            if (!documentDeletedViewModel.Visible)
            {
                RefreshDocumentList();
            }
        }

        private void DispatchNotFoundViewModel(object viewModel2)
        {
            var notFoundViewModel = (NotFoundViewModel)viewModel2;

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
        }

        private void RefreshDocumentList()
        {
            _viewModel.DocumentList = _documentListPresenter.Refresh(_viewModel.DocumentList);
        }

        private void RefreshDocumentTree()
        {
            object viewModel2 = _documentTreePresenter.Refresh(_viewModel.DocumentTree);
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
