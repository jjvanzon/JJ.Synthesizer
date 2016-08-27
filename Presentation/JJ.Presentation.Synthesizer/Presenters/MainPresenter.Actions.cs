using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Api;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

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
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Template Method
            AudioFileOutput audioFileOutput = null;
            AudioFileOutputGridViewModel gridViewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

                // Business
                audioFileOutput = _audioFileOutputManager.Create(document, mustGenerateName: true);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            if (gridViewModel.Successful)
            {
                // ToViewModel
                AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel();

                // DispatchViewModel
                DispatchViewModel(propertiesViewModel);

                // Refresh
                DocumentTreeRefresh();
                AudioFileOutputGridRefresh();
            }
        }

        public void AudioFileOutputDelete(int id)
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Template Method
            AudioFileOutputGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // Business
                _audioFileOutputManager.Delete(id);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            if (viewModel.Successful)
            {
                // ToViewModel
                MainViewModel.Document.AudioFileOutputPropertiesDictionary.Remove(id);

                // Refresh
                DocumentTreeRefresh();
                AudioFileOutputGridRefresh();
            }
        }

        public void AudioFileOutputPropertiesShow(int id)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _audioFileOutputPropertiesPresenter.Show(userInput));
        }

        public void AudioFileOutputPropertiesClose(int id)
        {
            AudioFileOutputPropertiesCloseOrLoseFocus(_audioFileOutputPropertiesPresenter.Close, id);
        }

        public void AudioFileOutputPropertiesLoseFocus(int id)
        {
            AudioFileOutputPropertiesCloseOrLoseFocus(_audioFileOutputPropertiesPresenter.LoseFocus, id);
        }

        private void AudioFileOutputPropertiesCloseOrLoseFocus(Func<AudioFileOutputPropertiesViewModel, AudioFileOutputPropertiesViewModel> partialAction, int id)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            AudioFileOutputPropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                AudioFileOutputGridRefresh();
            }
        }

        // AudioOutput

        public void AudioOutputPropertiesShow()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _audioOutputPropertiesPresenter.Show(userInput));
        }

        public void AudioOutputPropertiesClose()
        {
            AudioOutputPropertiesCloseOrLoseFocus(_audioOutputPropertiesPresenter.Close);
        }

        public void AudioOutputPropertiesLoseFocus()
        {
            AudioOutputPropertiesCloseOrLoseFocus(_audioOutputPropertiesPresenter.LoseFocus);
        }

        private void AudioOutputPropertiesCloseOrLoseFocus(Func<AudioOutputPropertiesViewModel, AudioOutputPropertiesViewModel> partialAction)
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            AudioOutputPropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));
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

        public void CurrentPatchAdd(int patchID)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _currentPatchesPresenter.Add(userInput, patchID));
        }

        public void CurrentPatchRemove(int patchID)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _currentPatchesPresenter.Remove(userInput, patchID));
        }

        public void CurrentPatchMove(int patchID, int newPosition)
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _currentPatchesPresenter.Move(userInput, patchID, newPosition));
        }

        public void CurrentPatchesShowAutoPatchPolyphonic()
        {
            // NOTE: Almost a copy of CurrentPatchesShowAutoPatch, except for the business method call.

            // GetViewModel
            CurrentPatchesViewModel currentPatchesViewModel = MainViewModel.Document.CurrentPatches;

            // RefreshCounter
            currentPatchesViewModel.RefreshCounter++;

            // Set !Successful
            currentPatchesViewModel.Successful = false;

            // ToEntity
            Document document = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Get Entities
            IList<Patch> underlyingPatches = currentPatchesViewModel.ToEntities(_repositories.PatchRepository);

            // Business
            var patchManager = new PatchManager(_patchRepositories);
            patchManager.AutoPatchPolyphonic(underlyingPatches, 2);

            // Business
            IResult validationResult = _documentManager.Save(document);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                currentPatchesViewModel.ValidationMessages.AddRange(validationResult.Messages);

                // DispatchViewModel
                DispatchViewModel(currentPatchesViewModel);

                return;
            }

            // ToViewModel
            PatchDetailsViewModel detailsViewModel = patchManager.Patch.ToDetailsViewModel(
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _repositories.PatchRepository,
                _entityPositionManager);

            // Non-Persisted
            detailsViewModel.Visible = true;

            // Successful
            currentPatchesViewModel.Successful = true;
            detailsViewModel.Successful = true;

            // DispatchViewModel
            DispatchAutoPatchDetailsViewModel(detailsViewModel);
        }

        public void CurrentPatchesShowAutoPatch()
        {
            // NOTE: Almost a copy of CurrentPatchesShowAutoPatchPolyphonic, except for the business method call.

            // GetViewModel
            CurrentPatchesViewModel currentPatchesViewModel = MainViewModel.Document.CurrentPatches;

            // RefreshCounter
            currentPatchesViewModel.RefreshCounter++;

            // Set !Successful
            currentPatchesViewModel.Successful = false;

            // ToEntity
            Document document = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Get Entities
            IList<Patch> underlyingPatches = currentPatchesViewModel.ToEntities(_repositories.PatchRepository);

            // Business
            var patchManager = new PatchManager(_patchRepositories);
            patchManager.AutoPatch(underlyingPatches);

            // Business
            IResult validationResult = _documentManager.Save(document);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                currentPatchesViewModel.ValidationMessages.AddRange(validationResult.Messages);

                // DispatchViewModel
                DispatchViewModel(currentPatchesViewModel);

                return;
            }

            // ToViewModel
            PatchDetailsViewModel detailsViewModel = patchManager.Patch.ToDetailsViewModel(
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _repositories.PatchRepository,
                _entityPositionManager);

            // Non-Persisted
            detailsViewModel.Visible = true;

            // Successful
            currentPatchesViewModel.Successful = true;
            detailsViewModel.Successful = true;

            // DispatchViewModel
            DispatchAutoPatchDetailsViewModel(detailsViewModel);
        }

        public void AutoPatchDetailsClose()
        {
            MainViewModel.Document.AutoPatchDetails.Visible = false;
        }

        // Curve

        public void CurveGridShow()
        {
            // GetViewModel
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveGridPresenter.Show(userInput));
        }

        public void CurveGridClose()
        {
            // GetViewModel
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveGridPresenter.Close(userInput));
        }

        public void CurveCreate()
        {
            // GetViewModel
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;

            // Template Method
            CurveGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

                // Business
                Curve curve = _curveManager.Create(document, mustGenerateName: true);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void CurveDelete(int curveID)
        {
            // GetViewModel
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;

            // Template Method
            CurveGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Curve curve = _repositories.CurveRepository.Get(curveID);

                // Business
                IResult result = _curveManager.DeleteWithRelatedEntities(curve);

                // Non-Persisted
                userInput.ValidationMessages.AddRange(result.Messages);

                // Successful?
                userInput.Successful = result.Successful;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void CurveDetailsShow(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveDetailsPresenter.Show(userInput));
        }

        public void CurveDetailsClose(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveDetailsPresenter.Close(userInput));
        }

        public void CurveDetailsLoseFocus(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveDetailsPresenter.LoseFocus(userInput));
        }

        public void CurvePropertiesShow(int id)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = ViewModelSelector.GetCurvePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curvePropertiesPresenter.Show(userInput));
        }

        public void CurvePropertiesClose(int id)
        {
            CurvePropertiesCloseOrLoseFocus(_curvePropertiesPresenter.Close, id);
        }

        public void CurvePropertiesLoseFocus(int id)
        {
            CurvePropertiesCloseOrLoseFocus(_curvePropertiesPresenter.LoseFocus, id);
        }

        private void CurvePropertiesCloseOrLoseFocus(Func<CurvePropertiesViewModel, CurvePropertiesViewModel> partialAction, int id)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = ViewModelSelector.GetCurvePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            CurvePropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh(); // Things that need to be refreshed: PatchDetails, Curve lookups, Curve Grid.
            }
        }

        // Document Grid

        public void DocumentGridShow()
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // Partial Action
            DocumentGridViewModel viewModel = _documentGridPresenter.Show(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentGridClose()
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // Partial Action
            DocumentGridViewModel viewModel = _documentGridPresenter.Close(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDetailsCreate()
        {
            // GetViewModel
            DocumentGridViewModel gridViewModel = MainViewModel.DocumentGrid;

            // RefreshCounter
            gridViewModel.RefreshCounter++;

            // Set !Successful
            gridViewModel.Successful = false;

            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();

            // Successful
            viewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDetailsClose()
        {
            // GetViewModel
            DocumentDetailsViewModel userInput = MainViewModel.DocumentDetails;

            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Close(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDetailsSave()
        {
            // GetViewModel
            DocumentDetailsViewModel userInput = MainViewModel.DocumentDetails;

            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Save(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);

            // Refresh
            if (viewModel.Successful)
            {
                DocumentGridRefresh();
            }
        }

        public void DocumentDeleteShow(int id)
        {
            // GetViewModel
            DocumentGridViewModel gridViewModel = MainViewModel.DocumentGrid;

            // RefreshCounter
            gridViewModel.RefreshCounter++;

            // Set !Successful
            gridViewModel.Successful = false;

            // Partial Action
            object viewModel2 = _documentDeletePresenter.Show(id);

            // Successful
            gridViewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel2);
        }

        public void DocumentCannotDeleteOK()
        {
            // GetViewModel
            DocumentCannotDeleteViewModel userInput = MainViewModel.DocumentCannotDelete;

            // Partial Action
            DocumentCannotDeleteViewModel viewModel = _documentCannotDeletePresenter.OK(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDeleteConfirm(int id)
        {
            // GetViewModel
            DocumentDeleteViewModel viewModel = MainViewModel.DocumentDelete;

            // Partial Action
            object viewModel2 = _documentDeletePresenter.Confirm(viewModel);

            // RefreshCounter
            viewModel.RefreshCounter++;

            // Set !Successful
            viewModel.Successful = false;

            if (viewModel2 is DocumentDeletedViewModel)
            {
                _repositories.Commit();
            }

            // Successful
            viewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel2);
        }

        public void DocumentDeleteCancel()
        {
            // GetViewModel
            DocumentDeleteViewModel userInput = MainViewModel.DocumentDelete;

            // Partial Action
            DocumentDeleteViewModel viewModel = _documentDeletePresenter.Cancel(MainViewModel.DocumentDelete);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDeletedOK()
        {
            // GetViewModel
            DocumentDeletedViewModel userInput = MainViewModel.DocumentDeleted;

            // Partial Action
            DocumentDeletedViewModel viewModel = _documentDeletedPresenter.OK(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        // Document

        public void DocumentOpen(int id)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.GetComplete(id);

            // Business
            var patchManager = new PatchManager(_patchRepositories);
            IList<Patch> grouplessPatches = patchManager.GetGrouplessPatches(document.Patches);
            IList<PatchGroupDto> patchGroupDtos = patchManager.GetPatchGroupDtos(document.Patches);
            IList<UsedInDto> curveUsedInDtos = document.Curves
                                                            .Select(x => new UsedInDto
                                                            {
                                                                EntityIDAndName = x.ToIDAndName(),
                                                                UsedInIDAndNames = _documentManager.GetUsedIn(x)
                                                            })
                                                            .ToArray();

            // ToViewModel
            DocumentViewModel viewModel = document.ToViewModel(
                grouplessPatches,
                patchGroupDtos,
                curveUsedInDtos,
                _repositories,
                _entityPositionManager);

            // Non-Persisted
            viewModel.DocumentTree.Visible = true;
            viewModel.IsOpen = true;

            // Set everything to successful.
            viewModel.AudioFileOutputGrid.Successful = true;
            viewModel.AudioFileOutputPropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.AudioOutputProperties.Successful = true;
            viewModel.AutoPatchDetails.Successful = true;
            viewModel.CurrentPatches.Successful = true;
            viewModel.CurveDetailsDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.CurveGrid.Successful = true;
            viewModel.CurvePropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.DocumentProperties.Successful = true;
            viewModel.DocumentTree.Successful = true;
            viewModel.NodePropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForBundles.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForCaches.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForCurves.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForCustomOperators.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForMakeContinuous.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForNumbers.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForPatchInlets.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForPatchOutlets.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForSamples.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_WithDimension.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_WithDimensionAndInterpolation.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_WithDimensionAndOutletCount.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_WithInletCount.Values.ForEach(x => x.Successful = true);
            viewModel.PatchDetailsDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.PatchGridDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.PatchPropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.SampleGrid.Successful = true;
            viewModel.SamplePropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.SamplePropertiesDictionary.Values.Select(x => x.Successful = true);
            viewModel.ScaleGrid.Successful = true;
            viewModel.ScalePropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.ToneGridEditDictionary.Values.ForEach(x => x.Successful = true);

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
            IResult validationResult = _documentManager.Save(document);
            IResult warningsResult = _documentManager.GetWarningsRecursive(document);

            // Commit
            if (validationResult.Successful)
            {
                _repositories.Commit();
            }

            // ToViewModel
            MainViewModel.ValidationMessages = validationResult.Messages;
            MainViewModel.WarningMessages = warningsResult.Messages;
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

        private void DocumentPropertiesCloseOrLoseFocus(Func<DocumentPropertiesViewModel, DocumentPropertiesViewModel> partialAction)
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

        public void DocumentTreeClose()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // Template Method
            TemplateActionMethod(userInput, () => _documentTreePresenter.Close(userInput));
        }

        // Node

        public void NodePropertiesShow(int id)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            TemplateActionMethod(userInput, () => _nodePropertiesPresenter.Show(userInput));
        }

        public void NodePropertiesClose(int id)
        {
            NodePropertiesCloseOrLoseFocus(_nodePropertiesPresenter.Close, id);
        }

        public void NodePropertiesLoseFocus(int id)
        {
            NodePropertiesCloseOrLoseFocus(_nodePropertiesPresenter.LoseFocus, id);
        }

        public void NodePropertiesCloseOrLoseFocus(Func<NodePropertiesViewModel, NodePropertiesViewModel> partialAction, int id)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            NodePropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                Node node = _repositories.NodeRepository.Get(id);
                CurveDetailsNodeRefresh(node.Curve.ID, id);
            }
        }

        public void NodeSelect(int curveID, int nodeID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _curveDetailsPresenter.SelectNode(userInput, nodeID));
        }

        public void NodeCreate(int curveID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // Template Method
            CurveDetailsViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Curve curve = _repositories.CurveRepository.Get(userInput.CurveID);

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
                    afterNode = curve.Nodes.OrderBy(x => x.X).Last();
                }

                // Business
                Node node = _curveManager.CreateNode(curve, afterNode);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void NodeDeleteSelected(int curveID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

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

            // Template Method
            CurveDetailsViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                int nodeID = userInput.SelectedNodeID.Value;
                Node node = _repositories.NodeRepository.Get(nodeID);

                // Business
                IResult result = _curveManager.DeleteNode(node);

                // Non-Persisted
                userInput.ValidationMessages = result.Messages;

                // Successful?
                userInput.Successful = result.Successful;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void NodeMove(int curveID, int nodeID, double x, double y)
        {
            // Opted to not use the TemplateActionMethod,
            // because this is faster but less robust.
            // Because it is not nice when moving nodes is slow.
            // When you work in-memory backed with zipped XML,
            // you might use the more robust method again.
            // The overhead is mainly in the database queries.

            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            userInput.ToEntityWithNodes(_curveRepositories);
            Node node = _repositories.NodeRepository.Get(nodeID);

            // Business
            node.X = x;
            node.Y = y;

            // Successful
            userInput.Successful = true;

            // Refresh
            CurveDetailsNodeRefresh(curveID, nodeID);
            NodePropertiesRefresh(nodeID);
        }

        /// <summary>
        /// Rotates between node types for the selected node.
        /// If no node is selected, nothing happens.
        /// </summary>
        public void NodeChangeSelectedNodeType(int curveID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validation
            if (!userInput.SelectedNodeID.HasValue)
            {
                return;
            }
            int nodeID = userInput.SelectedNodeID.Value;

            // Template Method
            CurveDetailsViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Node node = _repositories.NodeRepository.Get(nodeID);

                // Business
                _curveManager.RotateNodeType(node);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                CurveDetailsNodeRefresh(curveID, nodeID);
                NodePropertiesRefresh(nodeID);
            }
        }

        // Operator

        public void OperatorPropertiesShow(int id)
        {
            // GetViewModel & Partial Action
            {
                OperatorPropertiesViewModel userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForBundle userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForBundle(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForBundle.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCache userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForCache.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForCurve.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCustomOperator userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForCustomOperator.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForMakeContinuous userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForMakeContinuous(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForMakeContinuous.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForNumber userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForNumber.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchInlet userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchOutlet userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForPatchOutlet.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForSample userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_ForSample.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithDimension userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimension(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_WithDimension.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithDimensionAndInterpolation userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimensionAndInterpolation(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_WithDimensionAndInterpolation.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_WithDimensionAndCollectionRecalculation.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithDimensionAndOutletCount userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_WithDimensionAndOutletCount.Show(userInput));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithInletCount userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInletCount(MainViewModel.Document, id);
                if (userInput != null)
                {
                    TemplateActionMethod(userInput, () => _operatorPropertiesPresenter_WithInletCount.Show(userInput));
                    return;
                }
            }

            throw new Exception(String.Format("Properties ViewModel not found for Operator with ID '{0}'.", id));
        }

        public void OperatorPropertiesClose(int id)
        {
            OperatorPropertiesCloseOrLoseFocus(_operatorPropertiesPresenter.Close, id);
        }

        public void OperatorPropertiesClose_ForBundle(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForBundle(_operatorPropertiesPresenter_ForBundle.Close, id);
        }

        public void OperatorPropertiesClose_ForCache(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForCache(_operatorPropertiesPresenter_ForCache.Close, id);
        }

        public void OperatorPropertiesClose_ForCurve(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForCurve(_operatorPropertiesPresenter_ForCurve.Close, id);
        }

        public void OperatorPropertiesClose_ForCustomOperator(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(_operatorPropertiesPresenter_ForCustomOperator.Close, id);
        }

        public void OperatorPropertiesClose_ForMakeContinuous(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForMakeContinuous(_operatorPropertiesPresenter_ForMakeContinuous.Close, id);
        }

        public void OperatorPropertiesClose_ForNumber(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForNumber(_operatorPropertiesPresenter_ForNumber.Close, id);
        }

        public void OperatorPropertiesClose_ForPatchInlet(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(_operatorPropertiesPresenter_ForPatchInlet.Close, id);
        }

        public void OperatorPropertiesClose_ForPatchOutlet(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(_operatorPropertiesPresenter_ForPatchOutlet.Close, id);
        }

        public void OperatorPropertiesClose_ForSample(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForSample(_operatorPropertiesPresenter_ForSample.Close, id);
        }

        public void OperatorPropertiesClose_WithDimension(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithDimension(_operatorPropertiesPresenter_WithDimension.Close, id);
        }

        public void OperatorPropertiesClose_WithDimensionAndInterpolation(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithDimensionAndInterpolation(_operatorPropertiesPresenter_WithDimensionAndInterpolation.Close, id);
        }

        public void OperatorPropertiesClose_WithDimensionAndCollectionRecalculation(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithDimensionAndCollectionRecalculation(_operatorPropertiesPresenter_WithDimensionAndCollectionRecalculation.Close, id);
        }

        public void OperatorPropertiesClose_WithDimensionAndOutletCount(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithDimensionAndOutletCount(_operatorPropertiesPresenter_WithDimensionAndOutletCount.Close, id);
        }

        public void OperatorPropertiesClose_WithInletCount(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithInletCount(_operatorPropertiesPresenter_WithInletCount.Close, id);
        }

        public void OperatorPropertiesLoseFocus(int id)
        {
            OperatorPropertiesCloseOrLoseFocus(_operatorPropertiesPresenter.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForBundle(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForBundle(_operatorPropertiesPresenter_ForBundle.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForCache(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForCache(_operatorPropertiesPresenter_ForCache.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForCurve(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForCurve(_operatorPropertiesPresenter_ForCurve.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForCustomOperator(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(_operatorPropertiesPresenter_ForCustomOperator.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForMakeContinuous(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForMakeContinuous(_operatorPropertiesPresenter_ForMakeContinuous.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForNumber(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForNumber(_operatorPropertiesPresenter_ForNumber.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForPatchInlet(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(_operatorPropertiesPresenter_ForPatchInlet.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForPatchOutlet(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(_operatorPropertiesPresenter_ForPatchOutlet.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_ForSample(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_ForSample(_operatorPropertiesPresenter_ForSample.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_WithDimension(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithDimension(_operatorPropertiesPresenter_WithDimension.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_WithDimensionAndInterpolation(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithDimensionAndInterpolation(_operatorPropertiesPresenter_WithDimensionAndInterpolation.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_WithDimensionAndCollectionRecalculation(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithDimensionAndCollectionRecalculation(_operatorPropertiesPresenter_WithDimensionAndCollectionRecalculation.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_WithDimensionAndOutletCount(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithDimensionAndOutletCount(_operatorPropertiesPresenter_WithDimensionAndOutletCount.LoseFocus, id);
        }

        public void OperatorPropertiesLoseFocus_WithInletCount(int id)
        {
            OperatorPropertiesCloseOrLoseFocus_WithInletCount(_operatorPropertiesPresenter_WithInletCount.LoseFocus, id);
        }

        private void OperatorPropertiesCloseOrLoseFocus(Func<OperatorPropertiesViewModel, OperatorPropertiesViewModel> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel userInput = ViewModelSelector.GetOperatorPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForBundle(Func<OperatorPropertiesViewModel_ForBundle, OperatorPropertiesViewModel_ForBundle> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForBundle userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForBundle(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForBundle viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCache(Func<OperatorPropertiesViewModel_ForCache, OperatorPropertiesViewModel_ForCache> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCache userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCache viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCurve(Func<OperatorPropertiesViewModel_ForCurve, OperatorPropertiesViewModel_ForCurve> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCurve viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(Func<OperatorPropertiesViewModel_ForCustomOperator, OperatorPropertiesViewModel_ForCustomOperator> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCustomOperator userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCustomOperator viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForMakeContinuous(Func<OperatorPropertiesViewModel_ForMakeContinuous, OperatorPropertiesViewModel_ForMakeContinuous> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForMakeContinuous userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForMakeContinuous(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForMakeContinuous viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForNumber(Func<OperatorPropertiesViewModel_ForNumber, OperatorPropertiesViewModel_ForNumber> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForNumber userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForNumber viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(Func<OperatorPropertiesViewModel_ForPatchInlet, OperatorPropertiesViewModel_ForPatchInlet> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForPatchInlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForPatchInlet viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator); // Refresh Dependencies
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(Func<OperatorPropertiesViewModel_ForPatchOutlet, OperatorPropertiesViewModel_ForPatchOutlet> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForPatchOutlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.CustomOperator); // Refresh Dependent Things
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForSample(Func<OperatorPropertiesViewModel_ForSample, OperatorPropertiesViewModel_ForSample> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForSample userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForSample viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_WithDimension(Func<OperatorPropertiesViewModel_WithDimension, OperatorPropertiesViewModel_WithDimension> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithDimension userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithDimension(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithDimension viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_WithDimensionAndInterpolation(Func<OperatorPropertiesViewModel_WithDimensionAndInterpolation, OperatorPropertiesViewModel_WithDimensionAndInterpolation> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithDimensionAndInterpolation userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithDimensionAndInterpolation(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_WithDimensionAndCollectionRecalculation(Func<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation, OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_WithDimensionAndOutletCount(Func<OperatorPropertiesViewModel_WithDimensionAndOutletCount, OperatorPropertiesViewModel_WithDimensionAndOutletCount> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithDimensionAndOutletCount userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithDimensionAndOutletCount(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_WithInletCount(Func<OperatorPropertiesViewModel_WithInletCount, OperatorPropertiesViewModel_WithInletCount> partialAction, int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithInletCount userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithInletCount(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithInletCount viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorCreate(int patchID, int operatorTypeID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            PatchDetailsViewModel viewModel = TemplateActionMethod(userInput, () => _patchDetailsPresenter.CreateOperator(userInput, operatorTypeID));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorDeleteSelected(int patchID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // Template Method
            PatchDetailsViewModel viewModel = TemplateActionMethod(userInput, () => _patchDetailsPresenter.DeleteOperator(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorMove(int patchID, int operatorID, float centerX, float centerY)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _patchDetailsPresenter.MoveOperator(userInput, operatorID, centerX, centerY));
        }

        public void OperatorChangeInputOutlet(int patchID, int inletID, int inputOutletID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _patchDetailsPresenter.ChangeInputOutlet(userInput, inletID, inputOutletID));
        }

        public void OperatorSelect(int patchID, int operatorID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // Partial Action
            TemplateActionMethod(userInput, () => _patchDetailsPresenter.SelectOperator(userInput, operatorID));
        }

        // Patch

        public void PatchDetailsShow(int patchID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => _patchDetailsPresenter.Show(userInput));
        }

        public void PatchDetailsClose(int patchID)
        {
            PatchDetailsCloseOrLoseFocus(_patchDetailsPresenter.Close, patchID);
        }

        public void PatchDetailsLoseFocus(int patchID)
        {
            PatchDetailsCloseOrLoseFocus(_patchDetailsPresenter.LoseFocus, patchID);
        }

        private void PatchDetailsCloseOrLoseFocus(Func<PatchDetailsViewModel, PatchDetailsViewModel> partialAction, int patchID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            TemplateActionMethod(userInput, () => partialAction(userInput));
        }

        /// <summary> Returns output file path if ViewModel.Successful. <summary>
        public string PatchPlay(int patchID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

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

        public void PatchPropertiesShow(int patchID)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, patchID);

            // Template Method
            TemplateActionMethod(userInput, () => _patchPropertiesPresenter.Show(userInput));
        }

        public void PatchPropertiesClose(int patchID)
        {
            PatchPropertiesCloseOrLoseFocus(_patchPropertiesPresenter.Close, patchID);
        }

        public void PatchPropertiesLoseFocus(int patchID)
        {
            PatchPropertiesCloseOrLoseFocus(_patchPropertiesPresenter.LoseFocus, patchID);
        }

        private void PatchPropertiesCloseOrLoseFocus(Func<PatchPropertiesViewModel, PatchPropertiesViewModel> partialAction, int patchID)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, patchID);

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
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

            // Template Method
            TemplateActionMethod(userInput, () => _patchGridPresenter.Show(userInput));
        }

        public void PatchGridClose(string group)
        {
            // GetViewModel
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

            // Template Method
            TemplateActionMethod(userInput, () => _patchGridPresenter.Close(userInput));
        }

        /// <param name="group">nullable</param>
        public void PatchCreate(string group)
        {
            // GetViewModel
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

            // Template Method
            PatchGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

                // Business
                var patchManager = new PatchManager(_patchRepositories);
                patchManager.CreatePatch(document, mustGenerateName: true);
                Patch patch = patchManager.Patch;
                patch.GroupName = group;

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void PatchDelete(string group, int patchID)
        {
            // GetViewModel
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel(MainViewModel.Document, group);

            // Template Method
            PatchGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Patch patch = _repositories.PatchRepository.Get(patchID);

                // Businesss
                var patchManager = new PatchManager(_patchRepositories);
                patchManager.PatchID = patchID;
                IResult result = patchManager.DeletePatchWithRelatedEntities();

                // Non-Persisted
                userInput.ValidationMessages.AddRange(result.Messages);

                // Successful?
                userInput.Successful = result.Successful;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        // Sample

        public void SampleGridShow()
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // Template Method
            TemplateActionMethod(userInput, () => _sampleGridPresenter.Show(userInput));
        }

        public void SampleGridClose()
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // Template Method
            TemplateActionMethod(userInput, () => _sampleGridPresenter.Close(userInput));
        }

        public void SampleCreate()
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // Template Method
            SampleGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

                // Business
                Sample sample = _sampleManager.CreateSample(document, mustGenerateName: true);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void SampleDelete(int sampleID)
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // Template Method
            SampleGridViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // GetEntity
                Sample sample = _repositories.SampleRepository.Get(sampleID);

                // Business
                IResult result = _sampleManager.Delete(sample);

                // Non-Persisted
                userInput.ValidationMessages = result.Messages;

                // Successful?
                userInput.Successful = result.Successful;

                return userInput;
            });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void SamplePropertiesShow(int id)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = ViewModelSelector.GetSamplePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            TemplateActionMethod(userInput, () => _samplePropertiesPresenter.Show(userInput));
        }

        public void SamplePropertiesClose(int id)
        {
            SamplePropertiesCloseOrLoseFocus(_samplePropertiesPresenter.Close, id);
        }

        public void SamplePropertiesLoseFocus(int id)
        {
            SamplePropertiesCloseOrLoseFocus(_samplePropertiesPresenter.LoseFocus, id);
        }

        private void SamplePropertiesCloseOrLoseFocus(Func<SamplePropertiesViewModel, SamplePropertiesViewModel> partialAction, int sampleID)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = ViewModelSelector.GetSamplePropertiesViewModel(MainViewModel.Document, sampleID);

            // TemplateMethod
            SamplePropertiesViewModel viewModel = TemplateActionMethod(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                Sample sample = _repositories.SampleRepository.Get(sampleID);
                SampleGridRefresh();
                SampleLookupRefresh(sampleID);
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
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Document document = _repositories.DocumentRepository.Get(userInput.DocumentID);

                // Business
                Scale scale = _scaleManager.Create(document, mustSetDefaults: true, mustGenerateName: true);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            // Refresh
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
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Scale scale = _repositories.ScaleRepository.Get(id);

                // Business
                _scaleManager.DeleteWithRelatedEntities(id);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            if (viewModel.Successful)
            {
                // ToViewModel
                MainViewModel.Document.ScaleGrid.List.RemoveFirst(x => x.ID == id);
                MainViewModel.Document.ToneGridEditDictionary.Remove(id);
                MainViewModel.Document.ScalePropertiesDictionary.Remove(id);

                // Refresh
                DocumentTreeRefresh();
            }
        }

        public void ScaleShow(int id)
        {
            // GetViewModel
            ScalePropertiesViewModel userInput1 = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);
            ToneGridEditViewModel userInput2 = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID: id);

            // RefreshCounter
            userInput1.RefreshCounter++;
            userInput2.RefreshCounter++;

            // Set !Successful
            userInput1.Successful = false;
            userInput2.Successful = false;

            // ToEntity
            Document document = MainViewModel.ToEntityWithRelatedEntities(_repositories);

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
            IResult validationResult = _documentManager.Save(document);
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

        public void ScalePropertiesClose(int id)
        {
            ScalePropertiesCloseOrLoseFocus(_scalePropertiesPresenter.Close, id);
        }

        public void ScalePropertiesLoseFocus(int id)
        {
            ScalePropertiesCloseOrLoseFocus(_scalePropertiesPresenter.LoseFocus, id);
        }

        private void ScalePropertiesCloseOrLoseFocus(Func<ScalePropertiesViewModel, ScalePropertiesViewModel> partialAction, int id)
        {
            // Get ViewModel
            ScalePropertiesViewModel userInput = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

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
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // RefreshCounter
            userInput.RefreshCounter++;

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

            // TemplateMethod
            Tone tone = null;
            ToneGridEditViewModel viewModel = TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // Get Entity
                Scale scale = _repositories.ScaleRepository.Get(scaleID);

                // Business
                tone = _scaleManager.CreateTone(scale);

                // Successful
                userInput.Successful = true;

                return userInput;
            });

            if (viewModel.Successful)
            {
                // ToViewModel
                ToneViewModel toneViewModel = tone.ToViewModel();
                userInput.Tones.Add(toneViewModel);
                // Do not sort grid, so that the new item appears at the bottom.
            }
        }

        public void ToneDelete(int scaleID, int toneID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // RefreshCounter
            userInput.RefreshCounter++;

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

            // Template Method
            TemplateActionMethod(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntity
                Tone tone = _repositories.ToneRepository.Get(toneID);
                Scale scale = tone.Scale;

                // Business
                _scaleManager.DeleteTone(tone);

                // ToViewModel
                ToneGridEditViewModel viewModel = scale.ToToneGridEditViewModel();

                // Non-Persisted
                viewModel.Visible = userInput.Visible;

                // Successful
                viewModel.Successful = true;

                return viewModel;
            });
        }

        public void ToneGridEditClose(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            TemplateActionMethod(userInput, () => _toneGridEditPresenter.Close(userInput));
        }

        public void ToneGridEditLoseFocus(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            TemplateActionMethod(userInput, () => _toneGridEditPresenter.LoseFocus(userInput));
        }

        public void ToneGridEditEdit(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            TemplateActionMethod(userInput, () => _toneGridEditPresenter.Edit(userInput));
        }

        /// <summary>
        /// Writes a sine sound with the pitch of the tone to an audio file with a configurable duration.
        /// Returns the output file path if successful.
        /// TODO: This action is too dependent on infrastructure, because the AudioFileOutput business logic is.
        /// Instead of writing to a file it had better write to a stream.
        /// </summary>
        public string TonePlay(int scaleID, int toneID)
        {
            // NOTE:
            // Cannot use partial presenter, because this action uses both
            // ToneGridEditViewModel and CurrentPatches view model.

            // GetEntity
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // RefreshCounter
            userInput.RefreshCounter++;

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
            Document document = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            Tone tone = _repositories.ToneRepository.Get(toneID);
            AudioOutput audioOutput = document.AudioOutput;

            var underlyingPatches = new List<Patch>(MainViewModel.Document.CurrentPatches.List.Count);
            foreach (IDAndName itemViewModel in MainViewModel.Document.CurrentPatches.List)
            {
                Patch underlyingPatch = _repositories.PatchRepository.Get(itemViewModel.ID);
                underlyingPatches.Add(underlyingPatch);
            }

            // Business
            var patchManager = new PatchManager(_patchRepositories);

            Outlet outlet = null;
            if (underlyingPatches.Count != 0)
            {
                outlet = patchManager.TryAutoPatch_WithTone(tone, underlyingPatches);
            }

            if (outlet == null) // Fallback to Sine
            {
                var x = new PatchApi();
                double frequency = tone.GetFrequency();
                outlet = x.Sine(x.PatchInlet(DimensionEnum.Frequency, frequency));
            }

            IResult validationResult = _documentManager.Save(document);
            if (!validationResult.Successful)
            {
                userInput.Successful = false;
                userInput.ValidationMessages = validationResult.Messages;
                return null;
            }

            patchManager.Patch = outlet.Operator.Patch;
            IPatchCalculator patchCalculator = patchManager.CreateCalculator(
                outlet,
                audioOutput.SamplingRate,
                audioOutput.GetChannelCount(), 
                DEFAULT_CHANNEL_INDEX, 
                new CalculatorCache());

            // Infrastructure
            AudioFileOutput audioFileOutput = _audioFileOutputManager.Create();
            audioFileOutput.LinkTo(audioOutput.SpeakerSetup);
            audioFileOutput.SamplingRate = audioOutput.SamplingRate;
            audioFileOutput.FilePath = _playOutputFilePath;
            audioFileOutput.Duration = DEFAULT_DURATION;
            audioFileOutput.LinkTo(outlet);
            _audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

            // Successful
            userInput.Successful = true;

            return _playOutputFilePath;
        }

        // Helpers

        /// <summary>
        /// A template method for a MainPresenter action method.
        /// Works for most actions. Less suitable for specialized cases.
        /// In particular the ones that are not about the open document.
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
            Document document = MainViewModel.ToEntityWithRelatedEntities(_repositories);

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
            IResult validationResult = _documentManager.Save(document);
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
