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
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Common.Exceptions;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        // General Actions

        public void Show()
        {
            // Create ViewModel
            MainViewModel = ViewModelHelper.CreateEmptyMainViewModel();

            // Partial Actions
            MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
            DocumentGridViewModel documentGridViewModel = _documentGridPresenter.Show(MainViewModel.DocumentGrid);
            string titleBar = _titleBarPresenter.Show();

            // DispatchViewModel
            MainViewModel.TitleBar = titleBar;
            DispatchViewModel(menuViewModel);
            DispatchViewModel(documentGridViewModel);
        }

        public void PopupMessagesOK()
        {
            MainViewModel.PopupMessages = new List<Message> { };
        }

        // AudioFileOutput

        public void AudioFileOutputGridShow()
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _audioFileOutputGridPresenter.Show(userInput));
        }

        public void AudioFileOutputGridClose()
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _audioFileOutputGridPresenter.Close(userInput));
        }

        public void AudioFileOutputCreate()
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities(rootDocument, mustGenerateName: true);

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(_repositories.AudioFileFormatRepository, _repositories.SampleDataTypeRepository);

            // DispatchViewModel
            DispatchViewModel(propertiesViewModel);

            // Refresh
            AudioFileOutputGridRefresh();
        }

        public void AudioFileOutputDelete(int id)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            _audioFileOutputManager.DeleteWithRelatedEntities(id);

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            MainViewModel.Document.AudioFileOutputPropertiesList.RemoveFirst(x => x.Entity.ID == id);

            // Refresh
            AudioFileOutputGridRefresh();
        }

        public void AudioFileOutputPropertiesShow(int id)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel userInput = DocumentViewModelHelper.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _audioFileOutputPropertiesPresenter.Show(userInput));
        }

        public void AudioFileOutputPropertiesClose()
        {
            AudioFileOutputPropertiesCloseOrLoseFocus(_audioFileOutputPropertiesPresenter.Close);
        }

        public void AudioFileOutputPropertiesLoseFocus()
        {
            AudioFileOutputPropertiesCloseOrLoseFocus(_audioFileOutputPropertiesPresenter.LoseFocus);
        }

        private void AudioFileOutputPropertiesCloseOrLoseFocus(Func<AudioFileOutputPropertiesViewModel, AudioFileOutputPropertiesViewModel> partialAction)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel userInput = DocumentViewModelHelper.GetVisibleAudioFileOutputPropertiesViewModel(MainViewModel.Document);

            // TemplateMethod
            AudioFileOutputPropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
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
            TemplateActionMethod(userInput, () => _currentPatchesPresenter.Show(userInput));
        }

        public void CurrentPatchesClose()
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _currentPatchesPresenter.Close(userInput));
        }

        public void CurrentPatchAdd(int childDocumentID)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _currentPatchesPresenter.Add(userInput, childDocumentID));
        }

        public void CurrentPatchRemove(int childDocumentID)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _currentPatchesPresenter.Remove(userInput, childDocumentID));
        }

        public void CurrentPatchMove(int childDocumentID, int newPosition)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _currentPatchesPresenter.Move(userInput, childDocumentID, newPosition));
        }

        public void CurrentPatchesPreviewAutoPatch()
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

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

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            PatchDetailsViewModel viewModel = patchManager.Patch.ToDetailsViewModel(
                _repositories.OperatorTypeRepository,
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _repositories.PatchRepository,
                _entityPositionManager);
            viewModel.Visible = true;

            // DispatchViewModel
            MainViewModel.Document.AutoPatchDetails = viewModel;
        }

        // Curve

        public void CurveGridShow(int documentID)
        {
            // GetViewModel
            CurveGridViewModel userInput = DocumentViewModelHelper.GetCurveGridViewModel_ByDocumentID(MainViewModel.Document, documentID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveGridPresenter.Show(userInput));
        }

        public void CurveGridClose()
        {
            // GetViewModel
            CurveGridViewModel userInput = DocumentViewModelHelper.GetVisibleCurveGridViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveGridPresenter.Show(userInput));
        }

        public void CurveCreate(int documentID)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Document document = _repositories.DocumentRepository.Get(documentID);

            // Business
            Curve curve = _curveManager.Create(document, mustGenerateName: true);

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // Refresh
            DocumentViewModelRefresh();
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
                MainViewModel.PopupMessages.AddRange(result.Messages);
                return;
            }

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // Refresh
            DocumentViewModelRefresh();
        }

        public void CurveDetailsShow(int curveID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveDetailsPresenter.Show(userInput));
        }

        public void CurveDetailsClose()
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveDetailsPresenter.Close(userInput));
        }

        public void CurveDetailsLoseFocus()
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveDetailsPresenter.LoseFocus(userInput));
        }

        public void CurvePropertiesShow(int curveID)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = DocumentViewModelHelper.GetCurvePropertiesViewModel(MainViewModel.Document, curveID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curvePropertiesPresenter.Show(userInput));
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
            CurvePropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // Template Method
            TemplateActionMethod(userInput, () => _documentGridPresenter.Show(userInput));
        }

        public void DocumentGridClose()
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            TemplateActionMethod(userInput, () => _documentGridPresenter.Close(userInput));
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
            // Partial Action
            object viewModel = _documentDeletePresenter.Show(id);

            // DispatchViewModel
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
            // Partial Action
            object viewModel = _documentDeletePresenter.Confirm(id);

            if (viewModel is DocumentDeletedViewModel)
            {
                _repositories.Commit();
            }

            // DispatchViewModel
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

            string titleBar = _titleBarPresenter.Show(document);
            MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: true);

            // DispatchViewModel
            MainViewModel.Document = viewModel;
            MainViewModel.TitleBar = titleBar;
            MainViewModel.Menu = menuViewModel;

            // Non-Persisted
            MainViewModel.DocumentGrid.Visible = false;

            CurrentPatchesShow();
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
                // Partial Actions
                string titleBar = _titleBarPresenter.Show();
                MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
                DocumentViewModel documentViewModel = ViewModelHelper.CreateEmptyDocumentViewModel();

                // DispatchViewModel
                MainViewModel.TitleBar = titleBar;
                MainViewModel.Menu = menuViewModel;
                MainViewModel.Document = documentViewModel;
            }
        }

        public void DocumentPropertiesShow()
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // Template Method
            TemplateActionMethod(userInput, () => _documentPropertiesPresenter.Show(userInput));
        }

        public void DocumentPropertiesClose()
        {
            DocumentPropertiesCloseOrLoseFocus(_documentPropertiesPresenter.Close);
        }

        public void DocumentPropertiesLoseFocus()
        {
            DocumentPropertiesCloseOrLoseFocus(_documentPropertiesPresenter.LoseFocus);
        }

        private void DocumentPropertiesCloseOrLoseFocus(
            Func<DocumentPropertiesViewModel, DocumentPropertiesViewModel> partialAction)
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // Template Method
            DocumentPropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                TitleBarRefresh();
                DocumentGridRefresh();
                DocumentTreeRefresh();
            }
        }

        public void DocumentTreeShow()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // Template Method
            TemplateActionMethod(userInput, () => _documentTreePresenter.Show(userInput));
        }

        public void DocumentTreeExpandNode(int nodeIndex)
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // Template Method
            TemplateActionMethod(userInput, () => _documentTreePresenter.ExpandNode(userInput, nodeIndex));
        }

        public void DocumentTreeCollapseNode(int nodeIndex)
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // Template Method
            TemplateActionMethod(userInput, () => _documentTreePresenter.CollapseNode(userInput, nodeIndex));
        }

        public void DocumentTreeClose()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // Template Method
            TemplateActionMethod(userInput, () => _documentTreePresenter.Close(userInput));
        }

        // Node

        public void NodePropertiesShow(int nodeID)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = DocumentViewModelHelper.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);

            // Template Method
            NodePropertiesViewModel viewModel = TemplateActionMethod(userInput, () => _nodePropertiesPresenter.Show(userInput));
        }

        public void SelectedNodePropertiesShow()
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            if (userInput.SelectedNodeID.HasValue)
            {
                NodePropertiesShow(userInput.SelectedNodeID.Value);
            }
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
            NodePropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            TemplateActionMethod(userInput, () => _curveDetailsPresenter.SelectNode(userInput, nodeID));
        }

        public void NodeCreate()
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Curve curve = _repositories.CurveRepository.Get(userInput.ID);

            // Business
            // TODO: This kind of stuff belongs in the business layer.
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

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // Refresh
            DocumentViewModelRefresh();
        }

        public void NodeDelete()
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // ViewModel Validation
            if (!userInput.SelectedNodeID.HasValue)
            {
                MainViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedNodeID,
                    Text = PresentationMessages.SelectANodeFirst
                });
                return;
            }

            // Business
            // TODO: Verify this in the business.
            if (userInput.Nodes.Count <= 2)
            {
                MainViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PropertyNames.Nodes,
                    // TODO: If you would just have done the ToEntity-Business-ToViewModel roundtrip, the validator would have taken care of it.
                    Text = ValidationMessageFormatter.GreaterThanOrEqual(CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Nodes), 2)
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

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // Refresh
            DocumentViewModelRefresh();
        }

        public void NodeMove(int nodeID, double time, double value)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Node node = _repositories.NodeRepository.Get(nodeID);

            // Business
            node.Time = time;
            node.Value = value;

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // Refresh
            CurveDetailsNodeRefresh(nodeID);
            NodePropertiesRefresh(nodeID);
        }

        /// <summary>
        /// Rotates between node types for the selected node.
        /// If no node is selected, nothing happens.
        /// </summary>
        public void NodeChangeNodeType()
        {
            // GetViewModel
            CurveDetailsViewModel userInput = DocumentViewModelHelper.GetVisibleCurveDetailsViewModel(MainViewModel.Document);

            // ViewModel Validation
            if (!userInput.SelectedNodeID.HasValue)
            {
                return;
            }
            int nodeID = userInput.SelectedNodeID.Value;

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Node node = _repositories.NodeRepository.Get(nodeID);

            // Business
            NodeTypeEnum nodeTypeEnum = node.GetNodeTypeEnum();
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
            node.SetNodeTypeEnum(nodeTypeEnum, _repositories.NodeTypeRepository);

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // Refresh
            CurveDetailsNodeRefresh(nodeID);
            NodePropertiesRefresh(nodeID);
        }

        // Operator

        public void OperatorPropertiesShow(int id)
        {
            // GetViewModel & Partial Action
            {
                OperatorPropertiesViewModel userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForAggregate userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForAggregate(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForAggregate.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForBundle userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForBundle(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForBundle.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCache userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForCache.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCurve userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForCurve.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCustomOperator userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForCustomOperator.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForNumber userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForNumber.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchInlet userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchOutlet userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForPatchOutlet.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForRandom userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForRandom(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForRandom.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForResample userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForResample(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForResample.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForSample userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForSample.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForSpectrum userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForSpectrum(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForSpectrum.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForUnbundle userInput = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForUnbundle(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForUnbundle.Show(userInput));
                    return;
                }
            }

            throw new Exception(String.Format("Properties ViewModel not found for Operator with ID '{0}'.", id));
        }

        public void SelectedOperatorPropertiesShow()
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetVisiblePatchDetailsViewModel(MainViewModel.Document);

            if (userInput.SelectedOperator != null &&
                userInput.SelectedOperator.ID != 0)
            {
                OperatorPropertiesShow(userInput.SelectedOperator.ID);
            }
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

        public void OperatorPropertiesClose_ForCache()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCache(_operatorPropertiesPresenter_ForCache.Close);
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

        public void OperatorPropertiesLoseFocus_ForCache()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCache(_operatorPropertiesPresenter_ForCache.LoseFocus);
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
            OperatorPropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForAggregate viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForBundle viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCache(Func<OperatorPropertiesViewModel_ForCache, OperatorPropertiesViewModel_ForCache> partialAction)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCache userInput = DocumentViewModelHelper.GetVisibleOperatorPropertiesViewModel_ForCache(MainViewModel.Document);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCache viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForCurve viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForCustomOperator viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForNumber viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForPatchInlet viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForRandom viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForResample viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForSample viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForSpectrum viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            OperatorPropertiesViewModel_ForUnbundle viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorCreate(int operatorTypeID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetVisiblePatchDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            PatchDetailsViewModel viewModel = TemplateActionMethod(userInput, () => _patchDetailsPresenter.CreateOperator(userInput, operatorTypeID));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        /// <summary> Deletes the operator selected in PatchDetails. </summary>
        public void OperatorDelete()
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetVisiblePatchDetailsViewModel(MainViewModel.Document);

            // Template Method
            PatchDetailsViewModel viewModel = TemplateActionMethod(userInput, () => _patchDetailsPresenter.DeleteOperator(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorMove(int operatorID, float centerX, float centerY)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetVisiblePatchDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _patchDetailsPresenter.MoveOperator(userInput, operatorID, centerX, centerY));
        }

        public void OperatorChangeInputOutlet(int inletID, int inputOutletID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetVisiblePatchDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _patchDetailsPresenter.ChangeInputOutlet(userInput, inletID, inputOutletID));
        }

        public void OperatorSelect(int operatorID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetVisiblePatchDetailsViewModel(MainViewModel.Document);

            // Partial Action
            TemplateActionMethod(userInput, () => _patchDetailsPresenter.SelectOperator(userInput, operatorID));
        }

        // Patch

        public void PatchDetailsShow(int childDocumentID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetPatchDetailsViewModel_ByDocumentID(MainViewModel.Document, childDocumentID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _patchDetailsPresenter.Show(userInput));

            // TODO: Change to permanent solution. (Double click on empty area in PatchDetails should show PatchProperties.)
            PatchPropertiesShow(childDocumentID);
        }

        public void PatchDetailsClose()
        {
            PatchDetailsCloseOrLoseFocus(x => _patchDetailsPresenter.Close(x));
        }

        public void PatchDetailsLoseFocus()
        {
            PatchDetailsCloseOrLoseFocus(x => _patchDetailsPresenter.LoseFocus(x));
        }

        private void PatchDetailsCloseOrLoseFocus(Func<PatchDetailsViewModel, PatchDetailsViewModel> partialAction)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetVisiblePatchDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            TemplateActionMethod(userInput, () => partialAction(userInput));
        }

        /// <summary> Returns output file path if ViewModel.Successful. <summary>
        public string PatchPlay()
        {
            // GetViewModel
            PatchDetailsViewModel userInput = DocumentViewModelHelper.GetVisiblePatchDetailsViewModel(MainViewModel.Document);

            // TemplateMethod
            string outputFilePath = null;
            TemplateActionMethod(userInput, () =>
            {
                outputFilePath = _patchDetailsPresenter.Play(userInput, _repositories);
                return userInput;
            });

            // Non-Persisted
            // Move messages to popup messages, because the default
            // dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
            MainViewModel.PopupMessages.AddRange(userInput.ValidationMessages);
            userInput.ValidationMessages.Clear();

            return outputFilePath;
        }

        public void PatchPropertiesShow(int childDocumentID)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = DocumentViewModelHelper.GetPatchPropertiesViewModel_ByChildDocumentID(MainViewModel.Document, childDocumentID);

            // Template Method
            TemplateActionMethod(userInput, () => _patchPropertiesPresenter.Show(userInput));
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
            PatchPropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void PatchGridShow(string group)
        {
            // GetViewModel
            PatchGridViewModel userInput = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

            // Template Method
            TemplateActionMethod(userInput, () => _patchGridPresenter.Show(userInput));
        }

        public void PatchGridClose()
        {
            // GetViewModel
            PatchGridViewModel userInput = DocumentViewModelHelper.GetVisiblePatchGridViewModel(MainViewModel.Document);

            // Template Method
            TemplateActionMethod(userInput, () => _patchGridPresenter.Close(userInput));
        }

        /// <param name="group">nullable</param>
        public void PatchCreate(string group)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Business
            Document childDocument = _documentManager.CreateChildDocument(rootDocument, mustGenerateName: true);
            childDocument.GroupName = group;

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // Refresh
            DocumentViewModelRefresh();
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
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(result.Messages);
                return;
            }

            // Bsuiness
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // Refresh
            DocumentViewModelRefresh();
        }

        // Sample

        public void SampleGridShow(int documentID)
        {
            // GetViewModel
            SampleGridViewModel userInput = DocumentViewModelHelper.GetSampleGridViewModel_ByDocumentID(MainViewModel.Document, documentID);

            // Template Method
            TemplateActionMethod(userInput, () => _sampleGridPresenter.Show(userInput));
        }

        public void SampleGridClose()
        {
            // GetViewModel
            SampleGridViewModel userInput = DocumentViewModelHelper.GetVisibleSampleGridViewModel(MainViewModel.Document);

            // Template Method
            TemplateActionMethod(userInput, () => _sampleGridPresenter.Close(userInput));
        }

        public void SampleCreate(int documentID)
        {
            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Document document = _repositories.DocumentRepository.Get(documentID);

            // Business
            Sample sample = _sampleManager.CreateSample(document, mustGenerateName: true);

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages.AddRange(validationResult.Messages);
                return;
            }

            // ToViewModel
            DocumentViewModelRefresh();
        }

        public void SampleDelete(int sampleID)
        {
            // GetViewModel
            SampleGridViewModel userInput = DocumentViewModelHelper.GetSampleGridViewModel_BySampleID(MainViewModel.Document, sampleID);

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Sample sample = _repositories.SampleRepository.Get(sampleID);

            // Business
            IResult result = _sampleManager.Delete(sample);
            if (!result.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages = result.Messages;
                return;
            }

            // Business
            IResult validationResult = _documentManager.ValidateRecursive(rootDocument);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                MainViewModel.PopupMessages = validationResult.Messages;
                return;
            }

            // Successful
            userInput.Successful = true;

            // Refresh
            DocumentViewModelRefresh();
        }

        public void SamplePropertiesShow(int sampleID)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = DocumentViewModelHelper.GetSamplePropertiesViewModel(MainViewModel.Document, sampleID);

            // Template Method
            TemplateActionMethod(userInput, () => _samplePropertiesPresenter.Show(userInput));
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
            SamplePropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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
            TemplateActionMethod(userInput, () => _scaleGridPresenter.Show(userInput));
        }

        public void ScaleGridClose()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            TemplateActionMethod(userInput, () => _scaleGridPresenter.Close(userInput));
        }

        public void ScaleCreate()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            ScaleGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                Document rootDocument = _repositories.DocumentRepository.Get(userInput.DocumentID);
                Scale scale = _scaleManager.Create(rootDocument, mustSetDefaults: true, mustGenerateName: true);
                userInput.Successful = true;
                return userInput;
            });

            // ToViewModel
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void ScaleDelete(int id)
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            ScaleGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                Scale scale = _repositories.ScaleRepository.Get(id);
                _scaleManager.DeleteWithRelatedEntities(id);
                userInput.Successful = true;
                return userInput;
            });

            if (viewModel.Successful)
            {
                // ToViewModel
                MainViewModel.Document.ScaleGrid.List.RemoveFirst(x => x.ID == id);
                MainViewModel.Document.ToneGridEditList.RemoveFirst(x => x.ScaleID == id);
                MainViewModel.Document.ScalePropertiesList.RemoveFirst(x => x.Entity.ID == id);

                // Successful
                userInput.Successful = true;
            }
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
            ScalePropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

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

            // Business
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

            // Bsuiness
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
            // GetViewModel
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(MainViewModel.Document);

            // Template Method
            TemplateActionMethod(userInput, () => _toneGridEditPresenter.Close(userInput));
        }

        public void ToneGridEditLoseFocus()
        {
            // GetViewModel
            ToneGridEditViewModel userInput = DocumentViewModelHelper.GetVisibleToneGridEditViewModel(MainViewModel.Document);

            // Template Method
            TemplateActionMethod(userInput, () => _toneGridEditPresenter.LoseFocus(userInput));
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
        /// A template method for a MainPresenter action method.
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
            Func<TViewModel> partialAction)
            where TViewModel : ViewModelBase
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document rootDocument = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Action
            TViewModel viewModel = partialAction();
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
