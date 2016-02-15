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
            MainViewModel = ViewModelHelper.CreateEmptyMainViewModel();

            MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
            DispatchViewModel(menuViewModel);

            DocumentGridViewModel documentGridViewModel = _documentGridPresenter.Show(MainViewModel.DocumentGrid);
            DispatchViewModel(documentGridViewModel);

            MainViewModel.WindowTitle = Titles.ApplicationName;
        }

        public void NotFoundOK()
        {
            NotFoundViewModel userInput = MainViewModel.NotFound;
            NotFoundViewModel viewModel = _notFoundPresenter.OK(userInput);
            DispatchViewModel(viewModel);
        }

        public void PopupMessagesOK()
        {
            MainViewModel.PopupMessages = new List<Message> { };
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
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities(rootDocument, mustGenerateName: true);

            AudioFileOutputListItemViewModel listItemViewModel = audioFileOutput.ToListItemViewModel();
            MainViewModel.Document.AudioFileOutputGrid.List.Add(listItemViewModel);
            MainViewModel.Document.AudioFileOutputGrid.List = MainViewModel.Document.AudioFileOutputGrid.List.OrderBy(x => x.Name).ToList();

            AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(_repositories.AudioFileFormatRepository, _repositories.SampleDataTypeRepository, _repositories.SpeakerSetupRepository);
            MainViewModel.Document.AudioFileOutputPropertiesList.Add(propertiesViewModel);
        }

        public void AudioFileOutputDelete(int id)
        {
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // 'Business' / ToViewModel
            MainViewModel.Document.AudioFileOutputPropertiesList.RemoveFirst(x => x.Entity.ID == id);
            MainViewModel.Document.AudioFileOutputGrid.List.RemoveFirst(x => x.ID == id);
        }

        public void AudioFileOutputPropertiesShow(int id)
        {
            _audioFileOutputPropertiesPresenter.ViewModel = MainViewModel.Document.AudioFileOutputPropertiesList
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
            MainViewModel.ToEntityWithRelatedEntities(_repositories);

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
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, _currentPatchesPresenter.Show);
        }

        public void CurrentPatchesClose()
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, _currentPatchesPresenter.Close);
        }

        public void CurrentPatchAdd(int childDocumentID)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, x => _currentPatchesPresenter.Add(x, childDocumentID));
        }

        public void CurrentPatchRemove(int childDocumentID)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, x => _currentPatchesPresenter.Remove(x, childDocumentID));
        }

        public void CurrentPatchMove(int childDocumentID, int newPosition)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, x => _currentPatchesPresenter.Move(x, childDocumentID, newPosition));
        }

        public void CurrentPatchesPreviewAutoPatch()
        {
            // ToEntity
            MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Get Entities
            var underlyingPatches = new List<Patch>(MainViewModel.Document.CurrentPatches.List.Count);
            foreach (CurrentPatchItemViewModel itemViewModel in MainViewModel.Document.CurrentPatches.List)
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

            // For debugging, use this code line instead to preview the polyponic auto-patch.
            //patchManager.AutoPatchPolyphonic(underlyingPatches, 2);

            // ToViewModel
            MainViewModel.Document.AutoPatchDetails = patchManager.Patch.ToDetailsViewModel(
                _repositories.OperatorTypeRepository,
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _repositories.PatchRepository,
                _entityPositionManager);

            MainViewModel.Document.AutoPatchDetails.Visible = true;
        }

        // Curve

        public void CurveGridShow(int documentID)
        {
            // GetViewModel
            CurveGridViewModel userInput = DocumentViewModelHelper.GetCurveGridViewModel_ByDocumentID(MainViewModel.Document, documentID);

            // TemplateMethod
            TemplateActionMethod(userInput, _curveGridPresenter.Show);
        }

        public void CurveGridClose()
        {
            // GetViewModel
            CurveGridViewModel userInput = DocumentViewModelHelper.GetVisibleCurveGridViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, _curveGridPresenter.Show);
        }

        public void CurveCreate(int documentID)
        {
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Document document = _repositories.DocumentRepository.TryGet(documentID);

            Curve curve = _curveManager.Create(document, mustGenerateName: true);

            // ToViewModel
            CurveGridViewModel gridViewModel = DocumentViewModelHelper.GetCurveGridViewModel_ByDocumentID(MainViewModel.Document, document.ID);
            IDAndName listItemViewModel = curve.ToIDAndName();
            gridViewModel.List.Add(listItemViewModel);
            gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

            IList<CurveDetailsViewModel> detailsViewModels = DocumentViewModelHelper.GetCurveDetailsViewModelList_ByDocumentID(MainViewModel.Document, document.ID);
            CurveDetailsViewModel curveDetailsViewModel = curve.ToDetailsViewModel(_repositories.NodeTypeRepository);
            curveDetailsViewModel.Successful = true;
            detailsViewModels.Add(curveDetailsViewModel);

            IList<CurvePropertiesViewModel> propertiesViewModels = DocumentViewModelHelper.GetCurvePropertiesViewModelList_ByDocumentID(MainViewModel.Document, document.ID);
            CurvePropertiesViewModel curvePropertiesViewModel = curve.ToPropertiesViewModel();
            curvePropertiesViewModel.Successful = true;
            propertiesViewModels.Add(curvePropertiesViewModel);

            IList<NodePropertiesViewModel> nodePropertiesViewModelList = DocumentViewModelHelper.GetNodePropertiesViewModelList_ByCurveID(MainViewModel.Document, curve.ID);
            foreach (Node node in curve.Nodes)
            {
                NodePropertiesViewModel nodePropertiesViewModel = node.ToPropertiesViewModel(_repositories.NodeTypeRepository);
                nodePropertiesViewModel.Successful = true;
                nodePropertiesViewModelList.Add(nodePropertiesViewModel);
            }

            // NOTE: Curves in a child document are only added to the curve lookup of that child document,
            // while curve in the root document are added to all child documents.
            bool isRootDocument = document.ParentDocument == null;
            if (isRootDocument)
            {
                IDAndName idAndName = curve.ToIDAndName();
                foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentList)
                {
                    patchDocumentViewModel.CurveLookup.Add(idAndName);
                    patchDocumentViewModel.CurveLookup = patchDocumentViewModel.CurveLookup.OrderBy(x => x.Name).ToList();
                }
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = DocumentViewModelHelper.GetPatchDocumentViewModel(MainViewModel.Document, documentID);
                IDAndName idAndName = curve.ToIDAndName();
                patchDocumentViewModel.CurveLookup.Add(idAndName);
                patchDocumentViewModel.CurveLookup = patchDocumentViewModel.CurveLookup.OrderBy(x => x.Name).ToList();
            }
        }

        public void CurveDelete(int curveID)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Curve curve = _repositories.CurveRepository.Get(curveID);

            // Business
            IResult result = _curveManager.DeleteWithRelatedEntities(curve);
            if (!result.Successful)
            {
                MainViewModel.Successful &= result.Successful;
                MainViewModel.PopupMessages.AddRange(result.Messages);
                return;
            }

            IResult result2 = _documentManager.ValidateRecursive(rootDocument);
            if (!result2.Successful)
            {
                MainViewModel.Successful &= result2.Successful;
                MainViewModel.PopupMessages.AddRange(result2.Messages);
                return;
            }

            // Refresh
            DocumentViewModelRefresh();
        }

        public void CurveDetailsShow(int curveID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // Partial Action
            CurveDetailsViewModel viewModel = _curveDetailsPresenter.Show(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void CurveDetailsClose()
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, _curveDetailsPresenter.Close);
        }

        public void CurveDetailsLoseFocus()
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, _curveDetailsPresenter.LoseFocus);
        }

        public void CurvePropertiesShow(int curveID)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = DocumentViewModelHelper.GetCurvePropertiesViewModel(MainViewModel.Document, curveID);

            // TemplateMethod
            TemplateActionMethod(userInput, _curvePropertiesPresenter.Show);
        }

        public void CurvePropertiesClose()
        {
            CurvePropertiesCloseOrLoseFocus(x => _curvePropertiesPresenter.Close(x));
        }

        public void CurvePropertiesLoseFocus()
        {
            CurvePropertiesCloseOrLoseFocus(x => _curvePropertiesPresenter.LoseFocus(x));
        }

        private void CurvePropertiesCloseOrLoseFocus(Func<CurvePropertiesViewModel, CurvePropertiesViewModel> partialAction)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = DocumentViewModelHelper.GetVisibleCurvePropertiesViewModel(MainViewModel.Document);

            // TemplateMethod
            CurvePropertiesViewModel viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                // Things that need to be refreshed: PatchDetails, Curve lookups, Curve Grid.
                DocumentViewModelRefresh();
            }
        }

        // Document Grid

        public void DocumentGridShow()
        {
            TemplateActionMethod(MainViewModel.DocumentGrid, _documentGridPresenter.Show);
        }

        public void DocumentGridClose()
        {
            TemplateActionMethod(MainViewModel.DocumentGrid, _documentGridPresenter.Close);
        }

        public void DocumentDetailsCreate()
        {
            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDetailsClose()
        {
            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Close(MainViewModel.DocumentDetails);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDetailsSave()
        {
            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Save(MainViewModel.DocumentDetails);

            // DispatchViewModel
            DispatchViewModel(viewModel);

            // Refresh
            DocumentGridRefresh();
        }

        public void DocumentDelete(int id)
        {
            object viewModel = _documentDeletePresenter.Show(id);
            DispatchViewModel(viewModel);
        }

        public void DocumentCannotDeleteOK()
        {
            // Partial Action
            object viewModel = _documentCannotDeletePresenter.OK(MainViewModel.DocumentCannotDelete);

            // DispatchViewModel
            DispatchViewModel(viewModel);
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
            // Partial Action
            object viewModel = _documentDeletePresenter.Cancel(MainViewModel.DocumentDelete);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDeletedOK()
        {
            // Partial Action
            DocumentDeletedViewModel viewModel = _documentDeletedPresenter.OK();

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        // Document

        public void DocumentOpen(int documentID)
        {
            Document document = _repositories.DocumentRepository.GetComplete(documentID);

            DocumentViewModel viewModel = document.ToViewModel(_repositories, _entityPositionManager);

            // Non-Persisted
            viewModel.DocumentTree.Visible = true;
            viewModel.IsOpen = true;

            // Set everything to successful.
            viewModel.AudioFileOutputGrid.Successful = true;
            viewModel.AudioFileOutputPropertiesList.ForEach(x => x.Successful = true);
            viewModel.CurrentPatches.Successful = true;
            viewModel.CurveDetailsList.ForEach(x => x.Successful = true);
            viewModel.CurveGrid.Successful = true;
            viewModel.CurvePropertiesList.ForEach(x => x.Successful = true);
            viewModel.DocumentProperties.Successful = true;
            viewModel.DocumentTree.Successful = true;
            viewModel.PatchGridList.ForEach(x => x.Successful = true);
            viewModel.NodePropertiesList.ForEach(x => x.Successful = true);
            viewModel.SampleGrid.Successful = true;
            viewModel.SamplePropertiesList.ForEach(x => x.Successful = true);
            viewModel.ScaleGrid.Successful = true;
            viewModel.ScalePropertiesList.ForEach(x => x.Successful = true);
            viewModel.ToneGridEditList.ForEach(x => x.Successful = true);
            viewModel.AutoPatchDetails.Successful = true;

            foreach (PatchDocumentViewModel patchDocumentViewModel in viewModel.PatchDocumentList)
            {
                patchDocumentViewModel.CurveDetailsList.ForEach(x => x.Successful = true);
                patchDocumentViewModel.CurveGrid.Successful = true;
                patchDocumentViewModel.CurvePropertiesList.ForEach(x => x.Successful = true);
                patchDocumentViewModel.NodePropertiesList.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForAggregates.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForBundles.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForCurves.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForNumbers.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForRandoms.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForResamples.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForSamples.ForEach(x => x.Successful = true);
                patchDocumentViewModel.OperatorPropertiesList_ForUnbundles.ForEach(x => x.Successful = true);
                patchDocumentViewModel.PatchDetails.Successful = true;
                patchDocumentViewModel.PatchProperties.Successful = true;
                patchDocumentViewModel.SampleGrid.Successful = true;
                patchDocumentViewModel.SamplePropertiesList.Select(x => x.Successful = true);
            }

            // DispatchViewModel
            MainViewModel.Document = viewModel;

            // TODO: The rest of this method is not entirely in accordance with the new patterns.

            // Here only the view models are assigned that cannot vary.
            // E.g. SampleGrid is not assigned here, because it can be different for each child document.
            _audioFileOutputGridPresenter.ViewModel = MainViewModel.Document.AudioFileOutputGrid;

            MainViewModel.WindowTitle = String.Format("{0} - {1}", document.Name, Titles.ApplicationName);

            MainViewModel.Menu = _menuPresenter.Show(documentIsOpen: true);

            CurrentPatchesShow();

            MainViewModel.DocumentGrid.Visible = false;
        }

        public void DocumentSave()
        {
            // ToEntity
            Document document = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            IResult result = _documentManager.ValidateRecursive(document);

            if (result.Successful)
            {
                _repositories.Commit();
            }

            // TODO: Delegate to the manager
            IValidator warningsValidator = new DocumentWarningValidator_Recursive(document, _repositories.SampleRepository, new HashSet<object>());

            // ToViewModel
            MainViewModel.ValidationMessages = result.Messages;
            MainViewModel.WarningMessages = warningsValidator.ValidationMessages.ToCanonical();
        }

        public void DocumentClose()
        {
            if (MainViewModel.Document.IsOpen)
            {
                MainViewModel.Document = ViewModelHelper.CreateEmptyDocumentViewModel();
                MainViewModel.WindowTitle = Titles.ApplicationName;
                MainViewModel.Menu = _menuPresenter.Show(documentIsOpen: false);
            }
        }

        public void DocumentPropertiesShow()
        {
            TemplateActionMethod(MainViewModel.Document.DocumentProperties, _documentPropertiesPresenter.Show);
        }

        public void DocumentPropertiesClose()
        {
            TemplateActionMethod(MainViewModel.Document.DocumentProperties, _documentPropertiesPresenter.Close);

            if (MainViewModel.Document.DocumentProperties.Successful)
            {
                DocumentGridRefresh();
                DocumentTreeRefresh();
            }
        }

        public void DocumentPropertiesLoseFocus()
        {
            TemplateActionMethod(MainViewModel.Document.DocumentProperties, _documentPropertiesPresenter.LoseFocus);

            if (MainViewModel.Document.DocumentProperties.Successful)
            {
                DocumentGridRefresh();
                DocumentTreeRefresh();
            }
        }

        public void DocumentTreeShow()
        {
            TemplateActionMethod(MainViewModel.Document.DocumentTree, _documentTreePresenter.Show);
        }

        public void DocumentTreeExpandNode(int nodeIndex)
        {
            TemplateActionMethod(MainViewModel.Document.DocumentTree, x => _documentTreePresenter.ExpandNode(x, nodeIndex));
        }

        public void DocumentTreeCollapseNode(int nodeIndex)
        {
            TemplateActionMethod(MainViewModel.Document.DocumentTree, x => _documentTreePresenter.CollapseNode(x, nodeIndex));
        }

        public void DocumentTreeClose()
        {
            TemplateActionMethod(MainViewModel.Document.DocumentTree, _documentTreePresenter.Close);
        }

        // Node

        public void NodePropertiesShow(int nodeID)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = DocumentViewModelHelper.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);

            // Template Method
            NodePropertiesViewModel viewModel = TemplateActionMethod(userInput, _nodePropertiesPresenter.Show);
        }

        public void NodePropertiesClose()
        {
            NodePropertiesCloseOrLoseFocus(x => _nodePropertiesPresenter.Close(x));
        }

        public void NodePropertiesLoseFocus()
        {
            NodePropertiesCloseOrLoseFocus(x => _nodePropertiesPresenter.LoseFocus(x));
        }

        public void NodePropertiesCloseOrLoseFocus(Func<NodePropertiesViewModel, NodePropertiesViewModel> partialAction)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = DocumentViewModelHelper.GetVisibleNodePropertiesViewModel(MainViewModel.Document);

            // TemplateMethod
            NodePropertiesViewModel viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                int nodeID = viewModel.Entity.ID;
                CurveDetailsNodeRefresh(nodeID);
            }
        }

        public void NodeSelect(int nodeID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, x => _curveDetailsPresenter.SelectNode(x, nodeID));
        }

        public void NodeCreate()
        {
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            Curve curve = _repositories.CurveRepository.Get(userInput.ID);
            Node afterNode;
            if (userInput.SelectedNodeID.HasValue)
            {
                afterNode = _repositories.NodeRepository.Get(userInput.SelectedNodeID.Value);
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
                MainViewModel.Successful &= validationResult.Successful;
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel

            // CurveDetails NodeViewModel
            NodeViewModel nodeViewModel = node.ToViewModel();
            userInput.Nodes.Add(nodeViewModel);

            // NodeProperties
            NodePropertiesViewModel nodePropertiesViewModel = node.ToPropertiesViewModel(_repositories.NodeTypeRepository);
            IList<NodePropertiesViewModel> propertiesViewModelList = DocumentViewModelHelper.GetNodePropertiesViewModelList_ByCurveID(MainViewModel.Document, curve.ID);
            propertiesViewModelList.Add(nodePropertiesViewModel);
        }

        public void NodeDelete()
        {
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            if (!userInput.SelectedNodeID.HasValue)
            {
                MainViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedNodeID,
                    Text = PresentationMessages.SelectANodeFirst
                });
                return;
            }

            // TODO: Verify this in the business.
            if (userInput.Nodes.Count <= 2)
            {
                MainViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PropertyNames.Nodes,
                    // TODO: If you would just have done the ToEntity-Business-ToViewModel roundtrip, the validator would have taken care of it.
                    Text = ValidationMessageFormatter.Min(CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Nodes), 2)
                });
                return;
            }

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Curve curve = _repositories.CurveRepository.Get(userInput.ID);
            Document document = curve.Document;
            int nodeID = userInput.SelectedNodeID.Value;
            Node node = _repositories.NodeRepository.Get(nodeID);

            // Business
            _curveManager.DeleteNode(node);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                MainViewModel.Successful &= validationResult.Successful;
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel

            // CurveDetails NodeViewModel
            userInput.Nodes.RemoveFirst(x => x.ID == nodeID);
            userInput.SelectedNodeID = null;

            // NodeProperties
            bool isRemoved = MainViewModel.Document.NodePropertiesList.TryRemoveFirst(x => x.Entity.ID == nodeID);
            if (!isRemoved)
            {
                foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentList)
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
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            NodeViewModel nodeViewModel = userInput.Nodes.Where(x => x.ID == nodeID).Single();
            nodeViewModel.Time = time;
            nodeViewModel.Value = value;

            NodePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);
            propertiesViewModel.Entity.Time = time;
            propertiesViewModel.Entity.Value = value;
        }

        /// <summary>
        /// Rotates between node types for the selected node.
        /// If no node is selected, nothing happens.
        /// </summary>
        public void NodeChangeNodeType()
        {
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            if (!userInput.SelectedNodeID.HasValue)
            {
                return;
            }

            // ToViewModel
            int nodeID = userInput.SelectedNodeID.Value;

            NodeViewModel nodeViewModel1 = userInput.Nodes.Where(x => x.ID == nodeID).Single();

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

            NodePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);
            propertiesViewModel.Entity.NodeType = nodeType.ToIDAndDisplayName();
        }

        // Operator

        public void OperatorPropertiesShow(int id)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            {
                OperatorPropertiesViewModel userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForAggregate userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForAggregate(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForAggregate.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForBundle userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForBundle(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForBundle.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCurve userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForCurve.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCustomOperator userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForCustomOperator.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForNumber userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForNumber.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchInlet userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForPatchInlet.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchOutlet userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForPatchOutlet.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForRandom userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForRandom(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForRandom.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForResample userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForResample(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForResample.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForSample userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForSample.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForSpectrum userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForSpectrum(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForSpectrum.Show);
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForUnbundle userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForUnbundle(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, _operatorPropertiesPresenter_ForUnbundle.Show);
                    return;
                }
            }

            throw new Exception(String.Format("Properties ViewModel not found for Operator with ID '{0}'.", id));
        }

        public void OperatorPropertiesClose()
        {
            OperatorPropertiesCloseOrLoseFocus(_operatorPropertiesPresenter.Close);
        }

        public void OperatorPropertiesClose_ForAggregate()
        {
            OperatorPropertiesCloseOrLoseFocus_ForAggregate(_operatorPropertiesPresenter_ForAggregate.Close);
        }

        public void OperatorPropertiesClose_ForBundle()
        {
            OperatorPropertiesCloseOrLoseFocus_ForBundle(_operatorPropertiesPresenter_ForBundle.Close);
        }

        public void OperatorPropertiesClose_ForCurve()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCurve(_operatorPropertiesPresenter_ForCurve.Close);
        }

        public void OperatorPropertiesClose_ForCustomOperator()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(_operatorPropertiesPresenter_ForCustomOperator.Close);
        }

        public void OperatorPropertiesClose_ForNumber()
        {
            OperatorPropertiesCloseOrLoseFocus_ForNumber(_operatorPropertiesPresenter_ForNumber.Close);
        }

        public void OperatorPropertiesClose_ForPatchInlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(_operatorPropertiesPresenter_ForPatchInlet.Close);
        }

        public void OperatorPropertiesClose_ForPatchOutlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(_operatorPropertiesPresenter_ForPatchOutlet.Close);
        }

        public void OperatorPropertiesClose_ForRandom()
        {
            OperatorPropertiesCloseOrLoseFocus_ForRandom(_operatorPropertiesPresenter_ForRandom.Close);
        }

        public void OperatorPropertiesClose_ForResample()
        {
            OperatorPropertiesCloseOrLoseFocus_ForResample(_operatorPropertiesPresenter_ForResample.Close);
        }

        public void OperatorPropertiesClose_ForSample()
        {
            OperatorPropertiesCloseOrLoseFocus_ForSample(_operatorPropertiesPresenter_ForSample.Close);
        }

        public void OperatorPropertiesClose_ForSpectrum()
        {
            OperatorPropertiesCloseOrLoseFocus_ForSpectrum(_operatorPropertiesPresenter_ForSpectrum.Close);
        }

        public void OperatorPropertiesClose_ForUnbundle()
        {
            OperatorPropertiesCloseOrLoseFocus_ForUnbundle(_operatorPropertiesPresenter_ForUnbundle.Close);
        }

        public void OperatorPropertiesLoseFocus()
        {
            OperatorPropertiesCloseOrLoseFocus(_operatorPropertiesPresenter.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForAggregate()
        {
            OperatorPropertiesCloseOrLoseFocus_ForAggregate(_operatorPropertiesPresenter_ForAggregate.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForBundle()
        {
            OperatorPropertiesCloseOrLoseFocus_ForBundle(_operatorPropertiesPresenter_ForBundle.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForCurve()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCurve(_operatorPropertiesPresenter_ForCurve.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForCustomOperator()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(_operatorPropertiesPresenter_ForCustomOperator.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForNumber()
        {
            OperatorPropertiesCloseOrLoseFocus_ForNumber(_operatorPropertiesPresenter_ForNumber.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForPatchInlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(_operatorPropertiesPresenter_ForPatchInlet.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForPatchOutlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(_operatorPropertiesPresenter_ForPatchOutlet.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForRandom()
        {
            OperatorPropertiesCloseOrLoseFocus_ForRandom(_operatorPropertiesPresenter_ForRandom.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForResample()
        {
            OperatorPropertiesCloseOrLoseFocus_ForResample(_operatorPropertiesPresenter_ForResample.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForSample()
        {
            OperatorPropertiesCloseOrLoseFocus_ForSample(_operatorPropertiesPresenter_ForSample.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForSpectrum()
        {
            OperatorPropertiesCloseOrLoseFocus_ForSpectrum(_operatorPropertiesPresenter_ForSpectrum.LoseFocus);
        }

        public void OperatorPropertiesLoseFocus_ForUnbundle()
        {
            OperatorPropertiesCloseOrLoseFocus_ForUnbundle(_operatorPropertiesPresenter_ForUnbundle.LoseFocus);
        }

        private void OperatorPropertiesCloseOrLoseFocus(Func<OperatorPropertiesViewModel, OperatorPropertiesViewModel> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForAggregate(Func<OperatorPropertiesViewModel_ForAggregate, OperatorPropertiesViewModel_ForAggregate> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForAggregate userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForAggregate(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForAggregate viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForBundle(Func<OperatorPropertiesViewModel_ForBundle, OperatorPropertiesViewModel_ForBundle> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForBundle userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForBundle(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForBundle viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCurve(Func<OperatorPropertiesViewModel_ForCurve, OperatorPropertiesViewModel_ForCurve> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCurve userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForCurve(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCurve viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(Func<OperatorPropertiesViewModel_ForCustomOperator, OperatorPropertiesViewModel_ForCustomOperator> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCustomOperator userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCustomOperator viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForNumber(Func<OperatorPropertiesViewModel_ForNumber, OperatorPropertiesViewModel_ForNumber> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForNumber userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForNumber(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForNumber viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(Func<OperatorPropertiesViewModel_ForPatchInlet, OperatorPropertiesViewModel_ForPatchInlet> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForPatchInlet userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForPatchInlet viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator); // Refresh Dependencies
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(Func<OperatorPropertiesViewModel_ForPatchOutlet, OperatorPropertiesViewModel_ForPatchOutlet> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForPatchOutlet userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator); // Refresh Dependent Things
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForRandom(Func<OperatorPropertiesViewModel_ForRandom, OperatorPropertiesViewModel_ForRandom> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForRandom userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForRandom(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForRandom viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForResample(Func<OperatorPropertiesViewModel_ForResample, OperatorPropertiesViewModel_ForResample> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForResample userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForResample(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForResample viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForSample(Func<OperatorPropertiesViewModel_ForSample, OperatorPropertiesViewModel_ForSample> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForSample userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForSample(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForSample viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForSpectrum(Func<OperatorPropertiesViewModel_ForSpectrum, OperatorPropertiesViewModel_ForSpectrum> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForSpectrum userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForSpectrum(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForSpectrum viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForUnbundle(Func<OperatorPropertiesViewModel_ForUnbundle, OperatorPropertiesViewModel_ForUnbundle> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForUnbundle userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForUnbundle(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForUnbundle viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
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
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.PatchID);

            // Business
            var patchManager = new PatchManager(patch, _patchRepositories);
            Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                MainViewModel.Successful &= validationResult.Successful;
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
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
                        IList<OperatorPropertiesViewModel_ForPatchInlet> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForPatchInlets_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.PatchOutlet:
                    {
                        OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel = op.ToPropertiesViewModel_ForPatchOutlet(_repositories.OutletTypeRepository);
                        IList<OperatorPropertiesViewModel_ForPatchOutlet> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForPatchOutlets_ByPatchID(MainViewModel.Document, patch.ID);
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
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.PatchID);

            // Business
            var patchManager = new PatchManager(patch, _patchRepositories);
            Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                MainViewModel.Successful &= validationResult.Successful;
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
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
                case OperatorTypeEnum.Average:
                case OperatorTypeEnum.Minimum:
                case OperatorTypeEnum.Maximum:
                    {
                        OperatorPropertiesViewModel_ForAggregate propertiesViewModel = op.ToPropertiesViewModel_ForAggregate();
                        IList<OperatorPropertiesViewModel_ForAggregate> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForAggregates_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.Bundle:
                    {
                        OperatorPropertiesViewModel_ForBundle propertiesViewModel = op.ToPropertiesViewModel_ForBundle();
                        IList<OperatorPropertiesViewModel_ForBundle> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForBundles_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.Curve:
                    {
                        OperatorPropertiesViewModel_ForCurve propertiesViewModel = op.ToPropertiesViewModel_ForCurve(_repositories.CurveRepository);
                        IList<OperatorPropertiesViewModel_ForCurve> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForCurves_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.CustomOperator:
                    {
                        OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel = op.ToPropertiesViewModel_ForCustomOperator(_repositories.PatchRepository);
                        IList<OperatorPropertiesViewModel_ForCustomOperator> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForCustomOperators_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.Number:
                    {
                        OperatorPropertiesViewModel_ForNumber propertiesViewModel = op.ToPropertiesViewModel_ForNumber();
                        IList<OperatorPropertiesViewModel_ForNumber> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForNumbers_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.Random:
                    {
                        OperatorPropertiesViewModel_ForRandom propertiesViewModel = op.ToPropertiesViewModel_ForRandom();
                        IList<OperatorPropertiesViewModel_ForRandom> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForRandoms_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.Resample:
                    {
                        OperatorPropertiesViewModel_ForResample propertiesViewModel = op.ToPropertiesViewModel_ForResample();
                        IList<OperatorPropertiesViewModel_ForResample> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForResamples_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.Sample:
                    {
                        OperatorPropertiesViewModel_ForSample propertiesViewModel = op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);
                        IList<OperatorPropertiesViewModel_ForSample> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForSamples_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.Spectrum:
                    {
                        OperatorPropertiesViewModel_ForSpectrum propertiesViewModel = op.ToPropertiesViewModel_ForSpectrum();
                        IList<OperatorPropertiesViewModel_ForSpectrum> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForSpectrums_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                case OperatorTypeEnum.Unbundle:
                    {
                        OperatorPropertiesViewModel_ForUnbundle propertiesViewModel = op.ToPropertiesViewModel_ForUnbundle();
                        IList<OperatorPropertiesViewModel_ForUnbundle> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForUnbundles_ByPatchID(MainViewModel.Document, patch.ID);
                        propertiesViewModelList.Add(propertiesViewModel);
                        break;
                    }

                default:
                    {
                        OperatorPropertiesViewModel propertiesViewModel = op.ToPropertiesViewModel();
                        IList<OperatorPropertiesViewModel> propertiesViewModelList = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ByPatchID(MainViewModel.Document, patch.ID);
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
                Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
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
                    MainViewModel.Successful &= validationResult.Successful;
                    MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                    return;
                }

                // Partial Action
                _patchDetailsPresenter.DeleteOperator();

                // ToViewModel
                if (_patchDetailsPresenter.ViewModel.Successful)
                {
                    // Do a lot of if'ing and switching to be a little faster in removing the item a specific place in the view model.
                    PatchDocumentViewModel patchDocumentViewModel = MainViewModel.Document.PatchDocumentList.Where(x => x.ChildDocumentID == document.ID).First();
                    switch (operatorTypeEnum)
                    {
                        case OperatorTypeEnum.Average:
                        case OperatorTypeEnum.Minimum:
                        case OperatorTypeEnum.Maximum:
                            patchDocumentViewModel.OperatorPropertiesList_ForAggregates.RemoveFirst(x => x.ID == op.ID);
                            break;

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

                        case OperatorTypeEnum.Random:
                            patchDocumentViewModel.OperatorPropertiesList_ForRandoms.RemoveFirst(x => x.ID == op.ID);
                            break;

                        case OperatorTypeEnum.Resample:
                            patchDocumentViewModel.OperatorPropertiesList_ForResamples.RemoveFirst(x => x.ID == op.ID);
                            break;

                        case OperatorTypeEnum.Sample:
                            patchDocumentViewModel.OperatorPropertiesList_ForSamples.RemoveFirst(x => x.ID == op.ID);
                            break;

                        case OperatorTypeEnum.Spectrum:
                            patchDocumentViewModel.OperatorPropertiesList_ForSpectrums.RemoveFirst(x => x.ID == op.ID);
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
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            _patchDetailsPresenter.MoveOperator(operatorID, centerX, centerY);

            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        public void OperatorChangeInputOutlet(int inletID, int inputOutletID)
        {
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            _patchDetailsPresenter.ChangeInputOutlet(inletID, inputOutletID);

            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        public void OperatorSelect(int operatorID)
        {
            // HACK: If SelectOperator is too slow I cannot double click it.
            //Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            _patchDetailsPresenter.SelectOperator(operatorID);

            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        // Patch

        public void PatchDetailsShow(int childDocumentID)
        {
            PatchDetailsViewModel detailsViewModel = DocumentViewModelHelper.GetPatchDetailsViewModel_ByDocumentID(MainViewModel.Document, childDocumentID);
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
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            partialAction();

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                MainViewModel.Successful &= validationResult.Successful;
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            DispatchViewModel(_patchDetailsPresenter.ViewModel);
        }

        /// <summary> Returns output file path if ViewModel.Successful. <summary>
        public string PatchPlay()
        {
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            string outputFilePath = _patchDetailsPresenter.Play(_repositories);

            DispatchViewModel(_patchDetailsPresenter.ViewModel);

            // Move messages to popup messages, because the default dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
            MainViewModel.PopupMessages.AddRange(_patchDetailsPresenter.ViewModel.ValidationMessages);
            _patchDetailsPresenter.ViewModel.ValidationMessages.Clear();

            MainViewModel.Successful = _patchDetailsPresenter.ViewModel.Successful;

            DispatchViewModel(_patchDetailsPresenter.ViewModel);

            return outputFilePath;
        }

        public void PatchPropertiesShow(int childDocumentID)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = DocumentViewModelHelper.GetPatchPropertiesViewModel_ByChildDocumentID(MainViewModel.Document, childDocumentID);

            // Template Method
            TemplateActionMethod(userInput, _patchPropertiesPresenter.Show);
        }

        public void PatchPropertiesClose()
        {
            PatchPropertiesCloseOrLoseFocus(_patchPropertiesPresenter.Close);
        }

        public void PatchPropertiesLoseFocus()
        {
            PatchPropertiesCloseOrLoseFocus(_patchPropertiesPresenter.LoseFocus);
        }

        private void PatchPropertiesCloseOrLoseFocus(Func<PatchPropertiesViewModel, PatchPropertiesViewModel> partialAction)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = DocumentViewModelHelper.GetVisiblePatchPropertiesViewModel(MainViewModel.Document);

            // Template Method
            PatchPropertiesViewModel viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                DocumentTreeRefresh();
                CurrentPatchesRefresh();
                PatchGridsRefresh(); // Refresh all patch grids, because a Patch's group can change.
                UnderylingPatchLookupRefresh();
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator);
                OperatorProperties_ForCustomOperatorViewModels_Refresh(underlyingPatchID: userInput.ChildDocumentID);
            }
        }

        public void PatchGridShow(string group)
        {
            // GetViewModel
            PatchGridViewModel userInput = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

            // Template Method
            TemplateActionMethod(userInput, _patchGridPresenter.Show);
        }

        public void PatchGridClose()
        {
            // GetViewModel
            PatchGridViewModel userInput = DocumentViewModelHelper.GetVisiblePatchGridViewModel(MainViewModel.Document);

            // Template Method
            TemplateActionMethod(userInput, _patchGridPresenter.Close);
        }

        /// <param name="group">nullable</param>
        public void PatchCreate(string group)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            Document childDocument = _documentManager.CreateChildDocument(rootDocument, mustGenerateName: true);
            childDocument.GroupName = group;

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                MainViewModel.Successful &= validationResult.Successful;
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            ChildDocumentIDAndNameViewModel listItemViewModel = childDocument.ToChildDocumentIDAndNameViewModel();
            PatchGridViewModel gridViewModel = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);
            gridViewModel.List.Add(listItemViewModel);
            gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

            PatchDocumentViewModel documentViewModel = childDocument.ToPatchDocumentViewModel(_repositories, _entityPositionManager);
            MainViewModel.Document.PatchDocumentList.Add(documentViewModel);

            ChildDocumentIDAndNameViewModel lookupItemViewModel = childDocument.ToChildDocumentIDAndNameViewModel();
            MainViewModel.Document.UnderlyingPatchLookup.Add(lookupItemViewModel);
            MainViewModel.Document.UnderlyingPatchLookup = MainViewModel.Document.UnderlyingPatchLookup.OrderBy(x => x.Name).ToList();

            DocumentTreeRefresh();
        }

        public void PatchDelete(int childDocumentID)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Document childDocument = _repositories.DocumentRepository.Get(childDocumentID);

            // Businesss
            IResult result = _documentManager.DeleteWithRelatedEntities(childDocument);
            if (!result.Successful)
            {
                // ToViewModel
                MainViewModel.PopupMessages.AddRange(result.Messages);
                return;
            }

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                MainViewModel.Successful &= validationResult.Successful;
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            MainViewModel.Document.PatchDocumentList.RemoveFirst(x => x.ChildDocumentID == childDocumentID);
            MainViewModel.Document.CurrentPatches.List.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
            MainViewModel.Document.UnderlyingPatchLookup.RemoveFirst(x => x.ChildDocumentID == childDocumentID);
            MainViewModel.Document.DocumentTree.PatchesNode.PatchNodes.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
            foreach (PatchGroupTreeNodeViewModel nodeViewModel in MainViewModel.Document.DocumentTree.PatchesNode.PatchGroupNodes)
            {
                nodeViewModel.Patches.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
            }
            foreach (PatchGridViewModel gridViewModel in MainViewModel.Document.PatchGridList)
            {
                gridViewModel.List.TryRemoveFirst(x => x.ChildDocumentID == childDocumentID);
            }
        }

        // Sample

        public void SampleGridShow(int documentID)
        {
            // GetViewModel
            SampleGridViewModel userInput = DocumentViewModelHelper.GetSampleGridViewModel_ByDocumentID(MainViewModel.Document, documentID);

            // Template Method
            TemplateActionMethod(userInput, _sampleGridPresenter.Show);
        }

        public void SampleGridClose()
        {
            // GetViewModel
            SampleGridViewModel userInput = DocumentViewModelHelper.GetVisibleSampleGridViewModel(MainViewModel.Document);

            // Template Method
            TemplateActionMethod(userInput, _sampleGridPresenter.Close);
        }

        public void SampleCreate(int documentID)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Document document = _repositories.DocumentRepository.Get(documentID);

            // Business
            Sample sample = _sampleManager.CreateSample(document, mustGenerateName: true);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                MainViewModel.Successful &= validationResult.Successful;
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            SampleGridViewModel gridViewModel = DocumentViewModelHelper.GetSampleGridViewModel_ByDocumentID(MainViewModel.Document, document.ID);
            SampleListItemViewModel listItemViewModel = sample.ToListItemViewModel();
            gridViewModel.List.Add(listItemViewModel);
            gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

            IList<SamplePropertiesViewModel> propertiesViewModels = DocumentViewModelHelper.GetSamplePropertiesViewModels_ByDocumentID(MainViewModel.Document, document.ID);
            SamplePropertiesViewModel propertiesViewModel = sample.ToPropertiesViewModel(_sampleRepositories);
            propertiesViewModels.Add(propertiesViewModel);

            // NOTE: Samples in a child document are only added to the sample lookup of that child document,
            // while sample in the root document are added to all child documents.
            bool isRootDocument = document.ParentDocument == null;
            if (isRootDocument)
            {
                IDAndName idAndName = sample.ToIDAndName();
                foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentList)
                {
                    patchDocumentViewModel.SampleLookup.Add(idAndName);
                    patchDocumentViewModel.SampleLookup = patchDocumentViewModel.SampleLookup.OrderBy(x => x.Name).ToList();
                }
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = DocumentViewModelHelper.GetPatchDocumentViewModel(MainViewModel.Document, documentID);
                IDAndName idAndName = sample.ToIDAndName();
                patchDocumentViewModel.SampleLookup.Add(idAndName);
                patchDocumentViewModel.SampleLookup = patchDocumentViewModel.SampleLookup.OrderBy(x => x.Name).ToList();
            }
        }

        // 2016-01-26 WAS HERE REFACTORING, AND SampleDelete IS NOT FINISHED YET.

        public void SampleDelete(int sampleID)
        {
            // GetViewModel
            SampleGridViewModel userInput = DocumentViewModelHelper.GetSampleGridViewModel_BySampleID(MainViewModel.Document, sampleID);

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Sample sample = _repositories.SampleRepository.Get(sampleID);
            int documentID = sample.Document.ID;
            bool isRootDocument = sample.Document.ParentDocument == null;

            // Business
            IResult result = _sampleManager.Delete(sample);
            if (!result.Successful)
            {
                // ToViewModel
                MainViewModel.PopupMessages = result.Messages;
                return;
            }

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                MainViewModel.Successful = validationResult.Successful;
                MainViewModel.PopupMessages = validationResult.Messages;
                return;
            }

            // Set Successful
            userInput.Successful = true;

            // ToViewModel
            IList<SamplePropertiesViewModel> propertiesViewModels = DocumentViewModelHelper.GetSamplePropertiesViewModels_ByDocumentID(MainViewModel.Document, documentID);
            propertiesViewModels.RemoveFirst(x => x.Entity.ID == sampleID);

            SampleGridViewModel gridViewModel = DocumentViewModelHelper.GetSampleGridViewModel_ByDocumentID(MainViewModel.Document, documentID);
            gridViewModel.List.RemoveFirst(x => x.ID == sampleID);

            MainViewModel.Document.PatchDocumentList.ForEach(x => x.SampleLookup.TryRemoveFirst(y => y.ID == sampleID));
        }

        public void SamplePropertiesShow(int sampleID)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = DocumentViewModelHelper.GetSamplePropertiesViewModel(MainViewModel.Document, sampleID);

            // Template Method
            TemplateActionMethod(userInput, _samplePropertiesPresenter.Show);
        }

        public void SamplePropertiesClose()
        {
            SamplePropertiesCloseOrLoseFocus(_samplePropertiesPresenter.Close);
        }

        public void SamplePropertiesLoseFocus()
        {
            SamplePropertiesCloseOrLoseFocus(_samplePropertiesPresenter.LoseFocus);
        }

        private void SamplePropertiesCloseOrLoseFocus(Func<SamplePropertiesViewModel, SamplePropertiesViewModel> partialAction)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = DocumentViewModelHelper.GetVisibleSamplePropertiesViewModel(MainViewModel.Document);

            // TemplateMethod
            SamplePropertiesViewModel viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                int sampleID = userInput.Entity.ID;
                SampleGridRefresh(sampleID);
                SampleLookupsRefresh(sampleID);
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.Sample);
            }
        }

        // Scale

        public void ScaleGridShow()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            TemplateActionMethod(userInput, _scaleGridPresenter.Show);
        }

        public void ScaleGridClose()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            TemplateActionMethod(userInput, _scaleGridPresenter.Close);
        }

        public void ScaleCreate()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            Scale scale = _scaleManager.Create(rootDocument, mustSetDefaults: true, mustGenerateName: true);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                userInput.ValidationMessages = validationResult.Messages;
                DispatchViewModel(userInput);
                return;
            }

            // ToViewModel
            IDAndName listItemViewModel = scale.ToIDAndName();
            MainViewModel.Document.ScaleGrid.List.Add(listItemViewModel);
            MainViewModel.Document.ScaleGrid.List = MainViewModel.Document.ScaleGrid.List.OrderBy(x => x.Name).ToList();

            ToneGridEditViewModel toneGridEditViewModel = scale.ToToneGridEditViewModel();
            MainViewModel.Document.ToneGridEditList.Add(toneGridEditViewModel);
            
            ScalePropertiesViewModel scalePropertiesViewModel = scale.ToPropertiesViewModel(_repositories.ScaleTypeRepository);
            MainViewModel.Document.ScalePropertiesList.Add(scalePropertiesViewModel);

            // Set Successful
            userInput.Successful = true;
            scalePropertiesViewModel.Successful = true;
            toneGridEditViewModel.Successful = true;
        }

        public void ScaleDelete(int id)
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Scale scale = _repositories.ScaleRepository.Get(id);

            // Business
            _scaleManager.DeleteWithRelatedEntities(id);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                userInput.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(userInput);
                return;
            }

            // ToViewModel
            MainViewModel.Document.ScaleGrid.List.RemoveFirst(x => x.ID == id);
            MainViewModel.Document.ToneGridEditList.RemoveFirst(x => x.ScaleID == id);
            MainViewModel.Document.ScalePropertiesList.RemoveFirst(x => x.Entity.ID == id);

            // Successful
            userInput.Successful = true;
        }

        public void ScaleShow(int id)
        {
            // GetViewModel
            ScalePropertiesViewModel userInput1 = DocumentViewModelHelper.GetScalePropertiesViewModel(MainViewModel.Document, id);
            ToneGridEditViewModel userInput2 = DocumentViewModelHelper.GetToneGridEditViewModel(MainViewModel.Document, scaleID: id);

            // Set !Successful
            userInput1.Successful = false;
            userInput2.Successful = false;

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Actions
            ScalePropertiesViewModel scalePropertiesViewModel = _scalePropertiesPresenter.Show(userInput1);
            if (!scalePropertiesViewModel.Successful)
            {
                DispatchViewModel(scalePropertiesViewModel);
                return;
            }
            scalePropertiesViewModel.Successful = false;

            ToneGridEditViewModel toneGridEditViewModel = _toneGridEditPresenter.Show(userInput2);
            if (!toneGridEditViewModel.Successful)
            {
                DispatchViewModel(toneGridEditViewModel);
                return;
            }
            toneGridEditViewModel.Successful = false;

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                scalePropertiesViewModel.ValidationMessages.AddRange(validationResult.Messages);
                DispatchViewModel(scalePropertiesViewModel);
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
            ScalePropertiesCloseOrLoseFocus(_scalePropertiesPresenter.Close);
        }

        public void ScalePropertiesLoseFocus()
        {
            ScalePropertiesCloseOrLoseFocus(_scalePropertiesPresenter.LoseFocus);
        }

        private void ScalePropertiesCloseOrLoseFocus(Func<ScalePropertiesViewModel, ScalePropertiesViewModel> partialAction)
        {
            // Get ViewModel
            ScalePropertiesViewModel userInput = DocumentViewModelHelper.GetVisibleScalePropertiesViewModel(MainViewModel.Document);

            // Template Method
            ScalePropertiesViewModel viewModel = TemplateActionMethod(userInput, partialAction);

            // Refresh
            if (viewModel.Successful)
            {
                ToneGridEditRefresh(userInput.Entity.ID);
                ScaleGridRefresh();
            }
        }

        // Tone

        public void ToneCreate(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(MainViewModel.Document);

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
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Scale scale = _repositories.ScaleRepository.Get(scaleID);

            // Business
            Tone tone = _scaleManager.CreateTone(scale);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
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
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(MainViewModel.Document);

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
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Tone tone = _repositories.ToneRepository.Get(id);
            Scale scale = tone.Scale;

            // Business
            _scaleManager.DeleteTone(tone);

            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
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
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(MainViewModel.Document);
            TemplateActionMethod(userInput, _toneGridEditPresenter.Close);
        }

        public void ToneGridEditLoseFocus()
        {
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(MainViewModel.Document);
            TemplateActionMethod(userInput, _toneGridEditPresenter.LoseFocus);
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

            // GetEntity
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(MainViewModel.Document);

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validator
            IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
            if (!viewModelValidator.IsValid)
            {
                userInput.ValidationMessages = viewModelValidator.ValidationMessages.ToCanonical();
                DispatchViewModel(userInput);
                return null;
            }

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Tone tone = _repositories.ToneRepository.Get(id);

            var underlyingPatches = new List<Patch>(MainViewModel.Document.CurrentPatches.List.Count);
            foreach (CurrentPatchItemViewModel itemViewModel in MainViewModel.Document.CurrentPatches.List)
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

        // Helpers

        /// <summary>
        /// A template method for an MainPresenter action method.
        /// Works for most actions. Less suitable for specialized cases.
        /// 
        /// Executes a sub-presenter's action and surrounds it with
        /// converting the full document view model to entity,
        /// doing a full document validation and 
        /// managing view model transactionality.
        /// All you need to do is provide the right sub-viewmodel,
        /// provide a delegate to the sub-presenter's action method
        /// and possibly do some refreshing of other view models afterwards.
        /// </summary>
        private TViewModel TemplateActionMethod<TViewModel>(
            TViewModel userInput,
            Func<TViewModel, TViewModel> partialAction)
            where TViewModel : ViewModelBase
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            TViewModel viewModel = partialAction(userInput);
            if (!viewModel.Successful)
            {
                // DispatchViewModel
                DispatchViewModel(viewModel);
                return viewModel;
            }

            // Set !Successful
            viewModel.Successful = false;

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                viewModel.ValidationMessages.AddRange(validationResult.Messages);

                // DispatchViewModel
                DispatchViewModel(viewModel);
                return viewModel;
            }

            // Successful
            viewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel);

            return viewModel;
        }
    }
}
