using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Framework.Validation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Api;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Canonical;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        // General Actions

        public void Show()
        {
            // try-finally and Rollback are moved to the top-level project.
            ViewModel = ViewModelHelper.CreateEmptyMainViewModel();

            MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
            DispatchViewModel(menuViewModel);

            _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
            _documentGridPresenter.Show();
            DispatchViewModel(_documentGridPresenter.ViewModel);

            ViewModel.WindowTitle = Titles.ApplicationName;
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
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        // AudioFileOutput

        public void AudioFileOutputGridShow()
        {
            try
            {
                _audioFileOutputGridPresenter.Show();
                DispatchViewModel(_audioFileOutputGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputGridClose()
        {
            try
            {
                _audioFileOutputGridPresenter.Close();
                DispatchViewModel(_audioFileOutputGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputCreate()
        {
            try
            {
                // ToEntity
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities(document, mustGenerateName: true);

                // ToViewModel
                AudioFileOutputListItemViewModel listItemViewModel = audioFileOutput.ToListItemViewModel();
                ViewModel.Document.AudioFileOutputGrid.List.Add(listItemViewModel);
                ViewModel.Document.AudioFileOutputGrid.List = ViewModel.Document.AudioFileOutputGrid.List.OrderBy(x => x.Name).ToList();

                AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(_repositories.AudioFileFormatRepository, _repositories.SampleDataTypeRepository, _repositories.SpeakerSetupRepository);
                ViewModel.Document.AudioFileOutputPropertiesList.Add(propertiesViewModel);
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputPropertiesShow(int id)
        {
            try
            {
                _audioFileOutputPropertiesPresenter.ViewModel = ViewModel.Document.AudioFileOutputPropertiesList
                                                                                  .First(x => x.Entity.ID == id);
                _audioFileOutputPropertiesPresenter.Show();

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputPropertiesClose()
        {
            AudioFileOutputPropertiesCloseOrLoseFocus(() => _audioFileOutputPropertiesPresenter.Close());
        }

        public void AudioFileOutputPropertiesLoseFocus()
        {
            AudioFileOutputPropertiesCloseOrLoseFocus(() => _audioFileOutputPropertiesPresenter.LoseFocus());
        }

        private void AudioFileOutputPropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);

                if (_audioFileOutputPropertiesPresenter.ViewModel.Successful)
                {
                    AudioFileOutputGridRefresh();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // CurrentPatches

        public void CurrentPatchesShow()
        {
            try
            {
                _currentPatchesPresenter.Show();
                DispatchViewModel(_currentPatchesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurrentPatchesClose()
        {
            try
            {
                _currentPatchesPresenter.Close();
                DispatchViewModel(_currentPatchesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurrentPatchAdd(int childDocumentID)
        {
            // try-finally and Rollback are moved to the top-level project.

            // The recreation of MidiProcessor in the top-level project is going to need all the data.
            ViewModel.ToEntityWithRelatedEntities(_repositories);

            _currentPatchesPresenter.Add(childDocumentID);
            DispatchViewModel(_currentPatchesPresenter.ViewModel);
        }

        public void CurrentPatchRemove(int childDocumentID)
        {
            // try-finally and Rollback are moved to the top-level project.

            // The recreation of MidiProcessor in the top-level project is going to need all the data.
            ViewModel.ToEntityWithRelatedEntities(_repositories);

            _currentPatchesPresenter.Remove(childDocumentID);
            DispatchViewModel(_currentPatchesPresenter.ViewModel);
        }

        public void CurrentPatchMove(int childDocumentID, int newPosition)
        {
            try
            {
                _currentPatchesPresenter.Move(childDocumentID, newPosition);
                DispatchViewModel(_currentPatchesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurrentPatchesShowAutoPatch()
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Get Entities
                var underlyingPatches = new List<Patch>(ViewModel.Document.CurrentPatches.List.Count);
                foreach (CurrentPatchItemViewModel itemViewModel in ViewModel.Document.CurrentPatches.List)
                {
                    Document document = _repositories.DocumentRepository.Get(itemViewModel.ChildDocumentID);
                    if (document.Patches.Count != 1)
                    {
                        throw new NotEqualException(() => document.Patches.Count, 1);
                    }

                    underlyingPatches.Add(document.Patches[0]);
                }

                // Business
                var patchManager = new PatchManager(_patchRepositories);
                patchManager.AutoPatch(underlyingPatches);

                // ToViewModel
                ViewModel.Document.AutoPatchDetails = patchManager.Patch.ToDetailsViewModel(
                    _repositories.OperatorTypeRepository,
                    _repositories.SampleRepository,
                    _repositories.CurveRepository,
                    _repositories.PatchRepository,
                    _entityPositionManager);

                ViewModel.Document.AutoPatchDetails.Visible = true;
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Curve

        public void CurveGridShow(int documentID)
        {
            try
            {
                bool isRootDocument = documentID == ViewModel.Document.ID;
                if (isRootDocument)
                {
                    // Needed to create uncommitted child documents.
                    ViewModel.ToEntityWithRelatedEntities(_repositories);
                }

                CurveGridViewModel gridViewModel = DocumentViewModelHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                _curveGridPresenter.ViewModel = gridViewModel;
                _curveGridPresenter.Show();
                DispatchViewModel(_curveGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        public void CurveCreate(int documentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                Document document = _repositories.DocumentRepository.TryGet(documentID);

                // Business
                Curve curve = _curveManager.Create(document, mustGenerateName: true);

                // ToViewModel
                CurveGridViewModel gridViewModel = DocumentViewModelHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
                IDAndName listItemViewModel = curve.ToIDAndName();
                gridViewModel.List.Add(listItemViewModel);
                gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

                IList<CurveDetailsViewModel> detailsViewModels = DocumentViewModelHelper.GetCurveDetailsViewModels_ByDocumentID(ViewModel.Document, document.ID);
                CurveDetailsViewModel curveDetailsViewModel = curve.ToDetailsViewModel(_repositories.NodeTypeRepository);
                detailsViewModels.Add(curveDetailsViewModel);

                IList<CurvePropertiesViewModel> propertiesViewModels = DocumentViewModelHelper.GetCurvePropertiesViewModels_ByDocumentID(ViewModel.Document, document.ID);
                CurvePropertiesViewModel curvePropertiesViewModel = curve.ToPropertiesViewModel();
                propertiesViewModels.Add(curvePropertiesViewModel);

                IList<NodePropertiesViewModel> nodePropertiesViewModelList = DocumentViewModelHelper.GetNodePropertiesViewModelList_ByCurveID(ViewModel.Document, curve.ID);
                foreach (Node node in curve.Nodes)
                {
                    NodePropertiesViewModel nodePropertiesViewModel = node.ToPropertiesViewModel(_repositories.NodeTypeRepository);
                    nodePropertiesViewModelList.Add(nodePropertiesViewModel);
                }

                // NOTE: Curves in a child document are only added to the curve lookup of that child document,
                // while curve in the root document are added to all child documents.
                bool isRootDocument = document.ParentDocument == null;
                if (isRootDocument)
                {
                    IDAndName idAndName = curve.ToIDAndName();
                    foreach (PatchDocumentViewModel patchDocumentViewModel in ViewModel.Document.PatchDocumentList)
                    {
                        patchDocumentViewModel.CurveLookup.Add(idAndName);
                        patchDocumentViewModel.CurveLookup = patchDocumentViewModel.CurveLookup.OrderBy(x => x.Name).ToList();
                    }
                }
                else
                {
                    PatchDocumentViewModel patchDocumentViewModel = DocumentViewModelHelper.GetPatchDocumentViewModel(ViewModel.Document, documentID);
                    IDAndName idAndName = curve.ToIDAndName();
                    patchDocumentViewModel.CurveLookup.Add(idAndName);
                    patchDocumentViewModel.CurveLookup = patchDocumentViewModel.CurveLookup.OrderBy(x => x.Name).ToList();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurveDelete(int curveID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Curve curve = _repositories.CurveRepository.Get(curveID);
                IList<int> nodeIDs = curve.Nodes.Select(x => x.ID).ToArray();
                int documentID = curve.Document.ID;
                bool isRootDocument = curve.Document.ParentDocument == null;

                // Business
                VoidResult result = _curveManager.DeleteWithRelatedEntities(curve);
                if (result.Successful)
                {
                    // ToViewModel

                    // NOTE: Order-dependence:
                    // The DocumentViewModelHelper method uses the existence of CurveDetailsViewModel to find a match,
                    // so execute this before removing CurveDetailsViewModel
                    IList<NodePropertiesViewModel> nodePropertiesViewModelList = DocumentViewModelHelper.GetNodePropertiesViewModelList_ByCurveID(ViewModel.Document, curveID);
                    foreach (int nodeID in nodeIDs)
                    {
                        nodePropertiesViewModelList.RemoveFirst(x => x.Entity.ID == nodeID);
                    }

                    IList<CurveDetailsViewModel> detailsViewModels = DocumentViewModelHelper.GetCurveDetailsViewModels_ByDocumentID(ViewModel.Document, documentID);
                    detailsViewModels.RemoveFirst(x => x.Entity.ID == curveID);

                    CurveGridViewModel gridViewModel = DocumentViewModelHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    gridViewModel.List.RemoveFirst(x => x.ID == curveID);

                    IList<CurvePropertiesViewModel> propertiesViewModels = DocumentViewModelHelper.GetCurvePropertiesViewModels_ByDocumentID(ViewModel.Document, documentID);
                    propertiesViewModels.RemoveFirst(x => x.Entity.ID == curveID);

                    // NOTE: 
                    // If it is a curve in the root document, it is present in all child document's curve lookups.
                    // If it is a curve in a child document, it will only be present in the child document's curve lookup and we have to do less work.
                    if (isRootDocument)
                    {
                        ViewModel.Document.PatchDocumentList.ForEach(x => x.CurveLookup.RemoveFirst(y => y.ID == curveID));
                    }
                    else
                    {
                        IList<IDAndName> lookup = DocumentViewModelHelper.GetPatchDocumentViewModel(ViewModel.Document, documentID).CurveLookup;
                        lookup.RemoveFirst(x => x.ID == curveID);
                    }
                }
                else
                {
                    // ToViewModel
                    ViewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurveDetailsShow(int curveID)
        {
            try
            {
                CurveDetailsViewModel detailsViewModel = DocumentViewModelHelper.GetCurveDetailsViewModel(ViewModel.Document, curveID);
                _curveDetailsPresenter.ViewModel = detailsViewModel;
                _curveDetailsPresenter.Show();

                DispatchViewModel(_curveDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        public void CurvePropertiesShow(int curveID)
        {
            try
            {
                CurvePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetCurvePropertiesViewModel(ViewModel.Document, curveID);
                _curvePropertiesPresenter.ViewModel = propertiesViewModel;
                _curvePropertiesPresenter.Show();

                DispatchViewModel(_curvePropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurvePropertiesClose()
        {
            CurvePropertiesCloseOrLoseFocus(() => _curvePropertiesPresenter.Close());
        }

        public void CurvePropertiesLoseFocus()
        {
            CurvePropertiesCloseOrLoseFocus(() => _curvePropertiesPresenter.LoseFocus());
        }

        private void CurvePropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                partialAction();

                DispatchViewModel(_curvePropertiesPresenter.ViewModel);

                if (_curvePropertiesPresenter.ViewModel.Successful)
                {
                    int curveID = _curvePropertiesPresenter.ViewModel.Entity.ID;

                    CurveGridItemRefresh(curveID);
                    CurveLookupsItemsRefresh(curveID);
                }
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
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
                _repositories.Rollback();
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
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        public void DocumentDetailsSave()
        {
            try
            {
                _documentDetailsPresenter.Save();
                DispatchViewModel(_documentDetailsPresenter.ViewModel);

                DocumentGridRefresh();
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        public void DocumentConfirmDelete(int id)
        {
            try
            {
                object viewModel = _documentDeletePresenter.Confirm(id);

                if (viewModel is DocumentDeletedViewModel)
                {
                    _repositories.Commit();
                }

                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        // Document

        public void DocumentOpen(int documentID)
        {
            try
            {
                //Document document = _repositories.DocumentRepository.Get(documentID);
                Document document = _repositories.DocumentRepository.GetComplete(documentID);

                ViewModel.Document = document.ToViewModel(_repositories, _entityPositionManager);

                // Here only the view models are assigned that cannot vary.
                // E.g. SampleGrid is not assigned here, because it can be different for each child document.
                _audioFileOutputGridPresenter.ViewModel = ViewModel.Document.AudioFileOutputGrid;
                _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;
                _scaleGridPresenter.ViewModel = ViewModel.Document.ScaleGrid;
                _currentPatchesPresenter.ViewModel = ViewModel.Document.CurrentPatches;

                ViewModel.WindowTitle = String.Format("{0} - {1}", document.Name, Titles.ApplicationName);

                _menuPresenter.Show(documentIsOpen: true);
                ViewModel.Menu = _menuPresenter.ViewModel;

                CurrentPatchesShow();

                ViewModel.DocumentGrid.Visible = false;
                ViewModel.Document.DocumentTree.Visible = true;

                ViewModel.Document.IsOpen = true;
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentSave()
        {
            if (ViewModel.Document == null) throw new NullException(() => ViewModel.Document);

            try
            {
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositories);

                VoidResult result = _documentManager.ValidateRecursive(document);

                // TODO: Delegate to the manager
                IValidator warningsValidator = new DocumentWarningValidator_Recursive(document, _repositories.SampleRepository, new HashSet<object>());

                if (!result.Successful)
                {
                    _repositories.Rollback();
                }
                else
                {
                    _repositories.Commit();
                }

                ViewModel.ValidationMessages = result.Messages;
                ViewModel.WarningMessages = warningsValidator.ValidationMessages.ToCanonical();
            }
            finally
            {
                _repositories.Rollback();
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
                }
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        public void DocumentPropertiesClose()
        {
            DocumentPropertiesCloseOrLoseFocus(() => _documentPropertiesPresenter.Close());
        }

        public void DocumentPropertiesLoseFocus()
        {
            DocumentPropertiesCloseOrLoseFocus(() => _documentPropertiesPresenter.LoseFocus());
        }

        private void DocumentPropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // ToEntity: This should be just enough to correctly refresh the grids and tree furtheron.
                Document document = ViewModel.Document.ToEntity(_repositories.DocumentRepository);
                ViewModel.Document.PatchDocumentList.Select(x => x.PatchDetails.Entity)
                                                    .ForEach(x => x.ToPatch(_repositories.PatchRepository));
                ViewModel.Document.PatchDocumentList.ForEach(x => x.PatchProperties.ToChildDocument(_repositories.DocumentRepository));

                _documentPropertiesPresenter.ViewModel = ViewModel.Document.DocumentProperties;

                partialAction();

                DispatchViewModel(_documentPropertiesPresenter.ViewModel);

                if (_documentPropertiesPresenter.ViewModel.Successful)
                {
                    DocumentGridRefresh();
                    DocumentTreeRefresh();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentTreeShow()
        {
            try
            {
                _documentTreePresenter.Show();
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentTreeExpandNode(int nodeIndex)
        {
            try
            {
                _documentTreePresenter.ExpandNode(nodeIndex);
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentTreeCollapseNode(int nodeIndex)
        {
            try
            {
                _documentTreePresenter.CollapseNode(nodeIndex);
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentTreeClose()
        {
            try
            {
                _documentTreePresenter.Close();
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Node

        public void NodePropertiesShow(int nodeID)
        {
            try
            {
                NodePropertiesViewModel viewModel = DocumentViewModelHelper.GetNodePropertiesViewModel(ViewModel.Document, nodeID);
                _nodePropertiesPresenter.ViewModel = viewModel;
                _nodePropertiesPresenter.Show();
                DispatchViewModel(_nodePropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void NodePropertiesClose()
        {
            NodePropertiesCloseOrLoseFocus(() => _nodePropertiesPresenter.Close());
        }

        public void NodePropertiesLoseFocus()
        {
            NodePropertiesCloseOrLoseFocus(() => _nodePropertiesPresenter.LoseFocus());
        }

        public void NodePropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                NodePropertiesPresenter partialPresenter = _nodePropertiesPresenter;

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    int nodeID = partialPresenter.ViewModel.Entity.ID;
                    CurveDetailsNodeRefresh(nodeID);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void NodeSelect(int nodeID)
        {
            try
            {
                _curveDetailsPresenter.SelectNode(nodeID);
                DispatchViewModel(_curveDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void NodeCreate()
        {
            try
            {
                if (_curveDetailsPresenter.ViewModel == null) throw new NullException(() => _curveDetailsPresenter.ViewModel);

                // ToEntity
                // Note that the name of the curve is missing in the CurveDetails view.
                Curve curve = _curveDetailsPresenter.ViewModel.ToEntityWithRelatedEntities(_curveRepositories);
                Node afterNode;
                if (_curveDetailsPresenter.ViewModel.SelectedNodeID.HasValue)
                {
                    afterNode = _repositories.NodeRepository.Get(_curveDetailsPresenter.ViewModel.SelectedNodeID.Value);
                }
                else
                {
                    // Insert after last node if none selected.
                    afterNode = curve.Nodes.OrderBy(x => x.Time).Last();
                }

                // Business
                Node node = _curveManager.CreateNode(curve, afterNode);

                // ToViewModel

                // CurveDetails NodeViewModel
                NodeViewModel nodeViewModel = node.ToViewModel();
                _curveDetailsPresenter.ViewModel.Entity.Nodes.Add(nodeViewModel);

                // NodeProperties
                NodePropertiesViewModel propertiesViewModel = node.ToPropertiesViewModel(_repositories.NodeTypeRepository);
                IList<NodePropertiesViewModel> propertiesViewModelList = DocumentViewModelHelper.GetNodePropertiesViewModelList_ByCurveID(ViewModel.Document, curve.ID);
                propertiesViewModelList.Add(propertiesViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void NodeDelete()
        {
            try
            {
                if (_curveDetailsPresenter.ViewModel == null) throw new NullException(() => _curveDetailsPresenter.ViewModel);

                if (!_curveDetailsPresenter.ViewModel.SelectedNodeID.HasValue)
                {
                    ViewModel.ValidationMessages.Add(new Message
                    {
                        PropertyKey = PresentationPropertyNames.SelectedNodeID,
                        Text = PresentationMessages.SelectANodeFirst
                    });
                    return;
                }

                // TODO: Verify this in the business.
                if (_curveDetailsPresenter.ViewModel.Entity.Nodes.Count <= 2)
                {
                    ViewModel.ValidationMessages.Add(new Message
                    {
                        PropertyKey = PropertyNames.Nodes,
                        // TODO: If you would just have done the ToEntity-Business-ToViewModel roundtrip, the validator would have taken care of it.
                        Text = ValidationMessageFormatter.Min(CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Nodes), 2)
                    });
                    return;
                }

                // ToEntity
                // TODO: You might only convert the CurveDetails view models?
                int nodeID = _curveDetailsPresenter.ViewModel.SelectedNodeID.Value;
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                Curve curve = _repositories.CurveRepository.Get(_curveDetailsPresenter.ViewModel.Entity.ID);
                Document document = curve.Document;
                Node node = _repositories.NodeRepository.Get(nodeID);

                // Business
                _curveManager.DeleteNode(node);

                // ToViewModel

                // CurveDetails NodeViewModel
                _curveDetailsPresenter.ViewModel.Entity.Nodes.RemoveFirst(x => x.ID == nodeID);
                _curveDetailsPresenter.ViewModel.SelectedNodeID = null;

                // NodeProperties
                bool isRemoved = ViewModel.Document.NodePropertiesList.TryRemoveFirst(x => x.Entity.ID == nodeID);
                if (!isRemoved)
                {
                    foreach (PatchDocumentViewModel patchDocumentViewModel in ViewModel.Document.PatchDocumentList)
                    {
                        isRemoved = patchDocumentViewModel.NodePropertiesList.TryRemoveFirst(x => x.Entity.ID == nodeID);
                        if (isRemoved)
                        {
                            break;
                        }
                    }
                }
                if (!isRemoved)
                {
                    throw new Exception(String.Format("NodeProperties with Node ID '{0}' not found in either root DocumentViewModel or its PatchDocumentViewModels.", nodeID));
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void NodeMove(int nodeID, double time, double value)
        {
            try
            {
                if (_curveDetailsPresenter.ViewModel == null) throw new NullException(() => _curveDetailsPresenter.ViewModel);

                NodeViewModel nodeViewModel = _curveDetailsPresenter.ViewModel.Entity.Nodes.Where(x => x.ID == nodeID).Single();
                nodeViewModel.Time = time;
                nodeViewModel.Value = value;

                NodePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetNodePropertiesViewModel(ViewModel.Document, nodeID);
                propertiesViewModel.Entity.Time = time;
                propertiesViewModel.Entity.Value = value;
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        /// <summary>
        /// Rotates between node types for the selected node.
        /// If no node is selected, nothing happens.
        /// </summary>
        public void NodeChangeNodeType()
        {
            if (_curveDetailsPresenter.ViewModel == null) throw new NullException(() => _curveDetailsPresenter.ViewModel);

            if (!_curveDetailsPresenter.ViewModel.SelectedNodeID.HasValue)
            {
                return;
            }

            int nodeID = _curveDetailsPresenter.ViewModel.SelectedNodeID.Value;

            NodeViewModel nodeViewModel1 = _curveDetailsPresenter.ViewModel.Entity.Nodes.Where(x => x.ID == nodeID).Single();

            NodeTypeEnum nodeTypeEnum = (NodeTypeEnum)nodeViewModel1.NodeType.ID;
            switch (nodeTypeEnum)
            {
                case NodeTypeEnum.Off:
                    nodeTypeEnum = NodeTypeEnum.Block;
                    break;

                case NodeTypeEnum.Block:
                    nodeTypeEnum = NodeTypeEnum.Line;
                    break;

                case NodeTypeEnum.Line:
                    nodeTypeEnum = NodeTypeEnum.Curve;
                    break;

                case NodeTypeEnum.Curve:
                    nodeTypeEnum = NodeTypeEnum.Off;
                    break;

                default:
                    throw new InvalidValueException(nodeTypeEnum);
            }

            NodeType nodeType = _repositories.NodeTypeRepository.Get((int)nodeTypeEnum);

            nodeViewModel1.NodeType = nodeType.ToIDAndDisplayName();

            NodePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetNodePropertiesViewModel(ViewModel.Document, nodeID);
            propertiesViewModel.Entity.NodeType = nodeType.ToIDAndDisplayName();
        }

        // Operator

        public void OperatorPropertiesShow(int id)
        {
            try
            {
                {
                    OperatorPropertiesViewModel viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel(ViewModel.Document, id);
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
                    OperatorPropertiesViewModel_ForBundle viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForBundle(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForBundle partialPresenter = _operatorPropertiesPresenter_ForBundle;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForCurve viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCurve(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForCurve partialPresenter = _operatorPropertiesPresenter_ForCurve;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForCustomOperator viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCustomOperator(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForCustomOperator partialPresenter = _operatorPropertiesPresenter_ForCustomOperator;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForNumber viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForNumber(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForNumber partialPresenter = _operatorPropertiesPresenter_ForNumber;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForPatchInlet viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForPatchInlet(ViewModel.Document, id);
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
                    OperatorPropertiesViewModel_ForPatchOutlet viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForPatchOutlet(ViewModel.Document, id);
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
                    OperatorPropertiesViewModel_ForSample viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForSample(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForSample partialPresenter = _operatorPropertiesPresenter_ForSample;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForUnbundle viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForUnbundle(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForUnbundle partialPresenter = _operatorPropertiesPresenter_ForUnbundle;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }

                throw new Exception(String.Format("Properties ViewModel not found for Operator with ID '{0}'.", id));
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorPropertiesClose()
        {
            OperatorPropertiesCloseOrLoseFocus(() => _operatorPropertiesPresenter.Close());
        }

        public void OperatorPropertiesClose_ForBundle()
        {
            OperatorPropertiesCloseOrLoseFocus_ForBundle(() => _operatorPropertiesPresenter_ForBundle.Close());
        }

        public void OperatorPropertiesClose_ForCurve()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCurve(() => _operatorPropertiesPresenter_ForCurve.Close());
        }

        public void OperatorPropertiesClose_ForCustomOperator()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(() => _operatorPropertiesPresenter_ForCustomOperator.Close());
        }

        public void OperatorPropertiesClose_ForNumber()
        {
            OperatorPropertiesCloseOrLoseFocus_ForNumber(() => _operatorPropertiesPresenter_ForNumber.Close());
        }

        public void OperatorPropertiesClose_ForPatchInlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(() => _operatorPropertiesPresenter_ForPatchInlet.Close());
        }

        public void OperatorPropertiesClose_ForPatchOutlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(() => _operatorPropertiesPresenter_ForPatchOutlet.Close());
        }

        public void OperatorPropertiesClose_ForSample()
        {
            OperatorPropertiesCloseOrLoseFocus_ForSample(() => _operatorPropertiesPresenter_ForSample.Close());
        }

        public void OperatorPropertiesClose_ForUnbundle()
        {
            OperatorPropertiesCloseOrLoseFocus_ForUnbundle(() => _operatorPropertiesPresenter_ForUnbundle.Close());
        }

        public void OperatorPropertiesLoseFocus()
        {
            OperatorPropertiesCloseOrLoseFocus(() => _operatorPropertiesPresenter.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForBundle()
        {
            OperatorPropertiesCloseOrLoseFocus_ForBundle(() => _operatorPropertiesPresenter_ForBundle.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForCurve()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCurve(() => _operatorPropertiesPresenter_ForCurve.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForCustomOperator()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(() => _operatorPropertiesPresenter_ForCustomOperator.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForNumber()
        {
            OperatorPropertiesCloseOrLoseFocus_ForNumber(() => _operatorPropertiesPresenter_ForNumber.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForPatchInlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(() => _operatorPropertiesPresenter_ForPatchInlet.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForPatchOutlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(() => _operatorPropertiesPresenter_ForPatchOutlet.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForSample()
        {
            OperatorPropertiesCloseOrLoseFocus_ForSample(() => _operatorPropertiesPresenter_ForSample.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForUnbundle()
        {
            OperatorPropertiesCloseOrLoseFocus_ForUnbundle(() => _operatorPropertiesPresenter_ForUnbundle.LoseFocus());
        }

        private void OperatorPropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter partialPresenter = _operatorPropertiesPresenter;
                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForBundle(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForBundle partialPresenter = _operatorPropertiesPresenter_ForBundle;

                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCurve(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForCurve partialPresenter = _operatorPropertiesPresenter_ForCurve;

                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                // Convert the document, child documents + curves
                // because we are about to validate a curve operator's reference to its curve.
                ViewModel.Document.ToHollowDocumentWithHollowChildDocumentsWithCurvesWithName(
                    _repositories.DocumentRepository,
                    _repositories.CurveRepository);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(Action partialAction)
        {
            try
            {
                // Convert whole document, because we need:
                // - OperatorViewModel (from PatchDetail), because we are about to validate
                //   the inlets and outlets too, which are not defined in the OperatorPropertiesViewModel.
                // - The child documents + main patches
                //   because we are about to validate a custom operator's reference to an underlying document.
                // - The chosen Document's Patch's PatchInlets and PatchOutlets,
                //   because we are about to convert those to the custom operator.
                // We could convert only those things, to be a little bit faster,
                // but perhaps now it is better to choose being agnostic over being efficient.
                // (If you ever decie to go the other way, other OperatorProperties actions have code that almost converts all of that already,
                // except for the last bullet point.)
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                OperatorPropertiesPresenter_ForCustomOperator partialPresenter = _operatorPropertiesPresenter_ForCustomOperator;
                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(partialPresenter.ViewModel.ID);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForNumber(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForNumber partialPresenter = _operatorPropertiesPresenter_ForNumber;

                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForPatchInlet partialPresenter = _operatorPropertiesPresenter_ForPatchInlet;

                // ToEntity  (Most of the entity model is needed for Document_SideEffect_UpdateDependentCustomOperators.)
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(partialPresenter.ViewModel.ID);

                    // Refresh Dependencies
                    OperatorViewModels_OfTypeCustomOperators_Refresh();
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForPatchOutlet partialPresenter = _operatorPropertiesPresenter_ForPatchOutlet;

                // ToEntity  (Most of the entity model is needed for Document_SideEffect_UpdateDependentCustomOperators.)
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(partialPresenter.ViewModel.ID);

                    // Refresh Dependent Things
                    OperatorViewModels_OfTypeCustomOperators_Refresh();
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForSample(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForSample partialPresenter = _operatorPropertiesPresenter_ForSample;

                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                // Convert the document, child documents + samples
                // because we are about to validate a sample operator's reference to its sample.
                ViewModel.Document.ToHollowDocumentWithHollowChildDocumentsWithHollowSamplesWithName(
                    _repositories.DocumentRepository,
                    _repositories.SampleRepository);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForUnbundle(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForUnbundle partialPresenter = _operatorPropertiesPresenter_ForUnbundle;

                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorCreate(int operatorTypeID)
        {
            bool isPatchInletOrPatchOutlet = operatorTypeID == (int)OperatorTypeEnum.PatchInlet ||
                                             operatorTypeID == (int)OperatorTypeEnum.PatchOutlet;
            if (isPatchInletOrPatchOutlet)
            {
                OperatorCreate_ForPatchInletOrPatchOutlet(operatorTypeID);
            }
            else
            {
                OperatorCreate_ForOtherOperatorTypes(operatorTypeID);
            }
        }

        // TODO: Now that executing the side-effects is moved to the PatchManager, do I still need this specialized method?
        private void OperatorCreate_ForPatchInletOrPatchOutlet(int operatorTypeID)
        {
            try
            {
                // ToEntity  (Full entity model needed for Document_SideEffect_UpdateDependentCustomOperators.)
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.PatchID);

                // Business
                var patchManager = new PatchManager(patch, _patchRepositories);
                Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

                // ToViewModel

                // OperatorViewModel
                OperatorViewModel operatorViewModel = op.ToViewModelWithRelatedEntitiesAndInverseProperties(
                    _repositories.SampleRepository,
                    _repositories.CurveRepository,
                    _repositories.PatchRepository,
                    _entityPositionManager);
                _patchDetailsPresenter.ViewModel.Entity.Operators.Add(operatorViewModel);

                // OperatorPropertiesViewModel
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
                switch (operatorTypeEnum)
                {
                    case OperatorTypeEnum.PatchInlet:
                        {
                            OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel = op.ToPropertiesViewModel_ForPatchInlet(_repositories.InletTypeRepository);
                            IList<OperatorPropertiesViewModel_ForPatchInlet> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForPatchInlets_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.PatchOutlet:
                        {
                            OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel = op.ToPropertiesViewModel_ForPatchOutlet(_repositories.OutletTypeRepository);
                            IList<OperatorPropertiesViewModel_ForPatchOutlet> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForPatchOutlets_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    default:
                        throw new ValueNotSupportedException(operatorTypeEnum);
                }

                // Refresh Dependent Things
                OperatorViewModels_OfTypeCustomOperators_Refresh();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorCreate_ForOtherOperatorTypes(int operatorTypeID)
        {
            try
            {
                // ToEntity
                Patch patch = _patchDetailsPresenter.ViewModel.ToPatchWithRelatedEntities(_patchRepositories);

                // Business
                var patchManager = new PatchManager(patch, new PatchRepositories(_repositories));
                Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

                // ToViewModel

                // PatchDetails OperatorViewModel
                OperatorViewModel operatorViewModel = op.ToViewModelWithRelatedEntitiesAndInverseProperties(
                    _repositories.SampleRepository,
                    _repositories.CurveRepository,
                    _repositories.PatchRepository,
                    _entityPositionManager);
                _patchDetailsPresenter.ViewModel.Entity.Operators.Add(operatorViewModel);

                // Operator Properties
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
                switch (operatorTypeEnum)
                {
                    case OperatorTypeEnum.Bundle:
                        {
                            OperatorPropertiesViewModel_ForBundle propertiesViewModel = op.ToPropertiesViewModel_ForBundle();
                            IList<OperatorPropertiesViewModel_ForBundle> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForBundles_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.Curve:
                        {
                            OperatorPropertiesViewModel_ForCurve propertiesViewModel = op.ToPropertiesViewModel_ForCurve(_repositories.CurveRepository);
                            IList<OperatorPropertiesViewModel_ForCurve> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForCurves_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.CustomOperator:
                        {
                            OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel = op.ToPropertiesViewModel_ForCustomOperator(_repositories.PatchRepository);
                            IList<OperatorPropertiesViewModel_ForCustomOperator> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForCustomOperators_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.Number:
                        {
                            OperatorPropertiesViewModel_ForNumber propertiesViewModel = op.ToPropertiesViewModel_ForNumber();
                            IList<OperatorPropertiesViewModel_ForNumber> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForNumbers_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.Sample:
                        {
                            OperatorPropertiesViewModel_ForSample propertiesViewModel = op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);
                            IList<OperatorPropertiesViewModel_ForSample> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForSamples_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.Unbundle:
                        {
                            OperatorPropertiesViewModel_ForUnbundle propertiesViewModel = op.ToPropertiesViewModel_ForUnbundle();
                            IList<OperatorPropertiesViewModel_ForUnbundle> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForUnbundles_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    default:
                        {
                            OperatorPropertiesViewModel propertiesViewModel = op.ToPropertiesViewModel();
                            IList<OperatorPropertiesViewModel> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        /// <summary> Deletes the operator selected in PatchDetails. Does not delete anything if no operator is selected. </summary>
        public void OperatorDelete()
        {
            try
            {
                if (_patchDetailsPresenter.ViewModel.SelectedOperator != null)
                {
                    // ToEntity
                    // (Full entity model needed for Document_SideEffect_UpdateDependentCustomOperators and 
                    //  Operator_SideEffect_ApplyUnderlyingPatch)
                    Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                    Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.PatchID);
                    Document document = patch.Document;
                    Operator op = _repositories.OperatorRepository.Get(_patchDetailsPresenter.ViewModel.SelectedOperator.ID);
                    OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

                    // Business
                    var patchManager = new PatchManager(patch, _patchRepositories);
                    patchManager.DeleteOperator(op);

                    // Partial Action
                    _patchDetailsPresenter.DeleteOperator();

                    // ToViewModel
                    if (_patchDetailsPresenter.ViewModel.Successful)
                    {
                        // Do a lot of if'ing and switching to be a little faster in removing the item a specific place in the view model.
                        PatchDocumentViewModel patchDocumentViewModel = ViewModel.Document.PatchDocumentList.Where(x => x.ChildDocumentID == document.ID).First();
                        switch (operatorTypeEnum)
                        {
                            case OperatorTypeEnum.Bundle:
                                patchDocumentViewModel.OperatorPropertiesList_ForBundles.RemoveFirst(x => x.ID == op.ID);
                                break;

                            case OperatorTypeEnum.Curve:
                                patchDocumentViewModel.OperatorPropertiesList_ForCurves.RemoveFirst(x => x.ID == op.ID);
                                break;

                            case OperatorTypeEnum.CustomOperator:
                                patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators.RemoveFirst(x => x.ID == op.ID);
                                break;

                            case OperatorTypeEnum.Number:
                                patchDocumentViewModel.OperatorPropertiesList_ForNumbers.RemoveFirst(x => x.ID == op.ID);
                                break;

                            case OperatorTypeEnum.PatchInlet:
                                patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets.RemoveFirst(x => x.ID == op.ID);
                                break;

                            case OperatorTypeEnum.PatchOutlet:
                                patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets.RemoveFirst(x => x.ID == op.ID);
                                break;

                            case OperatorTypeEnum.Sample:
                                patchDocumentViewModel.OperatorPropertiesList_ForSamples.RemoveFirst(x => x.ID == op.ID);
                                break;

                            case OperatorTypeEnum.Unbundle:
                                patchDocumentViewModel.OperatorPropertiesList_ForUnbundles.RemoveFirst(x => x.ID == op.ID);
                                break;

                            case OperatorTypeEnum.Undefined:
                                throw new ValueNotSupportedException(operatorTypeEnum);

                            default:
                                patchDocumentViewModel.OperatorPropertiesList.RemoveFirst(x => x.ID == op.ID);
                                break;
                        }
                    }

                    // Refresh Dependent Things
                    OperatorViewModels_OfTypeCustomOperators_Refresh();
                }

                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorMove(int operatorID, float centerX, float centerY)
        {
            try
            {
                _patchDetailsPresenter.MoveOperator(operatorID, centerX, centerY);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorChangeInputOutlet(int inletID, int inputOutletID)
        {
            try
            {
                _patchDetailsPresenter.ChangeInputOutlet(inletID, inputOutletID);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorSelect(int operatorID)
        {
            try
            {
                _patchDetailsPresenter.SelectOperator(operatorID);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Patch

        public void PatchDetailsShow(int childDocumentID)
        {
            try
            {
                PatchDetailsViewModel detailsViewModel = DocumentViewModelHelper.GetPatchDetailsViewModel_ByDocumentID(ViewModel.Document, childDocumentID);
                _patchDetailsPresenter.ViewModel = detailsViewModel;
                _patchDetailsPresenter.Show();
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }

            // TODO: Change to permanent solution. (Double click on empty area in PatchDetails should show PatchProperties.)
            PatchPropertiesShow(childDocumentID);
        }

        public void PatchDetailsClose()
        {
            PatchDetailsCloseOrLoseFocus(() => _patchDetailsPresenter.Close());
        }

        public void PatchDetailsLoseFocus()
        {
            PatchDetailsCloseOrLoseFocus(() => _patchDetailsPresenter.LoseFocus());
        }

        private void PatchDetailsCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                int patchID = _patchDetailsPresenter.ViewModel.Entity.PatchID;
                Patch patch = _repositories.PatchRepository.Get(patchID);
                int documentID = patch.Document.ID;

                // Partial Action
                partialAction();

                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        /// <summary> Returns output file path if ViewModel.Successful. <summary>
        public string PatchPlay()
        {
            try
            {
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                string outputFilePath = _patchDetailsPresenter.Play(_repositories);

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
                _repositories.Rollback();
            }
        }

        public void PatchPropertiesShow(int childDocumentID)
        {
            try
            {
                _patchPropertiesPresenter.ViewModel = ViewModel.Document.PatchDocumentList
                                                                        .Where(x => x.ChildDocumentID == childDocumentID)
                                                                        .Select(x => x.PatchProperties)
                                                                        .First();
                _patchPropertiesPresenter.Show();

                DispatchViewModel(_patchPropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PatchPropertiesClose()
        {
            PatchPropertiesCloseOrLoseFocus(() => _patchPropertiesPresenter.Close());
        }

        public void PatchPropertiesLoseFocus()
        {
            PatchPropertiesCloseOrLoseFocus(() => _patchPropertiesPresenter.LoseFocus());
        }

        private void PatchPropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // ToEntity  (Most of the entity model is needed for Document_SideEffect_UpdateDependentCustomOperators.)
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                int childDocumentID = _patchPropertiesPresenter.ViewModel.ChildDocumentID;

                partialAction();

                DispatchViewModel(_patchPropertiesPresenter.ViewModel);

                if (_patchPropertiesPresenter.ViewModel.Successful)
                {
                    DocumentTreeRefresh();
                    CurrentPatchesRefresh();
                    PatchGridsRefresh(); // Refresh all patch grids, because a Patch's group can change.
                    UnderylingDocumentLookupRefresh();
                    OperatorViewModels_OfTypeCustomOperators_Refresh();
                    OperatorProperties_ForCustomOperatorViewModels_Refresh(underlyingPatchID: childDocumentID);
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PatchGridShow(string group)
        {
            try
            {
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                PatchGridViewModel gridViewModel = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(ViewModel.Document, group);
                _patchGridPresenter.ViewModel = gridViewModel;
                _patchGridPresenter.Show();
                DispatchViewModel(_patchGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        /// <param name="group">nullable</param>
        public void PatchCreate(string group)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                Document childDocument = _documentManager.CreateChildDocument(rootDocument, mustGenerateName: true);
                childDocument.GroupName = group;

                var patchManager = new PatchManager(_patchRepositories);
                patchManager.CreatePatch(childDocument, mustGenerateName: true);
                Patch patch = patchManager.Patch;

                // ToViewModel
                IDAndName listItemViewModel = childDocument.ToIDAndName();
                PatchGridViewModel gridViewModel = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(ViewModel.Document, group);
                gridViewModel.List.Add(listItemViewModel);
                gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

                PatchDocumentViewModel documentViewModel = childDocument.ToPatchDocumentViewModel(_repositories, _entityPositionManager);
                ViewModel.Document.PatchDocumentList.Add(documentViewModel);

                IDAndName idAndName = childDocument.ToIDAndName();
                ViewModel.Document.UnderlyingPatchLookup.Add(idAndName);
                ViewModel.Document.UnderlyingPatchLookup = ViewModel.Document.UnderlyingPatchLookup.OrderBy(x => x.Name).ToList();

                DocumentTreeRefresh();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PatchDelete(int childDocumentID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Document childDocument = _repositories.DocumentRepository.Get(childDocumentID);

                // Businesss
                VoidResult result = _documentManager.DeleteWithRelatedEntities(childDocument);
                if (!result.Successful)
                {
                    // ToViewModel
                    ViewModel.PopupMessages.AddRange(result.Messages);
                }
                else
                {
                    // ToViewModel
                    ViewModel.Document.PatchDocumentList.RemoveFirst(x => x.ChildDocumentID == childDocumentID);
                    ViewModel.Document.CurrentPatches.List.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
                    ViewModel.Document.UnderlyingPatchLookup.RemoveFirst(x => x.ID == childDocumentID);
                    ViewModel.Document.DocumentTree.PatchesNode.PatchNodes.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
                    foreach (PatchGroupTreeNodeViewModel nodeViewModel in ViewModel.Document.DocumentTree.PatchesNode.PatchGroupNodes)
                    {
                        nodeViewModel.Patches.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
                    }
                    foreach (PatchGridViewModel gridViewModel in ViewModel.Document.PatchGridList)
                    {
                        gridViewModel.List.TryRemoveFirst(x => x.ID == childDocumentID);
                    }
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Sample

        public void SampleGridShow(int documentID)
        {
            try
            {
                bool isRootDocument = documentID == ViewModel.Document.ID;
                if (!isRootDocument)
                {
                    // Needed to create uncommitted child documents.
                    ViewModel.ToEntityWithRelatedEntities(_repositories);
                }

                SampleGridViewModel gridViewModel = DocumentViewModelHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                _sampleGridPresenter.ViewModel = gridViewModel;
                _sampleGridPresenter.Show();
                DispatchViewModel(_sampleGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
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
                _repositories.Rollback();
            }
        }

        public void SampleCreate(int documentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                Document document = _repositories.DocumentRepository.Get(documentID);

                // Business
                Sample sample = _sampleManager.CreateSample(document, mustGenerateName: true);

                // ToViewModel
                SampleGridViewModel gridViewModel = DocumentViewModelHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
                SampleListItemViewModel listItemViewModel = sample.ToListItemViewModel();
                gridViewModel.List.Add(listItemViewModel);
                gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

                IList<SamplePropertiesViewModel> propertiesViewModels = DocumentViewModelHelper.GetSamplePropertiesViewModels_ByDocumentID(ViewModel.Document, document.ID);
                SamplePropertiesViewModel propertiesViewModel = sample.ToPropertiesViewModel(_sampleRepositories);
                propertiesViewModels.Add(propertiesViewModel);

                // NOTE: Samples in a child document are only added to the sample lookup of that child document,
                // while sample in the root document are added to all child documents.
                bool isRootDocument = document.ParentDocument == null;
                if (isRootDocument)
                {
                    IDAndName idAndName = sample.ToIDAndName();
                    foreach (PatchDocumentViewModel patchDocumentViewModel in ViewModel.Document.PatchDocumentList)
                    {
                        patchDocumentViewModel.SampleLookup.Add(idAndName);
                        patchDocumentViewModel.SampleLookup = patchDocumentViewModel.SampleLookup.OrderBy(x => x.Name).ToList();
                    }
                }
                else
                {
                    PatchDocumentViewModel patchDocumentViewModel = DocumentViewModelHelper.GetPatchDocumentViewModel(ViewModel.Document, documentID);
                    IDAndName idAndName = sample.ToIDAndName();
                    patchDocumentViewModel.SampleLookup.Add(idAndName);
                    patchDocumentViewModel.SampleLookup = patchDocumentViewModel.SampleLookup.OrderBy(x => x.Name).ToList();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void SampleDelete(int sampleID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Sample sample = _repositories.SampleRepository.Get(sampleID);
                int documentID = sample.Document.ID;
                bool isRootDocument = sample.Document.ParentDocument == null;

                // Business
                VoidResult result = _sampleManager.Delete(sample);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<SamplePropertiesViewModel> propertiesViewModels = DocumentViewModelHelper.GetSamplePropertiesViewModels_ByDocumentID(ViewModel.Document, documentID);
                    propertiesViewModels.RemoveFirst(x => x.Entity.ID == sampleID);

                    SampleGridViewModel gridViewModel = DocumentViewModelHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    gridViewModel.List.RemoveFirst(x => x.ID == sampleID);

                    // NOTE: 
                    // If it is a sample in the root document, it is present in all child document's sample lookups.
                    // If it is a sample in a child document, it will only be present in the child document's sample lookup and we have to do less work.
                    if (isRootDocument)
                    {
                        ViewModel.Document.PatchDocumentList.ForEach(x => x.SampleLookup.RemoveFirst(y => y.ID == sampleID));
                    }
                    else
                    {
                        IList<IDAndName> lookup = DocumentViewModelHelper.GetPatchDocumentViewModel(ViewModel.Document, documentID).SampleLookup;
                        lookup.RemoveFirst(x => x.ID == sampleID);
                    }
                }
                else
                {
                    // ToViewModel
                    ViewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void SamplePropertiesShow(int sampleID)
        {
            try
            {
                SamplePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetSamplePropertiesViewModel(ViewModel.Document, sampleID);
                _samplePropertiesPresenter.ViewModel = propertiesViewModel;
                _samplePropertiesPresenter.Show();

                DispatchViewModel(_samplePropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void SamplePropertiesClose()
        {
            SamplePropertiesCloseOrLoseFocus(() => _samplePropertiesPresenter.Close());
        }

        public void SamplePropertiesLoseFocus()
        {
            SamplePropertiesCloseOrLoseFocus(() => _samplePropertiesPresenter.LoseFocus());
        }

        private void SamplePropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                partialAction();

                DispatchViewModel(_samplePropertiesPresenter.ViewModel);

                if (_samplePropertiesPresenter.ViewModel.Successful)
                {
                    int sampleID = _samplePropertiesPresenter.ViewModel.Entity.ID;

                    SampleGridRefreshItem(sampleID);
                    SampleLookupsRefresh(sampleID);
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Scale

        public void ScaleGridShow()
        {
            try
            {
                _scaleGridPresenter.Show();
                DispatchViewModel(_scaleGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleGridClose()
        {
            try
            {
                _scaleGridPresenter.Close();
                DispatchViewModel(_scaleGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleCreate()
        {
            try
            {
                // ToEntity
                // TODO: Can I get away with converting only part of the user input to entities?
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                Scale scale = _scaleManager.Create(document, mustSetDefaults: true, mustGenerateName: true);

                // ToViewModel
                IDAndName listItemViewModel = scale.ToIDAndName();
                ViewModel.Document.ScaleGrid.List.Add(listItemViewModel);
                ViewModel.Document.ScaleGrid.List = ViewModel.Document.ScaleGrid.List.OrderBy(x => x.Name).ToList();

                ToneGridEditViewModel toneGridEditViewModel = scale.ToDetailsViewModel();
                ViewModel.Document.ToneGridEditList.Add(toneGridEditViewModel);

                ScalePropertiesViewModel propertiesViewModel = scale.ToPropertiesViewModel(_repositories.ScaleTypeRepository);
                ViewModel.Document.ScalePropertiesList.Add(propertiesViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleDelete(int id)
        {
            try
            {
                // TODO: It is not very clean to assume business logic will also not in the future have any delete constraints.

                // 'Business' / ToViewModel
                ViewModel.Document.ToneGridEditList.RemoveFirst(x => x.ScaleID == id);
                ViewModel.Document.ScaleGrid.List.RemoveFirst(x => x.ID == id);
                ViewModel.Document.ScalePropertiesList.RemoveFirst(x => x.Entity.ID == id);

                // No need to do ToEntity, 
                // because we are not executing any additional business logic or refreshing 
                // that uses the entity models.
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleShow(int id)
        {
            try
            {
                _toneGridEditPresenter.ViewModel = ViewModel.Document.ToneGridEditList.First(x => x.ScaleID == id);
                _toneGridEditPresenter.Show();
                DispatchViewModel(_toneGridEditPresenter.ViewModel);

                _scalePropertiesPresenter.ViewModel = ViewModel.Document.ScalePropertiesList.First(x => x.Entity.ID == id);
                _scalePropertiesPresenter.Show();
                DispatchViewModel(_scalePropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScalePropertiesClose()
        {
            ScalePropertiesCloseOrLoseFocus(() => _scalePropertiesPresenter.Close());
        }

        public void ScalePropertiesLoseFocus()
        {
            ScalePropertiesCloseOrLoseFocus(() => _scalePropertiesPresenter.LoseFocus());
        }

        private void ScalePropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // TODO: Can I get away with converting only part of the user input to entities?
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                // Update Number Column Title of the ToneGridEdit's ToneGrid.
                int scaleID = _scalePropertiesPresenter.ViewModel.Entity.ID;
                Scale scale = _repositories.ScaleRepository.Get(scaleID);
                ToneGridEditViewModel toneGridEditViewModel = DocumentViewModelHelper.GetToneGridEditViewModel(ViewModel.Document, scaleID);
                toneGridEditViewModel.NumberTitle = ViewModelHelper.GetToneGridEditNumberTitle(scale);

                DispatchViewModel(_scalePropertiesPresenter.ViewModel);

                if (_scalePropertiesPresenter.ViewModel.Successful)
                {
                    ScaleGridRefresh();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Tone

        public void ToneCreate(int scaleID)
        {
            try
            {
                // ToEntity
                // TODO: Can I get away with converting only part of the user input to entities?
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositories);
                Scale scale = _repositories.ScaleRepository.Get(scaleID);

                // Business
                Tone tone = _scaleManager.CreateTone(scale);

                // ToViewModel
                ToneGridEditViewModel toneGridEditViewModel = DocumentViewModelHelper.GetToneGridEditViewModel(ViewModel.Document, scale.ID);
                ToneViewModel toneViewModel = tone.ToViewModel();
                toneGridEditViewModel.Tones.Add(toneViewModel);
                // Do not sort grid, so that the new item appears at the bottom.
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ToneDelete(int id)
        {
            foreach (IList<ToneViewModel> tonesViewModel in ViewModel.Document.ToneGridEditList.Select(x => x.Tones))
            {
                bool isRemoved = tonesViewModel.TryRemoveFirst(x => x.ID == id);
                if (isRemoved)
                {
                    break;
                }
            }
        }

        public void ToneGridEditClose()
        {
            ToneGridEditCloseOrLoseFocus(() => _toneGridEditPresenter.Close());
        }

        public void ToneGridEditLoseFocus()
        {
            ToneGridEditCloseOrLoseFocus(() => _toneGridEditPresenter.LoseFocus());
        }

        private void ToneGridEditCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // TODO: You might be able to do this validation in the partial presenter,
                // if you do not do a full-document ToEntity in the MainPresenter.
                IValidator validator = new ToneGridEditViewModelValidator(_toneGridEditPresenter.ViewModel);
                if (!validator.IsValid)
                {
                    _toneGridEditPresenter.ViewModel.Successful = false;
                    _toneGridEditPresenter.ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                    DispatchViewModel(_toneGridEditPresenter.ViewModel);
                    return;
                }

                // TODO: Can I get away with converting only part of the user input to entities?
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                DispatchViewModel(_toneGridEditPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        /// <summary>
        /// Writes a sine sound with the pitch of the tone to an audio file with a configurable duration.
        /// Returns the output file path if successful.
        /// TODO: This action is too dependent on infrastructure, because the AudioFileOutput business logic is.
        /// Instead of writing to a file it had better write to a stream.
        /// </summary>
        public string TonePlay(int id)
        {
            try
            {
                IValidator validator = new ToneGridEditViewModelValidator(_toneGridEditPresenter.ViewModel);
                if (!validator.IsValid)
                {
                    _toneGridEditPresenter.ViewModel.Successful = false;
                    _toneGridEditPresenter.ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                    DispatchViewModel(_toneGridEditPresenter.ViewModel);
                    return null;
                }

                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Get Entities
                Tone tone = _repositories.ToneRepository.Get(id);

                var underlyingPatches = new List<Patch>(ViewModel.Document.CurrentPatches.List.Count);
                foreach (CurrentPatchItemViewModel itemViewModel in ViewModel.Document.CurrentPatches.List)
                {
                    Document underlyingDocument = _repositories.DocumentRepository.Get(itemViewModel.ChildDocumentID);
                    if (underlyingDocument.Patches.Count != 1)
                    {
                        throw new NotEqualException(() => underlyingDocument.Patches.Count, 1);
                    }

                    underlyingPatches.Add(underlyingDocument.Patches[0]);
                }

                // Business
                Outlet outlet = null;
                if (underlyingPatches.Count != 0)
                {
                    var patchManager = new PatchManager(_patchRepositories);
                    outlet = patchManager.TryAutoPatch_WithTone(tone, underlyingPatches);
                }

                // Fallback to Sine
                if (outlet == null)
                {
                    var x = new PatchApi();
                    double frequency = tone.GetFrequency();
                    outlet = x.Sine(x.Inlet(InletTypeEnum.Frequency, frequency));
                }

                AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.FilePath = _playOutputFilePath;
                audioFileOutput.Duration = DEFAULT_DURATION;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
                _audioFileOutputManager.WriteFile(audioFileOutput);

                return _playOutputFilePath;
            }
            finally
            {
                _repositories.Rollback();
            }
        }
    }
}