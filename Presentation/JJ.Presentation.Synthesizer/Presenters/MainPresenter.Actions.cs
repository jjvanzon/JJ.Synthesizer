using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Framework.Validation.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
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
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        // General Actions

        public void Show()
        {
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
            _notFoundPresenter.OK();
            DispatchViewModel(_notFoundPresenter.ViewModel);
        }

        public void PopupMessagesOK()
        {
            ViewModel.PopupMessages = new List<Message> { };
        }

        // AudioFileOutput

        public void AudioFileOutputGridShow()
        {
            _audioFileOutputGridPresenter.Show();
            DispatchViewModel(_audioFileOutputGridPresenter.ViewModel);
        }

        public void AudioFileOutputGridClose()
        {
            _audioFileOutputGridPresenter.Close();
            DispatchViewModel(_audioFileOutputGridPresenter.ViewModel);
        }

        public void AudioFileOutputCreate()
        {
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities(rootDocument, mustGenerateName: true);

            AudioFileOutputListItemViewModel listItemViewModel = audioFileOutput.ToListItemViewModel();
            ViewModel.Document.AudioFileOutputGrid.List.Add(listItemViewModel);
            ViewModel.Document.AudioFileOutputGrid.List = ViewModel.Document.AudioFileOutputGrid.List.OrderBy(x => x.Name).ToList();

            AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(_repositories.AudioFileFormatRepository, _repositories.SampleDataTypeRepository, _repositories.SpeakerSetupRepository);
            ViewModel.Document.AudioFileOutputPropertiesList.Add(propertiesViewModel);
        }

        public void AudioFileOutputDelete(int id)
        {
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // 'Business' / ToViewModel
            ViewModel.Document.AudioFileOutputPropertiesList.RemoveFirst(x => x.Entity.ID == id);
            ViewModel.Document.AudioFileOutputGrid.List.RemoveFirst(x => x.ID == id);
        }

        public void AudioFileOutputPropertiesShow(int id)
        {
            _audioFileOutputPropertiesPresenter.ViewModel = ViewModel.Document.AudioFileOutputPropertiesList
                                                                              .First(x => x.Entity.ID == id);
            _audioFileOutputPropertiesPresenter.Show();

            DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);
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
            ViewModel.ToEntityWithRelatedEntities(_repositories);

            partialAction();

            DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);

            if (_audioFileOutputPropertiesPresenter.ViewModel.Successful)
            {
                AudioFileOutputGridRefresh();
            }
        }

        // CurrentPatches

        public void CurrentPatchesShow()
        {
            _currentPatchesPresenter.Show();

            DispatchViewModel(_currentPatchesPresenter.ViewModel);
        }

        public void CurrentPatchesClose()
        {
            _currentPatchesPresenter.Close();

            DispatchViewModel(_currentPatchesPresenter.ViewModel);
        }

        public void CurrentPatchAdd(int childDocumentID)
        {
            ViewModel.ToEntityWithRelatedEntities(_repositories);

            _currentPatchesPresenter.Add(childDocumentID);

            DispatchViewModel(_currentPatchesPresenter.ViewModel);
        }

        public void CurrentPatchRemove(int childDocumentID)
        {
            ViewModel.ToEntityWithRelatedEntities(_repositories);

            _currentPatchesPresenter.Remove(childDocumentID);

            DispatchViewModel(_currentPatchesPresenter.ViewModel);
        }

        public void CurrentPatchMove(int childDocumentID, int newPosition)
        {
            ViewModel.ToEntityWithRelatedEntities(_repositories);

            _currentPatchesPresenter.Move(childDocumentID, newPosition);

            DispatchViewModel(_currentPatchesPresenter.ViewModel);
        }

        public void CurrentPatchesPreviewAutoPatch()
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

        // Curve

        public void CurveGridShow(int documentID)
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

        public void CurveGridClose()
        {
            _curveGridPresenter.Close();
            DispatchViewModel(_curveGridPresenter.ViewModel);
        }

        public void CurveCreate(int documentID)
        {
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Document document = _repositories.DocumentRepository.TryGet(documentID);

            Curve curve = _curveManager.Create(document, mustGenerateName: true);

            // ToViewModel
            CurveGridViewModel gridViewModel = DocumentViewModelHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
            IDAndName listItemViewModel = curve.ToIDAndName();
            gridViewModel.List.Add(listItemViewModel);
            gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

            IList<CurveDetailsViewModel> detailsViewModels = DocumentViewModelHelper.GetCurveDetailsViewModelList_ByDocumentID(ViewModel.Document, document.ID);
            CurveDetailsViewModel curveDetailsViewModel = curve.ToDetailsViewModel(_repositories.NodeTypeRepository);
            detailsViewModels.Add(curveDetailsViewModel);

            IList<CurvePropertiesViewModel> propertiesViewModels = DocumentViewModelHelper.GetCurvePropertiesViewModelList_ByDocumentID(ViewModel.Document, document.ID);
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

        public void CurveDelete(int curveID)
        {
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Curve curve = _repositories.CurveRepository.Get(curveID);

            // Business
            IResult result = _curveManager.DeleteWithRelatedEntities(curve);
            if (!result.Successful)
            {
                ViewModel.Successful &= result.Successful;
                ViewModel.PopupMessages.AddRange(result.Messages);
                return;
            }

            IResult result2 = _documentManager.ValidateRecursive(rootDocument);
            if (!result2.Successful)
            {
                ViewModel.Successful &= result2.Successful;
                ViewModel.PopupMessages.AddRange(result2.Messages);
                return;
            }

            // ToViewModel
            ViewModel.Document = rootDocument.ToViewModel(_repositories, _entityPositionManager);

            // TODO: Non-persisted properties are not retained.
        }

        public void CurveDetailsShow(int curveID)
        {
            CurveDetailsViewModel detailsViewModel = DocumentViewModelHelper.GetCurveDetailsViewModel(ViewModel.Document, curveID);
            _curveDetailsPresenter.ViewModel = detailsViewModel;
            _curveDetailsPresenter.Show();

            DispatchViewModel(_curveDetailsPresenter.ViewModel);
        }

        public void CurveDetailsClose()
        {
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            _curveDetailsPresenter.Close();

            DispatchViewModel(_curveDetailsPresenter.ViewModel);
        }

        public void CurveDetailsLoseFocus()
        {
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            _curveDetailsPresenter.LoseFocus();

            // Business
            IResult result = _documentManager.ValidateRecursive(rootDocument);
            if (!result.Successful)
            {
                ViewModel.Successful &= result.Successful;
                ViewModel.PopupMessages.AddRange(result.Messages);
                return;
            }

            // ToViewModel
            DispatchViewModel(_curveDetailsPresenter.ViewModel);
        }

        public void CurvePropertiesShow(int curveID)
        {
            CurvePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetCurvePropertiesViewModel(ViewModel.Document, curveID);
            _curvePropertiesPresenter.ViewModel = propertiesViewModel;
            _curvePropertiesPresenter.Show();

            DispatchViewModel(_curvePropertiesPresenter.ViewModel);
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
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            partialAction();

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            DispatchViewModel(_curvePropertiesPresenter.ViewModel);
            if (!_curvePropertiesPresenter.ViewModel.Successful)
            {
                return;
            }

            ViewModel.Document = rootDocument.ToViewModel(_repositories, _entityPositionManager);
        }

        // Document List

        public void DocumentGridShow(int pageNumber)
        {
            _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
            DocumentGridViewModel viewModel = _documentGridPresenter.Show(pageNumber);
            DispatchViewModel(viewModel);
        }

        public void DocumentGridClose()
        {
            _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
            _documentGridPresenter.Close();
            DispatchViewModel(_documentGridPresenter.ViewModel);
        }

        public void DocumentDetailsCreate()
        {
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();
            DispatchViewModel(viewModel);
        }

        public void DocumentDetailsClose()
        {
            _documentDetailsPresenter.Close();
            DispatchViewModel(_documentDetailsPresenter.ViewModel);
        }

        public void DocumentDetailsSave()
        {
            _documentDetailsPresenter.Save();
            DispatchViewModel(_documentDetailsPresenter.ViewModel);

            DocumentGridRefresh();
        }

        public void DocumentDelete(int id)
        {
            object viewModel = _documentDeletePresenter.Show(id);
            DispatchViewModel(viewModel);
        }

        public void DocumentCannotDeleteOK()
        {
            _documentCannotDeletePresenter.OK();
            DispatchViewModel(_documentCannotDeletePresenter.ViewModel);
        }

        public void DocumentConfirmDelete(int id)
        {
            object viewModel = _documentDeletePresenter.Confirm(id);

            if (viewModel is DocumentDeletedViewModel)
            {
                _repositories.Commit();
            }

            DispatchViewModel(viewModel);
        }

        public void DocumentCancelDelete()
        {
            _documentDeletePresenter.Cancel();
            DispatchViewModel(_documentDeletePresenter.ViewModel);
        }

        public void DocumentDeletedOK()
        {
            DocumentDeletedViewModel viewModel = _documentDeletedPresenter.OK();
            DispatchViewModel(viewModel);
        }

        // Document

        public void DocumentOpen(int documentID)
        {
            Document document = _repositories.DocumentRepository.GetComplete(documentID);

            ViewModel.Document = document.ToViewModel(_repositories, _entityPositionManager);

            // Here only the view models are assigned that cannot vary.
            // E.g. SampleGrid is not assigned here, because it can be different for each child document.
            _audioFileOutputGridPresenter.ViewModel = ViewModel.Document.AudioFileOutputGrid;
            _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;
            _currentPatchesPresenter.ViewModel = ViewModel.Document.CurrentPatches;

            ViewModel.WindowTitle = String.Format("{0} - {1}", document.Name, Titles.ApplicationName);

            _menuPresenter.Show(documentIsOpen: true);
            ViewModel.Menu = _menuPresenter.ViewModel;

            CurrentPatchesShow();

            ViewModel.DocumentGrid.Visible = false;
            ViewModel.Document.DocumentTree.Visible = true;

            ViewModel.Document.IsOpen = true;
        }

        public void DocumentSave()
        {
            // ToEntity
            Document document = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            IResult result = _documentManager.ValidateRecursive(document);

            if (result.Successful)
            {
                _repositories.Commit();
            }

            // TODO: Delegate to the manager
            IValidator warningsValidator = new DocumentWarningValidator_Recursive(document, _repositories.SampleRepository, new HashSet<object>());

            // ToViewModel
            ViewModel.ValidationMessages = result.Messages;
            ViewModel.WarningMessages = warningsValidator.ValidationMessages.ToCanonical();
        }

        public void DocumentClose()
        {
            if (ViewModel.Document.IsOpen)
            {
                ViewModel.Document = ViewModelHelper.CreateEmptyDocumentViewModel();
                ViewModel.WindowTitle = Titles.ApplicationName;

                _menuPresenter.Show(documentIsOpen: false);
                ViewModel.Menu = _menuPresenter.ViewModel;
            }
        }

        public void DocumentPropertiesShow()
        {
            _documentPropertiesPresenter.ViewModel = ViewModel.Document.DocumentProperties;
            _documentPropertiesPresenter.Show();
            DispatchViewModel(_documentPropertiesPresenter.ViewModel);
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
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            partialAction();

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            DispatchViewModel(_documentPropertiesPresenter.ViewModel);
            if (_documentPropertiesPresenter.ViewModel.Successful)
            {
                DocumentGridRefresh();
                DocumentTreeRefresh();
            }
        }

        public void DocumentTreeShow()
        {
            _documentTreePresenter.Show();
            DispatchViewModel(_documentTreePresenter.ViewModel);
        }

        public void DocumentTreeExpandNode(int nodeIndex)
        {
            _documentTreePresenter.ExpandNode(nodeIndex);
            DispatchViewModel(_documentTreePresenter.ViewModel);
        }

        public void DocumentTreeCollapseNode(int nodeIndex)
        {
            _documentTreePresenter.CollapseNode(nodeIndex);
            DispatchViewModel(_documentTreePresenter.ViewModel);
        }

        public void DocumentTreeClose()
        {
            _documentTreePresenter.Close();
            DispatchViewModel(_documentTreePresenter.ViewModel);
        }

        // Node

        public void NodePropertiesShow(int nodeID)
        {
            NodePropertiesViewModel viewModel = DocumentViewModelHelper.GetNodePropertiesViewModel(ViewModel.Document, nodeID);
            _nodePropertiesPresenter.ViewModel = viewModel;
            _nodePropertiesPresenter.Show();
            DispatchViewModel(_nodePropertiesPresenter.ViewModel);
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
            NodePropertiesPresenter partialPresenter = _nodePropertiesPresenter;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            partialAction();

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            if (partialPresenter.ViewModel.Successful)
            {
                int nodeID = partialPresenter.ViewModel.Entity.ID;
                CurveDetailsNodeRefresh(nodeID);
            }
            DispatchViewModel(partialPresenter.ViewModel);
        }

        public void NodeSelect(int nodeID)
        {
            _curveDetailsPresenter.SelectNode(nodeID);
            DispatchViewModel(_curveDetailsPresenter.ViewModel);
        }

        public void NodeCreate()
        {
            if (_curveDetailsPresenter.ViewModel == null) throw new NullException(() => _curveDetailsPresenter.ViewModel);

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            Curve curve = _repositories.CurveRepository.Get(_curveDetailsPresenter.ViewModel.ID);
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

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel

            // CurveDetails NodeViewModel
            NodeViewModel nodeViewModel = node.ToViewModel();
            _curveDetailsPresenter.ViewModel.Nodes.Add(nodeViewModel);

            // NodeProperties
            NodePropertiesViewModel nodePropertiesViewModel = node.ToPropertiesViewModel(_repositories.NodeTypeRepository);
            IList<NodePropertiesViewModel> propertiesViewModelList = DocumentViewModelHelper.GetNodePropertiesViewModelList_ByCurveID(ViewModel.Document, curve.ID);
            propertiesViewModelList.Add(nodePropertiesViewModel);
        }

        public void NodeDelete()
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
            if (_curveDetailsPresenter.ViewModel.Nodes.Count <= 2)
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
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Curve curve = _repositories.CurveRepository.Get(_curveDetailsPresenter.ViewModel.ID);
            Document document = curve.Document;
            int nodeID = _curveDetailsPresenter.ViewModel.SelectedNodeID.Value;
            Node node = _repositories.NodeRepository.Get(nodeID);

            // Business
            _curveManager.DeleteNode(node);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel

            // CurveDetails NodeViewModel
            _curveDetailsPresenter.ViewModel.Nodes.RemoveFirst(x => x.ID == nodeID);
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

        public void NodeMove(int nodeID, double time, double value)
        {
            if (_curveDetailsPresenter.ViewModel == null) throw new NullException(() => _curveDetailsPresenter.ViewModel);

            NodeViewModel nodeViewModel = _curveDetailsPresenter.ViewModel.Nodes.Where(x => x.ID == nodeID).Single();
            nodeViewModel.Time = time;
            nodeViewModel.Value = value;

            NodePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetNodePropertiesViewModel(ViewModel.Document, nodeID);
            propertiesViewModel.Entity.Time = time;
            propertiesViewModel.Entity.Value = value;
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

            // ToViewModel
            int nodeID = _curveDetailsPresenter.ViewModel.SelectedNodeID.Value;

            NodeViewModel nodeViewModel1 = _curveDetailsPresenter.ViewModel.Nodes.Where(x => x.ID == nodeID).Single();

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
            OperatorPropertiesPresenter partialPresenter = _operatorPropertiesPresenter;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Operator op = _repositories.OperatorRepository.Get(partialPresenter.ViewModel.ID);
            Patch patch = op.Patch;

            // Partial action
            partialAction();

            // ToViewModel
            // TODO: Think of a better way.
            OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);
            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
            }

            DispatchViewModel(partialPresenter.ViewModel);
        }

        // TODO: Fix all these other OperatorProperties actions that are similar to eachother.

        private void OperatorPropertiesCloseOrLoseFocus_ForBundle(Action partialAction)
        {
            OperatorPropertiesPresenter_ForBundle partialPresenter = _operatorPropertiesPresenter_ForBundle;

            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

            partialAction();

            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
            }

            DispatchViewModel(partialPresenter.ViewModel);
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCurve(Action partialAction)
        {
            OperatorPropertiesPresenter_ForCurve partialPresenter = _operatorPropertiesPresenter_ForCurve;

            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // TODO: Instead of convert, get both entity and view model.
            OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

            partialAction();

            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
            }

            DispatchViewModel(partialPresenter.ViewModel);
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(Action partialAction)
        {
            OperatorPropertiesPresenter_ForCustomOperator partialPresenter = _operatorPropertiesPresenter_ForCustomOperator;

            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            partialAction();

            IResult result = _documentManager.ValidateRecursive(rootDocument);
            partialPresenter.ViewModel.Successful &= result.Successful;
            partialPresenter.ViewModel.ValidationMessages.AddRange(result.Messages);

            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(partialPresenter.ViewModel.ID);
            }

            DispatchViewModel(partialPresenter.ViewModel);
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForNumber(Action partialAction)
        {
            OperatorPropertiesPresenter_ForNumber partialPresenter = _operatorPropertiesPresenter_ForNumber;

            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

            partialAction();

            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
            }

            DispatchViewModel(partialPresenter.ViewModel);
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(Action partialAction)
        {
            OperatorPropertiesPresenter_ForPatchInlet partialPresenter = _operatorPropertiesPresenter_ForPatchInlet;

            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            partialAction();

            IResult result = _documentManager.ValidateRecursive(rootDocument);
            partialPresenter.ViewModel.Successful &= result.Successful;
            partialPresenter.ViewModel.ValidationMessages.AddRange(result.Messages);

            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(partialPresenter.ViewModel.ID);

                // Refresh Dependencies
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator);
            }

            DispatchViewModel(partialPresenter.ViewModel);
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(Action partialAction)
        {
            OperatorPropertiesPresenter_ForPatchOutlet partialPresenter = _operatorPropertiesPresenter_ForPatchOutlet;

            // ToEntity 
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            partialAction();

            // Do a full validate, because of the error-proneness of the CustomOperator-UnderlyingPatch synchronization,
            // to prevent getting stuck in a screen in which you cannot correct the problem.
            IResult result = _documentManager.ValidateRecursive(rootDocument);
            partialPresenter.ViewModel.Successful &= result.Successful;
            partialPresenter.ViewModel.ValidationMessages.AddRange(result.Messages);

            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(partialPresenter.ViewModel.ID);

                // Refresh Dependent Things
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator);
            }

            DispatchViewModel(partialPresenter.ViewModel);
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForSample(Action partialAction)
        {
            OperatorPropertiesPresenter_ForSample partialPresenter = _operatorPropertiesPresenter_ForSample;

            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

            partialAction();

            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
            }

            DispatchViewModel(partialPresenter.ViewModel);
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForUnbundle(Action partialAction)
        {
            OperatorPropertiesPresenter_ForUnbundle partialPresenter = _operatorPropertiesPresenter_ForUnbundle;

            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

            partialAction();

            if (partialPresenter.ViewModel.Successful)
            {
                PatchDetails_RefreshOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
            }

            DispatchViewModel(partialPresenter.ViewModel);
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
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.PatchID);

            // Business
            var patchManager = new PatchManager(patch, _patchRepositories);
            Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

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
            OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator);
        }

        private void OperatorCreate_ForOtherOperatorTypes(int operatorTypeID)
        {
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.PatchID);

            // Business
            var patchManager = new PatchManager(patch, _patchRepositories);
            Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

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

        /// <summary> Deletes the operator selected in PatchDetails. Does not delete anything if no operator is selected. </summary>
        public void OperatorDelete()
        {
            if (_patchDetailsPresenter.ViewModel.SelectedOperator != null)
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.PatchID);
                Document document = patch.Document;
                Operator op = _repositories.OperatorRepository.Get(_patchDetailsPresenter.ViewModel.SelectedOperator.ID);
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

                // Business
                var patchManager = new PatchManager(patch, _patchRepositories);
                patchManager.DeleteOperator(op);

                IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
                if (!validationResult.Successful)
                {
                    ViewModel.Successful &= validationResult.Successful;
                    ViewModel.PopupMessages.AddRange(validationResult.Messages);
                    return;
                }

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
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator);
            }

            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        public void OperatorMove(int operatorID, float centerX, float centerY)
        {
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            _patchDetailsPresenter.MoveOperator(operatorID, centerX, centerY);

            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        public void OperatorChangeInputOutlet(int inletID, int inputOutletID)
        {
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            _patchDetailsPresenter.ChangeInputOutlet(inletID, inputOutletID);

            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        public void OperatorSelect(int operatorID)
        {
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            _patchDetailsPresenter.SelectOperator(operatorID);

            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        // Patch

        public void PatchDetailsShow(int childDocumentID)
        {
            PatchDetailsViewModel detailsViewModel = DocumentViewModelHelper.GetPatchDetailsViewModel_ByDocumentID(ViewModel.Document, childDocumentID);
            _patchDetailsPresenter.ViewModel = detailsViewModel;
            _patchDetailsPresenter.Show();
            DispatchViewModel(_patchDetailsPresenter.ViewModel);

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
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            int patchID = _patchDetailsPresenter.ViewModel.Entity.PatchID;
            Patch patch = _repositories.PatchRepository.Get(patchID);
            int documentID = patch.Document.ID;

            // Partial Action
            partialAction();

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        /// <summary> Returns output file path if ViewModel.Successful. <summary>
        public string PatchPlay()
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

        public void PatchPropertiesShow(int childDocumentID)
        {
            _patchPropertiesPresenter.ViewModel = ViewModel.Document.PatchDocumentList
                                                                    .Where(x => x.ChildDocumentID == childDocumentID)
                                                                    .Select(x => x.PatchProperties)
                                                                    .First();
            _patchPropertiesPresenter.Show();

            DispatchViewModel(_patchPropertiesPresenter.ViewModel);
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
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            int childDocumentID = _patchPropertiesPresenter.ViewModel.ChildDocumentID;

            // Partial Action
            partialAction();

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            // This might have to be done right after partial action>?
            DispatchViewModel(_patchPropertiesPresenter.ViewModel);

            if (_patchPropertiesPresenter.ViewModel.Successful)
            {
                DocumentTreeRefresh();
                CurrentPatchesRefresh();
                PatchGridsRefresh(); // Refresh all patch grids, because a Patch's group can change.
                UnderylingDocumentLookupRefresh();
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator);
                OperatorProperties_ForCustomOperatorViewModels_Refresh(underlyingPatchID: childDocumentID);
            }
        }

        public void PatchGridShow(string group)
        {
            ViewModel.ToEntityWithRelatedEntities(_repositories);

            PatchGridViewModel gridViewModel = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(ViewModel.Document, group);
            _patchGridPresenter.ViewModel = gridViewModel;
            _patchGridPresenter.Show();
            DispatchViewModel(_patchGridPresenter.ViewModel);
        }

        public void PatchGridClose()
        {
            _patchGridPresenter.Close();
            DispatchViewModel(_patchGridPresenter.ViewModel);
        }

        /// <param name="group">nullable</param>
        public void PatchCreate(string group)
        {
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            Document childDocument = _documentManager.CreateChildDocument(rootDocument, mustGenerateName: true);
            childDocument.GroupName = group;

            var patchManager = new PatchManager(_patchRepositories);
            patchManager.CreatePatch(childDocument, mustGenerateName: true);
            Patch patch = patchManager.Patch;

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            ChildDocumentIDAndNameViewModel listItemViewModel = childDocument.ToChildDocumentIDAndNameViewModel();
            PatchGridViewModel gridViewModel = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(ViewModel.Document, group);
            gridViewModel.List.Add(listItemViewModel);
            gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

            PatchDocumentViewModel documentViewModel = childDocument.ToPatchDocumentViewModel(_repositories, _entityPositionManager);
            ViewModel.Document.PatchDocumentList.Add(documentViewModel);

            ChildDocumentIDAndNameViewModel lookupItemViewModel = childDocument.ToChildDocumentIDAndNameViewModel();
            ViewModel.Document.UnderlyingPatchLookup.Add(lookupItemViewModel);
            ViewModel.Document.UnderlyingPatchLookup = ViewModel.Document.UnderlyingPatchLookup.OrderBy(x => x.Name).ToList();

            DocumentTreeRefresh();
        }

        public void PatchDelete(int childDocumentID)
        {
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Document childDocument = _repositories.DocumentRepository.Get(childDocumentID);

            // Businesss
            IResult result = _documentManager.DeleteWithRelatedEntities(childDocument);
            if (!result.Successful)
            {
                // ToViewModel
                ViewModel.PopupMessages.AddRange(result.Messages);
                return;
            }

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            ViewModel.Document.PatchDocumentList.RemoveFirst(x => x.ChildDocumentID == childDocumentID);
            ViewModel.Document.CurrentPatches.List.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
            ViewModel.Document.UnderlyingPatchLookup.RemoveFirst(x => x.ChildDocumentID == childDocumentID);
            ViewModel.Document.DocumentTree.PatchesNode.PatchNodes.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
            foreach (PatchGroupTreeNodeViewModel nodeViewModel in ViewModel.Document.DocumentTree.PatchesNode.PatchGroupNodes)
            {
                nodeViewModel.Patches.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
            }
            foreach (PatchGridViewModel gridViewModel in ViewModel.Document.PatchGridList)
            {
                gridViewModel.List.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
            }

        }

        // Sample

        public void SampleGridShow(int documentID)
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

        public void SampleGridClose()
        {
            _sampleGridPresenter.Close();
            DispatchViewModel(_sampleGridPresenter.ViewModel);
        }

        public void SampleCreate(int documentID)
        {
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Document document = _repositories.DocumentRepository.Get(documentID);

            // Business
            Sample sample = _sampleManager.CreateSample(document, mustGenerateName: true);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

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

        public void SampleDelete(int sampleID)
        {
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Sample sample = _repositories.SampleRepository.Get(sampleID);
            int documentID = sample.Document.ID;
            bool isRootDocument = sample.Document.ParentDocument == null;

            // Business
            IResult result = _sampleManager.Delete(sample);
            if (!result.Successful)
            {
                // ToViewModel
                ViewModel.PopupMessages = result.Messages;
                return;
            }

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

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

        public void SamplePropertiesShow(int sampleID)
        {
            SamplePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetSamplePropertiesViewModel(ViewModel.Document, sampleID);
            _samplePropertiesPresenter.ViewModel = propertiesViewModel;
            _samplePropertiesPresenter.Show();

            DispatchViewModel(_samplePropertiesPresenter.ViewModel);
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
            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial action
            partialAction();
            DispatchViewModel(_samplePropertiesPresenter.ViewModel);

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                ViewModel.Successful &= validationResult.Successful;
                ViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            if (_samplePropertiesPresenter.ViewModel.Successful)
            {
                int sampleID = _samplePropertiesPresenter.ViewModel.Entity.ID;

                SampleGridRefreshItem(sampleID);
                SampleLookupsRefresh(sampleID);
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.Sample);
            }
        }

        // Scale

        public void ScaleGridShow()
        {
            // GetViewModel
            ScaleGridViewModel userInput = ViewModel.Document.ScaleGrid;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            ScaleGridViewModel viewModel = _scaleGridPresenter.Show(userInput);
            if (!viewModel.Successful)
            {
                DispatchViewModel(viewModel);
                return;
            }

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                viewModel.Successful = false;
                viewModel.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(userInput);
                return;
            }

            // Successful
            viewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void ScaleGridClose()
        {
            // GetViewModel
            ScaleGridViewModel userInput = ViewModel.Document.ScaleGrid;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            ScaleGridViewModel viewModel = _scaleGridPresenter.Close(userInput);
            if (!viewModel.Successful)
            {
                DispatchViewModel(viewModel);
                return;
            }

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                viewModel.Successful = false;
                viewModel.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(userInput);
                return;
            }

            // Successful
            viewModel.Successful = true;

            // Dispatch ViewModel
            DispatchViewModel(viewModel);
        }

        public void ScaleCreate()
        {
            // GetViewModel
            ScaleGridViewModel userInput = ViewModel.Document.ScaleGrid;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            Scale scale = _scaleManager.Create(rootDocument, mustSetDefaults: true, mustGenerateName: true);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                userInput.Successful = false;
                userInput.ValidationMessages = validationResult.Messages;
                DispatchViewModel(userInput);
                return;
            }

            // ToViewModel
            IDAndName listItemViewModel = scale.ToIDAndName();
            ViewModel.Document.ScaleGrid.List.Add(listItemViewModel);
            ViewModel.Document.ScaleGrid.List = ViewModel.Document.ScaleGrid.List.OrderBy(x => x.Name).ToList();

            ToneGridEditViewModel toneGridEditViewModel = scale.ToToneGridEditViewModel();
            ViewModel.Document.ToneGridEditList.Add(toneGridEditViewModel);
            
            ScalePropertiesViewModel scalePropertiesViewModel = scale.ToPropertiesViewModel(_repositories.ScaleTypeRepository);
            ViewModel.Document.ScalePropertiesList.Add(scalePropertiesViewModel);

            // Set Successful
            userInput.Successful = true;
            scalePropertiesViewModel.Successful = true;
            toneGridEditViewModel.Successful = true;
        }

        public void ScaleDelete(int id)
        {
            // GetViewModel
            ScaleGridViewModel userInput = ViewModel.Document.ScaleGrid;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Scale scale = _repositories.ScaleRepository.Get(id);

            // Business
            _scaleManager.DeleteWithRelatedEntities(id);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                userInput.Successful = false;
                userInput.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(userInput);
                return;
            }

            // ToViewModel
            ViewModel.Document.ScaleGrid.List.RemoveFirst(x => x.ID == id);
            ViewModel.Document.ToneGridEditList.RemoveFirst(x => x.ScaleID == id);
            ViewModel.Document.ScalePropertiesList.RemoveFirst(x => x.Entity.ID == id);

            // Successful
            userInput.Successful = true;
        }

        public void ScaleShow(int id)
        {
            // GetViewModel
            ScalePropertiesViewModel userInput1 = DocumentViewModelHelper.GetScalePropertiesViewModel(ViewModel.Document, id);
            ToneGridEditViewModel userInput2 = DocumentViewModelHelper.GetToneGridEditViewModel(ViewModel.Document, scaleID: id);

            // Set !Successful
            userInput1.Successful = false;
            userInput2.Successful = false;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Actions
            ScalePropertiesViewModel scalePropertiesViewModel = _scalePropertiesPresenter.Show(userInput1);
            if (!scalePropertiesViewModel.Successful)
            {
                DispatchViewModel(scalePropertiesViewModel);
                return;
            }

            ToneGridEditViewModel toneGridEditViewModel = _toneGridEditPresenter.Show(userInput2);
            if (!toneGridEditViewModel.Successful)
            {
                DispatchViewModel(toneGridEditViewModel);
                return;
            }

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                scalePropertiesViewModel.Successful = false;
                scalePropertiesViewModel.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(scalePropertiesViewModel);

                toneGridEditViewModel.Successful = false;
                DispatchViewModel(toneGridEditViewModel);
                return;
            }

            // Set Successful
            scalePropertiesViewModel.Successful = true;
            toneGridEditViewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(scalePropertiesViewModel);
            DispatchViewModel(toneGridEditViewModel);
        }

        public void ScalePropertiesClose()
        {
            ScalePropertiesCloseOrLoseFocus(x => _scalePropertiesPresenter.Close(x));
        }

        public void ScalePropertiesLoseFocus()
        {
            ScalePropertiesCloseOrLoseFocus(x => _scalePropertiesPresenter.LoseFocus(x));
        }

        private void ScalePropertiesCloseOrLoseFocus(Func<ScalePropertiesViewModel, ScalePropertiesViewModel> partialAction)
        {
            // Get ViewModel
            ScalePropertiesViewModel userInput = DocumentViewModelHelper.GetVisibleScalePropertiesViewModel(ViewModel.Document);

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Scale scale = _repositories.ScaleRepository.Get(userInput.Entity.ID);

            // Partial Action
            ScalePropertiesViewModel viewModel = partialAction(userInput);
            if (!viewModel.Successful)
            {
                DispatchViewModel(viewModel);
                return;
            }

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                viewModel.Successful = false;
                viewModel.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(userInput);
                return;
            }

            // Successful
            viewModel.Successful = true;

            // Dispatch ViewModel
            DispatchViewModel(viewModel);

            // Refresh
            ToneGridEditRefresh(scale.ID);
            ScaleGridRefresh();
        }

        // Tone

        public void ToneCreate(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(ViewModel.Document);

            // Set !Successful
            userInput.Successful = false;

            // ViewModelValidator
            IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
            if (!viewModelValidator.IsValid)
            {
                userInput.ValidationMessages = viewModelValidator.ValidationMessages.ToCanonical();
                DispatchViewModel(userInput);
                return;
            }

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Scale scale = _repositories.ScaleRepository.Get(scaleID);

            // Business
            Tone tone = _scaleManager.CreateTone(scale);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                userInput.Successful = false;
                userInput.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(userInput);
                return;
            }

            // ToViewModel
            ToneViewModel toneViewModel = tone.ToViewModel();
            userInput.Tones.Add(toneViewModel);
            // Do not sort grid, so that the new item appears at the bottom.

            // Successful
            userInput.Successful = true;

            // DispatchViewModel
            DispatchViewModel(userInput);
        }

        public void ToneDelete(int id)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(ViewModel.Document);

            // Set !Successful
            userInput.Successful = false;

            // ViewModelValidator
            IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
            if (!viewModelValidator.IsValid)
            {
                userInput.Successful = false;
                userInput.ValidationMessages = viewModelValidator.ValidationMessages.ToCanonical();
                DispatchViewModel(userInput);
                return;
            }

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Tone tone = _repositories.ToneRepository.Get(id);
            Scale scale = tone.Scale;

            // Business
            _scaleManager.DeleteTone(tone);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                userInput.Successful = false;
                userInput.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(userInput);
                return;
            }

            // ToViewModel
            ToneGridEditViewModel viewModel = scale.ToToneGridEditViewModel();

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            // Successful
            viewModel.Successful = true;

            // Dispatch ViewModel
            DispatchViewModel(viewModel);
        }

        public void ToneGridEditClose()
        {
            ToneGridEditCloseOrLoseFocus(x => _toneGridEditPresenter.Close(x));
        }

        public void ToneGridEditLoseFocus()
        {
            ToneGridEditCloseOrLoseFocus(x => _toneGridEditPresenter.LoseFocus(x));
        }

        private void ToneGridEditCloseOrLoseFocus(Func<ToneGridEditViewModel, ToneGridEditViewModel> partialAction)
        {
            // Get ViewModel
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(ViewModel.Document);

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            ToneGridEditViewModel viewModel = partialAction(userInput);
            if (!viewModel.Successful)
            {
                DispatchViewModel(viewModel);
                return;
            }

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                viewModel.Successful = false;
                viewModel.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(viewModel);
                return;
            }

            // Successful
            viewModel.Successful = true;

            // Dispatch ViewModel
            DispatchViewModel(viewModel);
        }

        /// <summary>
        /// Writes a sine sound with the pitch of the tone to an audio file with a configurable duration.
        /// Returns the output file path if successful.
        /// TODO: This action is too dependent on infrastructure, because the AudioFileOutput business logic is.
        /// Instead of writing to a file it had better write to a stream.
        /// </summary>
        public string TonePlay(int id)
        {
            // NOTE:
            // Cannot use partial presenter, because this action uses both
            // ToneGridEditViewModel and CurrentPatches view model.

            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(ViewModel.Document);

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validator
            IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
            if (!viewModelValidator.IsValid)
            {
                userInput.Successful = false;
                userInput.ValidationMessages = viewModelValidator.ValidationMessages.ToCanonical();
                DispatchViewModel(userInput);
                return null;
            }

            // ToEntity
            Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
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

            if (outlet == null) // Fallback to Sine
            {
                var x = new PatchApi();
                double frequency = tone.GetFrequency();
                outlet = x.Sine(x.PatchInlet(InletTypeEnum.Frequency, frequency));
            }

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                userInput.Successful = false;
                userInput.ValidationMessages = validationResult.Messages;
                return null;
            }

            IPatchCalculator patchCalculator = CreatePatchCalculator(_patchRepositories, outlet);

            // Infrastructure
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities();
            audioFileOutput.FilePath = _playOutputFilePath;
            audioFileOutput.Duration = DEFAULT_DURATION;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
            _audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

            // Successful
            userInput.Successful = true;

            return _playOutputFilePath;
        }
    }
}
