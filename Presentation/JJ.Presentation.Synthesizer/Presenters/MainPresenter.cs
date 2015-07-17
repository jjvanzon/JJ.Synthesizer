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

        public MainViewModel ViewModel { get; private set; }

        public void Show()
        {
            try
            {
                ViewModel = ViewModelHelper.CreateEmptyMainViewModel(_repositoryWrapper.OperatorTypeRepository);

                MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
                DispatchViewModel(menuViewModel, null);

                DocumentListViewModel documentListViewModel = _documentListPresenter.Show();
                DispatchViewModel(documentListViewModel, null);

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
                object viewModel2 = _notFoundPresenter.OK();

                DispatchViewModel(viewModel2, null);
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

        public void DocumentListShow(int pageNumber)
        {
            try
            {
                var viewModel2 = _documentListPresenter.Show(pageNumber);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void DocumentListClose()
        {
            try
            {
                var viewModel2 = _documentListPresenter.Close();

                DispatchViewModel(viewModel2, null);
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
                object viewModel2 = _documentDetailsPresenter.Create();

                DispatchViewModel(viewModel2, null);
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
                object viewModel2 = _documentDetailsPresenter.Close();

                DispatchViewModel(viewModel2, null);
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
                object viewModel2 = _documentDetailsPresenter.Save(ViewModel.DocumentDetails);

                DispatchViewModel(viewModel2, null);

                RefreshDocumentList();
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
                object viewModel2 = _documentDeletePresenter.Show(id);

                DispatchViewModel(viewModel2, null);
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
                var viewModel2 = _documentCannotDeletePresenter.OK();

                DispatchViewModel(viewModel2, null);
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
        }

        public void DocumentCancelDelete()
        {
            try
            {
                object viewModel2 = _documentDeletePresenter.Cancel();

                DispatchViewModel(viewModel2, null);
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
                object viewModel2 = _documentDeletedPresenter.OK();

                DispatchViewModel(viewModel2, null);
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
                    _audioFileOutputListPresenter.ViewModel = null;
                    _audioFileOutputPropertiesPresenter.ViewModel = null;
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

                ViewModel.Document = document.ToViewModel(_repositoryWrapper, _entityPositionManager);

                ViewModel.WindowTitle = String.Format("{0} - {1}", document.Name, Titles.ApplicationName);
                ViewModel.Menu = _menuPresenter.Show(documentIsOpen: true);

                ViewModel.DocumentList.Visible = false;
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
                    ViewModel.Menu = _menuPresenter.Show(documentIsOpen: false);

                    _audioFileOutputListPresenter.ViewModel = null;
                    _audioFileOutputPropertiesPresenter.ViewModel = null;
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
        }

        public void DocumentPropertiesShow(int id)
        {
            try
            {
                object viewModel2 = _documentPropertiesPresenter.Show(id);

                DispatchViewModel(viewModel2, null);
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
                DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.Close(ViewModel.Document.DocumentProperties);

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
        }

        public void DocumentPropertiesLoseFocus()
        {

            try
            {
                DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.LoseFocus(ViewModel.Document.DocumentProperties);

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
        }

        public void DocumentTreeShow()
        {
            try
            {
                object viewModel2 = _documentTreePresenter.Show(ViewModel.Document.ID);

                DispatchViewModel(viewModel2, null);
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
                object viewModel2 = _documentTreePresenter.ExpandNode(ViewModel.Document.DocumentTree, nodeIndex);

                DispatchViewModel(viewModel2, null);
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
                object viewModel2 = _documentTreePresenter.CollapseNode(ViewModel.Document.DocumentTree, nodeIndex);

                DispatchViewModel(viewModel2, null);
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
                object viewModel2 = _documentTreePresenter.Close();

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // AudioFileOutput Actions

        public void AudioFileOutputListShow()
        {
            try
            {
                _audioFileOutputListPresenter.ViewModel = ViewModel.Document.AudioFileOutputList;
                _audioFileOutputListPresenter.Show();
                DispatchViewModel(_audioFileOutputListPresenter.ViewModel, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputListClose()
        {
            try
            {
                _audioFileOutputListPresenter.ViewModel = ViewModel.Document.AudioFileOutputList;
                _audioFileOutputListPresenter.Close();
                DispatchViewModel(_audioFileOutputListPresenter.ViewModel, null);
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
                ViewModel.Document.AudioFileOutputList.List.Add(listItemViewModel);
                ViewModel.Document.AudioFileOutputList.List = ViewModel.Document.AudioFileOutputList.List.OrderBy(x => x.Name).ToList();

                AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(_repositoryWrapper.AudioFileFormatRepository, _repositoryWrapper.SampleDataTypeRepository, _repositoryWrapper.SpeakerSetupRepository);
                ViewModel.Document.AudioFileOutputPropertiesList.Add(propertiesViewModel);
                // TODO: Remove outcommented code.
                //ViewModel.Document.AudioFileOutputPropertiesList = ViewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();
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
                //int listIndex;

                // 'Business' / ToViewModel
                //listIndex = ViewModel.Document.AudioFileOutputPropertiesList.IndexOf(x => x.Entity.ID == id);
                //ViewModel.Document.AudioFileOutputPropertiesList.RemoveAt(listIndex);
                ViewModel.Document.AudioFileOutputPropertiesList.RemoveFirst(x => x.Entity.ID == id);

                //listIndex = ViewModel.Document.AudioFileOutputList.List.IndexOf(x => x.ID == id);
                //ViewModel.Document.AudioFileOutputList.List.RemoveAt(listIndex);
                ViewModel.Document.AudioFileOutputList.List.RemoveFirst(x => x.ID == id);
                

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

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel, new ChildDocumentItemAlternativeKey { EntityListIndex = listIndex });
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputPropertiesClose(int id)
        {
            try
            {
                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                int listIndex = ViewModel.Document.AudioFileOutputPropertiesList.IndexOf(x => x.Entity.ID == id);

                _audioFileOutputPropertiesPresenter.ViewModel = ViewModel.Document.AudioFileOutputPropertiesList[listIndex];
                _audioFileOutputPropertiesPresenter.Close();

                if (_audioFileOutputPropertiesPresenter.ViewModel.Successful)
                {
                    // TODO: Remove outcommented code.
                    //// Update properties list
                    //ViewModel.Document.AudioFileOutputPropertiesList = ViewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();

                    // Update list
                    RefreshAudioFileOutputList();
                }

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel, new ChildDocumentItemAlternativeKey { EntityListIndex = listIndex });
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void AudioFileOutputPropertiesLoseFocus(int id)
        {
            try
            {
                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                int listIndex = ViewModel.Document.AudioFileOutputPropertiesList.IndexOf(x => x.Entity.ID == id);

                _audioFileOutputPropertiesPresenter.ViewModel = ViewModel.Document.AudioFileOutputPropertiesList[listIndex];
                _audioFileOutputPropertiesPresenter.LoseFocus();

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel, new ChildDocumentItemAlternativeKey { EntityListIndex = listIndex });

                if (_audioFileOutputPropertiesPresenter.ViewModel.Successful)
                {
                    // TODO: Remove outcommented code.
                    // Update properties list
                    //ViewModel.Document.AudioFileOutputPropertiesList = ViewModel.Document.AudioFileOutputPropertiesList.OrderBy(x => x.Entity.Name).ToList();

                    // Update list
                    RefreshAudioFileOutputList();
                }
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Curve Actions

        public void CurveListShow(int? childDocumentID)
        {
            try
            {
                // Needed to create uncommitted child documents.
                if (childDocumentID.HasValue)
                {
                    Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                }

                object viewModel2 = _curveListPresenter.Show(ViewModel.Document.ID, childDocumentID);
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveListClose()
        {
            try
            {
                object viewModel2 = _curveListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveCreate(int? childDocumentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);

                // Business
                Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(ViewModel.Document.ID, childDocumentID, _repositoryWrapper.DocumentRepository);
                Curve curve = _repositoryWrapper.CurveRepository.Create();
                curve.ID = _repositoryWrapper.IDRepository.GetID();
                curve.LinkTo(document);

                ISideEffect sideEffect = new Curve_SideEffect_GenerateName(curve);
                sideEffect.Execute();

                // ToViewModel
                CurveListViewModel curveListViewModel = ChildDocumentHelper.GetCurveListViewModel(ViewModel.Document, document.ID);
                CurveListItemViewModel listItemViewModel = curve.ToListItemViewModel();
                curveListViewModel.List.Add(listItemViewModel);
                curveListViewModel.List = curveListViewModel.List.OrderBy(x => x.Name).ToList();

                IList<CurveDetailsViewModel> curveDetailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels_ByDocumentID(ViewModel.Document, document.ID);
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
        }

        public void CurveDelete(int curveID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Curve curve = ChildDocumentHelper.TryGetCurve(rootDocument, curveID);
                if (curve == null)
                {
                    NotFoundViewModel notFoundViewModel = CreateNotFoundViewModel<Curve>();
                    DispatchViewModel(notFoundViewModel, null);
                    return;
                }
                int documentID = curve.Document.ID;

                // Business
                VoidResult result = _curveManager.DeleteWithRelatedEntities(curve);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<CurveDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels_ByDocumentID(ViewModel.Document, documentID);
                    int listIndex = detailsViewModels.IndexOf(x => x.Entity.ID == curveID);
                    detailsViewModels.RemoveAt(listIndex);

                    CurveListViewModel listViewModel = ChildDocumentHelper.GetCurveListViewModel(ViewModel.Document, documentID);
                    listViewModel.List.RemoveAt(listIndex);
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
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativeCurveKey(ViewModel.Document, curveID);
                CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.GetCurveDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _curveDetailsPresenter.Show(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveDetailsClose(int curveID)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativeCurveKey(ViewModel.Document, curveID);
                CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.GetCurveDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _curveDetailsPresenter.Close(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void CurveDetailsLoseFocus(int curveID)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativeCurveKey(ViewModel.Document, curveID);
                CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.GetCurveDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _curveDetailsPresenter.LoseFocus(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Effect Actions

        public void EffectListShow()
        {
            try
            {
                object viewModel2 = _effectListPresenter.Show(ViewModel.Document.ID, ChildDocumentTypeEnum.Effect);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void EffectListClose()
        {
            try
            {
                object viewModel2 = _effectListPresenter.Close();
                DispatchViewModel(viewModel2, null);
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
                Document effect = _repositoryWrapper.DocumentRepository.Create();
                effect.ID = _repositoryWrapper.IDRepository.GetID();
                effect.LinkEffectToDocument(parentDocument);

                ISideEffect sideEffect = new Effect_SideEffect_GenerateName(effect);
                sideEffect.Execute();

                // ToViewModel
                ChildDocumentListItemViewModel listItemViewModel = effect.ToChildDocumentListItemViewModel();
                ViewModel.Document.EffectList.List.Add(listItemViewModel);
                ViewModel.Document.EffectList.List = ViewModel.Document.EffectList.List.OrderBy(x => x.Name).ToList();

                ChildDocumentPropertiesViewModel propertiesViewModel = effect.ToChildDocumentPropertiesViewModel();
                ViewModel.Document.EffectPropertiesList.Add(propertiesViewModel);
                ViewModel.Document.EffectPropertiesList = ViewModel.Document.EffectPropertiesList.OrderBy(x => x.Name).ToList();

                ChildDocumentViewModel documentViewModel = effect.ToChildDocumentViewModel(_repositoryWrapper, _entityPositionManager);
                ViewModel.Document.EffectDocumentList.Add(documentViewModel);
                ViewModel.Document.EffectDocumentList = ViewModel.Document.EffectDocumentList.OrderBy(x => x.Name).ToList();

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
                int listIndex = ViewModel.Document.EffectList.List.IndexOf(x => x.ID == effectDocumentID);

                ViewModel.Document.EffectList.List.RemoveAt(listIndex);
                ViewModel.Document.EffectPropertiesList.RemoveAt(listIndex);
                ViewModel.Document.EffectDocumentList.RemoveAt(listIndex);
                ViewModel.Document.DocumentTree.Effects.RemoveAt(listIndex);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Instrument Actions

        public void InstrumentListShow()
        {
            try
            {
                object viewModel2 = _instrumentListPresenter.Show(ViewModel.Document.ID, ChildDocumentTypeEnum.Instrument);

                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void InstrumentListClose()
        {
            try
            {
                object viewModel2 = _instrumentListPresenter.Close();
                DispatchViewModel(viewModel2, null);
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
                Document instrument = _repositoryWrapper.DocumentRepository.Create();
                instrument.ID = _repositoryWrapper.IDRepository.GetID();
                instrument.LinkInstrumentToDocument(parentDocument);

                ISideEffect sideEffect = new Instrument_SideEffect_GenerateName(instrument);
                sideEffect.Execute();

                // ToViewModel
                ChildDocumentListItemViewModel listItemViewModel = instrument.ToChildDocumentListItemViewModel();
                ViewModel.Document.InstrumentList.List.Add(listItemViewModel);
                ViewModel.Document.InstrumentList.List = ViewModel.Document.InstrumentList.List.OrderBy(x => x.Name).ToList();

                ChildDocumentPropertiesViewModel propertiesViewModel = instrument.ToChildDocumentPropertiesViewModel();
                ViewModel.Document.InstrumentPropertiesList.Add(propertiesViewModel);
                ViewModel.Document.InstrumentPropertiesList = ViewModel.Document.InstrumentPropertiesList.OrderBy(x => x.Name).ToList();

                ChildDocumentViewModel documentViewModel = instrument.ToChildDocumentViewModel(_repositoryWrapper, _entityPositionManager);
                ViewModel.Document.InstrumentDocumentList.Add(documentViewModel);
                ViewModel.Document.InstrumentDocumentList = ViewModel.Document.InstrumentDocumentList.OrderBy(x => x.Name).ToList();

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
                int listIndex = ViewModel.Document.InstrumentList.List.IndexOf(x => x.ID == instrumentDocumentID);
                ViewModel.Document.InstrumentList.List.RemoveAt(listIndex);
                ViewModel.Document.InstrumentPropertiesList.RemoveAt(listIndex);
                ViewModel.Document.InstrumentDocumentList.RemoveAt(listIndex);
                ViewModel.Document.DocumentTree.Instruments.RemoveAt(listIndex);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Patch Actions

        public void PatchListShow(int? childDocumentID)
        {
            try
            {
                // Needed to create uncommitted child documents.
                if (childDocumentID.HasValue)
                {
                    Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                }

                object viewModel2 = _patchListPresenter.Show(ViewModel.Document.ID, childDocumentID);
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchListClose()
        {
            try
            {
                object viewModel2 = _patchListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchCreate(int? childDocumentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(ViewModel.Document.ID, childDocumentID, _repositoryWrapper.DocumentRepository);

                // Business
                Patch patch = _repositoryWrapper.PatchRepository.Create();
                patch.ID = _repositoryWrapper.IDRepository.GetID();
                patch.LinkTo(document);

                ISideEffect sideEffect = new Patch_SideEffect_GenerateName(patch);
                sideEffect.Execute();

                // ToViewModel
                PatchListViewModel listViewModel = ChildDocumentHelper.GetPatchListViewModel(ViewModel.Document, document.ID);
                PatchListItemViewModel listItemViewModel = patch.ToListItemViewModel();
                listViewModel.List.Add(listItemViewModel);
                listViewModel.List = listViewModel.List.OrderBy(x => x.Name).ToList();

                IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByDocumentID(ViewModel.Document, document.ID);
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
        }

        public void PatchDelete(int patchID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = ChildDocumentHelper.TryGetPatch(rootDocument, patchID);
                if (patch == null)
                {
                    NotFoundViewModel notFoundViewModel = CreateNotFoundViewModel<Patch>();
                    DispatchViewModel(notFoundViewModel, null);
                    return;
                }
                int documentID = patch.Document.ID;

                // Business
                VoidResult result = _patchManager.DeleteWithRelatedEntities(patch);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByDocumentID(ViewModel.Document, documentID);
                    int listIndex = detailsViewModels.IndexOf(x => x.Entity.ID == patchID);
                    detailsViewModels.RemoveAt(listIndex);

                    PatchListViewModel listViewModel = ChildDocumentHelper.GetPatchListViewModel(ViewModel.Document, documentID);
                    listViewModel.List.RemoveAt(listIndex);
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
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _patchDetailsPresenter.Show(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsClose(int patchID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = ChildDocumentHelper.GetPatch(rootDocument, patchID);
                int documentID = patch.Document.ID;

                // Get the right partial ViewModel
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByAlternativeKey(ViewModel.Document, key);
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
                    PatchListViewModel listViewModel = ChildDocumentHelper.GetPatchListViewModel(ViewModel.Document, documentID);
                    RefreshPatchList(listViewModel);
                }

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsLoseFocus(int patchID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Patch patch = ChildDocumentHelper.GetPatch(rootDocument, patchID);
                int documentID = patch.Document.ID;

                // Get the right partial ViewModel
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByAlternativeKey(ViewModel.Document, key);
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
                    PatchListViewModel listViewModel = ChildDocumentHelper.GetPatchListViewModel(ViewModel.Document, documentID);
                    RefreshPatchList(listViewModel);
                }

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsAddOperator(int patchID, int operatorTypeID)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _patchDetailsPresenter.AddOperator(detailsViewModel, operatorTypeID);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsMoveOperator(int patchID, int operatorID, float centerX, float centerY)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _patchDetailsPresenter.MoveOperator(detailsViewModel, operatorID, centerX, centerY);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsChangeInputOutlet(int patchID, int inletID, int inputOutletID)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _patchDetailsPresenter.ChangeInputOutlet(detailsViewModel, inletID, inputOutletID);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsSelectOperator(int patchID, int operatorID)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _patchDetailsPresenter.SelectOperator(detailsViewModel, operatorID);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        /// <summary>
        /// Deletes the selected operator. Does not delete anything, if no operator is selected.
        /// </summary>
        public void PatchDetailsDeleteOperator(int patchID)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _patchDetailsPresenter.DeleteOperator(detailsViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchDetailsSetValue(int patchID, string value)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                PatchDetailsViewModel viewModel2 = _patchDetailsPresenter.SetValue(detailsViewModel, value);

                // Move messages to popup messages, because the default dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
                ViewModel.PopupMessages.AddRange(viewModel2.ValidationMessages);
                viewModel2.ValidationMessages.Clear();

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void PatchPlay(int patchID, double duration, string sampleFilePath, string outputFilePath)
        {
            try
            {
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativePatchKey(ViewModel.Document, patchID);
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByAlternativeKey(ViewModel.Document, key);

                PatchDetailsViewModel viewModel2 = _patchDetailsPresenter.Play(detailsViewModel, duration, sampleFilePath, outputFilePath, _repositoryWrapper);

                // Move messages to popup messages, because the default dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
                ViewModel.PopupMessages.AddRange(viewModel2.ValidationMessages);
                viewModel2.ValidationMessages.Clear();

                ViewModel.Successful = viewModel2.Successful;

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // Sample Actions

        public void SampleListShow(int? childDocumentID)
        {
            try
            {
                // Needed to create uncommitted child documents.
                if (childDocumentID.HasValue)
                {
                    Document document = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                }

                object viewModel2 = _sampleListPresenter.Show(ViewModel.Document.ID, childDocumentID);
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SampleListClose()
        {
            try
            {
                object viewModel2 = _sampleListPresenter.Close();
                DispatchViewModel(viewModel2, null);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SampleCreate(int? childDocumentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(ViewModel.Document.ID, childDocumentID, _repositoryWrapper.DocumentRepository);

                // Business
                Sample sample = _sampleManager.CreateSample();
                sample.LinkTo(document);

                ISideEffect sideEffect = new Sample_SideEffect_GenerateName(sample);
                sideEffect.Execute();

                // ToViewModel
                SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(ViewModel.Document, document.ID);
                SampleListItemViewModel listItemViewModel = sample.ToListItemViewModel();
                listViewModel.List.Add(listItemViewModel);
                listViewModel.List = listViewModel.List.OrderBy(x => x.Name).ToList();

                IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByDocumentID(ViewModel.Document, document.ID);
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
        }

        public void SampleDelete(int sampleID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Sample sample = ChildDocumentHelper.TryGetSample(rootDocument, sampleID);
                if (sample == null)
                {
                    NotFoundViewModel notFoundViewModel = CreateNotFoundViewModel<Sample>();
                    DispatchViewModel(notFoundViewModel, null);
                    return;
                }
                int documentID = sample.Document.ID;

                // Business
                VoidResult result = _sampleManager.DeleteWithRelatedEntities(sample);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByDocumentID(ViewModel.Document, documentID);
                    int listIndex = propertiesViewModels.IndexOf(x => x.Entity.ID == sampleID);
                    propertiesViewModels.RemoveAt(listIndex);

                    SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(ViewModel.Document, documentID);
                    listViewModel.List.RemoveAt(listIndex);
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
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativeSampleKey(ViewModel.Document, sampleID);
                SamplePropertiesViewModel propertiesViewModel = ChildDocumentHelper.GetSamplePropertiesViewModel_ByAlternativeKey(ViewModel.Document, key);

                object viewModel2 = _samplePropertiesPresenter.Show(propertiesViewModel);

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SamplePropertiesClose(int sampleID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Sample sample = ChildDocumentHelper.GetSample(rootDocument, sampleID);
                int documentID = sample.Document.ID;

                // Get the right partial ViewModel
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativeSampleKey(ViewModel.Document, sampleID);
                IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByAlternativeKey(ViewModel.Document, key);
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
                    SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(ViewModel.Document, documentID);
                    RefreshSampleList(listViewModel);
                }

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        public void SamplePropertiesLoseFocus(int sampleID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositoryWrapper);
                Sample sample = ChildDocumentHelper.GetSample(rootDocument, sampleID);
                int documentID = sample.Document.ID;

                // Get the right partial ViewModel
                ChildDocumentItemAlternativeKey key = ChildDocumentHelper.GetAlternativeSampleKey(ViewModel.Document, sampleID);
                IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByAlternativeKey(ViewModel.Document, key);
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
                    SampleListViewModel listViewModel = ChildDocumentHelper.GetSampleListViewModel(ViewModel.Document, documentID);
                    RefreshSampleList(listViewModel);
                }

                DispatchViewModel(viewModel2, key);
            }
            finally
            {
                _repositoryWrapper.Rollback();
            }
        }

        // DispatchViewModel

        private Dictionary<Type, Action<object, ChildDocumentItemAlternativeKey>> _dispatchDelegateDictionary;

        private Dictionary<Type, Action<object, ChildDocumentItemAlternativeKey>> CreateDispatchDelegateDictionary()
        {
            var dictionary = new Dictionary<Type, Action<object, ChildDocumentItemAlternativeKey>>
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
        private void DispatchViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            if (viewModel2 == null) throw new NullException(() => viewModel2);

            Type viewModelType = viewModel2.GetType();

            Action<object, ChildDocumentItemAlternativeKey> dispatchDelegate;
            if (!_dispatchDelegateDictionary.TryGetValue(viewModelType, out dispatchDelegate))
            {
                throw new UnexpectedViewModelTypeException(viewModel2);
            }

            dispatchDelegate(viewModel2, alternativeKey);
        }

        private void DispatchAudioFileOutputListViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            ViewModel.Document.AudioFileOutputList = (AudioFileOutputListViewModel)viewModel2;

            if (ViewModel.Document.AudioFileOutputList.Visible)
            {
                HideAllListAndDetailViewModels();
                ViewModel.Document.AudioFileOutputList.Visible = true;
            }
        }

        private void DispatchAudioFileOutputPropertiesViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            if (alternativeKey == null) throw new NullException(() => alternativeKey);

            var audioFileOutputPropertiesViewModel = (AudioFileOutputPropertiesViewModel)viewModel2;

            ViewModel.Document.AudioFileOutputPropertiesList[alternativeKey.EntityListIndex] = audioFileOutputPropertiesViewModel;

            if (audioFileOutputPropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                audioFileOutputPropertiesViewModel.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(audioFileOutputPropertiesViewModel.ValidationMessages);
            audioFileOutputPropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchChildDocumentListViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            var childDocumentListViewModel = (ChildDocumentListViewModel)viewModel2;

            switch (childDocumentListViewModel.Keys.ChildDocumentTypeEnum)
            {
                case ChildDocumentTypeEnum.Instrument:
                    ViewModel.Document.InstrumentList = childDocumentListViewModel;
                    break;

                case ChildDocumentTypeEnum.Effect:
                    ViewModel.Document.EffectList = childDocumentListViewModel;
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

        private void DispatchChildDocumentPropertiesViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            var childDocumentPropertiesViewModel = (ChildDocumentPropertiesViewModel)viewModel2;

            int id = childDocumentPropertiesViewModel.ID;

            int? listIndex;
            
            listIndex = ViewModel.Document.InstrumentPropertiesList.TryGetIndexOf(x => x.ID == id);
            if (listIndex.HasValue)
            {
                ViewModel.Document.InstrumentPropertiesList[listIndex.Value] = childDocumentPropertiesViewModel;
            }

            listIndex = ViewModel.Document.EffectPropertiesList.TryGetIndexOf(x => x.ID == id);
            if (listIndex.HasValue)
            {
                ViewModel.Document.EffectPropertiesList[listIndex.Value] = childDocumentPropertiesViewModel;
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

            ViewModel.PopupMessages.AddRange(childDocumentPropertiesViewModel.ValidationMessages);
            childDocumentPropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveDetailsViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            if (alternativeKey == null) throw new NullException(() => alternativeKey);

            var curveDetailsViewModel = (CurveDetailsViewModel)viewModel2;

            IList<CurveDetailsViewModel> list = ChildDocumentHelper.GetCurveDetailsViewModels_ByAlternativeKey(ViewModel.Document, alternativeKey);
            list[alternativeKey.EntityListIndex] = curveDetailsViewModel;

            if (curveDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                curveDetailsViewModel.Visible = true;
            }

            ViewModel.ValidationMessages.AddRange(curveDetailsViewModel.ValidationMessages);
            curveDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveListViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            CurveListViewModel curveListViewModel = (CurveListViewModel)viewModel2;

            if (!curveListViewModel.ChildDocumentID.HasValue)
            {
                ViewModel.Document.CurveList = curveListViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(ViewModel.Document, curveListViewModel.ChildDocumentID.Value);
                childDocumentViewModel.CurveList = curveListViewModel;
            }

            if (curveListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                curveListViewModel.Visible = true;
            }
        }

        private void DispatchDocumentCannotDeleteViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            ViewModel.DocumentCannotDelete = (DocumentCannotDeleteViewModel)viewModel2;
        }

        private void DispatchDocumentDeletedViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            var documentDeletedViewModel = (DocumentDeletedViewModel)viewModel2;

            ViewModel.DocumentDeleted = documentDeletedViewModel;

            // TODO: This is quite an assumption.
            ViewModel.DocumentDelete.Visible = false;
            ViewModel.DocumentDetails.Visible = false;

            if (!documentDeletedViewModel.Visible)
            {
                // Also: this might better be done in the action method.
                RefreshDocumentList();
            }
        }

        private void DispatchDocumentDeleteViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            ViewModel.DocumentDelete = (DocumentDeleteViewModel)viewModel2;
        }

        private void DispatchDocumentDetailsViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            ViewModel.DocumentDetails = (DocumentDetailsViewModel)viewModel2;

            if (ViewModel.DocumentDetails.Visible)
            {
                HideAllListAndDetailViewModels();
                ViewModel.DocumentDetails.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(ViewModel.DocumentDetails.ValidationMessages);
            ViewModel.DocumentDetails.ValidationMessages.Clear();
        }

        private void DispatchDocumentListViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            ViewModel.DocumentList = (DocumentListViewModel)viewModel2;

            if (ViewModel.DocumentList.Visible)
            {
                HideAllListAndDetailViewModels();
                ViewModel.DocumentList.Visible = true;
            }
        }

        private void DispatchDocumentPropertiesViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            ViewModel.Document.DocumentProperties = (DocumentPropertiesViewModel)viewModel2;

            if (ViewModel.Document.DocumentProperties.Visible)
            {
                HideAllPropertiesViewModels();
                ViewModel.Document.DocumentProperties.Visible = true;
            }

            ViewModel.PopupMessages.AddRange(ViewModel.Document.DocumentProperties.ValidationMessages);
            ViewModel.Document.DocumentProperties.ValidationMessages.Clear();
        }

        private void DispatchDocumentTreeViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            ViewModel.Document.DocumentTree = (DocumentTreeViewModel)viewModel2;
        }

        private void DispatchMenuViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            ViewModel.Menu = (MenuViewModel)viewModel2;
        }

        private void DispatchNotFoundViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
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
                RefreshDocumentList();
            }
        }

        private void DispatchPatchDetailsViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            if (alternativeKey == null) throw new NullException(() => alternativeKey);

            var patchDetailsViewModel = (PatchDetailsViewModel)viewModel2;

            IList<PatchDetailsViewModel> list = ChildDocumentHelper.GetPatchDetailsViewModels_ByAlternativeKey(ViewModel.Document, alternativeKey);
            list[alternativeKey.EntityListIndex] = patchDetailsViewModel;

            if (patchDetailsViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                patchDetailsViewModel.Visible = true;
            }

            ViewModel.ValidationMessages.AddRange(patchDetailsViewModel.ValidationMessages);
            patchDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchListViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            PatchListViewModel patchListViewModel = (PatchListViewModel)viewModel2;

            if (!patchListViewModel.ChildDocumentID.HasValue)
            {
                ViewModel.Document.PatchList = patchListViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(ViewModel.Document, patchListViewModel.ChildDocumentID.Value);
                childDocumentViewModel.PatchList = patchListViewModel;
            }

            if (patchListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                patchListViewModel.Visible = true;
            }
        }

        private void DispatchSamplePropertiesViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            if (alternativeKey == null) throw new NullException(() => alternativeKey);

            var samplePropertiesViewModel = (SamplePropertiesViewModel)viewModel2;

            IList<SamplePropertiesViewModel> list = ChildDocumentHelper.GetSamplePropertiesViewModels_ByAlternativeKey(ViewModel.Document, alternativeKey);
            list[alternativeKey.EntityListIndex] = samplePropertiesViewModel;

            if (samplePropertiesViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                samplePropertiesViewModel.Visible = true;
            }

            ViewModel.ValidationMessages.AddRange(samplePropertiesViewModel.ValidationMessages);
            samplePropertiesViewModel.ValidationMessages.Clear();
        }

        private void DispatchSampleListViewModel(object viewModel2, ChildDocumentItemAlternativeKey alternativeKey)
        {
            SampleListViewModel sampleListViewModel = (SampleListViewModel)viewModel2;

            if (!sampleListViewModel.ChildDocumentID.HasValue)
            {
                ViewModel.Document.SampleList = sampleListViewModel;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = ChildDocumentHelper.GetChildDocumentViewModel(ViewModel.Document, sampleListViewModel.ChildDocumentID.Value);
                childDocumentViewModel.SampleList = sampleListViewModel;
            }

            if (sampleListViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                sampleListViewModel.Visible = true;
            }
        }

        // Helpers

        private void HideAllListAndDetailViewModels()
        {
            ViewModel.DocumentList.Visible = false;
            ViewModel.DocumentDetails.Visible = false;

            ViewModel.Document.InstrumentList.Visible = false;
            ViewModel.Document.EffectList.Visible = false;
            ViewModel.Document.SampleList.Visible = false;
            ViewModel.Document.CurveList.Visible = false;
            ViewModel.Document.AudioFileOutputList.Visible = false;
            ViewModel.Document.PatchList.Visible = false;

            foreach (CurveDetailsViewModel curveDetailsViewModel in ViewModel.Document.CurveDetailsList)
            {
                curveDetailsViewModel.Visible = false;
            }

            foreach (PatchDetailsViewModel patchDetailsViewModel in ViewModel.Document.PatchDetailsList)
            {
                patchDetailsViewModel.Visible = false;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in ViewModel.Document.InstrumentDocumentList)
            {
                childDocumentViewModel.SampleList.Visible = false;
                childDocumentViewModel.CurveList.Visible = false;
                childDocumentViewModel.PatchList.Visible = false;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in ViewModel.Document.EffectDocumentList)
            {
                childDocumentViewModel.SampleList.Visible = false;
                childDocumentViewModel.CurveList.Visible = false;
                childDocumentViewModel.PatchList.Visible = false;
            }
        }

        private void HideAllPropertiesViewModels()
        {
            ViewModel.DocumentDetails.Visible = false;
            ViewModel.Document.DocumentProperties.Visible = false;
            ViewModel.Document.AudioFileOutputPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.CurveDetailsList.ForEach(x => x.Visible = false);
            ViewModel.Document.EffectPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.InstrumentPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.SamplePropertiesList.ForEach(x => x.Visible = false);

            // Note that the Samples are the only ones with a Properties view inside the child documents.
            ViewModel.Document.EffectDocumentList.SelectMany(x => x.SamplePropertiesList).ForEach(x => x.Visible = false);
            ViewModel.Document.InstrumentDocumentList.SelectMany(x => x.SamplePropertiesList).ForEach(x => x.Visible = false);
        }

        private void RefreshDocumentList()
        {
            ViewModel.DocumentList = _documentListPresenter.Refresh(ViewModel.DocumentList);
        }

        private void RefreshDocumentTree()
        {
            object viewModel2 = _documentTreePresenter.Refresh(ViewModel.Document.DocumentTree);
            DispatchViewModel(viewModel2, null);
        }

        private void RefreshAudioFileOutputList()
        {
            object viewModel2 = _audioFileOutputListPresenter.Refresh();
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
            AudioFileOutputPropertiesViewModel viewModel = ViewModel.Document.AudioFileOutputPropertiesList
                                                                             .Where(x => x.Entity.ID == id)
                                                                             .Single();
            return viewModel;
        }

        private AudioFileOutputListItemViewModel GetAudioFileOutputListItemViewModel(int id)
        {
            AudioFileOutputListItemViewModel viewModel = ViewModel.Document.AudioFileOutputList.List
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
