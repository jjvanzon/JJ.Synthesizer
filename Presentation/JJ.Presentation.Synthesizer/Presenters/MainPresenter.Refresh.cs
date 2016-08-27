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
using JJ.Business.Synthesizer;

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

        private void CurveDetailsDictionaryRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Curve> entities = document.Curves;

            foreach (Curve entity in entities)
            {
                CurveDetailsViewModel viewModel = ViewModelSelector.TryGetCurveDetailsViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToDetailsViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.CurveDetailsDictionary[entity.ID] = viewModel;
                }
                else
                {
                    CurveDetailsRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.CurveDetailsDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.CurveDetailsDictionary.Remove(idToDelete);
            }
        }

        private void CurveDetailsNodeRefresh(int curveID, int nodeID)
        {
            CurveDetailsViewModel detailsViewModel = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

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

        private void CurveGridRefresh()
        {
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;
            CurveGridViewModel viewModel = _curveGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void CurveLookupRefresh()
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            // Business
            IList<UsedInDto> curveUsedInDtos = document.Curves
                                                            .Select(x => new UsedInDto
                                                            {
                                                                EntityIDAndName = x.ToIDAndName(),
                                                                UsedInIDAndNames = _documentManager.GetUsedIn(x)
                                                            })
                                                            .ToArray();
            // ToViewModel
            MainViewModel.Document.CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(curveUsedInDtos);
        }

        private void CurveLookupItemRefresh(int curveID)
        {
            Curve curve = _repositories.CurveRepository.Get(curveID);

            IDAndName idAndName = MainViewModel.Document.CurveLookup.Where(x => x.ID == curve.ID).FirstOrDefault();
            if (idAndName != null)
            {
                idAndName.Name = curve.Name;
                MainViewModel.Document.CurveLookup = MainViewModel.Document.CurveLookup.OrderBy(x => x.Name).ToList();
            }
        }

        private void CurvePropertiesDictionaryRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Curve> entities = document.Curves;

            foreach (Curve entity in entities)
            {
                CurvePropertiesViewModel viewModel = ViewModelSelector.TryGetCurvePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.CurvePropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    CurvePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.CurvePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.CurvePropertiesDictionary.Remove(idToDelete);
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
            AudioFileOutputPropertiesRefresh();
            AudioOutputPropertiesRefresh();
            AudioOutputPropertiesRefresh();
            CurrentPatchesRefresh();
            CurveDetailsDictionaryRefresh();
            CurveGridRefresh();
            CurveLookupRefresh();
            CurvePropertiesDictionaryRefresh();
            DocumentPropertiesRefresh();
            DocumentTreeRefresh();
            NodePropertiesDictionaryRefresh();
            OperatorPropertiesDictionary_ForBundles_Refresh();
            OperatorPropertiesDictionary_ForCaches_Refresh();
            OperatorPropertiesDictionary_ForCurves_Refresh();
            OperatorPropertiesDictionary_ForCustomOperators_Refresh();
            OperatorPropertiesDictionary_ForMakeContinuous_Refresh();
            OperatorPropertiesDictionary_ForNumbers_Refresh();
            OperatorPropertiesDictionary_ForPatchInlets_Refresh();
            OperatorPropertiesDictionary_ForPatchOutlets_Refresh();
            OperatorPropertiesDictionary_ForSamples_Refresh();
            OperatorPropertiesDictionary_WithDimension_Refresh();
            OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation_Refresh();
            OperatorPropertiesDictionary_WithDimensionAndInterpolation_Refresh();
            OperatorPropertiesDictionary_WithDimensionAndOutletCount_Refresh();
            OperatorPropertiesDictionary_WithInletCount_Refresh();
            OperatorPropertiesDictionaryRefresh();
            PatchDetailsDictionaryRefresh();
            PatchGridDictionaryRefresh();
            PatchPropertiesDictionaryRefresh();
            SampleGridRefresh();
            SampleLookupRefresh();
            SamplePropertiesDictionaryRefresh();
            ScaleGridRefresh();
            ScalePropertiesDictionaryRefresh();
            ToneGridEditDictionaryRefresh();
            UnderylingPatchLookupRefresh();

            // Note that AutoPatchDetails cannot be refreshed.
        }

        private void NodePropertiesDictionaryRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Node> entities = document.Curves.SelectMany(x => x.Nodes).ToArray();

            foreach (Node entity in entities)
            {
                NodePropertiesViewModel viewModel = ViewModelSelector.TryGetNodePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.NodePropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    NodePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.NodePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.NodePropertiesDictionary.Remove(idToDelete);
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
            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in
                MainViewModel.Document.OperatorPropertiesDictionary_ForCustomOperators.Values)
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

        private void OperatorPropertiesDictionary_ForBundles_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Bundle))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForBundle viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForBundle(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForBundle();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForBundles[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForBundle_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForBundles, operators);
        }

        private void OperatorPropertiesDictionary_ForCaches_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Cache))
                                                .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCache viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCache(_repositories.InterpolationTypeRepository);
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForCaches[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCache_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForCaches, operators);
        }

        private void OperatorPropertiesDictionary_ForCurves_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Curve))
                                                .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCurve viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCurve(_repositories.CurveRepository);
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForCurves[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCurve_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForCurves, operators);
        }

        private void OperatorPropertiesDictionary_ForCustomOperators_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.CustomOperator))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCustomOperator viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCustomOperator(_repositories.PatchRepository);
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForCustomOperators[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCustomOperator_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForCustomOperators, operators);
        }

        private void OperatorPropertiesDictionary_ForMakeContinuous_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.MakeContinuous))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForMakeContinuous viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForMakeContinuous(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForMakeContinuous();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForMakeContinuous[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForMakeContinuous_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForMakeContinuous, operators);
        }

        private void OperatorPropertiesDictionary_ForNumbers_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Number))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForNumber viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForNumber();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForNumbers[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForNumber_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForNumbers, operators);
        }

        private void OperatorPropertiesDictionary_ForPatchInlets_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.PatchInlet))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForPatchInlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForPatchInlet();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForPatchInlets[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForPatchInlet_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForPatchInlets, operators);
        }

        private void OperatorPropertiesDictionary_ForPatchOutlets_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForPatchOutlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForPatchOutlet();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForPatchOutlets[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForPatchOutlet_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForPatchOutlets, operators);
        }

        private void OperatorPropertiesDictionary_ForSamples_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Sample))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForSample viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_ForSamples[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForSample_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_ForSamples, operators);
        }

        private void OperatorPropertiesDictionary_WithDimension_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithDimension viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimension(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithDimension();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_WithDimension[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithDimension_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_WithDimension, operators);
        }

        private void OperatorPropertiesDictionary_WithDimensionAndInterpolation_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
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
                    MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndInterpolation[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithDimensionAndInterpolation_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndInterpolation, operators);
        }

        private void OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
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
                    MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithDimensionAndCollectionRecalculation_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation, operators);
        }

        private void OperatorPropertiesDictionary_WithDimensionAndOutletCount_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionAndOutletCountPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithDimensionAndOutletCount();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndOutletCount[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithDimensionAndOutletCount_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndOutletCount, operators);
        }

        private void OperatorPropertiesDictionary_WithInletCount_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithInletCountPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithInletCount viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInletCount(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithInletCount();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary_WithInletCount[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithInletCount_Refresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary_WithInletCount, operators);
        }

        private void OperatorPropertiesDictionaryRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithoutAlternativePropertiesView.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    MainViewModel.Document.OperatorPropertiesDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorPropertiesRefresh(viewModel);
                }
            }

            DeleteOperatorViewModels(MainViewModel.Document.OperatorPropertiesDictionary, operators);
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
            IEnumerable<PatchDetailsViewModel> patchDetailsViewModels = MainViewModel.Document.PatchDetailsDictionary.Values;

            IList<OperatorViewModel> operatorViewModels =
                MainViewModel.Document.PatchDetailsDictionary.Values
                                      .SelectMany(x => x.Entity.OperatorDictionary.Values)
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
            Operator op = _repositories.OperatorRepository.Get(operatorID);
            int patchID = op.Patch.ID;

            OperatorViewModel viewModel = ViewModelSelector.GetOperatorViewModel(MainViewModel.Document, patchID, operatorID);
            PatchDetails_RefreshOperator(viewModel);

            // TODO: Replace this with moving RefreshOperator to the PatchDetail presenter?
            PatchDetailsViewModel detailsViewModel = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);
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

        private void PatchDetailsDictionaryRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> entities = document.Patches;

            foreach (Patch entity in entities)
            {
                PatchDetailsViewModel viewModel = ViewModelSelector.TryGetPatchDetailsViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToDetailsViewModel(
                        _repositories.SampleRepository, 
                        _repositories.CurveRepository, 
                        _repositories.PatchRepository,
                        _entityPositionManager);

                    viewModel.Successful = true;
                    MainViewModel.Document.PatchDetailsDictionary[entity.ID] = viewModel;
                }
                else
                {
                    PatchDetailsRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.PatchDetailsDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.PatchDetailsDictionary.Remove(idToDelete);
            }
        }

        private void PatchDetailsRefresh(PatchDetailsViewModel userInput)
        {
            PatchDetailsViewModel viewModel = _patchDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchGridDictionaryRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            var patchManager = new PatchManager(_patchRepositories);
            IList<string> groups = patchManager.GetPatchGroupNames(document.Patches);

            // Always add nameless group even when there are no child documents in it.
            groups.Add(null);

            foreach (string group in groups)
            {
                PatchGridViewModel viewModel = ViewModelSelector.TryGetPatchGridViewModel_ByGroup(MainViewModel.Document, group);
                if (viewModel == null)
                {
                    IList<Patch> patchesInGroup = patchManager.GetPatchesInGroup_IncludingGroupless(document.Patches, group);
                    viewModel = patchesInGroup.ToPatchGridViewModel(document.ID, group);
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
            PatchGridViewModel viewModel = ViewModelSelector.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

            PatchGridRefresh(viewModel);
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
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            // Business
            var patchManager = new PatchManager(_patchRepositories);
            IList<Patch> grouplessPatches = patchManager.GetGrouplessPatches(document.Patches);
            IList<PatchGroupDto> patchGroupDtos = patchManager.GetPatchGroupDtos(document.Patches);

            // ToViewModel
            // Patch grids can be updated, created and deleted as group names are changed.
            // All the logic in CreatePatchGridViewModelDictionary is required for this.
            MainViewModel.Document.PatchGridDictionary = ViewModelHelper.CreatePatchGridViewModelDictionary(
                grouplessPatches,
                patchGroupDtos,
                document.ID);

            // DispatchViewModel
            foreach (PatchGridViewModel gridViewModel in MainViewModel.Document.PatchGridDictionary.Values.ToArray())
            {
                DispatchViewModel(gridViewModel);
            }
        }

        private void PatchPropertiesDictionaryRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> entities = document.Patches;

            foreach (Patch entity in entities)
            {
                PatchPropertiesViewModel viewModel = ViewModelSelector.TryGetPatchPropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();

                    viewModel.Successful = true;
                    MainViewModel.Document.PatchPropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    PatchPropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.PatchPropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.PatchPropertiesDictionary.Remove(idToDelete);
            }
        }

        private void PatchPropertiesRefresh(PatchPropertiesViewModel userInput)
        {
            PatchPropertiesViewModel viewModel = _patchPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void SampleGridRefresh()
        {
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;
            SampleGridViewModel viewModel = _sampleGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void SampleLookupRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            MainViewModel.Document.SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(document);
        }

        private void SampleLookupRefresh(int sampleID)
        {
            Sample sample = _repositories.SampleRepository.Get(sampleID);

            IDAndName idAndName = MainViewModel.Document.SampleLookup.Where(x => x.ID == sample.ID).FirstOrDefault();
            if (idAndName != null)
            {
                idAndName.Name = sample.Name;
                MainViewModel.Document.SampleLookup = MainViewModel.Document.SampleLookup.OrderBy(x => x.Name).ToList();
            }
        }

        private void SamplePropertiesDictionaryRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Sample> entities = document.Samples;

            foreach (Sample entity in entities)
            {
                SamplePropertiesViewModel viewModel = ViewModelSelector.TryGetSamplePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel(_sampleRepositories);
                    viewModel.Successful = true;
                    MainViewModel.Document.SamplePropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    SamplePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.SamplePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.SamplePropertiesDictionary.Remove(idToDelete);
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

        private void ScalePropertiesDictionaryRefresh()
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
                    MainViewModel.Document.ScalePropertiesDictionary[entity.ID] = viewModel;
                }
                else
                {
                    ScalePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = MainViewModel.Document.ScalePropertiesDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                MainViewModel.Document.ScalePropertiesDictionary.Remove(idToDelete);
            }
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

        private void ToneGridEditDictionaryRefresh()
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
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> patches = document.Patches.ToArray();
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
