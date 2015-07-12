using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Framework.Business;
using JJ.Framework.Presentation;
using JJ.Data.Synthesizer;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

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
    /// and until that time, all the data must be kept inside the view model.
    /// </summary>
    public class MainPresenter
    {
        private RepositoryWrapper _repositoryWrapper;

        private AudioFileOutputListPresenter _audioFileOutputListPresenter;
        private AudioFileOutputPropertiesPresenter _audioFileOutputPropertiesPresenter;
        private CurveDetailsPresenter _curveDetailsPresenter;
        private CurveListPresenter _curveListPresenter;
        private DocumentCannotDeletePresenter _documentCannotDeletePresenter;
        private DocumentDeletedPresenter _documentDeletedPresenter;
        private DocumentDeletePresenter _documentDeletePresenter;
        private DocumentDetailsPresenter _documentDetailsPresenter;
        private DocumentListPresenter _documentListPresenter;
        private DocumentPropertiesPresenter _documentPropertiesPresenter;
        private DocumentTreePresenter _documentTreePresenter;
        private ChildDocumentListPresenter _effectListPresenter;
        private ChildDocumentListPresenter _instrumentListPresenter;
        private MenuPresenter _menuPresenter;
        private NotFoundPresenter _notFoundPresenter;
        private PatchDetailsPresenter _patchDetailsPresenter;
        private PatchListPresenter _patchListPresenter;
        private SampleListPresenter _sampleListPresenter;
        private SamplePropertiesPresenter _samplePropertiesPresenter;

        private MainViewModel _viewModel;

        private EntityPositionManager _entityPositionManager;
        private DocumentManager _documentManager;
        private PatchManager _patchManager;
        private CurveManager _curveManager;
        private SampleManager _sampleManager;
        private AudioFileOutputManager _audioFileOutputManager;

        public MainPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositoryWrapper = repositoryWrapper;

            _audioFileOutputListPresenter = new AudioFileOutputListPresenter(_repositoryWrapper.DocumentRepository);
            _audioFileOutputPropertiesPresenter = new AudioFileOutputPropertiesPresenter(new AudioFileOutputRepositories(_repositoryWrapper));
            _curveDetailsPresenter = new CurveDetailsPresenter(
                _repositoryWrapper.CurveRepository, 
                _repositoryWrapper.NodeRepository, 
                _repositoryWrapper.NodeTypeRepository, 
                _repositoryWrapper.IDRepository);
            _curveListPresenter = new CurveListPresenter(_repositoryWrapper.DocumentRepository);
            _documentCannotDeletePresenter = new DocumentCannotDeletePresenter(_repositoryWrapper.DocumentRepository);
            _documentDeletedPresenter = new DocumentDeletedPresenter();
            _documentDeletePresenter = new DocumentDeletePresenter(_repositoryWrapper);
            _documentDetailsPresenter = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository, _repositoryWrapper.IDRepository);
            _documentListPresenter = new DocumentListPresenter(_repositoryWrapper.DocumentRepository, _repositoryWrapper.IDRepository);
            _documentPropertiesPresenter = new DocumentPropertiesPresenter(_repositoryWrapper.DocumentRepository, _repositoryWrapper.IDRepository);
            _documentTreePresenter = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository, _repositoryWrapper.IDRepository);
            _effectListPresenter = new ChildDocumentListPresenter(_repositoryWrapper);
            _instrumentListPresenter = new ChildDocumentListPresenter(_repositoryWrapper);
            _menuPresenter = new MenuPresenter();
            _notFoundPresenter = new NotFoundPresenter();
            _patchDetailsPresenter = _patchDetailsPresenter = new PatchDetailsPresenter(
                _repositoryWrapper.PatchRepository,
                _repositoryWrapper.OperatorRepository,
                _repositoryWrapper.OperatorTypeRepository,
                _repositoryWrapper.InletRepository,
                _repositoryWrapper.OutletRepository,
                _repositoryWrapper.EntityPositionRepository,
                _repositoryWrapper.CurveRepository,
                _repositoryWrapper.SampleRepository,
                _repositoryWrapper.IDRepository);
            _patchListPresenter = new PatchListPresenter(_repositoryWrapper.DocumentRepository);
            _sampleListPresenter = new SampleListPresenter(_repositoryWrapper.DocumentRepository);
            _samplePropertiesPresenter = new SamplePropertiesPresenter(new SampleRepositories(_repositoryWrapper));

            _documentManager = new DocumentManager(repositoryWrapper);
            _patchManager = new PatchManager(
                _repositoryWrapper.PatchRepository,
                _repositoryWrapper.OperatorRepository, 
                _repositoryWrapper.InletRepository, 
                _repositoryWrapper.OutletRepository,
                _repositoryWrapper.CurveRepository,
                _repositoryWrapper.SampleRepository,
                _repositoryWrapper.EntityPositionRepository);
            _curveManager = new CurveManager(_repositoryWrapper.CurveRepository, _repositoryWrapper.NodeRepository);
            _sampleManager = new SampleManager(new SampleRepositories(_repositoryWrapper));
            _audioFileOutputManager = new AudioFileOutputManager(
                _repositoryWrapper.AudioFileOutputRepository, 
                _repositoryWrapper.AudioFileOutputChannelRepository, 
                _repositoryWrapper.SampleDataTypeRepository, 
                _repositoryWrapper.SpeakerSetupRepository, 
                _repositoryWrapper.AudioFileFormatRepository,
                _repositoryWrapper.CurveRepository,
                _repositoryWrapper.SampleRepository,
                _repositoryWrapper.IDRepository);
            _entityPositionManager = new EntityPositionManager(_repositoryWrapper.EntityPositionRepository);

            _dispatchDelegateDictionary = CreateDispatchDelegateDictionary();
        }

        // General

        public MainViewModel Show()
        {
            try
            {
                _viewModel = ViewModelHelper.CreateEmptyMainViewModel(_repositoryWrapper.OperatorTypeRepository);

                MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
                DispatchViewModel(menuViewModel, null);

                DocumentListViewModel documentListViewModel = _documentListPresenter.Show();
                DispatchViewModel(documentListViewModel, null);

                _viewModel.WindowTitle = Titles.ApplicationName;
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel NotFoundOK(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _notFoundPresenter.OK();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PopupMessagesOK(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                _viewModel.PopupMessages = new List<Message> { };
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // Document List

        public MainViewModel DocumentListShow(MainViewModel userInput, int pageNumber)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                var viewModel2 = _documentListPresenter.Show(pageNumber);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                var viewModel2 = _documentListPresenter.Close();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentDetailsCreate(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentDetailsPresenter.Create();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentDetailsClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentDetailsPresenter.Close();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentDetailsSave(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentDetailsPresenter.Save(userInput.DocumentDetails);

                DispatchViewModel(viewModel2, null);

                RefreshDocumentList();
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentDelete(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentDeletePresenter.Show(id);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentCannotDeleteOK(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                var viewModel2 = _documentCannotDeletePresenter.OK();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentConfirmDelete(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentDeletePresenter.Confirm(id);

                if (viewModel2 is DocumentDeletedViewModel)
                {
                    _repositoryWrapper.Commit();
                }

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentCancelDelete(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentDeletePresenter.Cancel();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentDeletedOK(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentDeletedPresenter.OK();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // Document Actions

        public MainViewModel DocumentOpen(MainViewModel userInput, int documentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                if (_viewModel.Document.IsOpen)
                {
                    // If you do not clear the presenters, they will remember the view model,
                    // so if you open another document and then open the original one,
                    // the original unsaved data will be shown in the presenters,
                    // because they do not think they need to refresh their view models,
                    // since it is the same document.
                    _audioFileOutputListPresenter.Clear();
                    _audioFileOutputPropertiesPresenter.Clear();
                    _curveDetailsPresenter.Clear();
                    _curveListPresenter.Clear();
                    _documentPropertiesPresenter.Clear();
                    _documentTreePresenter.Clear();
                    _effectListPresenter.Clear();
                    _instrumentListPresenter.Clear();
                    _patchDetailsPresenter.Clear();
                    _patchListPresenter.Clear();
                    _sampleListPresenter.Clear();
                    _samplePropertiesPresenter.Clear();
                }

                Document document = _repositoryWrapper.DocumentRepository.Get(documentID);

                _viewModel.Document = document.ToViewModel(_repositoryWrapper, _entityPositionManager);

                _viewModel.WindowTitle = String.Format("{0} - {1}", document.Name, Titles.ApplicationName);
                _viewModel.Menu = _menuPresenter.Show(documentIsOpen: true);

                _viewModel.DocumentList.Visible = false;
                _viewModel.Document.DocumentTree.Visible = true;

                _viewModel.Document.IsOpen = true;
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentSave(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (userInput.Document == null) throw new NullException(() => userInput.Document);

            try
            {
                EnsureViewModel(userInput);

                Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

                IValidator validator = new DocumentValidator_Recursive(document, _repositoryWrapper, alreadyDone: new HashSet<object>());
                IValidator warningsValidator = new DocumentWarningValidator_Recursive(document, _repositoryWrapper.SampleRepository, new HashSet<object>());

                if (!validator.IsValid)
                {
                    _repositoryWrapper.Rollback();
                }
                else
                {
                    _repositoryWrapper.Commit();
                }

                _viewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                _viewModel.WarningMessages = warningsValidator.ValidationMessages.ToCanonical();

                return _viewModel;
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public MainViewModel DocumentClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                if (_viewModel.Document.IsOpen)
                {
                    _viewModel.Document = ViewModelHelper.CreateEmptyDocumentViewModel();
                    _viewModel.WindowTitle = Titles.ApplicationName;
                    _viewModel.Menu = _menuPresenter.Show(documentIsOpen: false);

                    _audioFileOutputListPresenter.Clear();
                    _audioFileOutputPropertiesPresenter.Clear();
                    _curveDetailsPresenter.Clear();
                    _curveListPresenter.Clear();
                    _documentPropertiesPresenter.Clear();
                    _documentTreePresenter.Clear();
                    _effectListPresenter.Clear();
                    _instrumentListPresenter.Clear();
                    _patchDetailsPresenter.Clear();
                    _patchListPresenter.Clear();
                    _sampleListPresenter.Clear();
                    _samplePropertiesPresenter.Clear();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesShow(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentPropertiesPresenter.Show(id);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.Close(userInput.Document.DocumentProperties);

                DispatchViewModel(viewModel2, null);

                if (viewModel2.Successful)
                {
                    RefreshDocumentList();
                    RefreshDocumentTree();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesLoseFocus(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.LoseFocus(userInput.Document.DocumentProperties);

                DispatchViewModel(viewModel2, null);

                if (viewModel2.Successful)
                {
                    RefreshDocumentList();
                    RefreshDocumentTree();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentTreeShow(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentTreePresenter.Show(_viewModel.Document.ID);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentTreeExpandNode(MainViewModel userInput, int nodeIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentTreePresenter.ExpandNode(userInput.Document.DocumentTree, nodeIndex);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentTreeCollapseNode(MainViewModel userInput, int nodeIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentTreePresenter.CollapseNode(userInput.Document.DocumentTree, nodeIndex);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel DocumentTreeClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _documentTreePresenter.Close();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // AudioFileOutput Actions

        public MainViewModel AudioFileOutputListShow(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _audioFileOutputListPresenter.Show(userInput.Document.ID);
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel AudioFileOutputListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _audioFileOutputListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel AudioFileOutputCreate(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.LinkTo(document);

                ISideEffect sideEffect = new AudioFileOutput_SideEffect_GenerateName(audioFileOutput);
                sideEffect.Execute();

                // ToViewModel
                AudioFileOutputListItemViewModel listItemViewModel = audioFileOutput.ToListItemViewModel();
                _viewModel.Document.AudioFileOutputList.List.Add(listItemViewModel);
                _viewModel.Document.AudioFileOutputList.List = _viewModel.Document.AudioFileOutputList.List.OrderBy(x => x.Name).ToList();

                AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(_repositoryWrapper.AudioFileFormatRepository, _repositoryWrapper.SampleDataTypeRepository, _repositoryWrapper.SpeakerSetupRepository);
                _viewModel.Document.AudioFileOutputPropertiesList.Add(propertiesViewModel);
                _viewModel.Document.AudioFileOutputPropertiesList = _viewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel AudioFileOutputDelete(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                int listIndex = _viewModel.Document.AudioFileOutputPropertiesList.IndexOf(x => x.Entity.ID == id);

                // 'Business' / ToViewModel
                _viewModel.Document.AudioFileOutputPropertiesList.RemoveAt(listIndex);
                _viewModel.Document.AudioFileOutputList.List.RemoveAt(listIndex);

                // No need to do ToEntity, 
                // because we are not executing any additional business logic or refreshing 
                // that uses the entity models.
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel AudioFileOutputPropertiesShow(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                int listIndex = _viewModel.Document.AudioFileOutputPropertiesList.IndexOf(x => x.Entity.ID == id);
                AudioFileOutputPropertiesViewModel propertiesViewModel = _viewModel.Document.AudioFileOutputPropertiesList[listIndex];
                _audioFileOutputPropertiesPresenter.ViewModel = propertiesViewModel;
                object viewModel2 = _audioFileOutputPropertiesPresenter.Show(propertiesViewModel);

                DispatchViewModel(viewModel2, new AlternativeChildDocumentItemKey { EntityListIndex = listIndex });
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel AudioFileOutputPropertiesClose(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

                int listIndex = _viewModel.Document.AudioFileOutputPropertiesList.IndexOf(x => x.Entity.ID == id);
                AudioFileOutputPropertiesViewModel propertiesViewModel = _viewModel.Document.AudioFileOutputPropertiesList[listIndex];
                AudioFileOutputPropertiesViewModel viewModel2 = _audioFileOutputPropertiesPresenter.Close(propertiesViewModel);

                if (viewModel2.Successful)
                {
                    // Update properties list
                    _viewModel.Document.AudioFileOutputPropertiesList = _viewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();

                    // Update list
                    RefreshAudioFileOutputList();
                }

                DispatchViewModel(viewModel2, new AlternativeChildDocumentItemKey { EntityListIndex = listIndex });
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel AudioFileOutputPropertiesLoseFocus(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                Document document = _viewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                int listIndex = _viewModel.Document.AudioFileOutputPropertiesList.IndexOf(x => x.Entity.ID == id);
                AudioFileOutputPropertiesViewModel propertiesViewModel = _viewModel.Document.AudioFileOutputPropertiesList[listIndex];
                AudioFileOutputPropertiesViewModel viewModel2 = _audioFileOutputPropertiesPresenter.LoseFocus(propertiesViewModel);

                if (viewModel2.Successful)
                {
                    // Update properties list
                    _viewModel.Document.AudioFileOutputPropertiesList = _viewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();

                    // Update list
                    RefreshAudioFileOutputList();
                }

                DispatchViewModel(viewModel2, new AlternativeChildDocumentItemKey { EntityListIndex = listIndex });
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // Curve Actions

        public MainViewModel CurveListShow(MainViewModel userInput, int? childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // Needed to create uncommitted child documents.
                if (childDocumentID.HasValue)
                {
                    Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                }
                
                object viewModel2 = _curveListPresenter.Show(userInput.Document.ID, childDocumentID);
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel CurveListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _curveListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel CurveCreate(MainViewModel userInput, int? childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentID, _repositoryWrapper.DocumentRepository);
                Curve curve = _repositoryWrapper.CurveRepository.Create();
                curve.ID = _repositoryWrapper.IDRepository.GetID();
                curve.LinkTo(document);

                ISideEffect sideEffect = new Curve_SideEffect_GenerateName(curve);
                sideEffect.Execute();

                // ToViewModel
                CurveListViewModel curveListViewModel = ChildDocumentHelper.GetCurveListViewModel(userInput.Document, document.ID);
                CurveListItemViewModel listItemViewModel = curve.ToListItemViewModel();
                curveListViewModel.List.Add(listItemViewModel);
                curveListViewModel.List = curveListViewModel.List.OrderBy(x => x.Name).ToList();

                IList<CurveDetailsViewModel> curveDetailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels_ByDocumentID(userInput.Document, document.ID);
                CurveDetailsViewModel curveDetailsViewModel = curve.ToDetailsViewModel(_repositoryWrapper.NodeTypeRepository);
                curveDetailsViewModels.Add(curveDetailsViewModel);

                IList<CurveDetailsViewModel> curveDetailsViewModelsSorted = curveDetailsViewModels.OrderBy(x => x.Entity.Name).ToList();
                curveDetailsViewModels.Clear();
                curveDetailsViewModels.AddRange(curveDetailsViewModelsSorted);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel CurveDelete(MainViewModel userInput, int curveID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Curve curve = ChildDocumentHelper.TryGetCurve(rootDocument, curveID);
                if (curve == null)
                {
                    NotFoundViewModel notFoundViewModel = CreateNotFoundViewModel<Curve>();
                    DispatchViewModel(notFoundViewModel, null);
                    return _viewModel;
                }
                int documentID = curve.Document.ID;

                // Business
                VoidResult result = _curveManager.DeleteWithRelatedEntities(curve);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<CurveDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels_ByDocumentID(_viewModel.Document, documentID);
                    int listIndex = detailsViewModels.IndexOf(x => x.Entity.ID == curveID);
                    detailsViewModels.RemoveAt(listIndex);

                    CurveListViewModel listViewModel = ChildDocumentHelper.GetCurveListViewModel(_viewModel.Document, documentID);
                    listViewModel.List.RemoveAt(listIndex);
                }
                else
                {
                    // ToViewModel
                    _viewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel CurveDetailsShow(MainViewModel userInput, int curveID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativeCurveKey(userInput.Document, curveID);
                CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.GetCurveDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _curveDetailsPresenter.Show(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel CurveDetailsClose(MainViewModel userInput, int curveID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativeCurveKey(userInput.Document, curveID);
                CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.GetCurveDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _curveDetailsPresenter.Close(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel CurveDetailsLoseFocus(MainViewModel userInput, int curveID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativeCurveKey(userInput.Document, curveID);
                CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.GetCurveDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _curveDetailsPresenter.LoseFocus(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // Effect Actions

        public MainViewModel EffectListShow(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _effectListPresenter.Show(userInput.Document.ID, ChildDocumentTypeEnum.Effect);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel EffectListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _effectListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel EffectCreate(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document parentDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                Document effect = _repositoryWrapper.DocumentRepository.Create();
                effect.ID = _repositoryWrapper.IDRepository.GetID();
                effect.LinkEffectToDocument(parentDocument);

                ISideEffect sideEffect = new Effect_SideEffect_GenerateName(effect);
                sideEffect.Execute();

                // ToViewModel
                ChildDocumentListItemViewModel listItemViewModel = effect.ToChildDocumentListItemViewModel();
                _viewModel.Document.EffectList.List.Add(listItemViewModel);
                _viewModel.Document.EffectList.List = _viewModel.Document.EffectList.List.OrderBy(x => x.Name).ToList();

                ChildDocumentPropertiesViewModel propertiesViewModel = effect.ToChildDocumentPropertiesViewModel();
                _viewModel.Document.EffectPropertiesList.Add(propertiesViewModel);
                _viewModel.Document.EffectPropertiesList = _viewModel.Document.EffectPropertiesList.OrderBy(x => x.Name).ToList();

                ChildDocumentViewModel documentViewModel = effect.ToChildDocumentViewModel(_repositoryWrapper, _entityPositionManager);
                _viewModel.Document.EffectDocumentList.Add(documentViewModel);
                _viewModel.Document.EffectDocumentList = _viewModel.Document.EffectDocumentList.OrderBy(x => x.Name).ToList();

                RefreshDocumentTree();
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel EffectDelete(MainViewModel userInput, int effectDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToViewModel Only
                int listIndex = _viewModel.Document.EffectList.List.IndexOf(x => x.ID == effectDocumentID);

                _viewModel.Document.EffectList.List.RemoveAt(listIndex);
                _viewModel.Document.EffectPropertiesList.RemoveAt(listIndex);
                _viewModel.Document.EffectDocumentList.RemoveAt(listIndex);
                _viewModel.Document.DocumentTree.Effects.RemoveAt(listIndex);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // Instrument Actions

        public MainViewModel InstrumentListShow(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _instrumentListPresenter.Show(userInput.Document.ID, ChildDocumentTypeEnum.Instrument);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel InstrumentListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _instrumentListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel InstrumentCreate(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document parentDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                Document instrument = _repositoryWrapper.DocumentRepository.Create();
                instrument.ID = _repositoryWrapper.IDRepository.GetID();
                instrument.LinkInstrumentToDocument(parentDocument);

                ISideEffect sideEffect = new Instrument_SideEffect_GenerateName(instrument);
                sideEffect.Execute();

                // ToViewModel
                ChildDocumentListItemViewModel listItemViewModel = instrument.ToChildDocumentListItemViewModel();
                _viewModel.Document.InstrumentList.List.Add(listItemViewModel);
                _viewModel.Document.InstrumentList.List = _viewModel.Document.InstrumentList.List.OrderBy(x => x.Name).ToList();

                ChildDocumentPropertiesViewModel propertiesViewModel = instrument.ToChildDocumentPropertiesViewModel();
                _viewModel.Document.InstrumentPropertiesList.Add(propertiesViewModel);
                _viewModel.Document.InstrumentPropertiesList = _viewModel.Document.InstrumentPropertiesList.OrderBy(x => x.Name).ToList();

                ChildDocumentViewModel documentViewModel = instrument.ToChildDocumentViewModel(_repositoryWrapper, _entityPositionManager);
                _viewModel.Document.InstrumentDocumentList.Add(documentViewModel);
                _viewModel.Document.InstrumentDocumentList = _viewModel.Document.InstrumentDocumentList.OrderBy(x => x.Name).ToList();

                RefreshDocumentTree();
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel InstrumentDelete(MainViewModel userInput, int instrumentDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToViewModel Only
                int listIndex = _viewModel.Document.InstrumentList.List.IndexOf(x => x.ID == instrumentDocumentID);
                _viewModel.Document.InstrumentList.List.RemoveAt(listIndex);
                _viewModel.Document.InstrumentPropertiesList.RemoveAt(listIndex);
                _viewModel.Document.InstrumentDocumentList.RemoveAt(listIndex);
                _viewModel.Document.DocumentTree.Instruments.RemoveAt(listIndex);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // Patch Actions

        public MainViewModel PatchListShow(MainViewModel userInput, int? childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);
                
                // Needed to create uncommitted child documents.
                if (childDocumentID.HasValue)
                {
                    Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                }
                
                object viewModel2 = _patchListPresenter.Show(userInput.Document.ID, childDocumentID);
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _patchListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchCreate(MainViewModel userInput, int? childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentID, _repositoryWrapper.DocumentRepository);

                // Business
                Patch patch = _repositoryWrapper.PatchRepository.Create();
                patch.ID = _repositoryWrapper.IDRepository.GetID();
                patch.LinkTo(document);

                ISideEffect sideEffect = new Patch_SideEffect_GenerateName(patch);
                sideEffect.Execute();

                // ToViewModel
                PatchListViewModel listViewModel = ChildDocumentHelper.GetPatchListViewModel(userInput.Document, document.ID);
                PatchListItemViewModel listItemViewModel = patch.ToListItemViewModel();
                listViewModel.List.Add(listItemViewModel);
                listViewModel.List = listViewModel.List.OrderBy(x => x.Name).ToList();

                IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByDocumentID(userInput.Document, document.ID);
                PatchDetailsViewModel detailsViewModel = patch.ToDetailsViewModel(_repositoryWrapper.OperatorTypeRepository, _entityPositionManager);
                detailsViewModels.Add(detailsViewModel);

                IList<PatchDetailsViewModel> propertyViewModelsSorted = detailsViewModels.OrderBy(x => x.Entity.Name).ToList();
                propertyViewModelsSorted.Clear();
                propertyViewModelsSorted.AddRange(propertyViewModelsSorted);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDelete(MainViewModel userInput, int patchID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = ChildDocumentHelper.TryGetPatch(rootDocument, patchID);
                if (patch == null)
                {
                    NotFoundViewModel notFoundViewModel = CreateNotFoundViewModel<Patch>();
                    DispatchViewModel(notFoundViewModel, null);
                    return _viewModel;
                }
                int documentID = patch.Document.ID;

                // Business
                VoidResult result = _patchManager.DeleteWithRelatedEntities(patch);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByDocumentID(_viewModel.Document, documentID);
                    int listIndex = detailsViewModels.IndexOf(x => x.Entity.ID == patchID);
                    detailsViewModels.RemoveAt(listIndex);

                    PatchListViewModel listViewModel = ChildDocumentHelper.GetPatchListViewModel(_viewModel.Document, documentID);
                    listViewModel.List.RemoveAt(listIndex);
                }
                else
                {
                    // ToViewModel
                    _viewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDetailsShow(MainViewModel userInput, int patchID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _patchDetailsPresenter.Show(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDetailsClose(MainViewModel userInput, int patchID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = ChildDocumentHelper.GetPatch(rootDocument, patchID);
                int documentID = patch.Document.ID;

                // Get the right partial ViewModel
                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByAlternativeKey(userInput.Document, key);
                PatchDetailsViewModel detailsViewModel = detailsViewModels[key.EntityListIndex];

                // Partial Action
                PatchDetailsViewModel viewModel2 = _patchDetailsPresenter.Close(detailsViewModel);

                if (viewModel2.Successful)
                {
                    // Update details list 
                    IList<PatchDetailsViewModel> detailsViewModelsSorted = detailsViewModels.OrderBy(x => x.Entity.Name).ToList();
                    detailsViewModels.Clear();
                    detailsViewModels.AddRange(detailsViewModelsSorted);

                    // Update list
                    PatchListViewModel listViewModel = ChildDocumentHelper.GetPatchListViewModel(userInput.Document, documentID);
                    RefreshPatchList(listViewModel);
                }

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDetailsLoseFocus(MainViewModel userInput, int patchID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = ChildDocumentHelper.GetPatch(rootDocument, patchID);
                int documentID = patch.Document.ID;

                // Get the right partial ViewModel
                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByAlternativeKey(userInput.Document, key);
                PatchDetailsViewModel detailsViewModel = detailsViewModels[key.EntityListIndex];

                // Partial Action
                PatchDetailsViewModel viewModel2 = _patchDetailsPresenter.LoseFocus(detailsViewModel);

                if (viewModel2.Successful)
                {
                    // Update details list 
                    IList<PatchDetailsViewModel> detailsViewModelsSorted = detailsViewModels.OrderBy(x => x.Entity.Name).ToList();
                    detailsViewModels.Clear();
                    detailsViewModels.AddRange(detailsViewModelsSorted);

                    // Update list
                    PatchListViewModel listViewModel = ChildDocumentHelper.GetPatchListViewModel(userInput.Document, documentID);
                    RefreshPatchList(listViewModel);
                }

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDetailsAddOperator(MainViewModel userInput, int patchID, int operatorTypeID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _patchDetailsPresenter.AddOperator(detailsViewModel, operatorTypeID);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDetailsMoveOperator(MainViewModel userInput, int patchID, int operatorID, float centerX, float centerY)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _patchDetailsPresenter.MoveOperator(detailsViewModel, operatorID, centerX, centerY);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDetailsChangeInputOutlet(MainViewModel userInput, int patchID, int inletID, int inputOutletID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _patchDetailsPresenter.ChangeInputOutlet(detailsViewModel, inletID, inputOutletID);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDetailsSelectOperator(MainViewModel userInput, int patchID, int operatorID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _patchDetailsPresenter.SelectOperator(detailsViewModel, operatorID);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        /// <summary>
        /// Deletes the selected operator. Does not delete anything, if no operator is selected.
        /// </summary>
        public MainViewModel PatchDetailsDeleteOperator(MainViewModel userInput, int patchID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _patchDetailsPresenter.DeleteOperator(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchDetailsSetValue(MainViewModel userInput, int patchID, string value)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                PatchDetailsViewModel viewModel2 = _patchDetailsPresenter.SetValue(detailsViewModel, value);

                // Move messages to popup messages, because the default dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
                _viewModel.PopupMessages.AddRange(viewModel2.ValidationMessages);
                viewModel2.ValidationMessages.Clear();

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel PatchPlay(MainViewModel userInput, int patchID, double duration, string sampleFilePath, string outputFilePath)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativePatchKey(userInput.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(userInput.Document, key);

                PatchDetailsViewModel viewModel2 = _patchDetailsPresenter.Play(detailsViewModel, duration, sampleFilePath, outputFilePath, _repositoryWrapper);

                // Move messages to popup messages, because the default dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
                _viewModel.PopupMessages.AddRange(viewModel2.ValidationMessages);
                viewModel2.ValidationMessages.Clear();

                _viewModel.Successful = viewModel2.Successful;

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // Sample Actions

        public MainViewModel SampleListShow(MainViewModel userInput, int? childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // Needed to create uncommitted child documents.
                if (childDocumentID.HasValue)
                {
                    Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                }

                object viewModel2 = _sampleListPresenter.Show(userInput.Document.ID, childDocumentID);
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel SampleListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                object viewModel2 = _sampleListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel SampleCreate(MainViewModel userInput, int? childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentID, _repositoryWrapper.DocumentRepository);

                // Business
                Sample sample = _sampleManager.CreateSample();
                sample.LinkTo(document);

                ISideEffect sideEffect = new Sample_SideEffect_GenerateName(sample);
                sideEffect.Execute();

                // ToViewModel
                SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(userInput.Document, document.ID);
                SampleListItemViewModel listItemViewModel = sample.ToListItemViewModel();
                listViewModel.List.Add(listItemViewModel);
                listViewModel.List = listViewModel.List.OrderBy(x => x.Name).ToList();

                IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByDocumentID(userInput.Document, document.ID);
                SamplePropertiesViewModel propertiesViewModel = sample.ToPropertiesViewModel(new SampleRepositories(_repositoryWrapper));
                propertiesViewModels.Add(propertiesViewModel);

                IList<SamplePropertiesViewModel> propertiesViewModelsSorted = propertiesViewModels.OrderBy(x => x.Entity.Name).ToList();
                propertiesViewModels.Clear();
                propertiesViewModels.AddRange(propertiesViewModelsSorted);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel SampleDelete(MainViewModel userInput, int sampleID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Sample sample = ChildDocumentHelper.TryGetSample(rootDocument, sampleID);
                if (sample == null)
                {
                    NotFoundViewModel notFoundViewModel = CreateNotFoundViewModel<Sample>();
                    DispatchViewModel(notFoundViewModel, null);
                    return _viewModel;
                }
                int documentID = sample.Document.ID;

                // Business
                VoidResult result = _sampleManager.DeleteWithRelatedEntities(sample);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByDocumentID(_viewModel.Document, documentID);
                    int listIndex = propertiesViewModels.IndexOf(x => x.Entity.ID == sampleID);
                    propertiesViewModels.RemoveAt(listIndex);

                    SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(_viewModel.Document, documentID);
                    listViewModel.List.RemoveAt(listIndex);
                }
                else
                {
                    // ToViewModel
                    _viewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel SamplePropertiesShow(MainViewModel userInput, int sampleID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativeSampleKey(userInput.Document, sampleID);
                SamplePropertiesViewModel propertiesViewModel = ChildDocumentHelper.GetSamplePropertiesViewModel_ByAlternativeKey(userInput.Document, key);

                object viewModel2 = _samplePropertiesPresenter.Show(propertiesViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel SamplePropertiesClose(MainViewModel userInput, int sampleID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Sample sample = ChildDocumentHelper.GetSample(rootDocument, sampleID);
                int documentID = sample.Document.ID;

                // Get the right partial ViewModel
                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativeSampleKey(userInput.Document, sampleID);
                IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByAlternativeKey(userInput.Document, key);
                SamplePropertiesViewModel propertiesViewModel = propertiesViewModels[key.EntityListIndex];

                // Partial Action
                SamplePropertiesViewModel viewModel2 = _samplePropertiesPresenter.Close(propertiesViewModel);

                if (viewModel2.Successful)
                {
                    // Update properties list 
                    IList<SamplePropertiesViewModel> propertiesViewModelsSorted = propertiesViewModels.OrderBy(x => x.Entity.Name).ToList();
                    propertiesViewModels.Clear();
                    propertiesViewModels.AddRange(propertiesViewModelsSorted);

                    // Update list
                    SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(userInput.Document, documentID);
                    RefreshSampleList(listViewModel);
                }

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        public MainViewModel SamplePropertiesLoseFocus(MainViewModel userInput, int sampleID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            try
            {
                EnsureViewModel(userInput);

                // ToEntity
                Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                Sample sample = ChildDocumentHelper.GetSample(rootDocument, sampleID);
                int documentID = sample.Document.ID;

                // Get the right partial ViewModel
                AlternativeChildDocumentItemKey key = ChildDocumentHelper.GetAlternativeSampleKey(userInput.Document, sampleID);
                IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByAlternativeKey(userInput.Document, key);
                SamplePropertiesViewModel propertiesViewModel = propertiesViewModels[key.EntityListIndex];

                // Partial Action
                SamplePropertiesViewModel viewModel2 = _samplePropertiesPresenter.LoseFocus(propertiesViewModel);

                if (viewModel2.Successful)
                {
                    // Update properties list 
                    IList<SamplePropertiesViewModel> propertiesViewModelsSorted = propertiesViewModels.OrderBy(x => x.Entity.Name).ToList();
                    propertiesViewModels.Clear();
                    propertiesViewModels.AddRange(propertiesViewModelsSorted);

                    // Update list
                    SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(userInput.Document, documentID);
                    RefreshSampleList(listViewModel);
                }

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }

            return _viewModel;
        }

        // DispatchViewModel

        private Dictionary<Type, Action<object, AlternativeChildDocumentItemKey>> _dispatchDelegateDictionary;

        private Dictionary<Type, Action<object, AlternativeChildDocumentItemKey>> CreateDispatchDelegateDictionary()
        {
            var dictionary = new Dictionary<Type, Action<object, AlternativeChildDocumentItemKey>>
            {
                { typeof(AudioFileOutputListViewModel), DispatchAudioFileOutputListViewModel },
                { typeof(AudioFileOutputPropertiesViewModel), DispatchAudioFileOutputPropertiesViewModel },
                { typeof(ChildDocumentListViewModel), DispatchChildDocumentListViewModel },
                { typeof(ChildDocumentPropertiesViewModel), DispatchChildDocumentPropertiesViewModel },
                { typeof(CurveDetailsViewModel), DispatchCurveDetailsViewModel },
                { typeof(CurveListViewModel), DispatchCurveListViewModel },
                { typeof(DocumentCannotDeleteViewModel), DispatchDocumentCannotDeleteViewModel },
                { typeof(DocumentDeletedViewModel), DispatchDocumentDeletedViewModel },
                { typeof(DocumentDeleteViewModel), DispatchDocumentDeleteViewModel },
                { typeof(DocumentDetailsViewModel), DispatchDocumentDetailsViewModel },
                { typeof(DocumentListViewModel), DispatchDocumentListViewModel },
                { typeof(DocumentPropertiesViewModel), DispatchDocumentPropertiesViewModel },
                { typeof(DocumentTreeViewModel), DispatchDocumentTreeViewModel },
                { typeof(MenuViewModel), DispatchMenuViewModel },
                { typeof(NotFoundViewModel), DispatchNotFoundViewModel },
                { typeof(PatchDetailsViewModel), DispatchPatchDetailsViewModel },
                { typeof(PatchListViewModel), DispatchPatchListViewModel },
                { typeof(SampleListViewModel), DispatchSampleListViewModel },
                { typeof(SamplePropertiesViewModel), DispatchSamplePropertiesViewModel },
            };

            return dictionary;
        }

        /// <summary>
        /// Applies a view model from a sub-presenter in the right way
        /// to the main view model.
        /// </summary>
        private void DispatchViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            if (viewModel2 == null) throw new NullException(() => viewModel2);

            Type viewModelType = viewModel2.GetType();

            Action<object, AlternativeChildDocumentItemKey> dispatchDelegate;
            if (!_dispatchDelegateDictionary.TryGetValue(viewModelType, out dispatchDelegate))
            {
                throw new UnexpectedViewModelTypeException(viewModel2);
            }

            dispatchDelegate(viewModel2, alternativeKey);
        }

        private void DispatchAudioFileOutputListViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            _viewModel.Document.AudioFileOutputList = (AudioFileOutputListViewModel)viewModel2;

            if (_viewModel.Document.AudioFileOutputList.Visible)
            {
                HideAllListAndDetailViewModels();
                _viewModel.Document.AudioFileOutputList.Visible = true;
            }
        }

        private void DispatchAudioFileOutputPropertiesViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            if (alternativeKey == null) throw new NullException(() => alternativeKey);

            var audioFileOutputPropertiesViewModel = (AudioFileOutputPropertiesViewModel)viewModel2;

            _viewModel.Document.AudioFileOutputPropertiesList[alternativeKey.EntityListIndex] = audioFileOutputPropertiesViewModel;

            if (audioFileOutputPropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                audioFileOutputPropertiesViewModel.Visible = true;
            }

            _viewModel.PopupMessages.AddRange(audioFileOutputPropertiesViewModel.ValidationMessages);
            audioFileOutputPropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchChildDocumentListViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            var childDocumentListViewModel = (ChildDocumentListViewModel)viewModel2;

            switch (childDocumentListViewModel.Keys.ChildDocumentTypeEnum)
            {
                case ChildDocumentTypeEnum.Instrument:
                    _viewModel.Document.InstrumentList = childDocumentListViewModel;
                    break;

                case ChildDocumentTypeEnum.Effect:
                    _viewModel.Document.EffectList = childDocumentListViewModel;
                    break;

                default:
                    throw new ValueNotSupportedException(childDocumentListViewModel.Keys.ChildDocumentTypeEnum);
            }

            if (childDocumentListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                childDocumentListViewModel.Visible = true;
            }
        }

        private void DispatchChildDocumentPropertiesViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            var childDocumentPropertiesViewModel = (ChildDocumentPropertiesViewModel)viewModel2;

            int id = childDocumentPropertiesViewModel.ID;

            int? listIndex;
            
            listIndex = _viewModel.Document.InstrumentPropertiesList.TryGetIndexOf(x => x.ID == id);
            if (listIndex.HasValue)
            {
                _viewModel.Document.InstrumentPropertiesList[listIndex.Value] = childDocumentPropertiesViewModel;
            }

            listIndex = _viewModel.Document.EffectPropertiesList.TryGetIndexOf(x => x.ID == id);
            if (listIndex.HasValue)
            {
                _viewModel.Document.EffectPropertiesList[listIndex.Value] = childDocumentPropertiesViewModel;
            }

            if (!listIndex.HasValue)
            {
                throw new Exception(String.Format("Neither _viewModel.Document.InstrumentPropertiesList or _viewModel.Document.EffectPropertiesList contain an item with ID '{0}'.", id));
            }

            if (childDocumentPropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                childDocumentPropertiesViewModel.Visible = true;
            }

            _viewModel.PopupMessages.AddRange(childDocumentPropertiesViewModel.ValidationMessages);
            childDocumentPropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveDetailsViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            if (alternativeKey == null) throw new NullException(() => alternativeKey);

            var curveDetailsViewModel = (CurveDetailsViewModel)viewModel2;

            IList<CurveDetailsViewModel> list = ChildDocumentHelper.GetCurveDetailsViewModels_ByAlternativeKey(_viewModel.Document, alternativeKey);
            list[alternativeKey.EntityListIndex] = curveDetailsViewModel;

            if (curveDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                curveDetailsViewModel.Visible = true;
            }

            _viewModel.ValidationMessages.AddRange(curveDetailsViewModel.ValidationMessages);
            curveDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveListViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            CurveListViewModel curveListViewModel = (CurveListViewModel)viewModel2;

            if (!curveListViewModel.ChildDocumentID.HasValue)
            {
                _viewModel.Document.CurveList = curveListViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(_viewModel.Document, curveListViewModel.ChildDocumentID.Value);
                childDocumentViewModel.CurveList = curveListViewModel;
            }

            if (curveListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                curveListViewModel.Visible = true;
            }
        }

        private void DispatchDocumentCannotDeleteViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            _viewModel.DocumentCannotDelete = (DocumentCannotDeleteViewModel)viewModel2;
        }

        private void DispatchDocumentDeletedViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            var documentDeletedViewModel = (DocumentDeletedViewModel)viewModel2;

            _viewModel.DocumentDeleted = documentDeletedViewModel;

            // TODO: This is quite an assumption.
            _viewModel.DocumentDelete.Visible = false;
            _viewModel.DocumentDetails.Visible = false;

            if (!documentDeletedViewModel.Visible)
            {
                // Also: this might better be done in the action method.
                RefreshDocumentList();
            }
        }

        private void DispatchDocumentDeleteViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            _viewModel.DocumentDelete = (DocumentDeleteViewModel)viewModel2;
        }

        private void DispatchDocumentDetailsViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            _viewModel.DocumentDetails = (DocumentDetailsViewModel)viewModel2;

            if (_viewModel.DocumentDetails.Visible)
            {
                HideAllListAndDetailViewModels();
                _viewModel.DocumentDetails.Visible = true;
            }

            _viewModel.PopupMessages.AddRange(_viewModel.DocumentDetails.ValidationMessages);
            _viewModel.DocumentDetails.ValidationMessages.Clear();
        }

        private void DispatchDocumentListViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            _viewModel.DocumentList = (DocumentListViewModel)viewModel2;

            if (_viewModel.DocumentList.Visible)
            {
                HideAllListAndDetailViewModels();
                _viewModel.DocumentList.Visible = true;
            }
        }

        private void DispatchDocumentPropertiesViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            _viewModel.Document.DocumentProperties = (DocumentPropertiesViewModel)viewModel2;

            if (_viewModel.Document.DocumentProperties.Visible)
            {
                HideAllPropertiesViewModels();
                _viewModel.Document.DocumentProperties.Visible = true;
            }

            _viewModel.PopupMessages.AddRange(_viewModel.Document.DocumentProperties.ValidationMessages);
            _viewModel.Document.DocumentProperties.ValidationMessages.Clear();
        }

        private void DispatchDocumentTreeViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            _viewModel.Document.DocumentTree = (DocumentTreeViewModel)viewModel2;
        }

        private void DispatchMenuViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            _viewModel.Menu = (MenuViewModel)viewModel2;
        }

        private void DispatchNotFoundViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
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

        private void DispatchPatchDetailsViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            if (alternativeKey == null) throw new NullException(() => alternativeKey);

            var patchDetailsViewModel = (PatchDetailsViewModel)viewModel2;

            IList<PatchDetailsViewModel> list = ChildDocumentHelper.GetPatchDetailsViewModels_ByAlternativeKey(_viewModel.Document, alternativeKey);
            list[alternativeKey.EntityListIndex] = patchDetailsViewModel;

            if (patchDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                patchDetailsViewModel.Visible = true;
            }

            _viewModel.ValidationMessages.AddRange(patchDetailsViewModel.ValidationMessages);
            patchDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchListViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            PatchListViewModel patchListViewModel = (PatchListViewModel)viewModel2;

            if (!patchListViewModel.ChildDocumentID.HasValue)
            {
                _viewModel.Document.PatchList = patchListViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(_viewModel.Document, patchListViewModel.ChildDocumentID.Value);
                childDocumentViewModel.PatchList = patchListViewModel;
            }

            if (patchListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                patchListViewModel.Visible = true;
            }
        }

        private void DispatchSamplePropertiesViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            if (alternativeKey == null) throw new NullException(() => alternativeKey);

            var samplePropertiesViewModel = (SamplePropertiesViewModel)viewModel2;

            IList<SamplePropertiesViewModel> list = ChildDocumentHelper.GetSamplePropertiesViewModels_ByAlternativeKey(_viewModel.Document, alternativeKey);
            list[alternativeKey.EntityListIndex] = samplePropertiesViewModel;

            if (samplePropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                samplePropertiesViewModel.Visible = true;
            }

            _viewModel.ValidationMessages.AddRange(samplePropertiesViewModel.ValidationMessages);
            samplePropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchSampleListViewModel(object viewModel2, AlternativeChildDocumentItemKey alternativeKey)
        {
            SampleListViewModel sampleListViewModel = (SampleListViewModel)viewModel2;

            if (!sampleListViewModel.ChildDocumentID.HasValue)
            {
                _viewModel.Document.SampleList = sampleListViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(_viewModel.Document, sampleListViewModel.ChildDocumentID.Value);
                childDocumentViewModel.SampleList = sampleListViewModel;
            }

            if (sampleListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                sampleListViewModel.Visible = true;
            }
        }

        // Helpers

        private void EnsureViewModel(MainViewModel userInput)
        {
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.Document.ID != userInput.Document.ID;
            if (mustCreateViewModel)
            {
                Document entity = userInput.Document.ToEntityWithRelatedEntities(_repositoryWrapper);
                _viewModel = CreateViewModel(userInput, entity);
            }
        }

        /// <summary>
        /// First do a ToEntity before you call this method.
        /// The user input is only used to yield over non-persisted properties.
        /// </summary>
        private MainViewModel CreateViewModel(MainViewModel userInput, Document entity)
        {
            MainViewModel mainViewModel = ViewModelHelper.CreateEmptyMainViewModel(_repositoryWrapper.OperatorTypeRepository);

            DocumentViewModel documentViewModel = entity.ToViewModel(_repositoryWrapper, _entityPositionManager);
            mainViewModel.Document = documentViewModel;

            MenuViewModel menuViewModel = _menuPresenter.Show(userInput.Document.IsOpen);
            mainViewModel.Menu = menuViewModel;

            DocumentListViewModel documentListViewModel = _documentListPresenter.Show();
            mainViewModel.DocumentList = documentListViewModel;

            mainViewModel.WindowTitle = Titles.ApplicationName;

            // TODO: Add other parts of the view model than the document?

            // TODO: Add other non-persisted data (such as validation messages) and the visibility of views.

            return mainViewModel;
        }

        private void HideAllListAndDetailViewModels()
        {
            _viewModel.DocumentList.Visible = false;
            _viewModel.DocumentDetails.Visible = false;

            _viewModel.Document.InstrumentList.Visible = false;
            _viewModel.Document.EffectList.Visible = false;
            _viewModel.Document.SampleList.Visible = false;
            _viewModel.Document.CurveList.Visible = false;
            _viewModel.Document.AudioFileOutputList.Visible = false;
            _viewModel.Document.PatchList.Visible = false;

            foreach (CurveDetailsViewModel curveDetailsViewModel in _viewModel.Document.CurveDetailsList)
            {
                curveDetailsViewModel.Visible = false;
            }

            foreach (PatchDetailsViewModel patchDetailsViewModel in _viewModel.Document.PatchDetailsList)
            {
                patchDetailsViewModel.Visible = false;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in _viewModel.Document.InstrumentDocumentList)
            {
                childDocumentViewModel.SampleList.Visible = false;
                childDocumentViewModel.CurveList.Visible = false;
                childDocumentViewModel.PatchList.Visible = false;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in _viewModel.Document.EffectDocumentList)
            {
                childDocumentViewModel.SampleList.Visible = false;
                childDocumentViewModel.CurveList.Visible = false;
                childDocumentViewModel.PatchList.Visible = false;
            }
        }

        private void HideAllPropertiesViewModels()
        {
            _viewModel.DocumentDetails.Visible = false;
            _viewModel.Document.DocumentProperties.Visible = false;
            _viewModel.Document.AudioFileOutputPropertiesList.ForEach(x => x.Visible = false);
            _viewModel.Document.CurveDetailsList.ForEach(x => x.Visible = false);
            _viewModel.Document.EffectPropertiesList.ForEach(x => x.Visible = false);
            _viewModel.Document.InstrumentPropertiesList.ForEach(x => x.Visible = false);
            _viewModel.Document.SamplePropertiesList.ForEach(x => x.Visible = false);

            // Note that the Samples are the only ones with a Properties view inside the child documents.
            _viewModel.Document.EffectDocumentList.SelectMany(x => x.SamplePropertiesList).ForEach(x => x.Visible = false);
            _viewModel.Document.InstrumentDocumentList.SelectMany(x => x.SamplePropertiesList).ForEach(x => x.Visible = false);
        }

        private void RefreshDocumentList()
        {
            _viewModel.DocumentList = _documentListPresenter.Refresh(_viewModel.DocumentList);
        }

        private void RefreshDocumentTree()
        {
            object viewModel2 = _documentTreePresenter.Refresh(_viewModel.Document.DocumentTree);
            DispatchViewModel(viewModel2, null);
        }

        private void RefreshAudioFileOutputList()
        {
            object viewModel2 = _audioFileOutputListPresenter.Refresh(_viewModel.Document.AudioFileOutputList);
            DispatchViewModel(viewModel2, null);
        }

        private void RefreshSampleList(SampleListViewModel sampleListViewModel)
        {
            object viewModel2 = _sampleListPresenter.Refresh(sampleListViewModel);
            DispatchViewModel(viewModel2, null);
        }

        private void RefreshPatchList(PatchListViewModel patchListViewModel)
        {
            object viewModel2 = _patchListPresenter.Refresh(patchListViewModel);
            DispatchViewModel(viewModel2, null);
        }

        private NotFoundViewModel CreateDocumentNotFoundViewModel()
        {
            NotFoundViewModel viewModel = new NotFoundPresenter().Show(PropertyDisplayNames.Document);
            return viewModel;
        }

        private AudioFileOutputPropertiesViewModel GetAudioFileOutputPropertiesViewModel(int id)
        {
            AudioFileOutputPropertiesViewModel viewModel = _viewModel.Document.AudioFileOutputPropertiesList
                                                                              .Where(x => x.Entity.ID == id)
                                                                              .Single();
            return viewModel;
        }

        private AudioFileOutputListItemViewModel GetAudioFileOutputListItemViewModel(int id)
        {
            AudioFileOutputListItemViewModel viewModel = _viewModel.Document.AudioFileOutputList.List
                                                                            .Where(x => x.ID == id)
                                                                            .Single();
            return viewModel;
        }

        private NotFoundViewModel CreateNotFoundViewModel<TEntity>()
        {
            string entityTypeName = typeof(TEntity).Name;
            string entityTypeDisplayName = ResourceHelper.GetPropertyDisplayName(entityTypeName);

            NotFoundViewModel viewModel = new NotFoundPresenter().Show(entityTypeDisplayName);
            return viewModel;
        }
    }
}
