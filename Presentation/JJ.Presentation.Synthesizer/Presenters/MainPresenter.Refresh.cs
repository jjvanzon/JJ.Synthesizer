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
                AudioFileOutputPropertiesViewModel viewModel = DocumentViewModelHelper.TryGetAudioFileOutputPropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.AudioFileOutputPropertiesList.Add(viewModel);
                }
                else
                {
                    AudioFileOutputPropertiesRefresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = entities.Select(x => x.ID).ToHashSet();
            MainViewModel.Document.AudioFileOutputPropertiesList = MainViewModel.Document.AudioFileOutputPropertiesList
                                                                                         .Where(x => idsToKeep.Contains(x.Entity.ID))
                                                                                         .ToList();
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

        private void CurveDetailsListRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Curve> curves = childDocument.Curves;

            foreach (Curve curve in curves)
            {
                CurveDetailsViewModel curveDetailsViewModel = DocumentViewModelHelper.TryGetCurveDetailsViewModel(MainViewModel.Document, curve.ID);
                if (curveDetailsViewModel == null)
                {
                    curveDetailsViewModel = curve.ToDetailsViewModel();
                    curveDetailsViewModel.Successful = true;
                    patchDocumentViewModel.CurveDetailsList.Add(curveDetailsViewModel);
                }
                else
                {
                    CurveDetailsRefresh(curveDetailsViewModel);
                }
            }

            HashSet<int> idsToKeep = curves.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.CurveDetailsList = patchDocumentViewModel.CurveDetailsList
                                                                            .Where(x => idsToKeep.Contains(x.ID))
                                                                            .ToList();
        }

        private void CurveDetailsListRefresh(DocumentViewModel documentViewModel)
        {
            Document document = _repositories.DocumentRepository.Get(documentViewModel.ID);
            IList<Curve> entities = document.Curves;

            foreach (Curve entity in entities)
            {
                CurveDetailsViewModel viewModel = DocumentViewModelHelper.TryGetCurveDetailsViewModel(documentViewModel, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToDetailsViewModel();
                    viewModel.Successful = true;
                    documentViewModel.CurveDetailsList.Add(viewModel);
                }
                else
                {
                    CurveDetailsRefresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = entities.Select(x => x.ID).ToHashSet();
            documentViewModel.CurveDetailsList = documentViewModel.CurveDetailsList
                                                                  .Where(x => idsToKeep.Contains(x.ID))
                                                                  .ToList();
        }

        private void CurveDetailsNodeRefresh(int nodeID)
        {
            // TODO: This is not very fast.
            CurveDetailsViewModel detailsViewModel = DocumentViewModelHelper.GetCurveDetailsViewModel_ByNodeID(MainViewModel.Document, nodeID);

            // Remove original node
            detailsViewModel.Nodes.RemoveFirst(x => x.ID == nodeID);

            // Add new version of the node
            Node node = _repositories.NodeRepository.Get(nodeID);
            NodeViewModel nodeViewModel = node.ToViewModel();
            detailsViewModel.Nodes.Add(nodeViewModel);

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

            foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentList)
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
                PatchDocumentViewModel patchDocumentViewModel = DocumentViewModelHelper.GetPatchDocumentViewModel(MainViewModel.Document, childDocument.ID);
                patchDocumentViewModel.CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(rootDocument, childDocument);
            }
        }

        private void CurvePropertiesListRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Curve> curves = childDocument.Curves;

            foreach (Curve curve in curves)
            {
                CurvePropertiesViewModel curvePropertiesViewModel = DocumentViewModelHelper.TryGetCurvePropertiesViewModel(MainViewModel.Document, curve.ID);
                if (curvePropertiesViewModel == null)
                {
                    curvePropertiesViewModel = curve.ToPropertiesViewModel();
                    curvePropertiesViewModel.Successful = true;
                    patchDocumentViewModel.CurvePropertiesList.Add(curvePropertiesViewModel);
                }
                else
                {
                    CurvePropertiesRefresh(curvePropertiesViewModel);
                }
            }

            HashSet<int> idsToKeep = curves.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.CurvePropertiesList = patchDocumentViewModel.CurvePropertiesList
                                                                               .Where(x => idsToKeep.Contains(x.ID))
                                                                               .ToList();
        }

        private void CurvePropertiesListRefresh(DocumentViewModel documentViewModel)
        {
            Document document = _repositories.DocumentRepository.Get(documentViewModel.ID);
            IList<Curve> entities = document.Curves;

            foreach (Curve entity in entities)
            {
                CurvePropertiesViewModel viewModel = DocumentViewModelHelper.TryGetCurvePropertiesViewModel(documentViewModel, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    documentViewModel.CurvePropertiesList.Add(viewModel);
                }
                else
                {
                    CurvePropertiesRefresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = entities.Select(x => x.ID).ToHashSet();
            documentViewModel.CurvePropertiesList = documentViewModel.CurvePropertiesList
                                                                     .Where(x => idsToKeep.Contains(x.ID))
                                                                     .ToList();
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
            CurveDetailsListRefresh(MainViewModel.Document);
            CurvePropertiesListRefresh(MainViewModel.Document);
            NodePropertiesListRefresh(MainViewModel.Document);
            PatchGridListsRefresh();
            SamplePropertiesListRefresh(MainViewModel.Document);
            ScalePropertiesListRefresh();
            ToneGridEditListRefresh();
            UnderylingPatchLookupRefresh();

            // Note that AutoPatchDetails cannot be refreshed.

            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            foreach (Document childDocument in rootDocument.ChildDocuments)
            {
                PatchDocumentViewModel patchDocumentViewModel = DocumentViewModelHelper.TryGetPatchDocumentViewModel(MainViewModel.Document, childDocument.ID);

                if (patchDocumentViewModel == null)
                {
                    patchDocumentViewModel = childDocument.ToPatchDocumentViewModel(_repositories, _entityPositionManager);

                    patchDocumentViewModel.CurveDetailsList.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.CurveGrid.Successful = true;
                    patchDocumentViewModel.CurvePropertiesList.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.NodePropertiesList.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesList.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesList_ForBundles.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesList_ForCaches.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesList_ForCurves.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesList_WithDimensions.ForEach(x => x.Successful = true);
                    patchDocumentViewModel.OperatorPropertiesList_ForFilters.ForEach(x => x.Successful = true);
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

                    MainViewModel.Document.PatchDocumentList.Add(patchDocumentViewModel);
                }
                else
                {
                    RefreshPatchDocument(patchDocumentViewModel);
                }
            }

            // Delete PatchDocumentViewModels
            HashSet<int> childDocumentIDsToKeep = rootDocument.ChildDocuments.Select(x => x.ID).ToHashSet();
            MainViewModel.Document.PatchDocumentList = MainViewModel.Document.PatchDocumentList
                                                                             .Where(x => childDocumentIDsToKeep.Contains(x.ChildDocumentID))
                                                                             .ToList();
        }

        private void NodePropertiesListRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Node> nodes = childDocument.Curves.SelectMany(x => x.Nodes).ToArray();

            foreach (Node node in nodes)
            {
                NodePropertiesViewModel nodePropertiesViewModel = DocumentViewModelHelper.TryGetNodePropertiesViewModel(MainViewModel.Document, node.ID);
                if (nodePropertiesViewModel == null)
                {
                    nodePropertiesViewModel = node.ToPropertiesViewModel();
                    nodePropertiesViewModel.Successful = true;
                    patchDocumentViewModel.NodePropertiesList.Add(nodePropertiesViewModel);
                }
                else
                {
                    NodePropertiesRefresh(nodePropertiesViewModel);
                }
            }

            HashSet<int> idsToKeep = nodes.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.NodePropertiesList = patchDocumentViewModel.NodePropertiesList
                                                                              .Where(x => idsToKeep.Contains(x.Entity.ID))
                                                                              .ToList();
        }

        private void NodePropertiesListRefresh(DocumentViewModel documentViewModel)
        {
            Document document = _repositories.DocumentRepository.Get(documentViewModel.ID);
            IList<Node> entities = document.Curves.SelectMany(x => x.Nodes).ToArray();

            foreach (Node entity in entities)
            {
                NodePropertiesViewModel viewModel = DocumentViewModelHelper.TryGetNodePropertiesViewModel(documentViewModel, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    documentViewModel.NodePropertiesList.Add(viewModel);
                }
                else
                {
                    NodePropertiesRefresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = entities.Select(x => x.ID).ToHashSet();
            documentViewModel.NodePropertiesList = documentViewModel.NodePropertiesList
                                                                    .Where(x => idsToKeep.Contains(x.Entity.ID))
                                                                    .ToList();
        }

        private void NodePropertiesRefresh(int nodeID)
        {
            NodePropertiesViewModel userInput = DocumentViewModelHelper.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);
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
                MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators).ToArray();

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in propertiesViewModelList)
            {
                OperatorProperties_ForCustomOperatorViewModel_Refresh(propertiesViewModel);
            }
        }

        private void OperatorProperties_WithDimension_Refresh(OperatorPropertiesViewModel_WithDimension userInput)
        {
            OperatorPropertiesViewModel_WithDimension viewModel = _operatorPropertiesPresenter_WithDimension.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForFilter_Refresh(OperatorPropertiesViewModel_ForFilter userInput)
        {
            OperatorPropertiesViewModel_ForFilter viewModel = _operatorPropertiesPresenter_ForFilter.Refresh(userInput);
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

        private void OperatorProperties_ForRandom_Refresh(OperatorPropertiesViewModel_ForRandom userInput)
        {
            OperatorPropertiesViewModel_ForRandom viewModel = _operatorPropertiesPresenter_ForRandom.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForResample_Refresh(OperatorPropertiesViewModel_ForResample userInput)
        {
            OperatorPropertiesViewModel_ForResample viewModel = _operatorPropertiesPresenter_ForResample.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForSample_Refresh(OperatorPropertiesViewModel_ForSample userInput)
        {
            OperatorPropertiesViewModel_ForSample viewModel = _operatorPropertiesPresenter_ForSample.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForUnbundle_Refresh(OperatorPropertiesViewModel_ForUnbundle userInput)
        {
            OperatorPropertiesViewModel_ForUnbundle viewModel = _operatorPropertiesPresenter_ForUnbundle.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorPropertiesList_ForBundles_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Bundle);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForBundle viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForBundle(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForBundle();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForBundles.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForBundle_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForBundles = patchDocumentViewModel.OperatorPropertiesList_ForBundles
                                                                                             .Where(x => idsToKeep.Contains(x.ID))
                                                                                             .ToList();
        }

        private void OperatorPropertiesList_ForCaches_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Cache);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCache viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCache(_repositories.InterpolationTypeRepository);
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForCaches.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForCache_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForCaches = patchDocumentViewModel.OperatorPropertiesList_ForCaches
                                                                                             .Where(x => idsToKeep.Contains(x.ID))
                                                                                             .ToList();
        }

        private void OperatorPropertiesList_ForCurves_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Curve);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCurve viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCurve(_repositories.CurveRepository);
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForCurves.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForCurve_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForCurves = patchDocumentViewModel.OperatorPropertiesList_ForCurves
                                                                                             .Where(x => idsToKeep.Contains(x.ID))
                                                                                             .ToList();
        }

        private void OperatorPropertiesList_ForCustomOperators_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.CustomOperator);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCustomOperator viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCustomOperator(_repositories.PatchRepository);
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForCustomOperator_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators = patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators
                                                                                                     .Where(x => idsToKeep.Contains(x.ID))
                                                                                                     .ToList();
        }

        private void OperatorPropertiesList_ForNumbers_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Number);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForNumber viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForNumber();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForNumbers.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForNumber_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForNumbers = patchDocumentViewModel.OperatorPropertiesList_ForNumbers
                                                                                             .Where(x => idsToKeep.Contains(x.ID))
                                                                                             .ToList();
        }

        private void OperatorPropertiesList_WithDimensions_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);

            IList<Operator> operators = childDocument.Patches[0].Operators
                                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithDimension viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_WithDimension(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithDimension();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_WithDimensions.Add(viewModel);
                }
                else
                {
                    OperatorProperties_WithDimension_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_WithDimensions = patchDocumentViewModel.OperatorPropertiesList_WithDimensions
                                                                                               .Where(x => idsToKeep.Contains(x.ID))
                                                                                               .ToList();
        }

        private void OperatorPropertiesList_ForFilters_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Filter);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForFilter viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForFilter(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForFilter();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForFilters.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForFilter_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForFilters = patchDocumentViewModel.OperatorPropertiesList_ForFilters
                                                                                               .Where(x => idsToKeep.Contains(x.ID))
                                                                                               .ToList();
        }

        private void OperatorPropertiesList_ForPatchInlets_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForPatchInlet viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForPatchInlet();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForPatchInlet_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets = patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets
                                                                                                 .Where(x => idsToKeep.Contains(x.ID))
                                                                                                 .ToList();
        }

        private void OperatorPropertiesList_ForPatchOutlets_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForPatchOutlet viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForPatchOutlet();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForPatchOutlet_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets = patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets
                                                                                                  .Where(x => idsToKeep.Contains(x.ID))
                                                                                                  .ToList();
        }

        private void OperatorPropertiesList_ForRandoms_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Random);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForRandom viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForRandom(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForRandom();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForRandoms.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForRandom_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForRandoms = patchDocumentViewModel.OperatorPropertiesList_ForRandoms
                                                                                             .Where(x => idsToKeep.Contains(x.ID))
                                                                                             .ToList();
        }

        private void OperatorPropertiesList_ForResamples_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Resample);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForResample viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForResample(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForResample();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForResamples.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForResample_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForResamples = patchDocumentViewModel.OperatorPropertiesList_ForResamples
                                                                                               .Where(x => idsToKeep.Contains(x.ID))
                                                                                               .ToList();
        }

        private void OperatorPropertiesList_ForSamples_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Sample);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForSample viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForSamples.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForSample_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForSamples = patchDocumentViewModel.OperatorPropertiesList_ForSamples
                                                                                             .Where(x => idsToKeep.Contains(x.ID))
                                                                                             .ToList();
        }

        private void OperatorPropertiesList_ForUnbundles_Refresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.Unbundle);

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForUnbundle viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel_ForUnbundle(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForUnbundle();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList_ForUnbundles.Add(viewModel);
                }
                else
                {
                    OperatorProperties_ForUnbundle_Refresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList_ForUnbundles = patchDocumentViewModel.OperatorPropertiesList_ForUnbundles
                                                                                               .Where(x => idsToKeep.Contains(x.ID))
                                                                                               .ToList();
        }

        private void OperatorPropertiesListRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Operator> operators = childDocument.Patches[0].Operators
                                                                .Where(x => !ViewModelHelper.OperatorTypeEnums_WithTheirOwnPropertyViews.Contains(x.GetOperatorTypeEnum()) &&
                                                                            !ViewModelHelper.OperatorTypeEnums_WithDimensionPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel viewModel = DocumentViewModelHelper.TryGetOperatorPropertiesViewModel(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    patchDocumentViewModel.OperatorPropertiesList.Add(viewModel);
                }
                else
                {
                    OperatorPropertiesRefresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = operators.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.OperatorPropertiesList = patchDocumentViewModel.OperatorPropertiesList
                                                                                  .Where(x => idsToKeep.Contains(x.ID))
                                                                                  .ToList();
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
            IList<PatchDetailsViewModel> patchDetailsViewModels = MainViewModel.Document.PatchDocumentList.Select(x => x.PatchDetails).ToArray();

            IList<OperatorViewModel> operatorViewModels =
                patchDetailsViewModels.SelectMany(x => x.Entity.Operators)
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
            OperatorViewModel viewModel = DocumentViewModelHelper.GetOperatorViewModel(MainViewModel.Document, operatorID);
            PatchDetails_RefreshOperator(viewModel);
        }

        private void PatchDetails_RefreshOperator(Operator entity, OperatorViewModel operatorViewModel)
        {
            // TODO: Not sure if I should also have a variation in which I call UpdateViewModel_WithoutEntityPosition instead.
            ViewModelHelper.RefreshViewModel_WithInletsAndOutlets_WithoutEntityPosition(
                entity, operatorViewModel,
                _repositories.SampleRepository, _repositories.CurveRepository, _repositories.PatchRepository);
        }

        private void PatchDetailsRefresh(PatchDetailsViewModel userInput)
        {
            PatchDetailsViewModel viewModel = _patchDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchGridListsRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            HashSet<string> groups = document.ChildDocuments.Select(x => x.GroupName ?? "")
                                                            .Distinct()
                                                            .ToHashSet();
            foreach (string group in groups)
            {
                PatchGridViewModel viewModel = DocumentViewModelHelper.TryGetPatchGridViewModel_ByGroup(MainViewModel.Document, group);
                if (viewModel == null)
                {
                    viewModel = document.ToPatchGridViewModel(group);
                    viewModel.Successful = true;
                    MainViewModel.Document.PatchGridList.Add(viewModel);
                }
                else
                {
                    PatchGridRefresh(viewModel);
                }
            }

            MainViewModel.Document.PatchGridList = MainViewModel.Document.PatchGridList
                                                                         .Where(x => groups.Contains(x.Group ?? ""))
                                                                         .ToList();
        }

        private void PatchGridRefresh(string group)
        {
            PatchGridViewModel viewModel2 = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

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
            // Patch grids can be updated, created and deleted as group names are changed.
            // All the logic in ToPatchGridViewModelList is required for this.
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            MainViewModel.Document.PatchGridList = document.ToPatchGridViewModelList();

            foreach (PatchGridViewModel gridViewModel in MainViewModel.Document.PatchGridList.ToArray())
            {
                DispatchViewModel(gridViewModel);
            }
        }

        private void PatchPropertiesRefresh(PatchPropertiesViewModel userInput)
        {
            PatchPropertiesViewModel viewModel = _patchPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void RefreshPatchDocument(PatchDocumentViewModel patchDocumentViewModel)
        {
            CurveGridRefresh(patchDocumentViewModel.CurveGrid);
            CurveLookupRefresh(patchDocumentViewModel);
            PatchDetailsRefresh(patchDocumentViewModel.PatchDetails);
            CurveDetailsListRefresh(patchDocumentViewModel);
            CurvePropertiesListRefresh(patchDocumentViewModel);
            NodePropertiesListRefresh(patchDocumentViewModel);

            OperatorPropertiesListRefresh(patchDocumentViewModel);
            OperatorPropertiesList_ForBundles_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForCaches_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForCurves_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForCustomOperators_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_WithDimensions_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForFilters_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForNumbers_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForPatchInlets_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForPatchOutlets_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForRandoms_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForResamples_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForSamples_Refresh(patchDocumentViewModel);
            OperatorPropertiesList_ForUnbundles_Refresh(patchDocumentViewModel);

            SamplePropertiesListRefresh(patchDocumentViewModel);

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
            SampleGridViewModel userInput = DocumentViewModelHelper.GetSampleGridViewModel_BySampleID(MainViewModel.Document, sampleID);
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentList)
            {
                IDAndName idAndName2 = patchDocumentViewModel.SampleLookup.Where(x => x.ID == sample.ID).FirstOrDefault();
                if (idAndName2 != null)
                {
                    idAndName2.Name = sample.Name;
                    patchDocumentViewModel.SampleLookup = patchDocumentViewModel.SampleLookup.OrderBy(x => x.Name).ToList();
                }
            }
        }

        private void SamplePropertiesListRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            IList<Sample> samples = childDocument.Samples;

            foreach (Sample sample in samples)
            {
                SamplePropertiesViewModel samplePropertiesViewModel = DocumentViewModelHelper.TryGetSamplePropertiesViewModel(MainViewModel.Document, sample.ID);
                if (samplePropertiesViewModel == null)
                {
                    samplePropertiesViewModel = sample.ToPropertiesViewModel(_sampleRepositories);
                    samplePropertiesViewModel.Successful = true;
                    patchDocumentViewModel.SamplePropertiesList.Add(samplePropertiesViewModel);
                }
                else
                {
                    SamplePropertiesRefresh(samplePropertiesViewModel);
                }
            }

            HashSet<int> idsToKeep = samples.Select(x => x.ID).ToHashSet();
            patchDocumentViewModel.SamplePropertiesList = patchDocumentViewModel.SamplePropertiesList
                                                                               .Where(x => idsToKeep.Contains(x.Entity.ID))
                                                                               .ToList();
        }

        private void SamplePropertiesListRefresh(DocumentViewModel documentViewModel)
        {
            Document document = _repositories.DocumentRepository.Get(documentViewModel.ID);
            IList<Sample> entities = document.Samples;

            foreach (Sample entity in entities)
            {
                SamplePropertiesViewModel viewModel = DocumentViewModelHelper.TryGetSamplePropertiesViewModel(documentViewModel, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel(_sampleRepositories);
                    viewModel.Successful = true;
                    documentViewModel.SamplePropertiesList.Add(viewModel);
                }
                else
                {
                    SamplePropertiesRefresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = entities.Select(x => x.ID).ToHashSet();
            documentViewModel.SamplePropertiesList = documentViewModel.SamplePropertiesList
                                                                      .Where(x => idsToKeep.Contains(x.Entity.ID))
                                                                      .ToList();
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
                ScalePropertiesViewModel viewModel = DocumentViewModelHelper.TryGetScalePropertiesViewModel(MainViewModel.Document, entity.ID);
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
                ToneGridEditViewModel viewModel = DocumentViewModelHelper.TryGetToneGridEditViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToToneGridEditViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.ToneGridEditList.Add(viewModel);
                }
                else
                {
                    ToneGridEditRefresh(viewModel);
                }
            }

            HashSet<int> idsToKeep = entities.Select(x => x.ID).ToHashSet();
            MainViewModel.Document.ToneGridEditList = MainViewModel.Document.ToneGridEditList
                                                                            .Where(x => idsToKeep.Contains(x.ScaleID))
                                                                            .ToList();
        }

        private void ToneGridEditRefresh(ToneGridEditViewModel userInput)
        {
            ToneGridEditViewModel viewModel = _toneGridEditPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ToneGridEditRefresh(int scaleID)
        {
            ToneGridEditViewModel viewModel = DocumentViewModelHelper.GetToneGridEditViewModel(MainViewModel.Document, scaleID);
            ToneGridEditRefresh(viewModel);
        }

        private void UnderylingPatchLookupRefresh()
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> patches = rootDocument.ChildDocuments.SelectMany(x => x.Patches).ToArray();
            MainViewModel.Document.UnderlyingPatchLookup = ViewModelHelper.CreateUnderlyingPatchLookupViewModel(patches);
        }
    }
}
