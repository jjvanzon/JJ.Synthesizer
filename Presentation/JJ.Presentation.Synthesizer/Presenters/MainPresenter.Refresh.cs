using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Data.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Enums;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        private void AudioFileOutputGridRefresh()
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Partial Action
            AudioFileOutputGridViewModel viewModel = _audioFileOutputGridPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void AudioFileOutputPropertiesRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<AudioFileOutput> entities = document.AudioFileOutputs;

            foreach (AudioFileOutput entity in entities)
            {
                AudioFileOutputPropertiesViewModel viewModel = ViewModelSelector.TryGetAudioFileOutputPropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.AudioFileOutputPropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    AudioFileOutputPropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.AudioFileOutputPropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.AudioFileOutputPropertiesDictionary.Remove(idToDelete);
            }
        }

        private void AudioFileOutputPropertiesRefresh(AudioFileOutputPropertiesViewModel userInput)
        {
            // Partial Action
            AudioFileOutputPropertiesViewModel viewModel = _audioFileOutputPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void AudioOutputPropertiesRefresh()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // Partial Action
            AudioOutputPropertiesViewModel viewModel = _audioOutputPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void CurrentPatchesRefresh()
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // Partial Action
            CurrentPatchesViewModel viewModel = _currentPatchesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void CurveDetailsDictionaryRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Curve> curves = childDocument.Curves;

            foreach (Curve curve in curves)
            {
                CurveDetailsViewModel curveDetailsViewModel = ViewModelSelector.TryGetCurveDetailsViewModel(MainViewModel.Document, curve.ID);
                if (curveDetailsViewModel == null)
                {
                    curveDetailsViewModel = curve.ToDetailsViewModel();
                    curveDetailsViewModel.Successful = true;
                    patchDocumentViewModel.CurveDetailsDictionary[curve.ID] = curveDetailsViewModel;
                }
                else
                {
                    CurveDetailsRefresh(curveDetailsViewModel);
                }
            }

            IEnumerable<int> existingIDs = patchDocumentViewModel.CurveDetailsDictionary.Keys;
            IEnumerable<int> idsToKeep = curves.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                patchDocumentViewModel.CurveDetailsDictionary.Remove(idToDelete);
            }
        }

        private void CurveDetailsDictionaryRefresh(DocumentViewModel documentViewModel)
        {
            Document document = _repositories.DocumentRepository.Get(documentViewModel.ID);
            IList<Curve> entities = document.Curves;

            foreach (Curve entity in entities)
            {
                CurveDetailsViewModel viewModel = ViewModelSelector.TryGetCurveDetailsViewModel(documentViewModel, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToDetailsViewModel();
                    viewModel.Successful = true;
                    documentViewModel.CurveDetailsDictionary[entity.ID] = viewModel;
                }
                else
                {
                    CurveDetailsRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = documentViewModel.CurveDetailsDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                documentViewModel.CurveDetailsDictionary.Remove(idToDelete);
            }
        }

        private void CurveDetailsNodeRefresh(int nodeID)
        {
            // TODO: This is not very fast.
            CurveDetailsViewModel detailsViewModel = ViewModelSelector.GetCurveDetailsViewModel_ByNodeID(MainViewModel.Document, nodeID);

            // Remove original node
            detailsViewModel.Nodes.Remove(nodeID);

            // Add new version of the node
            Node node = _repositories.NodeRepository.Get(nodeID);
            NodeViewModel nodeViewModel = node.ToViewModel();
            detailsViewModel.Nodes[nodeID] = nodeViewModel;

            detailsViewModel.RefreshCounter++;
        }

        private void CurveDetailsRefresh(CurveDetailsViewModel userInput)
        {
            CurveDetailsViewModel viewModel = _curveDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void CurveGridRefresh(CurveGridViewModel userInput)
        {
            CurveGridViewModel viewModel = _curveGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void CurveLookupRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            patchDocumentViewModel.CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(rootDocument, childDocument);
        }

        private void CurveLookupsItemsRefresh(int curveID)
        {
            Curve curve = _repositories.CurveRepository.Get(curveID);

            foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentDictionary.Values)
            {
                IDAndName idAndName2 = patchDocumentViewModel.CurveLookup.Where(x => x.ID == curve.ID).FirstOrDefault();
                if (idAndName2 != null)
                {
                    idAndName2.Name = curve.Name;
                    patchDocumentViewModel.CurveLookup = patchDocumentViewModel.CurveLookup.OrderBy(x => x.Name).ToList();
                }
            }
        }

        private void CurveLookupsRefresh()
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            foreach (Document childDocument in rootDocument.ChildDocuments)
            {
                PatchDocumentViewModel patchDocumentViewModel = ViewModelSelector.GetPatchDocumentViewModel(MainViewModel.Document, childDocument.ID);
                patchDocumentViewModel.CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(rootDocument, childDocument);
            }
        }

        private void CurvePropertiesDictionaryRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Curve> curves = childDocument.Curves;

            foreach (Curve curve in curves)
            {
                CurvePropertiesViewModel curvePropertiesViewModel = ViewModelSelector.TryGetCurvePropertiesViewModel(MainViewModel.Document, curve.ID);
                if (curvePropertiesViewModel == null)
                {
                    curvePropertiesViewModel = curve.ToPropertiesViewModel();
                    curvePropertiesViewModel.Successful = true;
                    patchDocumentViewModel.CurvePropertiesDictionary[curve.ID] = curvePropertiesViewModel;
                }
                else
                {
                    CurvePropertiesRefresh(curvePropertiesViewModel);
                }
            }

            IEnumerable<int> existingIDs = patchDocumentViewModel.CurvePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = curves.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                patchDocumentViewModel.CurvePropertiesDictionary.Remove(idToDelete);
            }
        }

        private void CurvePropertiesDictionaryRefresh(DocumentViewModel documentViewModel)
        {
            Document document = _repositories.DocumentRepository.Get(documentViewModel.ID);
            IList<Curve> entities = document.Curves;

            foreach (Curve entity in entities)
            {
                CurvePropertiesViewModel viewModel = ViewModelSelector.TryGetCurvePropertiesViewModel(documentViewModel, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    documentViewModel.CurvePropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    CurvePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = documentViewModel.CurvePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                documentViewModel.CurvePropertiesDictionary.Remove(idToDelete);
            }
        }

        private void CurvePropertiesRefresh(CurvePropertiesViewModel userInput)
        {
            CurvePropertiesViewModel viewModel = _curvePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void DocumentGridRefresh()
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _documentGridPresenter.Refresh(userInput));
        }

        private void DocumentPropertiesRefresh()
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // Partial Action
            DocumentPropertiesViewModel viewModel = _documentPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void DocumentTreeRefresh()
        {
            // Partial Action
            DocumentTreeViewModel viewModel = _documentTreePresenter.Refresh(MainViewModel.Document.DocumentTree);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void DocumentViewModelRefresh()
        {
            AudioFileOutputGridRefresh();
            CurrentPatchesRefresh();
            CurveGridRefresh(MainViewModel.Document.CurveGrid);
            DocumentPropertiesRefresh();
            DocumentTreeRefresh();
            SampleGridRefresh(MainViewModel.Document.SampleGrid);
            ScaleGridRefresh();

            AudioOutputPropertiesRefresh();
            AudioFileOutputPropertiesRefresh();
            AudioOutputPropertiesRefresh();
            CurveDetailsDictionaryRefresh(MainViewModel.Document);
            CurvePropertiesDictionaryRefresh(MainViewModel.Document);
            NodePropertiesListRefresh(MainViewModel.Document);
            PatchGridListsRefresh();
            SamplePropertiesDictionaryRefresh(MainViewModel.Document);
            ScalePropertiesListRefresh();
            ToneGridEditListRefresh();
            UnderylingPatchLookupRefresh();

            // Note that AutoPatchDetails cannot be refreshed.

            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            foreach (Document childDocument in rootDocument.ChildDocuments)
            {
                PatchDocumentViewModel patchDocumentViewModel = ViewModelSelector.TryGetPatchDocumentViewModel(MainViewModel.Document, childDocument.ID);

                if (patchDocumentViewModel == null)
                {
                    patchDocumentViewModel = childDocument.ToPatchDocumentViewModel(_repositories, _entityPositionManager);

                    patchDocumentViewModel.CurveDetailsDictionary.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.CurveGrid.Successful = true;
                    patchDocumentViewModel.CurvePropertiesDictionary.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.NodePropertiesDictionary.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForBundles.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForCaches.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForCurves.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForCustomOperators.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForMakeContinuous.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForNumbers.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchInlets.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchOutlets.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForSamples.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithDimension.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndInterpolation.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndOutletCount.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithInletCount.Values.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.PatchDetails.Successful = true;
                    patchDocumentViewModel.PatchProperties.Successful = true;
                    patchDocumentViewModel.SampleGrid.Successful = true;
                    patchDocumentViewModel.SamplePropertiesDictionary.Values.Select(x => x.Successful = true);

                    MainViewModel.Document.PatchDocumentDictionary[patchDocumentViewModel.ChildDocumentID] = patchDocumentViewModel;
                }
                else
                {
                    PatchDocumentRefresh(patchDocumentViewModel);
                }
            }

            // Delete Operations
            IEnumerable<int> existingChildDocumentIDs = MainViewModel.Document.PatchDocumentDictionary.Keys;
            IEnumerable<int> childDocumentIDsToKeep = rootDocument.ChildDocuments.Select(x => x.ID);
            IEnumerable<int> childDocumentIDsToDelete = existingChildDocumentIDs.Except(childDocumentIDsToKeep);

            foreach (int childDocumentIDToDelete in childDocumentIDsToDelete)
            {
                MainViewModel.Document.PatchDocumentDictionary.Remove(childDocumentIDToDelete);
            }
        }

        private void NodePropertiesListRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Node> nodes = childDocument.Curves.SelectMany(x => x.Nodes).ToArray();

            foreach (Node node in nodes)
            {
                NodePropertiesViewModel nodePropertiesViewModel = ViewModelSelector.TryGetNodePropertiesViewModel(MainViewModel.Document, node.ID);
                if (nodePropertiesViewModel == null)
                {
                    nodePropertiesViewModel = node.ToPropertiesViewModel();
                    nodePropertiesViewModel.Successful = true;
                    patchDocumentViewModel.NodePropertiesDictionary[node.ID] = nodePropertiesViewModel;
                }
                else
                {
                    NodePropertiesRefresh(nodePropertiesViewModel);
                }
            }

            IEnumerable<int> existingIDs = patchDocumentViewModel.NodePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = nodes.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                patchDocumentViewModel.NodePropertiesDictionary.Remove(idToDelete);
            }
        }

        private void NodePropertiesListRefresh(DocumentViewModel documentViewModel)
        {
            Document document = _repositories.DocumentRepository.Get(documentViewModel.ID);
            IList<Node> entities = document.Curves.SelectMany(x => x.Nodes).ToArray();

            foreach (Node entity in entities)
            {
                NodePropertiesViewModel viewModel = ViewModelSelector.TryGetNodePropertiesViewModel(documentViewModel, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    documentViewModel.NodePropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    NodePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = documentViewModel.NodePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                documentViewModel.NodePropertiesDictionary.Remove(idToDelete);
            }
        }

        private void NodePropertiesRefresh(int nodeID)
        {
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);
            NodePropertiesRefresh(userInput);
        }

        private void NodePropertiesRefresh(NodePropertiesViewModel userInput)
        {
            NodePropertiesViewModel viewModel = _nodePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForBundle_Refresh(OperatorPropertiesViewModel_ForBundle userInput)
        {
            OperatorPropertiesViewModel_ForBundle viewModel = _operatorPropertiesPresenter_ForBundle.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCache_Refresh(OperatorPropertiesViewModel_ForCache userInput)
        {
            OperatorPropertiesViewModel_ForCache viewModel = _operatorPropertiesPresenter_ForCache.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCurve_Refresh(OperatorPropertiesViewModel_ForCurve userInput)
        {
            OperatorPropertiesViewModel_ForCurve viewModel = _operatorPropertiesPresenter_ForCurve.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCustomOperator_Refresh(OperatorPropertiesViewModel_ForCustomOperator userInput)
        {
            OperatorPropertiesViewModel_ForCustomOperator viewModel = _operatorPropertiesPresenter_ForCustomOperator.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCustomOperatorViewModel_Refresh(OperatorPropertiesViewModel_ForCustomOperator userInput)
        {
            OperatorPropertiesViewModel_ForCustomOperator viewModel = _operatorPropertiesPresenter_ForCustomOperator.Refresh(userInput);

            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCustomOperatorViewModels_Refresh(int underlyingPatchID)
        {
            IList<OperatorPropertiesViewModel_ForCustomOperator> propertiesViewModelList = 
                MainViewModel.Document.PatchDocumentDictionary.Values.SelectMany(x => x.OperatorPropertiesDictionary_ForCustomOperators.Values).ToArray();

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in propertiesViewModelList)
            {
                OperatorProperties_ForCustomOperatorViewModel_Refresh(propertiesViewModel);
            }
        }

        private void OperatorProperties_ForMakeContinuous_Refresh(OperatorPropertiesViewModel_ForMakeContinuous userInput)
        {
            OperatorPropertiesViewModel_ForMakeContinuous viewModel = _operatorPropertiesPresenter_ForMakeContinuous.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForNumber_Refresh(OperatorPropertiesViewModel_ForNumber userInput)
        {
            OperatorPropertiesViewModel_ForNumber viewModel = _operatorPropertiesPresenter_ForNumber.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForPatchInlet_Refresh(OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            OperatorPropertiesViewModel_ForPatchInlet viewModel = _operatorPropertiesPresenter_ForPatchInlet.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForPatchOutlet_Refresh(OperatorPropertiesViewModel_ForPatchOutlet userInput)
        {
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = _operatorPropertiesPresenter_ForPatchOutlet.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForSample_Refresh(OperatorPropertiesViewModel_ForSample userInput)
        {
            OperatorPropertiesViewModel_ForSample viewModel = _operatorPropertiesPresenter_ForSample.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_WithDimension_Refresh(OperatorPropertiesViewModel_WithDimension userInput)
        {
            OperatorPropertiesViewModel_WithDimension viewModel = _operatorPropertiesPresenter_WithDimension.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_WithDimensionAndInterpolation_Refresh(OperatorPropertiesViewModel_WithDimensionAndInterpolation userInput)
        {
            OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel = _operatorPropertiesPresenter_WithDimensionAndInterpolation.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_WithDimensionAndCollectionRecalculation_Refresh(OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation userInput)
        {
            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel = _operatorPropertiesPresenter_WithDimensionAndCollectionRecalculation.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_WithDimensionAndOutletCount_Refresh(OperatorPropertiesViewModel_WithDimensionAndOutletCount userInput)
        {
            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = _operatorPropertiesPresenter_WithDimensionAndOutletCount.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_WithInletCount_Refresh(OperatorPropertiesViewModel_WithInletCount userInput)
        {
            OperatorPropertiesViewModel_WithInletCount viewModel = _operatorPropertiesPresenter_WithInletCount.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorPropertiesList_ForBundles_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Bundle);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForBundle viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForBundle(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForBundle();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForBundles[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForBundle_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForBundles, operators);
        }

        private void OperatorPropertiesList_ForCaches_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Cache);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCache viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCache(_repositories.InterpolationTypeRepository);
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForCaches[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCache_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForCaches, operators);
        }

        private void OperatorPropertiesList_ForCurves_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Curve);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCurve viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCurve(_repositories.CurveRepository);
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForCurves[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCurve_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForCurves, operators);
        }

        private void OperatorPropertiesList_ForCustomOperators_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.CustomOperator);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCustomOperator viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCustomOperator(_repositories.PatchRepository);
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForCustomOperators[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCustomOperator_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForCustomOperators, operators);
        }

        private void OperatorPropertiesList_ForMakeContinuous_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.MakeContinuous);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForMakeContinuous viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForMakeContinuous(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForMakeContinuous();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForMakeContinuous[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForMakeContinuous_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForMakeContinuous, operators);
        }

        private void OperatorPropertiesList_ForNumbers_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Number);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForNumber viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForNumber();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForNumbers[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForNumber_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForNumbers, operators);
        }

        private void OperatorPropertiesList_ForPatchInlets_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForPatchInlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForPatchInlet();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchInlets[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForPatchInlet_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchInlets, operators);
        }

        private void OperatorPropertiesList_ForPatchOutlets_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForPatchOutlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForPatchOutlet();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchOutlets[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForPatchOutlet_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchOutlets, operators);
        }

        private void OperatorPropertiesList_ForSamples_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Sample);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForSample viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_ForSamples[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForSample_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_ForSamples, operators);
        }

        private void OperatorPropertiesList_WithDimension_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);

            IList<Operator> operators = childDocument.Patches[0].Operators
                                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithDimension viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimension(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithDimension();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithDimension[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithDimension_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_WithDimension, operators);
        }

        private void OperatorPropertiesList_WithDimensionAndInterpolation_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);

            IList<Operator> operators = childDocument.Patches[0].Operators
                                                     .Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionAndInterpolationPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                     .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel = 
                    ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimensionAndInterpolation(MainViewModel.Document, op.ID);

                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithDimensionAndInterpolation();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndInterpolation[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithDimensionAndInterpolation_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndInterpolation, operators);
        }

        private void OperatorPropertiesList_WithDimensionAndCollectionRecalculation_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);

            IList<Operator> operators = childDocument.Patches[0].Operators
                                                     .Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionAndCollectionRecalculationPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                     .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel =
                    ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(MainViewModel.Document, op.ID);

                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithDimensionAndCollectionRecalculation();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithDimensionAndCollectionRecalculation_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation, operators);
        }

        private void OperatorPropertiesList_WithDimensionAndOutletCount_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].Operators
                                                     .Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionAndOutletCountPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                     .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithDimensionAndOutletCount();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndOutletCount[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithDimensionAndOutletCount_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndOutletCount, operators);
        }

        private void OperatorPropertiesList_WithInletCount_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].Operators
                                                     .Where(x => ViewModelHelper.OperatorTypeEnums_WithInletCountPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                     .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithInletCount viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInletCount(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithInletCount();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary_WithInletCount[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithInletCount_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary_WithInletCount, operators);
        }

        private void OperatorPropertiesListRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].Operators
                                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithoutAlternativePropertiesView.Contains(x.GetOperatorTypeEnum()))
                                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorPropertiesRefresh(viewModel);
                }
            }

            DeleteOperatorViewModels(patchDocumentViewModel.OperatorPropertiesDictionary, operators);
        }

        private void OperatorPropertiesRefresh(OperatorPropertiesViewModel userInput)
        {
            OperatorPropertiesViewModel viewModel = _operatorPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        /// <summary>
        /// When an underlying document of a custom operator is changed,
        /// we do not know which PatchDetails OperatorViewModels are affected,
        /// because no OperatorViewModel has as property saying what UnderlyingPatch it is.
        /// Therefore we refresh all CustomOperators.
        ///
        /// But also, a custom operator would need to be updated if something connected to it is deleted,
        /// because then the obsolete inlets and outlets might be cleaned up.
        /// </summary>
        private void OperatorViewModels_OfType_Refresh(OperatorTypeEnum operatorTypeEnum)
        {
            IList<PatchDetailsViewModel> patchDetailsViewModels = MainViewModel.Document.PatchDocumentDictionary.Values.Select(x => x.PatchDetails).ToArray();

            IList<OperatorViewModel> operatorViewModels =
                patchDetailsViewModels.SelectMany(x => x.Entity.OperatorDictionary.Values)
                                      .Where(x => x.OperatorType.ID == (int)operatorTypeEnum)
                                      .ToArray();

            foreach (OperatorViewModel operatorViewModel in operatorViewModels)
            {
                PatchDetails_RefreshOperator(operatorViewModel);
            }
        }

        private void PatchDetails_RefreshOperator(OperatorViewModel viewModel)
        {
            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);
            PatchDetails_RefreshOperator(entity, viewModel);
        }

        private void PatchDetails_RefreshOperator(int operatorID)
        {
            OperatorViewModel viewModel = ViewModelSelector.GetOperatorViewModel(MainViewModel.Document, operatorID);
            PatchDetails_RefreshOperator(viewModel);

            // TODO: Replace this with moving RefreshOperator to the PatchDetail presenter?
            PatchDetailsViewModel detailsViewModel = ViewModelSelector.GetPatchDetailsViewModel_ByOperatorID(MainViewModel.Document, operatorID);
            detailsViewModel.RefreshCounter++;
        }

        private void PatchDetails_RefreshOperator(Operator entity, OperatorViewModel operatorViewModel)
        {
            ViewModelHelper.RefreshViewModel_WithInletsAndOutlets(
                entity,
                operatorViewModel,
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _repositories.PatchRepository,
                _entityPositionManager);
        }

        private void PatchDetailsRefresh(PatchDetailsViewModel userInput)
        {
            PatchDetailsViewModel viewModel = _patchDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchGridListsRefresh()
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            IList<string> groups = _documentManager.GetChildDocumentGroupNames(rootDocument);

            // Always add nameless group even when there are no child documents in it.
            groups.Add(null);

            foreach (string group in groups)
            {
                PatchGridViewModel viewModel = ViewModelSelector.TryGetPatchGridViewModel_ByGroup(MainViewModel.Document, group);
                if (viewModel == null)
                {
                    IList<Document> childDocumentsInGroup = _documentManager.GetChildDocumentsInGroup_IncludingGroupless(rootDocument, group);
                    viewModel = childDocumentsInGroup.ToPatchGridViewModel(rootDocument.ID, group);
                    viewModel.Successful = true;
                    MainViewModel.Document.PatchGridDictionary[group] = viewModel;
                }
                else
                {
                    PatchGridRefresh(viewModel);
                }
            }

            // Delete operations
            IEnumerable<string> existingGroups = MainViewModel.Document.PatchGridDictionary.Keys;
            IEnumerable<string> groupsToDelete = groups.Select(x => x?.ToLower() ?? "").Except(existingGroups);

            foreach (string groupToDelete in groupsToDelete)
            {
                MainViewModel.Document.PatchGridDictionary.Remove(groupToDelete);
            }
        }

        private void PatchGridRefresh(string group)
        {
            PatchGridViewModel viewModel2 = ViewModelSelector.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

            PatchGridRefresh(viewModel2);
        }

        private void PatchGridRefresh(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);
            PatchGridViewModel viewModel = _patchGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchGridsRefresh()
        {
            // GetEntity
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            // Business
            IList<Document> grouplessChildDocuments = _documentManager.GetGrouplessChildDocuments(rootDocument);
            IList<ChildDocumentGroupDto> childDocumentGroupDtos = _documentManager.GetChildDocumentGroupDtos(rootDocument);

            // ToViewModel
            // Patch grids can be updated, created and deleted as group names are changed.
            // All the logic in CreatePatchGridViewModelList is required for this.
            MainViewModel.Document.PatchGridDictionary = ViewModelHelper.CreatePatchGridViewModelDictionary(
                grouplessChildDocuments,
                childDocumentGroupDtos,
                rootDocument.ID);

            // DispatchViewModel
            foreach (PatchGridViewModel gridViewModel in MainViewModel.Document.PatchGridDictionary.Values.ToArray())
            {
                DispatchViewModel(gridViewModel);
            }
        }

        private void PatchPropertiesRefresh(PatchPropertiesViewModel userInput)
        {
            PatchPropertiesViewModel viewModel = _patchPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchDocumentRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            CurveGridRefresh(patchDocumentViewModel.CurveGrid);
            CurveLookupRefresh(patchDocumentViewModel);
            PatchDetailsRefresh(patchDocumentViewModel.PatchDetails);
            CurveDetailsDictionaryRefresh(patchDocumentViewModel);
            CurvePropertiesDictionaryRefresh(patchDocumentViewModel);
            NodePropertiesListRefresh(patchDocumentViewModel);

            OperatorPropertiesListRefresh(patchDocumentViewModel);
            OperatorPropertiesList_ForBundles_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForCaches_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForCurves_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForCustomOperators_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForMakeContinuous_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForNumbers_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForPatchInlets_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForPatchOutlets_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForSamples_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_WithDimension_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_WithDimensionAndInterpolation_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_WithDimensionAndCollectionRecalculation_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_WithDimensionAndOutletCount_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_WithInletCount_Refresh(patchDocumentViewModel);

            SamplePropertiesDictionaryRefresh(patchDocumentViewModel);

            PatchPropertiesRefresh(patchDocumentViewModel.PatchProperties);
            SampleGridRefresh(patchDocumentViewModel.SampleGrid);
            SampleLookupRefresh(patchDocumentViewModel);
        }

        private void SampleGridRefresh(SampleGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Partial Action
            SampleGridViewModel viewModel = _sampleGridPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        /// <summary> Will update the SampleGrid that the sample with sampleID is part of. </summary>
        private void SampleGridRefresh(int sampleID)
        {
            SampleGridViewModel userInput = ViewModelSelector.GetSampleGridViewModel_BySampleID(MainViewModel.Document, sampleID);
            SampleGridViewModel viewModel = _sampleGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void SampleLookupRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            patchDocumentViewModel.SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(rootDocument, childDocument);
        }

        private void SampleLookupsRefresh(int sampleID)
        {
            Sample sample = _repositories.SampleRepository.Get(sampleID);

            foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentDictionary.Values)
            {
                IDAndName idAndName2 = patchDocumentViewModel.SampleLookup.Where(x => x.ID == sample.ID).FirstOrDefault();
                if (idAndName2 != null)
                {
                    idAndName2.Name = sample.Name;
                    patchDocumentViewModel.SampleLookup = patchDocumentViewModel.SampleLookup.OrderBy(x => x.Name).ToList();
                }
            }
        }

        private void SamplePropertiesDictionaryRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Sample> samples = childDocument.Samples;

            foreach (Sample sample in samples)
            {
                SamplePropertiesViewModel samplePropertiesViewModel = ViewModelSelector.TryGetSamplePropertiesViewModel(MainViewModel.Document, sample.ID);
                if (samplePropertiesViewModel == null)
                {
                    samplePropertiesViewModel = sample.ToPropertiesViewModel(_sampleRepositories);
                    samplePropertiesViewModel.Successful = true;
                    patchDocumentViewModel.SamplePropertiesDictionary[sample.ID] = samplePropertiesViewModel;
                }
                else
                {
                    SamplePropertiesRefresh(samplePropertiesViewModel);
                }
            }

            IEnumerable<int> existingIDs = patchDocumentViewModel.SamplePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = samples.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                patchDocumentViewModel.SamplePropertiesDictionary.Remove(idToDelete);
            }
        }

        private void SamplePropertiesDictionaryRefresh(DocumentViewModel documentViewModel)
        {
            Document document = _repositories.DocumentRepository.Get(documentViewModel.ID);
            IList<Sample> entities = document.Samples;

            foreach (Sample entity in entities)
            {
                SamplePropertiesViewModel viewModel = ViewModelSelector.TryGetSamplePropertiesViewModel(documentViewModel, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel(_sampleRepositories);
                    viewModel.Successful = true;
                    documentViewModel.SamplePropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    SamplePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = documentViewModel.SamplePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                documentViewModel.SamplePropertiesDictionary.Remove(idToDelete);
            }
        }

        private void SamplePropertiesRefresh(SamplePropertiesViewModel userInput)
        {
            SamplePropertiesViewModel viewModel = _samplePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ScaleGridRefresh()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Partial Action
            ScaleGridViewModel viewModel = _scaleGridPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void ScalePropertiesListRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Scale> entities = document.Scales;

            foreach (Scale entity in entities)
            {
                ScalePropertiesViewModel viewModel = ViewModelSelector.TryGetScalePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.ScalePropertiesList.Add(viewModel);
                }
                else
                {
                    ScalePropertiesRefresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = entities.Select(x => x.ID).ToHashSet();
            MainViewModel.Document.ScalePropertiesList = MainViewModel.Document.ScalePropertiesList
                                                                               .Where(x => idsToKeep.Contains(x.Entity.ID))
                                                                               .ToList();
        }

        private void ScalePropertiesRefresh(ScalePropertiesViewModel userInput)
        {
            ScalePropertiesViewModel viewModel = _scalePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void TitleBarRefresh()
        {
            int documentID = MainViewModel.Document.ID;
            Document document = _repositories.DocumentRepository.TryGet(documentID);
            string windowTitle = _titleBarPresenter.Show(document);
            MainViewModel.TitleBar = windowTitle;
        }

        private void ToneGridEditListRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Scale> entities = document.Scales;

            foreach (Scale entity in entities)
            {
                ToneGridEditViewModel viewModel = ViewModelSelector.TryGetToneGridEditViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToToneGridEditViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.ToneGridEditDictionary[entity.ID] = viewModel;
                }
                else
                {
                    ToneGridEditRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.ToneGridEditDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.ToneGridEditDictionary.Remove(idToDelete);
            }
        }

        private void ToneGridEditRefresh(ToneGridEditViewModel userInput)
        {
            ToneGridEditViewModel viewModel = _toneGridEditPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ToneGridEditRefresh(int scaleID)
        {
            ToneGridEditViewModel viewModel = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);
            ToneGridEditRefresh(viewModel);
        }

        private void UnderylingPatchLookupRefresh()
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> patches = rootDocument.ChildDocuments.SelectMany(x => x.Patches).ToArray();
            MainViewModel.Document.UnderlyingPatchLookup = ViewModelHelper.CreateUnderlyingPatchLookupViewModel(patches);
        }

        // Helpers

        private static void DeleteOperatorViewModels<TViewModel>(Dictionary<int, TViewModel> viewModelDictionary, IList<Operator> operators)
        {
            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);
            }
        }
    }
}
