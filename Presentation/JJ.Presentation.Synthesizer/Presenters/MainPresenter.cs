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
        private const int DUMMY_LIST_INDEX = 0;
        private const int DUMMY_NODE_INDEX = 0;

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
            _audioFileOutputPropertiesPresenter = CreateAudioFileOutputPropertiesPresenter();
            _curveDetailsPresenter = new CurveDetailsPresenter(_repositoryWrapper.CurveRepository, _repositoryWrapper.NodeRepository, _repositoryWrapper.NodeTypeRepository);
            _curveListPresenter = new CurveListPresenter(_repositoryWrapper.DocumentRepository);
            _documentCannotDeletePresenter = new DocumentCannotDeletePresenter(_repositoryWrapper.DocumentRepository);
            _documentDeletedPresenter = new DocumentDeletedPresenter();
            _documentDeletePresenter = new DocumentDeletePresenter(_repositoryWrapper);
            _documentDetailsPresenter = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository);
            _documentListPresenter = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            _documentPropertiesPresenter = new DocumentPropertiesPresenter(_repositoryWrapper.DocumentRepository);
            _documentTreePresenter = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository);
            _effectListPresenter = new ChildDocumentListPresenter(_repositoryWrapper);
            _instrumentListPresenter = new ChildDocumentListPresenter(_repositoryWrapper);
            _menuPresenter = new MenuPresenter();
            _notFoundPresenter = new NotFoundPresenter();
            _patchDetailsPresenter = CreatePatchDetailsPresenter();
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
                _repositoryWrapper.SampleRepository);
            _entityPositionManager = new EntityPositionManager(_repositoryWrapper.EntityPositionRepository);

            _dispatchDelegateDictionary = CreateDispatchDelegateDictionary();
        }

        // General

        public MainViewModel Show()
        {
            _viewModel = ViewModelHelper.CreateEmptyMainViewModel(_repositoryWrapper.OperatorTypeRepository);

            MenuViewModel menuViewModel = _menuPresenter.Show();
            DispatchViewModel(menuViewModel);

            DocumentListViewModel documentListViewModel = _documentListPresenter.Show();
            DispatchViewModel(documentListViewModel);

            _viewModel.WindowTitle = Titles.ApplicationName;

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel NotFoundOK(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _notFoundPresenter.OK();

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel PopupMessagesOK(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            _viewModel.PopupMessages = new List<Message> { };

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // Document List

        public MainViewModel DocumentListShow(MainViewModel userInput, int pageNumber)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            var viewModel2 = _documentListPresenter.Show(pageNumber);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            var viewModel2 = _documentListPresenter.Close();

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentDetailsCreate(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentDetailsPresenter.Create();

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentDetailsClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentDetailsPresenter.Close();

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentDetailsSave(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentDetailsPresenter.Save(userInput.DocumentDetails);

            DispatchViewModel(viewModel2);

            RefreshDocumentList();

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentDelete(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentDeletePresenter.Show(id);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentCannotDeleteOK(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            var viewModel2 = _documentCannotDeletePresenter.OK();

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentConfirmDelete(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentDeletePresenter.Confirm(id);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentCancelDelete(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentDeletePresenter.Cancel();

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentDeletedOK(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentDeletedPresenter.OK();

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // Document Actions

        public MainViewModel DocumentOpen(MainViewModel userInput, int documentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

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

            _viewModel.DocumentList.Visible = false;
            _viewModel.Document.DocumentTree.Visible = true;

            _viewModel.Document.IsOpen = true;

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentSave(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (userInput.Document == null) throw new NullException(() => userInput.Document);

            EnsureViewModel(userInput);

            Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            IValidator validator = new DocumentValidator_Recursive(document, _repositoryWrapper, alreadyDone: new HashSet<object>());
            _viewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();

            IValidator warningsValidator = new DocumentWarningValidator_Recursive(document);
            _viewModel.WarningMessages = warningsValidator.ValidationMessages.ToCanonical();

            if (!validator.IsValid)
            {
                _repositoryWrapper.Rollback();
            }
            else
            {
                _repositoryWrapper.Commit();
            }

            return _viewModel;
        }

        public MainViewModel DocumentClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            if (_viewModel.Document.IsOpen)
            {
                _viewModel.Document = ViewModelHelper.CreateEmptyDocumentViewModel();
                _viewModel.WindowTitle = Titles.ApplicationName;

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

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesShow(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentPropertiesPresenter.Show(id);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.Close(userInput.Document.DocumentProperties);

            DispatchViewModel(viewModel2);

            if (viewModel2.Successful)
            {
                RefreshDocumentList();
                RefreshDocumentTree();
            }

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentPropertiesLoseFocus(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.LoseFocus(userInput.Document.DocumentProperties);

            DispatchViewModel(viewModel2);

            if (viewModel2.Successful)
            {
                RefreshDocumentList();
                RefreshDocumentTree();
            }

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentTreeShow(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentTreePresenter.Show(_viewModel.Document.ID);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentTreeExpandNode(MainViewModel userInput, int nodeIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentTreePresenter.ExpandNode(userInput.Document.DocumentTree, nodeIndex);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentTreeCollapseNode(MainViewModel userInput, int nodeIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentTreePresenter.CollapseNode(userInput.Document.DocumentTree, nodeIndex);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel DocumentTreeClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _documentTreePresenter.Close();

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // AudioFileOutput Actions

        public MainViewModel AudioFileOutputListShow(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _audioFileOutputListPresenter.Show(userInput.Document.ID);
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel AudioFileOutputListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _audioFileOutputListPresenter.Close();
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel AudioFileOutputCreate(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            // Business
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities();
            audioFileOutput.LinkTo(document);

            ISideEffect sideEffect = new AudioFileOutput_SideEffect_GenerateName(audioFileOutput);
            sideEffect.Execute();

            // ToViewModel
            AudioFileOutputListItemViewModel listItemViewModel = audioFileOutput.ToListItemViewModel(DUMMY_LIST_INDEX);
            _viewModel.Document.AudioFileOutputList.List.Add(listItemViewModel);
            _viewModel.Document.AudioFileOutputList.List = _viewModel.Document.AudioFileOutputList.List.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.AudioFileOutputList.List);

            AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(DUMMY_LIST_INDEX, _repositoryWrapper.AudioFileFormatRepository, _repositoryWrapper.SampleDataTypeRepository, _repositoryWrapper.SpeakerSetupRepository);
            _viewModel.Document.AudioFileOutputPropertiesList.Add(propertiesViewModel);
            _viewModel.Document.AudioFileOutputPropertiesList = _viewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.AudioFileOutputPropertiesList);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel AudioFileOutputDelete(MainViewModel userInput, int listIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // 'Business' / ToViewModel
            _viewModel.Document.AudioFileOutputPropertiesList.RemoveAt(listIndex);
            _viewModel.Document.AudioFileOutputList.List.RemoveAt(listIndex);

            ListIndexHelper.RenumberListIndexes(_viewModel.Document.AudioFileOutputPropertiesList, listIndex);
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.AudioFileOutputList.List, listIndex);

            // No need to do ToEntity, 
            // because we are not executing any additional business logic or refreshing 
            // that uses the entity models.

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel AudioFileOutputPropertiesShow(MainViewModel userInput, int listIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            _audioFileOutputPropertiesPresenter.ViewModel = userInput.Document.AudioFileOutputPropertiesList[listIndex];
            object viewModel2 = _audioFileOutputPropertiesPresenter.Show(userInput.Document.AudioFileOutputPropertiesList[listIndex]);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel AudioFileOutputPropertiesClose(MainViewModel userInput, int listIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // TODO: Can I get away with converting only part of the user input to entities?
            // Do consider that channels reference patch outlets.
            Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            AudioFileOutputPropertiesViewModel viewModel2 = _audioFileOutputPropertiesPresenter.Close(userInput.Document.AudioFileOutputPropertiesList[listIndex]);

            if (viewModel2.Successful)
            {
                // Update properties list
                _viewModel.Document.AudioFileOutputPropertiesList = _viewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();
                ListIndexHelper.RenumberListIndexes(_viewModel.Document.AudioFileOutputPropertiesList);

                // Update list
                RefreshAudioFileOutputList();
            }

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel AudioFileOutputPropertiesLoseFocus(MainViewModel userInput, int listIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // TODO: Can I get away with converting only part of the user input to entities?
            // Do consider that channels reference patch outlets.
            Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            AudioFileOutputPropertiesViewModel viewModel2 = _audioFileOutputPropertiesPresenter.LoseFocus(userInput.Document.AudioFileOutputPropertiesList[listIndex]);

            if (viewModel2.Successful)
            {
                // Update properties list
                _viewModel.Document.AudioFileOutputPropertiesList = _viewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();
                ListIndexHelper.RenumberListIndexes(_viewModel.Document.AudioFileOutputPropertiesList);

                // Update list
                RefreshAudioFileOutputList();
            }

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // Curve Actions

        public MainViewModel CurveListShow(MainViewModel userInput, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            object viewModel2 = _curveListPresenter.Show(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex);
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel CurveListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _curveListPresenter.Close();
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel CurveCreate(MainViewModel userInput, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document parentDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            // Business
            Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex, _repositoryWrapper.DocumentRepository);
            Curve curve = _repositoryWrapper.CurveRepository.Create();
            curve.LinkTo(document);

            ISideEffect sideEffect = new Curve_SideEffect_GenerateName(curve);
            sideEffect.Execute();

            // ToViewModel
            CurveListViewModel curveListViewModel = ChildDocumentHelper.GetCurveListViewModel(userInput.Document, childDocumentTypeEnum, childDocumentListIndex);
            CurveListItemViewModel listItemViewModel = curve.ToListItemViewModel(DUMMY_LIST_INDEX, childDocumentListIndex);
            curveListViewModel.List.Add(listItemViewModel);
            curveListViewModel.List = curveListViewModel.List.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(curveListViewModel.List);

            IList<CurveDetailsViewModel> curvePropertyViewModels = ChildDocumentHelper.GetCurveDetailsViewModels(userInput.Document, childDocumentTypeEnum, childDocumentListIndex);
            CurveDetailsViewModel detailsViewModel = curve.ToDetailsViewModel(document.ID, childDocumentTypeEnum, childDocumentListIndex, DUMMY_LIST_INDEX, _repositoryWrapper.NodeTypeRepository);
            curvePropertyViewModels.Add(detailsViewModel);
            IList<CurveDetailsViewModel> curvePropertyViewModelsSorted = curvePropertyViewModels.OrderBy(x => x.Curve.Name).ToList();
            curvePropertyViewModels.Clear();
            curvePropertyViewModels.AddRange(curvePropertyViewModelsSorted);
            ListIndexHelper.RenumberListIndexes(curvePropertyViewModels);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel CurveDelete(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
            Document document = ChildDocumentHelper.GetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex, _repositoryWrapper.DocumentRepository);

            Curve curve = document.Curves[listIndex];

            // Business
            VoidResult result = _curveManager.DeleteWithRelatedEntities(curve);
            if (result.Successful)
            {
                // ToViewModel
                IList<CurveDetailsViewModel> curveDetailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels(_viewModel.Document, childDocumentTypeEnum, childDocumentListIndex);
                curveDetailsViewModels.RemoveAt(listIndex);
                ListIndexHelper.RenumberListIndexes(curveDetailsViewModels, listIndex);

                CurveListViewModel curveListViewModel = ChildDocumentHelper.GetCurveListViewModel(_viewModel.Document, childDocumentTypeEnum, childDocumentListIndex);
                curveListViewModel.List.RemoveAt(listIndex);
                ListIndexHelper.RenumberListIndexes(curveListViewModel.List, listIndex);
            }
            else
            {
                // ToViewModel
                _viewModel.PopupMessages = result.Messages;
            }

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel CurveDetailsEdit(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            IList<CurveDetailsViewModel> list = GetCurveDetailsViewModels(userInput, childDocumentTypeEnum, childDocumentListIndex);
            CurveDetailsViewModel item = list[listIndex];

            object viewModel2 = _curveDetailsPresenter.Show(item);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel CurveDetailsClose(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            IList<CurveDetailsViewModel> list = GetCurveDetailsViewModels(userInput, childDocumentTypeEnum, childDocumentListIndex);
            CurveDetailsViewModel item = list[listIndex];

            object viewModel2 = _curveDetailsPresenter.Close(item);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel CurveDetailsLoseFocus(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            IList<CurveDetailsViewModel> list = GetCurveDetailsViewModels(userInput, childDocumentTypeEnum, childDocumentListIndex);
            CurveDetailsViewModel item = list[listIndex];

            object viewModel2 = _curveDetailsPresenter.LoseFocus(item);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // Effect Actions

        public MainViewModel EffectListShow(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _effectListPresenter.Show(userInput.Document.ID, ChildDocumentTypeEnum.Effect);
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel EffectListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _effectListPresenter.Close();
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel EffectCreate(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document parentDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            // Business
            Document effect = _repositoryWrapper.DocumentRepository.Create();
            effect.LinkEffectToDocument(parentDocument);

            ISideEffect sideEffect = new Effect_SideEffect_GenerateName(effect);
            sideEffect.Execute();

            // ToViewModel
            ChildDocumentListItemViewModel listItemViewModel = effect.ToChildDocumentListItemViewModel(DUMMY_LIST_INDEX);
            _viewModel.Document.EffectList.List.Add(listItemViewModel);
            _viewModel.Document.EffectList.List = _viewModel.Document.EffectList.List.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.EffectList.List);

            ChildDocumentPropertiesViewModel propertiesViewModel = effect.ToChildDocumentPropertiesViewModel(DUMMY_LIST_INDEX);
            _viewModel.Document.EffectPropertiesList.Add(propertiesViewModel);
            _viewModel.Document.EffectPropertiesList = _viewModel.Document.EffectPropertiesList.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.EffectPropertiesList);

            ChildDocumentViewModel documentViewModel = effect.ToChildDocumentViewModel(DUMMY_LIST_INDEX, _repositoryWrapper, _entityPositionManager);
            _viewModel.Document.EffectDocumentList.Add(documentViewModel);
            _viewModel.Document.EffectDocumentList = _viewModel.Document.EffectDocumentList.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.EffectDocumentList);

            RefreshDocumentTree();

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel EffectDelete(MainViewModel userInput, int listIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToViewModel Only
            _viewModel.Document.EffectList.List.RemoveAt(listIndex);
            _viewModel.Document.EffectPropertiesList.RemoveAt(listIndex);
            _viewModel.Document.EffectDocumentList.RemoveAt(listIndex);
            _viewModel.Document.DocumentTree.Effects.RemoveAt(listIndex);

            ListIndexHelper.RenumberListIndexes(_viewModel.Document.EffectList.List, listIndex);
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.EffectPropertiesList, listIndex);
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.EffectDocumentList, listIndex);
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.DocumentTree.Effects, listIndex);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // Instrument Actions

        public MainViewModel InstrumentListShow(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _instrumentListPresenter.Show(userInput.Document.ID, ChildDocumentTypeEnum.Instrument);
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel InstrumentListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _instrumentListPresenter.Close();
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel InstrumentCreate(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document parentDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            // Business
            Document instrument = _repositoryWrapper.DocumentRepository.Create();
            instrument.LinkInstrumentToDocument(parentDocument);

            ISideEffect sideEffect = new Instrument_SideEffect_GenerateName(instrument);
            sideEffect.Execute();

            // ToViewModel
            ChildDocumentListItemViewModel listItemViewModel = instrument.ToChildDocumentListItemViewModel(DUMMY_LIST_INDEX);
            _viewModel.Document.InstrumentList.List.Add(listItemViewModel);
            _viewModel.Document.InstrumentList.List = _viewModel.Document.InstrumentList.List.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.InstrumentList.List);

            ChildDocumentPropertiesViewModel propertiesViewModel = instrument.ToChildDocumentPropertiesViewModel(DUMMY_LIST_INDEX);
            _viewModel.Document.InstrumentPropertiesList.Add(propertiesViewModel);
            _viewModel.Document.InstrumentPropertiesList = _viewModel.Document.InstrumentPropertiesList.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.InstrumentPropertiesList);

            ChildDocumentViewModel documentViewModel = instrument.ToChildDocumentViewModel(DUMMY_LIST_INDEX, _repositoryWrapper, _entityPositionManager);
            _viewModel.Document.InstrumentDocumentList.Add(documentViewModel);
            _viewModel.Document.InstrumentDocumentList = _viewModel.Document.InstrumentDocumentList.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.InstrumentDocumentList);

            RefreshDocumentTree();

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel InstrumentDelete(MainViewModel userInput, int listIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToViewModel Only
            _viewModel.Document.InstrumentList.List.RemoveAt(listIndex);
            _viewModel.Document.InstrumentPropertiesList.RemoveAt(listIndex);
            _viewModel.Document.InstrumentDocumentList.RemoveAt(listIndex);
            _viewModel.Document.DocumentTree.Instruments.RemoveAt(listIndex);

            ListIndexHelper.RenumberListIndexes(_viewModel.Document.InstrumentList.List, listIndex);
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.InstrumentPropertiesList, listIndex);
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.InstrumentDocumentList, listIndex);
            ListIndexHelper.RenumberListIndexes(_viewModel.Document.DocumentTree.Instruments, listIndex);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // Patch Actions

        public MainViewModel PatchListShow(MainViewModel userInput, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            object viewModel2 = _patchListPresenter.Show(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex);
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel PatchListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _patchListPresenter.Close();
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel PatchCreate(MainViewModel userInput, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document parentDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            // Business
            Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex, _repositoryWrapper.DocumentRepository);
            Patch patch = _repositoryWrapper.PatchRepository.Create();
            patch.LinkTo(document);

            ISideEffect sideEffect = new Patch_SideEffect_GenerateName(patch);
            sideEffect.Execute();

            // ToViewModel
            PatchListViewModel patchListViewModel = ChildDocumentHelper.GetPatchListViewModel(userInput.Document, childDocumentTypeEnum, childDocumentListIndex);
            PatchListItemViewModel listItemViewModel = patch.ToListItemViewModel(DUMMY_LIST_INDEX, childDocumentListIndex);
            patchListViewModel.List.Add(listItemViewModel);
            patchListViewModel.List = patchListViewModel.List.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(patchListViewModel.List);

            IList<PatchDetailsViewModel> patchPropertyViewModels = ChildDocumentHelper.GetPatchDetailsViewModels(userInput.Document, childDocumentTypeEnum, childDocumentListIndex);
            PatchDetailsViewModel detailsViewModel = patch.ToDetailsViewModel(document.ID, childDocumentTypeEnum, childDocumentListIndex, DUMMY_LIST_INDEX, _repositoryWrapper.OperatorTypeRepository, _entityPositionManager);
            patchPropertyViewModels.Add(detailsViewModel);
            IList<PatchDetailsViewModel> patchPropertyViewModelsSorted = patchPropertyViewModels.OrderBy(x => x.Patch.Name).ToList();
            patchPropertyViewModels.Clear();
            patchPropertyViewModels.AddRange(patchPropertyViewModelsSorted);
            ListIndexHelper.RenumberListIndexes(patchPropertyViewModels);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel PatchDelete(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
            Document document = ChildDocumentHelper.GetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex, _repositoryWrapper.DocumentRepository);
            Patch patch = document.Patches[listIndex];

            // Business
            VoidResult result = _patchManager.DeleteWithRelatedEntities(patch);
            if (result.Successful)
            {
                // ToViewModel
                IList<PatchDetailsViewModel> patchDetailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels(_viewModel.Document, childDocumentTypeEnum, childDocumentListIndex);
                patchDetailsViewModels.RemoveAt(listIndex);
                ListIndexHelper.RenumberListIndexes(patchDetailsViewModels, listIndex);

                PatchListViewModel patchListViewModel = ChildDocumentHelper.GetPatchListViewModel(_viewModel.Document, childDocumentTypeEnum, childDocumentListIndex);
                patchListViewModel.List.RemoveAt(listIndex);
                ListIndexHelper.RenumberListIndexes(patchListViewModel.List, listIndex);
            }
            else
            {
                // ToViewModel
                _viewModel.PopupMessages = result.Messages;
            }

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // Temporary Patch Actions

        public MainViewModel PatchDetailsEdit(MainViewModel userInput, int id)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // These are temporary dummy values. Patches will be opened differently later.
            object viewModel2 = _patchDetailsPresenter.Edit(id, 0, null, null, 0);

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        public MainViewModel PatchDetailsClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _patchDetailsPresenter.Close();

            DispatchViewModel(viewModel2);

            return _viewModel;
        }

        // Sample Actions

        public MainViewModel SampleListShow(MainViewModel userInput, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            Document document = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            object viewModel2 = _sampleListPresenter.Show(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex);
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel SampleListClose(MainViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            object viewModel2 = _sampleListPresenter.Close();
            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel SampleCreate(MainViewModel userInput, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document parentDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            // Business
            Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex, _repositoryWrapper.DocumentRepository);
            Sample sample = _sampleManager.CreateSample();
            sample.LinkTo(document);

            ISideEffect sideEffect = new Sample_SideEffect_GenerateName(sample);
            sideEffect.Execute();

            // ToViewModel
            SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(userInput.Document, childDocumentTypeEnum, childDocumentListIndex);
            SampleListItemViewModel listItemViewModel = sample.ToListItemViewModel(DUMMY_LIST_INDEX, childDocumentListIndex);
            listViewModel.List.Add(listItemViewModel);
            listViewModel.List = listViewModel.List.OrderBy(x => x.Name).ToList();
            ListIndexHelper.RenumberListIndexes(listViewModel.List);

            IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels(userInput.Document, childDocumentTypeEnum, childDocumentListIndex);
            SamplePropertiesViewModel propertiesViewModel = sample.ToPropertiesViewModel(_viewModel.Document.ID, childDocumentTypeEnum, childDocumentListIndex, DUMMY_LIST_INDEX, new SampleRepositories(_repositoryWrapper));
            propertiesViewModels.Add(propertiesViewModel);
            IList<SamplePropertiesViewModel> propertiesViewModelsSorted = propertiesViewModels.OrderBy(x => x.Entity.Name).ToList();
            propertiesViewModels.Clear();
            propertiesViewModels.AddRange(propertiesViewModelsSorted);
            ListIndexHelper.RenumberListIndexes(propertiesViewModels);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel SampleDelete(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document rootDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
            Document document = ChildDocumentHelper.GetRootDocumentOrChildDocument(userInput.Document.ID, childDocumentTypeEnum, childDocumentListIndex, _repositoryWrapper.DocumentRepository);
            Sample sample = document.Samples[listIndex];

            // Business
            VoidResult result = _sampleManager.DeleteWithRelatedEntities(sample);
            if (result.Successful)
            {
                // ToViewModel
                IList<SamplePropertiesViewModel> samplePropertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels(_viewModel.Document, childDocumentTypeEnum, childDocumentListIndex);
                samplePropertiesViewModels.RemoveAt(listIndex);
                ListIndexHelper.RenumberListIndexes(samplePropertiesViewModels, listIndex);

                SampleListViewModel sampleListViewModel = ChildDocumentHelper.GetSampleListViewModel(_viewModel.Document, childDocumentTypeEnum, childDocumentListIndex);
                sampleListViewModel.List.RemoveAt(listIndex);
                ListIndexHelper.RenumberListIndexes(sampleListViewModel.List, listIndex);
            }
            else
            {
                // ToViewModel
                _viewModel.PopupMessages = result.Messages;
            }

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel SamplePropertiesShow(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            IList<SamplePropertiesViewModel> list = GetSamplePropertiesViewModels(userInput, childDocumentTypeEnum, childDocumentListIndex);
            SamplePropertiesViewModel item = list[listIndex];

            object viewModel2 = _samplePropertiesPresenter.Show(item);

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel SamplePropertiesClose(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document destDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            // Get the right partial ViewModel
            IList<SamplePropertiesViewModel> propertiesViewModels = GetSamplePropertiesViewModels(userInput, childDocumentTypeEnum, childDocumentListIndex);
            SamplePropertiesViewModel item = propertiesViewModels[listIndex];

            // Partial Action
            SamplePropertiesViewModel viewModel2 = _samplePropertiesPresenter.Close(item);

            if (viewModel2.Successful)
            {
                // Update properties list 
                IList<SamplePropertiesViewModel> propertiesViewModelsSorted = propertiesViewModels.OrderBy(x => x.Entity.Name).ToList();
                propertiesViewModels.Clear();
                propertiesViewModels.AddRange(propertiesViewModelsSorted);
                ListIndexHelper.RenumberListIndexes(propertiesViewModels);

                // Update list
                SampleListViewModel listViewModel = GetSampleListViewModel(userInput, childDocumentTypeEnum, childDocumentListIndex);
                RefreshSampleList(listViewModel);
            }

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        public MainViewModel SamplePropertiesLoseFocus(MainViewModel userInput, int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (userInput == null) throw new NullException(() => userInput);

            EnsureViewModel(userInput);

            // ToEntity
            Document destDocument = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);

            // Get the right partial ViewModel
            IList<SamplePropertiesViewModel> propertiesViewModels = GetSamplePropertiesViewModels(userInput, childDocumentTypeEnum, childDocumentListIndex);
            SamplePropertiesViewModel item = propertiesViewModels[listIndex];

            // Partial Action
            SamplePropertiesViewModel viewModel2 = _samplePropertiesPresenter.LoseFocus(item);

            if (viewModel2.Successful)
            {
                // Update properties list 
                IList<SamplePropertiesViewModel> propertiesViewModelsSorted = propertiesViewModels.OrderBy(x => x.Entity.Name).ToList();
                propertiesViewModels.Clear();
                propertiesViewModels.AddRange(propertiesViewModelsSorted);
                ListIndexHelper.RenumberListIndexes(propertiesViewModels);

                // Update list
                SampleListViewModel listViewModel = GetSampleListViewModel(userInput, childDocumentTypeEnum, childDocumentListIndex);
                RefreshSampleList(listViewModel);
            }

            DispatchViewModel(viewModel2);

            _repositoryWrapper.Rollback();

            return _viewModel;
        }

        // DispatchViewModel

        private Dictionary<Type, Action<object>> _dispatchDelegateDictionary;

        private Dictionary<Type, Action<object>> CreateDispatchDelegateDictionary()
        {
            var dictionary = new Dictionary<Type, Action<object>>
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

        private void DispatchAudioFileOutputListViewModel(object viewModel2)
        {
            _viewModel.Document.AudioFileOutputList = (AudioFileOutputListViewModel)viewModel2;

            if (_viewModel.Document.AudioFileOutputList.Visible)
            {
                HideAllListAndDetailViewModels();
                _viewModel.Document.AudioFileOutputList.Visible = true;
            }
        }

        private void DispatchAudioFileOutputPropertiesViewModel(object viewModel2)
        {
            var audioFileOutputPropertiesViewModel = (AudioFileOutputPropertiesViewModel)viewModel2;

            _viewModel.Document.AudioFileOutputPropertiesList[audioFileOutputPropertiesViewModel.Entity.Keys.ListIndex] = audioFileOutputPropertiesViewModel;

            if (audioFileOutputPropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                audioFileOutputPropertiesViewModel.Visible = true;
            }

            _viewModel.PopupMessages.AddRange(audioFileOutputPropertiesViewModel.ValidationMessages);
            audioFileOutputPropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchChildDocumentListViewModel(object viewModel2)
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

        private void DispatchChildDocumentPropertiesViewModel(object viewModel2)
        {
            var childDocumentPropertiesViewModel = (ChildDocumentPropertiesViewModel)viewModel2;

            switch (childDocumentPropertiesViewModel.Keys.ChildDocumentTypeEnum)
            {
                case ChildDocumentTypeEnum.Instrument:
                    _viewModel.Document.InstrumentPropertiesList[childDocumentPropertiesViewModel.Keys.ListIndex] = childDocumentPropertiesViewModel;
                    break;

                case ChildDocumentTypeEnum.Effect:
                    _viewModel.Document.EffectPropertiesList[childDocumentPropertiesViewModel.Keys.ListIndex] = childDocumentPropertiesViewModel;
                    break;

                default:
                    throw new ValueNotSupportedException(childDocumentPropertiesViewModel.Keys.ChildDocumentTypeEnum);
            }

            if (childDocumentPropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                childDocumentPropertiesViewModel.Visible = true;
            }

            _viewModel.PopupMessages.AddRange(childDocumentPropertiesViewModel.ValidationMessages);
            childDocumentPropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveDetailsViewModel(object viewModel2)
        {
            var curveDetailsViewModel = (CurveDetailsViewModel)viewModel2;

            IList<CurveDetailsViewModel> list = GetCurveDetailsViewModels(_viewModel, curveDetailsViewModel.Curve.Keys.ChildDocumentTypeEnum, curveDetailsViewModel.Curve.Keys.ChildDocumentListIndex);

            list[curveDetailsViewModel.Curve.Keys.ListIndex] = curveDetailsViewModel;

            if (curveDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                curveDetailsViewModel.Visible = true;
            }

            _viewModel.ValidationMessages.AddRange(curveDetailsViewModel.ValidationMessages);
            curveDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveListViewModel(object viewModel2)
        {
            var curveListViewModel = (CurveListViewModel)viewModel2;

            if (!curveListViewModel.Keys.ChildDocumentTypeEnum.HasValue)
            {
                _viewModel.Document.CurveList = curveListViewModel;
            }
            else
            {
                if (!curveListViewModel.Keys.ChildDocumentListIndex.HasValue) throw new NullException(() => curveListViewModel.Keys.ChildDocumentListIndex);

                switch (curveListViewModel.Keys.ChildDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        _viewModel.Document.InstrumentDocumentList[curveListViewModel.Keys.ChildDocumentListIndex.Value].CurveList = curveListViewModel;
                        break;

                    case ChildDocumentTypeEnum.Effect:
                        _viewModel.Document.EffectDocumentList[curveListViewModel.Keys.ChildDocumentListIndex.Value].CurveList = curveListViewModel;
                        break;

                    default:
                        throw new ValueNotSupportedException(curveListViewModel.Keys.ChildDocumentTypeEnum.Value);
                }
            }

            if (curveListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                curveListViewModel.Visible = true;
            }
        }

        private void DispatchDocumentCannotDeleteViewModel(object viewModel2)
        {
            _viewModel.DocumentCannotDelete = (DocumentCannotDeleteViewModel)viewModel2;
        }

        private void DispatchDocumentDeletedViewModel(object viewModel2)
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

        private void DispatchDocumentDeleteViewModel(object viewModel2)
        {
            _viewModel.DocumentDelete = (DocumentDeleteViewModel)viewModel2;
        }

        private void DispatchDocumentDetailsViewModel(object viewModel2)
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

        private void DispatchDocumentListViewModel(object viewModel2)
        {
            _viewModel.DocumentList = (DocumentListViewModel)viewModel2;

            if (_viewModel.DocumentList.Visible)
            {
                HideAllListAndDetailViewModels();
                _viewModel.DocumentList.Visible = true;
            }
        }

        private void DispatchDocumentPropertiesViewModel(object viewModel2)
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

        private void DispatchDocumentTreeViewModel(object viewModel2)
        {
            _viewModel.Document.DocumentTree = (DocumentTreeViewModel)viewModel2;
        }
        
        private void DispatchMenuViewModel(object viewModel2)
        {
            _viewModel.Menu = (MenuViewModel)viewModel2;
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

        private void DispatchPatchDetailsViewModel(object viewModel2)
        {
            var patchDetailsViewModel = (PatchDetailsViewModel)viewModel2;

            IList<PatchDetailsViewModel> list = GetPatchDetailsViewModels(_viewModel, patchDetailsViewModel.Patch.Keys.ChildDocumentTypeEnum, patchDetailsViewModel.Patch.Keys.ChildDocumentListIndex);

            list[patchDetailsViewModel.Patch.Keys.ListIndex] = patchDetailsViewModel;

            if (patchDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                patchDetailsViewModel.Visible = true;
            }

            _viewModel.ValidationMessages.AddRange(patchDetailsViewModel.ValidationMessages);
            patchDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchListViewModel(object viewModel2)
        {
            var patchListViewModel = (PatchListViewModel)viewModel2;

            if (!patchListViewModel.Keys.ChildDocumentTypeEnum.HasValue)
            {
                _viewModel.Document.PatchList = patchListViewModel;
            }
            else
            {
                if (!patchListViewModel.Keys.ChildDocumentListIndex.HasValue) throw new NullException(() => patchListViewModel.Keys.ChildDocumentListIndex);

                switch (patchListViewModel.Keys.ChildDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        _viewModel.Document.InstrumentDocumentList[patchListViewModel.Keys.ChildDocumentListIndex.Value].PatchList = patchListViewModel;
                        break;

                    case ChildDocumentTypeEnum.Effect:
                        _viewModel.Document.EffectDocumentList[patchListViewModel.Keys.ChildDocumentListIndex.Value].PatchList = patchListViewModel;
                        break;

                    default:
                        throw new ValueNotSupportedException(patchListViewModel.Keys.ChildDocumentTypeEnum.Value);
                }
            }

            if (patchListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                patchListViewModel.Visible = true;
            }
        }

        private void DispatchSamplePropertiesViewModel(object viewModel2)
        {
            var samplePropertiesViewModel = (SamplePropertiesViewModel)viewModel2;

            IList<SamplePropertiesViewModel> list = GetSamplePropertiesViewModels(_viewModel, samplePropertiesViewModel.Entity.Keys.ChildDocumentTypeEnum, samplePropertiesViewModel.Entity.Keys.ChildDocumentListIndex);

            list[samplePropertiesViewModel.Entity.Keys.ListIndex] = samplePropertiesViewModel;

            if (samplePropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                samplePropertiesViewModel.Visible = true;
            }

            _viewModel.PopupMessages.AddRange(samplePropertiesViewModel.ValidationMessages);
            samplePropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchSampleListViewModel(object viewModel2)
        {
            var sampleListViewModel = (SampleListViewModel)viewModel2;

            if (!sampleListViewModel.Keys.ChildDocumentTypeEnum.HasValue)
            {
                _viewModel.Document.SampleList = sampleListViewModel;
            }
            else
            {
                if (!sampleListViewModel.Keys.ChildDocumentListIndex.HasValue) throw new NullException(() => sampleListViewModel.Keys.ChildDocumentListIndex);

                switch (sampleListViewModel.Keys.ChildDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        _viewModel.Document.InstrumentDocumentList[sampleListViewModel.Keys.ChildDocumentListIndex.Value].SampleList = sampleListViewModel;
                        break;

                    case ChildDocumentTypeEnum.Effect:
                        _viewModel.Document.EffectDocumentList[sampleListViewModel.Keys.ChildDocumentListIndex.Value].SampleList = sampleListViewModel;
                        break;

                    default:
                        throw new ValueNotSupportedException(sampleListViewModel.Keys.ChildDocumentTypeEnum.Value);
                }
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
                MainViewModel mainViewModel = ViewModelHelper.CreateEmptyMainViewModel(_repositoryWrapper.OperatorTypeRepository);

                Document entity = userInput.ToEntityWithRelatedEntities(_repositoryWrapper);
                DocumentViewModel documentViewModel = entity.ToViewModel(_repositoryWrapper, _entityPositionManager);
                mainViewModel.Document = documentViewModel;

                MenuViewModel menuViewModel = _menuPresenter.Show();
                mainViewModel.Menu = menuViewModel;

                DocumentListViewModel documentListViewModel = _documentListPresenter.Show();
                mainViewModel.DocumentList = documentListViewModel;

                mainViewModel.WindowTitle = Titles.ApplicationName;

                _viewModel = mainViewModel;
            }
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
            DispatchViewModel(viewModel2);
        }

        private void RefreshAudioFileOutputList()
        {
            object viewModel2 = _audioFileOutputListPresenter.Refresh(_viewModel.Document.AudioFileOutputList);
            DispatchViewModel(viewModel2);
        }

        private void RefreshSampleList(SampleListViewModel sampleListViewModel)
        {
            object viewModel2 = _sampleListPresenter.Refresh(sampleListViewModel);
            DispatchViewModel(viewModel2);
        }

        private NotFoundViewModel CreateDocumentNotFoundViewModel()
        {
            NotFoundViewModel viewModel = new NotFoundPresenter().Show(PropertyDisplayNames.Document);
            return viewModel;
        }

        private PatchDetailsPresenter CreatePatchDetailsPresenter()
        {
            var presenter2 = new PatchDetailsPresenter(
                _repositoryWrapper.PatchRepository,
                _repositoryWrapper.OperatorRepository,
                _repositoryWrapper.OperatorTypeRepository,
                _repositoryWrapper.InletRepository,
                _repositoryWrapper.OutletRepository,
                _repositoryWrapper.EntityPositionRepository,
                _repositoryWrapper.CurveRepository,
                _repositoryWrapper.SampleRepository);

            return presenter2;
        }

        private AudioFileOutputPropertiesPresenter CreateAudioFileOutputPropertiesPresenter()
        {
            var audioFileOutputRepositories = new AudioFileOutputRepositories(_repositoryWrapper);

            var presenter2 = new AudioFileOutputPropertiesPresenter(audioFileOutputRepositories);

            return presenter2;
        }

        private IList<CurveDetailsViewModel> GetCurveDetailsViewModels(MainViewModel viewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (!childDocumentTypeEnum.HasValue)
            {
                return viewModel.Document.CurveDetailsList;
            }
            else
            {
                if (childDocumentListIndex == null) throw new NullException(() => childDocumentListIndex);

                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return viewModel.Document.InstrumentDocumentList[childDocumentListIndex.Value].CurveDetailsList;

                    case ChildDocumentTypeEnum.Effect:
                        return viewModel.Document.EffectDocumentList[childDocumentListIndex.Value].CurveDetailsList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }

        private IList<PatchDetailsViewModel> GetPatchDetailsViewModels(MainViewModel viewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (!childDocumentTypeEnum.HasValue)
            {
                return viewModel.Document.PatchDetailsList;
            }
            else
            {
                if (childDocumentListIndex == null) throw new NullException(() => childDocumentListIndex);

                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return viewModel.Document.InstrumentDocumentList[childDocumentListIndex.Value].PatchDetailsList;

                    case ChildDocumentTypeEnum.Effect:
                        return viewModel.Document.EffectDocumentList[childDocumentListIndex.Value].PatchDetailsList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }

        private IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels(MainViewModel viewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (!childDocumentTypeEnum.HasValue)
            {
                return viewModel.Document.SamplePropertiesList;
            }
            else
            {
                if (childDocumentListIndex == null) throw new NullException(() => childDocumentListIndex);

                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return viewModel.Document.InstrumentDocumentList[childDocumentListIndex.Value].SamplePropertiesList;

                    case ChildDocumentTypeEnum.Effect:
                        return viewModel.Document.EffectDocumentList[childDocumentListIndex.Value].SamplePropertiesList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }

        private SampleListViewModel GetSampleListViewModel(MainViewModel viewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (!childDocumentTypeEnum.HasValue)
            {
                return viewModel.Document.SampleList;
            }
            else
            {
                if (childDocumentListIndex == null) throw new NullException(() => childDocumentListIndex);

                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return viewModel.Document.InstrumentDocumentList[childDocumentListIndex.Value].SampleList;

                    case ChildDocumentTypeEnum.Effect:
                        return viewModel.Document.EffectDocumentList[childDocumentListIndex.Value].SampleList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }
    }
}
