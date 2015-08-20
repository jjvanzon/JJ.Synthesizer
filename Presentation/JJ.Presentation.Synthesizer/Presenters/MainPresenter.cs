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
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Extensions;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

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

        private AudioFileOutputGridPresenter _audioFileOutputGridPresenter;
        private AudioFileOutputPropertiesPresenter _audioFileOutputPropertiesPresenter;
        private ChildDocumentGridPresenter _effectGridPresenter;
        private ChildDocumentGridPresenter _instrumentGridPresenter;
        private ChildDocumentPropertiesPresenter _childDocumentPropertiesPresenter;
        private CurveDetailsPresenter _curveDetailsPresenter;
        private CurveGridPresenter _curveGridPresenter;
        private DocumentCannotDeletePresenter _documentCannotDeletePresenter;
        private DocumentDeletedPresenter _documentDeletedPresenter;
        private DocumentDeletePresenter _documentDeletePresenter;
        private DocumentDetailsPresenter _documentDetailsPresenter;
        private DocumentGridPresenter _documentGridPresenter;
        private DocumentPropertiesPresenter _documentPropertiesPresenter;
        private DocumentTreePresenter _documentTreePresenter;
        private MenuPresenter _menuPresenter;
        private NotFoundPresenter _notFoundPresenter;
        private OperatorPropertiesPresenter _operatorPropertiesPresenter;
        private OperatorPropertiesPresenter_ForPatchInlet _operatorPropertiesPresenter_ForPatchInlet;
        private OperatorPropertiesPresenter_ForPatchOutlet _operatorPropertiesPresenter_ForPatchOutlet;
        private OperatorPropertiesPresenter_ForValue _operatorPropertiesPresenter_ForValue;
        private PatchDetailsPresenter _patchDetailsPresenter;
        private PatchGridPresenter _patchGridPresenter;
        private SampleGridPresenter _sampleGridPresenter;
        private SamplePropertiesPresenter _samplePropertiesPresenter;

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
            var patchRepositories = new PatchRepositories(_repositoryWrapper);

            _audioFileOutputGridPresenter = new AudioFileOutputGridPresenter(_repositoryWrapper.DocumentRepository);
            _audioFileOutputPropertiesPresenter = new AudioFileOutputPropertiesPresenter(new AudioFileOutputRepositories(_repositoryWrapper));
            _childDocumentPropertiesPresenter = new ChildDocumentPropertiesPresenter(
                _repositoryWrapper.DocumentRepository,
                _repositoryWrapper.ChildDocumentTypeRepository,
                _repositoryWrapper.IDRepository);
            _curveDetailsPresenter = new CurveDetailsPresenter(
                _repositoryWrapper.CurveRepository, 
                _repositoryWrapper.NodeRepository, 
                _repositoryWrapper.NodeTypeRepository);
            _curveGridPresenter = new CurveGridPresenter(_repositoryWrapper.DocumentRepository);
            _documentCannotDeletePresenter = new DocumentCannotDeletePresenter(_repositoryWrapper.DocumentRepository);
            _documentDeletedPresenter = new DocumentDeletedPresenter();
            _documentDeletePresenter = new DocumentDeletePresenter(_repositoryWrapper);
            _documentDetailsPresenter = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository, _repositoryWrapper.IDRepository);
            _documentGridPresenter = new DocumentGridPresenter(_repositoryWrapper.DocumentRepository);
            _documentPropertiesPresenter = new DocumentPropertiesPresenter(_repositoryWrapper.DocumentRepository);
            _documentTreePresenter = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository);
            _effectGridPresenter = new ChildDocumentGridPresenter(_repositoryWrapper.DocumentRepository);
            _instrumentGridPresenter = new ChildDocumentGridPresenter(_repositoryWrapper.DocumentRepository);
            _menuPresenter = new MenuPresenter();
            _notFoundPresenter = new NotFoundPresenter();
            _operatorPropertiesPresenter = new OperatorPropertiesPresenter(patchRepositories);
            _operatorPropertiesPresenter_ForPatchInlet = new OperatorPropertiesPresenter_ForPatchInlet(patchRepositories);
            _operatorPropertiesPresenter_ForPatchOutlet = new OperatorPropertiesPresenter_ForPatchOutlet(patchRepositories);
            _operatorPropertiesPresenter_ForValue = new OperatorPropertiesPresenter_ForValue(patchRepositories);
            _patchDetailsPresenter = _patchDetailsPresenter = new PatchDetailsPresenter(patchRepositories);
            _patchGridPresenter = new PatchGridPresenter(_repositoryWrapper.DocumentRepository);
            _sampleGridPresenter = new SampleGridPresenter(_repositoryWrapper.DocumentRepository, _repositoryWrapper.SampleRepository);
            _samplePropertiesPresenter = new SamplePropertiesPresenter(new SampleRepositories(_repositoryWrapper));

            _documentManager = new DocumentManager(repositoryWrapper);
            _patchManager = new PatchManager(patchRepositories);
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
                _repositoryWrapper.DocumentRepository,
                _repositoryWrapper.IDRepository);
            _entityPositionManager = new EntityPositionManager(_repositoryWrapper.EntityPositionRepository, _repositoryWrapper.IDRepository);

            _dispatchDelegateDictionary = CreateDispatchDelegateDictionary();
        }

        // General

        public MainViewModel ViewModel { get; private set; }

        public void Show()
        {
            try
            {
                ViewModel = ViewModelHelper.CreateEmptyMainViewModel(_repositoryWrapper.OperatorTypeRepository);

                MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
                DispatchViewModel(menuViewModel);

                _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
                _documentGridPresenter.Show();
                DispatchViewModel(_documentGridPresenter.ViewModel);

                ViewModel.WindowTitle = Titles.ApplicationName;
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void NotFoundOK()
        {
            try
            {
                _notFoundPresenter.OK();
                DispatchViewModel(_notFoundPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PopupMessagesOK()
        {
            try
            {
                ViewModel.PopupMessages = new List<Message> { };
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Document List

        public void DocumentGridShow(int pageNumber)
        {
            try
            {
                _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
                DocumentGridViewModel viewModel = _documentGridPresenter.Show(pageNumber);
                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentGridClose()
        {
            try
            {
                _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
                _documentGridPresenter.Close();
                DispatchViewModel(_documentGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentDetailsCreate()
        {
            try
            {
                DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();
                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentDetailsClose()
        {
            try
            {
                _documentDetailsPresenter.Close();
                DispatchViewModel(_documentDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentDetailsSave()
        {
            try
            {
                _documentDetailsPresenter.Save();
                DispatchViewModel(_documentDetailsPresenter.ViewModel);

                RefreshDocumentGrid();
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentDelete(int id)
        {
            try
            {
                object viewModel = _documentDeletePresenter.Show(id);
                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentCannotDeleteOK()
        {
            try
            {
                _documentCannotDeletePresenter.OK();
                DispatchViewModel(_documentCannotDeletePresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentConfirmDelete(int id)
        {
            try
            {
                object viewModel = _documentDeletePresenter.Confirm(id);

                if (viewModel is DocumentDeletedViewModel)
                {
                    _repositoryWrapper.Commit();
                }

                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentCancelDelete()
        {
            try
            {
                _documentDeletePresenter.Cancel();
                DispatchViewModel(_documentDeletePresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentDeletedOK()
        {
            try
            {
                DocumentDeletedViewModel viewModel = _documentDeletedPresenter.OK();
                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Document Actions

        public void DocumentOpen(int documentID)
        {
            try
            {
                if (ViewModel.Document.IsOpen)
                {
                    // If you do not clear the presenters, they will remember the view model,
                    // so if you open another document and then open the original one,
                    // the original unsaved data will be shown in the presenters,
                    // because they do not think they need to refresh their view models,
                    // since it is the same document.

                    // TODO: The whole MustCreateViewModel principle in the partial presenters has been refactored out,
                    // so this may not be relevant anymore?
                    _audioFileOutputGridPresenter.ViewModel = null;
                    _audioFileOutputPropertiesPresenter.ViewModel = null;
                    _childDocumentPropertiesPresenter.ViewModel = null;
                    _curveDetailsPresenter.ViewModel = null;
                    _curveGridPresenter.ViewModel = null;
                    _documentPropertiesPresenter.ViewModel = null;
                    _documentTreePresenter.ViewModel = null;
                    _effectGridPresenter.ViewModel = null;
                    _instrumentGridPresenter.ViewModel = null;
                    _patchDetailsPresenter.ViewModel = null;
                    _patchGridPresenter.ViewModel = null;
                    _sampleGridPresenter.ViewModel = null;
                    _samplePropertiesPresenter.ViewModel = null;
                }

                Document document = _repositoryWrapper.DocumentRepository.Get(documentID);

                ViewModel.Document = document.ToViewModel(_repositoryWrapper, _entityPositionManager);

                _audioFileOutputGridPresenter.ViewModel = ViewModel.Document.AudioFileOutputGrid;
                _effectGridPresenter.ViewModel = ViewModel.Document.EffectGrid;
                _instrumentGridPresenter.ViewModel = ViewModel.Document.InstrumentGrid;
                _curveGridPresenter.ViewModel = ViewModel.Document.CurveGrid;
                _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;

                ViewModel.WindowTitle = String.Format("{0} - {1}", document.Name, Titles.ApplicationName);
                _menuPresenter.Show(documentIsOpen: true);
                ViewModel.Menu = _menuPresenter.ViewModel;

                ViewModel.DocumentGrid.Visible = false;
                ViewModel.Document.DocumentTree.Visible = true;

                ViewModel.Document.IsOpen = true;
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentSave()
        {
            if (ViewModel.Document == null) throw new NullException(() => ViewModel.Document);

            try
            {
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

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

                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                ViewModel.WarningMessages = warningsValidator.ValidationMessages.ToCanonical();
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentClose()
        {
            try
            {
                if (ViewModel.Document.IsOpen)
                {
                    ViewModel.Document = ViewModelHelper.CreateEmptyDocumentViewModel();
                    ViewModel.WindowTitle = Titles.ApplicationName;

                    _menuPresenter.Show(documentIsOpen: false);

                    ViewModel.Menu = _menuPresenter.ViewModel;

                    _audioFileOutputGridPresenter.ViewModel = null;
                    _audioFileOutputPropertiesPresenter.ViewModel = null;
                    _childDocumentPropertiesPresenter.ViewModel = null;
                    _curveDetailsPresenter.ViewModel = null;
                    _curveGridPresenter.ViewModel = null;
                    _documentPropertiesPresenter.ViewModel = null;
                    _documentTreePresenter.ViewModel = null;
                    _effectGridPresenter.ViewModel = null;
                    _instrumentGridPresenter.ViewModel = null;
                    _patchDetailsPresenter.ViewModel = null;
                    _patchGridPresenter.ViewModel = null;
                    _sampleGridPresenter.ViewModel = null;
                    _samplePropertiesPresenter.ViewModel = null;
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentPropertiesShow()
        {
            try
            {
                _documentPropertiesPresenter.ViewModel = ViewModel.Document.DocumentProperties;
                _documentPropertiesPresenter.Show();
                DispatchViewModel(_documentPropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentPropertiesClose()
        {
            try
            {
                // ToEntity: This should be just enought to correctly refresh the grids and tree furtheron.
                Document document = ViewModel.Document.ToEntity(_repositoryWrapper.DocumentRepository);
                ToEntityHelper.ToChildDocuments(ViewModel.Document.ChildDocumentPropertiesList, document, _repositoryWrapper);

                _documentPropertiesPresenter.ViewModel = ViewModel.Document.DocumentProperties;
                _documentPropertiesPresenter.Close();
                DispatchViewModel(_documentPropertiesPresenter.ViewModel);

                if (_documentPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshDocumentGrid();
                    RefreshDocumentTree();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentPropertiesLoseFocus()
        {

            try
            {
                // ToEntity: This should be just enought to correctly refresh the grids and tree furtheron.
                Document document = ViewModel.Document.ToEntity(_repositoryWrapper.DocumentRepository);
                ToEntityHelper.ToChildDocuments(ViewModel.Document.ChildDocumentPropertiesList, document, _repositoryWrapper);

                _documentPropertiesPresenter.ViewModel = ViewModel.Document.DocumentProperties;
                _documentPropertiesPresenter.LoseFocus();
                DispatchViewModel(_documentPropertiesPresenter.ViewModel);

                if (_documentPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshDocumentGrid();
                    RefreshDocumentTree();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentTreeShow()
        {
            try
            {
                _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;
                _documentTreePresenter.Show();
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentTreeExpandNode(int nodeIndex)
        {
            try
            {
                _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;
                _documentTreePresenter.ExpandNode(nodeIndex);
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentTreeCollapseNode(int nodeIndex)
        {
            try
            {
                _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;
                _documentTreePresenter.CollapseNode(nodeIndex);
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentTreeClose()
        {
            try
            {
                _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;
                _documentTreePresenter.Close();
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Child Document Actions

        public void ChildDocumentPropertiesShow(int id)
        {
            try
            {
                int listIndex = ViewModel.Document.ChildDocumentPropertiesList.IndexOf(x => x.ID == id);

                _childDocumentPropertiesPresenter.ViewModel = ViewModel.Document.ChildDocumentPropertiesList[listIndex];
                _childDocumentPropertiesPresenter.Show();

                DispatchViewModel(_childDocumentPropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void ChildDocumentPropertiesClose()
        {
            try
            {
                // ToEntity: This should be just enought to correctly refresh the grids and tree furtheron.
                Document document = ViewModel.Document.ToEntity(_repositoryWrapper.DocumentRepository);
                ToEntityHelper.ToChildDocuments(ViewModel.Document.ChildDocumentPropertiesList, document, _repositoryWrapper);

                _childDocumentPropertiesPresenter.Close();

                DispatchViewModel(_childDocumentPropertiesPresenter.ViewModel);

                if (_childDocumentPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshDocumentTree();
                    RefreshInstrumentGrid(); // Refresh both efect and instrument grids, because ChildDocumentType can be changed.
                    RefreshEffectGrid();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void ChildDocumentPropertiesLoseFocus()
        {
            try
            {
                // ToEntity: This should be just enought to correctly refresh the grids and tree furtheron.
                Document document = ViewModel.Document.ToEntity(_repositoryWrapper.DocumentRepository);
                ToEntityHelper.ToChildDocuments(ViewModel.Document.ChildDocumentPropertiesList, document, _repositoryWrapper);

                _childDocumentPropertiesPresenter.LoseFocus();

                DispatchViewModel(_childDocumentPropertiesPresenter.ViewModel);

                if (_childDocumentPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshDocumentTree();
                    RefreshInstrumentGrid(); // Refresh both efect and instrument grids, because ChildDocumentType can be changed.
                    RefreshEffectGrid();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // AudioFileOutput Actions

        public void AudioFileOutputGridShow()
        {
            try
            {
                _audioFileOutputGridPresenter.ViewModel = ViewModel.Document.AudioFileOutputGrid;
                _audioFileOutputGridPresenter.Show();
                DispatchViewModel(_audioFileOutputGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputGridClose()
        {
            try
            {
                _audioFileOutputGridPresenter.ViewModel = ViewModel.Document.AudioFileOutputGrid;
                _audioFileOutputGridPresenter.Close();
                DispatchViewModel(_audioFileOutputGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputCreate()
        {
            try
            {
                // ToEntity
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.LinkTo(document);

                ISideEffect sideEffect = new AudioFileOutput_SideEffect_GenerateName(audioFileOutput);
                sideEffect.Execute();

                // ToViewModel
                AudioFileOutputListItemViewModel listItemViewModel = audioFileOutput.ToListItemViewModel();
                ViewModel.Document.AudioFileOutputGrid.List.Add(listItemViewModel);
                ViewModel.Document.AudioFileOutputGrid.List = ViewModel.Document.AudioFileOutputGrid.List.OrderBy(x => x.Name).ToList();

                AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(_repositoryWrapper.AudioFileFormatRepository, _repositoryWrapper.SampleDataTypeRepository, _repositoryWrapper.SpeakerSetupRepository);
                ViewModel.Document.AudioFileOutputPropertiesList.Add(propertiesViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputDelete(int id)
        {
            try
            {
                // 'Business' / ToViewModel
                ViewModel.Document.AudioFileOutputPropertiesList.RemoveFirst(x => x.Entity.ID == id);
                ViewModel.Document.AudioFileOutputGrid.List.RemoveFirst(x => x.ID == id);
                
                // No need to do ToEntity, 
                // because we are not executing any additional business logic or refreshing 
                // that uses the entity models.
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputPropertiesShow(int id)
        {
            try
            {
                int listIndex = ViewModel.Document.AudioFileOutputPropertiesList.IndexOf(x => x.Entity.ID == id);

                _audioFileOutputPropertiesPresenter.ViewModel = ViewModel.Document.AudioFileOutputPropertiesList[listIndex];
                _audioFileOutputPropertiesPresenter.Show();

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputPropertiesClose()
        {
            try
            {
                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                _audioFileOutputPropertiesPresenter.Close();

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);

                if (_audioFileOutputPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshAudioFileOutputGrid();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputPropertiesLoseFocus()
        {
            try
            {
                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                _audioFileOutputPropertiesPresenter.LoseFocus();

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);

                if (_audioFileOutputPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshAudioFileOutputGrid();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Curve Actions

        public void CurveGridShow(int documentID)
        {
            try
            {
                bool isRootDocument = documentID == ViewModel.Document.ID;
                if (isRootDocument)
                {
                    // Needed to create uncommitted child documents.
                    Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                }

                CurveGridViewModel curveGridViewModel = ChildDocumentHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                _curveGridPresenter.ViewModel = curveGridViewModel;
                _curveGridPresenter.Show();
                DispatchViewModel(_curveGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveGridClose()
        {
            try
            {
                _curveGridPresenter.Close();
                DispatchViewModel(_curveGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveCreate(int documentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                Document document = _repositoryWrapper.DocumentRepository.TryGet(documentID);
                var curve = new Curve();
                curve.ID = _repositoryWrapper.IDRepository.GetID();
                curve.LinkTo(document);
                _repositoryWrapper.CurveRepository.Insert(curve);

                ISideEffect sideEffect = new Curve_SideEffect_GenerateName(curve);
                sideEffect.Execute();

                // ToViewModel
                CurveGridViewModel curveGridViewModel = ChildDocumentHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
                CurveListItemViewModel listItemViewModel = curve.ToListItemViewModel();
                curveGridViewModel.List.Add(listItemViewModel);
                curveGridViewModel.List = curveGridViewModel.List.OrderBy(x => x.Name).ToList();

                IList<CurveDetailsViewModel> curveDetailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels_ByDocumentID(ViewModel.Document, document.ID);
                CurveDetailsViewModel curveDetailsViewModel = curve.ToDetailsViewModel(_repositoryWrapper.NodeTypeRepository);
                curveDetailsViewModels.Add(curveDetailsViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveDelete(int curveID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Curve curve = _repositoryWrapper.CurveRepository.TryGet(curveID);
                if (curve == null)
                {
                    NotFoundViewModel notFoundViewModel = ViewModelHelper.CreateNotFoundViewModel<Curve>();
                    DispatchViewModel(notFoundViewModel);
                    return;
                }
                int documentID = curve.Document.ID;

                // Business
                VoidResult result = _curveManager.DeleteWithRelatedEntities(curve);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<CurveDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels_ByDocumentID(ViewModel.Document, documentID);
                    detailsViewModels.RemoveFirst(x => x.Entity.ID == curveID);

                    CurveGridViewModel gridViewModel = ChildDocumentHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    gridViewModel.List.RemoveFirst(x => x.ID == curveID);
                }
                else
                {
                    // ToViewModel
                    ViewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveDetailsShow(int curveID)
        {
            try
            {
                CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.GetCurveDetailsViewModel(ViewModel.Document, curveID);
                _curveDetailsPresenter.ViewModel = detailsViewModel;
                _curveDetailsPresenter.Show();

                DispatchViewModel(_curveDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveDetailsClose()
        {
            try
            {
                _curveDetailsPresenter.Close();

                DispatchViewModel(_curveDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveDetailsLoseFocus()
        {
            try
            {
                _curveDetailsPresenter.LoseFocus();

                DispatchViewModel(_curveDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Effect Actions

        public void EffectGridShow()
        {
            try
            {
                _effectGridPresenter.ViewModel = ViewModel.Document.EffectGrid;
                _effectGridPresenter.Show();
                DispatchViewModel(_effectGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void EffectGridClose()
        {
            try
            {
                _effectGridPresenter.ViewModel = ViewModel.Document.EffectGrid;
                _effectGridPresenter.Close();
                DispatchViewModel(_effectGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void EffectCreate()
        {
            try
            {
                // ToEntity
                Document parentDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                var effect = new Document();
                effect.ID = _repositoryWrapper.IDRepository.GetID();
                effect.SetChildDocumentTypeEnum(ChildDocumentTypeEnum.Effect, _repositoryWrapper.ChildDocumentTypeRepository);
                effect.LinkToParentDocument(parentDocument);
                _repositoryWrapper.DocumentRepository.Insert(effect);

                ISideEffect sideEffect = new ChildDocument_SideEffect_GenerateName(effect);
                sideEffect.Execute();

                // ToViewModel
                ChildDocumentListItemViewModel listItemViewModel = effect.ToChildDocumentListItemViewModel();
                ViewModel.Document.EffectGrid.List.Add(listItemViewModel);
                ViewModel.Document.EffectGrid.List = ViewModel.Document.EffectGrid.List.OrderBy(x => x.Name).ToList();

                ChildDocumentPropertiesViewModel propertiesViewModel = effect.ToChildDocumentPropertiesViewModel(_repositoryWrapper.ChildDocumentTypeRepository);
                ViewModel.Document.ChildDocumentPropertiesList.Add(propertiesViewModel);

                ChildDocumentViewModel documentViewModel = effect.ToChildDocumentViewModel(_repositoryWrapper, _entityPositionManager);
                ViewModel.Document.ChildDocumentList.Add(documentViewModel);

                RefreshDocumentTree();
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void EffectDelete(int effectDocumentID)
        {
            try
            {
                // ToViewModel Only
                ViewModel.Document.EffectGrid.List.RemoveFirst(x => x.ID == effectDocumentID);
                ViewModel.Document.ChildDocumentPropertiesList.RemoveFirst(x => x.ID == effectDocumentID);
                ViewModel.Document.ChildDocumentList.RemoveFirst(x => x.ID == effectDocumentID);
                ViewModel.Document.DocumentTree.Effects.RemoveFirst(x => x.ChildDocumentID == effectDocumentID);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Instrument Actions

        public void InstrumentGridShow()
        {
            try
            {
                _instrumentGridPresenter.ViewModel = ViewModel.Document.InstrumentGrid;
                _instrumentGridPresenter.Show();
                DispatchViewModel(_instrumentGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void InstrumentGridClose()
        {
            try
            {
                _instrumentGridPresenter.ViewModel = ViewModel.Document.InstrumentGrid;
                _instrumentGridPresenter.Close();
                DispatchViewModel(_instrumentGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void InstrumentCreate()
        {
            try
            {
                // ToEntity
                Document parentDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                var instrument = new Document();
                instrument.ID = _repositoryWrapper.IDRepository.GetID();
                instrument.SetChildDocumentTypeEnum(ChildDocumentTypeEnum.Instrument, _repositoryWrapper.ChildDocumentTypeRepository);
                instrument.LinkToParentDocument(parentDocument);
                _repositoryWrapper.DocumentRepository.Insert(instrument);

                ISideEffect sideEffect = new ChildDocument_SideEffect_GenerateName(instrument);
                sideEffect.Execute();

                // ToViewModel
                ChildDocumentListItemViewModel listItemViewModel = instrument.ToChildDocumentListItemViewModel();
                ViewModel.Document.InstrumentGrid.List.Add(listItemViewModel);
                ViewModel.Document.InstrumentGrid.List = ViewModel.Document.InstrumentGrid.List.OrderBy(x => x.Name).ToList();

                ChildDocumentPropertiesViewModel propertiesViewModel = instrument.ToChildDocumentPropertiesViewModel(_repositoryWrapper.ChildDocumentTypeRepository);
                ViewModel.Document.ChildDocumentPropertiesList.Add(propertiesViewModel);

                ChildDocumentViewModel documentViewModel = instrument.ToChildDocumentViewModel(_repositoryWrapper, _entityPositionManager);
                ViewModel.Document.ChildDocumentList.Add(documentViewModel);

                RefreshDocumentTree();
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void InstrumentDelete(int instrumentDocumentID)
        {
            try
            {
                // ToViewModel Only
                ViewModel.Document.InstrumentGrid.List.RemoveFirst(x => x.ID == instrumentDocumentID);
                ViewModel.Document.ChildDocumentPropertiesList.RemoveFirst(x => x.ID == instrumentDocumentID);
                ViewModel.Document.ChildDocumentList.RemoveFirst(x => x.ID == instrumentDocumentID);
                ViewModel.Document.DocumentTree.Instruments.RemoveFirst(x => x.ChildDocumentID == instrumentDocumentID);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Operator Actions

        public void OperatorPropertiesShow(int id)
        {
            try
            {
                {
                    OperatorPropertiesViewModel viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter partialPresenter = _operatorPropertiesPresenter;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForPatchInlet viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel_ForPatchInlet(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForPatchInlet partialPresenter = _operatorPropertiesPresenter_ForPatchInlet;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForPatchOutlet viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel_ForPatchOutlet(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForPatchOutlet partialPresenter = _operatorPropertiesPresenter_ForPatchOutlet;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForValue viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel_ForValue(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForValue partialPresenter = _operatorPropertiesPresenter_ForValue;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }

                throw new Exception(String.Format("Properties view model not found for operator with ID '{0}'.", id));
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorPropertiesClose()
        {
            try
            {
                OperatorPropertiesPresenter partialPresenter = _operatorPropertiesPresenter;

                // Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
                // the inlets and outlets too, which are not defined in the OperatorDetailsViewModel.
                OperatorViewModel operatorViewModel = ChildDocumentHelper.GetOperatorViewModel(ViewModel.Document, partialPresenter.ViewModel.ID);
                Operator entity = operatorViewModel.ToEntityWithInletsAndOutlets(
                    _repositoryWrapper.OperatorRepository,
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository);

                partialPresenter.Close();

                if (partialPresenter.ViewModel.Successful)
                {
                    // Refresh the operator in the patch view.
                    ViewModelHelper.UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorPropertiesClose_ForPatchInlet()
        {
            try
            {
                OperatorPropertiesPresenter_ForPatchInlet partialPresenter = _operatorPropertiesPresenter_ForPatchInlet;

                // Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
                // the inlets and outlets too, which are not defined in the OperatorDetailsViewModel.
                OperatorViewModel operatorViewModel = ChildDocumentHelper.GetOperatorViewModel(ViewModel.Document, partialPresenter.ViewModel.ID);
                Operator entity = operatorViewModel.ToEntityWithInletsAndOutlets(
                    _repositoryWrapper.OperatorRepository,
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository);

                partialPresenter.Close();

                if (partialPresenter.ViewModel.Successful)
                {
                    // Refresh the operator in the patch view.
                    ViewModelHelper.UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorPropertiesClose_ForPatchOutlet()
        {
            try
            {
                OperatorPropertiesPresenter_ForPatchOutlet partialPresenter = _operatorPropertiesPresenter_ForPatchOutlet;

                // Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
                // the inlets and outlets too, which are not defined in the OperatorDetailsViewModel.
                OperatorViewModel operatorViewModel = ChildDocumentHelper.GetOperatorViewModel(ViewModel.Document, partialPresenter.ViewModel.ID);
                Operator entity = operatorViewModel.ToEntityWithInletsAndOutlets(
                    _repositoryWrapper.OperatorRepository,
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository);

                partialPresenter.Close();

                if (partialPresenter.ViewModel.Successful)
                {
                    // Refresh the operator in the patch view.
                    ViewModelHelper.UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorPropertiesClose_ForValue()
        {
            try
            {
                OperatorPropertiesPresenter_ForValue partialPresenter = _operatorPropertiesPresenter_ForValue;

                // Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
                // the inlets and outlets too, which are not defined in the OperatorDetailsViewModel.
                OperatorViewModel operatorViewModel = ChildDocumentHelper.GetOperatorViewModel(ViewModel.Document, partialPresenter.ViewModel.ID);
                Operator entity = operatorViewModel.ToEntityWithInletsAndOutlets(
                    _repositoryWrapper.OperatorRepository,
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository);

                partialPresenter.Close();

                if (partialPresenter.ViewModel.Successful)
                {
                    // Refresh the operator in the patch view.
                    ViewModelHelper.UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorPropertiesLoseFocus()
        {
            try
            {
                OperatorPropertiesPresenter partialPresenter = _operatorPropertiesPresenter;

                // Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
                // the inlets and outlets too, which are not defined in the OperatorDetailsViewModel.
                int operatorID = partialPresenter.ViewModel.ID;
                OperatorViewModel operatorViewModel = ChildDocumentHelper.GetOperatorViewModel(ViewModel.Document, operatorID);
                Operator entity = operatorViewModel.ToEntityWithInletsAndOutlets(
                    _repositoryWrapper.OperatorRepository, 
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository);

                partialPresenter.LoseFocus();

                if (partialPresenter.ViewModel.Successful)
                {
                    // Refresh the operator in the patch view.
                    ViewModelHelper.UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorPropertiesLoseFocus_ForPatchInlet()
        {
            try
            {
                OperatorPropertiesPresenter_ForPatchInlet partialPresenter = _operatorPropertiesPresenter_ForPatchInlet;

                // Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
                // the inlets and outlets too, which are not defined in the OperatorDetailsViewModel.
                OperatorViewModel operatorViewModel = ChildDocumentHelper.GetOperatorViewModel(ViewModel.Document, partialPresenter.ViewModel.ID);
                Operator entity = operatorViewModel.ToEntityWithInletsAndOutlets(
                    _repositoryWrapper.OperatorRepository,
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository);

                partialPresenter.LoseFocus();

                if (partialPresenter.ViewModel.Successful)
                {
                    // Refresh the operator in the patch view.
                    ViewModelHelper.UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorPropertiesLoseFocus_ForPatchOutlet()
        {
            try
            {
                OperatorPropertiesPresenter_ForPatchOutlet partialPresenter = _operatorPropertiesPresenter_ForPatchOutlet;

                // Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
                // the inlets and outlets too, which are not defined in the OperatorDetailsViewModel.
                OperatorViewModel operatorViewModel = ChildDocumentHelper.GetOperatorViewModel(ViewModel.Document, partialPresenter.ViewModel.ID);
                Operator entity = operatorViewModel.ToEntityWithInletsAndOutlets(
                    _repositoryWrapper.OperatorRepository,
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository);

                partialPresenter.LoseFocus();

                if (partialPresenter.ViewModel.Successful)
                {
                    // Refresh the operator in the patch view.
                    ViewModelHelper.UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorPropertiesLoseFocus_ForValue()
        {
            try
            {
                OperatorPropertiesPresenter_ForValue partialPresenter = _operatorPropertiesPresenter_ForValue;

                // Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
                // the inlets and outlets too, which are not defined in the OperatorDetailsViewModel.
                OperatorViewModel operatorViewModel = ChildDocumentHelper.GetOperatorViewModel(ViewModel.Document, partialPresenter.ViewModel.ID);
                Operator entity = operatorViewModel.ToEntityWithInletsAndOutlets(
                    _repositoryWrapper.OperatorRepository,
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository);

                partialPresenter.LoseFocus();

                if (partialPresenter.ViewModel.Successful)
                {
                    // Refresh the operator in the patch view.
                    ViewModelHelper.UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void OperatorCreate(int operatorTypeID)
        {
            try
            {
                // ToEntity
                Patch patch = _patchDetailsPresenter.ViewModel.ToEntity(
                    _repositoryWrapper.PatchRepository,
                    _repositoryWrapper.OperatorRepository,
                    _repositoryWrapper.OperatorTypeRepository,
                    _repositoryWrapper.InletRepository,
                    _repositoryWrapper.OutletRepository,
                    _repositoryWrapper.EntityPositionRepository);

                // Business
                Operator op = _patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);
                op.LinkTo(patch);

                // ToViewModel

                // PatchDetails Operator
                OperatorViewModel operatorViewModel = op.ToViewModelWithRelatedEntitiesAndInverseProperties(_entityPositionManager);
                operatorViewModel.CenterX = 100; // TODO: Low priority: Should these coordinates should be set in business logic? And randomized the same way as in other parts of the code? Maybe in the entity position manager?
                operatorViewModel.CenterY = 100;
                _patchDetailsPresenter.ViewModel.Entity.Operators.Add(operatorViewModel);

                // Operator Properties
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
                switch (operatorTypeEnum)
                {
                    case OperatorTypeEnum.PatchInlet:
                    {
                        OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel = op.ToPropertiesViewModel_ForPatchInlet();
                        IList<OperatorPropertiesViewModel_ForPatchInlet> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ForPatchInlets_ByPatchID(ViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                    case OperatorTypeEnum.PatchOutlet:
                    {
                        OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel = op.ToPropertiesViewModel_ForPatchOutlet();
                        IList<OperatorPropertiesViewModel_ForPatchOutlet> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ForPatchOutlets_ByPatchID(ViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                    case OperatorTypeEnum.Value:
                    {
                        OperatorPropertiesViewModel_ForValue propertiesViewModel = op.ToPropertiesViewModel_ForValue();
                        IList<OperatorPropertiesViewModel_ForValue> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ForValues_ByPatchID(ViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                    default:
                    {
                        OperatorPropertiesViewModel propertiesViewModel = op.ToPropertiesViewModel();
                        IList<OperatorPropertiesViewModel> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ByPatchID(ViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        /// <summary>
        /// Deletes the operator selected in PatchDetails. Does not delete anything, if no operator is selected.
        /// </summary>
        public void OperatorDelete()
        {
            try
            {
                if (_patchDetailsPresenter.ViewModel.SelectedOperator != null)
                {
                    int operatorID = _patchDetailsPresenter.ViewModel.SelectedOperator.ID;

                    _patchDetailsPresenter.DeleteOperator();

                    ViewModel.Document.OperatorPropertiesList.TryRemoveFirst(x => x.ID == operatorID);
                    ViewModel.Document.OperatorPropertiesList_ForPatchInlets.TryRemoveFirst(x => x.ID == operatorID);
                    ViewModel.Document.OperatorPropertiesList_ForPatchOutlets.TryRemoveFirst(x => x.ID == operatorID);
                    ViewModel.Document.OperatorPropertiesList_ForValues.TryRemoveFirst(x => x.ID == operatorID);

                    foreach (ChildDocumentViewModel childDocumentViewModel in ViewModel.Document.ChildDocumentList)
                    {
                        childDocumentViewModel.OperatorPropertiesList.TryRemoveFirst(x => x.ID == operatorID);
                        childDocumentViewModel.OperatorPropertiesList_ForPatchInlets.TryRemoveFirst(x => x.ID == operatorID);
                        childDocumentViewModel.OperatorPropertiesList_ForPatchOutlets.TryRemoveFirst(x => x.ID == operatorID);
                        childDocumentViewModel.OperatorPropertiesList_ForValues.TryRemoveFirst(x => x.ID == operatorID);
                    }
                }

                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Patch Actions

        public void PatchGridShow(int documentID)
        {
            try
            {
                bool isRootDocument = documentID == ViewModel.Document.ID;
                if (!isRootDocument)
                {
                    // Needed to create uncommitted child documents.
                    Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                }

                PatchGridViewModel patchGridViewModel = ChildDocumentHelper.GetPatchGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                _patchGridPresenter.ViewModel = patchGridViewModel;
                _patchGridPresenter.Show();
                DispatchViewModel(_patchGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchGridClose()
        {
            try
            {
                _patchGridPresenter.Close();
                DispatchViewModel(_patchGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchCreate(int documentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Document document = _repositoryWrapper.DocumentRepository.TryGet(documentID);

                // Business
                var patch = new Patch();
                patch.ID = _repositoryWrapper.IDRepository.GetID();
                patch.LinkTo(document);
                _repositoryWrapper.PatchRepository.Insert(patch);

                ISideEffect sideEffect = new Patch_SideEffect_GenerateName(patch);
                sideEffect.Execute();

                // ToViewModel
                PatchGridViewModel gridViewModel = ChildDocumentHelper.GetPatchGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
                PatchListItemViewModel listItemViewModel = patch.ToListItemViewModel();
                gridViewModel.List.Add(listItemViewModel);
                gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

                IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByDocumentID(ViewModel.Document, document.ID);
                PatchDetailsViewModel detailsViewModel = patch.ToDetailsViewModel(_repositoryWrapper.OperatorTypeRepository, _entityPositionManager);
                detailsViewModels.Add(detailsViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDelete(int patchID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = _repositoryWrapper.PatchRepository.TryGet(patchID);
                if (patch == null)
                {
                    NotFoundViewModel notFoundViewModel = ViewModelHelper.CreateNotFoundViewModel<Patch>();
                    DispatchViewModel(notFoundViewModel);
                    return;
                }
                int documentID = patch.Document.ID;

                // Business
                VoidResult result = _patchManager.DeleteWithRelatedEntities(patch);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByDocumentID(ViewModel.Document, documentID);
                    detailsViewModels.RemoveFirst(x => x.Entity.ID == patchID);

                    PatchGridViewModel gridViewModel = ChildDocumentHelper.GetPatchGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    gridViewModel.List.RemoveFirst(x => x.ID == patchID);
                }
                else
                {
                    // ToViewModel
                    ViewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsShow(int patchID)
        {
            try
            {
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel(ViewModel.Document, patchID);
                _patchDetailsPresenter.ViewModel = detailsViewModel;
                _patchDetailsPresenter.Show();
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsClose()
        {
            try
            {
                // ToEntity
                int patchID = _patchDetailsPresenter.ViewModel.Entity.ID;
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = _repositoryWrapper.PatchRepository.Get(patchID);
                int documentID = patch.Document.ID;

                // Partial Action
                _patchDetailsPresenter.Close();

                if (_patchDetailsPresenter.ViewModel.Successful)
                {
                    PatchGridViewModel gridViewModel = ChildDocumentHelper.GetPatchGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    RefreshPatchGrid(gridViewModel);
                }

                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsLoseFocus()
        {
            try
            {
                // ToEntity
                int patchID = _patchDetailsPresenter.ViewModel.Entity.ID;
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = _repositoryWrapper.PatchRepository.Get(patchID);
                int documentID = patch.Document.ID;

                // Partial Action
                _patchDetailsPresenter.LoseFocus();

                if (_patchDetailsPresenter.ViewModel.Successful)
                {
                    PatchGridViewModel gridViewModel = ChildDocumentHelper.GetPatchGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    RefreshPatchGrid(gridViewModel);
                }

                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsMoveOperator(int operatorID, float centerX, float centerY)
        {
            try
            {
                _patchDetailsPresenter.MoveOperator(operatorID, centerX, centerY);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsChangeInputOutlet(int inletID, int inputOutletID)
        {
            try
            {
                _patchDetailsPresenter.ChangeInputOutlet(inletID, inputOutletID);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsSelectOperator(int operatorID)
        {
            try
            {
                _patchDetailsPresenter.SelectOperator(operatorID);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsSetValue(string value)
        {
            try
            {
                _patchDetailsPresenter.SetValue(value);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);

                // Move messages to popup messages, because the default dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
                ViewModel.PopupMessages.AddRange(_patchDetailsPresenter.ViewModel.ValidationMessages);
                _patchDetailsPresenter.ViewModel.ValidationMessages.Clear();

                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        /// <summary>
        /// Returns output file path.
        /// </summary>
        public string PatchPlay()
        {
            try
            {
                string outputFilePath = _patchDetailsPresenter.Play(_repositoryWrapper);

                DispatchViewModel(_patchDetailsPresenter.ViewModel);

                // Move messages to popup messages, because the default dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
                ViewModel.PopupMessages.AddRange(_patchDetailsPresenter.ViewModel.ValidationMessages);
                _patchDetailsPresenter.ViewModel.ValidationMessages.Clear();

                ViewModel.Successful = _patchDetailsPresenter.ViewModel.Successful;

                DispatchViewModel(_patchDetailsPresenter.ViewModel);

                return outputFilePath;
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Sample Actions

        public void SampleGridShow(int documentID)
        {
            try
            {
                bool isRootDocument = documentID == ViewModel.Document.ID;
                if (!isRootDocument)
                {
                    // Needed to create uncommitted child documents.
                    Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                }

                SampleGridViewModel gridViewModel = ChildDocumentHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                _sampleGridPresenter.ViewModel = gridViewModel;
                _sampleGridPresenter.Show();
                DispatchViewModel(_sampleGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SampleGridClose()
        {
            try
            {
                _sampleGridPresenter.Close();
                DispatchViewModel(_sampleGridPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SampleCreate(int documentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Document document = _repositoryWrapper.DocumentRepository.TryGet(documentID);
                if (document == null)
                {
                    NotFoundViewModel notFoundViewModel = ViewModelHelper.CreateNotFoundViewModel<Document>();
                    DispatchViewModel(notFoundViewModel);
                    return;
                }

                // Business
                Sample sample = _sampleManager.CreateSample();
                sample.LinkTo(document);

                ISideEffect sideEffect = new Sample_SideEffect_GenerateName(sample);
                sideEffect.Execute();

                // ToViewModel
                SampleGridViewModel gridViewModel = ChildDocumentHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
                SampleListItemViewModel listItemViewModel = sample.ToListItemViewModel();
                gridViewModel.List.Add(listItemViewModel);
                gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

                IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByDocumentID(ViewModel.Document, document.ID);
                SamplePropertiesViewModel propertiesViewModel = sample.ToPropertiesViewModel(new SampleRepositories(_repositoryWrapper));
                propertiesViewModels.Add(propertiesViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SampleDelete(int sampleID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Sample sample = _repositoryWrapper.SampleRepository.TryGet(sampleID);
                if (sample == null)
                {
                    NotFoundViewModel notFoundViewModel = ViewModelHelper.CreateNotFoundViewModel<Sample>();
                    DispatchViewModel(notFoundViewModel);
                    return;
                }
                int documentID = sample.Document.ID;

                // Business
                VoidResult result = _sampleManager.DeleteWithRelatedEntities(sample);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByDocumentID(ViewModel.Document, documentID);
                    propertiesViewModels.RemoveFirst(x => x.Entity.ID == sampleID);

                    SampleGridViewModel gridViewModel = ChildDocumentHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    gridViewModel.List.RemoveFirst(x => x.ID == sampleID);
                }
                else
                {
                    // ToViewModel
                    ViewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SamplePropertiesShow(int sampleID)
        {
            try
            {
                SamplePropertiesViewModel propertiesViewModel = ChildDocumentHelper.GetSamplePropertiesViewModel(ViewModel.Document, sampleID);
                _samplePropertiesPresenter.ViewModel = propertiesViewModel;
                _samplePropertiesPresenter.Show();

                DispatchViewModel(_samplePropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SamplePropertiesClose()
        {
            try
            {
                _samplePropertiesPresenter.Close();
                DispatchViewModel(_samplePropertiesPresenter.ViewModel);

                if (_samplePropertiesPresenter.ViewModel.Successful)
                {
                    // Update list
                    int sampleID = _samplePropertiesPresenter.ViewModel.Entity.ID;
                    Sample sample = _repositoryWrapper.SampleRepository.Get(sampleID);
                    SampleGridViewModel gridViewModel = ChildDocumentHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, sample.Document.ID);
                    _sampleGridPresenter.ViewModel = gridViewModel;
                    _sampleGridPresenter.RefreshListItem(sampleID);
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SamplePropertiesLoseFocus()
        {
            try
            {
                _samplePropertiesPresenter.LoseFocus();
                DispatchViewModel(_samplePropertiesPresenter.ViewModel);

                if (_samplePropertiesPresenter.ViewModel.Successful)
                {
                    // Update list item
                    int sampleID = _samplePropertiesPresenter.ViewModel.Entity.ID;
                    SampleGridViewModel gridViewModel = ChildDocumentHelper.GetSampleGridViewModel_BySampleID(ViewModel.Document, sampleID);
                    _sampleGridPresenter.ViewModel = gridViewModel;
                    _sampleGridPresenter.RefreshListItem(sampleID);
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // DispatchViewModel

        private Dictionary<Type, Action<object>> _dispatchDelegateDictionary;

        private Dictionary<Type, Action<object>> CreateDispatchDelegateDictionary()
        {
            var dictionary = new Dictionary<Type, Action<object>>
            {
                { typeof(AudioFileOutputGridViewModel), DispatchAudioFileOutputGridViewModel },
                { typeof(AudioFileOutputPropertiesViewModel), DispatchAudioFileOutputPropertiesViewModel },
                { typeof(ChildDocumentGridViewModel), DispatchChildDocumentGridViewModel },
                { typeof(ChildDocumentPropertiesViewModel), DispatchChildDocumentPropertiesViewModel },
                { typeof(CurveDetailsViewModel), DispatchCurveDetailsViewModel },
                { typeof(CurveGridViewModel), DispatchCurveGridViewModel },
                { typeof(DocumentCannotDeleteViewModel), DispatchDocumentCannotDeleteViewModel },
                { typeof(DocumentDeletedViewModel), DispatchDocumentDeletedViewModel },
                { typeof(DocumentDeleteViewModel), DispatchDocumentDeleteViewModel },
                { typeof(DocumentDetailsViewModel), DispatchDocumentDetailsViewModel },
                { typeof(DocumentGridViewModel), DispatchDocumentGridViewModel },
                { typeof(DocumentPropertiesViewModel), DispatchDocumentPropertiesViewModel },
                { typeof(DocumentTreeViewModel), DispatchDocumentTreeViewModel },
                { typeof(MenuViewModel), DispatchMenuViewModel },
                { typeof(NotFoundViewModel), DispatchNotFoundViewModel },
                { typeof(OperatorPropertiesViewModel), DispatchOperatorPropertiesViewModel },
                { typeof(OperatorPropertiesViewModel_ForPatchInlet), DispatchOperatorPropertiesViewModel_ForPatchInlet },
                { typeof(OperatorPropertiesViewModel_ForPatchOutlet), DispatchOperatorPropertiesViewModel_ForPatchOutlet },
                { typeof(OperatorPropertiesViewModel_ForValue), DispatchOperatorPropertiesViewModel_ForValue },
                { typeof(PatchDetailsViewModel), DispatchPatchDetailsViewModel },
                { typeof(PatchGridViewModel), DispatchPatchGridViewModel },
                { typeof(SampleGridViewModel), DispatchSampleGridViewModel },
                { typeof(SamplePropertiesViewModel), DispatchSamplePropertiesViewModel },
            };

            return dictionary;
        }

        /// <summary> Applies a view model from a sub-presenter in the right way to the main view model. </summary>
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

        private void DispatchAudioFileOutputGridViewModel(object viewModel2)
        {
            var castedViewModel = (AudioFileOutputGridViewModel)viewModel2;
            
            ViewModel.Document.AudioFileOutputGrid = (AudioFileOutputGridViewModel)viewModel2;

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchAudioFileOutputPropertiesViewModel(object viewModel2)
        {
            var audioFileOutputPropertiesViewModel = (AudioFileOutputPropertiesViewModel)viewModel2;

            if (audioFileOutputPropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                audioFileOutputPropertiesViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(audioFileOutputPropertiesViewModel.ValidationMessages);
            audioFileOutputPropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchChildDocumentGridViewModel(object viewModel2)
        {
            var castedViewModel = (ChildDocumentGridViewModel)viewModel2;

            ChildDocumentTypeEnum childDocumentTypeEnum = (ChildDocumentTypeEnum)castedViewModel.ChildDocumentTypeID;

            switch (childDocumentTypeEnum)
            {
                case ChildDocumentTypeEnum.Instrument:
                    ViewModel.Document.InstrumentGrid = castedViewModel;
                    break;

                case ChildDocumentTypeEnum.Effect:
                    ViewModel.Document.EffectGrid = castedViewModel;
                    break;

                default:
                    throw new ValueNotSupportedException(childDocumentTypeEnum);
            }

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchChildDocumentPropertiesViewModel(object viewModel2)
        {
            var childDocumentPropertiesViewModel = (ChildDocumentPropertiesViewModel)viewModel2;

            if (childDocumentPropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                childDocumentPropertiesViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(childDocumentPropertiesViewModel.ValidationMessages);
            childDocumentPropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveDetailsViewModel(object viewModel2)
        {
            var curveDetailsViewModel = (CurveDetailsViewModel)viewModel2;

            if (curveDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                curveDetailsViewModel.Visible = true;
            }

            ViewModel.ValidationMessages.AddRange(curveDetailsViewModel.ValidationMessages);
            curveDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveGridViewModel(object viewModel2)
        {
            var castedViewModel = (CurveGridViewModel)viewModel2;

            bool isRootDocument = ViewModel.Document.ID == castedViewModel.DocumentID;
            if (isRootDocument)
            {
                ViewModel.Document.CurveGrid = castedViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(ViewModel.Document, castedViewModel.DocumentID);
                childDocumentViewModel.CurveGrid = castedViewModel;
            }

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchDocumentCannotDeleteViewModel(object viewModel2)
        {
            var documentCannotDeleteViewModel = (DocumentCannotDeleteViewModel)viewModel2;

            ViewModel.DocumentCannotDelete = documentCannotDeleteViewModel;
        }

        private void DispatchDocumentDeletedViewModel(object viewModel2)
        {
            var documentDeletedViewModel = (DocumentDeletedViewModel)viewModel2;

            ViewModel.DocumentDeleted = documentDeletedViewModel;

            // TODO: This is quite an assumption.
            ViewModel.DocumentDelete.Visible = false;
            ViewModel.DocumentDetails.Visible = false;

            if (!documentDeletedViewModel.Visible)
            {
                // Also: this might better be done in the action method.
                RefreshDocumentGrid();
            }
        }

        private void DispatchDocumentDeleteViewModel(object viewModel2)
        {
            var documentDeleteViewModel = (DocumentDeleteViewModel)viewModel2;
            ViewModel.DocumentDelete = documentDeleteViewModel;
        }

        private void DispatchDocumentDetailsViewModel(object viewModel2)
        {
            var documentDetailsViewModel = (DocumentDetailsViewModel)viewModel2;
            
            ViewModel.DocumentDetails = documentDetailsViewModel;

            if (documentDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                documentDetailsViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(documentDetailsViewModel.ValidationMessages);
            documentDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchDocumentGridViewModel(object viewModel2)
        {
            var gridViewModel = (DocumentGridViewModel)viewModel2;

            ViewModel.DocumentGrid = gridViewModel;

            if (gridViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                gridViewModel.Visible = true;
            }
        }

        private void DispatchDocumentPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (DocumentPropertiesViewModel)viewModel2;

            ViewModel.Document.DocumentProperties = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchDocumentTreeViewModel(object viewModel2)
        {
            var documentTreeViewModel = (DocumentTreeViewModel)viewModel2;
            ViewModel.Document.DocumentTree = documentTreeViewModel;
        }

        private void DispatchMenuViewModel(object viewModel2)
        {
            var menuViewModel = (MenuViewModel)viewModel2;
            ViewModel.Menu = menuViewModel;
        }

        private void DispatchNotFoundViewModel(object viewModel2)
        {
            var notFoundViewModel = (NotFoundViewModel)viewModel2;

            ViewModel.NotFound = notFoundViewModel;

            // HACK: Checking visibility of the NotFound view model
            // prevents refreshing the DocumentList twice:
            // once when showing the NotFound view model,
            // a second time when clicking OK on it.

            // TODO: Low priority: Eventually the NotFoundViewModel will create even more ambiguity,
            // when it is reused for multiple entity types.

            if (notFoundViewModel.Visible)
            {
                RefreshDocumentGrid();
            }
        }

        private void DispatchOperatorPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel)viewModel2;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchInlet(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchInlet)viewModel2;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchOutlet(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchOutlet)viewModel2;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForValue(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForValue)viewModel2;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchDetailsViewModel(object viewModel2)
        {
            var patchDetailsViewModel = (PatchDetailsViewModel)viewModel2;

            if (patchDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                patchDetailsViewModel.Visible = true;
            }

            ViewModel.ValidationMessages.AddRange(patchDetailsViewModel.ValidationMessages);
            patchDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchGridViewModel(object viewModel2)
        {
            var castedViewModel = (PatchGridViewModel)viewModel2;

            bool isRootDocument = ViewModel.Document.ID == castedViewModel.DocumentID;
            if (isRootDocument)
            {
                ViewModel.Document.PatchGrid = castedViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(ViewModel.Document, castedViewModel.DocumentID);
                childDocumentViewModel.PatchGrid = castedViewModel;
            }

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchSamplePropertiesViewModel(object viewModel2)
        {
            var samplePropertiesViewModel = (SamplePropertiesViewModel)viewModel2;

            if (samplePropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                samplePropertiesViewModel.Visible = true;
            }

            ViewModel.ValidationMessages.AddRange(samplePropertiesViewModel.ValidationMessages);
            samplePropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchSampleGridViewModel(object viewModel2)
        {
            var gridViewModel = (SampleGridViewModel)viewModel2;

            bool isRootDocument = ViewModel.Document.ID == gridViewModel.DocumentID;
            if (isRootDocument)
            {
                ViewModel.Document.SampleGrid = gridViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(ViewModel.Document, gridViewModel.DocumentID);
                childDocumentViewModel.SampleGrid = gridViewModel;
            }

            if (gridViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                gridViewModel.Visible = true;
            }
        }

        // Helpers

        private void HideAllListAndDetailViewModels()
        {
            ViewModel.DocumentGrid.Visible = false;
            ViewModel.DocumentDetails.Visible = false;

            ViewModel.Document.InstrumentGrid.Visible = false;
            ViewModel.Document.EffectGrid.Visible = false;
            ViewModel.Document.SampleGrid.Visible = false;
            ViewModel.Document.CurveGrid.Visible = false;
            ViewModel.Document.AudioFileOutputGrid.Visible = false;
            ViewModel.Document.PatchGrid.Visible = false;

            foreach (CurveDetailsViewModel curveDetailsViewModel in ViewModel.Document.CurveDetailsList)
            {
                curveDetailsViewModel.Visible = false;
            }

            foreach (PatchDetailsViewModel patchDetailsViewModel in ViewModel.Document.PatchDetailsList)
            {
                patchDetailsViewModel.Visible = false;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in ViewModel.Document.ChildDocumentList)
            {
                childDocumentViewModel.SampleGrid.Visible = false;
                childDocumentViewModel.CurveGrid.Visible = false;
                childDocumentViewModel.PatchGrid.Visible = false;

                foreach (PatchDetailsViewModel patchDetailsViewModel in childDocumentViewModel.PatchDetailsList)
                {
                    patchDetailsViewModel.Visible = false;
                }
            }
        }

        private void HideAllPropertiesViewModels()
        {
            ViewModel.DocumentDetails.Visible = false;
            ViewModel.Document.AudioFileOutputPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.CurveDetailsList.ForEach(x => x.Visible = false);
            ViewModel.Document.DocumentProperties.Visible = false;
            ViewModel.Document.OperatorPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.OperatorPropertiesList_ForPatchInlets.ForEach(x => x.Visible = false);
            ViewModel.Document.OperatorPropertiesList_ForPatchOutlets.ForEach(x => x.Visible = false);
            ViewModel.Document.OperatorPropertiesList_ForValues.ForEach(x => x.Visible = false);
            ViewModel.Document.SamplePropertiesList.ForEach(x => x.Visible = false);
            
            // Note that the Samples are the only ones with a Properties view inside the child documents.
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchInlets).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchOutlets).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForValues).ForEach(x => x.Visible = false);
        }

        private void RefreshDocumentGrid()
        {
            _documentGridPresenter.Refresh();
            ViewModel.DocumentGrid  = _documentGridPresenter.ViewModel;
        }

        private void RefreshDocumentTree()
        {
            _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;
            object viewModel2 = _documentTreePresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void RefreshAudioFileOutputGrid()
        {
            object viewModel2 = _audioFileOutputGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void RefreshSampleGrid(SampleGridViewModel sampleGridViewModel)
        {
            _sampleGridPresenter.ViewModel = sampleGridViewModel;
            object viewModel2 = _sampleGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void RefreshPatchGrid(PatchGridViewModel patchGridViewModel)
        {
            _patchGridPresenter.ViewModel = patchGridViewModel;
            object viewModel2 = _patchGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void RefreshChildDocumentGrid(ChildDocumentTypeEnum childDocumentTypeEnum)
        {
            switch (childDocumentTypeEnum)
            {
                case ChildDocumentTypeEnum.Instrument:
                    RefreshInstrumentGrid();
                    break;

                case ChildDocumentTypeEnum.Effect:
                    RefreshEffectGrid();
                    break;

                default:
                    throw new InvalidValueException(childDocumentTypeEnum);
            }
        }

        private void RefreshInstrumentGrid()
        {
            object viewModel2 = _instrumentGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void RefreshEffectGrid()
        {
            object viewModel2 = _effectGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }
    }
}
